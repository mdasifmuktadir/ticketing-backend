using Microsoft.AspNetCore.Mvc;
using Eapproval.Models;
using Eapproval.services;

using System.Text.Json;
using Eapproval.Helpers;
using Eapproval.Services;
using IronPdf;
using Eapproval.Helpers.IHelpers;
using Eapproval.Services.IServices;


namespace Eapproval.Controllers.LocationControllers;

    [Route("/")]
    [ApiController]
    public class LocationController : Controller
    {
    
        private readonly ILocationService _locationService;

  

        public LocationController(ILocationService locationService)
        {
            _locationService = locationService;
      
        }


        [HttpPost]
        [Route("addLocation")]
        public async Task<IActionResult> addLocation(IFormCollection data)
        {
            var newLocation = new Location
            {
                Name = data["name"],
            };

             await _locationService.AddLocation(newLocation);
            return Ok(true);

        }


        [HttpPost]
        [Route("deleteLocation")]
        public async Task<IActionResult> DeleteLocation(IFormCollection data)
        {


            await _locationService.DeleteLocation(int.Parse(data["id"]));
            return Ok(true);

        }

      



}
