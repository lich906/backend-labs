// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.Collections.Generic;
using ScrumBoardWeb.Application.DTO;

namespace ScrumBoardWeb.Application.Service
{
    public interface ScrumBoardServiceInterface
    {
        public void CreateBoard(BoardDTO boardDto);

        public void DeleteBoard(int index);

        public List<BoardDTO> GetAllBoards();

        public BoardDTO GetBoard(int index);

        public void CreateColumn(int boardIndex, ColumnDTO columnDto);

        public void DeleteColumn(int boardIndex, int index);

        public List<ColumnDTO> GetBoardColumns(int boardIndex);

        public ColumnDTO GetColumn(int boardIndex, int index);

        public void CreateCard(int boardIndex, CardDTO cardDto);

        public void DeleteCard(int boardIndex, int columnIndex, int index);

        public CardDTO GetCard(int boardIndex, int columnIndex, int index);

        public List<CardDTO> GetCards(int boardIndex, int columnIndex);
    }
}
