   M S B A M L   `   `   `  ���� G  CLibraryDbSim, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:LibraryDbSimLibraryDbSim      LibraryDbSim.BookListPage   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     ,localclr-namespace:LibraryDbSim     T PMahApps.Metro, Version=2.0.0.0, Culture=neutral, PublicKeyToken=51482d6f650b2b3f6mah,http://metro.mahapps.com/winfx/xaml/controls    N� Title$  Books Available��$	��460��$	��735��.��� $��5q� � RowDefinitions �� $	��350 ��� .+��� -    ��Name$ BooksAvailableLbl��$��Books Available��$��28(�$��Center=�$��Center=�$	��Top=�$��258,10,257,0q�$��45��$	��210��&   System.Windows.Controls.DataGrid  -     Name$ dataGrid��   AutoGenerateColumns .  $��50,70,50,10q�   
IsReadOnly .    SelectionMode$ Single=�   Columns 0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn   Header$ Title��	   CanUserResize	 .  
   CanUserReorder
 .  1  +System.Windows.Controls.DataGridBoundColumn   Binding �� *
bookName+  $ Author��	 .  
 .   �� *
bookAuthor+  $ Stock��	 .  
 .   �� *	bookStock+  $ Genre��	 .  
 .   �� *bookCategory+�� -    ��Name$ SelectBookBtn��$��select book��$
��Left=�$	��Top=�$��20(�$��
160,10,0,0q�$��1��    MahApps.Styles.Button.Square#���   $��40��$	��125���� -   $ 	CancelBtn��$
��Back��$��Right=�$	��Top=�$��20(�$��
0,10,160,0q�$��1��#���   $��40��$	��105��