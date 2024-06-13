using System.Text.Json;
using System.Windows.Input;
using MauiWeather.MVVM.Models;
using PropertyChanged;

namespace MauiWeather.MVVM.ViewModels;

[AddINotifyPropertyChangedInterface]
public class WeatherViewModel
{
    public WeatherData WeatherData { get; set; }

    public string PlaceName { get; set; }

    public DateTime Date => DateTime.Now;

    private HttpClient client;

    public WeatherViewModel()
    {
        client = new HttpClient();
    }

    public ICommand SearchCommand =>
        new Command(async (searchText) =>
            {
                PlaceName = searchText.ToString();

                var location = await GetCoordinatesAsync(searchText.ToString());

                await GetWeatherAsync(location);
            });

    private async Task<Location> GetCoordinatesAsync(string address)
    {
        IEnumerable<Location> locations = await Geocoding.Default.GetLocationsAsync(address);

        Location location = locations?.FirstOrDefault();

        if (location is not null) Console.WriteLine($"Latitude: {location.Latitude}, Longitude: {location.Longitude}, Altitude: {location.Altitude}");

        return location;
    }

    public async Task GetWeatherAsync(Location location)
    {
        string latitudeStr = location.Latitude.ToString().Replace(',', '.');
        string longitudeStr = location.Longitude.ToString().Replace(',', '.');

        var url = $"https://api.open-meteo.com/v1/forecast?latitude={latitudeStr}&longitude={longitudeStr}&daily=weather_code,temperature_2m_max,temperature_2m_min&timezone=America%2FChicago";

        var response = await client.GetAsync(url);

        if (response.IsSuccessStatusCode)
        {
            using (var responseStream = await response.Content.ReadAsStreamAsync())
            {
                var data = await JsonSerializer.DeserializeAsync<WeatherData>(responseStream);
                WeatherData = data;
            }
        }
    }

}
