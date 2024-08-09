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
public class CertifierController(ICertifierRepository certifierRepository, IMapper mapper) : ControllerBase
{
    
    [HttpPost]
    [ProducesResponseType(201)]
    [ProducesResponseType(409)]
    [CustomAuthorize("Admin", "Tester", "Default")]
    public async Task<IActionResult> CreateCertifier([FromBody] CertifierRequest certifierRequest)
    {
        var certifier = mapper.Map<CertifierRequest, Certifier>(certifierRequest);
        await certifierRepository.AddCertifierAsync(certifier);
        var certifierResponse = mapper.Map<Certifier, CertifierResponse >(certifier);
        return StatusCode(201, certifierResponse);
    }
    
    [HttpGet]
    [ProducesResponseType(200)]
    [CustomAuthorize("Admin", "Tester")]
    public async Task<IActionResult> GetCertifiers()
    {
        var certifiers = await certifierRepository.GetAllAsync();
        var certifiersResponse = mapper.Map<IEnumerable<Certifier>, IEnumerable<CertifierResponse>>(certifiers);
        return Ok(certifiersResponse);
    }
    
    [HttpGet("{id:long}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    [CustomAuthorize("Admin", "Tester")]
    public async Task<IActionResult> GetCertifierById(long id)
    {
        var certifier = await certifierRepository.GetByIdAsync(id);
        if (certifier == null) return NotFound();
        var certifierResponse = mapper.Map<Certifier, CertifierResponse>(certifier);
        return Ok(certifierResponse);
    }
    
    [HttpPut("{id:long}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    [ProducesResponseType(409)]
    [CustomAuthorize("Admin", "Tester")]
    public async Task<IActionResult> UpdateCertifier(long id, [FromBody] CertifierRequest certifierRequest)
    {
        var certifier = await certifierRepository.GetByIdAsync(id);
        if (certifier == null) return NotFound();
        mapper.Map(certifierRequest, certifier);
        await certifierRepository.UpdateCertifierAsync(certifier);
        var certifierResponse = mapper.Map<Certifier, CertifierResponse>(certifier);
        return Ok(certifierResponse);
    }
    
    [HttpDelete("{id:long}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    [CustomAuthorize("Admin", "Tester")]
    public async Task<IActionResult> DeleteCertifier(long id)
    {
        var certifier = await certifierRepository.GetByIdAsync(id);
        if (certifier == null) return NotFound();
        await certifierRepository.DeleteCertifierAsync(certifier);
        return Ok("Certifier deleted successfully.");
    }
}