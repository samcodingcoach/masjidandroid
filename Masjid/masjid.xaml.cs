using Newtonsoft.Json;
using System.Text;
using System.Net.Http;
namespace Masjid.Masjid;

public partial class masjid : ContentPage
{

    string gps;
    string id_mesjid;
    public masjid()
	{
		InitializeComponent();
       
        _ = get_masjiddata(); // Fire and forget
    }

    public class list_data
    {
        public int id_masjid { get; set; }
        public string nama_mesjid { get; set; } = string.Empty;
        public string alamat { get; set; } = string.Empty;
        public string email { get; set; } = string.Empty;
        public string? foto { get; set; }
        public string gps { get; set; } = string.Empty;
        public string kota { get; set; } = string.Empty;
        public string provinsi { get; set; } = string.Empty;
        public bool mushola { get; set; } = true;
        public string created_at { get; set; } = string.Empty;
    }

    public class ApiResponse
    {
        public int count { get; set; }
        public List<list_data> data { get; set; } = new List<list_data>();
        public bool success { get; set; }
    }

    private void LoadMap()
    {
        // Ganti dengan koordinat yang Anda inginkan -0.502464, 117.120475

      

        // URL Embed Google Maps (z=15 adalah level zoom)
        string mapUrl = $"https://maps.google.com/maps?q={gps}&z=15&output=embed";

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
    private async Task get_masjiddata()
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
                    ApiResponse apiResponse = JsonConvert.DeserializeObject<ApiResponse>(json);

                    if (apiResponse != null && apiResponse.success && apiResponse.data != null && apiResponse.data.Count > 0)
                    {
                        list_data row = apiResponse.data[0];
                        id_mesjid = row.id_masjid.ToString();
                        T_NamaMesjid.Text = row.nama_mesjid;
                        T_Alamat.Text = row.alamat;
                        T_Provinsi.Text = row.provinsi;
                        T_Kota.Text = row.kota;
                        T_Update.Text = row.created_at;
                        T_Email.Text = row.email;
                        gps = row.gps;
                        LoadMap();
                    }
                    else
                    {
                        // Handle empty data array or failure response
                        System.Diagnostics.Debug.WriteLine("API returned no data or failed request");
                    }
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine($"HTTP Error: {response.StatusCode}");
                }
            }
        }
        catch (Exception ex)
        {
            // Debug console exception
            System.Diagnostics.Debug.WriteLine($"Exception: {ex.Message}");
        }
    }


}