using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APIDemoApp.Business.Interfaces;
using APIDemoApp.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APIDemoApp.Controllers
{
    [Authorize]
    [ApiVersion("1")]
    [ApiExplorerSettings(GroupName ="v1")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBooksBusinessContract _booksBusinessContract;

        public BooksController(IBooksBusinessContract booksBusinessContract)
        {
            _booksBusinessContract = booksBusinessContract;
        }


        // GET api/values/5
        [HttpGet]
        [Route("api/v{version:apiVersion}/Books")]
        [ProducesResponseType(200, Type = typeof(Books))]
        [ProducesResponseType(401, Type = typeof(FailureResponse))]
        [ProducesResponseType(404, Type = typeof(FailureResponse))]
        [ProducesResponseType(500, Type = typeof(FailureResponse))]
        public async Task<ActionResult> GetAsync(int id)
        {
            ObjectResult result;
            try
            {
                var response = await _booksBusinessContract.ReadAsync(id);
                if (response != null)
                {
                    Books booksReadResponse = new Books()
                    {
                        BookName = response.Name,
                        Author = response.Author,
                        Country = response.Country,
                        Language = response.Language,
                        Genre = response.Genre,
                        CurrentEdition = response.CurrentEdition,
                        Isbn = response.Isbn
                    };
                    result = StatusCode(StatusCodes.Status200OK, booksReadResponse);
                }
                else
                {
                    result = StatusCode(StatusCodes.Status404NotFound, "book does not exist");
                }
            }
            catch (Exception ex)
            {
                result = StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
            return result;
        }

        // POST api/values
        [HttpPost]
        [Route("api/v{version:apiVersion}/Books")]
        [ProducesResponseType(201, Type = typeof(CreateResponse))]
        [ProducesResponseType(400, Type = typeof(FailureResponse))]
        [ProducesResponseType(401, Type = typeof(FailureResponse))]
        [ProducesResponseType(500, Type = typeof(FailureResponse))]
        public async Task<ActionResult> CreateAsync(Books book)
        {
            ObjectResult result;
            try
            {
                var bookCreateReasponse = await _booksBusinessContract.CreateAsync(new Business.Models.Books() {
                    Author = book.Author,
                    Country = book.Country,
                    CurrentEdition = book.CurrentEdition,
                    Genre = book.Genre,
                    Isbn = book.Isbn,
                    Language = book.Language,
                    Name = book.BookName
                });
                if (bookCreateReasponse != 0)
                {
                    CreateResponse createResponse = new CreateResponse()
                    {
                        BookId = bookCreateReasponse,
                        IsCreated = true
                    };
                    result = StatusCode(StatusCodes.Status201Created, createResponse);
                }
                else
                {
                    result = StatusCode(StatusCodes.Status400BadRequest, new FailureResponse() {
                        Error = "Book already exists"
                    });
                }
            }
            catch (Exception ex)
            {
                result = StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
            return result;
        }

        // PUT api/values/5
        [HttpPut]
        [Route("api/v{version:apiVersion}/Books")]
        [ProducesResponseType(201, Type = typeof(UpdateResponse))]
        [ProducesResponseType(401, Type = typeof(FailureResponse))]
        [ProducesResponseType(500, Type = typeof(FailureResponse))]
        public async Task<ActionResult> UpdateAsync(UpdateBookEdition updateBookEditionModel)
        {
            ObjectResult result;
            try
            {
                var response = await _booksBusinessContract.UpdateAsync(updateBookEditionModel.BookId, updateBookEditionModel.Edition);
                if (response)
                {
                    UpdateResponse updateResponse = new UpdateResponse()
                    {
                        IsUpdated = true
                    };
                    result = StatusCode(StatusCodes.Status201Created, updateResponse);
                }
                else
                {
                    result = StatusCode(StatusCodes.Status404NotFound, new UpdateResponse()
                    {
                        IsUpdated = false
                    });
                }
            }
            catch (Exception ex)
            {
                result = StatusCode(StatusCodes.Status500InternalServerError, new UpdateResponse()
                {
                    IsUpdated = false
                });
            }
            return result;
        }

        // DELETE api/values/5
        [HttpDelete]
        [Route("api/v{version:apiVersion}/Books")]
        [ProducesResponseType(200, Type = typeof(DeleteResponse))]
        [ProducesResponseType(401, Type = typeof(FailureResponse))]
        [ProducesResponseType(404, Type = typeof(FailureResponse))]
        [ProducesResponseType(500, Type = typeof(FailureResponse))]
        public async Task<ActionResult> DeleteAsync(int id)
        {
            ObjectResult result;
            try
            {
                var response = await _booksBusinessContract.DeleteAsync(id);
                if (response)
                {
                    result = StatusCode(StatusCodes.Status200OK, new DeleteResponse()
                    {
                        IsDeleted = true
                    });
                }
                else
                {
                    result = StatusCode(StatusCodes.Status404NotFound, new DeleteResponse()
                    {
                        IsDeleted = false
                    });
                }
            }
            catch (Exception ex)
            {
                result = StatusCode(StatusCodes.Status500InternalServerError, false);
            }
            return result;
        }
    }
}
