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
                    },
                    new Book
                    {
                        Title = "Nine Stories",
                        YearOfPublishing = 2016,
                        IsAvailable = true,
                        Author = new Author
                        {
                            Name = "J.D. Salinger"
                        },
                        Genre = new Genre
                        {
                            Name = "Short stories collection"
                        }
                    },
                    new Book
                    {
                        Title = "In Search of the Castaways",
                        YearOfPublishing = 2012,
                        IsAvailable = true,
                        Author = new Author
                        {
                            Name = "Jules Verne"
                        },
                        Genre = new Genre
                        {
                            Name = "Adventure novel"
                        }
                    },
                    new Book
                    {
                        Title = "Three Men in a Boat (To Say Nothing of the Dog)",
                        YearOfPublishing = 2013,
                        IsAvailable = true,
                        Author = new Author
                        {
                            Name = "Jerome K. Jerome"
                        },
                        Genre = new Genre
                        {
                            Name = "Comedy novel"
                        }
                    },
                    new Book
                    {
                        Title = "The Old Man and the Sea",
                        YearOfPublishing = 2014,
                        IsAvailable = true,
                        Author = new Author
                        {
                            Name = "Ernest Hemingway"
                        },
                        Genre = new Genre
                        {
                            Name = "Novella"
                        }
                    },
                    new Book
                    {
                        Title = "A Study in Scarlet",
                        YearOfPublishing = 2013,
                        IsAvailable = true,
                        Author = new Author
                        {
                            Name = "Arthur Conan Doyle"
                        },
                        Genre = new Genre
                        {
                            Name = "Detective novel"
                        }
                    },
                    new Book
                    {
                        Title = "The Tragedy of Hamlet, Prince of Denmark",
                        YearOfPublishing = 2013,
                        IsAvailable = true,
                        Author = new Author
                        {
                            Name = "William Shakespeare"
                        },
                        Genre = new Genre
                        {
                            Name = "Tragedy"
                        }
                    },
                    new Book
                    {
                        Title = "Alice's Adventures in Wonderland",
                        YearOfPublishing = 2015,
                        IsAvailable = true,
                        Author = new Author
                        {
                            Name = "Lewis Carroll"
                        },
                        Genre = new Genre
                        {
                            Name = "Literary nonsense"
                        }
                    },
                    new Book
                    {
                        Title = "The Hunger Games",
                        YearOfPublishing = 2015,
                        IsAvailable = true,
                        Author = new Author
                        {
                            Name = "Suzanne Collins"
                        },
                        Genre = new Genre
                        {
                            Name = "Dystopian novel"
                        }
                    },
                    new Book
                    {
                        Title = "The Maze Runner",
                        YearOfPublishing = 2015,
                        IsAvailable = true,
                        Author = new Author
                        {
                            Name = "James Dashner"
                        },
                        Genre = new Genre
                        {
                            Name = "Science fiction"
                        }
                    },
                    new Book
                    {
                        Title = "The Adventures of Tom Sawyer",
                        YearOfPublishing = 2012,
                        IsAvailable = true,
                        Author = new Author
                        {
                            Name = "Mark Twain"
                        },
                        Genre = new Genre
                        {
                            Name = "Picaresque novel"
                        }
                    },
                    new Book
                    {
                        Title = "Nineteen Eighty-Four: A Novel",
                        YearOfPublishing = 2012,
                        IsAvailable = true,
                        Author = new Author
                        {
                            Name = "George Orwell"
                        },
                        Genre = new Genre
                        {
                            Name = "Political fiction"
                        }
                    },
                    new Book
                    {
                        Title = "Winnie-the-Pooh",
                        YearOfPublishing = 2012,
                        IsAvailable = true,
                        Author = new Author
                        {
                            Name = "A. A. Milne"
                        },
                        Genre = new Genre
                        {
                            Name = "Children's literature"
                        }
                    },
                    new Book
                    {
                        Title = "The Curious Case of Benjamin Button",
                        YearOfPublishing = 2014,
                        IsAvailable = true,
                        Author = new Author
                        {
                            Name = "Scott Fitzgerald"
                        },
                        Genre = new Genre
                        {
                            Name = "Short story"
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
                        Name = "Sarah Connor",
                        Email = "sarahconnor@yopmail.com",
                        Password = BCrypt.Net.BCrypt.HashPassword("qwerty123"),
                        Role = UserRole.Librarian
                    },
                    new User
                    {
                        Name = "John Constantine",
                        Email = "johnconstantine@yopmail.com",
                        Password = BCrypt.Net.BCrypt.HashPassword("qwerty123"),
                        Role = UserRole.Librarian
                    },
                    new User
                    {
                        Name = "Jane Doe",
                        Email = "janedoe@yopmail.com",
                        Password = BCrypt.Net.BCrypt.HashPassword("qwerty123"),
                        Role = UserRole.Reader
                    },
                    new User
                    {
                        Name = "John Connor",
                        Email = "johnconnor@yopmail.com",
                        Password = BCrypt.Net.BCrypt.HashPassword("qwerty123"),
                        Role = UserRole.Reader
                    },
                    new User
                    {
                        Name = "John Wick",
                        Email = "johnwick@yopmail.com",
                        Password = BCrypt.Net.BCrypt.HashPassword("qwerty123"),
                        Role = UserRole.Reader
                    },
                    new User
                    {
                        Name = "Marty McFly",
                        Email = "martymcfly@yopmail.com",
                        Password = BCrypt.Net.BCrypt.HashPassword("qwerty123"),
                        Role = UserRole.Reader
                    },
                    new User
                    {
                        Name = "Lara Croft",
                        Email = "laracroft@yopmail.com",
                        Password = BCrypt.Net.BCrypt.HashPassword("qwerty123"),
                        Role = UserRole.Reader
                    },
                    new User
                    {
                        Name = "Barry Allen",
                        Email = "barryallen@yopmail.com",
                        Password = BCrypt.Net.BCrypt.HashPassword("qwerty123"),
                        Role = UserRole.Reader
                    },
                    new User
                    {
                        Name = "Diana Prince",
                        Email = "dianaprince@yopmail.com",
                        Password = BCrypt.Net.BCrypt.HashPassword("qwerty123"),
                        Role = UserRole.Reader
                    }
                };
                context.Users.AddRange(users);
                context.SaveChanges();
            }
        }
    }
}
