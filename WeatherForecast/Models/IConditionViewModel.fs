namespace WeatherForecast.Models



// Интерфейс текщей погоды.
[<Interface>]
type public IConditionViewModel = 
    
    // Указывает на время суток (день или ночь).
    abstract member IsDay: bool with get, set
    // Температура воздуха.
    abstract member Temperature: float with get, set
    // Температура воздуха (ощущается как).
    abstract member FeelsLikeTemperature: float with get, set
    // Погодные условия.
    abstract member WeatherCondition: uint16 with get, set