﻿namespace Inventory_Management_System.VerticalSlice.Features.Users.RegisterUser
{
    public class RegisterUserEndPointRequest
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
    }
}
