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
public class WorkShopCylinderController : ControllerBase
{
    private readonly IWorkShopCylinderRepository _workShopCylinderRepository;
    private readonly IMapper _mapper;
    private readonly CylinderFactory _factory;
    public WorkShopCylinderController(IWorkShopCylinderRepository workShopCylinderRepository, IMapper mapper, WorkShopCylinderFactory workShopCylinderFactory)
    {
        _workShopCylinderRepository = workShopCylinderRepository;
        _mapper = mapper;
        _factory = workShopCylinderFactory;
    }
    [HttpPost]
    [ProducesResponseType(201)]
    [ProducesResponseType(409)]
    [CustomAuthorize("Admin", "Tester", "Default")]
    public async Task<IActionResult> CreateWorkShopCylinder([FromBody] WorkShopCylinderRequest workShopCylinderRequest)
    {
        var workShopCylinder = _factory.CreateCylinder();
        _mapper.Map(workShopCylinderRequest, workShopCylinder);
        await _workShopCylinderRepository.AddWorkShopCylinderAsync((WorkShopCylinder)workShopCylinder);
        var workShopCylinderResponse = _mapper.Map<WorkShopCylinder, WorkShopCylinderResponse>((WorkShopCylinder)workShopCylinder);
        return StatusCode(201, workShopCylinderResponse);
    }
    
    [HttpGet]
    [ProducesResponseType(200)]
    [CustomAuthorize("Admin", "Tester")]
    public async Task<IActionResult> GetWorkShopCylinders()
    {
        var workShopCylinders = await _workShopCylinderRepository.GetAllAsync();
        var workShopCylindersResponse = _mapper.Map<IEnumerable<WorkShopCylinder>, IEnumerable<WorkShopCylinderResponse>>(workShopCylinders);
        return Ok(workShopCylindersResponse);
    }
    
    [HttpGet("{id:long}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    [CustomAuthorize("Admin", "Tester")]
    public async Task<IActionResult> GetWorkShopCylinderById(long id)
    {
        var workShopCylinder = await _workShopCylinderRepository.GetByIdAsync(id);
        if (workShopCylinder == null) return NotFound();
        var workShopCylinderResponse = _mapper.Map<WorkShopCylinder, WorkShopCylinderResponse>(workShopCylinder);
        return Ok(workShopCylinderResponse);
    }
    
    [HttpPut("{id:long}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    [ProducesResponseType(409)]
    [CustomAuthorize("Admin", "Tester")]
    public async Task<IActionResult> UpdateWorkShopCylinder(long id, [FromBody] WorkShopCylinderRequest workShopCylinderRequest)
    {
        var workShopCylinder = await _workShopCylinderRepository.GetByIdAsync(id);
        if (workShopCylinder == null) return NotFound();
        _mapper.Map<WorkShopCylinderRequest, WorkShopCylinder>(workShopCylinderRequest);
        await _workShopCylinderRepository.UpdateWorkShopCylinderAsync(workShopCylinder);
        var workShopCylinderResponse = _mapper.Map<WorkShopCylinder, WorkShopCylinderResponse>(workShopCylinder);
        return Ok(workShopCylinderResponse);
    }
    
    [HttpDelete("{id:long}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    [CustomAuthorize("Admin", "Tester")]
    public async Task<IActionResult> DeleteWorkShopCylinder(long id)
    {
        var workShopCylinder = await _workShopCylinderRepository.GetByIdAsync(id);
        if (workShopCylinder == null) return NotFound();
        await _workShopCylinderRepository.DeleteWorkShopCylinderAsync(workShopCylinder);
        return Ok("WorkShopCylinder deleted successfully");
    }
}