// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System;
using System.Collections.Generic;
using System.Linq;
using ScrumBoard.Model;
using ScrumBoard.Repository;
using ScrumBoardWeb.Infrastructure.DBContext;
using ScrumBoardWeb.Application.Exception;
using Microsoft.EntityFrameworkCore;

namespace ScrumBoardWeb.Infrastructure.Repository
{
    public class ScrumBoardRepository : ScrumBoardRepositoryInterface
    {
        private readonly BoardsContext _context;

        public ScrumBoardRepository(BoardsContext context)
        {
            _context = context;
            _context.Database.EnsureCreated();
        }

        public List<Board> GetBoards()
        {
            List<Board> boards = _context.Boards.ToList();

            if (boards != null)
            {
                return boards;
            }

            return new List<Board>();
        }

        public void CreateBoard(string name)
        {
            _context.Boards.Add(new Board(name));
            _context.SaveChanges();
        }

        public void DeleteBoard(int index)
        {
            try
            {
                _context.Boards.Remove(GetBoard(index));
                _context.SaveChanges();
            }
            catch (ArgumentOutOfRangeException)
            {
                throw new OutOfRangeException();
            }
        }

        public Board GetBoard(int index)
        {
            try
            {
                return _context.Boards.ElementAt(index);
            }
            catch (ArgumentOutOfRangeException)
            {
                throw new OutOfRangeException();
            }
        }

        public void CreateColumn(int boardIndex, string name)
        {
            try
            {
                _context.Boards.ElementAt(boardIndex).AddNewColumn(name);
                _context.SaveChanges();
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
            try
            {
                GetBoard(boardIndex).GetAllColumns().RemoveAt(index);
                _context.SaveChanges();
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
            try
            {
                return GetBoard(boardIndex).GetAllColumns().ElementAt(index);
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
            try
            {
                GetBoard(boardIndex).AddNewCard(name, description, priority);
                _context.SaveChanges();
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
            try
            {
                GetColumn(boardIndex, columnIndex).GetAllCards().RemoveAt(index);
                _context.SaveChanges();
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
            try
            {
                return GetColumn(boardIndex, columnIndex).GetAllCards().ElementAt(index);
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
