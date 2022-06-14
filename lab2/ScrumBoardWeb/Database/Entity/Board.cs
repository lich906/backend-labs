// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ScrumBoardWeb.Database.Entity
{
    public class Board
    {
        public Board(string name)
        {
            Name = name;
        }

        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public List<Column> Columns { get; set; }
    }
}
