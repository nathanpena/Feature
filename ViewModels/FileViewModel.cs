using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Features.ViewModels
{
    public class FileViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
        public string UserName { get; set; }
    }
}