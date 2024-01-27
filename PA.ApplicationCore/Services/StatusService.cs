using PA.ApplicationCore.Domain;
using PA.ApplicationCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PA.ApplicationCore.Services
{
    public class StatusService : Service<Status>, IStatus
    {
        public StatusService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
