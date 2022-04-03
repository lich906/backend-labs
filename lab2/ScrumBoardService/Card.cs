using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScrumBoardService
{
    public class Card
    {
        public enum PriorityType
        {
            Minor = 0,
            Normal,
            Major,
            Critical,
            Blocker
        };

        public Card(string name)
        {
            Name = name;
            Description = "";
            Priority = PriorityType.Normal;
        }

        public string Name { get; private set; }

        public string Description { get; private set; }

        public PriorityType Priority { get; private set; }

        public void SetName(string name)
        {
            Name = name;
        }

        public void SetDescription(string description)
        {
            Description = description;
        }

        public void SetPriority(PriorityType priority)
        {
            Priority = priority;
        }
    }
}
