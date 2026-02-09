using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Windows;

namespace LibraryApp
{
    public partial class MainWindow : Window
    {
        private readonly LibraryContext _context;
        private AppUser _user;

        public MainWindow()
        {
            // Окно логина до инициализации интерфейса
            var login = new LoginWindow();
            bool? result = login.ShowDialog();

            if (result != true || login.CurrentUser == null)
            {
                Application.Current.Shutdown();
                return;
            }

            _user = login.CurrentUser;

            InitializeComponent();
            _context = new LibraryContext();

            UserInfo.Text = $"Вы вошли как: {_user.Login} ({_user.Role.Name})";

            LoadData();
        }

        private void LoadData()
        {
            ReadersGrid.ItemsSource = _context.Readers.ToList();
            BooksGrid.ItemsSource = _context.Books.ToList();

            BorrowedGrid.ItemsSource = _context.BorrowedBooks
                .Include(b => b.Book)
                .Include(b => b.Reader)
                .ToList();
        }
        private void SaveChanges_Click(object sender, RoutedEventArgs e)
        {
            if (_user.Role.Name == "Admin")
            {
                try
                {
                    _context.SaveChanges();
                    LoadData();
                    MessageBox.Show("Изменения сохранены.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка при сохранении: " + ex.Message);
                }
            }
            else { MessageBox.Show("Нет прав"); }
        }

        // ---------------- ЧИТАТЕЛИ ----------------

        private void AddReader(object sender, RoutedEventArgs e)
        {
            if (_user.Role.Name == "Admin")
            {
                var r = new Reader
                {
                    FirstName = "Имя",
                    LastName = "Фамилия"
                };

                _context.Readers.Add(r);
                _context.SaveChanges();
                LoadData();
            }
            else { MessageBox.Show("Нет прав"); }
        }

        private void EditReader(object sender, RoutedEventArgs e)
        {
            if (_user.Role.Name == "Admin")
            {
                if (ReadersGrid.SelectedItem is Reader selected)
                {
                    var r = _context.Readers.First(x => x.Id == selected.Id);

                    r.FirstName = "НовоеИмя";
                    r.LastName = "НоваяФамилия";

                    _context.SaveChanges();
                    LoadData();
                }
            }
            else { MessageBox.Show("Нет прав"); }
        }

        private void DeleteReader(object sender, RoutedEventArgs e)
        {
            if (_user.Role.Name == "Admin")
            {
                if (ReadersGrid.SelectedItem is Reader selected)
                {
                    var r = _context.Readers.First(x => x.Id == selected.Id);

                    _context.Readers.Remove(r);
                    _context.SaveChanges();
                    LoadData();
                }
            }
            else { MessageBox.Show("Нет прав"); }
        }

        // ---------------- КНИГИ ----------------

        private void AddBook(object sender, RoutedEventArgs e)
        {
            if (_user.Role.Name == "Admin")
            {
                var b = new Book
                {
                    Title = "Новая книга",
                    Author = "Автор"
                };

                _context.Books.Add(b);
                _context.SaveChanges();
                LoadData();
            }
            else { MessageBox.Show("Нет прав"); }
        }

        private void EditBook(object sender, RoutedEventArgs e)
        {
            if (_user.Role.Name == "Admin")
            {
                if (BooksGrid.SelectedItem is Book selected)
                {
                    var b = _context.Books.First(x => x.Id == selected.Id);

                    b.Title = "Новое название";
                    b.Author = "Новый автор";

                    _context.SaveChanges();
                    LoadData();
                }
            }
            else { MessageBox.Show("Нет прав"); }
        }

        private void DeleteBook(object sender, RoutedEventArgs e)
        {
            if (_user.Role.Name == "Admin")
            {
                
                    if (BooksGrid.SelectedItem is Book selected)
                    {
                        var b = _context.Books.First(x => x.Id == selected.Id);

                        _context.Books.Remove(b);
                        _context.SaveChanges();
                        LoadData();
                    }
                }
            else { MessageBox.Show("Нет прав"); }
        }

        // ---------------- ВЗЯТЫЕ КНИГИ ----------------

        private void AddBorrow(object sender, RoutedEventArgs e)
        {
            if (_user.Role.Name == "Admin")
            {

                var book = _context.Books.FirstOrDefault();
                var reader = _context.Readers.FirstOrDefault();

                if (book == null || reader == null)
                {
                    MessageBox.Show("Нет книг или читателей.");
                    return;
                }

                var bb = new BorrowedBook
                {
                    BookId = book.Id,
                    ReaderId = reader.Id,
                    DateTaken = DateTime.Now,
                    DateReturned = null
                };

                _context.BorrowedBooks.Add(bb);
                _context.SaveChanges();
                LoadData();
            }
            else { MessageBox.Show("Нет прав"); }
        }

        private void EditBorrow(object sender, RoutedEventArgs e)
        {
            if (_user.Role.Name == "Admin")
            {
                if (BorrowedGrid.SelectedItem is BorrowedBook selected)
                {
                    var bb = _context.BorrowedBooks.First(x => x.Id == selected.Id);

                    bb.DateReturned = DateTime.Now;

                    _context.SaveChanges();
                    LoadData();
                }
            }
            else { MessageBox.Show("Нет прав"); }
        }

        private void DeleteBorrow(object sender, RoutedEventArgs e)
        {
            if (_user.Role.Name == "Admin")
            {
                if (BorrowedGrid.SelectedItem is BorrowedBook selected)
                {
                    var bb = _context.BorrowedBooks.First(x => x.Id == selected.Id);

                    _context.BorrowedBooks.Remove(bb);
                    _context.SaveChanges();
                    LoadData();
                }
            }
            else { MessageBox.Show("Нет прав"); }
        }
        }
}