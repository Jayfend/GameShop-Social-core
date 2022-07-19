using GameShop.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameShop.ViewModels.Catalog.Games
{
    public class GetPublicGamePagingRequest : PagingRequestBase
    {
        public int? GenreID { get; set; }
    }
}
