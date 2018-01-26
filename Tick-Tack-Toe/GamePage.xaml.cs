using System;
using Windows.UI;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Tick_Tack_Toe
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class GamePage : Page
    {
        private Game game = new Game();
        private Payload payload;
        private Player FirstPlayer = new Player();
        private Player SecondPlayer = new Player();
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            Frame frame = Window.Current.Content as Frame;
            SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility =
                AppViewBackButtonVisibility.Collapsed;
            if ((e.Parameter != null) && (e.Parameter.ToString() != ""))
            {
                payload = (Payload)e.Parameter;
            }
            PrintPlayersNames();
            SetAvatarImage();
        }
        public GamePage()
        {
            this.InitializeComponent();
            this.NavigationCacheMode = Windows.UI.Xaml.Navigation.NavigationCacheMode.Enabled;
            game.GameFieldClean();
            CreateGridRowsAndColumns();
            CreateGameFieldButtons();
            CheckPlayerQueueStroke();
        }
        private void PrintPlayersNames()
        {
            if((payload.playerCross != null) && (payload.playerZero != null))
            {
                FirstPlayer = payload.playerCross;
                SecondPlayer = payload.playerZero;
                string NickOne = FirstPlayer.Name;
                string NickTwo = SecondPlayer.Name;
                FirstPlayerName.Text = NickOne;
                SecondPlayerName.Text = NickTwo;
            } 
        }
        private void SetAvatarImage()
        {
            if (payload.playerZero == null)
            {
                if (FirstPlayer.Name == payload.playerCross.Name)
                {
                    FirstPlayer = payload.playerCross;
                    if (FirstPlayer.PlayerImage != null)
                    {
                        FirstPlayerAvatar.Source = new BitmapImage(new Uri(this.BaseUri, FirstPlayer.PlayerImage));
                        FirstPlayerAvatar.Stretch = Stretch.Uniform;
                    }  
                }
                else
                {
                    SecondPlayer = payload.playerCross;
                    if (SecondPlayer.PlayerImage != null)
                    {
                        SecondPlayerAvatar.Source = new BitmapImage(new Uri(this.BaseUri, SecondPlayer.PlayerImage));
                        SecondPlayerAvatar.Stretch = Stretch.Uniform;
                    }
                }
            }
        }

        private void NewGame_Button_OnClick(object sender, RoutedEventArgs e)
        {
           game.GameFieldClean();
           GameField_Grid.Children.Clear();
           CreateGameFieldButtons();
           CheckPlayerQueueStroke();
           ShowWin.Text = "";
        }

        private void CreateGameFieldButtons()
        {
            for (int i = 0; i < Game.GAME_HEIGTH_FIELD; i++)
            {
                for (int j = 0; j < Game.GAME_WIDTH_FIELD; j++)
                {
                    Button button = new Button();
                    button.MinHeight = 85;
                    button.MinWidth = 85;
                    button.HorizontalAlignment = HorizontalAlignment.Center;
                    button.VerticalAlignment = VerticalAlignment.Center;
                    GameField_Grid.Children.Add(button);
                    Grid.SetRow(button, i);
                    Grid.SetColumn(button, j);
                    button.Click += (s, e) =>
                    {
                        if (game.CheckCellIsNotEmpty(Grid.GetRow(button), Grid.GetColumn(button)) == false)
                        {
                            if ((game.SequencingStroke() == Game.CellStateCondition.Cross) && (game.GameIsWin == false))
                            {
                                ShowQueueStroke.Text = "Сейчас ходят нолики";
                                game.GameField[Grid.GetRow(button), Grid.GetColumn(button)] = game.SequencingStroke();
                                button.Content = new FontIcon
                                {
                                    FontFamily = new FontFamily("Segoe MDL2 Assets"),
                                    Glyph = "\uE106",
                                    Foreground = new SolidColorBrush(Colors.Black)
                                };
                                ShowWinMessage();
                            }
                            if ((game.SequencingStroke() == Game.CellStateCondition.Zero) && (game.GameIsWin == false))
                            {
                                ShowQueueStroke.Text = "Сейчас ходят крестики";
                                game.GameField[Grid.GetRow(button), Grid.GetColumn(button)] = game.SequencingStroke();
                                button.Content = new FontIcon
                                {
                                    FontFamily = new FontFamily("Segoe MDL2 Assets"),
                                    Glyph = "\uEA3A",
                                    Foreground = new SolidColorBrush(Colors.Black)
                                };
                                ShowWinMessage();
                            }
                            game.countStroke++;
                        }

                    };
                }

            }
        }
        private void CreateGridRowsAndColumns()
        {
            for (int i = 0; i < Game.GAME_HEIGTH_FIELD; i++)
            {
                RowDefinition c2 = new RowDefinition();
                c2.Height = new GridLength(1, GridUnitType.Star);
                GameField_Grid.RowDefinitions.Add(c2);
            }
            for (int j = 0; j < Game.GAME_WIDTH_FIELD; j++)
            {
                ColumnDefinition c1 = new ColumnDefinition();
                c1.Width = new GridLength(1, GridUnitType.Star);
                GameField_Grid.ColumnDefinitions.Add(c1);
            }
        }
        private void CheckPlayerQueueStroke()
        {
            if (game.SequencingStroke() == Game.CellStateCondition.Cross)
            {
                ShowQueueStroke.Text = "Сейчас ходят крестики";
            }
            if (game.SequencingStroke() == Game.CellStateCondition.Zero)
            {
                ShowQueueStroke.Text = "Сейчас ходят нолики";
            }
        }

        private void ShowWinMessage()
        {
            if ((game.GameIsWin) && (game.SequencingStroke() == Game.CellStateCondition.Cross))
            {
               ShowWin.Text = "Победили крестики";
               ShowQueueStroke.Text = "";
            }
            if ((game.GameIsWin) && (game.SequencingStroke() == Game.CellStateCondition.Zero))
            {
                ShowWin.Text = "Победили нолики";
                ShowQueueStroke.Text = "";
            }
            if ((game.CheckFieldHaveEmptyCells() == false))
            {
                ShowWin.Text = "Ничья";
                ShowQueueStroke.Text = "";
            }
        }

        private void SecondPlayerAvatar_OnTapped(object sender, TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(AvatarImages), SecondPlayer);
        }

        private void FirstPlayerAvatar_OnTapped(object sender, TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(AvatarImages), FirstPlayer);
        }
    }
}
