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
using System.Windows.Shapes;
using System.Windows.Threading;

namespace QrWebCam.Demo
{
  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow : Window
  {
    public MainWindow()
    {
      InitializeComponent();

    }

    private void camSelect_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
      webCam.CameraIndex = camSelect.SelectedIndex;
      webCam.StartDetection();
    }

    DispatcherTimer mDispatcherTimer;

    private void QrWebCamControl_QrDecoded(object sender, string e)
    {
  
      dtext.Text = "The decoded string is: " + e;
      MessageBox.Show(e);
      dtext.Text = "";
      //webCam.Restart();
      mDispatcherTimer.Start();
    }

    private void Window_Loaded(object sender, RoutedEventArgs e)
    {
       mDispatcherTimer = new DispatcherTimer();
       camSelect.ItemsSource = webCam.CameraNames;
       mDispatcherTimer.Tick += new EventHandler(DispatcherTimer_Tick);
       mDispatcherTimer.Interval = new TimeSpan(0, 0, 1);
    }
    private void DispatcherTimer_Tick(object sender, EventArgs e)
    {
      mDispatcherTimer.Stop();
      webCam.Restart();
    }

    private void Image_Loaded(object sender, RoutedEventArgs e)
    {
      // ... Create a new BitmapImage.
      BitmapImage b = new BitmapImage();
      var wCurrentLocation = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
      var wImagePath = System.IO.Path.Combine(wCurrentLocation, "image.png");
      b.BeginInit();
      b.UriSource = new Uri(wImagePath);
      b.EndInit();

      // ... Get Image reference from sender.
      var image = sender as Image;
      // ... Assign Source.
      image.Source = b;
    }

    private void restart_Click(object sender, RoutedEventArgs e)
    {
      webCam.Restart();
    }
  }
}
