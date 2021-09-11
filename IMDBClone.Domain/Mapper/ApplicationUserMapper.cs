using System;
using AutoMapper;
using IMDBClone.Data.Entities;
using IMDBClone.Domain.DTO.User;
using Microsoft.AspNetCore.Identity;

namespace IMDBClone.Domain.Mapper
{
    public class ApplicationUserMapper : Profile
    {
        public ApplicationUserMapper()
        {

            CreateMap<RegisterDTO, ApplicationUser>()
                .ForMember(destination => destination.FullName,
                    opt => opt.MapFrom(src => src.FullName))
                .ForMember(destination => destination.Email,
                    opt => opt.Condition(src => IsValidEmail(src.Email)))
                .ForMember(destination => destination.UserName,
                    opt => opt.MapFrom(src => src.Email));
        }
        bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
    }
}