using GameShop.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameShop.Application.Catalog.Games.Dtos.Manage
{
    public class GetGamePagingRequest : PagingRequestBase
    {
        public string Keyword { get; set; }
        public List<int> GenreIDs { get; set; }

    }
}
