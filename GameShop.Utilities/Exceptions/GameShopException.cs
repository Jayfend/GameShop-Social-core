using System;
using System.Collections.Generic;
using System.Text;

namespace GameShop.Utilities.Exceptions
{
    public class GameShopException : Exception
    {
        public GameShopException()
        {

        }
       
        public GameShopException(string message)
            : base(message)
        {
        }

        public GameShopException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
