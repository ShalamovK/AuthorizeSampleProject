using AuthorizeNetSample.BLL.Services.Base;
using AuthorizeNetSample.DAL.Data.Entity;
using AuthorizeNetSample.Domain.Interfaces.Services;
using AuthorizeNetSample.Domain.Models.Dtos;
using AuthorizeNetSample.Repositories;
using AutoMapper;

namespace AuthorizeNetSample.BLL.Services {
    public class AuthorizeService : BaseService, IAuthorizeService {
        public AuthorizeService(IUnitOfWork unitOfWork)
            : base(unitOfWork) { }

        public AuthorizeConfigDto GetConfig() {
            AuthorizeConfig config = _UnitOfWork.AuthorizeConfig.Get();
            return Mapper.Map<AuthorizeConfigDto>(config);
        }
    }
}
