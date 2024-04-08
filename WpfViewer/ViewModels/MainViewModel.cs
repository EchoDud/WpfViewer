using HelixToolkit.Wpf;
using Microsoft.Win32;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media.Media3D;
using System.Windows.Media;

namespace WpfViewer.ViewModels
{
    public class MainViewModel : BindableBase
    {
        private Model3D _modelContent;
        public Model3D ModelContent
        {
            get { return _modelContent; }
            set { SetProperty(ref _modelContent, value); }
        }

        public ICommand LoadModelCommand { get; }

        public MainViewModel()
        {
            LoadModelCommand = new DelegateCommand(OnLoadModel);
        }

        private void OnLoadModel()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                ModelContent = Display3d(openFileDialog.FileName);
            }
        }

        private Model3D Display3d(string modelPath)
        {
            Model3D device = null;
            try
            {
                ModelImporter import = new ModelImporter();
                import.DefaultMaterial = new DiffuseMaterial(new SolidColorBrush(Colors.LightGray));
                device = import.Load(modelPath);
            }
            catch (Exception e)
            {
                System.Windows.MessageBox.Show("Exception Error : " + e.StackTrace);
            }
            return device;
        }
    }
}
