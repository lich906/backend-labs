// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.Collections.Generic;

namespace ScrumBoardWeb.Application.Dto
{
    public class ColumnDto
    {
        public ColumnDto(string name, List<CardDto> cards)
        {
            Name = name;
            Cards = cards;
        }

        public string Name { get; }

        public List<CardDto> Cards { get; }
    }
}
