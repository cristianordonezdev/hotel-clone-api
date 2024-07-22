﻿using System.ComponentModel.DataAnnotations;

namespace hotel_clone_api.Models.DTOs
{
    public class RegisterUserDto
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public string[] Roles { get; set; }
    }
}
