// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ScrumBoardWeb.Application.DTO;
using ScrumBoardWeb.Application.DTO.Input;
using ScrumBoardWeb.Application.Service;

namespace ScrumBoardWeb.Infrastructure.ApiGateway.Controllers
{
    [Route("api/board/{boardId}/column")]
    [ApiController]
    public class BoardColumnController : ControllerBase
    {
        private readonly ScrumBoardServiceInterface _scrumBoardService;

        public BoardColumnController(ScrumBoardServiceInterface scrumBoardService)
        {
            _scrumBoardService = scrumBoardService;
        }

        // GET: api/board/5/column
        [HttpGet]
        public IActionResult GetColumns(int boardId)
        {
            try
            {
                return Ok(_scrumBoardService.GetBoardColumns(boardId));
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

        // GET: api/board/5/column/3
        [HttpGet("{id}")]
        public IActionResult GetColumn(int boardId, int id)
        {
            try
            {
                return Ok(_scrumBoardService.GetColumn(boardId, id));
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

        // POST: api/board/5/column/create
        [HttpPost("create")]
        public IActionResult CreateColumn(int boardId, [FromBody] ColumnInput columnInput)
        {
            try
            {
                _scrumBoardService.CreateColumn(boardId, new ColumnDTO(columnInput.Name, new List<CardDTO>()));

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

        // DELETE: api/board/5/column/create
        [HttpDelete("{id}/delete")]
        public IActionResult DeleteColumn(int boardId, int id)
        {
            try
            {
                _scrumBoardService.DeleteColumn(boardId, id);

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
