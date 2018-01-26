using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Tick_Tack_Toe
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            this.NavigationCacheMode = Windows.UI.Xaml.Navigation.NavigationCacheMode.Enabled;
        }

        public Player PlayerCross = new Player();
        public Player PlayerZero = new Player();
        public Payload payload = new Payload();

        private void SetPlayersName_OnClick(object sender, RoutedEventArgs e)
        {
            string firstName = SetFirstName.Text;
            string secondName = SetSecondName.Text;
            if (((String.IsNullOrEmpty(firstName) == false) && (String.IsNullOrEmpty(secondName) == false)) && (String.Equals(firstName, secondName) == false))
            {
                PlayerCross.Name = SetFirstName.Text;
                PlayerZero.Name = SetSecondName.Text;
                payload.playerCross = PlayerCross;
                payload.playerZero = PlayerZero;
                this.Frame.Navigate(typeof(GamePage), payload);
            }
        }
    }
}
