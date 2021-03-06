﻿using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using Shouldly;
using Xunit;

namespace AutoMapper.IntegrationTests
{
    using UnitTests;
    using QueryableExtensions;
        
    public class NullableLongToLong : AutoMapperSpecBase
    {
        public class Customer
        {
            [Key]
            public long? Id { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
        }

        public class CustomerViewModel
        {
            public long Id { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
        }

        public class Context : DbContext
        {
            public Context()
            {
                Database.SetInitializer<Context>(new DatabaseInitializer());
            }

            public DbSet<Customer> Customers { get; set; }
        }

        public class DatabaseInitializer : CreateDatabaseIfNotExists<Context>
        {
            protected override void Seed(Context context)
            {
                context.Customers.Add(new Customer
                {
                    Id = 1,
                    FirstName = "Bob",
                    LastName = "Smith",
                });

                base.Seed(context);
            }
        }

        protected override MapperConfiguration Configuration => new MapperConfiguration(cfg =>
        {
            cfg.CreateProjection<Customer, CustomerViewModel>();
        });

        [Fact]
        public void Can_map_with_projection()
        {
            using (var context = new Context())
            {
                var model = ProjectTo<CustomerViewModel>(context.Customers).Single();
                model.Id.ShouldBe(1);
                model.FirstName.ShouldBe("Bob");
                model.LastName.ShouldBe("Smith");
            }
        }
    }

    public class NullableIntToLong : AutoMapperSpecBase
    {
        public class Customer
        {
            [Key]
            public int? Id { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
        }

        public class CustomerViewModel
        {
            public long Id { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
        }

        public class Context : DbContext
        {
            public Context()
            {
                Database.SetInitializer<Context>(new DatabaseInitializer());
            }

            public DbSet<Customer> Customers { get; set; }
        }

        public class DatabaseInitializer : CreateDatabaseIfNotExists<Context>
        {
            protected override void Seed(Context context)
            {
                context.Customers.Add(new Customer
                {
                    Id = 1,
                    FirstName = "Bob",
                    LastName = "Smith",
                });

                base.Seed(context);
            }
        }

        protected override MapperConfiguration Configuration => new MapperConfiguration(cfg =>
        {
            cfg.CreateProjection<Customer, CustomerViewModel>();
        });

        [Fact]
        public void Can_map_with_projection()
        {
            using(var context = new Context())
            {
                var model = ProjectTo<CustomerViewModel>(context.Customers).Single();
                model.Id.ShouldBe(1);
                model.FirstName.ShouldBe("Bob");
                model.LastName.ShouldBe("Smith");
            }
        }
    }
}