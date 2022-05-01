using System;
using System.Collections.Generic;

namespace ScrumBoard
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

            if (ColumnNameExists(name))
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

            if (CardNameExists(name))
            {
                throw new ApplicationException($"Card with name '{name}' already exists.");
            }

            _columns[0].AddCard(new Card(name, description, priority));
        }

        public void MoveCard(string cardName, string columnName)
        {
            if (!CardNameExists(cardName))
            {
                throw new ApplicationException($"Card with name '{cardName}' does not exist.");
            }
            BoardColumn destColumn = GetColumnByName(columnName);
            int srcColumnIndex = _columns.FindIndex(column => column.GetAllCards().Exists(card => card.Name == cardName));
            destColumn.AddCard(_columns[srcColumnIndex].GetCardByName(cardName));
            _columns[srcColumnIndex].DeleteCardByName(cardName);
        }

        public BoardColumn GetColumnByName(string name)
        {
            int index;
            if ((index = _columns.FindIndex(column => column.Name == name)) >= 0)
            {
                return _columns[index];
            }
            else
            {
                throw new ApplicationException($"Column with name '{name}' does not exist.");
            }
        }

        public void DeleteColumnByName(string name)
        {
            if (_columns.RemoveAll(column => column.Name == name) == 0)
            {
                throw new ArgumentOutOfRangeException($"Column with name '{name}' does not exist.");
            }
        }

        public List<BoardColumn> GetAllColumns()
        {
            return _columns;
        }

        public bool CardNameExists(string name)
        {
            return _columns.Exists(column => column.GetAllCards().Exists(card => card.Name == name));
        }

        public bool ColumnNameExists(string name)
        {
            return _columns.Exists(column => column.Name == name);
        }
    }
}
