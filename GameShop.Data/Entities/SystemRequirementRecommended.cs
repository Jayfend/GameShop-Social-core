﻿using System;
using System.Collections.Generic;
using System.Text;

namespace GameShop.Data.Entities
{
    public class SystemRequirementRecommended
    {
        public int SRRID { get; set; }
        public string OS { get; set; }
        public string Processor { get; set; }
        public string Memory { get; set; }
        public string Graphics { get; set; }
        public string Storage { get; set; }
        public string AdditionalNotes { get; set; }
        public Game Game { get; set; }
        public int GameID { get; set; }
    }
}