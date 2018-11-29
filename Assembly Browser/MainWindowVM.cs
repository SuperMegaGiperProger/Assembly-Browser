using System.Collections.ObjectModel;
using System.Windows.Input;
using AssemblyStructure.Models;
using Assembly_Browser.Commands;
using Microsoft.Win32;

namespace Assembly_Browser
{
    public class MainWindowVM : BaseVM
    {
        public ICommand OpenFileDialogCommand { get; }

        private ObservableCollection<NamespaceDescription> _namespaces;

        public ObservableCollection<NamespaceDescription> Namespaces
        {
            get { return _namespaces; }
            set
            {
                _namespaces = value;
                OnPropertyChanged(nameof(Namespaces));
            }
        }

        public MainWindowVM()
        {
            OpenFileDialogCommand = new RelayCommand(o => OpenFileDialogCommandBody(), o => true);
        }

        private void OpenFileDialogCommandBody()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            Namespaces?.Clear();
            
            if (openFileDialog.ShowDialog() == true)
            {
                NamespaceDescription[] namespaceDescriptions =
                    new AssemblyStructure.AssemblyStructure(openFileDialog.FileName).NamespaceDescriptions;
                
                Namespaces = new ObservableCollection<NamespaceDescription>(namespaceDescriptions);
            }
        }
    }
}