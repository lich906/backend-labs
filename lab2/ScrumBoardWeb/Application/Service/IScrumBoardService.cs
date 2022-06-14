// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.Collections.Generic;
using ScrumBoardWeb.Application.Dto;

namespace ScrumBoardWeb.Application.Service
{
    public interface IScrumBoardService
    {
        public void CreateBoard(BoardDto boardDto);

        public void DeleteBoard(int index);

        public List<BoardDto> GetAllBoards();

        public BoardDto GetBoard(int index);

        public void CreateColumn(int boardIndex, ColumnDto columnDto);

        public void DeleteColumn(int boardIndex, int index);

        public List<ColumnDto> GetBoardColumns(int boardIndex);

        public ColumnDto GetColumn(int boardIndex, int index);

        public void CreateCard(int boardIndex, CardDto cardDto);

        public void DeleteCard(int boardIndex, int columnIndex, int index);

        public CardDto GetCard(int boardIndex, int columnIndex, int index);

        public List<CardDto> GetColumnCards(int boardIndex, int columnIndex);
    }
}
