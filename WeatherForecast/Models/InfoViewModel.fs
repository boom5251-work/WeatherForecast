namespace WeatherForecast.Models

open System
open System.Collections.Generic



// Модель-представление информации страницы.
[<Class>]
type public InfoViewModel(title: string) =
    
    // Заголовок.
    member val public Title: string = title with get, set
    // Описание.
    member val public Description: string = String.Empty with get, set
    // Ссылки на файлы скриптов.
    member val public ScriptFileNames: List<string> = List<string>() with get, set