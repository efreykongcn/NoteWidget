// Copyright (c) Efrey Kong. All Rights Reserved.
// Licensed under the Apache License, Version 2.0.

using System;
using Microsoft.Office.Interop.OneNote;

namespace NoteWidgetAddIn
{
    public class NoteApplicationContext
    {
        public NoteApplicationContext()
        {
            ResisteredApplicationType = null;
        }
        /// <summary>
        /// Construct context with specified type. Usually for testing purpose
        /// </summary>
        /// <param name="type">A type inherit from IApplication</param>
        /// <exception cref="InvalidOperationException"></exception>
        public NoteApplicationContext(Type type) 
        {
            ExceptionAssertion.ThrowArgumentNullExceptionIfNull(type, nameof(type));
            if (!typeof(IApplication).IsAssignableFrom(type))
            {
                throw new InvalidOperationException($"{nameof(type)} '{type.FullName}' must be derived from Microsoft.Office.Interop.OneNote.IApplication");
            }
            if (type.IsInterface || type.IsAbstract)
            {
                throw new InvalidOperationException($"{nameof(type)} cannot be interface or abstract.");
            }
            ResisteredApplicationType = type;
        }
        public Type ResisteredApplicationType { get;}
        public NoteApplication CreateApplication()
        {
            return new NoteApplication(this);
        }
    }
}
