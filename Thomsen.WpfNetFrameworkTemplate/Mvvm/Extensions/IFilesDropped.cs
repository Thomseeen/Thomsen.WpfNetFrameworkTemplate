using System.Threading.Tasks;

namespace Thomsen.WpfTools.Mvvm.Extensions {
    internal interface IFilesDropped {
        public Task OnFilesDroppedAsync(string[] files);
    }
}
