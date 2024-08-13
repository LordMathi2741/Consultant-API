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
public class CylinderProviderController : ControllerBase
{
    private readonly ICylinderProviderRepository _cylinderProviderRepository;
    private readonly IMapper _mapper;
    private readonly CylinderFactory _factory;
    public CylinderProviderController(ICylinderProviderRepository cylinderProviderRepository, IMapper mapper, CylinderProviderFactory cylinderProviderFactory)
    {
        _cylinderProviderRepository = cylinderProviderRepository;
        _mapper = mapper;
        _factory = cylinderProviderFactory;
    }
    [HttpPost]
    [ProducesResponseType(201)]
    [ProducesResponseType(409)]
    [CustomAuthorize("Admin", "Tester", "Default")]
    public async Task<IActionResult> CreateCylinderProvider([FromBody] CylinderProviderRequest cylinderProviderRequest)
    {
        var cylinderProvider = _factory.CreateCylinder();
        _mapper.Map<CylinderProviderRequest, CylinderProvider>(cylinderProviderRequest);
        await _cylinderProviderRepository.AddCylinderProviderAsync((CylinderProvider)cylinderProvider);
        var cylinderProviderResponse = _mapper.Map<CylinderProvider, CylinderProviderResponse>((CylinderProvider)cylinderProvider);
        return StatusCode(201, cylinderProviderResponse);
    }
    
    [HttpGet]
    [ProducesResponseType(200)]
    [CustomAuthorize("Admin", "Tester")]
    public async Task<IActionResult> GetCylinderProviders()
    {
        var cylinderProviders = await _cylinderProviderRepository.GetAllAsync();
        var cylinderProvidersResponse = _mapper.Map<IEnumerable<CylinderProvider>, IEnumerable<CylinderProviderResponse>>(cylinderProviders);
        return Ok(cylinderProvidersResponse);
    }
    
    [HttpGet("{id:long}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    [CustomAuthorize("Admin", "Tester")]
    public async Task<IActionResult> GetCylinderProviderById(long id)
    {
        var cylinderProvider = await _cylinderProviderRepository.GetByIdAsync(id);
        if (cylinderProvider == null) return NotFound();
        var cylinderProviderResponse = _mapper.Map<CylinderProvider, CylinderProviderResponse>(cylinderProvider);
        return Ok(cylinderProviderResponse);
    }
    
    [HttpPut("{id:long}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    [ProducesResponseType(409)]
    [CustomAuthorize("Admin", "Tester")]
    public async Task<IActionResult> UpdateCylinderProvider(long id, [FromBody] CylinderProviderRequest cylinderProviderRequest)
    {
        var cylinderProvider = await _cylinderProviderRepository.GetByIdAsync(id);
        if (cylinderProvider == null) return NotFound();
        _mapper.Map(cylinderProviderRequest, cylinderProvider);
        await _cylinderProviderRepository.UpdateCylinderProviderAsync(cylinderProvider);
        var cylinderProviderResponse = _mapper.Map<CylinderProvider, CylinderProviderResponse>(cylinderProvider);
        return Ok(cylinderProviderResponse);
    }
    
    [HttpDelete("{id:long}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    [CustomAuthorize("Admin", "Tester")]
    public async Task<IActionResult> DeleteCylinderProvider(long id)
    {
        var cylinderProvider = await _cylinderProviderRepository.GetByIdAsync(id);
        if (cylinderProvider == null) return NotFound();
        await _cylinderProviderRepository.DeleteCylinderProviderAsync(cylinderProvider);
        return Ok("Cylinder Provider deleted successfully");
    }
}