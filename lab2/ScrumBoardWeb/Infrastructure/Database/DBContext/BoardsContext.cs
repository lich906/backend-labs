// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ScrumBoard.Model;

namespace ScrumBoardWeb.Infrastructure.DBContext
{
    public class BoardsContext : DbContext
    {
        public BoardsContext(DbContextOptions<BoardsContext> options)
            : base(options)
        {
        }

        public DbSet<Board> Boards { get; set; }
    }
}
