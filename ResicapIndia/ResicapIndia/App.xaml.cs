using Xamarin.Forms.Xaml;

//[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace ResicapIndia
{
    using Services;
    using System.Threading.Tasks;
    using Xamarin.Forms;
    using ViewModels.Base;
    using Views;
    public partial class App : Application
    {
        public bool UseMockServices { get; set; }

        public App()
        {
            InitializeComponent();


            //MainPage = new MainPage();
            InitApp();

            if (Device.RuntimePlatform == Device.UWP)
            {
                InitNavigation();
            }
        }

        private void InitApp()
        {
            UseMockServices = true;
            ViewModelLocator.RegisterDependencies(UseMockServices);
        }


        private Task InitNavigation()
        {
            var navigationService = ViewModelLocator.Resolve<INavigationService>();
            return navigationService.InitializeAsync();
        }

        protected override async void OnStart()
        {
            if (Device.RuntimePlatform != Device.UWP)
            {
                await InitNavigation();
            }

            base.OnResume();
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
