// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ScrumBoardWeb.Infrastructure.Database.Entity
{
    public class Column
    {
        public Column(string name)
        {
            Name = name;
        }

        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public List<Card> Cards { get; set; }

        public Board Board { get; set; }
    }
}
