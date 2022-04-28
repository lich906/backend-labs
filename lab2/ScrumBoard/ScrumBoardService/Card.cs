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

        public Card(string name, string description, PriorityType priority)
        {
            Name = name;
            Description = description;
            Priority = priority;
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
