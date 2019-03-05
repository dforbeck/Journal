using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Journal.Models
{
    public class EntryEdit
    {
        public int EntryId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
    }
}
