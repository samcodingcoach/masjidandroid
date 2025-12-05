namespace Masjid
{
    public partial class App : Application
    {
        public static string? API_HOST { get; set; }
        public App()
        {
            InitializeComponent();
            string url_api = "http://192.168.77.8:5001/"; // Replace with actual API host
            API_HOST = url_api;
           // MainPage = new AppShell();

            MainPage = new NavigationPage(new Masjid.masjid());
        }
    }
}
