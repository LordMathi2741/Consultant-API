using Application.DTO.Response;
using AutoMapper;
using Infrastructure.Models;

namespace Application.DTO.Mapper;

public class ModelToResponse : Profile
{
    public ModelToResponse()
    {
        CreateMap<Client, ClientResponse>();
    }
}