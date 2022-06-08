// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ScrumBoard.Model;

namespace ScrumBoard.Repository
{
    public interface IScrumBoardRepository
    {
        public List<Board> GetBoards();
        public void CreateBoard(string name);

        public void DeleteBoard(int index);

        public Board GetBoard(int index);

        public void CreateColumn(int boardIndex, string name);

        public void DeleteColumn(int boardIndex, int index);

        public BoardColumn GetColumn(int boardIndex, int index);

        public void CreateCard(int boardIndex, string name, string description, Card.PriorityType priority);

        public void DeleteCard(int boardIndex, int columnIndex, int index);

        public Card GetCard(int boardIndex, int columnIndex, int index);
    }
}
