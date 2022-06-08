// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace ScrumBoardWeb.Application.DTO
{
    public class BoardDTO
    {
        public string Name { get; }

        public BoardDTO(string name) => Name = name;
    }
}
