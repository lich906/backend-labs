using System;

namespace ScrumBoard
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

        public Card(string name, string description, PriorityType priority)
        {
            Name = name;
            Description = description;
            Priority = priority;
        }

        public string Name { get; private set; }

        public string Description { get; private set; }

        public PriorityType Priority { get; private set; }

        public string GetPriorityString()
        {
            switch(Priority)
            {
                case PriorityType.Minor:
                    return "Minor";
                case PriorityType.Normal:
                    return "Normal";
                case PriorityType.Major:
                    return "Major";
                case PriorityType.Critical:
                    return "Critical";
                case PriorityType.Blocker:
                    return "Blocker";
                default:
                    throw new ArgumentException("Internal error: invalid card priority.");
            }
        }
    }
}
