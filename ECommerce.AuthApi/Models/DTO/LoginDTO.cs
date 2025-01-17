﻿using System.ComponentModel.DataAnnotations;

namespace ECommerce.AuthApi.Models.DTO
{
    public class LoginDTO
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
