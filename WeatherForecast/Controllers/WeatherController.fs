namespace WeatherForecast.Controllers

open Microsoft.AspNetCore.Mvc
open WeatherForecast.Models
open WeatherForecast.Data



[<Class; ApiController>]
type public WeatherController() =
    
    inherit Controller()


    [<HttpGet; Route("/")>]
    member public this.Index(): IActionResult =

        let infoViewModel: InfoViewModel = InfoViewModel("Geolocation")
        infoViewModel.Description <- "Grant access to geolocation."
        infoViewModel.ScriptFileNames.Add("geolocation.js")

        this.View("Info", infoViewModel)



    [<HttpGet; Route("/{lat:float},{lon:float}")>]
    member public this.GetWeatherByCoords (lat: float) (lon: float): ViewResult =
        
        let weatherViewModel: WeatherViewModel = WeatherViewModel()
        weatherViewModel.Current <- WeatherApi.getCurrentByCoords lat lon
        weatherViewModel.Forecast <- WeatherApi.getForecastByCoords lat lon
        weatherViewModel.Astronomy <- WeatherApi.getAstronomyByCoords lat lon

        if weatherViewModel.IsInited then
            this.View("Weather", weatherViewModel)
        else
            let infoViewModel: InfoViewModel = InfoViewModel("Not found")
            infoViewModel.Description <- "Couldn't get a forecast for this region."

            this.View("Info", infoViewModel)



    [<HttpGet; Route("/{city}")>]
    member public this.GetWeatherByCity(city: string): ViewResult =
        
        let viewModel: WeatherViewModel = WeatherViewModel()
        viewModel.Current <- WeatherApi.getCurrentByCity city
        viewModel.Forecast <- WeatherApi.getForecastByCity city
        viewModel.Astronomy <- WeatherApi.getAstronomyByCity city

        if viewModel.IsInited then
            this.View("Weather", viewModel)
        else
            let infoViewModel: InfoViewModel = InfoViewModel("Not found")
            infoViewModel.Description <- "Couldn't get a forecast for this region."

            this.View("Info", infoViewModel)