using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScrumBoard.Model
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

        public Card GetCardByName(string cardName)
        {
            try
            {
                return _cards.Where(card => card.Name == cardName).Single();
            }
            catch (InvalidOperationException)
            {
                throw new KeyNotFoundException($"Card with name '{cardName}' does not exist.");
            }
        }

        public bool HasCard(string cardName)
        {
            try
            {
                GetCardByName(cardName);
                return true;
            }
            catch (KeyNotFoundException)
            {
                return false;
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
