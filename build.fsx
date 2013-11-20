#I @"packages/FAKE/tools/"
#r @"FakeLib.dll"

open Fake
open System
open System.IO

let buildDir = "./build"
let nugetDir = "./packages"
let packagesDir = "./packages"

Target "Clean" (fun _ -> CleanDirs [buildDir])

Target "BuildSolution" (fun _ ->
    MSBuildWithDefaults "Build" ["./COMA.nproj"]
    |> Log "AppBuild-Output: "
)

Target "Success" (fun _ -> ())

"Clean"
    ==> "BuildSolution"
    ==> "Success"

RunTargetOrDefault "Success"