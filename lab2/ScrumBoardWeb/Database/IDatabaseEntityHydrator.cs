// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ScrumBoard.Model;
using ScrumBoardWeb.Database.Entity;

namespace ScrumBoardWeb.Database
{
    public interface IDatabaseEntityHydrator
    {
        public List<ScrumBoard.Model.Board> HydrateBoards(List<Entity.Board> boards);

        public ScrumBoard.Model.Board HydrateBoard(Entity.Board board);

        public List<ScrumBoard.Model.Column> HydrateColumns(List<Entity.Column> boards);

        public ScrumBoard.Model.Column HydrateColumn(Entity.Column column);

        public List<ScrumBoard.Model.Card> HydrateCards(List<Entity.Card> boards);

        public ScrumBoard.Model.Card HydrateCard(Entity.Card card);
    }
}
