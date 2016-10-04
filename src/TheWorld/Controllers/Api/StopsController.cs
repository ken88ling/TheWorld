using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Logging;
using TheWorld.Models;
using TheWorld.Services;
using TheWorld.ViewModels;

namespace TheWorld.Controllers.Api
{
    [Route("/api/trips/{tripName}/stops")]
    public class StopsController : Controller
    {
        private readonly ILogger<StopsController> _logger;
        private readonly IWorldRepository _repository;
        private readonly GeoCoordsService _coordesService;

        public StopsController(IWorldRepository repository, ILogger<StopsController> logger,GeoCoordsService coordsService)
        {
            _repository = repository;
            _logger = logger;
            _coordesService = coordsService;
        }

        [HttpGet("")]
        public IActionResult Get(string tripName)
        {
            try
            {
                var trip = _repository.GetTripByName(tripName);
                return Ok(Mapper.Map<IEnumerable<StopViewModel>>(trip.Stops.OrderBy(s => s.Order).ToList()));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get stops:{0}", ex);
            }
            return BadRequest("Failed to get stops");
        }

        [HttpPost("")]
        public async Task<IActionResult> Post(string tripName, [FromBody] StopViewModel vm)
        {
            try
            {
                //if the VM is valid
                if (ModelState.IsValid)
                {
                    var newStop = Mapper.Map<Stop>(vm);

                    //Lookup the Geocodes
                    var result = await _coordesService.GetCoordsAsync(newStop.Name);
                    if (!result.Success)
                    {
                        _logger.LogError(result.Message);
                    }
                    else
                    {
                        newStop.Latitude = result.Latitude;
                        newStop.Longitude = result.Longitude;
                    }

                    // Save to the Database
                    _repository.AddStop(tripName,newStop);

                    if (await _repository.SaveChangeAsync())
                    {
                        return Created($"/api/trips/{tripName}/stops/{newStop.Name}",
                        Mapper.Map<StopViewModel>(newStop));
                    }
                    
                }

            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to save new stop: {0}",ex);
            }
            return BadRequest("Failed to save new stops");
        }
    }
}
