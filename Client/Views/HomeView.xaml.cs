using Client.ViewModels;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Client.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class HomeView : Page
    {

        #region Properties

        public HomeViewModel HomeViewModel { get; set; }

        #endregion


        /// <summary>
        /// Constructor.
        /// </summary>
        public HomeView()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (HomeViewModel == null)
                HomeViewModel = e.Parameter as HomeViewModel;

        }


    }
}
