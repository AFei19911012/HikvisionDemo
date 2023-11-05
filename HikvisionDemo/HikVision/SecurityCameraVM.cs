using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using HikvisionDemo.Helper;
using HikvisionDemo.LogWpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Forms;
using System.Xml.Linq;

namespace HikvisionDemo.HikVision
{
    ///
    /// ----------------------------------------------------------------
    /// Copyright @CoderMan/CoderdMan1012 2022 All rights reserved
    /// Author      : CoderMan/CoderdMan1012
    /// Created Time: 2022/10/31 14:27:10
    /// Description :
    /// ----------------------------------------------------------------
    /// Version      Modified Time              Modified By                               Modified Content
    /// V1.0.0.0     2022/10/31 14:27:10    CoderMan/CoderdMan1012         首次编写         
    ///
    public class SecurityCameraVM : ViewModelBase
    {
        #region 1. 绑定变量
        private ObservableCollection<SecurityCameraInfoVM> listCameraInfos = new ObservableCollection<SecurityCameraInfoVM>();
        public ObservableCollection<SecurityCameraInfoVM> ListCameraInfos
        {
            get => listCameraInfos;
            set => Set(ref listCameraInfos, value);
        }

        private int intCurDevice = -1;
        public int IntCurDevice
        {
            get => intCurDevice;
            set => Set(ref intCurDevice, value);
        }

        private int intRows = 3;
        public int IntRows
        {
            get => intRows;
            set => Set(ref intRows, value);
        }

        private int intCols = 3;
        public int IntCols
        {
            get => intCols;
            set => Set(ref intCols, value);
        }
        #endregion

        #region 2. 全局变量
        private UniformGrid MyVideoContainer { get; set; }
        private List<PictureBox> MyPictureBoxes { get; set; } = new List<PictureBox>();
        private SecurityCameraInfoVM MySecurityCameraInfoVM { get; set; }

        /// <summary>
        /// 设备是否初始化
        /// </summary>
        private bool IsInitSDK { get; set; } = false;
        private bool IsRecord { get; set; } = false;

        public int ErrId { get; set; }
        #endregion


        #region 委托和事件 打印日志消息 传递变量
        // 声明一个委托
        public delegate void LogEventHandler(string info, EnumLogType type);
        // 声明一个事件
        public event LogEventHandler LogEvent;
        // 触发事件
        protected virtual void PrintLog(string info, EnumLogType type)
        {
            LogEvent?.Invoke(info, type);
        }
        #endregion


        public SecurityCameraVM()
        {

        }


        #region 3. 绑定命令
        /// <summary>
        /// 关联控件
        /// </summary>
        public RelayCommand<RoutedEventArgs> CmdLoaded => new Lazy<RelayCommand<RoutedEventArgs>>(() => new RelayCommand<RoutedEventArgs>(Loaded)).Value;
        private void Loaded(RoutedEventArgs e)
        {
            MySecurityCameraInfoVM = (e.Source as SecurityCameraTool).MySecurityCameraInfoControl.DataContext as SecurityCameraInfoVM;

            MyVideoContainer = (e.Source as SecurityCameraTool).MyVideoContainer;
            // 图像显示容器
            for (int i = 0; i < MyVideoContainer.Children.Count; i++)
            {
                // PictureBoxControl 需要和 SecurityCameraTool 放在一个项目里
                MyPictureBoxes.Add((MyVideoContainer.Children[i] as PictureBoxControl).MyPictureBoxHost.Child as PictureBox);
            }
        }

