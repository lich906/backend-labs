// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using ScrumBoardWeb.Application.DTO.Input;

namespace ScrumBoardWeb.Application.DTO.Mapper
{
    public interface CardDtoMapperInterface
    {
        public CardDTO FromCardInput(CardInput cardInput);
    }
}
