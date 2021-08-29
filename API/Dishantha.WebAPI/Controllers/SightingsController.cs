using Dishantha.Application.Sightings.DTOs;
using Dishantha.Application.Sightings.Requests;
using Dishantha.Application.Sightings.Services;
using Dishantha.WebAPI.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Dishantha.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SightingsController : ControllerBase
    {

        private readonly ILogger<SightingsController> _logger;
        private readonly ISightingService _sightingService;

        public SightingsController(ILogger<SightingsController> logger, ISightingService sightingService)
        {
            _logger = logger;
            _sightingService = sightingService;
        }

        //GET api/Sightings
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var result = await _sightingService.GetAllAsync();
                return Ok(new ResponseModel<List<SightingDTO>>(result));
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Get All Sightings - Error" + ex.Message);
                return BadRequest(new ResponseModel<SightingDTO>(ex));
            }
        }

        //GET api/Sightings/1
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var result = await _sightingService.GetByIdAsync(id);
                return Ok(new ResponseModel<SightingDTO>(result));
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Get All Sightings - Error" + ex.Message);
                return BadRequest(new ResponseModel<SightingDTO>(ex));
            }

        }

        //POST api/Sightings
        [HttpPost]
        public async Task<IActionResult> Post(CreateSightingRequest model)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            try
            {                
                var result = await _sightingService.AddAsync(model);
                return Ok(new ResponseModel<SightingDTO>(result));
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Get All Sightings - Error" + ex.Message);
                return BadRequest(new ResponseModel<SightingDTO>(ex));
            }

        }

        //PUT api/Sightings/5
        [HttpPut]
        public async Task<IActionResult> Put(UpdateSightingRequest model)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            try
            {
                var result = await _sightingService.UpdateAsync(model);
                return Ok(new ResponseModel<SightingDTO>(result));
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Get All Sightings - Error" + ex.Message);
                return BadRequest(new ResponseModel<SightingDTO>(ex));
            }
        }

        //GET api/Sightings/search?value=''
        [HttpGet("Search")]
        public async Task<IActionResult> Search(string value)
        {
            try
            {
                var result = await _sightingService.Search(new SearchSightingRequest { SearchText = value });
                return Ok(new ResponseModel<List<SightingDTO>>(result));
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Search Sightings - Error" + ex.Message);
                return BadRequest(new ResponseModel<SightingDTO>(ex));
            }
        }

        // DELETE api/Sightings/5
        [HttpDelete("{id}")]        
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var result = await _sightingService.DeleteAsync(id);
                return Ok(new ResponseModel<bool>(result));
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Get All Sightings - Error" + ex.Message);
                return BadRequest(new ResponseModel<SightingDTO>(ex));
            }
        }

    }
}
