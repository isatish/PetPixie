using System;
using Merial.PetPixie.Core.Models;

namespace Merial.PetPixie.Core.Services.Contracts
{
    public interface IFacebookService
    {
        event EventHandler<FacebookUserModel> DataResponse;
        void GetFacebookData();
    }
}