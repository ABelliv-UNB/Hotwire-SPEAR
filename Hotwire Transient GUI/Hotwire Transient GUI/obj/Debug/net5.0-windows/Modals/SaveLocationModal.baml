   M S B A M L   `   `   `  ����P  LHotwire Transient GUI, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullK*clr-namespace:Hotwire_Transient_GUI.ModalsHotwire_Transient_GUI.Modals  5   	   4    .Hotwire_Transient_GUI.Modals.SaveLocationModal   5   	   W SPresentationCore, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   <local*clr-namespace:Hotwire_Transient_GUI.Modals     5   	     � Title$  SaveLocationModal��5   	   $	��220��6#   $	��400��60    � 
ResizeMode$ NoResize=�6<    � ShowInTaskbar$ False��6R    � WindowStartupLocation$ CenterOwner=�6h   
������  � WindowStyle$
 None=�6�    � AllowsTransparency$
 True��6�   .��5	      ��    	CardTheme#��[  6   $��10q�60   ._�5
   
   � $��10q�6    � RowDefinitions 5   
   �� 5      $��20 �6   �� 5      �� 5      �� 5      $��70 �6   .+�5   
   �� -   5        $��×��5        ButtonTheme#��[ 6   
������ 
���   �$��Right=�6�   $��30��6�    
 myFont#��[ 6�   $��20(�6�   $��1��6�   $��21,0,0,0q�6�   �� 5   
   $��Save file as��6   #��[ 6(   $��24(�6M   $��Center=�6[   $��Center=�6v   $��1��6�   �� 5   
   -   5        $��FileNameTextBox��5      $��2��6)   $��2��66   $��	10,0,10,0q�6J   $��40��6^   $��20(�6j   #��[ 6x   $��Center=�6�   � 5   
   $��3��6   $��
10,0,10,10q�6   $��2��60   $��2��6D    � ColumnDefinitions 5      �� 5      �� 5      .+�5      �� -   5        $��Default Folder��5      $��20(�6k   #��[ 6y   $��4��6�   $��0��6�   $��50��6�   ��6I   �� *SaveToDefault+6�   �� 5      -   5        $��Browse��5      $��20(�6<   #��[ 6J   $��4��6o   $��1��6|   $��50��6�   5      5      5      5       