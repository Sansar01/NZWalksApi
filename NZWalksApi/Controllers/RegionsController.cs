using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZWalksApi.Data;
using NZWalksApi.Models.Domain;

namespace NZWalksApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly NZWalksDbContext nZWalksDbContext;

        public RegionsController(NZWalksDbContext dbContextOptions)
        {
            this.nZWalksDbContext = dbContextOptions;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            //var regions = new List<Region>
            //{
            //    new Region
            //    {
            //        Id = Guid.NewGuid(),
            //        Name = "Auckland Region",
            //        Code = "AKL",
            //        RegionImageUrl = ""
            //    },
            //    new Region
            //    {
            //        Id = Guid.NewGuid(),
            //        Name = "Wellington Region",
            //        Code = "WL G",
            //        RegionImageUrl = ""
            //    }
            //};

            var regions = nZWalksDbContext.Regions.ToList();

            return Ok(regions); 
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public IActionResult GetById([FromRoute] Guid Id)
        {

        }
    }
}
