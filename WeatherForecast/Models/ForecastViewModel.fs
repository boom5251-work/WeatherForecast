namespace WeatherForecast.Models

open System



// Модель-представление прогноза.
[<Class>]
type public ForecastViewModel() =

    // Дата и время.
    member val public DateTime: DateTime = DateTime.UtcNow with get, set


    interface IConditionViewModel with

        // Время суток.
        member val IsDay: bool = false with get, set

        // Температура воздуха.
        member val Temperature: float = 0. with get, set
        // Температура воздуха (ощущается как).
        member val FeelsLikeTemperature: float = 0. with get, set
    
        // Погодные условия.
        member val WeatherCondition: uint16 = 0us with get, set

    // Проверяет проинициализированы ли значения.
    member public this.IsInited
        with get(): bool =
            let _this: IConditionViewModel = this :> IConditionViewModel
            _this.Temperature <> 0. && _this.FeelsLikeTemperature <> 0. && _this.WeatherCondition <> 0us

    

    
