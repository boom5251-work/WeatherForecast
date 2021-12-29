namespace WeatherForecast.Models

open System



[<Class>]
type public AstronomyViewModel() =
    
    // Астрономические данные (время рассвета и заката).
    member val public Sunrise: TimeOnly = TimeOnly.MinValue with get, set
    member val public Sunset: TimeOnly = TimeOnly.MinValue with get, set


    member public this.IsInited
        with get(): bool = this.Sunrise <> TimeOnly.MinValue && this.Sunset <> TimeOnly.MinValue