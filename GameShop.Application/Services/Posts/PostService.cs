using AutoMapper;
using GameShop.Application.Common;
using GameShop.Data.EF;
using GameShop.Data.Entities;
using GameShop.Utilities.Exceptions;
using GameShop.ViewModels.Catalog.Comments;
using GameShop.ViewModels.Catalog.Games;
using GameShop.ViewModels.Catalog.Likes;
using GameShop.ViewModels.Catalog.Posts;
using GameShop.ViewModels.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace GameShop.Application.Services.Posts
{
    public class PostService : IPostService
    { private readonly ISaveFileService _saveFileService;
        private readonly UserManager<AppUser> _userManager;
        private readonly GameShopDbContext _context;
        private readonly IMapper _mapper;
        public PostService(ISaveFileService saveFileService, GameShopDbContext context, IMapper mapper, UserManager<AppUser> userManager)
        {
            _saveFileService = saveFileService;
            _context = context;
            _mapper = mapper;
            _userManager = userManager;
        }
        public async Task<PostCreateResModel> CreateAsync(PostCreateReqModel req)
        {
            if (req == null)
            {
                throw new GameShopException("yêu cầu không được rỗng");

            }
            var user = await _userManager.FindByIdAsync(req.UserId.ToString());
            if (user == null)
            {
                throw new GameShopException("Không tìm thấy người dùng");
            }
            var newPost = new Post()
            {
                CreatorId = req.UserId,
                Content = req.Content,
                ImagePath = await _saveFileService.SaveFileAsync(req.Image),
                CreatedDate = DateTime.Now,
                Comments = new List<Comment>(),
                Likes = new List<Like>(),
                Status = true,
                UserName = user.UserName

            };
            await _context.Posts.AddAsync(newPost);
            await _context.SaveChangesAsync();
            return _mapper.Map<Post, PostCreateResModel>(newPost);

        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var post = await _context.Posts.Where(x => x.Id == id && x.Status == true).FirstOrDefaultAsync();
            if (post == null)
            {
                throw new GameShopException("Không tìm thấy bài viết");
            }
            post.Status = false;
            _context.Posts.Update(post);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<PagedResult<PostResModel>> GetAsync(GetPostPagingRequest req)
        {
            var user = _userManager.FindByIdAsync(req.UserId.ToString());
            var friends = await _context.Friends.Where(x => x.UserId1 == req.UserId || x.UserId2 == req.UserId).ToListAsync();
            var idList = friends.Select(x=>x.UserId1).Concat(friends.Select(x=>x.UserId2)).ToList().Distinct();
           
            var query = _context.Posts.Where(x=>idList.Contains(x.CreatorId)).AsQueryable();

            // filter
            if (!string.IsNullOrEmpty(req.Keyword))
            {
                query = query.Where(x => x.Content.Contains(req.Keyword));
            }

            if (req.UserName != null)
            {
                query = query.Where(x =>  idList.Contains(x.CreatorId));
            }
            //paging
            int totalrow = await query.CountAsync();
          
            var postList = _mapper.Map<List<Post>, List<PostResModel>>(await query.ToListAsync());
            var postIdList = postList.Select(x=>x.Id).ToList();
            var likes = await _context.Likes.Where(x => postIdList.Contains(x.PostId)).ToListAsync();
            foreach(var post in postList)
            {
                var like = likes.Where(x=>x.PostId == post.Id && x.CreatorId == req.UserId).FirstOrDefault()
                if(like != null)
                {
                    post.IsLike = like.IsLiked;
                }
                else
                {
                    post.IsLike = false;
                }
                   
            }
            var pagedResult = new PagedResult<PostResModel>()
            {
                TotalRecords = totalrow,
                PageIndex = req.PageIndex,
                PageSize = req.PageSize,
                Items = postList
            };
            return pagedResult;
        }

        public async Task<PostResModel> GetByIdAsync(Guid id)
        {
            var post = await _context.Posts.Where(x => x.Id == id && x.Status == true).FirstOrDefaultAsync();
            if (post == null)
            {
                throw new GameShopException("Không tìm thấy bài viết");
            }
            
            var result = _mapper.Map<Post, PostResModel>(post);
            return result;
        }

        public async Task<bool> LikeAsync(LikeReqModel req)
        {
            var user =await  _userManager.FindByIdAsync(req.UserId.ToString());
            if(user == null)
            {
                throw new GameShopException("Không tìm thấy người dùng");
            }
            var post = await _context.Posts.Where(x => x.Id == req.PostId && x.Status == true).FirstOrDefaultAsync();
            if (post == null)
            {
                throw new GameShopException("Không tìm thấy bài viết");
            }
            var like = await _context.Likes.Where(x=>x.AppUser.Id == req.UserId).FirstOrDefaultAsync();
            if(like != null)
            {
                like.IsLiked = !like.IsLiked;
                _context.Likes.Update(like);
                
            }
            else
            {
                var newLike = new Like()
                {
                    IsLiked = true,
                    PostId = req.PostId,
                    CreatorId = req.UserId,
                    CreatedDate = DateTime.Now,
                    Status = true
                };
                await _context.Likes.AddAsync(newLike);
            }
            _context.SaveChanges();
            return true;

        }

        public async Task<PostUpdateResModel> UpdateAsync(PostUpdateReqModel req)
        {
            var post = await _context.Posts.Where(x => x.Id == req.Id && x.Status == true).FirstOrDefaultAsync();
            if (post == null)
            {
                throw new GameShopException("Không tìm thấy bài viết");
            }
            post.Content = req.Content;
            post.ImagePath = await _saveFileService.SaveFileAsync(req.Image);
            post.UpdatedDate = DateTime.Now;
             _context.Posts.Update(post);
            await _context.SaveChangesAsync();
            return _mapper.Map<Post,PostUpdateResModel>(post);
        }
    }
}
