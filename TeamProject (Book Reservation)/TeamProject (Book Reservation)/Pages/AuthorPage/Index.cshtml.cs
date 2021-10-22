using DAL.Entities;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TeamProject__Book_Reservation_.Pages.AuthorPage
{
    public class IndexModel : PageModel
    {
        private readonly DAL.BookRentalDbContext _context;

        public IndexModel(DAL.BookRentalDbContext context)
        {
            _context = context;
        }

        public IList<Author> Author { get; set; }

        public async Task OnGetAsync()
        {
            Author = await _context.Authors.ToListAsync();
        }
    }
}
