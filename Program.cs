using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

class Program
{
    static void Main()
    {
        Library library = new Library();
        CommandProcessor processor = new CommandProcessor(library);
        processor.Run();
    }
}

class CommandProcessor
{
    private readonly Library _library;

    public CommandProcessor(Library library)
    {
        _library = library;
    }

    enum Command { AddBook = 1, ShowBooks, Exit }

public void Run()
{
    while (true)
    {
        Console.WriteLine($"{(int)Command.AddBook}. Додати книгу");
        Console.WriteLine($"{(int)Command.ShowBooks}. Показати книги");
        Console.WriteLine($"{(int)Command.Exit}. Вихід");

        if (int.TryParse(Console.ReadLine(), out int choice))
        {
            if (choice == (int)Command.AddBook) _library.AddBook();
            else if (choice == (int)Command.ShowBooks) _library.ShowBooks();
            else if (choice == (int)Command.Exit) break;
        }
    }
}


class Library
{
    private IList<Book> books = new List<Book>();
    private const string FilePath = "books.json";

    public Library()
    {
        LoadBooks();
    }

    public void AddBook()
{
    Book newBook = CreateBook();
    books.Add(newBook);
    SaveBooks();
}

private Book CreateBook()
{
    string title = GetUserInput("Введіть назву книги: ");
    string author = GetUserInput("Введіть автора книги: ");
    return new Book(title, author);
}


    public void ShowBooks()
    {
        foreach (var book in books)
        {
            Console.WriteLine($"{book.Title} - {book.Author}");
        }
    }

    private void SaveBooks()
    {
        string json = JsonSerializer.Serialize(books);
        File.WriteAllText(FilePath, json);
    }

    private void LoadBooks()
    {
        if (File.Exists(FilePath))
        {
            string json = File.ReadAllText(FilePath);
            books = JsonSerializer.Deserialize<List<Book>>(json) ?? new List<Book>();
        }
    }

private string GetUserInput(string prompt)
{
    string input;
    do
    {
        Console.Write(prompt);
        input = Console.ReadLine()?.Trim();
    } while (string.IsNullOrEmpty(input));

    return input;
}

}

class Book
{
    public string Title { get; set; }
    public string Author { get; set; }

    public Book() { }

    public Book(string t, string a)
    {
        Title = t;
        Author = a;
    }
}