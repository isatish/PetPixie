 // ReSharper disable once CheckNamespace
namespace Merial.PetPixie.Core.Models.Kinvey
{
    public partial class KHashTag
    {
        public string DisplayTagForSearch => $"# {Text} - ({MediaCount})";
    }
}