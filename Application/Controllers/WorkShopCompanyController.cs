using System.Net.Mime;
using Application.DTO.Request;
using Application.DTO.Response;
using Application.Filters;
using AutoMapper;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Support.Factory.Company;

namespace Application.Controllers;

[Route("/api/v1/[controller]")]
[ApiController]
[Produces(MediaTypeNames.Application.Json)]
[ProducesResponseType(400)]
[ProducesResponseType(500)]
[ProducesResponseType(401)]
[ProducesResponseType(403)]
public class WorkShopCompanyController(IWorkShopCompanyRepository workShopCompanyRepository, IMapper mapper) : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(201)]
    [ProducesResponseType(409)]
    [CustomAuthorize("Admin", "Tester", "Default")]
    public async Task<IActionResult> CreateWorkShopCompany([FromBody] WorkShopCompanyRequest workShopCompanyRequest)
    {
        var factory = new WorkShopCompanyFactory();
        var workShopCompany = factory.CreateCompany();
        mapper.Map(workShopCompanyRequest, workShopCompany);
        await workShopCompanyRepository.AddWorkShopCompanyAsync((WorkShopCompany)workShopCompany);
        var workShopCompanyResponse = mapper.Map<WorkShopCompany, WorkShopCompanyResponse>((WorkShopCompany)workShopCompany);
        return StatusCode(201, workShopCompanyResponse);
    }
    
    [HttpGet]
    [ProducesResponseType(200)]
    [CustomAuthorize("Admin", "Tester")]
    public async Task<IActionResult> GetWorkShopCompanies()
    {
        var workShopCompanies = await workShopCompanyRepository.GetAllAsync();
        var workShopCompaniesResponse = mapper.Map<IEnumerable<WorkShopCompany>, IEnumerable<WorkShopCompanyResponse>>(workShopCompanies);
        return Ok(workShopCompaniesResponse);
    }
    
    [HttpGet("{id:long}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    [CustomAuthorize("Admin", "Tester")]
    public async Task<IActionResult> GetWorkShopCompanyById(long id)
    {
        var workShopCompany = await workShopCompanyRepository.GetByIdAsync(id);
        if (workShopCompany == null) return NotFound();
        var workShopCompanyResponse = mapper.Map<WorkShopCompany, WorkShopCompanyResponse>(workShopCompany);
        return Ok(workShopCompanyResponse);
    }
    
    [HttpPut("{id:long}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    [ProducesResponseType(409)]
    [CustomAuthorize("Admin", "Tester")]
    public async Task<IActionResult> UpdateWorkShopCompany(long id, [FromBody] WorkShopCompanyRequest workShopCompanyRequest)
    {
        var workShopCompany = await workShopCompanyRepository.GetByIdAsync(id);
        if (workShopCompany == null) return NotFound();
        mapper.Map<WorkShopCompanyRequest, WorkShopCompany>(workShopCompanyRequest);
        await workShopCompanyRepository.UpdateWorkShopCompanyAsync(workShopCompany);
        var workShopCompanyResponse = mapper.Map<WorkShopCompany, WorkShopCompanyResponse>(workShopCompany);
        return Ok(workShopCompanyResponse);
    }
    
    [HttpDelete("{id:long}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    [CustomAuthorize("Admin", "Tester")]
    public async Task<IActionResult> DeleteWorkShopCompany(long id)
    {
        var workShopCompany = await workShopCompanyRepository.GetByIdAsync(id);
        if (workShopCompany == null) return NotFound();
        await workShopCompanyRepository.DeleteWorkShopCompanyAsync(workShopCompany);
        return Ok("WorkShopCompany deleted successfully");
    }
}