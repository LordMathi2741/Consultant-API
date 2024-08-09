using System.Net.Mime;
using Application.DTO.Request;
using Application.DTO.Response;
using Application.Filters;
using AutoMapper;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Support.Factory.Cylinder;

namespace Application.Controllers;

[Route("/api/v1/[controller]")]
[ApiController]
[Produces(MediaTypeNames.Application.Json)]
[ProducesResponseType(400)]
[ProducesResponseType(500)]
[ProducesResponseType(401)]
[ProducesResponseType(403)]
public class CylinderProviderController(ICylinderProviderRepository cylinderProviderRepository, IMapper mapper) : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(201)]
    [ProducesResponseType(409)]
    [CustomAuthorize("Admin", "Tester", "Default")]
    public async Task<IActionResult> CreateCylinderProvider([FromBody] CylinderProviderRequest cylinderProviderRequest)
    {
        var factory = new CylinderProviderFactory();
        var cylinderProvider = factory.CreateCylinder();
        mapper.Map<CylinderProviderRequest, CylinderProvider>(cylinderProviderRequest);
        await cylinderProviderRepository.AddCylinderProviderAsync((CylinderProvider)cylinderProvider);
        var cylinderProviderResponse = mapper.Map<CylinderProvider, CylinderProviderResponse>((CylinderProvider)cylinderProvider);
        return StatusCode(201, cylinderProviderResponse);
    }
    
    [HttpGet]
    [ProducesResponseType(200)]
    [CustomAuthorize("Admin", "Tester")]
    public async Task<IActionResult> GetCylinderProviders()
    {
        var cylinderProviders = await cylinderProviderRepository.GetAllAsync();
        var cylinderProvidersResponse = mapper.Map<IEnumerable<CylinderProvider>, IEnumerable<CylinderProviderResponse>>(cylinderProviders);
        return Ok(cylinderProvidersResponse);
    }
    
    [HttpGet("{id:long}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    [CustomAuthorize("Admin", "Tester")]
    public async Task<IActionResult> GetCylinderProviderById(long id)
    {
        var cylinderProvider = await cylinderProviderRepository.GetByIdAsync(id);
        if (cylinderProvider == null) return NotFound();
        var cylinderProviderResponse = mapper.Map<CylinderProvider, CylinderProviderResponse>(cylinderProvider);
        return Ok(cylinderProviderResponse);
    }
    
    [HttpPut("{id:long}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    [ProducesResponseType(409)]
    [CustomAuthorize("Admin", "Tester")]
    public async Task<IActionResult> UpdateCylinderProvider(long id, [FromBody] CylinderProviderRequest cylinderProviderRequest)
    {
        var cylinderProvider = await cylinderProviderRepository.GetByIdAsync(id);
        if (cylinderProvider == null) return NotFound();
        mapper.Map(cylinderProviderRequest, cylinderProvider);
        await cylinderProviderRepository.UpdateCylinderProviderAsync(cylinderProvider);
        var cylinderProviderResponse = mapper.Map<CylinderProvider, CylinderProviderResponse>(cylinderProvider);
        return Ok(cylinderProviderResponse);
    }
    
    [HttpDelete("{id:long}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    [CustomAuthorize("Admin", "Tester")]
    public async Task<IActionResult> DeleteCylinderProvider(long id)
    {
        var cylinderProvider = await cylinderProviderRepository.GetByIdAsync(id);
        if (cylinderProvider == null) return NotFound();
        await cylinderProviderRepository.DeleteCylinderProviderAsync(cylinderProvider);
        return Ok("Cylinder Provider deleted successfully");
    }
}