using DAL.Entities;
using DAL.Entities.ConnectionTables;
using DAL.Enums;
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
                    "the Voyages extraordinaires, a series of bestselling adventure novels."
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
                },
                new Author
                {
                    Id = 9,
                    Name = "Douglas",
                    Surname = "Adams",
                    Description = "Douglas Noël Adams was an English author, comic radio dramatist, and musician. He is best known as " +
                    "the author of the Hitchhiker's Guide to the Galaxy series. Hitchhiker's began on radio, and developed into a " +
                    "trilogy of five books (which sold more than fifteen million copies during his lifetime) as well as a television " +
                    "series, a comic book series, a computer game, and a feature film that was completed after Adams' death. The series " +
                    "has also been adapted for live theatre using various scripts; the earliest such productions used material newly written " +
                    "by Adams. He was known to some fans as Bop Ad (after his illegible signature), or by his initials \"DNA\"."
                },
                new Author
                {
                    Id = 10,
                    Name = "George",
                    Surname = "Orwell",
                    Description = "Eric Arthur Blair, better known by his pen name George Orwell, was an English author and journalist. " +
                    "His work is marked by keen intelligence and wit, a profound awareness of social injustice, an intense opposition to " +
                    "totalitarianism, a passion for clarity in language, and a belief in democratic socialism."
                },
                new Author
                {
                    Id = 11,
                    Name = "Dan",
                    Surname = "Brown",
                    Description = "Dan Brown is the author of numerous #1 bestselling novels, including The Da Vinci Code, which has " +
                    "become one of the best selling novels of all time as well as the subject of intellectual debate among readers and " +
                    "scholars. Brown’s novels are published in 52 languages around the world with 200 million copies in print."
                },
                new Author
                {
                    Id = 12,
                    Name = "Antoine",
                    Surname = "de Saint-Exupéry",
                    Description = "Antoine de Saint-Exupéry was born in Lyons on June 29, 1900. He flew for the first time at the age " +
                    "of twelve, at the Ambérieu airfield, and it was then that he became determined to be a pilot. He kept that ambition " +
                    "even after moving to a school in Switzerland and while spending summer vacations at the family's château " +
                    "at Saint-Maurice-de-Rémens, in eastern France. (The house at Saint-Maurice appears again and again in Saint-Exupéry's writing.)"
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
                },
                new User
                {
                    Id = 3,
                    Name = "Eve",
                    Surname = "Third",
                    Email = "evee@gmail.com",
                    HashedPassword = "",
                    IsAdmin = false
                },
                new User
                {
                    Id = 4,
                    Name = "Jack",
                    Surname = "Fourth",
                    Email = "jack@gmail.com",
                    HashedPassword = "",
                    IsAdmin = false
                },
                new User
                {
                    Id = 5,
                    Name = "Malory",
                    Surname = "Fifth",
                    Email = "malory@gmail.com",
                    HashedPassword = "",
                    IsAdmin = false
                }
            );

            modelBuilder.Entity<Book>().HasData(
                new Book
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
                new Book
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
                new Book
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
                },
                new Book
                {
                    Id = 4,
                    Title = "The Hitchhiker's Guide to the Galaxy",
                    Description = "Shakespeare's immortal drama tells the story of star-crossed lovers, rival dynasties and bloody " +
                    "revenge. Romeo and Juliet is a hymn to youth and the thrill of forbidden love, charged with sexual passion and " +
                    "violence, but also a warning of death: a dazzling combination of bawdy comedy and high tragedy.",
                    ISBN = "9780671461492",
                    PageCount = 193,
                    DateOfRelease = new DateTime(2007, 6, 23),
                    Language = Language.English
                },
                new Book
                {
                    Id = 5,
                    Title = "The Little Prince",
                    Description = "A pilot stranded in the desert awakes one morning to see, standing before him, the most extraordinary " +
                    "little fellow. \"Please,\" asks the stranger, \"draw me a sheep.\" And the pilot realizes that when life's events are " +
                    "too difficult to understand, there is no choice but to succumb to their mysteries. He pulls out pencil and paper... " +
                    "And thus begins this wise and enchanting fable that, in teaching the secret of what is really important in life, has " +
                    "changed forever the world for its readers.",
                    ISBN = "9783140464079",
                    PageCount = 96,
                    DateOfRelease = new DateTime(1943, 4, 6),
                    Language = Language.English
                },
                new Book
                {
                    Id = 6,
                    Title = "The Da Vinci Code",
                    Description = "While in Paris, Harvard symbologist Robert Langdon is awakened by a phone call in the dead " +
                    "of the night. The elderly curator of the Louvre has been murdered inside the museum, his body covered in " +
                    "baffling symbols. As Langdon and gifted French cryptologist Sophie Neveu sort through the bizarre riddles, " +
                    "they are stunned to discover a trail of clues hidden in the works of Leonardo da Vinci—clues visible for all " +
                    "to see and yet ingeniously disguised by the painter.",
                    ISBN = "9780307277671 ",
                    PageCount = 489,
                    DateOfRelease = new DateTime(2006, 3, 28),
                    Language = Language.English
                },
                new Book
                {
                    Id = 7,
                    Title = "Animal Farm",
                    Description = "A farm is taken over by its overworked, mistreated animals. With flaming idealism and stirring slogans, " +
                    "they set out to create a paradise of progress, justice, and equality. Thus the stage is set for one of the most telling " +
                    "satiric fables ever penned –a razor-edged fairy tale for grown-ups that records the evolution from revolution against " +
                    "tyranny to a totalitarianism just as terrible. When Animal Farm was first published, Stalinist Russia was seen as its target. " +
                    "Today it is devastatingly clear that wherever and whenever freedom is attacked,under whatever banner,the cutting clarity and savage " +
                    "comedy of George Orwell’s masterpiece have a meaning and message still ferociously fresh.",
                    ISBN = "9780451526342",
                    PageCount = 141,
                    DateOfRelease = new DateTime(1945, 8, 17),
                    Language = Language.English
                },
                new Book
                {
                    Id = 8,
                    Title = "Pride and Prejudice",
                    Description = "Since its immediate success in 1813, Pride and Prejudice has remained one of the most popular novels in " +
                    "the English language. Jane Austen called this brilliant work \"her own darling child\" and its vivacious heroine, Elizabeth " +
                    "Bennet, \" as delightful a creature as ever appeared in print.\" The romantic clash between the opinionated Elizabeth and " +
                    "her proud beau, Mr. Darcy, is a splendid performance of civilized sparring. And Jane Austen's radiant wit sparkles as her " +
                    "characters dance a delicate quadrille of flirtation and intrigue, making this book the most superb comedy of manners of " +
                    "Regency England.",
                    ISBN = "9780679783268",
                    PageCount = 279,
                    DateOfRelease = new DateTime(1813, 1, 28),
                    Language = Language.English
                }
            );

            modelBuilder.Entity<AuthorBook>().HasData(
                new { BookID = 1, AuthorID = 2 },
                new { BookID = 2, AuthorID = 1 },
                new { BookID = 3, AuthorID = 1 },
                new { BookID = 4, AuthorID = 9 },
                new { BookID = 5, AuthorID = 12 },
                new { BookID = 6, AuthorID = 11 },
                new { BookID = 7, AuthorID = 10 },
                new { BookID = 8, AuthorID = 6 },
                new { BookID = 9, AuthorID = 1 }
            );

            modelBuilder.Entity<BookGenre>().HasData(
                new { BookID = 1, GenreID = (int)GenreType.Classic },
                new { BookID = 2, GenreID = (int)GenreType.Classic },
                new { BookID = 3, GenreID = (int)GenreType.Classic },
                new { BookID = 4, GenreID = (int)GenreType.Scifi },
                new { BookID = 5, GenreID = (int)GenreType.Fantasy },
                new { BookID = 6, GenreID = (int)GenreType.Detective },
                new { BookID = 7, GenreID = (int)GenreType.Fantasy },
                new { BookID = 8, GenreID = (int)GenreType.Lovestory },
                new { BookID = 9, GenreID = (int)GenreType.Classic }
            );

            modelBuilder.Entity<BookInstance>().HasData(
                new BookInstance
                {
                    Id = 1,
                    BookOwnerId = 1,
                    BookTemplateID = 1,
                    Conditon = BookInstanceCondition.Fair,
                },
                new BookInstance
                {
                    Id = 2,
                    BookOwnerId = 1,
                    BookTemplateID = 2,
                    Conditon = BookInstanceCondition.Fair,
                },
                new BookInstance
                {
                    Id = 3,
                    BookOwnerId = 2,
                    BookTemplateID = 3,
                    Conditon = BookInstanceCondition.New,
                },
                new BookInstance
                {
                    Id = 4,
                    BookOwnerId = 3,
                    BookTemplateID = 4,
                    Conditon = BookInstanceCondition.New,
                },
                new BookInstance
                {
                    Id = 5,
                    BookOwnerId = 4,
                    BookTemplateID = 5,
                    Conditon = BookInstanceCondition.Good,
                },
                new BookInstance
                {
                    Id = 6,
                    BookOwnerId = 5,
                    BookTemplateID = 6,
                    Conditon = BookInstanceCondition.VeryGood,
                },
                new BookInstance
                {
                    Id = 7,
                    BookOwnerId = 5,
                    BookTemplateID = 7,
                    Conditon = BookInstanceCondition.Good,
                },
                new BookInstance
                {
                    Id = 8,
                    BookOwnerId = 3,
                    BookTemplateID = 8,
                    Conditon = BookInstanceCondition.Poor,
                }
            );

            modelBuilder.Entity<BookCollection>().HasData(
                new BookCollection
                {
                    Id = 1,
                    Title = "Classics",
                    Description = "Just classics.",
                    CreationDate = new DateTime(2021, 9, 30),
                    UserId = 2,
                    Books = new List<BookCollectionBook> { }
                }
            );

            modelBuilder.Entity<BookCollectionBook>().HasData(
                new { BookCollectionID = 1, BookID = 1 },
                new { BookCollectionID = 1, BookID = 2 },
                new { BookCollectionID = 1, BookID = 3 }
            );

            modelBuilder.Entity<Review>().HasData(
                new Review
                {
                    Id = 1,
                    CreationDate = new DateTime(2021, 2, 28),
                    Content = "Best",
                    StarsAmmount = 5,
                    BookTemplateID = 1,
                    UserID = 1
                },
                new Review
                {
                    Id = 2,
                    CreationDate = new DateTime(2021, 3, 8),
                    Content = "Great!!",
                    StarsAmmount = 5,
                    BookTemplateID = 1,
                    UserID = 2
                },
                new Review
                {
                    Id = 3,
                    CreationDate = new DateTime(2021, 10, 30),
                    Content = "huh",
                    StarsAmmount = 1,
                    BookTemplateID = 1,
                    UserID = 3
                },
                new Review
                {
                    Id = 4,
                    CreationDate = new DateTime(2021, 7, 4),
                    Content = "Changed my life",
                    StarsAmmount = 5,
                    BookTemplateID = 8,
                    UserID = 4
                }
            );

            modelBuilder.Entity<Genre>().HasData(
                new Genre
                {
                    Id = (int)GenreType.Detective
                },
                new Genre
                {
                    Id = (int)GenreType.Lovestory
                },
                new Genre
                {
                    Id = (int)GenreType.Classic
                },
                new Genre
                {
                    Id = (int)GenreType.Fantasy
                },
                new Genre
                {
                    Id = (int)GenreType.Medieval
                },
                new Genre
                {
                    Id = (int)GenreType.Scifi
                }
            );

            modelBuilder.Entity<EBook>().HasData(
                new EBook
                {
                    Id = 9,
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
                },
                new EBook
                {
                    Id = 10,
                    Title = "Jacky and Katka against Speed",
                    Description = "They dont like me at all :sadCatto: :(",
                    ISBN = "9780141920282",
                    PageCount = 420,
                    DateOfRelease = new DateTime(1591, 1, 1),
                    Language = Language.English,
                    MemorySize = 1024,
                    Format = EBookFormat.EPUB
                }
            );

            modelBuilder.Entity<Reservation>().HasData(
                new Reservation
                {
                    Id = 1,
                    DateFrom = new DateTime(2021, 1, 30),
                    DateTill = new DateTime(2021, 2, 28),
                    UserID = 1
                },
                new Reservation
                {
                    Id = 2,
                    DateFrom = new DateTime(2021, 3, 1),
                    DateTill = new DateTime(2021, 3, 8),
                    UserID = 1
                },
                new Reservation
                {
                    Id = 3,
                    DateFrom = new DateTime(2021, 9, 30),
                    DateTill = new DateTime(2021, 10, 30),
                    UserID = 1
                },
                new Reservation
                {
                    Id = 4,
                    DateFrom = new DateTime(2021, 10, 30),
                    DateTill = new DateTime(2021, 11, 30),
                    UserID = 2
                },
                new Reservation
                {
                    Id = 5,
                    DateFrom = new DateTime(2021, 9, 30),
                    DateTill = new DateTime(2021, 10, 30),
                    UserID = 2
                },
                new Reservation
                {
                    Id = 6,
                    DateFrom = new DateTime(2021, 8, 30),
                    DateTill = new DateTime(2021, 9, 1),
                    UserID = 4
                },
                new Reservation
                {
                    Id = 7,
                    DateFrom = new DateTime(2021, 12, 1),
                    DateTill = new DateTime(2021, 12, 15),
                    UserID = 3
                },
                new Reservation
                {
                    Id = 8,
                    DateFrom = new DateTime(2021, 1, 30),
                    DateTill = new DateTime(2021, 4, 30),
                    UserID = 2
                },
                new Reservation
                {
                    Id = 9,
                    DateFrom = new DateTime(2021, 4, 30),
                    DateTill = new DateTime(2021, 9, 4),
                    UserID = 1
                },
                new Reservation
                {
                    Id = 10,
                    DateFrom = new DateTime(2021, 7, 30),
                    DateTill = new DateTime(2021, 8, 10),
                    EReaderID = 1,
                    UserID = 4
                }
            );

            modelBuilder.Entity<ReservationBookInstance>().HasData(
                new ReservationBookInstance
                {
                    ReservationID = 1,
                    BookInstanceID = 1
                },
                new ReservationBookInstance
                {
                    ReservationID = 2,
                    BookInstanceID = 2
                },
                new ReservationBookInstance
                {
                    ReservationID = 3,
                    BookInstanceID = 3
                },
                new ReservationBookInstance
                {
                    ReservationID = 4,
                    BookInstanceID = 1
                }, new ReservationBookInstance
                {
                    ReservationID = 5,
                    BookInstanceID = 2
                }, new ReservationBookInstance
                {
                    ReservationID = 6,
                    BookInstanceID = 5
                }, new ReservationBookInstance
                {
                    ReservationID = 7,
                    BookInstanceID = 3
                }, new ReservationBookInstance
                {
                    ReservationID = 8,
                    BookInstanceID = 7
                }, new ReservationBookInstance
                {
                    ReservationID = 9,
                    BookInstanceID = 8
                }
            );

            modelBuilder.Entity<EReader>().HasData(
                new EReader
                {
                    Id = 1,
                    Model = "632 Touch HD 3",
                    CompanyMake = "PocketBook",
                    MemoryInMB = 16000
                },
                new EReader
                {
                    Id = 2,
                    Model = "Nova 3",
                    CompanyMake = "ONYX BOOX",
                    MemoryInMB = 32000
                },
                new EReader
                {
                    Id = 3,
                    Model = "1040 InkPad X",
                    CompanyMake = "PocketBook",
                    MemoryInMB = 32000
                }
            );

            modelBuilder.Entity<EReaderInstance>().HasData(
                new EReaderInstance
                {
                    Id = 1,
                    EreaderOwnerId = 1,
                    EReaderTemplateID = 1
                },
                new EReaderInstance
                {
                    Id = 2,
                    EreaderOwnerId = 2,
                    EReaderTemplateID = 1
                },
                new EReaderInstance
                {
                    Id = 3,
                    EreaderOwnerId = 3,
                    EReaderTemplateID = 2
                }
            );

            modelBuilder.Entity<EBookEReaderInstance>().HasData(
                new EBookEReaderInstance
                {
                    EBookID = 10,
                    EReaderInstanceID = 1
                },
                new EBookEReaderInstance
                {
                    EBookID = 10,
                    EReaderInstanceID = 2
                },
                new EBookEReaderInstance
                {
                    EBookID = 9,
                    EReaderInstanceID = 1
                }
            );
        }
    }
}
