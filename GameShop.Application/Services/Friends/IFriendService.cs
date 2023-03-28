using GameShop.ViewModels.Friends;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GameShop.Application.Services.Friends
{
    public interface IFriendService
    {
        Task<bool> AddFriendAsync(AddFriendReqModel req);
    }
}
