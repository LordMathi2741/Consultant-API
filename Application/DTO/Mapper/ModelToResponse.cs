using Application.DTO.Response;
using AutoMapper;
using Support.Factory.Company;
using Support.Factory.Cylinder;
using Support.Models;

namespace Application.DTO.Mapper;

public class ModelToResponse : Profile
{
    public ModelToResponse()
    {
        CreateMap<User, ClientResponse>();
        CreateMap<Certifier, CertifierResponse>();
        CreateMap<Cylinder,CylinderResponse>();
        CreateMap<Observation, ObservationResponse>();
        CreateMap<OperationCenter, OperationCenterResponse>();
        CreateMap<Owner,OwnerResponse>();
        CreateMap<Valve, ValveResponse>();
        CreateMap<Vehicle, VehicleResponse>();
        
        CreateMap<CylinderProvider,CylinderProviderResponse>();
        CreateMap<InstallerCompany, InstallerCompanyResponse>();
        CreateMap<ProviderCompany,ProviderCompanyResponse>();
        CreateMap<WorkShopCompany, WorkShopCompanyResponse>();
        CreateMap<WorkShopCylinder, WorkShopCylinderResponse>();
        CreateMap<WorkShop, WorkShopResponse>();
    }
}