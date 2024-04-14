open System

// For more information see https://aka.ms/fsharp-console-apps


// --------------------------------------
// COMMAND LINE ARGUMENTS ---------------
// --------------------------------------
let commandLineArgs = Environment.GetCommandLineArgs()
for arg in commandLineArgs do
    printfn $"arg = {arg}"



// --------------------------------------
// MUTABLE PERSON -----------------------
// --------------------------------------
let mutable person = String.Empty

if commandLineArgs.Length > 1 then
    person <- commandLineArgs.[1]
else
    Console.WriteLine("What's your name, motherfucker?")
    person <- Console.ReadLine()

printfn $"Hello, {person}"


// --------------------------------------
// NOT MUTABLE PERSON IS BETTER ---------
// --------------------------------------
let notMutablePerson =
    if commandLineArgs.Length > 1 then
        commandLineArgs.[1]
    else
        Console.WriteLine("What's your name, bitch?")
        Console.ReadLine()
printfn $"Hello, {notMutablePerson}"


// --------------------------------------
// PRESS A KEY TO EXIT ------------------
// --------------------------------------
Console.ReadKey() |> ignore
exit 0
