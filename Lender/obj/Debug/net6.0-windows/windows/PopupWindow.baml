   M S B A M L   `   `   `  ����A  =Lender, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null/clr-namespace:Lender.windowsLender.windows  5   	        Lender.windows.PopupWindow   5   	   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   .localclr-namespace:Lender.windows     5   	   -   5        $��PopupWindowParent��5   	     � Title$  Popup��5	   	    � SizeToContent$ WidthAndHeight=�6    � WindowStyle$
 None=�5
   	    � AllowsTransparency$
 True��5   	   
������  � 
ResizeMode$ NoResize=�5   	    � UseLayoutRounding$
 True��5   	   .��5      �� $��5 5 5 7q�5   	    �� CornerRadius$ 10��5   	      
GhostWhite#��[  5   	   $
��1.25q�5   	     
MatteBlack#��[ 5   	   $
 True��5   	   ._�5   
   �  � RowDefinitions 5      �� 5      $
��0.1* �6    $��60��6.   $��40��6=   �� 5      $
��0.9* �6    .+�5      � $��0��6    � ColumnDefinitions 5      �� 5       $��* �6'   �� 5!      $��84 �6'   .+�5$      �� $��0��6     TitleBarLabel#��[ 6(   $��Lender��6O   $��	50 0 30 0q�6`   �� 5&      -   5        $��
ControlBar��5'      $��0��5(      #��[ 5)      	 J� Opacity$
	 0.01Q�6>   $��7��5*      $��4 0 0 0q�5+      $	��Top=�5,      
 � Cursor$
 	ScrollAll��5-      5/      �� 51      $��1��6    �� Orientation$ 
Horizontal=�6-   $	��Top=�6F   .+�52      �� -   5        $��MinimizeControlButton��52        ControlButton#��[ 6:   $��32��6a   $��32��6m   $��4 4 4 0q�6x   .��54      �� $��32��6!   $��32��6,   .+�55      F� ����     -1� H� -1	$��3��6w   ��67   �� *
Foreground+ �� ElementName$ MinimizeControlButton��6w   z�56      �� 57   "    �� CenterX$ 16Q�61    �� CenterY$ 16Q�6>    �� ScaleX$ 0.375Q�6K    �� ScaleY$ 0.375Q�6Z   59      5:      �� 5;      -   5        $��CloseControlButton��5;      #��[ 67   $��32��6^   $��32��6j   $��6 4 2 0q�6u   .��5=      �� $��32��6!   $��32��6,   .+�5>      F� +����        � H� H��     H�� H�    	$��3��6�   ��6B   �� *
Foreground+$ CloseControlButton��6�   z�5?      �� 5@   "   $ 16Q�61   $ 16Q�6>   $ 0.375Q�6K   $ 0.375Q�6Z   5B      5C      5D      �� 5F      
���   �$��4��6-   $��0 0 0 1q�6A   $		 0.5Q�6[    J� Effect 5G      -  'System.Windows.Media.Effects.BlurEffect  5H         Radius$ 2Q�6%      
KernelType$ Gaussian=�60   5J      5K       � 5N      -   5        $��ContentFrame��5N   !   $��1��6     � NavigationUIVisibility$ Hidden=�65   5P      5R      5S      5U      