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
public class OperationCenterController( IOperationCenterRepository operationCenterRepository, IMapper mapper) : ControllerBase
{
    
        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [CustomAuthorize("Admin", "Tester")]
        public async Task<IActionResult> GetOperationCenters()
        {
            var operationCenters = await operationCenterRepository.GetAllAsync();
            var operationCentersResponse = mapper.Map<IEnumerable<OperationCenter>, IEnumerable<OperationCenterResponse>>(operationCenters);
            return Ok(operationCentersResponse);
        }
    
        [HttpGet("{id:long}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [CustomAuthorize("Admin", "Tester")]
        public async Task<IActionResult> GetOperationCenterById(long id)
        {
            var operationCenter = await operationCenterRepository.GetByIdAsync(id);
            if (operationCenter == null) return NotFound();
            var operationCenterResponse = mapper.Map<OperationCenter, OperationCenterResponse>(operationCenter);
            return Ok(operationCenterResponse);
        }
        
        [HttpPost]
        [ProducesResponseType(200)]
        [CustomAuthorize("Admin", "Tester", "Default")]
        public async Task<IActionResult> CreateOperationCenter([FromBody] OperationCenterRequest operationCenterRequest)
        {
            var operationCenter = mapper.Map<OperationCenterRequest, OperationCenter>(operationCenterRequest);
            await operationCenterRepository.AddOperationCenterAsync(operationCenter);
            var operationCenterResponse = mapper.Map<OperationCenter, OperationCenterResponse >(operationCenter);
            return StatusCode(201, operationCenterResponse);
        }
        
        [HttpPut("{id:long}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [CustomAuthorize("Admin", "Tester")]
        public async Task<IActionResult> UpdateOperationCenter(long id, [FromBody] OperationCenterRequest operationCenterRequest)
        {
            var operationCenter = await operationCenterRepository.GetByIdAsync(id);
            if (operationCenter == null) return NotFound();
            operationCenter = mapper.Map(operationCenterRequest, operationCenter);
            await operationCenterRepository.UpdateOperationCenterAsync(operationCenter);
            var operationCenterResponse = mapper.Map<OperationCenter, OperationCenterResponse >(operationCenter);
            return Ok(operationCenterResponse);
        }
        
        [HttpDelete("{id:long}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [CustomAuthorize("Admin", "Tester")]
        public async Task<IActionResult> DeleteOperationCenter(long id)
        {
            var operationCenter = await operationCenterRepository.GetByIdAsync(id);
            if (operationCenter == null) return NotFound();
            await operationCenterRepository.DeleteOperationCenterAsync(operationCenter);
            return Ok("OperationCenter deleted successfully.");
        }
}