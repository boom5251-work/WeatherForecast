namespace WeatherForecast.Models



[<Interface>]
type public IConditionViewModel = 
    
    abstract member IsDay: bool with get, set

    abstract member Temperature: float with get, set
    abstract member FeelsLikeTemperature: float with get, set

    abstract member WeatherCondition: uint16 with get, set