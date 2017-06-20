using System;
using Merial.PetPixie.Core.Models;

namespace Merial.PetPixie.Core.Services.Contracts
{
    public interface ITwitterService
    {
        event EventHandler<TwitterUserModel> DataResponse;
        void GetTwitterData();
    }
}