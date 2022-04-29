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
                throw new Exception("Columns limit exceeding");
            }

            _columns.Add(new BoardColumn(name));
        }

        public void AddNewCard(string name, string description, Card.PriorityType priority)
        {
            if (_columns.Count == 0)
            {
                throw new Exception("Board has no columns");
            }

            _columns[0].AddCard(new Card(name, description, priority));
        }

        public void MoveCard(string srcColumnName, string cardName, string destColumnName)
        {
            try
            {
                BoardColumn destColumn = GetColumnByName(destColumnName);
                BoardColumn srcColumn = GetColumnByName(srcColumnName);
                destColumn.AddCard(srcColumn.GetCardByName(cardName));
                srcColumn.DeleteCardByName(cardName);
            }
            catch (ArgumentOutOfRangeException)
            {
                throw;
            }
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
                throw new ArgumentNullException($"Column with name '{name}' not found.");
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
    }
}
