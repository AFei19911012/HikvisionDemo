   M S B A M L   `   `   `  ����\  XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089S OHandyControl, Version=3.3.0.0, Culture=neutral, PublicKeyToken=45be8712787a1e5b   MyDataGridStyleSmall  DataGrid.Small  �� BasedOn �� 
TargetType&     System.Windows.Controls.DataGrid �� Property  MyDataGridCellSytle �� Value  MyDataGridRowSytle  MyDataGridColumnHeaderStyle  MyDataGridRowHeaderStyle # DataGridColumnHeaderStyle.Small=   7System.Windows.Controls.Primitives.DataGridColumnHeader  PrimaryBrush �� 
TargetType:   4System.Windows.Controls.Primitives.DataGridRowHeader  ReverseTextBrush)   #System.Windows.Controls.DataGridRow �� Triggers P� Property P� Value 	 BackgroundBrushDark 
 BackgroundBrushLight  MouseOverBrush  ListSelectedBrush ]� 
Conditions	 �� Property
 �� Value  LostFocusBrush*   $System.Windows.Controls.DataGridCell �� Binding �� Value  	InfoBrush  WarningBrush  SuccessBrush  DangerBrush  MyDataGridCellSytleNG  MyListBoxRowSytle  ListBoxItemBaseStyle.Small  MyLoadingCircleStyle  LoadingCircleBaseStyle)  #HandyControl.Controls.LoadingCircle)  #HandyControl.Controls.BorderElement(  "HandyControl.Controls.TitleElement  MyNumericUpDownStyle  NumericUpDownPlusBaseStyle)  #HandyControl.Controls.NumericUpDown  MyButtonIcon  ButtonBaseStyle  BorderBrush  PrimaryTextBrush �� CornerRadius �� Path �� RelativeSource J� SnapsToDevicePixels � ColumnDefinitions F�Name �� Stretch'	  !HandyControl.Controls.IconElement 	  Geometry �� ElementName �� Triggers �� 
TargetName  AccentBrush�� 5      F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      :x,http://schemas.microsoft.com/winfx/2006/xaml      5      0hc'https://handyorg.github.io/handycontrol  5      %�  &	        7  &	 f    7  &	 |    &	 D    &	 $    &	 �
    7  &	 M    7  &	 �    7  &	 V    7  &	     7  �� 5      8    "   6S   .�5   
   ��  �@  	CellStyle# �  6&   �� 5   
    �@  RowStyle# �  6%   �� 5	   
    �@  ColumnHeaderStyle# �  6.   �� 5
   
    �@  RowHeaderStyle# �  6+   �� 5   
    �@  ColumnHeaderHeight$ 28Q�6/   �� 5   
    �@  	RowHeight$ 24Q�6&   �� 5   
    �@  AutoGenerateColumns .  60   �� 5   
    �@  CanUserResizeRows .  6.   �� 5   
    �@  CanUserReorderColumns .  62   �� 5   
    �@  CanUserAddRows .  6+   �� 5   
    � ��
 ���� 6'   �� 5   
    � ��$ 1q�6,   5      �� 5      8    "  6k   .�5   
   ��  � ��$
 Bold"�6'   �� 5   
    � ��# �  6'   �� 5   
    � ��$	 6,0q�6#   �� 5   
    � �� 5      �� 5      "  6"   .R�5      �� $��Center=�6'   $
��Left=�6B   5      5      5       �� 5"      "  6-   .�5#   
   ��  � ��# �  6'   �� 5$   
    � �� 5%      �� 5&      "  6"   .R�5'      �� $��Center=�6'   $��Center=�6B   5(      5*      5+      �� 5-      "  6'   .�5.   
   ��  � ��$ Center=�6.   �� 5/   
    � ��$
 Left=�60   �� 50   
    � ��$ 0q�6,   �� 51   
    � ��$ 0q�6#    52   
   P� 54       �@ AlternationIndex$ 0��62   . �55      ��  � ��# � 	 6/   56      P� 57       �@ AlternationIndex$ 1��62   . �58      ��  � ��# � 
 6/   59      P� 5<       �@J�IsMouseOver . . �5=      ��  � ��# �  6/   5>      P� 5A       �@ 
IsSelected . . �5B      ��  � ��# �  6/   5C      ]� 5F       5G      �� 5H      	 �@ 
IsSelected
 . 66   �� 5I      	 �@��IsSelectionActive
 .  6F   ./�5K      ��  � ��# �  6/   5L      5N      �� 5P      "  6(   .�5Q   
   ��  � ��$ Center=�65   �� 5R   
    � ��$ Center=�6.   �� 5S   
    � ��$ 0q�6,    5T   
   P� 5V       �@ 
IsSelected . . �5W      ��  � ��# �  6/   5X      ]� 5[       5\      �� 5]      	 �@ 
IsSelected
 . 66   �� 5^      	 �@��IsSelectionActive
 .  6F   ./�5`      ��  � ��# �  6/   5a      �� 5d       6   �� *Type+	 Debug63   .a�5e      ��  � ��
 �   �6/   5f      �� 5h       6   �� *Type+ Info63   .a�5i      ��  � ��# �  6/   5j      �� 5l       6   �� *Type+ Warning63   .a�5m      ��  � ��# �  6/   5n      �� 5p       6   �� *Type+ Success63   .a�5q      ��  � ��# �  6/   5r      �� 5t       6   �� *Type+	 Error63   .a�5u      ��  � ��# �  6/   5v      5x      �� 5{      8    "  6Y    5|   
   �� 5}       6   �� *Result+ NG65   .a�5~      ��  � ��# �  6/   5      5�      �� 5�      8    " ��6\   .�5�   
   ��  � ��$ Center=�65   �� 5�   
    � ��$ Center=�6.    5�   
   ]� 5�       5�      �� 5�      	 �@��
