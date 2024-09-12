using Microsoft.AspNetCore.Mvc;
using Eapproval.Models;
using Eapproval.services;

using System.Text.Json;
using Eapproval.Helpers;
using Eapproval.Services;
using IronPdf;
using Eapproval.Helpers.IHelpers;
using Eapproval.Services.IServices;


namespace Eapproval.Controllers;

    [Route("/")]
    [ApiController]
    public class LocationListsController : Controller
    {
    
        private readonly ILocationService _locationService;

        public LocationListsController(ILocationService locationService)
        {
            _locationService = locationService;
      
        }


        [HttpGet]
        [Route("getLocations")]
        public async Task<IActionResult> GetLocations()
        {
            var locations = await _locationService.GetAllLocations();
            return Ok(locations);

        }


   
      

      


   



    


  






        



}
