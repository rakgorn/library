public class Role
{
    public int Id { get; set; }
    public string Name { get; set; }
}

public class AppUser
{
    public int Id { get; set; }
    public string Login { get; set; }
    public string Password { get; set; }
    public int RoleId { get; set; }
    public Role Role { get; set; }
}

public class Reader
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string MiddleName { get; set; }
    public string Phone { get; set; }
}

public class Book
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Author { get; set; }
    public decimal DepositPrice { get; set; }
    public decimal Price { get; set; }
    public string Genre { get; set; }
}

public class BorrowedBook
{
    public int Id { get; set; }
    public int BookId { get; set; }
    public Book Book { get; set; }
    public int ReaderId { get; set; }
    public Reader Reader { get; set; }
    public DateTime DateTaken { get; set; }
    public DateTime? DateReturned { get; set; }
}