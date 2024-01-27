using PA.ApplicationCore.Domain;
using PA.ApplicationCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PA.ApplicationCore.Services
{
    public class TypeService : Service<Types>, ITypes
    {
        public TypeService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
