using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfViewer.Models;

namespace WpfViewer.Services
{
    public interface IModelService
    {
        IEnumerable<Model> GetModels();
        void AddModel(Model model);
        void DeleteModel(string modelId);
        Model GetModel(string modelId);
        void UpdateModel(Model model);
        void ExportModel(string modelId, string destinationPath);//Здесь я думая можно возвращать файл для его сохранения, но это под вопросом
    }
}
