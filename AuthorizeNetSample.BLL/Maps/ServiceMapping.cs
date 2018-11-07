using AuthorizeNetSample.DAL.Data.Entity;
using AuthorizeNetSample.Domain.Models.Dtos;
using AutoMapper;

namespace AuthorizeNetSample.BLL.Maps {
    public class ServiceMapping : Profile {
        public ServiceMapping() {
            CreateMap<AuthorizeConfig, AuthorizeConfigDto>();
        }
    }
}