        /// <summary>
        /// 加载、导出设备信息
        /// </summary>
        public RelayCommand<string> CmdLoadSaveDevice => new Lazy<RelayCommand<string>>(() => new RelayCommand<string>(LoadSaveDevice)).Value;
        private void LoadSaveDevice(string flag)
        {
            try
            {
                if (flag == "Load")
                {
                    PrintLog("加载设备信息", EnumLogType.Debug);
                    OpenFileDialog dialog = new OpenFileDialog
                    {
                        Title = "加载网络设备文件",
                        Filter = "网络设备文件(*.xml)|*.xml",
                        RestoreDirectory = true,
                        InitialDirectory = Environment.CurrentDirectory + "\\Device",
                    };
                    if (dialog.ShowDialog() != DialogResult.OK)
                    {
                        PrintLog("取消文件加载", EnumLogType.Warning);
                        return;
                    }
                    string filename = dialog.FileName;

                    LoadDevice(filename);
                }
                else if (flag == "Save")
                {
                    PrintLog("保存设备信息", EnumLogType.Debug);
                    SaveFileDialog dialog = new SaveFileDialog
                    {
                        Title = "保存网络设备文件",
                        Filter = "网络设备文件(*.xml)|*.xml",
                        RestoreDirectory = true,
                        InitialDirectory = Environment.CurrentDirectory + "\\Device",
                    };
                    if (dialog.ShowDialog() != DialogResult.OK)
                    {
                        PrintLog("取消文件保存", EnumLogType.Warning);
                        return;
                    }
                    string filename = dialog.FileName;

                    // 写入 xml 内容
                    // 创建文档
                    XDocument xDoc = new XDocument();
                    // 根节点
                    XElement root = new XElement("Device");
                    // 添加根节点
                    xDoc.Add(root);
                    for (int i = 0; i < ListCameraInfos.Count; i++)
                    {
                        // 节点
                        XElement node = new XElement(string.Format("Device{0}", i + 1));
                        // 属性
                        string device_name = ListCameraInfos[i].DeviceName;
                        string device_type = ListCameraInfos[i].IntDeviceType == 0 ? "球机" : "枪机";
                        string ip = ListCameraInfos[i].Ip;
                        int port = ListCameraInfos[i].Port;
                        string user_name = ListCameraInfos[i].UserName;
                        string password = ListCameraInfos[i].PassWord;
                        XAttribute att1 = new XAttribute("DeviceName", device_name);
                        XAttribute att2 = new XAttribute("DeviceType", device_type);
                        XAttribute att3 = new XAttribute("Ip", ip);
                        XAttribute att4 = new XAttribute("Port", port);
                        XAttribute att5 = new XAttribute("UserName", user_name);
                        XAttribute att6 = new XAttribute("PassWord", password);
                        node.Add(att1, att2, att3, att4, att5, att6);
                        // 添加节点
                        root.Add(node);
                    }
                    // 保存
                    xDoc.Save(filename);
                    PrintLog("设备保存完成：" + filename, EnumLogType.Success);
                }
            }
            catch (Exception ex)
            {
                PrintLog("设备存取异常：" + ex.Message, EnumLogType.Error);
            }
        }

        /// <summary>
        /// 添加设备
        /// </summary>
        public RelayCommand CmdAddDevice => new Lazy<RelayCommand>(() => new RelayCommand(AddDevice)).Value;
        private void AddDevice()
        {
            try
            {
                bool flag = IPAddress.TryParse(MySecurityCameraInfoVM.Ip, out _);
                if (!flag)
                {
                    PrintLog("IP 设置错误", EnumLogType.Error);
                    return;
                }
                // 获取参数
                SecurityCameraInfoVM info = new SecurityCameraInfoVM(MySecurityCameraInfoVM);
                // 检测是否已存在
                //foreach (SecurityCameraInfoVM item in ListCameraInfos)
                //{
                //    if (info.Ip == item.Ip)
                //    {
                //        PrintLog("IP 已存在", EnumLogType.Warning);
                //        return;
                //    }
                //}
                ListCameraInfos.Add(info);
                IntCurDevice = ListCameraInfos.Count - 1;
                PrintLog(string.Format("添加设备【{0}】", info.DeviceName), EnumLogType.Success);
            }
            catch (Exception ex)
            {
                PrintLog("设备添加异常：" + ex.Message, EnumLogType.Error);
            }
        }

