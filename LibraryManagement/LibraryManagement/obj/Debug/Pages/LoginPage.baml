   M S B A M L   `   `   `  ����L  HLibraryManagement, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullA%clr-namespace:LibraryManagement.PagesLibraryManagement.Pages  5      '    !LibraryManagement.Pages.LoginPage   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      7local%clr-namespace:LibraryManagement.Pages     5        N� Title$  	LoginPage��5	       N� 
Background �#f0ecec.��5      �  � RowDefinitions 5   
   �� 5      $
��1.5* �6   �� 5      $��1* �6   �� 5      $��1* �6   �� 5      $
��1.5* �6   .+�5   
   �� $��0��6   $��Center=�6"   $��Center=�6?   .+�5      �� $��Library��6   $��40(�6'   $��	ExtraBold"�65   5      �� 5   
   $��1��6   $��Center=�6"   $��20q�6=   $	��500��6I   .+�5      �� $��Email or username :��6   $
��Bold"�63   �� 5      -   5         ��Name$ emailUserNameBox��5      5      �� 5   
   $��2��6   $��Center=�6"   $��20q�6=   $	��500��6I   .+�5      �� $��
Password :��6   $
��Bold"�6*   G� 5      -   5         G�Name$ passwordBox��5      5      �� 5   
   $��3��6   $��Center=�6"   .+�5      �� -   5         ��Name$ signInButton��5      $��Sign in��6+   $	��150��6=   $��0,0,0,5q�6I   $��5q�6Z   6f   �� 5       -   5        $ createAccount��5       $��Create account��6,   $	��150��6E   $��5q�6Q   6]   5!      5"      5#      