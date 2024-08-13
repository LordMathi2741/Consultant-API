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
public class ProviderCompanyController : ControllerBase
{
    private readonly IProviderCompanyRepository _providerCompanyRepository;
    private readonly IMapper _mapper;
    private readonly CompanyFactory _factory;
    public ProviderCompanyController(IProviderCompanyRepository providerCompanyRepository, IMapper mapper, ProviderCompanyFactory providerCompanyFactory)
    {
        _providerCompanyRepository = providerCompanyRepository;
        _mapper = mapper;
        _factory = providerCompanyFactory;
    }
    
    [HttpPost]
    [ProducesResponseType(201)]
    [ProducesResponseType(409)]
    [CustomAuthorize("Admin", "Tester", "Default")]
    public async Task<IActionResult> CreateProviderCompany([FromBody] ProviderCompanyRequest providerCompanyRequest)
    {
        var providerCompany = _factory.CreateCompany();
        _mapper.Map(providerCompanyRequest, providerCompany);
        await _providerCompanyRepository.AddProviderCompanyAsync((ProviderCompany)providerCompany);
        var providerCompanyResponse = _mapper.Map<ProviderCompany, ProviderCompanyResponse>((ProviderCompany)providerCompany);
        return StatusCode(201, providerCompanyResponse);
    }
    
    [HttpGet]
    [ProducesResponseType(200)]
    [CustomAuthorize("Admin", "Tester")]
    public async Task<IActionResult> GetProviderCompanies()
    {
        var providerCompanies = await _providerCompanyRepository.GetAllAsync();
        var providerCompaniesResponse = _mapper.Map<IEnumerable<ProviderCompany>, IEnumerable<ProviderCompanyResponse>>(providerCompanies);
        return Ok(providerCompaniesResponse);
    }
    
    [HttpGet("{id:long}")]
    [ProducesResponseType(404)]
    [ProducesResponseType(200)]
    [CustomAuthorize("Admin", "Tester")]
    public async Task<IActionResult> GetProviderCompanyById(long id)
    {
        var providerCompany = await _providerCompanyRepository.GetByIdAsync(id);
        if (providerCompany == null) return NotFound();
        var providerCompanyResponse = _mapper.Map<ProviderCompany, ProviderCompanyResponse>(providerCompany);
        return Ok(providerCompanyResponse);
    }
    
    [HttpPut("{id:long}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    [ProducesResponseType(409)]
    [CustomAuthorize("Admin", "Tester")]
    public async Task<IActionResult> UpdateProviderCompany(long id, [FromBody] ProviderCompanyRequest providerCompanyRequest)
    {
        var providerCompany = await _providerCompanyRepository.GetByIdAsync(id);
        if (providerCompany == null) return NotFound();
        _mapper.Map<ProviderCompanyRequest, ProviderCompany>(providerCompanyRequest);
        await _providerCompanyRepository.UpdateProviderCompanyAsync(providerCompany);
        var providerCompanyResponse = _mapper.Map<ProviderCompany, ProviderCompanyResponse>(providerCompany);
        return Ok(providerCompanyResponse);
    }
    
    [HttpDelete("{id:long}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    [CustomAuthorize("Admin", "Tester")]
    public async Task<IActionResult> DeleteProviderCompany(long id)
    {
        var providerCompany = await _providerCompanyRepository.GetByIdAsync(id);
        if (providerCompany == null) return NotFound();
        await _providerCompanyRepository.DeleteProviderCompanyAsync(providerCompany);
        return Ok("Provider Company deleted successfully");
    }
}