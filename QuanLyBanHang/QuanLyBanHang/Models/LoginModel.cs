using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuanLyBanHang.Models
{
    public class LoginModel
    {
            public string Username { get; set; }
            public string Password { get; set; }
            public bool Remember { get; set; }

    }
}