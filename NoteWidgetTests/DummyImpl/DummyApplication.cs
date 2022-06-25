using Microsoft.Office.Interop.OneNote;
using NoteWidgetAddIn.Model;
using System;
using System.Linq;
using System.Xml.Linq;

namespace NoteWidgetAddIn
{
    public class DummyApplication : IApplication
    {
        private Windows _windows;
        private XDocument _xdoc;
        public DummyApplication()
        {
            _xdoc = XDocument.Parse(DummyData.GetTargetedFileXmlContent(NodeType.Notebook));
            var win = new DummyWindow(this, _xdoc);
            _windows = new DummyWindows(win);
        }

        public Windows Windows => _windows;
        public void GetHierarchy(string bstrStartNodeID, HierarchyScope hsScope, out string pbstrHierarchyXmlOut, XMLSchema xsSchema = XMLSchema.xs2013)
        {
            if (bstrStartNodeID == null)
            {
                pbstrHierarchyXmlOut = _xdoc.ToString();
            }
            else
            {
                var xe = _xdoc.Descendants().Where(e => e.Attribute("ID")?.Value == bstrStartNodeID).FirstOrDefault();
                pbstrHierarchyXmlOut = xe?.ToString();
            }
        }

        public void GetPageContent(string bstrPageID, out string pbstrPageXmlOut, PageInfo pageInfoToExport = PageInfo.piBasic, XMLSchema xsSchema = XMLSchema.xs2013)
        {
            ExceptionAssertion.ThrowArgumentNullExceptionIfNull(bstrPageID, nameof(bstrPageID));
            pbstrPageXmlOut = DummyData.GetFileContent($"{bstrPageID}.xml");
        }

        #region Not Implemented members
        public void UpdateHierarchy(string bstrChangesXmlIn, XMLSchema xsSchema = XMLSchema.xs2013)
        {
            throw new NotImplementedException();
        }

        public void OpenHierarchy(string bstrPath, string bstrRelativeToObjectID, out string pbstrObjectID, CreateFileType cftIfNotExist = CreateFileType.cftNone)
        {
            throw new NotImplementedException();
        }

        public void DeleteHierarchy(string bstrObjectID, DateTime dateExpectedLastModified, bool deletePermanently = false)
        {
            throw new NotImplementedException();
        }

        public void CreateNewPage(string bstrSectionID, out string pbstrPageID, NewPageStyle npsNewPageStyle = NewPageStyle.npsDefault)
        {
            throw new NotImplementedException();
        }

        public void CloseNotebook(string bstrNotebookID, bool force = false)
        {
            throw new NotImplementedException();
        }

        public void GetHierarchyParent(string bstrObjectID, out string pbstrParentID)
        {
            throw new NotImplementedException();
        }

        public void UpdatePageContent(string bstrPageChangesXmlIn, DateTime dateExpectedLastModified, XMLSchema xsSchema = XMLSchema.xs2013, bool force = false)
        {
            throw new NotImplementedException();
        }

        public void GetBinaryPageContent(string bstrPageID, string bstrCallbackID, out string pbstrBinaryObjectB64Out)
        {
            throw new NotImplementedException();
        }

        public void DeletePageContent(string bstrPageID, string bstrObjectID, DateTime dateExpectedLastModified, bool force = false)
        {
            throw new NotImplementedException();
        }

        public void NavigateTo(string bstrHierarchyObjectID, string bstrObjectID = "", bool fNewWindow = false)
        {
            throw new NotImplementedException();
        }

        public void NavigateToUrl(string bstrUrl, bool fNewWindow = false)
        {
            throw new NotImplementedException();
        }

        public void Publish(string bstrHierarchyID, string bstrTargetFilePath, PublishFormat pfPublishFormat = PublishFormat.pfOneNote, string bstrCLSIDofExporter = "")
        {
            throw new NotImplementedException();
        }

        public void OpenPackage(string bstrPathPackage, string bstrPathDest, out string pbstrPathOut)
        {
            throw new NotImplementedException();
        }

        public void GetHyperlinkToObject(string bstrHierarchyID, string bstrPageContentObjectID, out string pbstrHyperlinkOut)
        {
            throw new NotImplementedException();
        }

        public void FindPages(string bstrStartNodeID, string bstrSearchString, out string pbstrHierarchyXmlOut, bool fIncludeUnindexedPages = false, bool fDisplay = false, XMLSchema xsSchema = XMLSchema.xs2013)
        {
            throw new NotImplementedException();
        }

        public void FindMeta(string bstrStartNodeID, string bstrSearchStringName, out string pbstrHierarchyXmlOut, bool fIncludeUnindexedPages = false, XMLSchema xsSchema = XMLSchema.xs2013)
        {
            throw new NotImplementedException();
        }

        public void GetSpecialLocation(SpecialLocation slToGet, out string pbstrSpecialLocationPath)
        {
            throw new NotImplementedException();
        }

        public void MergeFiles(string bstrBaseFile, string bstrClientFile, string bstrServerFile, string bstrTargetFile)
        {
            throw new NotImplementedException();
        }

        public IQuickFilingDialog QuickFiling()
        {
            throw new NotImplementedException();
        }

        public void SyncHierarchy(string bstrHierarchyID)
        {
            throw new NotImplementedException();
        }

        public void SetFilingLocation(FilingLocation flToSet, FilingLocationType fltToSet, string bstrFilingSectionID)
        {
            throw new NotImplementedException();
        }

        public void MergeSections(string bstrSectionSourceId, string bstrSectionDestinationId)
        {
            throw new NotImplementedException();
        }

        public void GetWebHyperlinkToObject(string bstrHierarchyID, string bstrPageContentObjectID, out string pbstrHyperlinkOut)
        {
            throw new NotImplementedException();
        }

        public bool Dummy1 => throw new NotImplementedException();

        public dynamic COMAddIns => throw new NotImplementedException();

        public dynamic LanguageSettings => throw new NotImplementedException();
        #endregion
    }
}
