using GameShop.ViewModels.Catalog.Likes;
using GameShop.ViewModels.Catalog.Posts;
using GameShop.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GameShop.Application.Services.Posts
{
    public interface IPostService
    {
        Task<PostCreateResModel> CreateAsync(PostCreateReqModel req);
        Task<bool> DeleteAsync(Guid id);
        Task<PostUpdateResModel> UpdateAsync(PostUpdateReqModel req);
        Task<PostResModel>GetByIdAsync(Guid id);
        Task<PagedResult<PostResModel>> GetAsync(GetPostPagingRequest req);
        Task<bool> LikeAsync(LikeReqModel req);
    }
}
