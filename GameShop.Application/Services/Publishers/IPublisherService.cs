using GameShop.ViewModels.Catalog.Publishers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GameShop.Application.Services.Publishers
{
    public interface IPublisherService
    {
        Task<PublisherDTO> CreateAsync(PublisherCreateDTO req);
        Task<List<PublisherDTO>> GetAllPublisher();
        Task<List<string>> GenerateKeyAsync(Guid publisherId, int amount);



    }
}
