﻿using System;
using System.Collections.Generic;

#nullable disable

namespace COMP2001.Models
{
    public partial class User
    {
        public int UserID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
