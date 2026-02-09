using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace LibraryApp
{
    public class AuthService
    {
        private readonly LibraryContext _context;

        public AuthService(LibraryContext context)
        {
            _context = context;
        }

        public AppUser Login(string login, string password)
        {
            return _context.Users
                .Include(u => u.Role)
                .FirstOrDefault(u => u.Login == login && u.Password == password);
        }

        public bool Register(string login, string password)
        {
            if (_context.Users.Any(u => u.Login == login))
                return false;

            _context.Users.Add(new AppUser
            {
                Login = login,
                Password = password,
                RoleId = 2
            });

            _context.SaveChanges();
            return true;
        }
    }
}