﻿using System;
using System.Collections.Generic;
using System.Text;

namespace GameShop.ViewModels.Catalog.Categories
{
    public class EditCategoryRequest
    {
        public int GenreID { get; set; }
        public string GenreName { get; set; }
    }
}