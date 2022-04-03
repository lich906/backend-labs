using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScrumBoardService
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

        public void AddNewCard(string name)
        {
            if (_columns.Count == 0)
            {
                throw new Exception("Board has no columns");
            }

            _columns[0].AddCard(new Card(name));
        }

        public void MoveCard(int srcColumn, int cardOrder, int destColumn)
        {
            try
            {
                _columns[destColumn].AddCard(_columns[srcColumn].GetCard(cardOrder));
                _columns[srcColumn].DeleteCard(cardOrder);
            }
            catch (ArgumentOutOfRangeException)
            {
                throw;
            }
        }

        public BoardColumn GetColumn(int order)
        {
            try
            {
                return _columns[order];
            }
            catch (ArgumentOutOfRangeException)
            {
                throw new ArgumentOutOfRangeException("Attempt to get column at out of range position");
            }
        }
        public void DeleteColumn(int order)
        {
            try
            {
                _columns.RemoveAt(order);
            }
            catch (ArgumentOutOfRangeException)
            {
                throw new ArgumentOutOfRangeException("Attempt to delete column at out of range position");
            }
        }

        public List<BoardColumn> GetAllColumns()
        {
            return _columns;
        }
    }
}
