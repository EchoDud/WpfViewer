using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfViewer.Models;

namespace WpfViewer.Services
{
    public class ModelService : IModelService
    {
        private readonly IMongoCollection<Model> _modelsCollection;

        public ModelService(string connectionString, string databaseName)
        {
            var client = new MongoClient(connectionString);
            var database = client.GetDatabase(databaseName);
            _modelsCollection = database.GetCollection<Model>("models");
        }

        public void AddModel(Model model)
        {
            throw new NotImplementedException();
        }

        public void DeleteModel(string modelId)
        {
            throw new NotImplementedException();
        }

        public void ExportModel(string modelId, string destinationPath)
        {
            throw new NotImplementedException();
        }

        public Model GetModel(string modelId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Model> GetModels()
        {
            throw new NotImplementedException();
        }

        public void UpdateModel(Model model)
        {
            throw new NotImplementedException();
        }
    }
}
