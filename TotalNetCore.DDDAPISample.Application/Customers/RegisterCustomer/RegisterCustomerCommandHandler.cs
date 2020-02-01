using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TotalNetCore.DDDAPISample.Domain.Customers;
using TotalNetCore.DDDAPISample.Domain.Interfaces;

namespace TotalNetCore.DDDAPISample.Application.Customers.RegisterCustomer
{
    public class RegisterCustomerCommandHandler : IRequestHandler<RegisterCustomerCommand, CustomerDto>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly ICustomerUniquenessChecker _customerUniquenessChecker;
        private readonly IUnitOfWork _unitOfWork;

        public RegisterCustomerCommandHandler(ICustomerRepository customerRepository,
            ICustomerUniquenessChecker customerUniquenessChecker,
            IUnitOfWork unitOfWork)
        {
            this._customerRepository = customerRepository;
            _customerUniquenessChecker = customerUniquenessChecker;
            _unitOfWork = unitOfWork;
        }

        public async Task<CustomerDto> Handle(RegisterCustomerCommand request, CancellationToken cancellationToken)
        {
            var customer = Customer.CreateRegistered(request.Email, request.Name, this._customerUniquenessChecker);

            await this._customerRepository.AddAsync(customer);
            await this._unitOfWork.CommitAsync(cancellationToken);//工作者单元把领域的事件发布出去，并且把Domain INotification Object持久化到数据库

            return new CustomerDto { Id = customer.Id.Value };
        }
    }
}
