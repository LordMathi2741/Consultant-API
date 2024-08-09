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
public class InstallerCompanyController(IInstallerCompanyRepository installerCompanyRepository, IMapper mapper) : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(201)]
    [ProducesResponseType(409)]
    [CustomAuthorize("Admin", "Tester", "Default")]
    public async Task<IActionResult> CreateInstallerCompany([FromBody] InstallerCompanyRequest installerCompanyRequest)
    {
        var factory = new InstallerCompanyFactory();
        var installerCompany = factory.CreateCompany();
        mapper.Map(installerCompanyRequest, installerCompany);
        await installerCompanyRepository.AddInstallerCompanyAsync((InstallerCompany)installerCompany);
        var installerCompanyResponse = mapper.Map<InstallerCompany, InstallerCompanyResponse>((InstallerCompany)installerCompany);
        return StatusCode(201, installerCompanyResponse);
    }
    
    [HttpGet]
    [ProducesResponseType(200)]
    [CustomAuthorize("Admin", "Tester")]
    public async Task<IActionResult> GetInstallerCompanies()
    {
        var installerCompanies = await installerCompanyRepository.GetAllAsync();
        var installerCompaniesResponse = mapper.Map<IEnumerable<InstallerCompany>, IEnumerable<InstallerCompanyResponse>>(installerCompanies);
        return Ok(installerCompaniesResponse);
    }
    
    [HttpGet("{id:long}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    [CustomAuthorize("Admin", "Tester")]
    public async Task<IActionResult> GetInstallerCompanyById(long id)
    {
        var installerCompany = await installerCompanyRepository.GetByIdAsync(id);
        if (installerCompany == null) return NotFound();
        var installerCompanyResponse = mapper.Map<InstallerCompany, InstallerCompanyResponse>(installerCompany);
        return Ok(installerCompanyResponse);
    }
    
    [HttpPut("{id:long}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    [ProducesResponseType(409)]
    [CustomAuthorize("Admin", "Tester")]
    public async Task<IActionResult> UpdateInstallerCompany(long id, [FromBody] InstallerCompanyRequest installerCompanyRequest)
    {
        var installerCompany = await installerCompanyRepository.GetByIdAsync(id);
        if (installerCompany == null) return NotFound();
        mapper.Map<InstallerCompanyRequest, InstallerCompany>(installerCompanyRequest);
        await installerCompanyRepository.UpdateInstallerCompanyAsync(installerCompany);
        var installerCompanyResponse = mapper.Map<InstallerCompany, InstallerCompanyResponse>(installerCompany);
        return Ok(installerCompanyResponse);
    }
    
    [HttpDelete("{id:long}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    [CustomAuthorize("Admin", "Tester")]
    public async Task<IActionResult> DeleteInstallerCompany(long id)
    {
        var installerCompany = await installerCompanyRepository.GetByIdAsync(id);
        if (installerCompany == null) return NotFound();
        await installerCompanyRepository.DeleteInstallerCompanyAsync(installerCompany);
        return Ok("Installer Company deleted successfully");
    }
}