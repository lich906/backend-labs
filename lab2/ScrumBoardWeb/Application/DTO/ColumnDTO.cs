// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.Collections.Generic;

namespace ScrumBoardWeb.Application.DTO
{
    public class ColumnDTO
    {
        public ColumnDTO(string name, List<CardDTO> cards)
        {
            Name = name;
            Cards = cards;
        }

        public string Name { get; }

        public List<CardDTO> Cards { get; }
    }
}
