// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System;

namespace ScrumBoardWeb.Application.Exception
{
    public class OutOfRangeException : SystemException
    {
        public OutOfRangeException() : base("Item index is out of range") {}
    }
}
