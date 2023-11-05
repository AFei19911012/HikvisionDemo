using System.IO;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;

namespace HikvisionDemo
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public BitmapSource Source
        {
            get { return (BitmapSource)GetValue(SourceProperty); }
            set { SetValue(SourceProperty, value); }
        }
        public static readonly DependencyProperty SourceProperty =
            DependencyProperty.Register("Source", typeof(BitmapSource), typeof(MainWindow), new PropertyMetadata(null));



        public MainWindow()
        {
            InitializeComponent();

            Init();
        }

        private void Init()
        {
            Task.Run(() =>
            {
                while (true)
                {
                    var request = WebRequest.Create("http://192.168.1.64:80/ISAPI/Streaming/channels/101/picture");
                    request.Method = "GET";
                    request.Credentials = new NetworkCredential("admin", "Big Wang");
                    using (WebResponse webRes = request.GetResponse())
                    {
                        int length = (int)webRes.ContentLength;
                        HttpWebResponse response = webRes as HttpWebResponse;
                        Stream stream = response.GetResponseStream();
                        MemoryStream MemoryStream = new MemoryStream();
                        byte[] buffer = new byte[length];
                        int i;
                        while ((i = stream.Read(buffer, 0, buffer.Length)) > 0)
                        {
                            MemoryStream.Write(buffer, 0, i);
                        }
                        byte[] data = MemoryStream.ToArray();
                        MemoryStream.Close();

                        Dispatcher.Invoke(() => { Source = BytesToBitmapImage(data); });
                    }
                    Thread.Sleep(10);
                }
            });
        }

        private BitmapImage BytesToBitmapImage(byte[] bytes)
        {
            using (MemoryStream ms = new MemoryStream(bytes))
            {
                BitmapImage result = new BitmapImage();
                result.BeginInit();
                result.StreamSource = ms;
                result.CacheOption = BitmapCacheOption.OnLoad;
                result.EndInit();
                result.Freeze();
                return result;
            }
        }
    }
}