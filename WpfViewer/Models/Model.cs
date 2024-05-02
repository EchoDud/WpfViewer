using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfViewer.Models
{
    public class Model
    {
        public string Name { get; set; }
        public string FileType { get; set; }
        public string Owner { get; set; }
        public string Project { get; set; }
        public int VersionNumber { get; set; }
        public string Description { get; set; }
    }
}
