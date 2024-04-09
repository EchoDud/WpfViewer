using Prism.Commands;
using Prism.Mvvm;
using System.Collections.ObjectModel;
using Microsoft.Win32;
using System.IO;
using System.Windows;
using WpfViewer.Models;
using HelixToolkit.Wpf;
using System.Windows.Media.Media3D;
using System.Windows.Media;

namespace WpfViewer.ViewModels
{
    public class MainViewModel : BindableBase
    {
        private Repository _selectedProject;
        private ModelItem _selectedModel;
        private Model3D _currentModel3D;

        public ObservableCollection<Repository> Projects { get; } = new ObservableCollection<Repository>();

        public Repository SelectedProject
        {
            get => _selectedProject;
            set => SetProperty(ref _selectedProject, value);
        }

        public ModelItem SelectedModel
        {
            get => _selectedModel;
            set
            {
                if (SetProperty(ref _selectedModel, value))
                {
                    LoadModel();
                }
            }
        }
        public Model3D CurrentModel3D
        {
            get => _currentModel3D;
            set => SetProperty(ref _currentModel3D, value);
        }

        public DelegateCommand CreateRepositoryCommand { get; }
        public DelegateCommand AddModelCommand { get; }
        public DelegateCommand RemoveModelCommand { get; }

        public MainViewModel()
        {
            CreateRepositoryCommand = new DelegateCommand(CreateRepository);
            AddModelCommand = new DelegateCommand(AddModel, CanAddModel).ObservesProperty(() => SelectedProject);
            RemoveModelCommand = new DelegateCommand(RemoveModel, CanRemoveModel).ObservesProperty(() => SelectedModel);
        }

        private void CreateRepository()
        {
            var dialog = new SaveFileDialog
            {
                Title = "Создате репозиторий",
                FileName = "Новая папка",               
                CheckFileExists = false,
                CheckPathExists = false,
                ValidateNames = false
            };

            if (dialog.ShowDialog() == true)
            {
                var path = Path.GetFullPath(dialog.FileName);

                // Возможно, нужна дополнительная проверка здесь, чтобы убедиться, что path является допустимым
                if (!string.IsNullOrWhiteSpace(path))
                {
                    TryCreateRepositoryAt(path);
                }
            }
        }


        private void TryCreateRepositoryAt(string path)
        {
            // Проверяем, существует ли директория
            if (Directory.Exists(path))
            {
                MessageBox.Show("Repository already exists.");
            }
            else
            {
                // Если директория не существует, создаем ее и репозиторий
                var newRepository = new Repository { Name = Path.GetFileName(path), Path = path };
                Projects.Add(newRepository);
            }
        }

        private void AddModel()
        {
            var dialog = new OpenFileDialog
            {
                Filter = "3D Model Files|*.obj;*.stl;*.3ds", // Adjust the filter based on your application's needs
                Title = "Select a 3D model file"
            };

            if (dialog.ShowDialog() == true)
            {
                var modelItem = new ModelItem { Name = Path.GetFileName(dialog.FileName), Path = dialog.FileName };
                SelectedProject?.Models.Add(modelItem);
            }
        }

        private bool CanAddModel() => SelectedProject != null;

        private void RemoveModel()
        {
            if (SelectedModel != null)
            {
                SelectedProject?.Models.Remove(SelectedModel);
                SelectedModel = null; // Clear the selection
            }
        }

        private bool CanRemoveModel() => SelectedModel != null;

        private void LoadModel()
        {
            if (SelectedModel != null && File.Exists(SelectedModel.Path))
            {
                try
                {
                    var modelImporter = new ModelImporter();
                    modelImporter.DefaultMaterial = new DiffuseMaterial(new SolidColorBrush(Colors.LightGray));
                    CurrentModel3D = modelImporter.Load(SelectedModel.Path);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Failed to load model: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    CurrentModel3D = null;
                }
            }
            else
            {
                CurrentModel3D = null;
            }
        }
    }
}
