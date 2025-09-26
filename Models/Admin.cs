using System;
using System.Collections.Generic;

namespace coba.Models;

public partial class Admin
{
    public int IdAdmin { get; set; }

    public string? Username { get; set; }

    public string? Password { get; set; }

    public string? NamaAdmin { get; set; }
}
