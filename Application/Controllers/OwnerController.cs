using System.Net.Mime;
using Application.DTO.Request;
using Application.DTO.Response;
using Application.Filters;
using AutoMapper;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Support.Models;

namespace Application.Controllers;

[Route("/api/v1/[controller]")]
[ApiController]
[Produces(MediaTypeNames.Application.Json)]
[ProducesResponseType(400)]
[ProducesResponseType(500)]
[ProducesResponseType(401)]
[ProducesResponseType(403)]
public class OwnerController(IOwnerRepository ownerRepository, IMapper mapper) : ControllerBase
{
    
        [HttpGet]
        [ProducesResponseType(200)]
        [CustomAuthorize("Admin", "Tester")]
        public async Task<IActionResult> GetOwners()
        {
            var owners = await ownerRepository.GetAllAsync();
            var ownersResponse = mapper.Map<IEnumerable<Owner>, IEnumerable<OwnerResponse>>(owners);
            return Ok(ownersResponse);
        }
    
        [HttpGet("{id:long}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [CustomAuthorize("Admin", "Tester")]
        public async Task<IActionResult> GetOwnerById(long id)
        {
            var owner = await ownerRepository.GetByIdAsync(id);
            if (owner == null) return NotFound();
            var ownerResponse = mapper.Map<Owner, OwnerResponse>(owner);
            return Ok(ownerResponse);
        }
        
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(409)]
        [CustomAuthorize("Admin", "Tester", "Default")]
        public async Task<IActionResult> CreateOwner([FromBody] OwnerRequest ownerRequest)
        {
            var owner = mapper.Map<OwnerRequest, Owner>(ownerRequest);
            await ownerRepository.AddOwnerAsync(owner);
            var ownerResponse = mapper.Map<Owner, OwnerResponse >(owner);
            return StatusCode(201, ownerResponse);
        }
        
        [HttpPut("{id:long}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(409)]
        [CustomAuthorize("Admin", "Tester")]
        public async Task<IActionResult> UpdateOwner(long id, [FromBody] OwnerRequest ownerRequest)
        {
            var owner = await ownerRepository.GetByIdAsync(id);
            if (owner == null) return NotFound();
            mapper.Map(ownerRequest, owner);
            await ownerRepository.UpdateOwnerAsync(owner);
            var ownerResponse = mapper.Map<Owner, OwnerResponse>(owner);
            return Ok(ownerResponse);
        }
        
        [HttpDelete("{id:long}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [CustomAuthorize("Admin", "Tester")]
        public async Task<IActionResult> DeleteOwner(long id)
        {
            var owner = await ownerRepository.GetByIdAsync(id);
            if (owner == null) return NotFound();
            await ownerRepository.DeleteOwnerAsync(owner);
            return Ok("Owner deleted successfully.");
        }
}