using BookATable.Application;
using BookATable.DataAccess;
using BookATable.Domain.Tables;

namespace BookATable.API.Core
{
    public class DbExceptionHandler : IExceptionLogger
    {
        private readonly Context _aspContext;
        public DbExceptionHandler(Context aspContext)
        {
            _aspContext = aspContext;
        }

        public Guid Log(Exception ex, IApplicationActor actor)
        {
            Guid id = Guid.NewGuid();
            ErrorLog log = new()
            {
                Id = id,
                Message = ex.Message,
                Time = DateTime.UtcNow,
                StrackTrace = ex.StackTrace
            };
            _aspContext.ErrorLogs.Add(log);
            _aspContext.SaveChanges();
            return id;
        }
    }
}
