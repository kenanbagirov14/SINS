using System;
using System.Linq;
using System.Reflection;
using AutoMapper;

namespace NIS.BLCore.Mapping
{
    public static class AutoMapperConfig
    {
        public static IMapper CreateMapper()
        {
            var profileType = typeof(Profile);
            var profiles = Assembly.GetExecutingAssembly().GetTypes()
                .Where(q => profileType.IsAssignableFrom(q)
                            && q.GetConstructor(Type.EmptyTypes) != null)
                .Select(Activator.CreateInstance)
                .Cast<Profile>();
            return new MapperConfiguration(l =>
            {
                foreach (var profile in profiles)
                {
                    l.AddProfile(profile);
                }
            }).CreateMapper();
        }
    }
}
