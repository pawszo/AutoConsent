using AutoConsent.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AutoConsent
{
    public partial class Window : Form
    {
        private readonly IRepository _repository;
        public Controller Controller { get; set; }
        public Window(IRepository repository) : base()
        {
            InitializeComponent();
            _repository = repository;
        }

        private void browseButton_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            
            var result = dialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                string directory = dialog.SelectedPath;
                directoryBox.Text = directory;
            }

        }

        private void generateButton_Click(object sender, EventArgs e)
        {
            _repository.Items.Add(Constants.Keys.Directory.ToString(), directoryBox.Text);
            this.Visible = false;
            Controller.Run();
            this.Close();
        }
    }
}
