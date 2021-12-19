using BL.DTOs.Enums;
using System;

namespace MVCProject.StateManager.FilterStates
{
    public class BookFilterState : FilterState
    {
        public BookFilterState()
        {
            Title = null;
            AuthorName = null;
            AuthorSurname = null;
            Genres = null;
            Language = null;
            PageFrom = null;
            PageTo = null;
            ReleaseFrom = null;
        }

        public string Title { get; set; }
        public string AuthorName { get; set; }
        public string AuthorSurname { get; set; }
        public GenreTypeDTO[] Genres { get; set; }
        public LanguageDTO? Language { get; set; }
        public int? PageFrom { get; set; }
        public int? PageTo { get; set; }
        public DateTime? ReleaseFrom { get; set; }
        public DateTime? ReleaseTo { get; set; }
    }
}
