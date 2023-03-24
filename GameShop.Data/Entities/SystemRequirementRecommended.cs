using System;
using System.Collections.Generic;
using System.Text;

namespace GameShop.Data.Entities
{
    public class SystemRequirementRecommended : BaseEntity
    {
        public string OS { get; set; }
        public string Processor { get; set; }
        public string Memory { get; set; }
        public string Graphics { get; set; }
        public string Storage { get; set; }
        public string AdditionalNotes { get; set; }
        public Game Game { get; set; }
        public Guid GameID { get; set; }
        public string Soundcard { get; set; }
    }
}
