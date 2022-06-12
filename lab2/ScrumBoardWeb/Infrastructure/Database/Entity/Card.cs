// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ScrumBoardWeb.Infrastructure.Database.Entity
{
    public class Card
    {
        public Card(string name, string description, int priority)
        {
            Name = name;
            Description = description;
            Priority = priority;
        }

        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int Priority { get; set; }

        public Column Column { get; set; }
    }
}
