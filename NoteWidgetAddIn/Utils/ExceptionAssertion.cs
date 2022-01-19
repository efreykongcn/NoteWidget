// Copyright (c) Efrey Kong. All Rights Reserved.
// Licensed under the Apache License, Version 2.0.

using System;

namespace NoteWidgetAddIn
{
    public class ExceptionAssertion
    {
        public static void ThrowArgumentNullExceptionIfNull(object arg, string argumentName)
        {
            if (arg == null)
            {
                throw new ArgumentNullException(argumentName);
            }
        }
    }
}
