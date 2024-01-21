using AutoMapper;
using NIS.BLCore.DTO;
using NIS.BLCore.Models.Subscriber;
using NIS.DALCore.Context;

namespace NIS.BLCore.Mapping.Profiles
{
    public class SubscriberProfile : Profile
    {
        public SubscriberProfile()
        {
            CreateMap<SubscriberCreateModel, Subscriber>();
            CreateMap<Subscriber, SubscriberViewModel>();
            CreateMap<Subscriber, SubscriberDto>();
            CreateMap<Subscriber, SubscriberCreateModel>();
            CreateMap<SubscriberCreateModel, Subscriber>();
            CreateMap<Subscriber, SubscriberUpdateModel>();
            CreateMap<SubscriberUpdateModel, Subscriber>();
            CreateMap<Subscriber, SubscriberDeleteModel>();
            CreateMap<SubscriberDeleteModel, Subscriber>();
        }
       
    }
}
