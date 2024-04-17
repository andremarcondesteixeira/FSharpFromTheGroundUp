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
    let meanScore = pieces.[2..] |> Array.averageBy float
    (studentName, studentId, meanScore)

let summarize filePath =
    let lines = File.ReadAllLines filePath
    let studentCount = lines.Length - 1

    printfn $"Student count: {studentCount}"

    lines
    |> Array.skip 1
    |> Array.map meanScore
    |> Array.iter (
        fun (studentName, studentId, meanScore) ->
            printfn $"{studentId}: %0.2f{meanScore} - {studentName}"
    )

summarize filePath

printfn "Press any key to exit"
Console.ReadKey () |> ignore
exit 0
