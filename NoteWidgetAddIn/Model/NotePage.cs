// Copyright (c) Efrey Kong. All Rights Reserved.
// Licensed under the Apache License, Version 2.0.

using System;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Web;
using System.Xml.Linq;

namespace NoteWidgetAddIn.Model
{
    public class NotePage
    {
        #region Element class
        public class NoteTextRange
        {
            public NoteTextRange(XElement xe)
            {
                ExceptionAssertion.ThrowArgumentNullExceptionIfNull(xe, nameof(xe));
                if (xe.Name.LocalName != "T")
                {
                    throw new InvalidCastException($"Invalid element: {xe.Name}. ");
                }
                Root = xe;
            }
            public XElement Root { get; }
            /// <summary>
            /// Raw value of <one:T> element. 
            /// May contains formated styles. E.g.<one:T><![CDATA[<span style='font-weight:bold'>function to create embedded image string as base64</span>]]></one:T>
            /// </summary>
            public string Value
            {
                get
                {
                    return Root.Value;
                }
            }
            /// <summary>
            /// Inner Text of TextRange (<one:T>) element.
            /// Formated styles is removed.
            /// </summary>
            public string InnerText
            {
                get
                {
                    var htmlContent = Root.Value;
                    if (string.IsNullOrEmpty(htmlContent))
                    {
                        return htmlContent;
                    }
                    var htmlDoc = new HtmlAgilityPack.HtmlDocument();
                    htmlDoc.LoadHtml(htmlContent);
                    return HttpUtility.HtmlDecode(htmlDoc.DocumentNode.InnerText);
                }
            }
        }
        public class NoteImage
        {
            public NoteImage(XElement xe)
            {
                ExceptionAssertion.ThrowArgumentNullExceptionIfNull(xe, nameof(xe));
                if (xe.Name.LocalName != "Image")
                {
                    throw new InvalidCastException($"Invalid element: {xe.Name}. ");
                }
                Root = xe;
            }
            public XElement Root { get; }
            /// <summary>
            /// Image format. Png, Jpg etc.
            /// </summary>
            public string Format
            {
                get
                {
                    return Root.Attribute("format")?.Value;
                }
            }
            public Size Size
            {
                get
                {
                    var e = Root.Element(Root.Name.Namespace + "Size");
                    if (e != null)
                    {
                        return new Size(int.Parse(e.Attribute("width")?.Value ?? "0"), int.Parse(e.Attribute("height")?.Value ?? "0"));
                    }
                    return new Size(0, 0);
                }
            }
            public string Data
            {
                get
                {
                    return Root.Element(Root.Name.Namespace + "Data")?.Value?.Replace("\r", "").Replace("\n", "");
                }
            }
            public string ToBase64Image()
            {
                return $"data:image/{Format};base64,{Data}";
            }
            public void Save(string imageFilePath)
            {
                System.IO.File.WriteAllBytes(imageFilePath, Convert.FromBase64String(Data));
            }
        }
        #endregion

        public const string MarkdownFlag = "IsMarkdownPage";
        public NotePage(XElement xelement)
        {
            ExceptionAssertion.ThrowArgumentNullExceptionIfNull(xelement, nameof(xelement));
            if (xelement.Name.LocalName != "Page")
            {
                throw new InvalidOperationException($"Invalid argument: {xelement.Name}");
            }
            Root = xelement;
            PageID = Root.Attribute("ID").Value;
            LastModifiedTime = Root.Attribute("lastModifiedTime").Value;
            Namespace = Root.Name.Namespace;
        }
        public string PageID { get;}
        public string LastModifiedTime { get; }
        public XElement Root { get;}
        /// <summary>
        /// Root Namespace
        /// </summary>
        public XNamespace Namespace { get; }
        public NoteTextRange Title
        {
            get
            {
                var oe = Root.Descendants(Namespace + "Title").FirstOrDefault()?.Descendants(Namespace + "OE").FirstOrDefault();
                XElement tr = new XElement(Namespace + "T", new XCData(oe?.Value));
                return new NoteTextRange(tr);
            }
            set
            {
                var toe = Root.Descendants(Namespace + "Title").FirstOrDefault().Descendants(Namespace + "OE").FirstOrDefault();
                toe?.ReplaceNodes(new XElement(Namespace + "T", new XCData(value.Value)));
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ContentInnerText
        {
            get
            {
                StringBuilder contentBuilder = new StringBuilder();
                AppendElementsInnerText(contentBuilder, Root);
                return contentBuilder.ToString().TrimEnd('\r', '\n');
            }
        }
        private void AppendElementsInnerText(StringBuilder appender, XElement container)
        {
            foreach(var item in container.Elements())
            {
                if (item.Name.LocalName == "Outline")
                {
                    AppendElementsInnerText(appender, item.Element(Namespace + "OEChildren"));
                }
                else if (item.Name.LocalName == "Image")
                {
                    var ni = new NoteImage(item);
                    //TODO: Large image (more than 2MB) should be saved to file, not embedded in html content.
                    appender.AppendLine($"![]({ni.ToBase64Image()})");
                }
                else if (item.Name.LocalName == "Table" && item.Attribute("hasHeaderRow")?.Value == "false")
                {
                    foreach(var oe in item.Descendants(Namespace + "OE"))
                    {
                        if (item.Elements(Namespace + "T").Count() > 0)
                        {
                            var tr = new NoteTextRange(new XElement(Namespace + "T", new XCData(item.Value)));
                            appender.AppendLine(tr.InnerText);
                        }
                        else
                        {
                            AppendElementsInnerText(appender, item);
                        }
                    }
                }
                else if (item.Name.LocalName == "OE")
                {
                    if (item.Elements(Namespace + "T").Count() > 0)
                    {
                        var tr = new NoteTextRange(new XElement(Namespace + "T", new XCData(item.Value)));
                        appender.AppendLine(tr.InnerText);
                    }
                    else
                    {
                        AppendElementsInnerText(appender, item);
                    }
                }
                
            }
        }
        public void AddCustomTagToTitle(string tagName, string symbol)
        {
            var index = 10000.ToString();
            Root.AddFirst(
                new XElement(Namespace + "TagDef",
                    new XAttribute("index", index),
                    new XAttribute("type", "0"),
                    new XAttribute("symbol", symbol),
                    new XAttribute("name", tagName)
                ));
            var tag = new XElement(Namespace + "Tag",
                    new XAttribute("index", index),
                    new XAttribute("completed", "true"),
                    new XAttribute("disabled", "false")
                );
            Root.Element(Namespace + "Title").Element(Namespace + "OE").AddFirst(tag);
        }
        public void AddCustomMeta(string name, string value)
        {
            if (!Root.Elements(Namespace + "Meta").Any(e => e.Attribute("name")?.Value == MarkdownFlag))
            {
                Root.AddFirst(new XElement(Namespace + "Meta",
                    new XAttribute("name", name),
                    new XAttribute("content", value)));
            }
        }
        public void SetMarkdownFlag()
        {
            AddCustomMeta(MarkdownFlag, "true");
        }
        public override string ToString()
        {
            return $"NotePage PageID:{PageID}";
        }
    }
}
