using System;
using System.Collections.Generic;
using System.Text;

namespace GameShop.ViewModels.Catalog.Comments
{
    public class CommentDTO
    {
        public Guid Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public bool Status { get; set; }
        public string Content { get; set; }
        public Guid GameId { get; set; }
        public Guid UserId { get; set; }
        public int Rating { get; set; }
        public string ImagePath { get; set; }

    }
}
