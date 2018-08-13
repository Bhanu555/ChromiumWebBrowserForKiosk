using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using mshtml;
using CefSharp;
using System.IO;
using CefSharp.Wpf;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
       

        private ChromiumWebBrowser leftBrowser;
        private ChromiumWebBrowser rightBrowser;
        public MainWindow()
        {
            InitializeComponent();
            var settings = new CefSettings() { RemoteDebuggingPort = 8088 };
            {
                //By default CefSharp will use an in-memory cache, you need to specify a Cache Folder to persist data
                var CachePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "CefSharp\\Cache");
            };                   
           settings.CefCommandLineArgs.Add("enable-media-stream", "1");
            Cef.Initialize(settings, performDependencyCheck: true, browserProcessHandler: null);
            //Cef.Initialize(settings);          
            leftBrowser = new ChromiumWebBrowser();
            RootGrid.Children.Add(leftBrowser);
            leftBrowser.SetValue(Grid.ColumnProperty, 0);

            rightBrowser = new ChromiumWebBrowser();
            RootGrid.Children.Add(rightBrowser);
            rightBrowser.SetValue(Grid.ColumnProperty, 1);

            leftBrowser.FrameLoadEnd += LeftBrowser_FrameLoadEnd;
           // leftBrowser.Address = "http://localhost:54352/VideoChat/Fms/TwilioCall.aspx?roomName=";
            leftBrowser.Address = "https://secure.inmatecanteen.com/default.aspx";
        }

        private void LeftBrowser_FrameLoadEnd(object sender, FrameLoadEndEventArgs e)
        {
            Dispatcher.BeginInvoke((Action) (() =>
            { rightBrowser.Address = "localhost:8088";
            }));
        }
    }


}
