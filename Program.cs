﻿using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

// Adding Context Class
public class ShopContext: DbContext
{
    public DbSet<User> Users {get;set;}
    public DbSet<Address> Addresses {get;set;}
    public DbSet<Customer> Customers {get;set;}

    public static readonly ILoggerFactory MyLoggerFactory = LoggerFactory.Create(builder => { builder.AddConsole(); }); // LINQ to SQL at Console
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {      
        optionsBuilder
        .UseLoggerFactory(MyLoggerFactory)
        .UseSqlite("Data Source=shop2.db");
    }
}

// Adding Entity Classes

public class User
{
    public int Id { get; set; } // Primary Key
    [Required]
    public string Username { get; set; }
    [Required]
    public string Email { get; set; }
    public List<Address> Addresses { get; set; } // Navigation Property
    public Customer Customer { get; set; } // Navigation Property
}

public class Address
{
    public int Id { get; set; } // Primary Key
    [Required]
    public string Fullname { get; set; }
    public string Title { get; set; }
    public string Body { get; set; }
    public User User { get; set; } // Navigation Property
    public int UserId { get; set; } // Foreign Key
}

public class Customer
{
    public int Id { get; set; } // Primary Key
    public string IdentityNumber { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public User User { get; set; } // Navigation Property
    public int UserId { get; set; } // Unique Key
}

public class Supplier
{
    public int Id { get; set; } // Primary Key
    public string Name { get; set; }
    public string TaxNumber { get; set; }
}

class Program
{

    static void InsertUsers()
    {
        var users = new List<User>(){
            new User(){Username="deneme1", Email="email@email.com"},
            new User(){Username="deneme2", Email="email@email.com"},
            new User(){Username="deneme3", Email="email@email.com"},
        };

        using (var db = new ShopContext())
        {
            db.Users.AddRange(users);
            db.SaveChanges();
        }
    }

    static void InsertAddress()
    {
        var addresses = new List<Address>(){
            new Address(){Fullname="Deneme 1", Title="Ev Adresi", Body="Ankara", UserId=1},
            new Address(){Fullname="Deneme 1", Title="İş Adresi", Body="İstanbul", UserId=1},
            new Address(){Fullname="Deneme 2", Title="Ev Adresi", Body="İzmir", UserId=2},
            new Address(){Fullname="Deneme 2", Title="İş Adresi", Body="Antalya", UserId=2},
            new Address(){Fullname="Deneme 3", Title="Ev Adresi", Body="Ankara", UserId=3},
            new Address(){Fullname="Deneme 3", Title="İş Adresi", Body="İstanbul", UserId=3},
        };

        using (var db = new ShopContext())
        {
            db.Addresses.AddRange(addresses);
            db.SaveChanges();
        }
    }

    static void InsertCustomer()
    {
        using (var db = new ShopContext())
        {
            var customer = new Customer()
            {
                IdentityNumber="6542314",
                FirstName="Namecustomer4",
                LastName="Lastnamecustomer4",
                User = db.Users.FirstOrDefault(i=>i.Id==4)
            };

            db.Customers.Add(customer);
            db.SaveChanges();
        }

        // using (var db = new ShopContext())
        // {
        //     var user = new User()
        //     {
        //         Username = "user",
        //         Email="user@gmail.com",
        //         Customer = new Customer(){
        //             FirstName = "Userdeneme",
        //             LastName = "Userdeneme",
        //             IdentityNumber="1234567"
        //         }
        //     };
        //     db.Users.Add(user);
        //     db.SaveChanges();
        // }

    }
    
    static void Main()
    {
        InsertUsers();
    }
}
