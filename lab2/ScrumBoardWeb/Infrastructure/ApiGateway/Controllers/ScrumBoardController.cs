// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ScrumBoardWeb.Application.DTO;
using ScrumBoardWeb.Application.Service;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ScrumBoardWeb.Infrastructure.ApiGateway.Controllers
{
    [Route("api/board")]
    [ApiController]
    public class ScrumBoardController : ControllerBase
    {
        private readonly ScrumBoardServiceInterface _scrumBoardService;

        public ScrumBoardController(ScrumBoardServiceInterface scrumBoardService)
        {
            _scrumBoardService = scrumBoardService;
        }

        // GET: api/board
        [HttpGet]
        public IActionResult Get()
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
            catch (ApplicationException e)
            {
                return Problem(e.Message);
            }
        }

        // GET: api/board/5/column
        [HttpGet("{boardId}/column")]
        public IActionResult GetColumns(int boardId)
        {
            try
            {
                return Ok(_scrumBoardService.GetAllBoardColumns(boardId));
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
        [HttpGet("{boardId}/column/{id}")]
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
            catch (ApplicationException e)
            {
                return Problem(e.Message);
            }
        }

        // GET: api/board/5/column/3/card
        [HttpGet("{boardId}/column/{columnId}/card")]
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
        [HttpGet("{boardId}/column/{columnId}/card/{id}")]
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
            catch (ApplicationException e)
            {
                return Problem(e.Message);
            }
        }

        //// POST api/<ScrumBoardController>
        //[HttpPost]
        //public void Post([FromBody] string value)
        //{
        //}

        //// PUT api/<ScrumBoardController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE api/<ScrumBoardController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
