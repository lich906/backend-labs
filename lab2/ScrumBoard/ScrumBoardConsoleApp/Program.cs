using System;
using System.Collections.Generic;
using System.Linq;
using ScrumBoardService;

namespace ScrumBoardConsoleApp
{
    class ConsoleApp
    {
        enum Command
        {
            AddColumn,
            AddCard,
            MoveCard,
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

        static Board InitBoard()
        {
            Console.WriteLine("Enter board name...");
            string name = Console.ReadLine();

            return new Board(name);
        }

        static (Command, string[]) ReadCommand()
        {
            string rawString = Console.ReadLine();

            string[] splittedStr = rawString.Split(' ');

            if (!_mapStringToCommand.ContainsKey(splittedStr[0]))
            {
                return (Command.Unknown, new string[0]);
            }

            return (_mapStringToCommand[splittedStr[0]], splittedStr.Skip(1).ToArray());
        }

        static void ValidateCommand((Command type, string[] args) command)
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

        static void HandleCommand((Command type, string[] args) command, Board board)
        {
            ValidateCommand(command);

            switch (command.type)
            {
                case Command.AddCard:
                    board.AddNewCard(command.args[0], command.args[1], _mapStringToPriority[command.args[2]]);
                    break;
                case Command.AddColumn:
                    board.AddNewColumn(command.args[0]);
                    break;
                case Command.Exit:
                    break;
                default:
                    throw new ArgumentException("In function HandleCommand: Invalid command type.");
            }
        }
    }
}
