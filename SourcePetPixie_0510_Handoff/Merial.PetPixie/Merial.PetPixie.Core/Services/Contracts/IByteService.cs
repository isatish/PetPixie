using System;
using System.IO;
namespace Merial.PetPixie.Core.Services.Contracts
{
	public interface IByteService

	{
		
		void SetByte(byte[] file);
		void ResetB();
		byte[] GetByte();
		byte[] GetByte(string path);

	}
}
