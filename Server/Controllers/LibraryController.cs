﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.Models;
using Newtonsoft.Json;
using Server.Services.Classes;
using Server.Services.Interfaces;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LibraryController : ControllerBase
    {
        public ILibraryService _libraryService { get; set; }

        public LibraryController()
        {
            _libraryService = new LibraryService();
        }
        // GET: api/library/book
        [HttpGet("book")]
        public async Task<ActionResult<IEnumerable<Book>>> GetBooks()
        {
            try
            {
                var books = await _libraryService.GetBooksItemsAsync();
                return Ok(books);
            }
            catch (Exception err)
            {
                Console.WriteLine(err);
                return BadRequest(null);
            }
        }


        // GET: api/library/jornal
        [HttpGet("jornal")]
        public async Task<ActionResult<IEnumerable<Jornal>>> GetJornals()
        {
            try
            {
                var jornals = await _libraryService.GetJornalsItemsAsync();
                jornals.Where(j => j.IsActive);
                return Ok(jornals);
            }
            catch (Exception err)
            {
                Console.WriteLine(err);
                return BadRequest(null);
            }
        }

        // GET: api/library/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetBook(int id)
        {
            var item = await _libraryService.GetItemAsync(id);

            if (item == null)
            {
                return NotFound();
            }

            return Ok(item);
        }

        // POST: api/library/create/book
        [HttpPost("Create/book")]
        public async Task<ActionResult<bool>> CreateBook([FromBody] string json)
        {
            try
            {
                var book = JsonConvert.DeserializeObject<Book>(json);
                if (book.Price == 0 && book.Copies == 0)
                {
                    throw new Exception("You need enter a valid price and copies. Pleate try again!");
                }
                if (book.Price == 0)
                {
                    throw new Exception("You need enter a valid price(more than 1). Pleate try again!");
                }
                if (book.Copies == 0)
                {
                    throw new Exception("You need enter a valid copies(more than 1). Pleate try again!");
                }
                var success = await _libraryService.CreateBookAsync(book);
                if (success)
                    return Ok();
                else
                    return BadRequest();

            }
            catch (Exception err)
            {
                Console.WriteLine(err);
                return NotFound(err.Message);
            }
        }

        // POST: api/library/create
        [HttpPost("Create/jornal")]
        public async Task<ActionResult<bool>> CreateJornal([FromBody] string json)
        {
            try
            {
                var jornal = JsonConvert.DeserializeObject<Jornal>(json);
                if (jornal.Price == 0)
                {
                    throw new Exception("You need enter a valid price(more than 1). Pleate try again!");
                }
                var success = await _libraryService.CreateJornalAsync(jornal);
                if (success)
                    return Ok();
                else
                    return BadRequest();

            }
            catch (Exception err)
            {
                Console.WriteLine(err);
                return NotFound();
            }
        }

        // PATCH: api/library/5
        [HttpPut("{guid}")]
        public async Task<ActionResult<AbstractItem>> DeleteItem(Guid guid)
        {
            var item = await _libraryService.DeleteItemAsync(guid);

            if (item == null)
            {
                return NotFound();
            }

            return Ok(item);
        }

    }
}