// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.Collections.Generic;

namespace ScrumBoardWeb.Application.Dto
{
    public class BoardDto
    {
        public BoardDto(string name, List<ColumnDto> columns)
        {
            Name = name;
            Columns = columns;
        }

        public string Name { get; }

        public List<ColumnDto> Columns { get; }
    }
}