IsSelected
 . 66   �� 5�      	 �@��IsSelectionActive
 .  6F   ./�5�      ��  � ��# �  6/   �� 5�       � ��# � ��6/   5�      5�      �� 5�      8    "  6[   .�5�   
   ��  � ��$ 0,0,0,1q�6,   �� 5�   
    �@ CornerRadius$ 0��6:   �� 5�   
    �@ TitlePlacement$
 Left=�6:   5�      �� 5�      8    "  6_   .�5�   
   ��  � ��$ 0,0,0,1q�6,   �� 5�   
    �@ CornerRadius$ 0��6:   �� 5�   
    �@ TitlePlacement$
 Left=�6:   5�      �� 5�      8    " ��6L   .�5�   
   ��  � ��
 ���� 6'   �� 5�   
    � ��# �  6(   �� 5�   
    � ��$ 1q�6,   �� 5�   
    � ��# �  6'   �� 5�   
    �@ CornerRadius$ 0��6:   �� 5�   
    �@�Cursor$
 Hand��6#   �� 5�   
    � �� 5�      �� 5�      " ��6"   .R�5�      �� #��z��6�   #��z��6�   #��z��6�    6   �� $% (hc:BorderElement.CornerRadius)� �� *TemplatedParent+._�5�      �� #��z��6)   #��z��6l   #��z��6�   # z 6�   .��5�      �  5�   "   �� 5�   &   $
��auto �67   �� 5�   &   .+�5�   "   F� $ MyPath��6'   #��z��67    . $ Uniform=�6v   #��z��6�   #��z 6�   �� 5�   "   $��1��6,   $��Center=�6<   #��z��6W   $	��5,0q�6x   ��5�   ,   �� $ MyPath��$
 Fill�5�      5�      5�       5�      P� 5�       �@J�IsMouseOver . . �5�      �� $ MyPath��6%   # �  6I    � ��5�      P� 5�       � ��$ 开始采集��65   . �5�      �� # �  6;    � ��5�      P� 5�       � ��$ 停止采集��65   . �5�      �� # �  6;    � ��5�      P� 5�       � ��$ 连接设备��65   . �5�      �� # �  6;    � ��5�      P� 5�       � ��$ 断开设备��65   . �5�      �� # �  6;    � ��5�      5�      5�      5�      5�      