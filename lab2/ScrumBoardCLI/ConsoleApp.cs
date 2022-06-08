using System;
using System.Collections.Generic;
using ScrumBoard.Model;

namespace ScrumBoardConsoleApp
{
    class ConsoleApp
    {
        enum Command
        {
            AddColumn,
            RenameColumn,
            RenameCard,
            ChangeCardDescription,
            ChangeCardPrority,
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
                { "rename-column", Command.RenameColumn },
                { "rename-card", Command.RenameCard },
                { "change-card-description", Command.ChangeCardDescription },
                { "change-card-prority", Command.ChangeCardPrority },
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
                catch (Exception e)
                {
                    LogError(e.Message);
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

            return GetCommandByString(rawString);
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
                case Command.RenameColumn:
                    RenameColumn(board);
                    break;
                case Command.RenameCard:
                    RenameCard(board);
                    break;
                case Command.ChangeCardDescription:
                    ChangeCardDescription(board);
                    break;
                case Command.ChangeCardPrority:
                    ChangeCardPriority(board);
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
            List<Column> columns = board.GetAllColumns();

            if (columns.Count == 0)
            {
                Console.WriteLine("There is no columns yet. Use 'add-column' command to add some columns.");
            }

            foreach (Column column in columns)
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
            Card.PriorityType priority = ReadPriority("Enter priority");

            board.AddNewCard(name, description, priority);

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

        private static void RenameColumn(Board board)
        {
            Console.WriteLine("Enter the column name you want to rename.");
            string name = Console.ReadLine();
            Console.WriteLine("Enter the new column name.");
            string newName = Console.ReadLine();

            board.RenameColumn(name, newName);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Column renamed successfully.");
            Console.ResetColor();
        }

        private static void RenameCard(Board board)
        {
            Console.WriteLine("Enter the card name you want to rename.");
            string name = Console.ReadLine();
            Console.WriteLine("Enter the new card name.");
            string newName = Console.ReadLine();

            board.RenameCard(name, newName);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Card renamed successfully.");
            Console.ResetColor();
        }

        private static void ChangeCardDescription(Board board)
        {
            Console.WriteLine("Enter the card name for which want to change the description.");
            string name = Console.ReadLine();
            Console.WriteLine("Enter the new description.");
            string newDescription = Console.ReadLine();

            board.GetCardByName(name).ChangeDescription(newDescription);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Card's description changed successfully.");
            Console.ResetColor();
        }

        private static void ChangeCardPriority(Board board)
        {
            Console.WriteLine("Enter the card name for which want to change the priorty.");
            string name = Console.ReadLine();
            Card.PriorityType priority = ReadPriority("Enter the new priority.");

            board.GetCardByName(name).ChangePriority(priority);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Card's priority changed successfully.");
            Console.ResetColor();
        }

        private static void ShowHelp()
        {
            Console.WriteLine("'help'\t\t\t\tShows this help message.");
            Console.WriteLine("'add-column'\t\t\tAdd column on the board. Column names must be unique");
            Console.WriteLine("'add-card'\t\t\tAdd card on the board. Card will be placed in the first column.");
            Console.WriteLine("'move-card'\t\t\tMove card to specific column");
            Console.WriteLine("'rename-column'\t\t\tChanges the column's name");
            Console.WriteLine("'rename-card'\t\t\tChanges the card's name");
            Console.WriteLine("'change-card-description'\tChanges the card's description");
            Console.WriteLine("'change-card-priority'\t\tChanges the card's priority");
            Console.WriteLine("'show'\t\t\t\tShows the current board.");
            Console.WriteLine("'exit'\t\t\t\tExit the program.");
        }

        private static Command GetCommandByString(string commandString)
        {
            if (!_mapStringToCommand.ContainsKey(commandString))
            {
                throw new ArgumentException($"Unknown command '{commandString}'. Use 'help' command to see available commands.");
            }

            return _mapStringToCommand[commandString];
        }

        private static Card.PriorityType GetPriorityByString(string priorityString)
        {
            if (!_mapStringToPriority.ContainsKey(priorityString))
            {
                throw new ArgumentException("Invalid priority type. Available values: 'minor', 'normal', 'major', 'critical', 'blocker'");
            }

            return _mapStringToPriority[priorityString];
        }

        private static Card.PriorityType ReadPriority(string msg)
        {
            while (true)
            {
                Console.WriteLine(msg);
                string priorityString = Console.ReadLine();

                if (!_mapStringToPriority.ContainsKey(priorityString))
                {
                    LogError("Invalid priority type. Available values: 'minor', 'normal', 'major', 'critical', 'blocker'");
                }
                else
                {
                    return _mapStringToPriority[priorityString];
                }
            }
        }

        private static void LogError(string msg)
        {
            Console.BackgroundColor = ConsoleColor.Red;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine(msg);
            Console.ResetColor();
        }
    }
}
