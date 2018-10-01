using AuthorizeNetSample.DAL.Data.Context;

namespace AuthorizeNetSample.Repositories
{
	public class ContextAdapter : IContextAdapter
	{
		private AuthorizeDbContext _context;

		public AuthorizeDbContext GetContext()
		{
			if (_context == null)
				_context = new AuthorizeDbContext();

			return _context;
		}
	}
}
