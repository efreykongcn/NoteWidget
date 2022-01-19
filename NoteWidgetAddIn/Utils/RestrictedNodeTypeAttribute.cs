// Copyright (c) Efrey Kong. All Rights Reserved.
// Licensed under the Apache License, Version 2.0.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NoteWidgetAddIn.Model;

namespace NoteWidgetAddIn
{
    public class RestrictedNodeTypeAttribute : Attribute
    {
        public RestrictedNodeTypeAttribute()
        {
        }
        public RestrictedNodeTypeAttribute(params NodeType[] nodeTypes)
        {
            NoteTypes = nodeTypes;
        }

        public NodeType[] NoteTypes { get; set; }
    }
}
