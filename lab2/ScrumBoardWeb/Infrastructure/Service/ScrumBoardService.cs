// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.Linq;
using System.Collections.Generic;
using ScrumBoard.Model;
using ScrumBoard.Repository;
using ScrumBoardWeb.Application.DTO;
using ScrumBoardWeb.Application.Service;

namespace ScrumBoardWeb.Infrastructure.Service
{
    public class ScrumBoardService : IScrumBoardService
    {
        private readonly IScrumBoardRepository _scrumBoardRepository;
        public ScrumBoardService(IScrumBoardRepository scrumBoardRepo)
        {
            _scrumBoardRepository = scrumBoardRepo;
        }

        public void CreateBoard(BoardDTO boardDto)
        {
            _scrumBoardRepository.CreateBoard(boardDto.Name);
        }

        public void DeleteBoard(int index)
        {
            _scrumBoardRepository.DeleteBoard(index);
        }

        public void CreateCard(int boardIndex, CardDTO cardDto)
        {
            _scrumBoardRepository.CreateCard(boardIndex, cardDto.Name, cardDto.Description, cardDto.Priority);
        }

        public void CreateColumn(int boardIndex, ColumnDTO columnDto)
        {
            _scrumBoardRepository.CreateColumn(boardIndex, columnDto.Name);
        }


        public void DeleteCard(int boardIndex, int columnIndex, int index)
        {
            _scrumBoardRepository.DeleteCard(boardIndex, columnIndex, index);
        }

        public void DeleteColumn(int boardIndex, int index)
        {
            _scrumBoardRepository.DeleteColumn(boardIndex, index);
        }

        public List<BoardDTO> GetAllBoards()
        {
            return _scrumBoardRepository.GetBoards().Select(board => new BoardDTO(board.Name)).ToList();
        }

        public BoardDTO GetBoard(int index)
        {
            return new BoardDTO(_scrumBoardRepository.GetBoard(index).Name);
        }

        public CardDTO GetCard(int boardIndex, int columnIndex, int index)
        {
            Card card = _scrumBoardRepository.GetCard(boardIndex, columnIndex, index);
            return new CardDTO(card.Name, card.Description, card.Priority);
        }

        public ColumnDTO GetColumn(int boardIndex, int index)
        {
            return new ColumnDTO(_scrumBoardRepository.GetColumn(boardIndex, index).Name);
        }

        public List<ColumnDTO> GetAllBoardColumns(int boardIndex)
        {
            return _scrumBoardRepository.GetBoard(boardIndex)
                .GetAllColumns().Select(column => new ColumnDTO(column.Name)).ToList();
        }

        public List<CardDTO> GetCards(int boardIndex, int columnIndex)
        {
            return _scrumBoardRepository.GetBoard(boardIndex)
                .GetAllColumns().ElementAt(columnIndex)
                .GetAllCards().Select(card => new CardDTO(card.Name, card.Description, card.Priority)).ToList();
        }
    }
}
