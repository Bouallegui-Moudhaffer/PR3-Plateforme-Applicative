   M S B A M L   `   `   `  ����E  APA.Desktop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:PA.Desktop.ViewsPA.Desktop.Views  5      ;"clr-namespace:PA.Desktop.ViewModelPA.Desktop.ViewModel  5      E'clr-namespace:PA.Desktop.CustomControlsPA.Desktop.CustomControls  5	          PA.Desktop.Views.HomeView   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      0localclr-namespace:PA.Desktop.Views     5      8	viewmodel"clr-namespace:PA.Desktop.ViewModel     5      :custom'clr-namespace:PA.Desktop.CustomControls     5	      .��5      �� .+�5   
   �� $	��798��6   $	��200��6   .+�5      �� $��MAC Address��6      titleColor2#��[  5   $   $��16(�5   $   $��
Montserrat)�5   $   $
��Bold"�5   $   $��Center=�66   
  �� Top$  5��6S   $	��Top=�6b   �� 5       
 color7#��[ 5   $   $��16(�5   $   $��
Montserrat)�5   $   $��Medium"�5   $   $��Center=�5   $   $  30��5   $   ��5      �� *
MacAddress+5   $   �� 5      $��
IP Address��6   #��[  5   $   $��16(�5   $   $��
Montserrat)�5   $   $
��Bold"�5   $   $	��Top=�5    $   $��Center=�6<   $  55��6Y   �� 5!       
 color6#��[ 5"   $   $��16(�5#   $   $��
Montserrat)�5$   $   $
��Bold"�5%   $   $��Center=�5&   $   $  80��6?   ��5!      �� *	IpAddress+5&   ?   �� 5'      $��	CPU Usage��6   #��[  5(   $   $��16(�5)   $   $��
Montserrat)�5*   $   $
��Bold"�5+   $    �� Right$ 90��66    �� Left$	 628��6H   $  5��6Z   3   -PA.Desktop.CustomControls.CircularProgressBar  5,      -   5          Name$ CpuProgressBar��5,   )      ArcThickness$ 15��5.   %      BackgroundBrush
 �iii�   IndicatorBrush
 � d �$��25(�51   %   $	��118��52   %   $	��109��61   $	 610��53   %   $  41��67      Value 5-   %   �� *CpuUsageValue+53   7   54      � 56   
    � ShowGridLines$ False��6   	 � RowDefinitions	 57      �� 58      �� 59      �� 5:      �� 5;      
 � ColumnDefinitions
 5=      �� 5>      $
��Auto �6#   �� 5?      �� 5@      $
��Auto �6#   �� 5A      .+�5C      �� $��0��6   $��0��6!   $
��CPU:��61   �� 5D      -   5         ��Name$ CpuUsage��5D   E   $��0��6   $��1��6!   $��3��61   ��6W   �� *CpuUsageValue+�� 5E      $��1��6   $��0��6!   $
��Mem:��61   �� 5F      -   5        $ MemUsage��5F   E   $��1��6   $��1��6!   $��3��61   ��6W   �� *MemUsageValue+5G      5H      5I      