using System.Net.Mime;
using Application.DTO.Request;
using Application.DTO.Response;
using Application.Filters;
using AutoMapper;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Support.Factory.Cylinder;
using Support.Models;

namespace Application.Controllers;



[Route("/api/v1/[controller]")]
[ApiController]
[Produces(MediaTypeNames.Application.Json)]
[ProducesResponseType(400)]
[ProducesResponseType(500)]
[ProducesResponseType(401)]
[ProducesResponseType(403)]
public class CylinderController : ControllerBase
{
    private readonly ICylinderRepository _cylinderRepository;
    private readonly IMapper _mapper;
    private readonly CylinderFactory _factory;
    
    public CylinderController(ICylinderRepository cylinderRepository, IMapper mapper, CylinderOperationCenterFactory cylinderOperationCenterFactory)
    {
        _cylinderRepository = cylinderRepository;
        _mapper = mapper;
        _factory = cylinderOperationCenterFactory;
    }
    
    [HttpPost]
    [ProducesResponseType(201)]
    [ProducesResponseType(409)]
    [CustomAuthorize("Admin", "Tester", "Default")]
    
    public async Task<IActionResult> CreateCylinder([FromBody] CylinderRequest cylinderRequest)
    {
        var cylinder = _factory.CreateCylinder();
        _mapper.Map(cylinderRequest, cylinder);
        await _cylinderRepository.AddCylinderAsync((Cylinder)cylinder);
        var cylinderResponse = _mapper.Map<Cylinder, CylinderResponse >((Cylinder)cylinder);
        return StatusCode(201, cylinderResponse);
    }
    
    [HttpGet]
    [ProducesResponseType(200)]
    [CustomAuthorize("Admin", "Tester")]
    public async Task<IActionResult> GetCylinders()
    {
        var cylinders = await _cylinderRepository.GetAllAsync();
        var cylindersResponse = _mapper.Map<IEnumerable<Cylinder>, IEnumerable<CylinderResponse>>(cylinders);
        return Ok(cylindersResponse);
    }
    
    [HttpGet("{id:long}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    [CustomAuthorize("Admin", "Tester")]
    public async Task<IActionResult> GetCylinderById(long id)
    {
        var cylinder = await _cylinderRepository.GetByIdAsync(id);
        if (cylinder == null) return NotFound();
        var cylinderResponse = _mapper.Map<Cylinder, CylinderResponse>(cylinder);
        return Ok(cylinderResponse);
    }
    
    [HttpPut("{id:long}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    [ProducesResponseType(409)]
    [CustomAuthorize("Admin", "Tester")]
    public async Task<IActionResult> UpdateCylinder(long id, [FromBody] CylinderRequest cylinderRequest)
    {
        var cylinder = await _cylinderRepository.GetByIdAsync(id);
        if (cylinder == null) return NotFound();
        _mapper.Map(cylinderRequest, cylinder);
        await _cylinderRepository.UpdateCylinderAsync(cylinder);
        var cylinderResponse = _mapper.Map<Cylinder, CylinderResponse>(cylinder);
        return Ok(cylinderResponse);
    }
    
    [HttpDelete("{id:long}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    [CustomAuthorize("Admin", "Tester")]
    public async Task<IActionResult> DeleteCylinder(long id)
    {
        var cylinder = await _cylinderRepository.GetByIdAsync(id);
        if (cylinder == null) return NotFound();
        await _cylinderRepository.DeleteCylinderAsync(cylinder);
        return Ok("Cylinder deleted successfully.");
    }
}