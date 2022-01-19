﻿// Copyright (c) Efrey Kong. All Rights Reserved.
// Licensed under the Apache License, Version 2.0.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Markdig;

namespace NoteWidgetAddIn
{
    public class MarkdownHelper
    {
        private static readonly MarkdownPipeline _pipeline;
        static MarkdownHelper()
        {
            _pipeline = new MarkdownPipelineBuilder()
                    .UsePragmaLines()
                    .UseEmojiAndSmiley()
                    .UseYamlFrontMatter()
                    .UseEmphasisExtras()
                    .UseAdvancedExtensions()
                    .Build();
        }
        public static string MarkdownToHtml(string markdownText)
        {
            return Markdig.Markdown.ToHtml(markdownText ?? string.Empty, _pipeline);
        }
    }
}