        /// <summary>
        /// 删除设备
        /// </summary>
        public RelayCommand CmdDelDevice => new Lazy<RelayCommand>(() => new RelayCommand(DelDevice)).Value;
        private void DelDevice()
        {
            try
            {
                for (int i = 0; i < ListCameraInfos.Count; i++)
                {
                    if (ListCameraInfos[i].Ip == MySecurityCameraInfoVM.Ip)
                    {
                        PrintLog(string.Format("删除设备【{0}】", ListCameraInfos[i].DeviceName), EnumLogType.Success);
                        // 注销登陆对应设备
                        Dispose(i);
                        ListCameraInfos.RemoveAt(i);
                        IntCurDevice = -1;
                        IntCurDevice = ListCameraInfos.Count - 1 > i ? i : ListCameraInfos.Count - 1;
                        // 强制界面刷新
                        RefreshPictureBox();
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                PrintLog("设备删除异常：" + ex.Message, EnumLogType.Error);
            }
        }

        /// <summary>
        /// 更新设备
        /// </summary>
        public RelayCommand CmdRefreshDevice => new Lazy<RelayCommand>(() => new RelayCommand(RefreshDevice)).Value;
        private void RefreshDevice()
        {
            if (IntCurDevice < 0)
            {
                return;
            }
            ListCameraInfos[IntCurDevice].DeviceName = MySecurityCameraInfoVM.DeviceName;
            ListCameraInfos[IntCurDevice].IntDeviceType = MySecurityCameraInfoVM.IntDeviceType;
            ListCameraInfos[IntCurDevice].Ip = MySecurityCameraInfoVM.Ip;
            ListCameraInfos[IntCurDevice].UserName = MySecurityCameraInfoVM.UserName;
            ListCameraInfos[IntCurDevice].PassWord = MySecurityCameraInfoVM.PassWord;
        }

        /// <summary>
        /// 开始监控
        /// </summary>
        public RelayCommand CmdStart => new Lazy<RelayCommand>(() => new RelayCommand(Start)).Value;
        public void Start()
        {
            try
            {
                /// 初始化
                PrintLog("初始化设备", EnumLogType.Debug);
                IsInitSDK = CHCNetSDK.NET_DVR_Init();
                if (!IsInitSDK)
                {
                    ErrId = (int)CHCNetSDK.NET_DVR_GetLastError();
                    PrintLog("NET_DVR_Init error, error code= " + ErrId, EnumLogType.Error);
                }
                else
                {
                    ErrId = 0;
                    // 保存 SDK 日志
                    _ = CHCNetSDK.NET_DVR_SetLogToFile(3, @"SdkLog\", true);
                    // 设置连接时间
                    _ = CHCNetSDK.NET_DVR_SetConnectTime(1000, 1);
                    // 设置重连时间
                    _ = CHCNetSDK.NET_DVR_SetConnectTime(3000, 1);
                }

                for (int i = 0; i < ListCameraInfos.Count; i++)
                {
                    /// 登陆
                    if (ListCameraInfos[i].UserID < 0)
                    {
                        //OnNoticed(string.Format("设置设备【{0}】登陆信息", ListCameraInfos[i].DeviceName), EnumLogType.Debug);
                        /// 登陆信息
                        ListCameraInfos[i].LogInfo = new CHCNetSDK.NET_DVR_USER_LOGIN_INFO();

                        // 设备 IP 地址或者域名
                        byte[] byIP = Encoding.Default.GetBytes(ListCameraInfos[i].Ip);
                        ListCameraInfos[i].LogInfo.sDeviceAddress = new byte[129];
                        byIP.CopyTo(ListCameraInfos[i].LogInfo.sDeviceAddress, 0);

                        // 设备用户名
                        byte[] byUserName = Encoding.Default.GetBytes(ListCameraInfos[i].UserName);
                        ListCameraInfos[i].LogInfo.sUserName = new byte[64];
                        byUserName.CopyTo(ListCameraInfos[i].LogInfo.sUserName, 0);

                        // 设备密码
                        byte[] byPassword = Encoding.Default.GetBytes(ListCameraInfos[i].PassWord);
                        ListCameraInfos[i].LogInfo.sPassword = new byte[64];
                        byPassword.CopyTo(ListCameraInfos[i].LogInfo.sPassword, 0);

                        // 设备服务端口号
                        ListCameraInfos[i].LogInfo.wPort = (ushort)ListCameraInfos[i].Port;

                        // 是否异步登录：0- 否，1- 是 
                        ListCameraInfos[i].LogInfo.bUseAsynLogin = false;

                        ListCameraInfos[i].DeviceInfo = new CHCNetSDK.NET_DVR_DEVICEINFO_V40();

                        /// 登录设备
                        // 一台设备对应一个，从 0 开始
                        ListCameraInfos[i].UserID = CHCNetSDK.NET_DVR_Login_V40(ref ListCameraInfos[i].LogInfo, ref ListCameraInfos[i].DeviceInfo);
                        if (ListCameraInfos[i].UserID < 0)
                        {
                            ErrId = (int)CHCNetSDK.NET_DVR_GetLastError();
                            // 登录失败，输出错误号
                            PrintLog("NET_DVR_Login_V40 failed, error code= " + ErrId, EnumLogType.Error);
                            return;
                        }
                        ErrId = 0;
                        PrintLog(string.Format("设备【{0}】已登陆", ListCameraInfos[i].DeviceName), EnumLogType.Success);
                    }

                    /// 取流
                    if (ListCameraInfos[i].RealHandle < 0)
                    {
                        /// 预览实时流回调函数
                        //OnNoticed(string.Format("预览设备【{0}】实时流回调函数", ListCameraInfos[i].DeviceName), EnumLogType.Debug);
                        if (ListCameraInfos[i].RealDataCallBack == null)
                        {
                            // 预览实时流回调函数
                            ListCameraInfos[i].RealDataCallBack = new CHCNetSDK.REALDATACALLBACK(RealDataCallBack);
                        }

                        /// 预览参数
                        //OnNoticed(string.Format("预览设备【{0}】参数", ListCameraInfos[i].DeviceName), EnumLogType.Debug);
                        CHCNetSDK.NET_DVR_PREVIEWINFO lpPreviewInfo = new CHCNetSDK.NET_DVR_PREVIEWINFO
                        {
                            // 预览窗口
                            hPlayWnd = MyPictureBoxes[i].Handle,
                            // 预览的设备通道
                            lChannel = ListCameraInfos[i].Channel,
                            // 用以下设置不卡顿
                            // 码流类型：0-主码流，1-子码流，2-码流3，3-码流4，以此类推
                            dwStreamType = 1,  // 默认 0
                            // 连接方式：0- TCP方式，1- UDP方式，2- 多播方式，3- RTP方式，4-RTP/RTSP，5-RSTP/HTTP
                            dwLinkMode = 4,  // 默认 0
                            // 0- 非阻塞取流，1- 阻塞取流
                            bBlocked = true, // 默认 false
                            // 播放库播放缓冲区最大缓冲帧数
                            dwDisplayBufNum = 5, // 默认 1
                            byProtoType = 0,
                            byPreviewMode = 0
                        };

                        /// 打开预览
                        //OnNoticed(string.Format("预览设备【{0}】", ListCameraInfos[i].DeviceName), EnumLogType.Debug);
                        // 用户数据
                        IntPtr pUser = new IntPtr();
                        // 不抓数据流 RealDataCallBack --> null
                        //ListCameraInfos[i].RealHandle = CHCNetSDK.NET_DVR_RealPlay_V40(ListCameraInfos[i].UserID, ref lpPreviewInfo, ListCameraInfos[i].RealDataCallBack, pUser);
                        ListCameraInfos[i].RealHandle = CHCNetSDK.NET_DVR_RealPlay_V40(ListCameraInfos[i].UserID, ref lpPreviewInfo, null, pUser);
                        if (ListCameraInfos[i].RealHandle < 0)
                        {
                            ErrId = (int)CHCNetSDK.NET_DVR_GetLastError();
                            PrintLog("NET_DVR_RealPlay_V40 failed, error code= " + ErrId, EnumLogType.Error);
                            return;
                        }
                    }
                    ErrId = 0;
                    PrintLog(string.Format("开始监控【{0}】", ListCameraInfos[i].DeviceName), EnumLogType.Debug);
                }
            }
            catch (Exception ex)
            {
                PrintLog("设备启动异常：" + ex.Message, EnumLogType.Error);
            }
        }

        /// <summary>
        /// 停止监控
        /// </summary>
        public RelayCommand CmdStop => new Lazy<RelayCommand>(() => new RelayCommand(Stop)).Value;
        public void Stop()
        {
            try
            {
                if (IsInitSDK)
                {
                    ErrId = 0;
                    for (int i = 0; i < ListCameraInfos.Count; i++)
                    {
                        if (ListCameraInfos[i].UserID >= 0)
                        {
                            /// 停止预览
                            //OnNoticed(string.Format("设备【{0}】停止预览", ListCameraInfos[i].DeviceName), EnumLogType.Debug);
                            bool result = CHCNetSDK.NET_DVR_StopRealPlay(ListCameraInfos[i].RealHandle);
                            if (!result)
                            {
                                ErrId = (int)CHCNetSDK.NET_DVR_GetLastError();
                                PrintLog("NET_DVR_StopRealPlay failed, error code= " + ErrId, EnumLogType.Error);
                                return;
                            }
                            ListCameraInfos[i].RealHandle = -1;

                            /// 注销登录
                            //OnNoticed(string.Format("设备【{0}】注销登录", ListCameraInfos[i].DeviceName), EnumLogType.Debug);
                            result = CHCNetSDK.NET_DVR_Logout(ListCameraInfos[i].UserID);
                            if (!result)
                            {
                                ErrId = (int)CHCNetSDK.NET_DVR_GetLastError();
                                PrintLog("NET_DVR_Logout failed, error code= " + ErrId, EnumLogType.Error);
                                return;
                            }
                            ListCameraInfos[i].UserID = -1;
                        }
                        ListCameraInfos[i].IsRecord = false;
                        PrintLog(string.Format("停止监控【{0}】", ListCameraInfos[i].DeviceName), EnumLogType.Success);
                    }
                    /// 清空
                    //OnNoticed("清理设备", EnumLogType.Debug);
                    _ = CHCNetSDK.NET_DVR_Cleanup();
                    // 强制界面刷新
                    RefreshPictureBox();
                }
            }
            catch (Exception ex)
            {
                PrintLog("设备停止异常：" + ex.Message, EnumLogType.Error);
            }
        }

        /// <summary>
        /// 取流
        /// </summary>
        public RelayCommand<string> CmdCapture => new Lazy<RelayCommand<string>>(() => new RelayCommand<string>(Capture)).Value;
        private void Capture(string flag)
        {
            try
            {
                if (flag == "bmp")
                {
                    for (int i = 0; i < ListCameraInfos.Count; i++)
                    {
                        // BMP 抓图
                        string filename = GenImageName(i, flag);
                        if (!CHCNetSDK.NET_DVR_CapturePicture(ListCameraInfos[i].RealHandle, filename))
                        {
                            ErrId = (int)CHCNetSDK.NET_DVR_GetLastError();
                            PrintLog("NET_DVR_CapturePicture failed, error code= " + ErrId, EnumLogType.Error);
                        }
                        else
                        {
                            ErrId = 0;
                            PrintLog(string.Format("设备【{0}】存图完成：", ListCameraInfos[i].DeviceName) + filename, EnumLogType.Success);
                        }
                    }
                }
                else if (flag == "jpg")
                {
                    for (int i = 0; i < ListCameraInfos.Count; i++)
                    {
                        string filename = GenImageName(i, flag);
                        // 通道号
                        int lChannel = ListCameraInfos[i].Channel;
                        CHCNetSDK.NET_DVR_JPEGPARA lpJpegPara = new CHCNetSDK.NET_DVR_JPEGPARA
                        {
                            // 图像质量
                            wPicQuality = 0,
                            // 抓图分辨率 Picture size: 2- 4CIF，0xff- Auto(使用当前码流分辨率)，抓图分辨率需要设备支持，更多取值请参考SDK文档
                            wPicSize = 0xff,
                        };
                        // JPEG 抓图
                        if (!CHCNetSDK.NET_DVR_CaptureJPEGPicture(ListCameraInfos[i].UserID, lChannel, ref lpJpegPara, filename))
                        {
                            ErrId = (int)CHCNetSDK.NET_DVR_GetLastError();
                            PrintLog("NET_DVR_CaptureJPEGPicture failed, error code= " + ErrId, EnumLogType.Error);
                        }
                        else
                        {
                            ErrId = 0;
                            PrintLog(string.Format("设备【{0}】存图完成：", ListCameraInfos[i].DeviceName) + filename, EnumLogType.Success);
                        }
                    }
                }
                else if (flag == "mp4")
                {
                    IsRecord = !IsRecord;
                    if (IsRecord)
                    {
                        IsRecord = true;
                        for (int i = 0; i < ListCameraInfos.Count; i++)
                        {
                            string filename = GenVideoName(i);
                            _ = StartRecord(filename, i);
                        }
                    }
                    else
                    {
                        for (int i = 0; i < ListCameraInfos.Count; i++)
                        {
                            _ = StopRecord(i);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                PrintLog("取流异常：" + ex.Message, EnumLogType.Error);
            }
        }

        /// <summary>
        /// 布局
        /// </summary>
        public RelayCommand<int> CmdLayOut => new Lazy<RelayCommand<int>>(() => new RelayCommand<int>(LayOut)).Value;
        private void LayOut(int flag)
        {
            IntRows = (int)Math.Sqrt(flag);
            IntCols = IntRows;
            for (int i = 0; i < MyVideoContainer.Children.Count; i++)
            {
                MyVideoContainer.Children[i].Visibility = i < flag ? Visibility.Visible : Visibility.Collapsed;
            }
        }
        #endregion

        #region 4. 内部方法
        /// <summary>
        /// 生成图像名
        /// </summary>
        /// <param name="flag"></param>
        /// <returns></returns>
        private string GenImageName(int idx, string flag = "bmp")
        {
            return Environment.CurrentDirectory + "\\Data\\CapImage\\" + FileIoHelper.DateTimeNowToString() + "_" + ListCameraInfos[idx].DeviceName + "." + flag;
        }

        /// <summary>
        /// 生成视频名
        /// </summary>
        /// <returns></returns>
        private string GenVideoName(int idx)
        {
            return Environment.CurrentDirectory + "\\Data\\CapVideo\\" + FileIoHelper.DateTimeNowToString() + "_" + ListCameraInfos[idx].DeviceName + ".mp4";
        }

        /// <summary>
		/// 清理所有正在使用的资源
		/// </summary>
		private void Dispose(int idx)
        {
            if (ListCameraInfos[idx].RealHandle >= 0)
            {
                _ = CHCNetSDK.NET_DVR_StopRealPlay(ListCameraInfos[idx].RealHandle);
            }
            if (ListCameraInfos[idx].UserID >= 0)
            {
                _ = CHCNetSDK.NET_DVR_Logout(ListCameraInfos[idx].UserID);
            }
        }

        /// <summary>
        /// 刷新图像显示窗口
        /// </summary>
        private void RefreshPictureBox()
        {
            _ = System.Windows.Application.Current.Dispatcher.BeginInvoke(new Action(() =>
            {
                for (int i = 0; i < MyVideoContainer.Children.Count; i++)
                {
                    MyVideoContainer.Children[i].Visibility = Visibility.Collapsed;
                }
                LayOut(IntRows * IntCols);
            }));
        }

        /// <summary>
        /// 取流回调函数
        /// </summary>
        /// <param name="lRealHandle"></param>
        /// <param name="dwDataType"></param>
        /// <param name="pBuffer"></param>
        /// <param name="dwBufSize"></param>
        /// <param name="pUser"></param>
        public void RealDataCallBack(int lRealHandle, uint dwDataType, IntPtr pBuffer, uint dwBufSize, IntPtr pUser)
        {
            if (dwBufSize > 0)
            {
                byte[] sData = new byte[dwBufSize];
                Marshal.Copy(pBuffer, sData, 0, (int)dwBufSize);
                // 多设备注意文件占用
                string str = string.Format(@"Data\CapStream\实时流数据{0}.ps", lRealHandle);
                FileStream fs = new FileStream(str, FileMode.Create);
                int iLen = (int)dwBufSize;
                fs.Write(sData, 0, iLen);
                fs.Close();
                ErrId = (int)CHCNetSDK.NET_DVR_GetLastError();
            }
            else
            {
                ErrId = -1;
            }
        }
        #endregion

        #region 5. 外部接口
        public void LoadDevice(string filename)
        {
            // 初始化
            for (int i = 0; i < ListCameraInfos.Count; i++)
            {
                Dispose(i);
            }
            // 清理
            if (IsInitSDK)
            {
                _ = CHCNetSDK.NET_DVR_Cleanup();
            }
            ListCameraInfos = new ObservableCollection<SecurityCameraInfoVM>();

            // 加载 xml 文件
            XDocument xDoc = XDocument.Load(filename);
            XElement roots = xDoc.Root;
            //IEnumerable<XElement> allElements = xDoc.Descendants();
            // 遍历所有节点 设备数量未知
            foreach (XElement element in xDoc.Descendants())
            {
                string node_name = element.Name.ToString();
                XElement node = roots.Element(node_name);
                // 判断非 null
                if (node != null)
                {
                    // 获取值
                    string device_name = roots.Element(node_name).Attribute("DeviceName").Value;
                    string device_type = roots.Element(node_name).Attribute("DeviceType").Value;
                    string ip = roots.Element(node_name).Attribute("Ip").Value;
                    int port = int.Parse(roots.Element(node_name).Attribute("Port").Value);
                    string user_name = roots.Element(node_name).Attribute("UserName").Value;
                    string password = roots.Element(node_name).Attribute("PassWord").Value;
                    SecurityCameraInfoVM info = new SecurityCameraInfoVM
                    {
                        DeviceName = device_name,
                        IntDeviceType = device_type == "球机" ? 0 : 1,
                        Ip = ip,
                        Port = port,
                        UserName = user_name,
                        PassWord = password,
                    };
                    ListCameraInfos.Add(info);
                }
            }
            IntCurDevice = ListCameraInfos.Count > 0 ? 0 : -1;
            // 布局
            LayOut(9);
            PrintLog("设备加载完成：" + filename, EnumLogType.Success);
        }

        /// <summary>
        /// 截图
        /// </summary>
        public void CaptureImage(int idx, string filename)
        {
            // 通道号
            int lChannel = ListCameraInfos[idx].Channel;
            CHCNetSDK.NET_DVR_JPEGPARA lpJpegPara = new CHCNetSDK.NET_DVR_JPEGPARA
            {
                // 图像质量
                wPicQuality = 0,
                // 抓图分辨率 Picture size: 2- 4CIF，0xff- Auto(使用当前码流分辨率)，抓图分辨率需要设备支持，更多取值请参考SDK文档
                wPicSize = 0xff,
            };
            // JPEG 抓图
            if (!CHCNetSDK.NET_DVR_CaptureJPEGPicture(ListCameraInfos[idx].UserID, lChannel, ref lpJpegPara, filename))
            {
                ErrId = (int)CHCNetSDK.NET_DVR_GetLastError();
                PrintLog("NET_DVR_CaptureJPEGPicture failed, error code= " + ErrId, EnumLogType.Error);
            }
            else
            {
                ErrId = 0;
                PrintLog(string.Format("设备【{0}】存图完成：", ListCameraInfos[idx].DeviceName) + filename, EnumLogType.Success);
            }
        }

        /// <summary>
        /// 开始录屏
        /// </summary>
        /// <returns></returns>
        public bool StartRecord(string filename, int idx)
        {
            try
            {
                if (ListCameraInfos[idx].IsRecord)
                {
                    //OnNoticed(string.Format("设备【{0}】已经处于录屏状态", ListCameraInfos[idx].DeviceName), EnumLogType.Warning);
                    return false;
                }
                else
                {
                    // 通道号
                    int lChannel = ListCameraInfos[idx].Channel;
                    _ = CHCNetSDK.NET_DVR_MakeKeyFrame(ListCameraInfos[idx].UserID, lChannel);

                    // 开始录像
                    bool result = CHCNetSDK.NET_DVR_SaveRealData(ListCameraInfos[idx].RealHandle, filename);
                    if (!result)
                    {
                        ErrId = (int)CHCNetSDK.NET_DVR_GetLastError();
                        PrintLog("NET_DVR_SaveRealData failed, error code= " + ErrId, EnumLogType.Error);
                    }
                    else
                    {
                        ErrId = 0;
                        PrintLog(string.Format("设备【{0}】开始录制视频：", ListCameraInfos[idx].DeviceName) + filename, EnumLogType.Success);

                    }
                    ListCameraInfos[idx].IsRecord = true;
                }
                return true;
            }
            catch (Exception ex)
            {
                PrintLog("启动录屏异常：" + ex.Message, EnumLogType.Error);
                return false;
            }
        }

        /// <summary>
        /// 结束录屏
        /// </summary>
        /// <returns></returns>
        public bool StopRecord(int idx)
        {
            try
            {
                if (!ListCameraInfos[idx].IsRecord)
                {
                    //OnNoticed(string.Format("设备【{0}】未处于录屏状态", ListCameraInfos[idx].DeviceName), EnumLogType.Warning);
                    return false;
                }
                else
                {
                    // 停止录像
                    if (!CHCNetSDK.NET_DVR_StopSaveRealData(ListCameraInfos[idx].RealHandle))
                    {
                        ErrId = (int)CHCNetSDK.NET_DVR_GetLastError();
                        PrintLog("NET_DVR_SaveRealData failed, error code= " + ErrId, EnumLogType.Error);
                    }
                    else
                    {
                        ErrId = 0;
                        PrintLog(string.Format("设备【{0}】停止录制视频", ListCameraInfos[idx].DeviceName), EnumLogType.Success);
                    }
                    ListCameraInfos[idx].IsRecord = false;
                }
                return true;
            }
            catch (Exception ex)
            {
                PrintLog("结束录屏异常：" + ex.Message, EnumLogType.Error);
                return false;
            }
        }

        /// <summary>
        /// 自动录制一段时间视频
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="idx"></param>
        /// <param name="seconds"></param>
        public void AutoRecord(string filename, int idx, int seconds)
        {
            try
            {
                _ = Task.Run(() =>
                {
                    _ = StartRecord(filename, idx);
                    DateTime t = DateTime.Now.AddMilliseconds(seconds * 1000);
                    while (DateTime.Now < t)
                    {
                        DispatcherHelper.DoEvents();
                    }
                    _ = StopRecord(idx);
                });
            }
            catch (Exception ex)
            {
                PrintLog("录屏异常：" + ex.Message, EnumLogType.Error);
            }
        }

        /// <summary>
        /// 释放资源 程序关闭时调用
        /// </summary>
        public void Dispose()
        {
            if (IsInitSDK)
            {
                for (int i = 0; i < ListCameraInfos.Count; i++)
                {
                    Dispose(i);
                }
                _ = CHCNetSDK.NET_DVR_Cleanup();
                ErrId = 0;
            }
        }
        #endregion
    }
}