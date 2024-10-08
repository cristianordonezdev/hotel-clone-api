﻿using System.ComponentModel.DataAnnotations.Schema;

namespace hotel_clone_api.Models.Domain
{
    public class Offer
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }

        [NotMapped]
        public Image Image { get; set; }
    }
}
