using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BarOMeterWebApiCore.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;

namespace BarOMeterWebApiCore.Controllers
{
    /// <summary>
    /// BarController class for the Wen Api.
    /// <para>
    /// Default route: "api/bars.
    /// </para>
    /// <para>
    ///  Can respond to various GET/ PUT/ POST/ DELETE Http requests.
    /// </para>
    /// </summary>
    [Route("api/bars")]
    [ApiController]
    public class BarController : ControllerBase
    {
        
        private IRepository db;

        /// <summary>
        /// Constructor for the controller.
        /// <para>
        /// Gets the repository for use.
        /// </para> 
        /// </summary>
        /// <param name="barRepo">
        /// Dependency injected through Startup.ConfigureServices()
        /// </param>
        public BarController(IRepository barRepo)
        {
            if (db == null)
                db = barRepo;
        }

        /// <summary>
        /// Returns all Bars
        /// </summary>
        /// <returns>
        /// A List of Bars and ActionResult Ok (Http 200) if found.
        /// If empty, returns NoContent.
        /// </returns>
        [HttpGet] // /api/bars
        [ProducesResponseType(typeof(List<Bar>), 200)]
        [ProducesResponseType( typeof(Nullable) ,StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetBars()
        {
            var bars = await db.GetBars();
            if (bars.Count() > 0)
                return Ok(bars);
            else
                return NotFound();
        }

        /// <summary>
        /// Returns a specific Bar found by provided id
        /// </summary>
        /// <param name="id">
        /// id is BarName property of Bar class.
        /// </param>
        /// <example>
        /// "https://IP:PORT/api/bars/Katrines Kælder"
        /// </example>
        /// <returns>
        /// ActionResult Ok with the found Bar object if successful.
        /// ActionResult NotFound if the bar could not be found.
        /// </returns>
        //api/bars/{id}
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Bar), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetBar(string id)
        {
            var bar = await db.GetBar(id);
            if (bar != null)
                return Ok(bar);
            else
                return NotFound($"Bar with BarName: {id} not found");
        }

        /// <summary>
        /// Adds a Bar object to the database, if bar with same name does not exist
        /// </summary>
        /// <param name="bar">
        /// Bar object supplied in the Http Body in JSON formatting
        /// </param>
        /// <returns>
        /// If successful, will return the created object with /api/bars/{id}
        /// If unsuccessful, returns 400 (Bad Request)
        /// </returns>
        [HttpPost]
        [ProducesResponseType(typeof(Bar), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddBar([FromBody]Bar bar)
        {
            var b = new Bar();
            b.BarName = bar.BarName;
            b.Rating = bar.Rating;
            bool success = await db.AddBar(b);
            if (success)
                return CreatedAtAction(nameof(GetBar), new { id = bar.BarName }, bar);
            else
            {
                return BadRequest();
            }
        }

        /// <summary>
        /// Deletes a bar identified by id
        /// </summary>
        /// <param name="id">
        /// string id which must match a BarName
        /// </param>
        /// <returns>
        /// Returns 200 Ok if deletion is successful.
        /// Returns 404 Not found, if bar could not be found.
        /// </returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteBar(string id)
        {
            bool result = await db.RemoveBar(id);
            if (result)
                return Ok();
            return NotFound(id);
        }

        /// <summary>
        /// Updates a bar if it already exists.
        /// </summary>
        /// <param name="bar">
        /// Bar object supplied in the Http Body in JSON formatting.
        /// Must include "BarName": string and "Rating": int
        /// </param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(typeof(Bar), StatusCodes.Status202Accepted)]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateRating([FromBody]Bar bar)
        {
            // Skal lige laves ordenligt udfra EFCore
            return Unauthorized("Ez lol");
        }
    }
}