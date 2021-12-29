namespace WeatherForecast.Models

open System



[<Class>]
type public ForecastViewModel() =

    // Дата и время.
    member val public DateTime: DateTime = DateTime.UtcNow with get, set


    interface IConditionViewModel with

        // Время суток
        member val IsDay: bool = false with get, set

        // Температура.
        member val Temperature: float = 0 with get, set
        member val FeelsLikeTemperature: float = 0 with get, set
    
        // Погодные условия
        member val WeatherCondition: uint16 = 0us with get, set


    member public this.IsInited
        with get(): bool =
            let _this: IConditionViewModel = this :> IConditionViewModel
            _this.Temperature <> 0 && _this.FeelsLikeTemperature <> 0 && _this.WeatherCondition <> 0us

    

    
