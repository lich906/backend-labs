// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.Collections.Generic;
using ScrumBoardWeb.Application.Dto;
using ScrumBoardWeb.Database.Entity;
using ScrumBoard.Model;

namespace ScrumBoardWeb.Database
{
    public class DatabaseEntityHydrator : IDatabaseEntityHydrator
    {
        public List<ScrumBoard.Model.Board> HydrateBoards(List<Entity.Board> boardsData)
        {
            List<ScrumBoard.Model.Board> hydratedBoards = new();

            if (boardsData != null)
            {
                foreach (Entity.Board boardData in boardsData)
                {
                    hydratedBoards.Add(HydrateBoard(boardData));
                }
            }

            return hydratedBoards;
        }

        public ScrumBoard.Model.Board HydrateBoard(Entity.Board board)
        {
            ScrumBoard.Model.Board hydratedBoard = new(board.Name);

            foreach (ScrumBoard.Model.Column hydratedColumn in HydrateColumns(board.Columns))
            {
                hydratedBoard.AppendColumn(hydratedColumn);
            }

            return hydratedBoard;
        }

        public List<ScrumBoard.Model.Column> HydrateColumns(List<Entity.Column> columnsData)
        {
            List<ScrumBoard.Model.Column> hydratedColumns = new();

            if (columnsData != null)
            {
                foreach (Entity.Column columnData in columnsData)
                {
                    hydratedColumns.Add(HydrateColumn(columnData));
                }
            }

            return hydratedColumns;
        }

        public ScrumBoard.Model.Column HydrateColumn(Entity.Column column)
        {
            ScrumBoard.Model.Column hydratedColumn = new(column.Name);

            foreach (Entity.Card cardData in column.Cards)
            {
                hydratedColumn
                    .AddCard(new ScrumBoard.Model.Card(
                        cardData.Name,
                        cardData.Description,
                        (ScrumBoard.Model.Card.PriorityType)cardData.Priority
                    ));
            }

            return hydratedColumn;
        }

        public ScrumBoard.Model.Card HydrateCard(Entity.Card card)
        {
            return new ScrumBoard.Model.Card(
                card.Name,
                card.Description,
                (ScrumBoard.Model.Card.PriorityType)card.Priority
            );
        }

        public List<ScrumBoard.Model.Card> HydrateCards(List<Entity.Card> cardsData)
        {
            List<ScrumBoard.Model.Card> hydratedCards = new();

            if (cardsData != null)
            {
                foreach (Entity.Card cardData in cardsData)
                {
                    hydratedCards.Add(HydrateCard(cardData));
                }
            }

            return hydratedCards;
        }
    }
}
