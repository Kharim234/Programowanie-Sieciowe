   M S B A M L   `   `   `  ����B  >WpfApp2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null!clr-namespace:WpfApp2WpfApp2  5   	       WpfApp2.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   'localclr-namespace:WpfApp2     5   	     � Title$  
MainWindow��5   	   $	��250��6   $	��900��6)   .��5	      � .+�5
   
   �� -   5         ��Name$ 
ChatWindow��5
      $
��Left=�6&   $	��123��6A   $��
260,10,0,0q�6N   $	��Top=�6c   $	��562��6{   �� 5   
   $
��Left=�6   $��23��6-   $��257,138,0,0q�69    �� TextWrapping$
 Wrap=�6N   $��TextBox��6b   $	��Top=�6q   $	��120��6�   �� 5   
   $
��Left=�6   $��23��6-   $��257,138,0,0q�69   $
 Wrap=�6N   $��TextBox��6b   $	��Top=�6q   $	��120��6�   �� 5   
   -   5         ��Name$ IPserver��5      $
��Left=�6%   $��23��6@   $��	24,28,0,0q�6L   $
 Wrap=�6_   $��	localhost��6s   $	��Top=�6�   $	��173��6�   �� 5   
   -   5        $ Message��5      $
��Left=�6#   $��23��6>   $��257,138,0,0q�6J   $
 Wrap=�6_   $�� ��6s   $	��Top=�6{   $	��565��6�   �� 5   
   -   5        $ 
PortServer��5      $
��Left=�6&   $��23��6A   $��	24,83,0,0q�6M   $
 Wrap=�6`   $��21��6t   $	��Top=�6~   $	��173��6�   �� 5   
   $
��Left=�6   $��24,7,0,0q�6/   $
��Wrap=�6A   $	��IP:��6U   $	��Top=�6`   �� 5   
   $
��Left=�6   $��	24,62,0,0q�6/   $
��Wrap=�6B   $��Port:��6V   $	��Top=�6c   �� 5   
   -   5         ��Name$ 
ButtonSend��5      $
��Send��6%   $
��Left=�64   $��257,173,0,0q�6O   $	��Top=�6d   $��75��6|   6�   �� 5   
   -   5        $ ButtonConnect��5      $��Connect��6(   $
��Left=�6:   $��
24,138,0,0q�6U   $	��Top=�6i   $��84��6�   6�   �� 5   
   -   5        $ ButtonDisconnect��5      $
��Left=�6+   $��113,138,0,0q�6F   $	��Top=�6[   $��84��6s   .��6�   
Disconnect6�   5      5      