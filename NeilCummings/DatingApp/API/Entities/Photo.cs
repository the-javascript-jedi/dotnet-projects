using System.ComponentModel.DataAnnotations.Schema;
// add migrations
// > dotnet ef migrations add ExtendedUserEntity
// remove migration
// > dotnet ef migrations remove
//  update db
// > dotnet ef database update
namespace API.Entities
{
    // for custom table name
    [Table("Photos")]
    public class Photo
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public bool IsMain { get; set; }
        public string PublicId { get; set; }
        // relationship to a user so that when entity is created it is not null
        public int AppUserId { get; set; }
        public AppUser AppUser { get; set; }
    }
}