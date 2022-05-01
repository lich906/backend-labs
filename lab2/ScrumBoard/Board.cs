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
            BoardColumn destColumn = GetColumnByName(columnName);
            int srcColumnIndex = FindColumnIndexWithCard(cardName);
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

        public Card GetCardByName(string name)
        {
            return _columns[FindColumnIndexWithCard(name)].GetCardByName(name);
        }

        public void DeleteColumnByName(string name)
        {
            if (_columns.RemoveAll(column => column.Name == name) == 0)
            {
                throw new ArgumentOutOfRangeException($"Can't delete column: column with name '{name}' does not exist.");
            }
        }

        public void RenameColumn(string name, string newName)
        {
            if(!ColumnNameExists(name))
            {
                throw new ApplicationException($"Can't rename column: column with name '{name}' does not exist.");
            }

            if(ColumnNameExists(newName))
            {
                throw new ApplicationException($"Can't rename column: column with name '{newName}' already exists.");
            }

            GetColumnByName(name).ChangeName(newName);
        }

        public void RenameCard(string name, string newName)
        {
            if (CardNameExists(newName))
            {
                throw new ApplicationException($"Can't rename card: card with name '{newName}' already exists.");
            }

            GetCardByName(name).ChangeName(newName);
        }

        public List<BoardColumn> GetAllColumns()
        {
            return _columns;
        }

        public bool CardNameExists(string name)
        {
            try
            {
                FindColumnIndexWithCard(name);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool ColumnNameExists(string name)
        {
            return _columns.Exists(column => column.Name == name);
        }

        private int FindColumnIndexWithCard(string cardName)
        {
            int index = _columns.FindIndex(column => column.GetAllCards().Exists(card => card.Name == cardName));

            if (index >= 0)
            {
                return index;
            }
            else
            {
                throw new ApplicationException($"Card with name '{cardName}' does not exist.");
            }
        }
    }
}
