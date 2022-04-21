namespace WeatherForecast.Models

open System
open System.Collections.Generic
open System.Linq
open WeatherForecast.Dependencies
open WeatherForecast.Models.Extentions



// Модель-представления, объединяющая все остальные.
[<Class>]
type public WeatherViewModel() =

    // Модель-представление текущей погоды.
    member val public Current: CurrentWeatherViewModel = CurrentWeatherViewModel() with get, set
    // Моедли-представления прогноза.
    member val public Forecast: List<ForecastViewModel> = List<ForecastViewModel>() with get, set
    // Модель-представление астрономических данных.
    member val public Astronomy: AstronomyViewModel = AstronomyViewModel() with get, set


    // Проверяет проинициализированы ли значения.
    member public this.IsInited
        with get(): bool = this.Current.IsInited && this.Forecast.IsInited() && this.Astronomy.IsInited


    // Получение названия файла иконки.
    member public this.GetSvgFileName (service: IConditionIconService) (conditionViewModel: IConditionViewModel): string = 
        
        let svgFileNames: List<string> = service.GetIconFilesByCode conditionViewModel.WeatherCondition
        let mutable svgFileName: string = String.Empty

        if not conditionViewModel.IsDay then do
            svgFileName <- svgFileNames.Last()
        else
            // Дневная иконка для солнечной погоды зависит от температуры.
            if conditionViewModel.WeatherCondition = 1000us && conditionViewModel.Temperature > 30. then do
                svgFileName <- svgFileNames.[1]
            else
                svgFileName <- svgFileNames.First()

        svgFileName



    // Выбор файла стилей на основе времени суток и температуры.
    member public this.GetStylesFileName(): string =
        
        let mutable fileName: string = "blue.css"
        let current: IConditionViewModel = this.Current :> IConditionViewModel

        if not current.IsDay then do
            fileName <- "dark.css"
        else
            let temperature: float = current.Temperature

            if 0. <= temperature && temperature < 20. then do
                fileName <- "green.css"
            elif 20. <= temperature && temperature < 30. then do
                fileName <- "brown.css"
            elif 30. <= temperature then do
                fileName <- "orange.css"

        fileName