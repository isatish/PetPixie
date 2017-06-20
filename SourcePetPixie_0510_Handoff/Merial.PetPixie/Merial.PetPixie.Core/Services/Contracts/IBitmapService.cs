using Splat;

namespace Merial.PetPixie.Core.Services.Contracts
{
    public interface IBitmapService
    {
        void SetBitmap(IBitmap bitmap);
        void Reset();
        IBitmap GetBitmap();
        IBitmap GetBitmap(string path);
    }
}