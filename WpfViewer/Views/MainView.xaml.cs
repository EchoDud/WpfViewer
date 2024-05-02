using FileVersionControl.Core.Repositories;
using FileVersionControl.Core.Services;
using HelixToolkit.Wpf;
using Microsoft.Win32;
using MongoDB.Driver;
using System.Reflection;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfViewer.Services;
using WpfViewer.ViewModels;

namespace WpfViewer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainView : Window
    {
        public MainView()
        {
            InitializeComponent();
            /*IModelService fileService = new ModelService("mongodb://localhost:27017/","Models");*/
            var client = new MongoClient("mongodb://localhost:27017/");
            var databaseName = "Models";
            var fileRepository = new FileRepository(client, databaseName);
            var fileService = new FileService(fileRepository);
            DataContext = new MainViewModel(fileService);
        }
                
    }
}