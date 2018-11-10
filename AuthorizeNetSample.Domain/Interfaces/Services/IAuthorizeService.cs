using AuthorizeNetSample.Domain.Interfaces.Services.Base;
using AuthorizeNetSample.Domain.Models.Dtos;

namespace AuthorizeNetSample.Domain.Interfaces.Services {
    public interface IAuthorizeService : IService {
        AuthorizeConfigDto GetConfig();
    }
}
