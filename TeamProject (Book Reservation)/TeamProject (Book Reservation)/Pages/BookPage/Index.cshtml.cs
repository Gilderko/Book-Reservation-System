using DAL.Entities;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TeamProject__Book_Reservation_.Pages.BookPage
{
    public class IndexModel : PageModel
    {
        private readonly DAL.BookRentalDbContext _context;

        public IndexModel(DAL.BookRentalDbContext context)
        {
            _context = context;
        }

        public IList<Book> Book { get; set; }

        public async Task OnGetAsync()
        {
            Book = await _context.BookTemplates.ToListAsync();
        }
    }
}
