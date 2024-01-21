using AutoMapper;
using NIS.BLCore.DTO;
using NIS.BLCore.Models.Rating;
using NIS.DALCore.Context;

namespace NIS.BLCore.Mapping.Profiles
{
    public class RatingProfile : Profile
    {
        public RatingProfile()
        {
            CreateMap<RatingCreateModel, Rating>();
            CreateMap<Rating, RatingViewModel>();
            CreateMap<Rating, RatingDto>();
            CreateMap<Rating, RatingCreateModel>();
            CreateMap<RatingCreateModel, Rating>();
            CreateMap<Rating, RatingUpdateModel>();
            CreateMap<RatingUpdateModel, Rating>();
            CreateMap<Rating, RatingDeleteModel>();
            CreateMap<RatingDeleteModel, Rating>();
        }
       
    }
}
