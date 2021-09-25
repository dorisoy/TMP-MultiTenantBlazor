using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorTemplates.ServerSide.Data
{
    public abstract class CoreDbBaseOperations
    {
        public async Task<bool> Commit(ApplicationDbContext db, ILogger logger)
        {
            try
            {
                var saved = await db.SaveChangesAsync();

                foreach (var ct in db.ChangeTracker.Entries())
                {
                    ct.State = Microsoft.EntityFrameworkCore.EntityState.Detached;
                }
            }
            catch (Exception ex)
            {
                logger.LogError("error saving db changes");
                Debug.WriteLine($"Db Save Exception: {ex.Message}\n\n Inner Exception: {ex?.InnerException}");
                return false;
            }

            return true;
        }
    }
}
