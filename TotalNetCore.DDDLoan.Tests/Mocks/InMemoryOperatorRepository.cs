﻿using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TotalNetCore.DDDLoan.Web.DomainModel;

namespace TotalNetCore.DDDLoan.Tests.Mocks
{
    public class InMemoryOperatorRepository : IOperatorRepository
    {
        private readonly ConcurrentDictionary<OperatorId, Operator> operators = new ConcurrentDictionary<OperatorId, Operator>();

        public InMemoryOperatorRepository(IEnumerable<Operator> initialData)
        {
            foreach(var @operator in initialData)
            {
                operators[@operator.Id] = @operator;
            }
        }

        public void Add(Operator @operator)
        {
            operators[@operator.Id] = @operator;
        }

        public Operator WithLogin(string login)
        {
            return operators.Values.FirstOrDefault(t => t.Login == login);
        }
    }
}
