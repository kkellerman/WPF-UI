using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Windows;
using System;
using System.Windows;

namespace WPF_UI
{
    public partial class MainWindow : Window
    {
        private const string WinAppDriverUrl = "http://127.0.0.1:4723";
        private const string ApplicationPath = @"C:\Program Files\Notepad++\notepad++.exe";
        private WindowsDriver<WindowsElement> _session;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnStartAutomation_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Start the WinAppDriver session to automate Notepad++
                var appiumOptions = new AppiumOptions();
                appiumOptions.AddAdditionalCapability("app", @"C:\Program Files\Notepad++\notepad++.exe");
                _session = new WindowsDriver<WindowsElement>(new Uri(WinAppDriverUrl), appiumOptions);

                // Give Notepad++ time to open
                Thread.Sleep(2000);

                // Create a new file (File -> New)
                _session.Keyboard.PressKey(OpenQA.Selenium.Keys.Control + "n");
                _session.Keyboard.ReleaseKey(OpenQA.Selenium.Keys.Control + "n");

                // Give time for the new file to be created
                Thread.Sleep(1000);

                // Find the editor area and type "Hello World"
                var editor = _session.FindElementByClassName("Scintilla");
                editor.SendKeys("Hello World");

                MessageBox.Show("Notepad++ automation completed successfully!");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Automation failed: {ex.Message}");
            }
            finally
            {
                _session?.Quit();
            }
        }
    }
}