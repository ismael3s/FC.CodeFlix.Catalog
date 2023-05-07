﻿using FC.CodeFlix.Catalog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace FC.CodeFlix.Catalog.Infra.Data.EF;
public class CodeflixCatalogDbContext : DbContext
{

    public DbSet<Category> Categories => Set<Category>();
    public CodeflixCatalogDbContext(DbContextOptions<CodeflixCatalogDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //modelBuilder.ApplyConfiguration(new CategoryConfiguration());
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
