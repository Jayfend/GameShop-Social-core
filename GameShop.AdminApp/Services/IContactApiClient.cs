using GameShop.ViewModels.Catalog.Contacts;
using GameShop.ViewModels.Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GameShop.AdminApp.Services
{
    public interface IContactApiClient
    {
        Task<ApiResult<List<ContactViewModel>>> GetContacts();
    }
}