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
public class WorkShopController(IWorkShopRepository workShopRepository, IMapper mapper) : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(201)]
    [ProducesResponseType(409)]
    [CustomAuthorize("Admin", "Tester", "Default")]
    public async Task<IActionResult> CreateWorkShop([FromBody] WorkShopRequest workShopRequest)
    {
        var workShop = mapper.Map<WorkShopRequest, WorkShop>(workShopRequest);
        await workShopRepository.AddWorkShopAsync(workShop);
        var workShopResponse = mapper.Map<WorkShop, WorkShopResponse>(workShop);
        return StatusCode(201, workShopResponse);
    }
    
    [HttpGet]
    [ProducesResponseType(200)]
    [CustomAuthorize("Admin", "Tester")]
    public async Task<IActionResult> GetWorkShops()
    {
        var workShops = await workShopRepository.GetAllAsync();
        var workShopsResponse = mapper.Map<IEnumerable<WorkShop>, IEnumerable<WorkShopResponse>>(workShops);
        return Ok(workShopsResponse);
    }
    
    [HttpGet("{id:long}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    [CustomAuthorize("Admin", "Tester")]
    public async Task<IActionResult> GetWorkShopById(long id)
    {
        var workShop = await workShopRepository.GetByIdAsync(id);
        if (workShop == null) return NotFound();
        var workShopResponse = mapper.Map<WorkShop, WorkShopResponse>(workShop);
        return Ok(workShopResponse);
    }
    
    [HttpPut("{id:long}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    [ProducesResponseType(409)]
    [CustomAuthorize("Admin", "Tester")]
    public async Task<IActionResult> UpdateWorkShop(long id, [FromBody] WorkShopRequest workShopRequest)
    {
        var workShop = await workShopRepository.GetByIdAsync(id);
        if (workShop == null) return NotFound();
        mapper.Map(workShopRequest, workShop);
        await workShopRepository.UpdateWorkShopAsync(workShop);
        var workShopResponse = mapper.Map<WorkShop, WorkShopResponse>(workShop);
        return Ok(workShopResponse);
    }
    
    [HttpDelete("{id:long}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    [CustomAuthorize("Admin", "Tester")]
    public async Task<IActionResult> DeleteWorkShop(long id)
    {
        var workShop = await workShopRepository.GetByIdAsync(id);
        if (workShop == null) return NotFound();
        await workShopRepository.DeleteWorkShopAsync(workShop);
        return Ok("WorkShop deleted successfully");
    }
}