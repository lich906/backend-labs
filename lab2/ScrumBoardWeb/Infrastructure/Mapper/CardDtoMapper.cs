// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ScrumBoard.Model;
using ScrumBoardWeb.Application.DTO;
using ScrumBoardWeb.Application.DTO.Input;
using ScrumBoardWeb.Application.DTO.Mapper;

namespace ScrumBoardWeb.Infrastructure.Mapper
{
    public class CardDtoMapper : CardDtoMapperInterface
    {
        public CardDto FromCardInput(CardInput cardInput)
        {
            return new CardDto(
                cardInput.Name,
                cardInput.Description,
                (Card.PriorityType)cardInput.Priority
            );
        }
    }
}
