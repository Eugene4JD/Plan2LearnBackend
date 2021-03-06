using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApplication.Models;
using WebApplication.Services;

namespace WebApplication.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ResourceController : ControllerBase
    {
        private IResourceService _resourceService;

        public ResourceController(IResourceService resourceService)
        {
            _resourceService = resourceService;
        }

        [HttpPost]
        [Route("/api/resource")]
        public async Task<ActionResult<Resource>> CreateResource([FromBody] Resource resource)
        {
            try
            {
                return Ok(await _resourceService.AddResource(resource));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete]
        [Route("/api/resource")]
        public async Task<ActionResult<int>> DeleteResource([FromQuery] int id)
        {
            try
            {
                return Ok(await _resourceService.RemoveResource(id));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet]
        [Route("/api/resource")]
        public async Task<ActionResult<List<Resource>>> GetResources([FromQuery] int resourcesAmount)
        {
            try
            {
                return Ok(await _resourceService.GetResources(resourcesAmount));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}