using Application.DTO.Request;
using AutoMapper;
using Support.Factory.Cylinder;
using Support.Models;

namespace Application.DTO.Mapper;

public class RequestToModel : Profile
{
    public RequestToModel()
    {
        CreateMap<ClientRequest, User>();
        CreateMap<CertifierRequest, Certifier>();
        CreateMap<CylinderRequest, Cylinder>();
        CreateMap<ObservationRequest, Observation>();
        CreateMap<OperationCenterRequest, OperationCenter>();
        CreateMap<OwnerRequest, Owner>();
        CreateMap<ValveRequest, Valve>();
        CreateMap<VehicleRequest, Vehicle>();
    }
}