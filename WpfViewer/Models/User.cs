using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfViewer.Models
{
    public class User
    {
        public string Username { get; set; }
        public List<Project> Projects { get; set; } = new List<Project>();
    }
}
