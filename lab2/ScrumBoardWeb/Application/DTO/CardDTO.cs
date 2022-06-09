// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using ScrumBoard.Model;

namespace ScrumBoardWeb.Application.DTO
{
    public class CardDTO
    {
        public CardDTO(string name, string description, Card.PriorityType priority)
        {
            Priority = priority;
            Name = name;
            Description = description;
        }
        public Card.PriorityType Priority { get; }

        public string Name { get; }

        public string Description { get; }
    }
}
