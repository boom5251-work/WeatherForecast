namespace WeatherForecast.Models

open System



[<Class>]
type public CurrentWeatherViewModel() =
    
    let empty = String.Empty

    // Местоположение (населенный пункт, регион, страна).
    member val public LocName: string = empty with get, set
    member val public Region: string = empty with get, set
    member val public Country: string = empty with get, set

    // Скорость и направление ветра.
    member val public WindDirection: string = String.Empty with get, set
    member val public WindSpeed: float = 0 with get, set


    member public this.IsInited
        with get(): bool = this.LocName <> empty && this.WindDirection <> empty && this.WindSpeed <> 0
    

    interface IConditionViewModel with
        
        // Время суток.
        member val IsDay: bool = false with get, set

        // Температура.
        member val Temperature: float = 0 with get, set
        member val FeelsLikeTemperature: float = 0 with get, set

        member val WeatherCondition: uint16 = 0us with get, set