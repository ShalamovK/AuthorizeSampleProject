using AuthorizeNetSample.DAL.Data.Entity;

namespace AuthorizeNetSample.Repositories.Config {
    public interface IAuthorizeConfigRepository {
        AuthorizeConfig Get();
        void Update(AuthorizeConfig config);
    }
}
