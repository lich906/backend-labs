// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.Collections.Generic;

namespace ScrumBoardWeb.Application.DTO
{
    public class BoardDTO
    {
        public BoardDTO(string name, List<ColumnDTO> columns)
        {
            Name = name;
            Columns = columns;
        }

        public string Name { get; }

        public List<ColumnDTO> Columns { get; }
    }
}
