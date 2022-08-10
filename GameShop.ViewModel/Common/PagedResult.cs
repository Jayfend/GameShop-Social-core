using System;
using System.Collections.Generic;
using System.Text;

namespace GameShop.ViewModels.Common
{
    public class PagedResult<T> : PagedResultBase
    {
        public List<T> Items { get; set; }
    }
}