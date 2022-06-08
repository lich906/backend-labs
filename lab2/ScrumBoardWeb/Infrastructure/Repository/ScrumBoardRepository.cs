// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Caching.Memory;
using ScrumBoard.Model;
using ScrumBoard.Repository;
using ScrumBoardWeb.Application.Exception;

namespace ScrumBoardWeb.Infrastructure.Repository
{
    public class ScrumBoardRepository : IScrumBoardRepository
    {
        private readonly IMemoryCache _memoryCache;
        private const string _memoryCacheBoardKey = "boards";

        public ScrumBoardRepository(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        public List<Board> GetBoards()
        {
            List<Board>? boards = _memoryCache.Get<List<Board>>(_memoryCacheBoardKey);

            if (boards != null)
            {
                return boards;
            }

            return new List<Board>();
        }

        public void CreateBoard(string name)
        {
            List<Board> boards = GetBoards();

            boards.Add(new Board(name));

            _memoryCache.Set(_memoryCacheBoardKey, boards);
        }

        public void DeleteBoard(int index)
        {
            List<Board> boards = GetBoards();

            try
            {
                boards.RemoveAt(index);
            }
            catch (ArgumentOutOfRangeException)
            {
                throw new OutOfRangeException();
            }

            _memoryCache.Set(_memoryCacheBoardKey, boards);
        }

        public Board GetBoard(int index)
        {
            List<Board> boards = GetBoards();

            try
            {
                return boards.ElementAt(index);
            }
            catch (ArgumentOutOfRangeException)
            {
                throw new OutOfRangeException();
            }
        }

        public void CreateColumn(int boardIndex, string name)
        {
            List<Board> boards = GetBoards();

            try
            {
                boards.ElementAt(boardIndex).AddNewColumn(name);
                _memoryCache.Set(_memoryCacheBoardKey, boards);
            }
            catch (ArgumentOutOfRangeException)
            {
                throw new OutOfRangeException();
            }
            catch (ApplicationException)
            {
                throw;
            }
        }

        public void DeleteColumn(int boardIndex, int index)
        {
            List<Board> boards = GetBoards();

            try
            {
                boards.ElementAt(boardIndex).GetAllColumns().RemoveAt(index);
                _memoryCache.Set(_memoryCacheBoardKey, boards);
            }
            catch (ArgumentOutOfRangeException)
            {
                throw new OutOfRangeException();
            }
            catch (ApplicationException)
            {
                throw;
            }
        }

        public Column GetColumn(int boardIndex, int index)
        {
            List<Board> boards = GetBoards();

            try
            {
                return boards.ElementAt(boardIndex).GetAllColumns().ElementAt(index);
            }
            catch (ArgumentOutOfRangeException)
            {
                throw new OutOfRangeException();
            }
            catch (ApplicationException)
            {
                throw;
            }
        }

        public void CreateCard(int boardIndex, string name, string description, Card.PriorityType priority)
        {
            List<Board> boards = GetBoards();

            try
            {
                boards.ElementAt(boardIndex).AddNewCard(name, description, priority);
                _memoryCache.Set(_memoryCacheBoardKey, boards);
            }
            catch (ArgumentOutOfRangeException)
            {
                throw new OutOfRangeException();
            }
            catch (ApplicationException)
            {
                throw;
            }
        }

        public void DeleteCard(int boardIndex, int columnIndex, int index)
        {
            List<Board> boards = GetBoards();

            try
            {
                boards.ElementAt(boardIndex).GetAllColumns().ElementAt(columnIndex).GetAllCards().RemoveAt(index);
                _memoryCache.Set(_memoryCacheBoardKey, boards);
            }
            catch (ArgumentOutOfRangeException)
            {
                throw new OutOfRangeException();
            }
            catch (ApplicationException)
            {
                throw;
            }
        }

        public Card GetCard(int boardIndex, int columnIndex, int index)
        {
            List<Board> boards = GetBoards();

            try
            {
                return boards.ElementAt(boardIndex).GetAllColumns().ElementAt(columnIndex).GetAllCards().ElementAt(index);
            }
            catch (ArgumentOutOfRangeException)
            {
                throw new OutOfRangeException();
            }
            catch (ApplicationException)
            {
                throw;
            }
        }
    }
}
