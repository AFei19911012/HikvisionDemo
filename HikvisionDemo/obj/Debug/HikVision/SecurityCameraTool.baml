   M S B A M L   `   `   `  ����H  DHikvisionDemo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullA%clr-namespace:HikvisionDemo.HikVisionHikvisionDemo.HikVision  5      0    *HikvisionDemo.HikVision.SecurityCameraTool   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      S OHandyControl, Version=3.3.0.0, Culture=neutral, PublicKeyToken=45be8712787a1e5b0hc'https://handyorg.github.io/handycontrol  5      1local%clr-namespace:HikvisionDemo.HikVision  5         RegionBrush#���   5      $��14(�5
        � DataContext  5      .   (HikvisionDemo.HikVision.SecurityCameraVM  5   
   ,  &HandyControl.Interactivity.Interaction   Triggers 5      -  'HandyControl.Interactivity.EventTrigger  5   
      	EventName$ Loaded��6   ,  &HandyControl.Interactivity.TriggerBase   Actions. 5      /  )HandyControl.Interactivity.EventToCommand     PassEventArgsToCommand .    Command 6    �� *	CmdLoaded+6>   5      .��5      �  � ColumnDefinitions 5   
   �� 5      $
��auto �6   �� 5      .+�5   
   ��   BorderBrush#���  6   $��1q�6=   ._�5      �  � RowDefinitions 5      �� 5      $
��auto �6$   �� 5      �� 5       $
��auto �6$   �� 5!      $
��auto �6$   .+�5$      ��  �� Orientation$ 
Horizontal=�6   .+�5%      �� $��导入��6   $��16(�6*   	 � Cursor$
	 Hand��68     IconExportIn'  !HandyControl.Controls.IconElement
   Geometry#
 �  6F     MyButtonIcon#���  6   $
��Load��5&   C   ��6   �� *CmdLoadSaveDevice+6C   �� 5'      $��导出��6   $��16(�6*   $
	 Hand��68     IconExportOut#
 �  6F   #���  6�   $
��Save��5(   C   ��6   �� *CmdLoadSaveDevice+6C   �� 5*      $��监控��6   $��16(�6*   $
	 Hand��68     IconWatching#
 �  6F   #���  6   )  #HandyControl.Controls.BorderElement   CornerRadius$ 0��5+      ��6?   �� *
CmdStart+�� 5,      $��停止��6   $��16(�6*   $
	 Hand��68     IconStop#
 �  6F   #���  6{   $
��Stop��5-   9   ��6   �� *	CmdStop+69   5.      9� 50      $��1��6   $ 0��6(    9� 
IsExpanded . ��51      �� 52      $��设备列表��6$   $��-5q�60   $
��Bold"�6<   .��54      �� -   5        $��	LB_Device��54      $��0q�6~   
������ $	��Top=�55        MyListBoxRowSytle#���  66    �� AlternationCount$ 2��6o   ��54   /   �� *ListCameraInfos+ �� SelectedIndex 6W   �� *IntCurDevice+��57      �� 58      .R�59   "   � $	��0,5q�6'    5:   &   �� 5;   *   $��32 �6;   �� 5<   *   .+�5>   &   �� $��0q�5?   -   $��	-10,0,0,0q�6A    J� IsHitTestVisible .    
ButtonIcon#���  5@   -   
 5>   -   �� *
DeviceType+ 	 GeometryConverter �� 	Converter# [	 5@   -   �� 5A   &   $��TB��6@   -   5        $��1��5A   0   $��Center=�6f   $��5q�6�   
������ $ 0��5B   I   ��5A   J   �� *
DeviceName+5C   0   5D   #   5E      5G      5H      7   1HikvisionDemo.HikVision.SecurityCameraInfoControl  5J      -   5          Name$! MySecurityCameraInfoControl��5J   ?   $��2��62   6?   �� 5L      $��3��6   $ 
Horizontal=�6*   $	��0,2q�6C   .+�5M      �� $��添加��6   $��16(�6*   $
	 Hand��68    
 IconPlusSqure#
 � 
 6F   #���  6�   $	��100��5N   >   ��6   �� *CmdAddDevice+6>   �� 5O      $��删除��6   $��16(�6*   $
	 Hand��68     IconMinusSqure#
 �  6F   #���  6�   $	��100��5P   >   ��6   �� *CmdDelDevice+6>   �� 5Q      $��修改��6   $��16(�6*   $
	 Hand��68     
IconDefine#
 �  6F   #���  6}   $	��100��5R   B   ��6   �� *CmdRefreshDevice+6B   5S      5T      5U      � 5W   
   $��1��6    5X      �� 5Y      $
��auto �6    �� 5Z      .+�5\      �� $ 
Horizontal=�6   $
��Left=�62   .+�5]      �� $��	存图BMP��6   $��16(�6)   $
	 Hand��67     IconImageEmpty#
 �  6E   #���  6�   $	��bmp��5^   8   ��6   �� *
CmdCapture+68   �� 5_      $��	存图JPG��6   $��16(�6)   $
	 Hand��67     IconFormatJpg#
 �  6E   #���  6   $	��jpg��5`   8   ��6   �� *
CmdCapture+68   �� 5a      $��	视频MP4��6   $��16(�6)   $
	 Hand��67     IconFormatMp4#
 �  6E   #���  6   $	��mp4��5b   8   ��6   �� *
CmdCapture+68   �� 5d      $��	标准窗��6   $��16(�6'   $
	 Hand��65     IconLayout_1_1#
 �  6C   #���  6~   $��1��5e   7   ��6   �� *	CmdLayOut+67   �� 5f      $��	四宫格��6   $��16(�6'   $
	 Hand��65     IconLayout_2_2#
 �  6C   #���  6~   $��4��5g   7   ��6   �� *	CmdLayOut+67   �� 5h      $��	九宫格��6   $��16(�6'   $
	 Hand��65     IconLayout_3_3#
 �  6C   #���  6~   $��9��5i   7   ��6   �� *	CmdLayOut+67   �� 5j      $��十六宫格��6   $��16(�6(   $
	 Hand��66     IconLayout_4_4#
 �  6D   #���  6   $��16��5k   7   ��6   �� *	CmdLayOut+67   5l      A� 5n      -   5        $��MyVideoContainer��5n   '   $��1��6    A� Rows 6?   �� *	IntRows+ A� Columns 6X   �� *	IntCols+.+�5p      /	   )HikvisionDemo.HikVision.PictureBoxControl	  	  5q      	  5r      	  5s      	  5t      	  5u      	  5v      	  5w      	  5x      	  5y      $y�	Collapsed=�6*   	  5z      $y�	Collapsed=�6*   	  5{      $y�	Collapsed=�6*   	  5|      $y�	Collapsed=�6*   	  5}      $y�	Collapsed=�6*   	  5~      $y�	Collapsed=�6*   	  5      $y�	Collapsed=�6*   5�      5�      5�      5�      