﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebAPI.Models;

namespace WebAPI.Data
{
    public class WebAPIContext : DbContext
    {
        public WebAPIContext (DbContextOptions<WebAPIContext> options)
            : base(options)
        {
        }

        public DbSet<WebAPI.Models.Product> Product { get; set; } = default!;
        public DbSet<WebAPI.Models.Animal> Animal { get; set; } = default!;
        public DbSet<WebAPI.Models.Client> Client { get; set; } = default!;
    }
}
