// Copyright (c) Efrey Kong. All Rights Reserved.
// Licensed under the Apache License, Version 2.0.

using NoteWidgetAddIn.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media;
using System.Xml.Linq;

namespace NoteWidgetAddIn
{
    public static class Extensions
    {
        #region NoteNode
        /// <summary>
        /// Returns a collection of the descendant nodes of source. 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="nodeType"></param>
        /// <returns></returns>
        public static IEnumerable<NoteNode> Descendants(this NoteNode source)
        {
            ExceptionAssertion.ThrowArgumentNullExceptionIfNull(source, nameof(source));

            yield return source;

            foreach (var child in source.Children)
            {
                foreach(var descendant in Descendants(child))
                {
                    yield return descendant;
                }
            }
        }

        /// <summary>
        /// Returns a filtered collection of the descendant nodes of source. 
        /// Only nodes that satisfy the condition are included in the collection.        
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static IEnumerable<NoteNode> Descendants(this NoteNode source, Func<NoteNode, bool> predicate)
        {
            ExceptionAssertion.ThrowArgumentNullExceptionIfNull(source, nameof(source));
            ExceptionAssertion.ThrowArgumentNullExceptionIfNull(predicate, nameof(predicate));

            if (predicate(source))
            {
                yield return source;
            }

            var enumerator = source.Children.GetEnumerator();
            var stack = new Stack<IEnumerator<NoteNode>>();

            try
            {
                while (true)
                {
                    if (enumerator.MoveNext())
                    {
                        var child = enumerator.Current;
                        if (predicate(child))
                        {
                            yield return child;
                        }

                        stack.Push(enumerator);
                        enumerator = child.Children.GetEnumerator();
                    }
                    else if (stack.Count > 0)
                    {
                        enumerator.Dispose();
                        enumerator = stack.Pop();
                    }
                    else
                    {
                        yield break;
                    }
                }
            }
            finally
            {
                enumerator.Dispose();
                while (stack.Count > 0)
                {
                    stack.Pop().Dispose();
                }
            }
        }
        #endregion

        #region Enum
        public static string GetDescription(this Enum e)
        {
            FieldInfo fi = e.GetType().GetField(e.ToString());
            object[] attrs = fi.GetCustomAttributes(false);

            foreach (var attr in attrs)
            {
                if (attr is DescriptionAttribute)
                {
                    return ((DescriptionAttribute)attr).Description;
                }
            }

            return e.ToString();
        }

        public static NodeType[] GetRestrictedNodeTypes(this Enum e)
        {
            FieldInfo fi = e.GetType().GetField(e.ToString());
            object[] attrs = fi.GetCustomAttributes(false);

            foreach (var attr in attrs)
            {
                if (attr is RestrictedNodeTypeAttribute)
                {
                    return ((RestrictedNodeTypeAttribute)attr).NoteTypes;
                }
            }

            return new NodeType[0];
        }
        #endregion

        #region Image
        public static ImageSource ToImageSource(this Bitmap source, ImageFormat sourceFormat)
        {
            ExceptionAssertion.ThrowArgumentNullExceptionIfNull(source, nameof(source));
            var c = new ImageSourceConverter();
            using (var ms = new MemoryStream())
            {
                source.Save(ms, sourceFormat);
                return (ImageSource)c.ConvertFrom(ms);
            }
        }
        public static ImageSource ToImageSource(this Icon source)
        {
            ExceptionAssertion.ThrowArgumentNullExceptionIfNull(source, nameof(source));
            var c = new ImageSourceConverter();
            using (var ms = new MemoryStream())
            {
                source.Save(ms);
                return (ImageSource)c.ConvertFrom(ms);
            }
        }
        /// <summary>
        /// Convert image file to base64 string.
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string ToBase64(this Bitmap source, ImageFormat sourceFormat)
        {
            ExceptionAssertion.ThrowArgumentNullExceptionIfNull(source, nameof(source));
            using (MemoryStream ms = new MemoryStream())
            {
                source.Save(ms, sourceFormat);
                byte[] arr = new byte[ms.Length];
                ms.Position = 0;
                ms.Read(arr, 0, (int)ms.Length);
                return Convert.ToBase64String(arr);
            }
        }
        public static Bitmap Base64StringToImage(this string source)
        {
            ExceptionAssertion.ThrowArgumentNullExceptionIfNull(source, nameof(source));
            byte[] b = Convert.FromBase64String(source);
            using (var ms = new MemoryStream(b))
            {
                Bitmap bitmap = new Bitmap(ms);
                return bitmap;
            }
        }
        #endregion

        #region Threading 
        public static Task<T> SingleThreadedInvoke<T>(this Object obj, Func<T> func)
        {
            var source = new TaskCompletionSource<T>();
            var thread = new Thread(() =>
            {
                try
                {
                    source.SetResult(func());
                }
                catch (Exception ex)
                {
                    source.SetException(ex);
                }
            });
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
            return source.Task;
        }

        public static Task SingleThreadedInvoke(this Object obj, Action action)
        {
            var source = new TaskCompletionSource<object>();
            var thread = new Thread(() =>
            {
                try
                {
                    action();
                    source.SetResult(null);
                }
                catch (Exception ex)
                {
                    source.SetException(ex);
                }
            });
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
            return source.Task;
        }
        #endregion
    }
}
