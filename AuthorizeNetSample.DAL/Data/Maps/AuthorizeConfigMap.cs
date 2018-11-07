using AuthorizeNetSample.DAL.Data.Entity;
using System.Data.Entity.ModelConfiguration;

namespace AuthorizeNetSample.DAL.Data.Maps {
    public class AuthorizeConfigMap : EntityTypeConfiguration<AuthorizeConfig> {
        public AuthorizeConfigMap() {
            HasKey(x => x.Id);
        }
    }
}
