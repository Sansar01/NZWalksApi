using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZWalksApi.Data;
using NZWalksApi.Models.Domain;
using NZWalksApi.Models.DTO;

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

            var regionsDomain = nZWalksDbContext.Regions.ToList();

            var regionDto = new List<RegionsDto>();

            foreach(var regionDomain in regionsDomain)
            {
                regionDto.Add(new RegionsDto() { 
                     Id = regionDomain.Id,
                     Name = regionDomain.Name,
                     Code = regionDomain.Code,
                     RegionImageUrl = regionDomain.RegionImageUrl

                });
            }

            return Ok(regionDto); 
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public IActionResult GetById([FromRoute] Guid Id)
        {
            var regionDomain = nZWalksDbContext.Regions.Find(Id);

            //var regions = nZWalksDbContext.Regions.FirstOrDefault(x=>x.Id == id);

            if (regionDomain ==null)
            {
                return NotFound();
            }

            var regionDto = new RegionsDto
            {
                Id = regionDomain.Id,
                Name = regionDomain.Name,
                Code = regionDomain.Code,
                RegionImageUrl = regionDomain.RegionImageUrl
            };

            return Ok(regionDto); 
        }

        [HttpPost]
        public IActionResult Create([FromBody] AddRegionRequestDto addRegionRequestDto)
        {
            //Map or convert DTO to domain model
            var regionDomainModel = new Region
            {
                Code = addRegionRequestDto.Code,
                Name = addRegionRequestDto.Name,
                RegionImageUrl = addRegionRequestDto.RegionImageUrl
            };

            nZWalksDbContext.Regions.Add(regionDomainModel);
            nZWalksDbContext.SaveChanges();

            var regionDto = new RegionsDto
            {
                Id = regionDomainModel.Id,
                Code = regionDomainModel.Code,
                Name = regionDomainModel.Name,
                RegionImageUrl = regionDomainModel.RegionImageUrl
            };

            return CreatedAtAction(nameof(GetById), new { id = regionDto.Id }, regionDto);
        }

        [HttpPut]
        [Route("{id:Guid}")]
        public IActionResult Update([FromRoute] Guid id, [FromBody] UpdateRegionRequestDto updateRegionRequestDto)
        {
            var regionDomainModel = nZWalksDbContext.Regions.FirstOrDefault(x => x.Id == id);

            if(regionDomainModel == null)
            {
                return NotFound();
            }

            // Map Dto to domain model
            regionDomainModel.Code = updateRegionRequestDto.Code;
            regionDomainModel.Name = updateRegionRequestDto.Name;
            regionDomainModel.RegionImageUrl = updateRegionRequestDto.RegionImageUrl;

            nZWalksDbContext.SaveChanges();

            //covert domainModel to dto
            var regionDto = new RegionsDto
            {
                Id = regionDomainModel.Id,
                Code = regionDomainModel.Code,
                Name = regionDomainModel.Name,
                RegionImageUrl = regionDomainModel.RegionImageUrl
            };

            return Ok(regionDto);
        }
    }
}
