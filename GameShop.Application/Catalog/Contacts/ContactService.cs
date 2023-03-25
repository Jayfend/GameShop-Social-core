using GameShop.Data.EF;
using GameShop.Data.Entities;
using GameShop.ViewModels.Catalog.Contacts;
using GameShop.ViewModels.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameShop.Application.Catalog.Contacts
{
    public class ContactService : IContactService
    {
        private readonly GameShopDbContext _context;

        public ContactService(GameShopDbContext context)
        {
            _context = context;
        }

        public async Task<ApiResult<List<ContactViewModel>>> GetContact()
        {
            var contacts = await _context.Contacts.Select(x => new ContactViewModel()
            {
                Email = x.Email,
                Title = x.Titile,
                Content = x.Content,
                Receiveddate = x.ReceiveDate
            }).ToListAsync();
            if (contacts == null)
            {
                return new ApiErrorResult<List<ContactViewModel>>("Không tìm thấy phản hồi!");
            }
            else return new ApiSuccessResult<List<ContactViewModel>>(contacts);
        }

        public async Task<ApiResult<bool>> SendContact(SendContactRequest request)
        {
            if (request == null)
            {
                return new ApiErrorResult<bool>("Gửi không thành công!");
            }
            var newContact = new Contact()
            {
                Email = request.Email,
                Titile = request.Title,
                Content = request.Content,
                ReceiveDate = DateTime.Now,
            };
            _context.Contacts.Add(newContact);
            await _context.SaveChangesAsync();
            return new ApiSuccessResult<bool>();
        }
    }
}