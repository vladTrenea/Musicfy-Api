using System.Collections.Generic;

namespace Musicfy.Bll.Models
{
    public class PaginationModel<T>
    {
        public int Page { get; set; }

        public IEnumerable<T> Items { get; set; }
    }
}