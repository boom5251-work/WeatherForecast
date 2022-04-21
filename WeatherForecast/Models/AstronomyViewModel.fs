namespace WeatherForecast.Models

open System



/// Модель-представление астрономических данных.
[<Class>]
type public AstronomyViewModel() =
    
    
    // Время восхода солнаца.
    member val public Sunrise: TimeOnly = TimeOnly.MinValue with get, set
    // Время захода солнца.
    member val public Sunset: TimeOnly = TimeOnly.MinValue with get, set

    // Проверяет проинициализированы ли значения.
    member public this.IsInited
        with get(): bool = this.Sunrise <> TimeOnly.MinValue && this.Sunset <> TimeOnly.MinValue