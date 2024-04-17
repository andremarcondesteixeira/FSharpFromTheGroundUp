open System
open System.IO

let commandLineArgs = Environment.GetCommandLineArgs () // () is unit

if commandLineArgs.Length < 2 then
    printfn "Please provide a file path"
    exit 1

let filePath = commandLineArgs.[1]

if not (File.Exists filePath) then
    printfn $"Provided file does not exist: {filePath}"
    exit 1

let meanScore (row : string) =
    let pieces = row.Split("\t")
    let studentName = pieces.[0]
    let studentId = pieces.[1]
    let scores = Array.map float pieces[2..]
    let meanScore = Array.average scores
    let minScore = Array.min scores
    let maxScore = Array.max scores
    (studentName, studentId, meanScore, minScore, maxScore)

let summarize filePath =
    let lines = File.ReadAllLines filePath
    let studentCount = lines.Length - 1

    printfn $"Student count: {studentCount}"

    lines
    |> Array.skip 1
    |> Array.map meanScore
    |> Array.iter (
        fun (studentName, studentId, meanScore, minScore, maxScore) ->
            // the format specifier has the following format:
            // %<0 if want to pad left><total length of number including numbers after the point>.<amount of numbers after the point>
            printfn $"{studentId}: mean: %06.2f{meanScore}, min: %06.2f{minScore}, max: %06.2f{maxScore} - {studentName}"
    )

summarize filePath

printfn "Press any key to exit"
Console.ReadKey () |> ignore
exit 0
