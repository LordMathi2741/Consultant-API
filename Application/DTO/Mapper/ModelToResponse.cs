using Application.DTO.Response;
using AutoMapper;
using Client = Support.Models.Client;

namespace Application.DTO.Mapper;

public class ModelToResponse : Profile
{
    public ModelToResponse()
    {
        CreateMap<Client, ClientResponse>();
    }
}