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
public class WorkShopCylinderController(IWorkShopCylinderRepository workShopCylinderRepository,IMapper mapper) : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(201)]
    [ProducesResponseType(409)]
    [CustomAuthorize("Admin", "Tester", "Default")]
    public async Task<IActionResult> CreateWorkShopCylinder([FromBody] WorkShopCylinderRequest workShopCylinderRequest)
    {
        var factory = new WorkShopCylinderFactory();
        var workShopCylinder = factory.CreateCylinder();
        mapper.Map(workShopCylinderRequest, workShopCylinder);
        await workShopCylinderRepository.AddWorkShopCylinderAsync((WorkShopCylinder)workShopCylinder);
        var workShopCylinderResponse = mapper.Map<WorkShopCylinder, WorkShopCylinderResponse>((WorkShopCylinder)workShopCylinder);
        return StatusCode(201, workShopCylinderResponse);
    }
    
    [HttpGet]
    [ProducesResponseType(200)]
    [CustomAuthorize("Admin", "Tester")]
    public async Task<IActionResult> GetWorkShopCylinders()
    {
        var workShopCylinders = await workShopCylinderRepository.GetAllAsync();
        var workShopCylindersResponse = mapper.Map<IEnumerable<WorkShopCylinder>, IEnumerable<WorkShopCylinderResponse>>(workShopCylinders);
        return Ok(workShopCylindersResponse);
    }
    
    [HttpGet("{id:long}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    [CustomAuthorize("Admin", "Tester")]
    public async Task<IActionResult> GetWorkShopCylinderById(long id)
    {
        var workShopCylinder = await workShopCylinderRepository.GetByIdAsync(id);
        if (workShopCylinder == null) return NotFound();
        var workShopCylinderResponse = mapper.Map<WorkShopCylinder, WorkShopCylinderResponse>(workShopCylinder);
        return Ok(workShopCylinderResponse);
    }
    
    [HttpPut("{id:long}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    [ProducesResponseType(409)]
    [CustomAuthorize("Admin", "Tester")]
    public async Task<IActionResult> UpdateWorkShopCylinder(long id, [FromBody] WorkShopCylinderRequest workShopCylinderRequest)
    {
        var workShopCylinder = await workShopCylinderRepository.GetByIdAsync(id);
        if (workShopCylinder == null) return NotFound();
        mapper.Map<WorkShopCylinderRequest, WorkShopCylinder>(workShopCylinderRequest);
        await workShopCylinderRepository.UpdateWorkShopCylinderAsync(workShopCylinder);
        var workShopCylinderResponse = mapper.Map<WorkShopCylinder, WorkShopCylinderResponse>(workShopCylinder);
        return Ok(workShopCylinderResponse);
    }
    
    [HttpDelete("{id:long}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    [CustomAuthorize("Admin", "Tester")]
    public async Task<IActionResult> DeleteWorkShopCylinder(long id)
    {
        var workShopCylinder = await workShopCylinderRepository.GetByIdAsync(id);
        if (workShopCylinder == null) return NotFound();
        await workShopCylinderRepository.DeleteWorkShopCylinderAsync(workShopCylinder);
        return Ok("WorkShopCylinder deleted successfully");
    }
}