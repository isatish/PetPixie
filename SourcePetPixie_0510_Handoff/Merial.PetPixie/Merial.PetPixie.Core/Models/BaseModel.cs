using SQLite.Net.Attributes;

namespace Merial.PetPixie.Core.Model
{
    public class BaseModel
    {
        [PrimaryKey,AutoIncrement]
        public int ID { get; set; }
    }
}
