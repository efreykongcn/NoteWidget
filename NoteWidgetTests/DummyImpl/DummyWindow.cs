using Microsoft.Office.Interop.OneNote;
using NoteWidgetAddIn.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace NoteWidgetAddIn
{
    public class DummyWindow : Window
    {
        private XDocument _xdoc;

        public DummyWindow(IApplication app, XDocument xdoc)
        {
            ExceptionAssertion.ThrowArgumentNullExceptionIfNull(xdoc, nameof(xdoc));
            _xdoc = xdoc;
        }

        #region Implemented members
        public ulong WindowHandle => (ulong)Process.GetCurrentProcess().MainWindowHandle.ToInt64();

        public string CurrentPageId => GetCurrentlyElementId(NodeType.Page);

        public string CurrentSectionId => GetCurrentlyElementId(NodeType.Section);

        public string CurrentSectionGroupId => GetCurrentlyElementId(NodeType.SectionGroup);

        public string CurrentNotebookId => GetCurrentlyElementId(NodeType.Notebook);
        public IApplication Application { get; private set; }

        public bool Active { get => true; set { } }

        private string GetCurrentlyElementId(NodeType nodeType)
        {
            var xe = _xdoc.Descendants(_xdoc.Root.Name.Namespace + nodeType.ToString())
                          .Where(e => e.Attribute("isCurrentlyViewed")?.Value == "true")
                          .FirstOrDefault();

            if (xe != null && xe.Attribute("ID") != null)
            {
                return xe.Attribute("ID").Value;
            }

            return null;
        }

        #endregion

        #region Not Implemented members
        public void NavigateTo(string bstrHierarchyObjectID, string bstrObjectID = "")
        {
            throw new NotImplementedException();
        }

        public void NavigateToUrl(string bstrUrl)
        {
            throw new NotImplementedException();
        }

        public void SetDockedLocation(DockLocation DockLocation, tagPOINT ptMonitor)
        {
            
            throw new NotImplementedException();
        }

        public bool FullPageView { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public DockLocation DockedLocation { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public bool SideNote => throw new NotImplementedException();
        #endregion
    }
}
