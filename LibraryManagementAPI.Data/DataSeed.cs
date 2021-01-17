using System.Collections.Generic;
using System.Linq;
using LibraryManagementAPI.Data.Enums;
using LibraryManagementAPI.Data.Models;

namespace LibraryManagementAPI.Data
{
    public static class DataSeed
    {
        public static void SeedData(LibraryContext context)
        {
            SeedBooks(context);
            SeedUsers(context);
        }
        
        public static void SeedBooks(LibraryContext context)
        {
            if (!context.Books.Any())
            {
                List<Book> books = new List<Book>
                {
                    new Book
                    {
                        Title = "Ivanhoe",
                        YearOfPublishing = 2015,
                        IsAvailable = true,
                        Author = new Author
                        {
                            Name = "Walter Scott"
                        },
                        Genre = new Genre
                        {
                            Name = "Historic novel"
                        }
                    },
                    new Book
                    {
                        Title = "City of Bones",
                        YearOfPublishing = 2016,
                        IsAvailable = true,
                        Author = new Author
                        {
                            Name = "Cassandra Clare"
                        },
                        Genre = new Genre
                        {
                            Name = "Fantasy"
                        }
                    }
                };
                context.Books.AddRange(books);
                context.SaveChanges();
            }
        }

        public static void SeedUsers(LibraryContext context)
        {
            if (!context.Users.Any())
            {
                List<User> users = new List<User>
                {
                    new User
                    {
                        Name = "John Doe",
                        Email = "johndoe@yopmail.com",
                        Password = BCrypt.Net.BCrypt.HashPassword("qwerty123"),
                        Role = UserRole.Librarian
                    },
                    new User
                    {
                        Name = "Jane Doe",
                        Email = "janedoe@yopmail.com",
                        Password = BCrypt.Net.BCrypt.HashPassword("qwerty456"),
                        Role = UserRole.Reader
                    },
                };
                context.Users.AddRange(users);
                context.SaveChanges();
            }
        }
    }
}
