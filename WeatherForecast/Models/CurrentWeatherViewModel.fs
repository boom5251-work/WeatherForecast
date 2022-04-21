namespace WeatherForecast.Models

open System



// Модель-представление текущей погоды.
[<Class>]
type public CurrentWeatherViewModel() =
    
    // Населенный пункт.
    member val public LocName: string = String.Empty with get, set
    // Регион.
    member val public Region: string = String.Empty with get, set
    // Страна.
    member val public Country: string = String.Empty with get, set

    // Скорость ветра.
    member val public WindDirection: string = String.Empty with get, set
    // Напрвление ветра.
    member val public WindSpeed: float = 0. with get, set

    // Проверяет проинициализированы ли значения.
    member public this.IsInited
        with get(): bool = this.LocName <> String.Empty && this.WindDirection <> String.Empty && this.WindSpeed <> 0.
    

    interface IConditionViewModel with
        
        // Время суток.
        member val IsDay: bool = false with get, set

        // Температура воздуха.
        member val Temperature: float = 0. with get, set
        // Температура воздуха (ощущается как).
        member val FeelsLikeTemperature: float = 0. with get, set
        // Погодные условия.
        member val WeatherCondition: uint16 = 0us with get, set