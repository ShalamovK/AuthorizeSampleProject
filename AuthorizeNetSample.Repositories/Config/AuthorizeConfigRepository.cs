using AuthorizeNetSample.DAL.Data.Context;
using AuthorizeNetSample.DAL.Data.Entity;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;

namespace AuthorizeNetSample.Repositories.Config {
    public class AuthorizeConfigRepository : IAuthorizeConfigRepository {
        private readonly AuthorizeDbContext _context;
        public AuthorizeConfigRepository(AuthorizeDbContext context) {
            _context = context;
        }

        public AuthorizeConfig Get() {
            return _context.AuthorizeConfig.FirstOrDefault();
        }

        public void Update(AuthorizeConfig config) {
            DbEntityEntry dbEntityEntry = _context.Entry(config);
            if (dbEntityEntry.State == EntityState.Detached) {
                _context.AuthorizeConfig.Attach(config);
            }
            if (dbEntityEntry.State != EntityState.Modified) {
                dbEntityEntry.State = EntityState.Modified;
            }
        }
    }
}
