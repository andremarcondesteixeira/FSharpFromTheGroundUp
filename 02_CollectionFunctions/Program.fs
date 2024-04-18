open System
open System.IO
open System.Globalization

let commandLineArgs = Environment.GetCommandLineArgs () // () is unit

if commandLineArgs.Length < 2 then
    printfn "Please provide a file path"
    exit 1

let filePath = commandLineArgs.[1]

if not (File.Exists filePath) then
    printfn $"Provided file does not exist: {filePath}"
    exit 1

module Float =
    let tryFromString (s: string): float option =
        let isNumber, number = Double.TryParse(s, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture)
        
        if isNumber then
            Some(number)
        else
            None

    let fromStringOr (defaultValue: float) (s: string) : float =
        s |> tryFromString |> Option.defaultValue defaultValue

// this is a record type
type StudentScoresSummary = {
    Name: string
    Id: string
    MeanScore: float
    MinScore: float
    MaxScore: float
}

module StudentScoresSummary =
    let fromString (s: string): StudentScoresSummary =
        let pieces = s.Split("\t")
        let name = pieces.[0]
        let id = pieces.[1]
        let scores = pieces[2..] |> Array.map (Float.fromStringOr 50)
        let meanScore = Array.average scores
        let minScore = Array.min scores
        let maxScore = Array.max scores
        
        {
            Name = name
            Id = id
            MeanScore = meanScore
            MinScore = minScore
            MaxScore = maxScore
        }

    let printSummary (summary: StudentScoresSummary) =
        // the %0.6f format specifier has the following format:
        // %<0 if want to pad left><total length of number including numbers after the point>.<amount of numbers after the point>
        printfn "%s: mean: %06.2f, min: %06.2f, max: %06.2f - %s"
            summary.Id
            summary.MeanScore
            summary.MinScore
            summary.MaxScore
            summary.Name
    

let summarize filePath =
    let lines = File.ReadAllLines filePath
    let studentCount = lines.Length - 1

    printfn $"Student count: {studentCount}"

    lines
    |> Array.skip 1
    |> Array.map StudentScoresSummary.fromString
    |> Array.sortByDescending (fun summary -> summary.MeanScore)
    |> Array.iter StudentScoresSummary.printSummary

summarize filePath

printfn "Press any key to exit"
Console.ReadKey () |> ignore
exit 0
