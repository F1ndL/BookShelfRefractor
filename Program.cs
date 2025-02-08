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
        library.Run();
    }
}

class Library
{
    private List<Book> books = new List<Book>();
    private const string FilePath = "books.json";

    public Library()
    {
        LoadBooks();
    }

    public void Run()
    {
        while (true)
        {
            Console.WriteLine("1. Додати книгу\n2. Показати книги\n3. Вихiд");
            string choice = Console.ReadLine();
            if (choice == "1") AddBook();
            else if (choice == "2") ShowBooks();
            else if (choice == "3") break;
        }
    }

    private void AddBook()
    {
        Console.Write("Введiть назву книги: ");
        string title = Console.ReadLine();
        Console.Write("Введiть автора книги: ");
        string author = Console.ReadLine();
        books.Add(new Book(title, author));
        SaveBooks();
    }

    private void ShowBooks()
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
}

class Book
{
    public string Title { get; set; }
    public string Author { get; set; }

    public Book() { } // Безпараметричний конструктор для десерiалiзацiї

    public Book(string t, string a)
    {
        Title = t;
        Author = a;
    }
}
