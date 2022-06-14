// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.Linq;
using System.Collections.Generic;
using ScrumBoard.Model;
using ScrumBoard.Repository;
using ScrumBoardWeb.Application.Dto;
using ScrumBoardWeb.Application.Service;
using System;

namespace ScrumBoardWeb.Infrastructure.Service
{
    public class ScrumBoardService : IScrumBoardService
    {
        private readonly IScrumBoardRepository _scrumBoardRepository;
        public ScrumBoardService(IScrumBoardRepository scrumBoardRepo)
        {
            _scrumBoardRepository = scrumBoardRepo;
        }

        public void CreateBoard(BoardDto boardDto)
        {
            _scrumBoardRepository.CreateBoard(boardDto.Name);
        }

        public void DeleteBoard(int index)
        {
            _scrumBoardRepository.DeleteBoard(index);
        }

        public void CreateCard(int boardIndex, CardDto cardDto)
        {
            _scrumBoardRepository.CreateCard(boardIndex, cardDto.Name, cardDto.Description, cardDto.Priority);
        }

        public void CreateColumn(int boardIndex, ColumnDto columnDto)
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

        public List<BoardDto> GetAllBoards()
        {
            try
            {
                return _scrumBoardRepository.GetBoards()
                    .Select((board, index) => new BoardDto(board.Name, GetBoardColumns(index))).ToList();
            }
            catch (InvalidOperationException)
            {
                return new();
            }
        }

        public BoardDto GetBoard(int index)
        {
            return new BoardDto(
                _scrumBoardRepository.GetBoard(index).Name,
                GetBoardColumns(index)
            );
        }

        public CardDto GetCard(int boardIndex, int columnIndex, int index)
        {
            Card card = _scrumBoardRepository.GetCard(boardIndex, columnIndex, index);
            return new CardDto(card.Name, card.Description, card.Priority);
        }

        public ColumnDto GetColumn(int boardIndex, int index)
        {
            return new ColumnDto(
                _scrumBoardRepository.GetColumn(boardIndex, index).Name,
                GetColumnCards(boardIndex, index)
            );
        }

        public List<ColumnDto> GetBoardColumns(int boardIndex)
        {
            try
            {
                return _scrumBoardRepository.GetBoard(boardIndex)
                    .GetAllColumns().Select((column, index) => new ColumnDto(column.Name, GetColumnCards(boardIndex, index))).ToList();
            }
            catch (InvalidOperationException)
            {
                return new();
            }
        }

        public List<CardDto> GetColumnCards(int boardIndex, int columnIndex)
        {
            try
            {
                return _scrumBoardRepository.GetBoard(boardIndex)
                    .GetAllColumns().ElementAt(columnIndex)
                    .GetAllCards().Select(card => new CardDto(card.Name, card.Description, card.Priority)).ToList();
            }
            catch (InvalidOperationException)
            {
                return new();
            }
        }
    }
}
