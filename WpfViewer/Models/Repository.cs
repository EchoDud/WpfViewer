using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfViewer.Models
{
    public class Repository
    {
        public string Name { get; set; }
        public string Path { get; set; }
        public ObservableCollection<ModelItem> Models { get; set; } = new ObservableCollection<ModelItem>();
    }

}
