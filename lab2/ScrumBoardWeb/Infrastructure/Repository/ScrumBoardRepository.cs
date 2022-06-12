// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System;
using System.Collections.Generic;
using System.Linq;
using ScrumBoard.Model;
using ScrumBoard.Repository;
using ScrumBoardWeb.Infrastructure.Database.DBContext;
using ScrumBoardWeb.Application.Exception;
using ScrumBoardWeb.Infrastructure.Database;

namespace ScrumBoardWeb.Infrastructure.Repository
{
    public class ScrumBoardRepository : ScrumBoardRepositoryInterface
    {
        private readonly ScrumBoardDbContext _context;
        private readonly DatabaseEntityHydratorInterface _hydrator;

        public ScrumBoardRepository(ScrumBoardDbContext context, DatabaseEntityHydratorInterface hydrator)
        {
            _context = context;
            _hydrator = hydrator;
            _context.Database.EnsureCreated();
        }

        public List<Board> GetBoards()
        {
            List<Board> boards = _context.Boards
                .Select(b => _hydrator.HydrateBoard(b))
                .AsEnumerable().ToList();

            if (boards != null)
            {
                return boards;
            }

            return new List<Board>();
        }

        public void CreateBoard(string name)
        {
            _context.Boards.Add(new Database.Entity.Board(name));
            _context.SaveChanges();
        }

        public void DeleteBoard(int index)
        {
            try
            {
                _context.Boards.Remove(_context.Boards.ElementAt(index));
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
                return _context.Boards
                    .Where(b => b.Id == index)
                    .Select(b => _hydrator.HydrateBoard(b))
                    .Single();
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
                _context.Boards
                    .Where(b => b.Id == boardIndex)
                    .Select(b => b).Single().Columns.Add(new Database.Entity.Column(name));
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

                Database.Entity.Column columnToDelete = _context.Columns
                    .Where(c => c.Board.Id == boardIndex)
                    .SingleOrDefault(c => c.Id == index);

                if (columnToDelete != null)
                {
                    _context.Columns.Remove(columnToDelete);
                    _context.SaveChanges();
                }
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
                return _context.Columns
                    .Where(c => c.Board.Id == boardIndex)
                    .Where(c => c.Id == index)
                    .Select(c => _hydrator.HydrateColumn(c)).Single();
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
                _context.Boards
                    .Where(b => b.Id == boardIndex).Single()
                    .Columns[0].Cards
                    .Add(new Database.Entity.Card(name, description, (int)priority));
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
                Database.Entity.Card cardToDelete = _context.Cards
                    .Where(c => c.Column.Id == columnIndex)
                    .Where(c => c.Column.Board.Id == boardIndex)
                    .SingleOrDefault(c => c.Id == index);

                if (cardToDelete != null)
                {
                    _context.Cards.Remove(cardToDelete);
                    _context.SaveChanges();
                }
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
                return _context.Cards
                    .Where(c => c.Column.Id == columnIndex)
                    .Where(c => c.Column.Board.Id == boardIndex)
                    .Where(c => c.Id == index)
                    .Select(c => _hydrator.HydrateCard(c)).Single();
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
