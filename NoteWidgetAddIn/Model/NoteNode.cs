// Copyright (c) Efrey Kong. All Rights Reserved.
// Licensed under the Apache License, Version 2.0.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace NoteWidgetAddIn.Model
{
    public class NoteNode
    {
        public NoteNode(NoteNode parent, NodeType nodeType)
        {
            Parent = parent;
            NodeType = nodeType;

            Children = new List<NoteNode>();
        }

        public NoteNode Parent { get; private set; }
        public NodeType NodeType { get; private set; }
        public bool Colored
        {
            get
            {
                bool result = false;
                switch (NodeType)
                {
                    case NodeType.Notebook:
                    case NodeType.Section:
                        result = true;
                        break;
                }

                return result;
            }
        }

        public string ID { get; set; }
        public string Name { get; set; }
        public string Nickname { get; set; }
        public string CreatedTime { get; set; }
        public string LastModifiedTime { get; set; }
        public bool IsCurrentlyViewed { get; set; }
        public string Color { get; set; }
        public string Path { get; set; }
        public int PageLevel { get; set; }

        public ICollection<NoteNode> Children { get; protected set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="xdoc"></param>
        /// <returns></returns>
        public static ICollection<NoteNode> HierarchicalNodesFrom(string xmlContent)
        {
            ExceptionAssertion.ThrowArgumentNullExceptionIfNull(xmlContent, nameof(xmlContent));
            XDocument xdoc = XDocument.Parse(xmlContent);
            ICollection<NoteNode> result = new List<NoteNode>();

            if (xdoc != null)
            {
                var localName = xdoc.Root.Name.LocalName == "Notebooks" ? "Notebook" : xdoc.Root.Name.LocalName;
                var nodeType = (NodeType)Enum.Parse(typeof(NodeType), localName);
                var ns = xdoc.Root.Name.Namespace;
                foreach (var xe in xdoc.Descendants(ns + nodeType.ToString()))
                {
                    var node = CreateNoteNode(nodeType, null, xe);
                    result.Add(node);

                    if (nodeType != NodeType.Page)
                    {
                        LoadChildren(node, xdoc);
                    }
                }
            }

            return result;
        }
        private static void LoadChildren(NoteNode parent, XDocument xdoc)
        {
            var desendants = xdoc.Descendants()
                                 .Where(e => e.Parent?.Attribute("ID")?.Value == parent.ID
                                          && !(e.Attribute("isRecycleBin")?.Value == "true" || e.Attribute("isInRecycleBin")?.Value == "true"));
            foreach (var xe in desendants)
            {
                var nodeType = (NodeType)Enum.Parse(typeof(NodeType), xe.Name.LocalName);

                var node = CreateNoteNode(nodeType, parent, xe);
                if (node.NodeType != NodeType.Page)
                {
                    parent.Children.Add(node);
                    LoadChildren(node, xdoc);
                }
                else
                {
                    if (node.PageLevel == 1)
                    {
                        parent.Children.Add(node);
                    }
                    else
                    {
                        NoteNode parentPage = parent.Children.LastOrDefault();
                        NoteNode last = parentPage;
                        while (true)
                        {
                            if (node.PageLevel - parentPage.PageLevel == 1 || last == null)
                            {
                                parentPage.Children.Add(node);
                                break;
                            }
                            else if (last.Children.Count == 0)
                            {
                                last.Children.Add(node);
                                break;
                            }

                            if (parentPage.ID != last.ID)
                            {
                                parentPage = last;
                            }

                            last = parentPage.Children.LastOrDefault();
                        }
                    }
                }
            }
        }
        private static NoteNode CreateNoteNode(NodeType nodeType, NoteNode parent, XElement xelement)
        {
            var node = new NoteNode(parent, nodeType);
            node.ID = xelement.Attribute("ID")?.Value;
            node.Name = xelement.Attribute("name")?.Value;
            node.LastModifiedTime = xelement.Attribute("lastModifiedTime")?.Value;
            node.IsCurrentlyViewed = xelement.Attribute("isCurrentlyViewed")?.Value == "true";

            if (nodeType == NodeType.Notebook)
            {
                node.Color = xelement.Attribute("color")?.Value;
                node.Path = xelement.Attribute("path")?.Value;
                node.Nickname = xelement.Attribute("nickname")?.Value;
            }
            else if (nodeType == NodeType.SectionGroup)
            {
                node.Path = xelement.Attribute("path")?.Value;
            }
            else if (nodeType == NodeType.Section)
            {
                node.Color = xelement.Attribute("color")?.Value;
                node.Path = xelement.Attribute("path")?.Value;
            }
            else if (nodeType == NodeType.Page)
            {
                node.CreatedTime = xelement.Attribute("dateTime")?.Value;
                node.PageLevel = int.Parse(xelement.Attribute("pageLevel")?.Value ?? "1");
            }

            return node;
        }

        #region EmptySequence
        private static IEnumerable<NoteNode> s_emptySequence;
        public static IEnumerable<NoteNode> EmptySequence
        {
            get
            {
                if (s_emptySequence == null)
                {
                    s_emptySequence = new NoteNode[0];
                }
                return s_emptySequence;
            }
        }
        #endregion
    }
}
