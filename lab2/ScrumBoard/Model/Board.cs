using System;
using System.Collections.Generic;
using System.Linq;

namespace ScrumBoard.Model
{
    public class Board
    {
        private const int COLUMNS_LIMIT = 10;

        private List<BoardColumn> _columns = new List<BoardColumn>();

        public Board(string name)
        {
            Name = name;
        }

        public string Name { get; }

        public void AddNewColumn(string name)
        {
            if (_columns.Count == COLUMNS_LIMIT)
            {
                throw new ApplicationException("Failed to add column: columns limit reached.");
            }

            if (ColumnExists(name))
            {
                throw new ApplicationException($"Column with name '{name}' already exists.");
            }

            _columns.Add(new BoardColumn(name));
        }

        public void AddNewCard(string name, string description, Card.PriorityType priority)
        {
            if (_columns.Count == 0)
            {
                throw new ApplicationException("Board has no columns");
            }

            if (CardExists(name))
            {
                throw new ApplicationException($"Card with name '{name}' already exists.");
            }

            _columns[0].AddCard(new Card(name, description, priority));
        }

        public void MoveCard(string cardName, string columnName)
        {
            BoardColumn destColumn = GetColumnByName(columnName);
            BoardColumn srcColumn = GetColumnByCardName(cardName);
            destColumn.AddCard(srcColumn.GetCardByName(cardName));
            srcColumn.DeleteCardByName(cardName);
        }

        public BoardColumn GetColumnByName(string name)
        {
            try
            {
                return _columns.Where(column => column.Name == name).Single();
            }
            catch(InvalidOperationException)
            {
                throw new ApplicationException($"Column with name '{name}' does not exist.");
            }
        }

        public Card GetCardByName(string name)
        {
            return GetColumnByCardName(name).GetCardByName(name);
        }

        public void DeleteColumnByName(string columnName)
        {
            if (_columns.RemoveAll(column => column.Name == columnName) == 0)
            {
                throw new ArgumentOutOfRangeException($"Can't delete column: column with name '{columnName}' does not exist.");
            }
        }

        public void RenameColumn(string name, string newName)
        {
            if(!ColumnExists(name))
            {
                throw new ApplicationException($"Can't rename column: column with name '{name}' does not exist.");
            }

            if(ColumnExists(newName))
            {
                throw new ApplicationException($"Can't rename column: column with name '{newName}' already exists.");
            }

            GetColumnByName(name).ChangeName(newName);
        }

        public void RenameCard(string name, string newName)
        {
            if (CardExists(newName))
            {
                throw new ApplicationException($"Can't rename card: card with name '{newName}' already exists.");
            }

            GetCardByName(name).ChangeName(newName);
        }

        public List<BoardColumn> GetAllColumns()
        {
            return _columns;
        }

        public bool CardExists(string cardName)
        {
            try
            {
                GetColumnByCardName(cardName);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool ColumnExists(string columnName)
        {
            return _columns.Exists(column => column.Name == columnName);
        }

        private BoardColumn GetColumnByCardName(string cardName)
        {
            try
            {
                return _columns.Where(column => column.HasCard(cardName)).Single();
            }
            catch (InvalidOperationException)
            {
                throw new ApplicationException($"Column with card '{cardName}' does not exist.");
            }
        }
    }
}
