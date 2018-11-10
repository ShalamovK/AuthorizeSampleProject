using AuthorizeNetSample.BLL.Services.Base;
using AuthorizeNetSample.DAL.Data.Entity;
using AuthorizeNetSample.Domain.Interfaces.Services;
using AuthorizeNetSample.Domain.Interfaces.Services.Base;
using AuthorizeNetSample.Domain.Models.Dtos;
using AuthorizeNetSample.Repositories;
using AutoMapper;

namespace AuthorizeNetSample.BLL.Services {
    public class AuthorizeService : BaseService, IAuthorizeService {
        public AuthorizeService(IServiceHost serviceHost, IUnitOfWork unitOfWork)
            : base(serviceHost, unitOfWork) { }

        public AuthorizeConfigDto GetConfig() {
            AuthorizeConfig config = _unitOfWork.AuthorizeConfig.Get();
            return Mapper.Map<AuthorizeConfigDto>(config);
        }
    }
}
