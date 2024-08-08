using Application.DTO.Response;
using AutoMapper;
using Support.Models;
using Client = Support.Models.Client;

namespace Application.DTO.Mapper;

public class ModelToResponse : Profile
{
    public ModelToResponse()
    {
        CreateMap<Client, ClientResponse>();
        CreateMap<Certifier, CertifierResponse>();
        CreateMap<Cylinder,CylinderResponse>();
        CreateMap<Observation, ObservationResponse>();
        CreateMap<OperationCenter, OperationCenterResponse>();
        CreateMap<Owner,OwnerResponse>();
        CreateMap<Valve, ValveResponse>();
        CreateMap<VehicleResponse, VehicleResponse>();
    }
}