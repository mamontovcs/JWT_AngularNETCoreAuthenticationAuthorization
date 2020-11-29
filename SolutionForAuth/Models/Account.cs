﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SolutionForAuth.Models
{
    public class Account
    {
        public long Id { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public Role[] Roles { get; set; }
    }

    public enum Role
    {
        User,
        Admin
    }
}
