namespace WeatherForecast.Data

open System
open System.Collections.Generic
open System.Net.Http
open System.Text.Json
open System.Threading.Tasks
open WeatherForecast.Models



/// <sammary>Модуль, отвечающий за работу с сервисов WeatherApi.</sammary>
module public WeatherApi =
    
    let private apiKey: string = "d4b9d636ed454ab68e4121950212512"
    let private webSite: string = "https://api.weatherapi.com"
    let private currentUrl: string = $"{webSite}/v1/current.json?key={apiKey}"
    let private forecastUrl: string = $"{webSite}/v1/forecast.json?key={apiKey}"
    let private astronomyUrl: string = $"{webSite}/v1/astronomy.json?key={apiKey}"
    let private applicationJson: string = "application/json"



    /// <sammary>Приведение координат к строковому формату агрумента.</sammary>
    /// <param name="lat">Широта.</param>
    /// <param name="lon">Долгота.</param>
    let private coordsToString (lat: float) (lon: float): string =
        
        let latStr: string = lat.ToString().Replace(',', '.')
        let lonStr: string = lon.ToString().Replace(',', '.')
        $"{latStr},{lonStr}"



    /// <sammary>Парсинг json-документа, полученного из ответа.</sammary>
    /// <param name="response">Отвевт на HTTP-запрос.</param>
    let private parseJson(response: HttpResponseMessage): JsonDocument =
        
        let contentType: string = response.Content.Headers.ContentType.MediaType
        let mutable jsonDocument: JsonDocument = null

        if response.IsSuccessStatusCode && contentType = applicationJson then do
            let task: Task<string> = response.Content.ReadAsStringAsync()
            task.Wait()

            // Парсинг json-документа.
            jsonDocument <- JsonDocument.Parse(task.Result)

        jsonDocument



    /// <sammary>Отправка GET-запросов.<sammary>
    /// <param name=""url>Путь HTTP-запроса.</param>
    let public sendHttpGetRequest(url: string): JsonDocument =
        
        async {
            use httpClient = new HttpClient()
            let! response = httpClient.GetAsync(url) |> Async.AwaitTask

            return parseJson response

        } |> Async.RunSynchronously



    /// <sammary>Получение текущей погоды.</sammary>
    /// <param name="lat">Широта.</param>
    /// <param name="lon">Долгота.</param>
    let public getCurrentByCoords (lat: float) (lon: float): CurrentWeatherViewModel =
        
        let fullUrl: string = currentUrl + $"&q={coordsToString lat lon}&aqi=no"
        let jsonDocument: JsonDocument = sendHttpGetRequest fullUrl

        let mutable viewModel: CurrentWeatherViewModel = CurrentWeatherViewModel()

        if not(isNull jsonDocument) then do
            viewModel <- Modeler.createCurrentWeatherViewModel jsonDocument

        viewModel



    /// <sammary>Получение текущей погоды.</sammary>
    /// <param name="city">Название города.</param>
    let public getCurrentByCity(city: string): CurrentWeatherViewModel =
    
        let fullUrl: string = currentUrl + $"&q={city}&aqi=no"
        let jsonDocument: JsonDocument = sendHttpGetRequest fullUrl

        let mutable viewModel: CurrentWeatherViewModel = CurrentWeatherViewModel()
        
        if not(isNull jsonDocument) then do
            viewModel <- Modeler.createCurrentWeatherViewModel jsonDocument
        
        viewModel



    /// <sammary>Получение прогноза на сутки.</sammary>
    /// <param name="lat">Широта.</param>
    /// <param name="lon">Долгота.</param>
    let public getForecastByCoords (lat: float) (lon: float): List<ForecastViewModel> =
        
        let fullUrl: string = forecastUrl + $"&q={coordsToString lat lon}&days=2&aqi=no&alerts=no"
        let jsonDoument: JsonDocument = sendHttpGetRequest fullUrl

        let mutable viewModels: List<ForecastViewModel> = List<ForecastViewModel>()

        if not(isNull jsonDoument) then do
            viewModels <- Modeler.createForecastViewModels jsonDoument

        viewModels


    
    /// <sammary>Получение прогноза на сутки.</sammary>
    /// <param name="city">Название города.</param>
    let public getForecastByCity(city: string): List<ForecastViewModel> =
        
        let fullUrl: string = forecastUrl + $"&q={city}&days=2&aqi=no&alerts=no"

        let jsonDoument: JsonDocument = sendHttpGetRequest fullUrl
        
        let mutable viewModels: List<ForecastViewModel> = List<ForecastViewModel>()
        
        if not(isNull jsonDoument) then do
            viewModels <- Modeler.createForecastViewModels jsonDoument
        
        viewModels



    /// <samary>Получение астрономических данных.</sammary>
    /// <param name="lat">Широта.</param>
    /// <param name="lon">Долгота.</param>
    let public getAstronomyByCoords (lat: float) (lon: float): AstronomyViewModel = 
        
        let dateStr: string = DateTime.Now.ToString("yyyy-MM-dd")
        let fullUrl: string = astronomyUrl + $"&q={coordsToString lat lon}&dt={dateStr}"

        let jsonDocument: JsonDocument = sendHttpGetRequest fullUrl

        let mutable viewModel: AstronomyViewModel = AstronomyViewModel()

        if not(isNull jsonDocument) then do
            viewModel <- Modeler.createAstronomyViewModel jsonDocument

        viewModel



    /// <sammary>Получение астрономических данных.</sammary>
    /// <param name="city">Название города.</param>
    let public getAstronomyByCity(city: string): AstronomyViewModel =
        
        let dateStr: string = DateTime.Now.ToString("yyyy-MM-dd")
        let fullUrl: string = astronomyUrl + $"&q={city}&dt={dateStr}"

        let jsonDocument: JsonDocument = sendHttpGetRequest fullUrl

        let mutable viewModel: AstronomyViewModel = AstronomyViewModel()

        if not(isNull jsonDocument) then do
            viewModel <- Modeler.createAstronomyViewModel jsonDocument

        viewModel