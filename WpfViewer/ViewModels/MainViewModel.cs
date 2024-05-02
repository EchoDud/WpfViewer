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
using System.Windows.Input;
using FileVersionControl.Core.DTOs.FileStorageDTOs;
using FileVersionControl.Core.Services;
using MongoDB.Bson;

namespace WpfViewer.ViewModels
{
    public class MainViewModel : BindableBase
    {
        private readonly FileService _fileService;
        private Project _selectedProject;
        private Model _selectedModel;
        private ObservableCollection<Project> _projects = new ObservableCollection<Project>();

        public MainViewModel(FileService fileService)
        {
            _fileService = fileService;
            CreateProjectCommand = new DelegateCommand(CreateProject);
            DeleteProjectCommand = new DelegateCommand(DeleteProject, () => SelectedProject != null).ObservesProperty(() => SelectedProject);
            AddModelCommand = new DelegateCommand(AddModel, () => SelectedProject != null).ObservesProperty(() => SelectedProject);
            RemoveModelCommand = new DelegateCommand(RemoveModel, () => SelectedModel != null).ObservesProperty(() => SelectedModel);
            UpdateModelCommand = new DelegateCommand(UpdateModel, () => SelectedModel != null).ObservesProperty(() => SelectedModel);
        }

        public ICommand CreateProjectCommand { get; }
        public ICommand DeleteProjectCommand { get; }
        public ICommand AddModelCommand { get; }
        public ICommand RemoveModelCommand { get; }
        public ICommand UpdateModelCommand { get; }

        public ObservableCollection<Project> Projects
        {
            get { return _projects; }
            set { SetProperty(ref _projects, value); }
        }

        public Project SelectedProject
        {
            get { return _selectedProject; }
            set { SetProperty(ref _selectedProject, value); }
        }

        public Model SelectedModel
        {
            get { return _selectedModel; }
            set { SetProperty(ref _selectedModel, value); }
        }

        private void CreateProject()
        {
            var projectName = Microsoft.VisualBasic.Interaction.InputBox("Enter new project name:", "New Project", "Default Project");
            if (!string.IsNullOrWhiteSpace(projectName))
                Projects.Add(new Project { Name = projectName });
        }

        private void DeleteProject()
        {
            if (SelectedProject != null)
                Projects.Remove(SelectedProject);
        }

        private async void AddModel()
        {
            var openFileDialog = new OpenFileDialog { Filter = "Model Files|*.stl" };
            if (openFileDialog.ShowDialog() == true)
            {
                using (var stream = File.OpenRead(openFileDialog.FileName))
                {
                    var uploadFileDTO = new UploadFileDTO
                    {
                        Name = Path.GetFileName(openFileDialog.FileName),
                        Type = "model",
                        Owner = "User", // Update this as per your user management system
                        Project = SelectedProject.Name,
                        Description = "Added via application",
                        Stream = stream
                    };
                    var objectId = await _fileService.UploadFileAsync(uploadFileDTO);
                    SelectedProject.Models.Add(new Model
                    {
                        Name = Path.GetFileName(openFileDialog.FileName),
                        FileType = "model",
                        Owner = "User",
                        Project = SelectedProject.Name,
                        VersionNumber = 1, // This should ideally be fetched from the service after upload
                        Description = "Added via application"
                    });
                }
            }
        }

        private async void RemoveModel()
        {
            if (SelectedModel != null)
            {
                var deleteFileDTO = new DeleteFileDTO
                {
                    Name = SelectedModel.Name,
                    Owner = SelectedModel.Owner,
                    Project = SelectedModel.Project
                };
                await _fileService.DeleteFileAsync(deleteFileDTO);
                SelectedProject.Models.Remove(SelectedModel);
            }
        }

        private async void UpdateModel()
        {
            var openFileDialog = new OpenFileDialog { Filter = "Model Files|*.stl" };
            if (openFileDialog.ShowDialog() == true)
            {
                using (var stream = File.OpenRead(openFileDialog.FileName))
                {
                    var uploadFileDTO = new UploadFileDTO
                    {
                        Name = Path.GetFileName(openFileDialog.FileName),
                        Type = "model",
                        Owner = "User",
                        Project = SelectedProject.Name,
                        Description = "Updated via application",
                        Stream = stream
                    };
                    var objectId = await _fileService.UploadFileAsync(uploadFileDTO);
                    SelectedModel.Name = Path.GetFileName(openFileDialog.FileName);
                    SelectedModel.FileType = "model";
                    SelectedModel.Owner = "User";
                    SelectedModel.Project = SelectedProject.Name;
                    SelectedModel.VersionNumber = 1; // This should be updated to the new version
                    SelectedModel.Description = "Updated via application";
                }
            }
        }
    }
}