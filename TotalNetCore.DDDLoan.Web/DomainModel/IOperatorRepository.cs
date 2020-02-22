using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TotalNetCore.DDDLoan.Web.DomainModel
{
    public interface IOperatorRepository
    {
        void Add(Operator @operator);
        Operator WithLogin(string login);

    }
}
