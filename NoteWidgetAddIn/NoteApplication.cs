// Copyright (c) Efrey Kong. All Rights Reserved.
// Licensed under the Apache License, Version 2.0.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Microsoft.Office.Interop.OneNote;
using NLog;
using NoteWidgetAddIn.Model;

#pragma warning disable CS3001 // Type is not CLS-compliant
namespace NoteWidgetAddIn
{
    public sealed class NoteApplication : IDisposable
    {
        #region Helpers
        private class SimpleWin32Window : System.Windows.Forms.IWin32Window
        {
            public SimpleWin32Window(long windowHandle)
            {
                Handle = new IntPtr(windowHandle);
            }
            public IntPtr Handle { get; }
        }
        
        public static System.Windows.Forms.IWin32Window CreateWin32Window(long handle)
        {
            return new SimpleWin32Window(handle);
        }
        #endregion

        private readonly NoteApplicationContext _context;
        private IApplication _application;
        internal NoteApplication(NoteApplicationContext context)
        {
            _context = context;
            _application = context.ResisteredApplicationType == null? new Application() :
                (IApplication)Activator.CreateInstance(_context.ResisteredApplicationType);
        }

        public string CurrentNotebookID => _application?.Windows.CurrentWindow?.CurrentNotebookId;
        public string CurrentSectionGroupID => _application?.Windows.CurrentWindow?.CurrentSectionGroupId;
        public string CurrentSectionID => _application?.Windows.CurrentWindow?.CurrentSectionId;
        public string CurrentPageID => _application?.Windows.CurrentWindow?.CurrentPageId;
        public System.Windows.Forms.IWin32Window CreateCurrentWin32Window() => CreateWin32Window((long)(_application?.Windows.CurrentWindow?.WindowHandle ?? 0));

        #region NoteNode
        public ICollection<NoteNode> GetAllNotebookHierarchy()
        {
            var xmlOut = GetHierarchy(null, HierarchyScope.hsPages);

            if (!string.IsNullOrEmpty(xmlOut))
            {
                return NoteNode.HierarchicalNodesFrom(xmlOut);
            }

            return new List<NoteNode>();
        }
        public NoteNode GetNoteNodeHierarchy(string startNodeId)
        {
            if (string.IsNullOrEmpty(startNodeId))
                throw new ArgumentNullException("startNodeId");

            _application.GetHierarchy(startNodeId, HierarchyScope.hsPages, out var xmlOut);

            if (!string.IsNullOrEmpty(xmlOut))
            {
                return NoteNode.HierarchicalNodesFrom(xmlOut).FirstOrDefault();
            }
            return null;
        }
        #endregion

        #region Hierarchy
        /// <summary>
        /// Returns xml hierarchy for specified startNodeID. Returns all notebooks hierarchy if startNodeID is null.
        /// </summary>
        /// <param name="startNodeID">An ID of a Notebook/Section Group/Section/Page.</param>
        /// <param name="scope"></param>
        /// <returns></returns>
        public string GetHierarchy(string startNodeID, HierarchyScope scope)
        {
            _application.GetHierarchy(startNodeID, scope, out var xmlOut);
            return xmlOut;
        }
        #endregion

        #region Page Content
        public void NewMarkdownPage()
        {
            var sectionID = CurrentSectionID;
            if (sectionID == null)
            {
                throw new InvalidOperationException("No Section is currently viewed.");
            }
            _application.CreateNewPage(sectionID, out var newPageID);
            var page = GetNotePage(newPageID);
            page.SetMarkdownFlag();
            UpdatePage(page);
        }
        public NotePage GetCurrentNotePage(NotePageInfo pageInfo = NotePageInfo.All)
        {
            return GetNotePage(CurrentPageID, pageInfo);
        }
        public NotePage GetNotePage(string pageID, NotePageInfo pageInfo = NotePageInfo.All)
        {
            ExceptionAssertion.ThrowArgumentNullExceptionIfNull(pageID, nameof(pageID));
            _application.GetPageContent(pageID, out var xmlOut, (PageInfo)pageInfo, XMLSchema.xs2013);
            if (!string.IsNullOrEmpty(xmlOut))
            {
                return new NotePage(XElement.Parse(xmlOut));
            }
            return null;
        }

        public void DeletePageContent(string pageID, string objectID)
        {
            _application.DeletePageContent(pageID, objectID);
        }

        public void UpdatePage(NotePage page)
        {
            _application.UpdatePageContent(page.Root.ToString(SaveOptions.DisableFormatting), DateTime.MinValue, XMLSchema.xs2013, true);
        }
        #endregion

        #region Export
        public void Publish(string nodeID, PublishFormat format, string filePath)
        {
            ExceptionAssertion.ThrowArgumentNullExceptionIfNull(nodeID, nameof(nodeID));
            ExceptionAssertion.ThrowArgumentNullExceptionIfNull(filePath, nameof(filePath));
            _application.Publish(nodeID, filePath, format);
        }
        #endregion

        #region IDisposable
        public void Dispose()
        {
            _application = null;
        }
        #endregion        
    }
}
