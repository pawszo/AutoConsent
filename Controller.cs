using AutoConsent.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AutoConsent
{
    public class Controller
    {
        private readonly IFileService _fileService;
        private readonly IInputService _inputService;
        private readonly IRepository _repository;

        public void Initialize()
        {
            Window window = new Window(_repository);
            window.Controller = this;
            window.ShowDialog();
        }
        public void Run()
        {
            _inputService.GetRecords();
            _fileService.PrintFile();
        }

        public Controller(IRepository repository, IFileService fileService, IInputService inputService)
        {
            _repository = repository;
            _fileService = fileService;
            _inputService = inputService;
        }
    }
}
