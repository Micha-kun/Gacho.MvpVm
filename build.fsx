// Include FAKE lib
#r "packages/FAKE/tools/FakeLib.dll"
open Fake
open Fake.AssemblyInfoFile

let buildDir = "./build/"
let testDir = "./test/"

let versionNumber =
    match buildServer with
    | TeamCity -> buildVersion
    | _ -> "0.1.0.0"

let baseAttributes = [
    Attribute.Product "Gacho.MvpVm"
    Attribute.Company "Michael-Jorge Gómez Campos"
    Attribute.Copyright "Copyright © 2015 Michael-Jorge Gómez"
    Attribute.Version versionNumber
    Attribute.FileVersion versionNumber
]

Target "Clean" (fun _ ->
    CleanDirs [buildDir; testDir]
    RestorePackages()
)

Target "BuildLib" (fun _ ->
    CreateCSharpAssemblyInfo "src\Gacho.MvpVm.Core.Net40\Properties\AssemblyInfo.cs"
        [
            yield Attribute.Title "Gacho.MvpVm"
            yield Attribute.Description "An MvpVm library in C# for ASP.Net WebForms, WinForms or Console applications."
            yield Attribute.Guid "0F3256A8-05C5-46DA-A2AA-A5CF4E8E6C7A"
            yield Attribute.InternalsVisibleTo "Gacho.MvpVm.Core.Tests"
            yield! baseAttributes
        ]

    !! "src/**/*.csproj"
        |> MSBuildRelease buildDir "Build"
        |> Log "LibBuild-Output: "
)

Target "BuildTest" (fun _ ->
    !! "tests/**/*.csproj"
        |> MSBuildDebug testDir "Build"
        |> Log "TestBuild-Output: "
)

Target "Test" (fun _ ->
    !! (testDir + "/Gacho.MvpVm.Core.Tests.dll")
      |> NUnit (fun p ->
          {p with
             ToolPath = "./packages/NUnit.Runners.2.6.3/tools"
             DisableShadowCopy = true;
             OutputFile = testDir + "TestResults.xml" })
)

Target "NuGet" (fun _ ->
    NuGet (fun p -> 
        {p with
            Authors = ["Michael-Jorge Gómez Campos"]
            Project = "Gacho.MvpVm"
            Summary = ""
            Description = "An MvpVm library in C# for ASP.Net WebForms, WinForms or Console applications."
            Version = "0.1.0.0"
            ReleaseNotes = ""
            Tags = "C#"
            OutputPath = "nuget"
            AccessKey = getBuildParamOrDefault "nugetkey" ""
            Publish = hasBuildParam "nugetkey"
            Files = [
                        ("""..\build\*.dll""", Some "lib", None)
                        ("""..\build\*.xml""", Some "lib", None)
                    ]
        }) "nuget/Gacho.MvpVm.nuspec"
)

Target "Default" DoNothing

"Clean"
    ==> "BuildLib"
    ==> "BuildTest"
    ==> "Test"
    ==> "NuGet"
    ==> "Default"

RunTargetOrDefault "Default"
