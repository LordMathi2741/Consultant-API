using Application.DTO.Request;
using AutoMapper;
using Support.Models;
using Client = Support.Models.Client;

namespace Application.DTO.Mapper;

public class RequestToModel : Profile
{
    public RequestToModel()
    {
        CreateMap<ClientRequest, Client>();
        CreateMap<CertifierRequest, Certifier>();
        CreateMap<CylinderRequest, Cylinder>();
        CreateMap<ObservationRequest, Observation>();
        CreateMap<OperationCenterRequest, OperationCenter>();
        CreateMap<OwnerRequest, Owner>();
        CreateMap<ValveRequest, Valve>();
        CreateMap<VehicleRequest, Vehicle>();
    }
}