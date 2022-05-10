using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

using Thomsen.WpfNetFrameworkTemplate.ViewModels;
using Thomsen.WpfTools.Util;

namespace Thomsen.WpfNetFrameworkTemplate {
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application {
        #region Private Fields
        private readonly MainWindowViewModel _viewModel = new();
        #endregion Private Fields

        #region Application Overrides
        protected override void OnStartup(StartupEventArgs e) {
            base.OnStartup(e);

            DispatcherUnhandledException += App_DispatcherUnhandledException;
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            TaskScheduler.UnobservedTaskException += TaskScheduler_UnobservedTaskException;

            _viewModel.Show();
        }

        protected override void OnExit(ExitEventArgs e) {
            base.OnExit(e);

            _viewModel.Dispose();
        }
        #endregion Application Overrides

        #region Crash Handling
        private void App_DispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e) {
            WriteCrashDumpFile("App", e.Exception);
        }

        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e) {
            WriteCrashDumpFile("CurrentDomain", (Exception)e.ExceptionObject);
        }

        private void TaskScheduler_UnobservedTaskException(object sender, UnobservedTaskExceptionEventArgs e) {
            WriteCrashDumpFile("TaskScheduler", e.Exception);
        }

        private static void WriteCrashDumpFile(string facility, Exception ex) {
            string path = $"{facility}_{DateTime.Now:yyyy-MM-dd_HH-mm-ss}.dmp";

            using StreamWriter writer = new(path);

            writer.WriteLine($"--- Fatal error in {facility} ---");
            writer.WriteLine($"Time local: {DateTime.Now}");
            writer.WriteLine($"Time UTC: {DateTime.UtcNow}\r\n");
            writer.WriteLine($"Message: {ex.GetAllMessages()}");
            writer.WriteLine($"Source: {ex.Source}");
            writer.WriteLine($"TargetSite: {ex.TargetSite}");
            writer.WriteLine($"Stack Trace:");
            writer.WriteLine($"---");
            writer.Write($"{ex.StackTrace}");
            writer.WriteLine($"---\r\n");
            writer.WriteLine($"Full Exception:");
            writer.WriteLine($"---");
            writer.WriteLine($"{ex}");
            writer.WriteLine($"---");

            writer.Flush();
            writer.Close();
        }
        #endregion Crash Handling
    }
}
