using GameShop.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameShop.Application.Catalog.Games.Dtos.Public
{
    public class GetGamePagingRequest : PagingRequestBase
    {
        public int GenreIDs { get; set; }
    }
}
