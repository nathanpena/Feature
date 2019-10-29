using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Features.Models
{
    public class File
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
        public virtual User User { get; set; }
    }
}