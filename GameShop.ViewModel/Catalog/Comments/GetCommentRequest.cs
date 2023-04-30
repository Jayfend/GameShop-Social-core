using GameShop.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameShop.ViewModels.Catalog.Comments
{
    public class GetCommentRequest : PagingRequestBase
    {
        public Guid? GameId { get; set; }

    }
}
