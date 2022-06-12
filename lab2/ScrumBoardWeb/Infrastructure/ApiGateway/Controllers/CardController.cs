// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ScrumBoardWeb.Application.DTO.Input;
using ScrumBoardWeb.Application.DTO.Mapper;
using ScrumBoardWeb.Application.Service;

namespace ScrumBoardWeb.Infrastructure.ApiGateway.Controllers
{
    [Route("api/board/{boardid}/column/{columnId}/card")]
    [ApiController]
    public class CardController : ControllerBase
    {
        private readonly ScrumBoardServiceInterface _scrumBoardService;
        private readonly CardDtoMapperInterface _cardDtoMapper;
        public CardController(ScrumBoardServiceInterface scrumBoardService, CardDtoMapperInterface cardDtoMapper)
        {
            _scrumBoardService = scrumBoardService;
            _cardDtoMapper = cardDtoMapper;
        }

        // GET: api/board/5/column/3/card
        [HttpGet]
        public IActionResult GetCards(int boardId, int columnId)
        {
            try
            {
                return Ok(_scrumBoardService.GetCards(boardId, columnId));
            }
            catch (IndexOutOfRangeException e)
            {
                return NotFound(e.Message);
            }
            catch (ApplicationException e)
            {
                return Problem(e.Message);
            }
        }

        // GET: api/board/5/column/3/card/2
        [HttpGet("{id}")]
        public IActionResult GetCard(int boardId, int columnId, int id)
        {
            try
            {
                return Ok(_scrumBoardService.GetCard(boardId, columnId, id));
            }
            catch (IndexOutOfRangeException e)
            {
                return NotFound(e.Message);
            }
            catch (InvalidOperationException e)
            {
                return NotFound(e.Message);
            }
            catch (ApplicationException e)
            {
                return Problem(e.Message);
            }
        }

        // POST: api/board/5/column/3/card/create
        [HttpPost("create")]
        public IActionResult CreateColumn(int boardId, [FromBody] CardInput columnInput)
        {
            try
            {
                _scrumBoardService.CreateCard(boardId, _cardDtoMapper.FromCardInput(columnInput));

                return Ok("Column successfully created");
            }
            catch (IndexOutOfRangeException e)
            {
                return NotFound(e.Message);
            }
            catch (ApplicationException e)
            {
                return Problem(e.Message);
            }
        }

        // DELETE: api/board/5/column/3/card/2/delete
        [HttpDelete("{id}/delete")]
        public IActionResult DeleteColumn(int boardId, int columnId, int id)
        {
            try
            {
                _scrumBoardService.DeleteCard(boardId, columnId, id);

                return Ok("Column successfully deleted");
            }
            catch (IndexOutOfRangeException e)
            {
                return NotFound(e.Message);
            }
            catch (ApplicationException e)
            {
                return Problem(e.Message);
            }
        }
    }
}
