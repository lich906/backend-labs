// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ScrumBoardWeb.Application.DTO;
using ScrumBoardWeb.Application.DTO.Input;
using ScrumBoardWeb.Application.DTO.Mapper;
using ScrumBoardWeb.Application.Service;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ScrumBoardWeb.ApiGateway.Controllers
{
    [Route("api/board")]
    [ApiController]
    public class BoardController : ControllerBase
    {
        private readonly IScrumBoardService _scrumBoardService;

        public BoardController(IScrumBoardService scrumBoardService)
        {
            _scrumBoardService = scrumBoardService;
        }

        // GET: api/board
        [HttpGet]
        public IActionResult GetBoards()
        {
            try
            {
                return Ok(_scrumBoardService.GetAllBoards());
            }
            catch (IndexOutOfRangeException e)
            {
                return NotFound(e.Message);
            }
            catch(ApplicationException e)
            {
                return Problem(e.Message);
            }
        }

        // GET: api/board/5
        [HttpGet("{id}")]
        public IActionResult GetBoard(int id)
        {
            try
            {
                return Ok(_scrumBoardService.GetBoard(id));
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

        // POST: api/board/create
        [HttpPost("create")]
        public IActionResult CreateBoard([FromBody] BoardInput boardInput)
        {
            try
            {
                _scrumBoardService.CreateBoard(new BoardDto(boardInput.Name, new List<ColumnDto>()));
            }
            catch (ApplicationException)
            {
                return BadRequest("Failed to create board");
            }

            return Ok("Board successfully created");
        }

        // DELETE: api/board/5/delete
        [HttpDelete("{id}/delete")]
        public IActionResult DeleteBoard(int id)
        {
            try
            {
                _scrumBoardService.DeleteBoard(id);
            }
            catch (IndexOutOfRangeException e)
            {
                return NotFound(e.Message);
            }
            catch (ApplicationException)
            {
                return BadRequest("Failed to delete board");
            }

            return Ok("Board successfully deleted");
        }
    }
}
