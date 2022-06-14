// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ScrumBoardWeb.Database.Entity;

namespace ScrumBoardWeb.Database.DBContext
{
    public class ScrumBoardDbContext : DbContext
    {
        public ScrumBoardDbContext(DbContextOptions<ScrumBoardDbContext> options)
            : base(options)
        {
        }

        public DbSet<Board> Boards { get; set; }

        public DbSet<Column> Columns { get; set; }

        public DbSet<Card> Cards { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Board>().HasMany(b => b.Columns);
            modelBuilder.Entity<Column>().HasMany(c => c.Cards);
        }
    }
}
