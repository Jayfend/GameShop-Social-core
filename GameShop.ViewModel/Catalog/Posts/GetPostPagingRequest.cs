using GameShop.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameShop.ViewModels.Catalog.Posts
{
    public class GetPostPagingRequest : PagingRequestBase
    {
        public string Keyword { get; set; }
        public string UserName { get; set; }
    }
}
