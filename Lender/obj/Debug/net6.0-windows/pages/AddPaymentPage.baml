   M S B A M L   `   `   `  ����A  =Lender, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:Lender.pagesLender.pages  5      -clr-namespace:Lender.ModelsLender.Models  5      !    Lender.pages.AddPaymentPage   5      W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      ,localclr-namespace:Lender.pages     5      .modelsclr-namespace:Lender.Models     5      $	��275��5	      $	��300��6     N� Title$  AddPaymentPage��5
       � DataContext 5      ��  �� RelativeSource �� *Self+.��5      � $��1��6   $��10q�6    � ColumnDefinitions 5   
   �� 5      $
��0.4* �6   �� 5       � RowDefinitions 5   
   �� 5      �� 5      �� 5      �� 5      �� 5      .+�5   
   �� $��0��6   $��0��6   $��2��6-   $��20(�6A   $��Add Payment��6O      
FancyLabel#��[  6e   $��0 0 0 5q�6�   �� 5   
   $��1��6   $��0��6   $��14(�6-   $
��Type��6;   #��[  6J    	 Khaki#��[ 6n   $��	10 0 10 5q�6�   �� 5   
   -   5        $��PaymentTypeComboBox��5      $��1��6.   $��1��6;     FancyComboBox#��[ 6K   $��	10 0 10 5q�6r   .@�5      �� $��Disbursement��6    �� 
IsSelected$
 True��62   �� 5       $��	Repayment��6   5!      �� 5#   
   $��2��6   $��0��6   $��14(�6-   $��Amount��6;   #��[  6L   #��[ 6p   $��0 0 0 15q�6�   �� 5$   
   $��2��6   $��1��6"   .+�5%      �� -   5        $��PaymentAmount��5%        FancyTextBox#��[ 6+   $��	10 0 10 0q�6Q   6d   �� 5&      -   5        $��PaymentAmountError��5&      #��[  6.   
���  ��$��9(�6c     FiraCode#��[ 6p   $�� ��6�   5'      �� 5)   
   $��3��6   $��0��6   $��14(�6-   $
��Date��6;   #��[  6J   #��[ 6n   $��0 0 0 15q�6�   �� 5*   
   $��3��6   $��1��6"   .+�5+      (  "System.Windows.Controls.DatePicker  -   5        $��PaymentDate��5+        FancyDatePicker#��[ 6,   $��0 0 0 0q�6U   6f   �� 5,      -   5        $��PaymentDateError��5,      #��[  6,   
���  ��$��9(�6a   #��[ 6n   $�� ��6�   5-      �� 5/   
   $��4��6   $��0��6#   $��2��63   $��Center=�6G   $��Center=�6d   .+�50      �� -   5        $��CreatePaymentButton��50        FancyButton#��[ 51      $��Create��52      $��Center=�53      $��Center=�6.   6I   �� 54      -   5        $��CreateButtonError��54      #��[  6-   
���  ��$��9(�6b   #��[ 6o   $�� ��6�   55      56      57      