// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace ScrumBoardWeb.Application.DTO
{
    public class ColumnDTO
    {
        public string Name { get; }

        public ColumnDTO(string name) => Name = name;
    }
}
