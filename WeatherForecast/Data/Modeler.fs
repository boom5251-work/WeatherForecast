namespace WeatherForecast.Data

open System
open System.Collections.Generic
open System.Globalization
open System.Linq
open System.Text.Json
open WeatherForecast.Models



module public Modeler =

    let private dateTimePattern: string = "yyyy-MM-dd H:mm"
    let private timePattern: string = "h:mm tt"



    // Извелчение данных о местоположении и времени.
    let private initLocation (jsonDocument: JsonDocument) (viewModel: CurrentWeatherViewModel): unit =
        
        let location: JsonElement = jsonDocument.RootElement.GetProperty("location")
        
        viewModel.LocName <- location.GetProperty("name").GetString()
        viewModel.Region <- location.GetProperty("region").GetString()
        viewModel.Country <- location.GetProperty("country").GetString()



    // Извлечение данных о температуре, скорости вестра, времени суток и погодных условиях.
    let private initCondition (jsonDocument: JsonDocument) (viewModel: CurrentWeatherViewModel): unit =
        
        let current: JsonElement = jsonDocument.RootElement.GetProperty("current")
        let conditionViewModel: IConditionViewModel = viewModel :> IConditionViewModel

        conditionViewModel.IsDay <- current.GetProperty("is_day").GetInt32() <> 0

        conditionViewModel.Temperature <- current.GetProperty("temp_c").GetDouble()
        conditionViewModel.FeelsLikeTemperature <- current.GetProperty("feelslike_c").GetDouble()

        conditionViewModel.WeatherCondition <- uint16(current.GetProperty("condition").GetProperty("code").GetInt32())

        conditionViewModel.IsDay <- current.GetProperty("is_day").GetInt32() <> 0

        viewModel.WindDirection <- current.GetProperty("wind_dir").GetString()
        viewModel.WindSpeed <- current.GetProperty("wind_kph").GetDouble()



    // Извлечение прогноза на сутки.
    let public initForecast (hours: List<JsonElement>) (localNow: DateTime) (viewModels: List<ForecastViewModel>): unit =
        
        // Прошедшие часы пропускаются.
        let hours: List<JsonElement> = hours.Skip(localNow.Hour).ToList()
        
        // Диапазон прогноза 24 часа, шаг прогноза 4 часа.
        for i: int in 3..4..24 do
            let viewModel: ForecastViewModel = ForecastViewModel()
            let conditionViewModel: IConditionViewModel = viewModel :> IConditionViewModel

            let dateTime: string = hours[i].GetProperty("time").GetString()
            viewModel.DateTime <- DateTime.ParseExact(dateTime, dateTimePattern, CultureInfo.InvariantCulture)

            conditionViewModel.IsDay <- hours[i].GetProperty("is_day").GetInt32() <> 0

            conditionViewModel.Temperature <- hours[i].GetProperty("temp_c").GetDouble()
            conditionViewModel.FeelsLikeTemperature <- hours[i].GetProperty("feelslike_c").GetDouble()

            conditionViewModel.WeatherCondition <- uint16(hours[i].GetProperty("condition").GetProperty("code").GetInt32())

            viewModels.Add(viewModel)



    // Извлечение автрономических данных.
    let public initAstronomy (jsonDocument: JsonDocument) (viewModel: AstronomyViewModel): unit =

        let astro: JsonElement = jsonDocument.RootElement.GetProperty("astronomy").GetProperty("astro")

        let sunriseStr: string = astro.GetProperty("sunrise").GetString()
        let sunsetStr: string = astro.GetProperty("sunset").GetString()

        let sunriseDateTime: DateTime = DateTime.ParseExact(sunriseStr, timePattern, CultureInfo.InvariantCulture)
        let sunsetDateTime: DateTime = DateTime.ParseExact(sunsetStr, timePattern, CultureInfo.InvariantCulture)

        viewModel.Sunrise <- TimeOnly.FromDateTime(sunriseDateTime)
        viewModel.Sunset <- TimeOnly.FromDateTime(sunsetDateTime)

        

    // Создание модели-представления погоды.
    let public createCurrentWeatherViewModel(jsonDoucment: JsonDocument): CurrentWeatherViewModel =

        let viewModel: CurrentWeatherViewModel = CurrentWeatherViewModel()
        initLocation jsonDoucment viewModel
        initCondition jsonDoucment viewModel
        viewModel



    // Создание модели-представления прогноза на сутки.
    let public createForecastViewModels(jsonDocument: JsonDocument): List<ForecastViewModel> =
        
        let hours: List<JsonElement> = List<JsonElement>()

        let forecastDaysArr: JsonElement = 
            jsonDocument.RootElement.GetProperty("forecast").GetProperty("forecastday")

        // Получение локальной даты и времени.
        let localNowStr: string =
            jsonDocument.RootElement.GetProperty("location").GetProperty("localtime").GetString()

        let localNow: DateTime = DateTime.ParseExact(localNowStr, dateTimePattern, CultureInfo.InvariantCulture)
        

        // Перебор дней.
        for day: JsonElement in forecastDaysArr.EnumerateArray() do
            let hoursArr: JsonElement = day.GetProperty("hour")

            // Объединение двух дней.
            hours.AddRange(hoursArr.EnumerateArray())


        let viewModels: List<ForecastViewModel> = List<ForecastViewModel>()
        initForecast hours localNow viewModels
        viewModels



    // Создание модели-представления для астрономических данных.
    let public createAstronomyViewModel(jsonDocument: JsonDocument): AstronomyViewModel =
        
        let viewModel: AstronomyViewModel = AstronomyViewModel()
        initAstronomy jsonDocument viewModel
        viewModel