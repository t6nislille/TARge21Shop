using TARge21Shop.Core.Dto.WeatherDtos;

namespace TARge21Shop.Core.ServiceInterface
{
    public interface IWeatherForecastServices
    {
        Task<WeatherResultDto> WeatherDetail(WeatherResultDto dto);
    }
}
