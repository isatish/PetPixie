using System;
using System.Threading.Tasks;

namespace Merial.PetPixie.Core.Services.Contracts
{
	public interface IVideoService
	{

		void SetPath(string path);
		Task<string> GetPath(string path);
		string GetPath();
	}
}
