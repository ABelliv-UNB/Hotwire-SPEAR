   M S B A M L   `   `   `  ����P  LHotwire Transient GUI, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullK*clr-namespace:Hotwire_Transient_GUI.ModalsHotwire_Transient_GUI.Modals  5   	   4    .Hotwire_Transient_GUI.Modals.SaveLocationModal   5   	   \ XPresentationFramework, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089W SPresentationCore, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   <local*clr-namespace:Hotwire_Transient_GUI.Modals     5   	     � Title$  SaveLocationModal��5   	   $	��220��6#   $	��400��60    � 
ResizeMode$ NoResize=�6<    � ShowInTaskbar$ False��6R    � WindowStartupLocation$ CenterOwner=�6h   
������  � WindowStyle$
 None=�6�    � AllowsTransparency$
 True��6�   .��5
      ��    	CardTheme#���   6   $��10q�61   ._�5   
   � $��10q�6    � RowDefinitions 5   
   �� 5      $��20 �6   �� 5      �� 5      �� 5      $��70 �6   .+�5   
   �� -   5        $��×��5        ButtonTheme#���  5      
������ $��Right=�5      $��30��5       
 myFont#���  5        	TextColor#���  5      $��20(�5      $��1��5      $��21,0,0,0q�5      �� 5   
   $��Save file as��5         	TextStyle#���  5!      $��24(�5"      $��Center=�5#      $��Center=�5$      $��1��5%      �� 5'   
   -   5        $��FileNameTextBox��5(      $��2��5)      $��2��5*      $��	10,0,10,0q�5+      $��40��5,      $��20(�5-        TextBoxTheme#���  5.      $��Center=�5/      � 51   
   $��3��6   $��
10,0,10,10q�52      $��2��53      $��2��54       � ColumnDefinitions 55      �� 56      �� 57      .+�5:      �� -   5        $��Default Folder��5;      $��20(�5>      #���  5?      $��4��5@      $��0��5A      $��50��5B      ��5=      �� *SaveToDefault+5B      �� 5D      -   5        $��Browse��5E      $��20(�5G      #���  5H      $��4��5I      $��1��5J      $��50��5K      5M      5N      5O      5P      