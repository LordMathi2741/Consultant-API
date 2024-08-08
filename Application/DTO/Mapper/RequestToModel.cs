using Application.DTO.Request;
using AutoMapper;
using Client = Support.Models.Client;

namespace Application.DTO.Mapper;

public class RequestToModel : Profile
{
    public RequestToModel()
    {
        CreateMap<ClientRequest, Client>();
    }
}