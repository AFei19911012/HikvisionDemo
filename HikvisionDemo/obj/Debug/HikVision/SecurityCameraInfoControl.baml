   M S B A M L   `   `   `  ����H  DHikvisionDemo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullA%clr-namespace:HikvisionDemo.HikVisionHikvisionDemo.HikVision  5      7    1HikvisionDemo.HikVision.SecurityCameraInfoControl   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      S OHandyControl, Version=3.3.0.0, Culture=neutral, PublicKeyToken=45be8712787a1e5b0hc'https://handyorg.github.io/handycontrol  5      1local%clr-namespace:HikvisionDemo.HikVision  5        � DataContext  5	      2   ,HikvisionDemo.HikVision.SecurityCameraInfoVM  5
   
   .��5      9� )  #HandyControl.Controls.BorderElement   CornerRadius$ 0��6    9� 
IsExpanded . ��5   
   �� 5      $��设备信息��6   $��-5q�6$   $
��Bold"�60   .��5   
   �� $	��5,0q�6   .+�5      ��  �� Orientation$ Vertical=�6   .+�5      #  HandyControl.Controls.TextBox  $ 0��69   $��0,0,0,1q�6[   (  "HandyControl.Controls.TitleElement   
TitleWidth$ 80 �5         TitlePlacement$
 Left=�6<      Title$ 	设备名��5      ��5      �� *
DeviceName+5      $  HandyControl.Controls.ComboBox  5      $ 0��6F    �� 
IsEditable .  $ 80 �5      $
 Left=�6=   $ 设备类型��5      $��0,0,0,1q�6:    �� SelectedIndex 5      �� *IntDeviceType+.@�5      �� $��球机��6#   �� 5      $��枪机��6#   5        5      $ 0��61   $��0,0,0,1q�6S   $ 80 �5      $
 Left=�6<   $ 设备IP��5      ��5      �� *Ip+5        5       $ 0��63   $��0,0,0,1q�6U   $ 80 �5!      $
 Left=�6<   	 �� 
IsReadOnly	 . $ 设备端口��5"      ��5       �� *Port+5"        5#      $ 0��67   $��0,0,0,1q�6Y   $ 80 �5$      $
 Left=�6<   $ 	用户名��5%      ��5#      �� *
UserName+5%        5&      $ 0��67   $��0,0,0,1q�6Y   $ 80 �5'      $
 Left=�6<   $ 密码��5(      ��5&      �� *
PassWord+5(      5)        5+      $ 0��62   $��0,0,0,1q�6T   	 . $ 80 �5,      $
 Left=�68   $ 预览通道��6]   $��Center=�5-      ��5+      �� *	Channel+5-      5.      5/      50      