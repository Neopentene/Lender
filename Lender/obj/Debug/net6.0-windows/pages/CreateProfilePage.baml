   M S B A M L   `   `   `  ����
  Lender     Lender.pages.CreateProfile   5      W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5        N� Title$  Create Profile��5       � DataContext 5	      ��  �� RelativeSource �� *Self+.��5      �  � RowDefinitions 5   
   �� 5      �� 5      �� 5      �� 5       � ColumnDefinitions 5   
   �� 5      �� 5      �� 5      �� 5      .+�5   
   �� $��0��6   $��1��6   $��2��6-   $��28(�6A   $��Create Profile��6O      
FancyLabel#��[  6h   �� 5   
   -   5        $��GoBack��5      $��0��5      $��0��5        FancyFabButton#��[ 5       $��38��5!      $��38��6   $��Center=�5"      $��Center=�5#      $��5q�5$      $��Center=�5%      $��Center=�6/   .��5&      �� $��32��6   $��32��6    $��Center=�6,   $��Center=�6I   .+�5'      F� =����     $� �@]� @KL �     $� �@]� ����     $� � H� $� 	 �� StrokeEndLineCap$ Round=�5(       �� StrokeStartLineCap$ Round=�60   $��4��6K   ��5'   D   �� *
Foreground+ �� ElementName$ GoBack��5(   K   z�5)      �� 5*       �� CenterX$ 16Q�6%   	 �� CenterY$	 16Q�62   
 �� ScaleX$	
 0.4Q�6?    �� ScaleY$	 0.4Q�6L   5,      5-      �� 5/   
   $��1��6   $��1��6$   $��2��64   $��2��6H   $��Hidden=�6Y   $��Disabled=�6~   .��50      �  51      �� 52      �� 53      �� 54      �� 55      �� 56      �� 57      �� 58      �� 59      �� 5:      �� 5;      �� 5<      �� 5=      �� 5>       5A      �� 5B      $
��0.6* �6'   �� 5C      .+�5F      �� $��0��6   $��Borrower Name:��6%   #��[  6>   $
��Left=�6b   $��Bottom=�6}   �� 5G      -   5        $��BorrowerName��5G   7   $��0��6   $��1��6'     FancyTextBox#��[ 6K   6q   �� 5H      -   5        $��BorrowerNameError��5H   5   $��1��6   $��1��6%   #��[  6N   
���  ��$��9(�6�     FiraCode#��[ 6�   $�� ��6�   �� 5J      $��2��6   $��Principal Amount:��6%   #��[  6A   $
��Left=�6e   $��Bottom=�6�   �� 5K      -   5        $��PrincipalAmount��5K   7   $��2��6   $��1��6'   #��[ 6N   6t   �� 5L      -   5        $��PrincipalAmountError��5L   5   $��3��6   $��1��6%   #��[  6Q   
���  ��$��9(�6�   #��[ 6�   $�� ��6�   �� 5N      $��4��6   $��Interest Rate:��6%   #��[  6>   $
��Left=�6b   $��Bottom=�6}   �� 5O      -   5        $��InterestRate��5O   7   $��4��6   $��1��6'   #��[ 6K   6q   �� 5P      -   5        $��InterestRateError��5P   5   $��5��6   $��1��6%   #��[  6N   
���  ��$��9(�6�   #��[ 6�   $�� ��6�   �� 5R      $��6��6   $��Sanctioned Amount:��6%   #��[  6B   $
��Left=�6f   $��Bottom=�6�   �� 5S      -   5        $��SanctionedAmount��5S   7   $��6��6   $��1��6'   #��[ 6O   6u   �� 5T      -	   5        $��SanctionedAmountError��5T   5   $��7��6   $��1��6%   #��[  6R   
���  ��$��9(�6�   #��[ 6�   $�� ��6�   �� 5V      $��8��6   $��Loan Start Date:��6%   #��[  6@   $
��Left=�6d   $��Bottom=�6   (  "System.Windows.Controls.DatePicker  5W      -
   5        $��	StartDate��5W   :   $��8��6   $��1��6*     FancyDatePicker#��[ 6K   6t   �� 5X      -   5        $��StartDateError��5X   5   $��9��6   $��1��6%   #��[  6K   
���  ��$��9(�6�   #��[ 6�   $�� ��6�   �� 5Z      $��10��6   $��Time Period:��6&   #��[  6=   $
��Left=�6a   $��Bottom=�6|   �� 5[      -   5        $��
TimePeriod��5[   8   $��10��6   $��1��6(   #��[ 6J   6p   �� 5\      -   5        $��TimePeriodError��5\   6   $��11��6   $��1��6&   #��[  6M   
���  ��$��9(�6�   #��[ 6�   $�� ��6�   5]      5^      �� 5`   
   $��3��5a      $��1��5b      $��2��5c      $��Center=�5d      $��Center=�5e      .+�5g      �� -   5        $��CreateButton��5h        FancyButton#��[ 5i      $��5q�5j      $��8q�5k      $��Center=�5l      $��Center=�5m      $��Create��5n      6"   �� 5p      -   5        $��CreateProfileError��5p      #��[  6.   
���  ��$��9(�6c   #��[ 6p   $�� ��6�   5q      5s      5t      