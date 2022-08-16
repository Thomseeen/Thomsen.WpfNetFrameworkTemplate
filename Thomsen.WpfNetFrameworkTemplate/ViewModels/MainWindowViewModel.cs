using System.Threading.Tasks;
using System.Windows.Input;

using Thomsen.WpfNetFrameworkTemplate.Views;
using Thomsen.WpfTools.Mvvm;
using Thomsen.WpfTools.Util;

namespace Thomsen.WpfNetFrameworkTemplate.ViewModels {
    internal class MainWindowViewModel : BaseWindowViewModel<MainWindowView> {
        #region Private Fields
        #region Commands
        private ICommand _testCmd;
        #endregion Commands
        #endregion Private Fields

        #region Public Properties
        #region Commands
        public ICommand TestCmd => _testCmd ??= new CommandHandler(async param => await TestAsync(), () => true);
        #endregion Commands
        #endregion Public Properties

        #region Private Methods
        private async Task TestAsync() {
            using WaitCursor _ = new();

            await Task.Delay(1000);
        }
        #endregion Private Methods
    }
}
