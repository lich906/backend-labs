using System;
using System.Collections.Generic;
using ScrumBoard;

namespace ScrumBoardConsoleApp
{
    class ConsoleApp
    {
        enum Command
        {
            AddColumn,
            AddCard,
            MoveCard,
            Show,
            Help,
            Exit
        }

        private static readonly Dictionary<string, Command> _mapStringToCommand =
            new Dictionary<string, Command>()
            {
                { "add-column", Command.AddColumn },
                { "add-card", Command.AddCard },
                { "move-card", Command.MoveCard },
                { "show", Command.Show },
                { "help", Command.Help },
                { "exit", Command.Exit }
            };

        private static readonly Dictionary<string, Card.PriorityType> _mapStringToPriority =
            new Dictionary<string, Card.PriorityType>()
            {
                { "minor", Card.PriorityType.Minor },
                { "normal", Card.PriorityType.Normal },
                { "major", Card.PriorityType.Major },
                { "critical", Card.PriorityType.Critical },
                { "blocker", Card.PriorityType.Blocker }
            };

        static void Main()
        {
            Board board = InitBoard();
            Command command = Command.Help;

            while (command != Command.Exit)
            {
                try
                {
                    command = ReadCommand();
                    HandleCommand(command, board);
                }
                catch(Exception e)
                {
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.WriteLine(e.Message);
                    Console.ResetColor();
                }
            }
        }

        private static Board InitBoard()
        {
            Console.WriteLine("Enter board name...");
            string name = Console.ReadLine();

            return new Board(name);
        }

        private static Command ReadCommand()
        {
            string rawString = Console.ReadLine();

            if (!_mapStringToCommand.ContainsKey(rawString))
            {
                throw new ArgumentException($"Unknown command '{rawString}'. Use 'help' command to see available commands.");
            }

            return _mapStringToCommand[rawString];
        }

        private static void HandleCommand(Command command, Board board)
        {
            switch (command)
            {
                case Command.AddCard:
                    AddCard(board);
                    break;
                case Command.AddColumn:
                    AddColumn(board);
                    break;
                case Command.MoveCard:
                    MoveCard(board);
                    break;
                case Command.Show:
                    ShowBoard(board);
                    break;
                case Command.Help:
                    ShowHelp();
                    break;
                case Command.Exit:
                    Console.WriteLine("Exiting...");
                    break;
                default:
                    throw new ArgumentException("In function HandleCommand: Invalid command type.");
            }
        }

        private static void ShowBoard(Board board)
        {
            Console.WriteLine($"++++++++++++++++++ {board.Name} ++++++++++++++++++");
            List<BoardColumn> columns = board.GetAllColumns();

            if (columns.Count == 0)
            {
                Console.WriteLine("There is no columns yet. Use 'add-column' command to add some columns.");
            }

            foreach (BoardColumn column in columns)
            {
                Console.WriteLine($"============ {column.Name} ============");
                List<Card> cards = column.GetAllCards();
                if (cards.Count == 0)
                {
                    Console.WriteLine("Column is empty.");
                }
                foreach (Card card in cards)
                {
                    Console.WriteLine("----------------------------");
                    Console.WriteLine($"|   {card.Name}");
                    Console.WriteLine($"| > {card.GetPriorityString()}");
                    Console.WriteLine("|");
                    Console.WriteLine($"|   {card.Description}");
                    Console.WriteLine("----------------------------");
                    Console.WriteLine();
                }
            }
        }

        private static void AddCard(Board board)
        {
            Console.WriteLine("Enter card name");
            string name = Console.ReadLine();
            Console.WriteLine("Enter description");
            string description = Console.ReadLine();
            Console.WriteLine("Enter priority");
            string priorityString = Console.ReadLine();
            if (!_mapStringToPriority.ContainsKey(priorityString))
            {
                throw new ArgumentException("Invalid priority type. Available values: 'minor', 'normal', 'major', 'critical', 'blocker'");
            }

            board.AddNewCard(name, description, _mapStringToPriority[priorityString]);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Card was added successfully.");
            Console.ResetColor();
        }

        private static void AddColumn(Board board)
        {
            Console.WriteLine("Enter column name");
            string name = Console.ReadLine();
            board.AddNewColumn(name);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Column was added successfully.");
            Console.ResetColor();
        }

        private static void MoveCard(Board board)
        {
            Console.WriteLine("Enter the card's name you want to move.");
            string cardName = Console.ReadLine();
            Console.WriteLine("Enter the column's name to which you want to move the card.");
            string columnName = Console.ReadLine();

            board.MoveCard(cardName, columnName);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Card was moved successfully.");
            Console.ResetColor();
        }

        private static void ShowHelp()
        {
            Console.WriteLine("'help'\t\tShows this help message.");
            Console.WriteLine("'add-column'\tAdd column on the board. Column names must be unique");
            Console.WriteLine("'add-card'\tAdd card on the board. Card will be placed in the first column.");
            Console.WriteLine("'move-card'\tMove card to specific column");
            Console.WriteLine("'show'\t\tShows the current board.");
            Console.WriteLine("'exit'\t\tExit the program.");
        }
    }
}
