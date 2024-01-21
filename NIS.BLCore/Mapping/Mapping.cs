using AutoMapper;

namespace NIS.BLCore.Mapping
{
    public static class Mapping<TModel, TDto> where TModel: class where TDto : class
    {
        public static MapperConfiguration Config = new MapperConfiguration(cfg => {
            cfg.CreateMap<TModel, TDto>().ReverseMap();
        });

        public static  IMapper Mapper = Config.CreateMapper();

        public static TDto MapToDto(TModel model)
        {
            return Mapper.Map<TDto>(model);
        }

        public static TModel MapToModel(TDto dto)
        {
            return Mapper.Map<TModel>(dto);
        }

        public static TModel MapUpdatedToModel(TDto dto, TModel model)
        {
            return Mapper.Map(dto, model);
        }
    }
}
