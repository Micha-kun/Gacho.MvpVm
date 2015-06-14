// Include FAKE lib
#r "packages/FAKE/tools/FakeLib.dll"
open Fake
open Fake.AssemblyInfoFile

Target "Default" DoNothing

RunTargetOrDefault "Default"