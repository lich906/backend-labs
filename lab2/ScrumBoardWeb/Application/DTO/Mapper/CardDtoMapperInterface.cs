// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using ScrumBoardWeb.Application.Dto.Input;

namespace ScrumBoardWeb.Application.Dto.Mapper
{
    public interface CardDtoMapperInterface
    {
        public CardDto FromCardInput(CardInput cardInput);
    }
}
