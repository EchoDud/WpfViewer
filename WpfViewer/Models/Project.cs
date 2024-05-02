using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfViewer.Models
{
    public class Project
    {
        public string Name { get; set; }
        public ObservableCollection<Model> Models { get; set; } = new ObservableCollection<Model>();
    }
}
