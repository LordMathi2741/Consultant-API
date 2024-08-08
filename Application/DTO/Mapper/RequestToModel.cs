using Application.DTO.Request;
using AutoMapper;
using Infrastructure.Models;

namespace Application.DTO.Mapper;

public class RequestToModel : Profile
{
    public RequestToModel()
    {
        CreateMap<ClientRequest, Client>();
    }
}