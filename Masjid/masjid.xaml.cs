using Newtonsoft.Json;
using System.Text;
using System.Net.Http;
namespace Masjid.Masjid;

public partial class masjid : ContentPage
{
	public masjid()
	{
		InitializeComponent();
        LoadMap();
    }

    private void LoadMap()
    {
        // Ganti dengan koordinat yang Anda inginkan -0.502464, 117.120475

        double lat = -0.502464;
        double lng = 117.120475;

        // URL Embed Google Maps (z=15 adalah level zoom)
        string mapUrl = $"https://maps.google.com/maps?q={lat},{lng}&z=15&output=embed";

        // Bungkus URL dalam iframe HTML agar responsif di WebView
        var htmlSource = new HtmlWebViewSource();
        htmlSource.Html = $@"
            <html>
            <head>
            <meta name='viewport' content='width=device-width, initial-scale=1.0'>
            <style>
                body, html {{ margin: 0; padding: 0; height: 100%; overflow: hidden; }}
                iframe {{ width: 100%; height: 100%; border: 0; }}
            </style>
            </head>
            <body>
                <iframe src='{mapUrl}' allowfullscreen></iframe>
            </body>
            </html>";

        MapWebView.Source = htmlSource;
    }
}