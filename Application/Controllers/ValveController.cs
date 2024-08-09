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
public class ValveController(IMapper mapper, IValveRepository valveRepository) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(200)]
    [CustomAuthorize("Admin", "Tester")]
    public async Task<IActionResult> GetValves()
    {
        var valves = await valveRepository.GetAllAsync();
        var valvesResponse = mapper.Map<IEnumerable<Valve>, IEnumerable<ValveResponse>>(valves);
        return Ok(valvesResponse);
    }
    
    [HttpGet("{id:long}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    [CustomAuthorize("Admin", "Tester")]
    public async Task<IActionResult> GetValveById(long id)
    {
        var valve = await valveRepository.GetByIdAsync(id);
        if (valve == null) return NotFound();
        var valveResponse = mapper.Map<Valve, ValveResponse>(valve);
        return Ok(valveResponse);
    }
    
    [HttpPost]
    [ProducesResponseType(201)]
    [ProducesResponseType(409)]
    [CustomAuthorize("Admin", "Tester", "Default")]
    public async Task<IActionResult> CreateValve([FromBody] ValveRequest valveRequest)
    {
        var valve = mapper.Map<ValveRequest, Valve>(valveRequest);
        await valveRepository.AddValveAsync(valve);
        var valveResponse = mapper.Map<Valve, ValveResponse >(valve);
        return StatusCode(201, valveResponse);
    }
    
    [HttpPut("{id:long}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    [ProducesResponseType(409)]
    [CustomAuthorize("Admin", "Tester")]
    public async Task<IActionResult> UpdateValve(long id, [FromBody] ValveRequest valveRequest)
    {
        var valve = await valveRepository.GetByIdAsync(id);
        if (valve == null) return NotFound();
        mapper.Map(valveRequest, valve);
        await valveRepository.UpdateValveAsync(valve);
        var valveResponse = mapper.Map<Valve, ValveResponse>(valve);
        return Ok(valveResponse);
    }
    
    [HttpDelete("{id:long}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    [CustomAuthorize("Admin", "Tester")]
    public async Task<IActionResult> DeleteValve(long id)
    {
        var valve = await valveRepository.GetByIdAsync(id);
        if (valve == null) return NotFound();
        await valveRepository.DeleteValveAsync(valve);
        return Ok("Valve deleted successfully.");
    }
}