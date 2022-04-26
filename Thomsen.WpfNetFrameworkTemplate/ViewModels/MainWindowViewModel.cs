using System.Threading.Tasks;
using System.Windows.Input;

using Thomsen.WpfTools.Mvvm;

namespace Thomsen.WpfNetFrameworkTemplate.ViewModels {
    internal class MainWindowViewModel : BaseViewModel {
        #region Private Fields
        #region Commands
        private ICommand _testCmd;
        #endregion Commands
        #endregion Private Fields

        #region Public Properties
        #region Commands
        public ICommand TestCmd => _testCmd ??= new CommandHandler(async () => await TestAsync(), () => true);
        #endregion Commands
        #endregion Public Properties

        #region Private Methods
        private async Task TestAsync() {
            await Task.Delay(1000);
        }
        #endregion Private Methods
    }
}
