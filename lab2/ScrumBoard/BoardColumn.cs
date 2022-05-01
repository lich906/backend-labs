using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScrumBoard
{
    public class BoardColumn
    {
        private List<Card> _cards = new List<Card>();

        public BoardColumn(string name)
        {
            Name = name;
        }

        public string Name { get; private set; }

        public void ChangeName(string newName)
        {
            Name = newName;
        }

        public void AddCard(Card card)
        {
            _cards.Add(card);
        }

        public Card GetCardByName(string name)
        {
            int index;
            if((index = _cards.FindIndex(card => card.Name == name)) >= 0)
            {
                return _cards[index];
            }
            else
            {
                throw new KeyNotFoundException($"Card with name '{name}' does not exist.");
            }
        }

        public List<Card> GetAllCards()
        {
            return _cards;
        }

        public void DeleteCardByName(string name)
        {
            if (_cards.RemoveAll(card => card.Name == name) == 0)
            {
                throw new ApplicationException($"There is no card '{name}' in that column.");
            }
        }
    }
}
