using Lesson6LoadingsTask.Data;
using Lesson6LoadingsTask.Models;
using Microsoft.EntityFrameworkCore;

//Library db(EF CORE)
//Admin panel
//Lib Id daxil ederek sisteme girish edir.
//Kitablara baxa bilmeli.
//Yeni Kitab elave ede bilir.
//Sechdiyi kitabi Update edir.
//Kitabi sile bilir (eger hemin kitabi kimse oxuyursa, sile bilmez)
//CRUD operations

var context = new LibraryContext();


while (true)
{
    start:

    int libId;

    Console.Clear();    

    Console.Write("Enter the Lib ID : ");
    libId = int.Parse(Console.ReadLine()!);

    var lib = context.Libs.FirstOrDefault(l => l.Id == libId);

    try
    {

        if (lib is not null)
        {
            int choice;
            Console.WriteLine("0.Exit");
            Console.WriteLine("1.Books");
            Console.WriteLine("2.Add book");
            Console.WriteLine("3.Update the book");
            Console.WriteLine("4.Delete the book");
            Console.Write("Enter your choice : ");
            choice = int.Parse(Console.ReadLine()!);

            if (choice == 0)
            {
                break;
            }

            else if (choice == 1)
            {
                var books = context.Books.ToList();
                foreach (var book in books)
                {
                    Console.WriteLine(book.Name);
                }

                Console.ReadKey();
            }
            else if (choice == 2)
            {
                string name;
                int page;
                int yearPress;
                int idThemes;
                int idCategory;
                int idAuthor;
                int idPress;
                string? comment;
                int quantity;

                Console.Write("Enter name : ");
                name = Console.ReadLine()!;
                Console.Write("Enter count of page :");
                page = int.Parse(Console.ReadLine()!);
                Console.Write("Enter year of Press :");
                yearPress = int.Parse(Console.ReadLine()!);
                Console.Write("Enter id of theme :");
                idThemes = int.Parse(Console.ReadLine()!);
                Console.Write("Enter id of category :");
                idCategory = int.Parse(Console.ReadLine()!);
                Console.Write("Enter id of author :");
                idAuthor = int.Parse(Console.ReadLine()!);
                Console.Write("Enter id of press :");
                idPress = int.Parse(Console.ReadLine()!);
                Console.Write("Enter comment : ");
                comment = Console.ReadLine()!;
                Console.Write("Enter quantity :");
                quantity = int.Parse(Console.ReadLine()!);

                var books = context.Books.ToList();
                int lastId = books.Last().Id;

                var book = new Book
                {
                    Id = lastId + 1,
                    Name = name,
                    Pages = page,
                    YearPress = yearPress,
                    IdThemes = idThemes,
                    IdCategory = idCategory,
                    IdAuthor = idAuthor,
                    IdPress = idPress,
                    Comment = comment,
                    Quantity = quantity
                };

                context.Books.Add(book);
                context.SaveChanges();

                Console.WriteLine("Book is added");
                Thread.Sleep(2000);
            }
            else if (choice == 3)
            {
                int id;
                Console.WriteLine("Enter ID : ");
                id = int.Parse(Console.ReadLine()!);
                var book = context.Books.FirstOrDefault(b => b.Id == id);

                Console.Write("Write a new comment : ");
                string comment = Console.ReadLine()!;

                Console.Write("Enter the new quantity : ");
                int quantity = int.Parse(Console.ReadLine()!);

                book!.Comment = comment;
                book!.Quantity = quantity;

                context.Books.Update(book);
                context.SaveChanges();

                Console.WriteLine("Book is updated");
                Thread.Sleep(2000);
            }
            else if (choice == 4)
            {
                removeZone:
                int id;
                Console.Write("Enter ID : ");
                id = int.Parse(Console.ReadLine()!);
                var book = context.Books.FirstOrDefault(b => b.Id == id);

                var sc = context.SCards.FirstOrDefault(b => b.IdBook == id);
                var tc = context.TCards.FirstOrDefault(b => b.IdBook == id);

                if(sc is null && tc is null)
                {
                    context.Books.Remove(book!);
                    context.SaveChanges();
                }
                else
                {
                    Console.WriteLine("You can't remove this book because it's taken");
                    Thread.Sleep(2000);
                    goto removeZone;
                }
                Console.WriteLine("Book is removed");
                Thread.Sleep(2000);
            }
            else
            {
                throw new Exception("Enter again!(1,2,3,4)");
            }
        }

        else
        {
            throw new Exception("This id doesn't exist in this lib");
        }

    }
    catch (Exception ex)
    {

        Console.WriteLine(ex.Message);
        Thread.Sleep(2000);
        goto start;
    }

}



