// Copyright (c) Efrey Kong. All Rights Reserved.
// Licensed under the Apache License, Version 2.0.

using System.IO;
using System.Text.RegularExpressions;

namespace NoteWidgetAddIn
{
    public static class PathHelper
    {
        public const string MappedVirtualHostName = "notewidget-vitual-host";
        public static string MakeValidFileName(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
                return fileName;

            string invalidChars = Regex.Escape(new string(Path.GetInvalidFileNameChars()));
            string invalidRegStr = string.Format(@"([{0}]*\.+$)|([{0}]+)", invalidChars);

            return Regex.Replace(fileName, invalidRegStr, "-");
        }

        public static string MakeValidFolderName(string folderName)
        {
            if (string.IsNullOrEmpty(folderName))
                return folderName;

            return string.Join("-", folderName.Split(Path.GetInvalidPathChars()));
        }

        public static string MakeUniqueFolderName(string fullFolderName)
        {
            var currentPath = fullFolderName;
            if (Directory.Exists(currentPath))
            {
                int i = 1;
                while (true)
                {
                    currentPath = fullFolderName + " (" +i.ToString() + ")";
                    if (!Directory.Exists(currentPath))
                    {
                        break;
                    }
                    i++;
                }
            }

            return currentPath;
        }
        /// <summary>
        /// Returns virtual host root path start with <code>file:///</code> protocol.
        /// </summary>
        /// <returns></returns>
        public static string GetVirtualHostRootPath()
        {
            return "file:///" + GetWidgetRootPath().Replace("\\", "/");
        }
        /// <summary>
        /// Returns root path of widget without <code>file:\\</code> protocol
        /// </summary>
        /// <returns></returns>
        public static string GetWidgetRootPath()
        {
            return Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase).Replace("file:\\", ""); 
        }
    }
}
