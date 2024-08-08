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
public class ObservationController(IObservationRepository observationRepository, IMapper mapper) : ControllerBase
{
    
    [HttpPost]
    [ProducesResponseType(201)]
    [CustomAuthorize("Admin", "Tester", "Default")]
    public async Task<IActionResult> CreateObservation([FromBody] ObservationRequest observationRequest)
    {
        var observation = mapper.Map<ObservationRequest, Observation>(observationRequest);
        await observationRepository.AddObservationAsync(observation);
        var observationResponse = mapper.Map<Observation, ObservationResponse >(observation);
        return StatusCode(201, observationResponse);
    }
    
    [HttpGet]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    [CustomAuthorize("Admin", "Tester")]
    public async Task<IActionResult> GetObservations()
    {
        var observations = await observationRepository.GetAllAsync();
        var observationsResponse = mapper.Map<IEnumerable<Observation>, IEnumerable<ObservationResponse>>(observations);
        return Ok(observationsResponse);
    }

    [HttpGet("{id:long}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    [CustomAuthorize("Admin", "Tester")]
    public async Task<IActionResult> GetObservationById(long id)
    {
        var observation = await observationRepository.GetByIdAsync(id);
        if (observation == null) return NotFound();
        var observationResponse = mapper.Map<Observation, ObservationResponse>(observation);
        return Ok(observationResponse);
    }

    
}