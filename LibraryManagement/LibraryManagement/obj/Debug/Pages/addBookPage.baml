   M S B A M L   `   `   `  ����L  HLibraryManagement, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullA%clr-namespace:LibraryManagement.PagesLibraryManagement.Pages  5      )    #LibraryManagement.Pages.addBookPage   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      7local%clr-namespace:LibraryManagement.Pages     5        N� Title$  addBookPage��5	       N� 
Background �#f0ecec.��5      �  � RowDefinitions 5   
   �� 5      $��2* �6   �� 5      $
��0.5* �6   �� 5      $
��0.5* �6   �� 5      $
��0.5* �6   �� 5      $
��0.5* �6   �� 5      $��1* �6   .+�5   
   �� $��0��6   $��Center=�6"   $��Center=�6=   .+�5      �� $��	ExtraBold"�5      $��20(�5      Add book to library5   '   5      �� 5   
   $��1��6   $��Center=�6"   $	��500��6=   .+�5      �� $��Name :��6   $
��Bold"�5      �� 5!      -   5         ��Name$ nameBox��5!      6)   5"      �� 5$   
   $��2��6   $��Center=�6"   $	��500��6=   .+�5%      �� $��Writer:��6   $
��Bold"�5&      �� 5(      -   5        $ 	writerBox��5(      6+   5)      �� 5+   
   $��3��6   $��Center=�6"   $	��500��6=   .+�5,      �� $��Genre :��6   $
��Bold"�5-      �� 5/      -   5        $ genreBox��5/      6*   50      �� 52   
   $��4��6   $��Center=�6"   $	��500��6=   .+�53      �� $��Printing number :��6   $
��Bold"�54      �� 56      -   5        $ printingNumberBox��56      63   57      �� 59   
   $��5��6    �� Orientation$ 
Horizontal=�6"   $��Center=�6;   $��0,0,0,10q�6X   .+�5:      �� $��Center=�6   $
��Left=�64   $��0, 0, 50, 0q�6O   .+�5;      �� $��Center=�6   $
��Bold"�69   
Proceed?6U   �� 5<      -   5         ��Name$ addBookButton��5=      $��30��5>      $��70��6!   $��0,10,0,0q�5?      $��Add book��6'   5@      5A      �� 5C      $��Center=�6   $��50,0,0,0q�64   .+�5D      �� $��Center=�6   $
��Bold"�69   	Go back ?6V   �� 5E      -   5        $ cancelButton��5E      $��30��5F      $��70��6%   $��0,10,0,0q�5G      $��Cancel��6+   5H      5I      5K      5L      5M      