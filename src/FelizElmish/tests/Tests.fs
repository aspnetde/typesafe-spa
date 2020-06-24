module Tests

open Fable.Mocha
open App

let appTests = testList "App tests" [
    testCase "sample" <| fun _ ->
        Expect.equal 1 1 "Expected 1=1"
]

let allTests = testList "All" [
    appTests
]

[<EntryPoint>]
let main (args: string[]) = Mocha.runTests allTests
