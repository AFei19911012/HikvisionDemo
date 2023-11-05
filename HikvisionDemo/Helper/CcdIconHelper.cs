using System.Windows.Media;

namespace HikvisionDemo.Helper
{
    ///
    /// ----------------------------------------------------------------
    /// Copyright @BigWang 2023 All rights reserved
    /// Author      : BigWang
    /// Created Time: 2023/11/5 16:43:53
    /// Description :
    /// ----------------------------------------------------------------
    /// Version      Modified Time              Modified By     Modified Content
    /// V1.0.0.0     2023/11/5 16:43:53                     BigWang         首次编写         
    ///
    public class CcdIconHelper
    {
        public static Brush CcdBrushConnected { get; set; } = Brushes.Red;
        public static Brush CcdBrushDisConnected { get; set; } = Brushes.Black;
        public static string IconCcdConnected { get; set; } = "IconConnected";
        public static string IconCcdConnectedOff { get; set; } = "IconConnectedOff";
        public static string IconCcdConnectOn { get; set; } = "IconConnectOn";
        public static string IconCcdConnectOff { get; set; } = "IconConnectOff";
        public static string CcdIconDome { get; set; } = "IconCameraDome";
        public static string CcdIconBox { get; set; } = "IconCameraBox";
        public static string IconUsb { get; set; } = "IconUsb";
        public static string IconGige { get; set; } = "IconVideo";
    }
}