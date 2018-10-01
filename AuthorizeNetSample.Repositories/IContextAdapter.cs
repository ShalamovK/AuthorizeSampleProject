using AuthorizeNetSample.DAL.Data.Context;

namespace AuthorizeNetSample.Repositories
{
	public interface IContextAdapter
	{
		AuthorizeDbContext GetContext();
	}
}
