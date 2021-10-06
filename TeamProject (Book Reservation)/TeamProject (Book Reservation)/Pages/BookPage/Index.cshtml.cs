using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DAL;
using DAL.Entities;

namespace TeamProject__Book_Reservation_.Pages.BookPage
{
    public class IndexModel : PageModel
    {
        private readonly DAL.BookRentalDbContext _context;

        public IndexModel(DAL.BookRentalDbContext context)
        {
            _context = context;
        }

        public IList<Book> Book { get;set; }

        public async Task OnGetAsync()
        {
            Book = await _context.BookTemplates.ToListAsync();
        }
    }
}
