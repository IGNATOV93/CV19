using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using CV19.ViewModels.Base;

namespace CV19.ViewModels
{
    class DirectoryViewModel : ViewModel
    {
        private readonly DirectoryInfo _DirectoryInfo;

        public IEnumerable<DirectoryViewModel> SubDirectories=> _DirectoryInfo
            .EnumerateDirectories()
            .Select(dir_info => new DirectoryViewModel(dir_info.FullName));

        public IEnumerable<FileViewModel> Files => _DirectoryInfo
            .EnumerateFiles()
            .Select(file => new FileViewModel(file.FullName));

        public IEnumerable<object> DirectoryItems =>SubDirectories.Cast<object>().Concat(Files);

        public string Name=> _DirectoryInfo.Name;
        public string Path => _DirectoryInfo.FullName;
        public DateTime CreationTime => _DirectoryInfo.CreationTime;
        public DirectoryViewModel(string Path)=>_DirectoryInfo = new DirectoryInfo(Path);
        
    }

    class  FileViewModel : ViewModel
    {
        private FileInfo _FileInfo;

        public string Name => _FileInfo.Name;
        public string Path => _FileInfo.FullName;
        
        public DateTime CreationTime => _FileInfo.CreationTime;
        public FileViewModel(string Path) => _FileInfo = new FileInfo(Path);
    }
}
