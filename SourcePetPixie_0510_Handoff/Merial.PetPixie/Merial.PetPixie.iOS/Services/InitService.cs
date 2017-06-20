using System.Threading.Tasks;
using Merial.PetPixie.Core.Services.Contracts;
using PCLStorage;
using System;

namespace Merial.PetPixie.iOS.Services
{
    public class InitService : IInitService
    {
        public async Task Init()
        {
			var RootFolder = await FileSystem.Current.GetFolderFromPathAsync(Environment.GetFolderPath(Environment.SpecialFolder.Personal));

            // R�cup�ration du folder merial.
            var ProjectFolder = await RootFolder.CreateFolderAsync("merial", CreationCollisionOption.OpenIfExists);

            // Cr�ation du dossier Pet+Pixie.
            await ProjectFolder.CreateFolderAsync("PetPixie", CreationCollisionOption.OpenIfExists);
        }
    }
}