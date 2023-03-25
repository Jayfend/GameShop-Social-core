using GameShop.ViewModels.Catalog.Posts;
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
    }
}
