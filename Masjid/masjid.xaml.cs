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

    public class list_data
    {
        public string id_masjid { get; set; }
        public string nama_masjid { get; set; }
        public string alamat { get; set; }
        public string email { get; set; }
        public string foto { get; set; }
        public string gps { get; set; }
        public string kota { get; set; }
        public string provinsi { get; set; }
        public bool mushola { get; set; } = true;
        public string created_at { get; set; } = string.Empty;
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


    string key;
    private async void get_masjiddata()
    {
        try
        {
            string url = App.API_HOST + "api/masjid.list";
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    List<list_data> rowData = JsonConvert.DeserializeObject<List<list_data>>(json);

                    if (rowData != null && rowData.Count > 0)
                    {
                        list_data row = rowData[0];
                        
                        T_NamaMesjid.Text = row.nama_masjid;
                        T_Alamat.Text = row.alamat;
                        T_Provinsi.Text = row.provinsi;
                        T_Kota.Text = row.kota;
                        T_Update.Text = row.created_at;
                        T_Email.Text = row.email;
                        



                    }
                    else
                    {
                        // Handle jumlah kosong dan grandtotal kosong

                    }
                }
                else
                {

                }
            }
        }

        catch (Exception ex)
        {
            //debug console exception
            
        }
    }


}