using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Application.DTO.Request;
using Application.DTO.Response;
using Application.Filters;
using AutoMapper;
using Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Client = Support.Models.Client;

namespace Application.Controllers
{
    [Route("/api/v1/[controller]")]
    [ApiController]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    [ProducesResponseType(401)]
    [ProducesResponseType(403)]
    public class ClientController(IClientRepository clientRepository, IMapper mapper) : ControllerBase
    {
        [HttpPost("sign-up")]
        [AllowAnonymous]
        [ProducesResponseType(201)]
        public async Task<IActionResult> SignUp([FromBody] ClientRequest clientRequest)
        {
           var client = mapper.Map<ClientRequest, Client>(clientRequest);
           await clientRepository.SignUp(client);
           var clientResponse = mapper.Map<Client, ClientResponse >(client);
           return StatusCode(201, clientResponse);
        }
        
        [HttpGet("sign-in/{email}/{password}")]
        [AllowAnonymous]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> SignIn( string email,  string password)
        {
            var token = await clientRepository.SignIn(email, password);
            Response.Cookies.Append("Token", token , 
                new CookieOptions
                {
                    HttpOnly = true, 
                    SameSite = SameSiteMode.Strict, 
                    Secure = true
                });
            return Ok(token);
        }
        
        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [CustomAuthorize("Tester" , "Admin")]
        public async Task<IActionResult> GetClients()
        {
            var clients = await clientRepository.GetAllAsync();
            var clientsResponse = mapper.Map<IEnumerable<Client>, IEnumerable<ClientResponse>>(clients);
            return Ok(clientsResponse);

        }
        
        [HttpGet("{id:long}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [CustomAuthorize("Tester" , "Admin")]
        public async Task<IActionResult> GetClientById( long id)
        {
            var client = await clientRepository.GetByIdAsync(id);
            if (client == null)
                return NotFound();
            var clientResponse = mapper.Map<Client, ClientResponse>(client);
            return Ok(clientResponse);
        }
        
        [HttpPut("{id:long}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [CustomAuthorize("Tester" , "Admin")]
        public async Task<IActionResult> UpdateClient( long id, [FromBody] ClientRequest clientRequest)
        {
            var client = await clientRepository.GetByIdAsync(id);
            if (client == null) return NotFound();
            mapper.Map(clientRequest, client);
            await clientRepository.UpdateClient(client);
            var clientResponse = mapper.Map<Client, ClientResponse>(client);
            return Ok(clientResponse);
        }
        
        [HttpPut("change-role/{id:long}/{role}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [CustomAuthorize("Tester" , "Admin")]
        public async Task<IActionResult> UpdateClientRole(long id, [FromRoute] string role)
        {
            var client = await clientRepository.GetByIdAsync(id);
            if (client == null) return NotFound();
            await clientRepository.UpdateClientRole(client, role);
            var clientResponse = mapper.Map<Client, ClientResponse>(client);
            return Ok(clientResponse);
        }

        [HttpDelete("{id:long}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [CustomAuthorize("Tester" , "Admin")]
        public async Task<IActionResult> DeleteClient([FromRoute] long id)
        {
            var client = await clientRepository.GetByIdAsync(id);
            if (client == null) return NotFound();
            await clientRepository.DeleteClient(client);
            return StatusCode(200, "Client deleted successfully");
        }
    }
}