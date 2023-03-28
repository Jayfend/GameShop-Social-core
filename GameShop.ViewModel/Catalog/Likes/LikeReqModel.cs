using System;
using System.Collections.Generic;
using System.Text;

namespace GameShop.ViewModels.Catalog.Likes
{
    public class LikeReqModel
    {
        public Guid UserId { get; set; }
        public Guid PostId { get; set; }
    }
}
