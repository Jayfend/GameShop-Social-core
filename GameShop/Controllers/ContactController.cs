using FRT.MasterDataCore.Customs;
using GameShop.Application.Services.Contacts;
using GameShop.ViewModels.Catalog.Contacts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Transactions;

namespace GameShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        private readonly IContactService _contactService;
        readonly ITransactionCustom _transactionCustom;
        public ContactController(IContactService contactService, ITransactionCustom transactionCustom)
        {
            _contactService = contactService;
            _transactionCustom = transactionCustom;
        }

        [HttpPost]
        public async Task<IActionResult> SendContact(SendContactRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            using (var transaction = _transactionCustom.CreateTransaction(isolationLevel: IsolationLevel.ReadUncommitted))
            {
                var result = await _contactService.SendContact(request);
                if (!result.IsSuccess)
                {
                    return BadRequest(result);
                }
                return Ok(result);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetContact()
        {
            var result = await _contactService.GetContact();
            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
    }
}