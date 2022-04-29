using System;
using System.Collections.Generic;
using System.Linq;
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
            Exit,
            Unknown
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

            Command lastCommand = Command.Unknown;

            while (lastCommand != Command.Exit)
            {
                (Command command, string[] args) = ReadCommand();
                lastCommand = command;
                try
                {
                    HandleCommand((command, args), board);
                }
                catch(Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }

        private static Board InitBoard()
        {
            Console.WriteLine("Enter board name...");
            string name = Console.ReadLine();

            return new Board(name);
        }

        private static (Command, string[]) ReadCommand()
        {
            string rawString = Console.ReadLine();

            string[] splittedStr = rawString.Split(' ');

            if (!_mapStringToCommand.ContainsKey(splittedStr[0]))
            {
                return (Command.Unknown, new string[0]);
            }

            return (_mapStringToCommand[splittedStr[0]], splittedStr.Skip(1).ToArray());
        }

        private static void HandleCommand((Command type, string[] args) command, Board board)
        {
            ValidateCommand(command);

            switch (command.type)
            {
                case Command.AddCard:
                    board.AddNewCard(command.args[0], command.args[1], _mapStringToPriority[command.args[2]]);
                    Console.WriteLine($"Card '{command.args[0]}' successfully added to first column");
                    break;
                case Command.AddColumn:
                    board.AddNewColumn(command.args[0]);
                    Console.WriteLine($"Column '{command.args[0]}' successfully added.");
                    break;
                case Command.Exit:
                    Console.WriteLine("Gracefully exiting...");
                    break;
                case Command.Show:
                    ShowBoard(board);
                    break;
                default:
                    throw new ArgumentException("In function HandleCommand: Invalid command type.");
            }
        }

        private static void ValidateCommand((Command type, string[] args) command)
        {
            switch (command.type)
            {
                case Command.AddCard:
                    if (command.args.Length != 3)
                    {
                        throw new ArgumentException("Invalid arguments for 'add-card' command. Usage: add-card <card name> <description> <priority>");
                    }
                    if (!_mapStringToPriority.ContainsKey(command.args[2]))
                    {
                        throw new ArgumentException($"Unknown priority '{command.args[2]}' specified. Use 'help' command for info.");
                    }
                    break;
                case Command.AddColumn:
                    if (command.args.Length != 1)
                    {
                        throw new ArgumentException("Invalid arguments for 'add-column' command. Usage: add-column <column name>");
                    }
                    break;
                case Command.Unknown:
                    throw new ArgumentException("Unknown command. Use 'help' command for info.");
                default:
                    throw new ArgumentException("In function ValidateCommand: Invalid command type.");
            }
        }

        private static void ShowBoard(Board board)
        {
            List<BoardColumn> columns = board.GetAllColumns();

            foreach (BoardColumn column in columns)
            {
                Console.WriteLine($"====== {column.Name} ======");
                foreach(Card card in column.GetAllCards())
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
    }
}
