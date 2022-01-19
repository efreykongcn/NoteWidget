// Copyright (c) Efrey Kong. All Rights Reserved.
// Licensed under the Apache License, Version 2.0.

using Microsoft.Office.Interop.OneNote;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteWidgetAddIn.Model
{
    public enum NotePageInfo
    {
        /// <summary>
        /// Returns only basic page content, without selection markup, file types for binary data objects and binary data objects. 
        /// This is the standard value to pass.
        /// Value = 0
        /// </summary>
        Basic = PageInfo.piBasic,
        /// <summary>
        /// Returns page content with no selection markup, but with all binary data.
        /// Value = 1
        /// </summary>
        BinaryData = PageInfo.piBinaryData,
        /// <summary>
        /// Returns page content with selection markup, but no binary data.
        /// Value = 2
        /// </summary>
        Selection = PageInfo.piSelection,
        /// <summary>
        /// Returns page content with selection markup and all binary data.
        /// Value = 3
        /// </summary>
        BinaryDataSelection = PageInfo.piBinaryDataSelection,
        /// <summary>
        /// Returns page content with file type info for binary data objects.
        /// Value = 4
        /// </summary>        
        FileType = PageInfo.piFileType,
        /// <summary>
        /// Returns page content with file type info for binary data objects and binary data objects
        /// Value = 5
        /// </summary>
        BinaryDataFileType = PageInfo.piBinaryDataFileType,
        /// <summary>
        /// Returns page content with selection markup and file type info for binary data.
        /// Value = 6
        /// </summary>
        SelectionFileType = PageInfo.piSelectionFileType,
        /// <summary>
        /// Returns all page content.
        /// Value = 7
        /// </summary>
        All = PageInfo.piAll,
    }
}
