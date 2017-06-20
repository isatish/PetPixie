using System;
using Merial.PetPixie.Core.Services.Contracts;


namespace Merial.PetPixie.iOS.Services
{
	public class ByteService : IByteService
	{
		private Byte[] _byte;

		public Byte[] Byte
		{
			get { return _byte; }
			set { _byte = value; }
		}


		public byte[] GetByte(string path)
		{
			return System.IO.File.ReadAllBytes(path);
		}

		public byte[] GetByte()
		{
			return Byte;
		}

		public void ResetB()
		{
			Byte = null;
		}

		public void SetByte(byte[] file)
		{
			if (file != null)
				Byte = file;
		}

	}
}
