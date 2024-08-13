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
public class WorkShopCompanyController : ControllerBase
{
    private readonly IWorkShopCompanyRepository _workShopCompanyRepository;
    private readonly IMapper _mapper;
    private readonly CompanyFactory _factory;
    public WorkShopCompanyController(IWorkShopCompanyRepository workShopCompanyRepository, IMapper mapper, WorkShopCompanyFactory workShopCompanyFactory)
    {
        _workShopCompanyRepository = workShopCompanyRepository;
        _mapper = mapper;
        _factory = workShopCompanyFactory;
    }
    [HttpPost]
    [ProducesResponseType(201)]
    [ProducesResponseType(409)]
    [CustomAuthorize("Admin", "Tester", "Default")]
    public async Task<IActionResult> CreateWorkShopCompany([FromBody] WorkShopCompanyRequest workShopCompanyRequest)
    {
        var workShopCompany = _factory.CreateCompany();
        _mapper.Map(workShopCompanyRequest, workShopCompany);
        await _workShopCompanyRepository.AddWorkShopCompanyAsync((WorkShopCompany)workShopCompany);
        var workShopCompanyResponse = _mapper.Map<WorkShopCompany, WorkShopCompanyResponse>((WorkShopCompany)workShopCompany);
        return StatusCode(201, workShopCompanyResponse);
    }
    
    [HttpGet]
    [ProducesResponseType(200)]
    [CustomAuthorize("Admin", "Tester")]
    public async Task<IActionResult> GetWorkShopCompanies()
    {
        var workShopCompanies = await _workShopCompanyRepository.GetAllAsync();
        var workShopCompaniesResponse = _mapper.Map<IEnumerable<WorkShopCompany>, IEnumerable<WorkShopCompanyResponse>>(workShopCompanies);
        return Ok(workShopCompaniesResponse);
    }
    
    [HttpGet("{id:long}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    [CustomAuthorize("Admin", "Tester")]
    public async Task<IActionResult> GetWorkShopCompanyById(long id)
    {
        var workShopCompany = await _workShopCompanyRepository.GetByIdAsync(id);
        if (workShopCompany == null) return NotFound();
        var workShopCompanyResponse = _mapper.Map<WorkShopCompany, WorkShopCompanyResponse>(workShopCompany);
        return Ok(workShopCompanyResponse);
    }
    
    [HttpPut("{id:long}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    [ProducesResponseType(409)]
    [CustomAuthorize("Admin", "Tester")]
    public async Task<IActionResult> UpdateWorkShopCompany(long id, [FromBody] WorkShopCompanyRequest workShopCompanyRequest)
    {
        var workShopCompany = await _workShopCompanyRepository.GetByIdAsync(id);
        if (workShopCompany == null) return NotFound();
        _mapper.Map<WorkShopCompanyRequest, WorkShopCompany>(workShopCompanyRequest);
        await _workShopCompanyRepository.UpdateWorkShopCompanyAsync(workShopCompany);
        var workShopCompanyResponse = _mapper.Map<WorkShopCompany, WorkShopCompanyResponse>(workShopCompany);
        return Ok(workShopCompanyResponse);
    }
    
    [HttpDelete("{id:long}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    [CustomAuthorize("Admin", "Tester")]
    public async Task<IActionResult> DeleteWorkShopCompany(long id)
    {
        var workShopCompany = await _workShopCompanyRepository.GetByIdAsync(id);
        if (workShopCompany == null) return NotFound();
        await _workShopCompanyRepository.DeleteWorkShopCompanyAsync(workShopCompany);
        return Ok("WorkShopCompany deleted successfully");
    }
}