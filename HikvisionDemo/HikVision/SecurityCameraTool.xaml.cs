using System.Windows;
using System.Windows.Controls;
using HikvisionDemo.LogWpf;

namespace HikvisionDemo.HikVision
{
    /// <summary>
    /// SecurityCameraTool.xaml 的交互逻辑
    /// </summary>
    public partial class SecurityCameraTool : UserControl
    {
        #region 委托和事件 打印日志消息
        // 声明一个委托
        public delegate void LogEventHandler(string info, EnumLogType type);
        // 声明一个事件
        public event LogEventHandler LogEvent;
        // 触发事件
        protected virtual void PrintLog(string info, EnumLogType type)
        {
            LogEvent?.Invoke(string.Format("[{0}] {1}", GetType().Name, info), type);
        }
        #endregion

        private SecurityCameraVM VM { get; set; }

        public SecurityCameraTool()
        {
            InitializeComponent();

            // 日志委托
            VM = DataContext as SecurityCameraVM;
            VM.LogEvent += PrintLog;
        }


        public void ErrorIdEvent()
        {
            if (VM.ErrId == -1)
            {
                PrintLog("取流失败", EnumLogType.Error);
            }
            else if (VM.ErrId > 0)
            {
                PrintLog("错误码：" + VM.ErrId, EnumLogType.Error);
            }
            else
            {
                PrintLog("正常取流", EnumLogType.Info);
            }
        }


        public void CaptureImage(int idx, string filename)
        {
            VM.CaptureImage(idx, filename);
        }

        public void StartRecord(string filename, int idx)
        {
            _ = VM.StartRecord(filename, idx);
        }

        public void StopRecord(int idx)
        {
            _ = VM.StopRecord(idx);
        }


        public void Stop()
        {
            VM.Stop();
        }

        public void Dispose()
        {
            VM.Dispose();
        }


        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int idx = LB_Device.SelectedIndex;
            if (idx >= 0)
            {
                // 显示当前设备信息
                SecurityCameraVM vm = DataContext as SecurityCameraVM;
                (MySecurityCameraInfoControl.DataContext as SecurityCameraInfoVM).SetParams(vm.ListCameraInfos[idx]);

                // 标准窗口模式下查看大图
                if (vm.IntRows == 1)
                {
                    for (int i = 0; i < MyVideoContainer.Children.Count; i++)
                    {
                        MyVideoContainer.Children[i].Visibility = Visibility.Collapsed;
                        if (i == idx)
                        {
                            MyVideoContainer.Children[i].Visibility = Visibility.Visible;
                        }
                    }
                }
            }
        }

        private void TextBlock_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            ListBox_SelectionChanged(null, null);
        }
    }
}
