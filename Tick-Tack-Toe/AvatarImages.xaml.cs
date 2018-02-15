using System;
using System.Collections.Generic;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Tick_Tack_Toe
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AvatarImages : Page
    {
        public Dictionary<int, Uri> DictionaryImages = new Dictionary<int, Uri>();

        public Player player = new Player();
        public System.Uri NewAvatarUri;
        public Payload payload1 = new Payload();
        public AvatarImages()
        {
            this.InitializeComponent();
            AddDictionaryToGrid();
        }

        private void App_BackRequested(object sender, Windows.UI.Core.BackRequestedEventArgs e)
        {
            Frame frame = Window.Current.Content as Frame;
            if (frame.CanGoBack)
            {
                frame.Navigate(typeof(GamePage));
                e.Handled = true; // указываем, что событие обработано
            }
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            Frame frame = Window.Current.Content as Frame;
            if (frame.CanGoBack)
            {
                SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility =
                    AppViewBackButtonVisibility.Visible;
                Windows.UI.Core.SystemNavigationManager.GetForCurrentView().BackRequested += App_BackRequested;
            }
            else
            {
                SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility =
                    AppViewBackButtonVisibility.Collapsed;
            }
            if ((e.Parameter != null) && (e.Parameter.ToString() != ""))
            {
                player = (Player)e.Parameter;
            }

        }

        private void SetImage_OnClick(object sender, RoutedEventArgs e)
        {
            player.PlayerImage = NewAvatarUri;
            payload1.playerCross = player;
            payload1.playerZero = null;
            Frame.Navigate(typeof(GamePage), payload1);

        }

        private void AddDictionaryToGrid()
        {
            System.Uri uri = new System.Uri("ms-appx:///Assets/Images/Cat.png");
            DictionaryImages.Add(0, uri);
            System.Uri uri1 = new System.Uri("ms-appx:///Assets/Wide310x150Logo.scale-200.png");
            DictionaryImages.Add(1, uri1);
            System.Uri uri2 = new System.Uri("ms-appx:///Assets/Images/Dog.png");
            DictionaryImages.Add(2, uri2);
            System.Uri uri3 = new System.Uri("ms-appx:///Assets/Images/Food1.jpg");
            DictionaryImages.Add(3, uri3);
            System.Uri uri4 = new System.Uri("ms-appx:///Assets/StoreLogo.png");
            DictionaryImages.Add(4, uri4);
            System.Uri uri5 = new System.Uri("ms-appx:///Assets/Images/Food2.jpg");
            DictionaryImages.Add(5, uri5);
            System.Uri uri6 = new System.Uri("ms-appx:///Assets/Images/Cat.png");
            DictionaryImages.Add(6, uri6);
            System.Uri uri7 = new System.Uri("ms-appx:///Assets/StoreLogo.png");
            DictionaryImages.Add(7, uri7);
            System.Uri uri8 = new System.Uri("ms-appx:///Assets/Images/Dog.png");
            DictionaryImages.Add(8, uri8);

            int z = 0;
            for (int i = 0; i < 3; i++)
            {
                RowDefinition c2 = new RowDefinition();
                c2.Height = new GridLength(1, GridUnitType.Star);
                Grid_AvatarImages.RowDefinitions.Add(c2);
            }
            for (int j = 0; j < 3; j++)
            {
                ColumnDefinition c1 = new ColumnDefinition();
                c1.Width = new GridLength(1, GridUnitType.Star);
                Grid_AvatarImages.ColumnDefinitions.Add(c1);
            }

            List<Border> BorderList = new List<Border>();
            while (z < 9)
            {
                for (int k = 0; k < 3; k++) 
                {
                    for (int i = 0; i < 3; i++)
                    {
                        Image image = new Image();
                        Border border = new Border();
                        System.Uri temp = DictionaryImages[z];
                        image.Source = new BitmapImage(temp);
                        image.Height = 100;
                        image.Stretch = Stretch.Uniform;
                        border.BorderThickness = new Thickness(4);
                        border.BorderBrush = new SolidColorBrush(Windows.UI.Colors.Gray);
                        border.Child = image;
                        border.Tapped += (s, e) =>
                        {
                            int row = Grid.GetRow(border);
                            int column = Grid.GetColumn(border);
                            int index;
                            border.BorderBrush = new SolidColorBrush(Windows.UI.Colors.Red);
                            switch (row)
                            {
                                case 0:
                                    index = row + column;
                                    NewAvatarUri = DictionaryImages[index];
                                    for (int t = 0; t < 9; t++)
                                    {
                                        BorderList[t].BorderBrush = new SolidColorBrush(Windows.UI.Colors.Gray);
                                        if (t == index)
                                        {
                                            BorderList[t].BorderBrush = new SolidColorBrush(Windows.UI.Colors.Red);
                                        }
                                    }
                                    break;
                                case 1:
                                    index = 2 + row + column;
                                    NewAvatarUri = DictionaryImages[index];
                                    for (int t = 0; t < 9; t++)
                                    {
                                        BorderList[t].BorderBrush = new SolidColorBrush(Windows.UI.Colors.Gray);
                                        if (t == index)
                                        {
                                            BorderList[t].BorderBrush = new SolidColorBrush(Windows.UI.Colors.Red);
                                        }
                                    }
                                    break;
                                case 2:
                                    index = 4 + row + column;
                                    NewAvatarUri = DictionaryImages[index];
                                    for (int t = 0; t < 9; t++)
                                    {
                                        BorderList[t].BorderBrush = new SolidColorBrush(Windows.UI.Colors.Gray);
                                        if (t == index)
                                        {
                                            BorderList[t].BorderBrush = new SolidColorBrush(Windows.UI.Colors.Red);
                                        }
                                    }
                                    break;
                            }
                        };
                        BorderList.Add(border);
                        Grid_AvatarImages.Children.Add(BorderList[z]);
                        Grid.SetRow(BorderList[z], k);
                        Grid.SetColumn(BorderList[z], i);
                        z++;
                    }
                }
            }
        }

    }
}
