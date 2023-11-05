using GalaSoft.MvvmLight;
using HikvisionDemo.Helper;

namespace HikvisionDemo.HikVision
{
    ///
    /// ----------------------------------------------------------------
    /// Copyright @CoderMan/CoderdMan1012 2022 All rights reserved
    /// Author      : CoderMan/CoderdMan1012
    /// Created Time: 2022/10/31 14:26:14
    /// Description :
    /// ----------------------------------------------------------------
    /// Version      Modified Time              Modified By                               Modified Content
    /// V1.0.0.0     2022/10/31 14:26:14    CoderMan/CoderdMan1012         首次编写         
    ///
    public class SecurityCameraInfoVM : ViewModelBase
    {
        private string deviceName = "监控";
        public string DeviceName
        {
            get => deviceName;
            set => _ = Set(ref deviceName, value);
        }


        private string _DeviceType = CcdIconHelper.CcdIconDome;
        public string DeviceType
        {
            get => _DeviceType;
            set => Set(ref _DeviceType, value);
        }


        private int intDeviceType = 0;
        public int IntDeviceType
        {
            get => intDeviceType;
            set
            {
                _ = Set(ref intDeviceType, value);
                DeviceType = IntDeviceType == 0 ? CcdIconHelper.CcdIconDome : CcdIconHelper.CcdIconBox;
            }
        }

        private string ip = "192.168.1.64";
        public string Ip
        {
            get => ip;
            set => Set(ref ip, value);
        }

        private int port = 8000;
        public int Port
        {
            get => port;
            set => Set(ref port, value);
        }

        private string userName = "admin";
        public string UserName
        {
            get => userName;
            set => Set(ref userName, value);
        }

        private string passWord = "Admin";
        public string PassWord
        {
            get => passWord;
            set => Set(ref passWord, value);
        }

        private int channel = 1;
        public int Channel
        {
            get => channel;
            set => Set(ref channel, value);
        }

        public SecurityCameraInfoVM()
        {

        }

        public SecurityCameraInfoVM(SecurityCameraInfoVM vm)
        {
            SetParams(vm);
        }

        public void SetParams(SecurityCameraInfoVM vm)
        {
            DeviceName = vm.DeviceName;
            IntDeviceType = vm.IntDeviceType;
            DeviceType = vm.IntDeviceType == 0 ? CcdIconHelper.CcdIconDome : CcdIconHelper.CcdIconBox;
            Ip = vm.ip;
            Port = vm.port;
            UserName = vm.UserName;
            PassWord = vm.PassWord;
            Channel = vm.Channel;
        }

        /// <summary>
        /// 其它参数
        /// </summary>
        public int UserID { get; set; } = -1;
        public int RealHandle { get; set; } = -1;
        public bool IsRecord { get; set; } = false;
        public CHCNetSDK.REALDATACALLBACK RealDataCallBack { get; set; }
        public CHCNetSDK.NET_DVR_USER_LOGIN_INFO LogInfo;
        public CHCNetSDK.NET_DVR_DEVICEINFO_V40 DeviceInfo;
    }
}