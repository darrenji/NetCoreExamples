using System;
using System.Collections.Generic;
using System.Text;
using TotalNetCore.DDDLoan.Web.DomainModel.Ddd;

namespace TotalNetCore.DDDLoan.Tests.Mocks
{
    public class UnitOfWorkMock : IUnitOfWork
    {
        public void CommitChanges()
        {
            //do nothing
        }
    }
}
