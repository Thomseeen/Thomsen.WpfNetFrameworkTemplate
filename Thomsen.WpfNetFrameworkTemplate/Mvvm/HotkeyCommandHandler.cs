using System;
using System.Globalization;
using System.Windows.Input;

namespace Thomsen.WpfTools.Mvvm {
    internal class HotkeyCommandHandler : CommandHandler {
        #region Private Fields
        private readonly KeyGesture _gesture;
        #endregion Private Fields

        #region Properties
        public string GestureText => _gesture.GetDisplayStringForCulture(CultureInfo.CurrentUICulture);

        public KeyGesture Gesture => _gesture;
        #endregion Properties

        #region Constructors
        /// <summary>
        /// Creates instance of the command handler
        /// </summary>
        /// <param name="action">Action to be executed by the command</param>
        /// <param name="canExecute">A bolean property to containing current permissions to execute the command</param>
        public HotkeyCommandHandler(Action<object> action, Func<bool> canExecute, KeyGesture gesture) : base(action, canExecute) {
            _gesture = gesture;
        }
        #endregion Constructors
    }
}
