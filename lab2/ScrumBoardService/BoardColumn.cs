using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScrumBoardService
{
    public class BoardColumn
    {
        private List<Card> _cards = new List<Card>();

        public BoardColumn(string name)
        {
            Name = name;
        }

        public string Name { get; private set; }

        public void Rename(string name)
        {
            Name = name;
        }

        public void AddCard(Card card)
        {
            _cards.Add(card);
        }

        public Card GetCard(int order)
        {
            try
            {
                return _cards[order];
            }
            catch (ArgumentOutOfRangeException)
            {
                throw new ArgumentOutOfRangeException("Attempt to get card at out of range position");
            }
        }

        public List<Card> GetAllCards()
        {
            return _cards;
        }

        public void DeleteCard(int order)
        {
            try
            {
                _cards.RemoveAt(order);
            }
            catch (ArgumentOutOfRangeException)
            {
                throw new ArgumentOutOfRangeException("Attempt to delete card at out of range position");
            }
        }
    }
}
