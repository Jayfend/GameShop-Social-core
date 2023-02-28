using GameShop.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameShop.ViewModels.Catalog.Games
{
    public class GetManageGamePagingRequest : PagingRequestBase
    {
        public string Keyword { get; set; }
        public Guid? GenreID { get; set; }

    }
}
