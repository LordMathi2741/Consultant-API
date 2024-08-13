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
public class InstallerCompanyController : ControllerBase
{
    private readonly IInstallerCompanyRepository _installerCompanyRepository;
    private readonly IMapper _mapper;
    private readonly CompanyFactory _factory;
    
    public InstallerCompanyController(IInstallerCompanyRepository installerCompanyRepository, IMapper mapper, InstallerCompanyFactory installerCompanyFactory)
    {
        _installerCompanyRepository = installerCompanyRepository;
        _mapper = mapper;
        _factory = installerCompanyFactory;
    }
    
    [HttpPost]
    [ProducesResponseType(201)]
    [ProducesResponseType(409)]
    [CustomAuthorize("Admin", "Tester", "Default")]
    public async Task<IActionResult> CreateInstallerCompany([FromBody] InstallerCompanyRequest installerCompanyRequest)
    {
        var installerCompany = _factory.CreateCompany();
        _mapper.Map(installerCompanyRequest, installerCompany);
        await _installerCompanyRepository.AddInstallerCompanyAsync((InstallerCompany)installerCompany);
        var installerCompanyResponse = _mapper.Map<InstallerCompany, InstallerCompanyResponse>((InstallerCompany)installerCompany);
        return StatusCode(201, installerCompanyResponse);
    }
    
    [HttpGet]
    [ProducesResponseType(200)]
    [CustomAuthorize("Admin", "Tester")]
    public async Task<IActionResult> GetInstallerCompanies()
    {
        var installerCompanies = await _installerCompanyRepository.GetAllAsync();
        var installerCompaniesResponse = _mapper.Map<IEnumerable<InstallerCompany>, IEnumerable<InstallerCompanyResponse>>(installerCompanies);
        return Ok(installerCompaniesResponse);
    }
    
    [HttpGet("{id:long}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    [CustomAuthorize("Admin", "Tester")]
    public async Task<IActionResult> GetInstallerCompanyById(long id)
    {
        var installerCompany = await _installerCompanyRepository.GetByIdAsync(id);
        if (installerCompany == null) return NotFound();
        var installerCompanyResponse = _mapper.Map<InstallerCompany, InstallerCompanyResponse>(installerCompany);
        return Ok(installerCompanyResponse);
    }
    
    [HttpPut("{id:long}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    [ProducesResponseType(409)]
    [CustomAuthorize("Admin", "Tester")]
    public async Task<IActionResult> UpdateInstallerCompany(long id, [FromBody] InstallerCompanyRequest installerCompanyRequest)
    {
        var installerCompany = await _installerCompanyRepository.GetByIdAsync(id);
        if (installerCompany == null) return NotFound();
        _mapper.Map<InstallerCompanyRequest, InstallerCompany>(installerCompanyRequest);
        await _installerCompanyRepository.UpdateInstallerCompanyAsync(installerCompany);
        var installerCompanyResponse = _mapper.Map<InstallerCompany, InstallerCompanyResponse>(installerCompany);
        return Ok(installerCompanyResponse);
    }
    
    [HttpDelete("{id:long}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    [CustomAuthorize("Admin", "Tester")]
    public async Task<IActionResult> DeleteInstallerCompany(long id)
    {
        var installerCompany = await _installerCompanyRepository.GetByIdAsync(id);
        if (installerCompany == null) return NotFound();
        await _installerCompanyRepository.DeleteInstallerCompanyAsync(installerCompany);
        return Ok("Installer Company deleted successfully");
    }
}