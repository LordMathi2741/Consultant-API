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
public class VehicleController(IMapper mapper, IVehicleRepository vehicleRepository) : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(201)]
    [CustomAuthorize("Admin", "Tester", "Default")]
    public async Task<IActionResult> CreateVehicle([FromBody] VehicleRequest vehicleRequest)
    {
        var vehicle = mapper.Map<VehicleRequest, Vehicle>(vehicleRequest);
        await vehicleRepository.AddVehicleAsync(vehicle);
        var vehicleResponse = mapper.Map<Vehicle, VehicleResponse>(vehicle);
        return StatusCode(201, vehicleResponse);
    }
    
    [HttpGet]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    [CustomAuthorize("Admin", "Tester")]
    public async Task<IActionResult> GetVehicles()
    {
        var vehicles = await vehicleRepository.GetAllAsync();
        var vehiclesResponse = mapper.Map<IEnumerable<Vehicle>, IEnumerable<VehicleResponse>>(vehicles);
        return Ok(vehiclesResponse);
    }
    
    [HttpGet("{id:long}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    [CustomAuthorize("Admin", "Tester")]
    public async Task<IActionResult> GetVehicleById(long id)
    {
        var vehicle = await vehicleRepository.GetByIdAsync(id);
        if (vehicle == null) return NotFound();
        var vehicleResponse = mapper.Map<Vehicle, VehicleResponse>(vehicle);
        return Ok(vehicleResponse);
    }
    
    [HttpPut("{id:long}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    [CustomAuthorize("Admin", "Tester")]
    public async Task<IActionResult> UpdateVehicle(long id, [FromBody] VehicleRequest vehicleRequest)
    {
        var vehicle = await vehicleRepository.GetByIdAsync(id);
        if (vehicle == null) return NotFound();
        mapper.Map(vehicleRequest, vehicle);
        await vehicleRepository.UpdateVehicleAsync(vehicle);
        var vehicleResponse = mapper.Map<Vehicle, VehicleResponse>(vehicle);
        return Ok(vehicleResponse);
    }
    
    [HttpDelete("{id:long}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    [CustomAuthorize("Admin", "Tester")]
    public async Task<IActionResult> DeleteVehicle(long id)
    {
        var vehicle = await vehicleRepository.GetByIdAsync(id);
        if (vehicle == null) return NotFound();
        await vehicleRepository.DeleteVehicle(vehicle);
        return Ok("Vehicle deleted successfully.");
    }
}