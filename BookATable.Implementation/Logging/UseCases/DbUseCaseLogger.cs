using BookATable.Application;
using BookATable.DataAccess;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookATable.Implementation.Logging.UseCases
{
    public class DbUseCaseLogger : IUseCaseLogger
    {
        private Context ctx;

        public DbUseCaseLogger(Context ctx)
        {
            this.ctx = ctx;
        }

        public void Log(Application.DTO.UseCaseLog log)
        {
            Domain.Tables.UseCaseLog newLog = new Domain.Tables.UseCaseLog
            {
                UseCaseName = log.UseCaseName,
                Username = log.Username,
                UseCaseData = JsonConvert.SerializeObject(log.UseCaseData),
                ExecutedAt = DateTime.UtcNow
            };

            ctx.UseCaseLogs.Add(newLog);
            ctx.SaveChanges();
        }
    }
}
