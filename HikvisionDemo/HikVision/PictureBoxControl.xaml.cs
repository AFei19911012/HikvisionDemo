using System.Windows.Controls;
using System.Windows.Media;

namespace HikvisionDemo.HikVision
{
    /// <summary>
    /// PictureBoxControl.xaml 的交互逻辑
    /// </summary>
    public partial class PictureBoxControl : UserControl
    {
        public PictureBoxControl()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 设置边框颜色
        /// </summary>
        /// <param name="brush"></param>
        public void SetBorderBrush(Brush brush)
        {
            MyBorder.BorderBrush = brush;
        }
    }
}