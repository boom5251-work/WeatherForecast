namespace WeatherForecast.Models.Extentions

open System.Collections.Generic
open System.Runtime.CompilerServices
open WeatherForecast.Models



[<Extension>]
type public ForecastViewModelsExtension() =
    
    [<Extension>]
    static member public IsInited (ty: List<ForecastViewModel>): bool =
        
        let mutable resutl: bool = true

        for item: ForecastViewModel in ty do
            
            if (not item.IsInited) then do
                resutl <- false

        resutl
        

