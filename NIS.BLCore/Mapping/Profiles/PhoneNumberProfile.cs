using AutoMapper;
using NIS.BLCore.DTO;
using NIS.BLCore.Models.PhoneNumber;
using NIS.DALCore.Context;

namespace NIS.BLCore.Mapping.Profiles
{
    public class PhoneNumberProfile : Profile
    {
        public PhoneNumberProfile()
        {
            CreateMap<PhoneNumberCreateModel, PhoneNumber>();
            CreateMap<PhoneNumber, PhoneNumberViewModel>();
            CreateMap<PhoneNumber, PhoneNumberDto>();
            CreateMap<PhoneNumber, PhoneNumberCreateModel>();
            CreateMap<PhoneNumberCreateModel, PhoneNumber>(); 
            CreateMap<PhoneNumber, PhoneNumberUpdateModel>();
            CreateMap<PhoneNumberUpdateModel, PhoneNumber>();
            CreateMap<PhoneNumber, PhoneNumberDeleteModel>();
            CreateMap<PhoneNumberDeleteModel, PhoneNumber>();
        }
       
    }
}
