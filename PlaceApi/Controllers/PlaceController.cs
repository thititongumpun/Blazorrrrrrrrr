using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using PlaceApi.Db;
using PlaceApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlaceApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlaceController : ControllerBase
    {
        private readonly PlaceDbContext _context;
        private readonly IHubContext<PlaceApiHub> _hubContext;

        public PlaceController(PlaceDbContext context, IHubContext<PlaceApiHub> hubContext)
        {
            _context = context;
            _hubContext = hubContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetTopPlaces()
        {
            var places = await _context.Places.OrderByDescending(o => o.Reviews).Take(10).ToListAsync();
            return Ok(places);
        }

        [HttpPost]
        public async Task<IActionResult> CreateNewPlace([FromBody] Place place)
        {
            var newId = _context.Places.Select(x => x.Id).Max() + 1;
            place.Id = newId;
            place.LastUpdated = DateTime.Now;

            await _context.AddAsync(place);
            int rowAffected = await _context.SaveChangesAsync();
            
            if (rowAffected > 0)
            {
                await _hubContext.Clients.All.SendAsync("AddedNewPlace", place.Id, place.Name);
            }

            return Ok(place);
        }

        [HttpPut]
        public IActionResult UpdatePlace([FromBody] Place place)
        {
            var placeUpdate = _context.Places.Find(place.Id);

            if (placeUpdate == null)
            {
                return NotFound();
            }

            placeUpdate.Name = place.Name;
            placeUpdate.Location = place.Location;
            placeUpdate.About = place.About;
            placeUpdate.Reviews = place.Reviews;
            placeUpdate.ImageData = place.ImageData;
            placeUpdate.LastUpdated = DateTime.Now;

            _context.Update(placeUpdate);
            _context.SaveChanges();

            return Ok(placeUpdate);
        }
    }
}
