// Copyright (c) Efrey Kong. All Rights Reserved.
// Licensed under the Apache License, Version 2.0.

using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using Markdig;

namespace NoteWidgetAddIn.Markdown
{
    public class HtmlTemplate
    {
        static HtmlTemplate()
        {
            BuildDefaultTemplates();
        }
        #region consts
        private const string HTML_HEAD = @"<!DOCTYPE html>
<html lang=""en"" xmlns=""http://www.w3.org/1999/xhtml"">
    <head>
        <meta http-equiv=""X-UA-Compatible"" content=""IE=Edge"" />
        <meta charset=""utf-8"" />
        {{HeadContent}}
        <title>{{Title}}</title>
    </head>
    <body>
        <div id=""___markdown-content___"" class=""markdown-body"">";
        private const string HTML_FOOT = @"
        </div>        
        <footer class=""notewidget-footer"">
            <p>{{footer}}</p>
        </footer>
        {{FootContent}}
    </body>
</html>
";
        #endregion
        public static HtmlTemplate LocalResourceTemplate { get; private set; }
        public static HtmlTemplate OnlineResourceTemplate { get; private set; }
        public static void BuildDefaultTemplates()
        {
            var templateBuilder = HtmlTemplateBuilder.CreateDefaultHtmlTemplateBuilder(TemplateResourceType.Local);
            LocalResourceTemplate = templateBuilder.Build();
            templateBuilder = HtmlTemplateBuilder.CreateDefaultHtmlTemplateBuilder(TemplateResourceType.Online);
            OnlineResourceTemplate = templateBuilder.Build();
        }

        public IList<string> Stylesheets { get; }
        public IList<string> Scripts { get; }
        public IList<string> PostScripts { get; }
        public HtmlTemplate()
        {
            Stylesheets = new List<string>();
            Scripts = new List<string>();
            PostScripts = new List<string>();
        }
        public string ToHead(string title)
        {
            StringBuilder stringBuilder = new StringBuilder();
            foreach(var style in Stylesheets)
            {
                stringBuilder.AppendLine($"\t\t{style}");
            }

            foreach(var script in Scripts)
            {
                stringBuilder.AppendLine($"\t\t{script}");
            }

            return HTML_HEAD.Replace("{{HeadContent}}", stringBuilder.ToString()).Replace("{{Title}}", HttpUtility.HtmlDecode(title ?? string.Empty));
        }
        public string ToFoot(string footer = null)
        {
            StringBuilder stringBuilder = new StringBuilder();
            foreach (var script in PostScripts)
            {
                stringBuilder.AppendLine($"\t\t{script}");
            }
            return HTML_FOOT.Replace("{{footer}}", footer ?? string.Empty).Replace("{{FootContent}}", stringBuilder.ToString());
        }

        public string ToHtml(string title, string htmlBodyContent, string footer = null)
        {
            return ToHead(title) + htmlBodyContent + ToFoot(footer);
        }
    }
}
