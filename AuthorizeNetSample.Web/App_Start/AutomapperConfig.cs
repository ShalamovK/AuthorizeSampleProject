using AuthorizeNetSample.Domain.Models.Dtos;
using AuthorizeNetSample.Web.Models;
using AutoMapper;

namespace AuthorizeNetSample.Web.App_Start {
    public class AutomapperConfig : Profile {
        public AutomapperConfig() {
            CreateMap<AuthorizeConfigDto, GetRequestTokenPageViewModel>().
                ForMember(trg => trg.OAuthUrl, opt => opt.Ignore()).
                ForMember(trg => trg.Scope, opt => opt.Ignore()).
                ForMember(trg => trg.State, opt => opt.Ignore()).
                ForMember(trg => trg.Sub, opt => opt.Ignore());
        }
    }
}