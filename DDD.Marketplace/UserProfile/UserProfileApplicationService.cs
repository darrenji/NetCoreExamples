using DDD.Marketplace.Adapter;
using DDD.Marketplace.Domain.Shared;
using DDD.Marketplace.Domain.UserProfile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static DDD.Marketplace.UserProfile.Contracts;

namespace DDD.Marketplace.UserProfile
{
    public class UserProfileApplicationService : IApplicationService
    {
        private readonly IUserProfileRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly CheckTextForProfanity _checkText;

        public UserProfileApplicationService(IUserProfileRepository repository,
            IUnitOfWork unitOfWork,
            CheckTextForProfanity checkText)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _checkText = checkText;
        }
        public Task Handle(object command)
        {
            throw new NotImplementedException();
        }

        private async Task HandleCreate(V1.RegisterUser cmd)
        {
            if(await _repository.Exists(new Domain.UserId(cmd.UserId)))
            {
                throw new InvalidOperationException($"Entity with id {cmd.UserId} already existes");
            }

            var userProfile = new Domain.UserProfile.UserProfile(new Domain.UserId(cmd.UserId),FullName.FromString(cmd.FullName),DisplayName.FromString(cmd.DisplayName, _checkText));

            await _repository.Add(userProfile);
            await _unitOfWork.Commit();
        }

        private async Task HandleUpdate(Guid id, Action<Domain.UserProfile.UserProfile> operation)
        {
            var userProfile = await _repository.Load(new Domain.UserId(id));
            if(userProfile==null)
                throw new InvalidOperationException(
                    $"Entity with id {id} cannot be found"
                );

            operation(userProfile);
            await _unitOfWork.Commit();
        }
    }
}
