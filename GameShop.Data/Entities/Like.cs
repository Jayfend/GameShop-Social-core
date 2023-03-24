using System;

namespace GameShop.Data.Entities
{
    public class Like : BaseEntity
    {
        public bool IsLiked { get; set; }
        public AppUser AppUser { get; set; }
        public Guid CreatorId { get; set; }
        public Post Post { get; set; }
        public Guid PostId { get; set; }
    }
}
