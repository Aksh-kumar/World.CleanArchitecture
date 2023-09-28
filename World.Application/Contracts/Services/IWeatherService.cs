using System.Threading.Tasks;

namespace World.Application.Contracts.Services
{
    public interface IWeatherService : IDisposable
    {
        Task<List<Domain.Entity.WeatherForecast>> getWeatherForcast();
    }
}
