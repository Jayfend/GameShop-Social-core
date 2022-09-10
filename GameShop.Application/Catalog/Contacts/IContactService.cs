﻿using GameShop.ViewModels.Catalog.Contacts;
using GameShop.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GameShop.Application.Catalog.Contacts
{
    public interface IContactService
    {
        public Task<ApiResult<bool>> SendContact(SendContactRequest request);
    }
}