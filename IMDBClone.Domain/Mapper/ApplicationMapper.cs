using AutoMapper;
using IMDBClone.Data.Entities;
using IMDBClone.Domain.DTO;
using IMDBClone.Domain.DTO.User;

namespace IMDBClone.Domain.Mapper
{
    public class ApplicationMapper : Profile
    {
        public ApplicationMapper()
        {
            //Straight way
            CreateMap<BaseEntity, BaseDTO>();
            CreateMap<Actor, ActorDTO>()
                .IncludeBase<BaseEntity, BaseDTO>();
            CreateMap<Movie, MovieDTO>()
                .IncludeBase<BaseEntity, BaseDTO>();
            CreateMap<MovieActor, MovieActorDTO>()
                .IncludeBase<BaseEntity, BaseDTO>();
            CreateMap<ApplicationUser, ApplicationUserDTO>();

            //Reverse way
            CreateMap<BaseDTO, BaseEntity>();
            CreateMap<ActorDTO ,Actor>()
                .IncludeBase<BaseDTO, BaseEntity>();
            CreateMap<MovieDTO, Movie>()
                .IncludeBase<BaseDTO, BaseEntity>();
            CreateMap<MovieActorDTO, MovieActor>()
                .IncludeBase<BaseDTO, BaseEntity>();
            CreateMap<ApplicationUserDTO, ApplicationUser>();
        }
    }
}