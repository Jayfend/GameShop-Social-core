using GameShop.Data.EF;
using GameShop.Data.Entities;
using GameShop.ViewModels.Catalog.Contacts;
using GameShop.ViewModels.Common;
using System;
using System.Collections.Generic;
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

        public async Task<ApiResult<bool>> SendContact(SendContactRequest request)
        {
            if (request == null)
            {
                return new ApiErrorResult<bool>("Gửi không thành công!");
            }
            var newContact = new Contact()
            {
                Email = request.Email,
                Titile = request.Titile,
                Content = request.Content,
                ReceiveDate = DateTime.Now,
            };
            _context.Contacts.Add(newContact);
            await _context.SaveChangesAsync();
            return new ApiSuccessResult<bool>();
        }
    }
}