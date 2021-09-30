using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace DAL
{
    public static class DataInitializer
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Author>().HasData(
                new Author 
                { 
                    Id = 1, 
                    Name = "William", 
                    Surname = "Shakespeare", 
                    Description = "William Shakespeare was an English playwright, poet, and actor, " +
                    "widely regarded as the greatest writer in the English language and the world's greatest dramatist."
                },
                new Author 
                { 
                    Id = 2, 
                    Name = "Mary", 
                    Surname = "Shelley", 
                    Description = "Mary Wollstonecraft Shelley (30 August 1797 – 1 February 1851) " +
                    "was an English novelist who wrote the Gothic novel Frankenstein; or, " +
                    "The Modern Prometheus (1818), which is considered an early example of science fiction." 
                },
                new Author
                { 
                    Id = 3, 
                    Name = "Charlotte", 
                    Surname = "Brontë", 
                    Description = "Charlotte Brontë (21 April 1816 – 31 March 1855) was an English novelist and poet, the eldest " +
                    "of the three Brontë sisters who survived into adulthood and whose novels became classics of English literature." 
                },
                new Author
                { 
                    Id = 4, 
                    Name = "Emily", 
                    Surname = "Brontë", 
                    Description = "Emily Jane Brontë (30 July 1818 – 19 December 1848) was an English novelist and poet " +
                    "who is best known for her only novel, Wuthering Heights, now considered a classic of English literature." 
                },
                new Author
                { 
                    Id = 5, 
                    Name = "Jules", 
                    Surname = "Verne", 
                    Description = "Jules Gabriel Verne (8 February 1828 – 24 March 1905) was a French novelist, poet, " +
                    "and playwright. His collaboration with the publisher Pierre-Jules Hetzel led to the creation of " +
                    "the Voyages extraordinaires,[3] a series of bestselling adventure novels including Journey to " +
                    "the Center of the Earth (1864), Twenty Thousand Leagues Under the Seas (1870), and Around the World " +
                    "in Eighty Days (1872). His novels, always very well documented, are generally set in the second half " +
                    "of the 19th century, taking into account the technological advances of the time." 
                },
                new Author
                { 
                    Id = 6, 
                    Name = "Jane", 
                    Surname = "Austen", 
                    Description = "Jane Austen (16 December 1775 – 18 July 1817) was an English novelist known primarily " +
                    "for her six major novels, which interpret, critique and comment upon the British landed gentry at the end " +
                    "of the 18th century." 
                },
                new Author
                { 
                    Id = 7, 
                    Name = "Charles", 
                    Surname = "Dickens", 
                    Description = "Charles John Huffam Dickens (7 February 1812 – 9 June 1870) was an English writer and social " +
                    "critic. He created some of the world's best-known fictional characters and is regarded by many as the greatest " +
                    "novelist of the Victorian era." 
                },
                new Author
                { 
                    Id = 8, 
                    Name = "Edgar Allan", 
                    Surname = "Poe", 
                    Description = "Edgar Allan Poe (born Edgar Poe; January 19, 1809 – October 7, 1849) was an American writer, poet, " +
                    "editor, and literary critic. Poe is best known for his poetry and short stories, particularly his tales of mystery " +
                    "and the macabre. " 
                }
            );

            modelBuilder.Entity<User>().HasData(
                new User 
                { 
                    Id = 1,
                    Name = "Alice", 
                    Surname = "First", 
                    Email = "alice@gmail.com", 
                    HashedPassword = "", 
                    IsAdmin = true
                },
                new User
                {
                    Id = 2,
                    Name = "Bob", 
                    Surname = "Second", 
                    Email = "bob@gmail.com", 
                    HashedPassword = "", 
                    IsAdmin = false
                }
            );

            modelBuilder.Entity<BookTemplate>().HasData(
                new BookTemplate 
                { 
                    Id = 1, 
                    Title = "Frankenstein",
                    Description = "Victor Frankenstein, a brilliant but wayward scientist, builds a human from dead flesh. " +
                    "Horrified at what he has done, he abandons his creation. The hideous creature learns language and becomes " +
                    "civilized but society rejects him. Spurned, he seeks vengeance on his creator. So begins a cycle of destruction, " +
                    "with Frankenstein and his 'monster' pursuing each other to the extremes of nature until all vestiges of their " +
                    "humanity are lost.",
                    ISBN = "9781509827756",
                    PageCount = 280,
                    DateOfRelease = new DateTime(1818, 1, 1),
                    Language = Language.English
                },
                new BookTemplate
                {
                    Id = 2,
                    Title = "Hamlet",
                    Description = "Considered one of Shakespeare′s most rich and enduring plays, the depiction of its hero Hamlet as " +
                    "he vows to avenge the murder of his father by his brother Claudius is both powerful and complex. As Hamlet tries " +
                    "to find out the truth of the situation, his troubled relationship with his mother comes to the fore, as do " +
                    "the paradoxes in his personality. A play of carefully crafted conflict and tragedy, Shakespeare′s intricate " +
                    "dialogue continues to fascinate audiences to this day.",
                    ISBN = "9780007902347",
                    PageCount = 192,
                    DateOfRelease = new DateTime(1600, 1, 1),
                    Language = Language.English
                },
                new BookTemplate
                {
                    Id = 3,
                    Title = "Romeo and Juliet",
                    Description = "Shakespeare's immortal drama tells the story of star-crossed lovers, rival dynasties and bloody " +
                    "revenge. Romeo and Juliet is a hymn to youth and the thrill of forbidden love, charged with sexual passion and " +
                    "violence, but also a warning of death: a dazzling combination of bawdy comedy and high tragedy.",
                    ISBN = "9780007902361",
                    PageCount = 160,
                    DateOfRelease = new DateTime(1591, 1, 1),
                    Language = Language.English
                }
            );

            modelBuilder.Entity<BookInstance>().HasData(
                new BookInstance
                { 
                    Id = 1, 
                    BookTemplateID = 1
                },
                new BookInstance
                {
                    Id = 2,
                    BookTemplateID = 1
                },
                new BookInstance
                {
                    Id = 1,
                    BookTemplateID = 1
                }
            );

            modelBuilder.Entity<BookCollection>().HasData(
                new BookCollection 
                { 
                    Id = 1,
                    Title = "Classics", 
                    Description = "Just classics.", 
                    CreationDate = new DateTime(2021, 9, 30), 
                    UserId = 2
                }
            );

            modelBuilder.Entity<Review>().HasData(
                new Review 
                { 
                    Id = 1, 
                    CreationDate = new DateTime(2021, 9, 30),
                    Content = "Best",
                    StarsAmmount = 5,
                    BookTemplateID = 1,
                    UserID = 2
                }
            );

            modelBuilder.Entity<Genre>().HasData(
                new Genre 
                { 
                    GenreID = GenreType.Detective, 
                    Books = new List<BookTemplate>() { } 
                }
            );

            modelBuilder.Entity<EBookTemplate>().HasData(
                new EBookTemplate 
                {
                    Id = 4,
                    Title = "Romeo and Juliet",
                    Description = "Shakespeare's immortal drama tells the story of star-crossed lovers, rival dynasties and bloody " +
                        "revenge. Romeo and Juliet is a hymn to youth and the thrill of forbidden love, charged with sexual passion and " +
                        "violence, but also a warning of death: a dazzling combination of bawdy comedy and high tragedy.",
                    ISBN = "9780141920252",
                    PageCount = 320,
                    DateOfRelease = new DateTime(1591, 1, 1),
                    Language = Language.English,
                    MemorySize = 1024,
                    Format = EBookFormat.EPUB
                }
            );

            modelBuilder.Entity<EBookInstance>().HasData(
                new EBookInstance 
                { 
                    Id = 1, 
                    EBookTemplateID = 4
                },
                new EBookInstance
                {
                    Id = 2,
                    EBookTemplateID = 4
                }
            );

            modelBuilder.Entity<Reservation>().HasData(
                new Reservation 
                { 
                    Id = 1, 
                    DateFrom = new DateTime(2021, 9, 30), 
                    DateTill = new DateTime(2021, 10, 30),
                    BookInstanceID = 1,
                    UserID = 2
                }
            );

            // modelBuilder.Entity<EReaderTemplate>().HasData(new EReaderTemplate { });

            // modelBuilder.Entity<EReaderInstance>().HasData(new EReaderInstance { });
        }
    }
}
