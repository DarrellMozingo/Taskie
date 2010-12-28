$ErrorActionPreference = "Stop"

include ".\functions_fileIO.ps1"

properties { # General
	$version = if ($env:build_number -ne $NULL) { $env:build_number } else { "0.999" }
	$script:configuration = "Debug"
}

properties { # Directories
	$framework_dir = Get-FrameworkDirectory

	$executing_directory = new-object System.IO.DirectoryInfo $pwd
	$base_dir = $executing_directory.Parent.FullName

	$source_dir = "$base_dir\src"
	$sample_dir = "$source_dir\Samples"

	$build_dir = "$base_dir\build"
	$tools_dir = "$base_dir\tools"
	$build_tools_dir = "$build_dir\tools"

	$build_artifacts_dir = "$base_dir\artifacts"
	$build_output_dir = "$build_artifacts_dir\output"
	$build_output_build = "$build_artifacts_dir\output\build"
	$build_output_assemblies = "$build_output_dir\required_assemblies"
	$build_output_merged = "$build_output_dir\merged_assembly"
	$build_output_nuget = "$build_output_dir\NuGet"
	$build_reports_dir = "$build_artifacts_dir\reports"

	$transient_folders = @($build_artifacts_dir, 
						   $build_output_dir,
						   $build_output_build,
						   $build_output_assemblies,
						   $build_output_merged,
						   $build_output_nuget,
						   $build_reports_dir)
}

properties { # Executables
	$tools_nunit = "$tools_dir\NUnit\nunit-console-x86.exe"
	$tools_nuget = "$build_tools_dir\NuGet\NuGet.exe"
	$tools_ilmerge = "$build_tools_dir\ILMerge\ILMerge.exe"
}

properties { # Files
	$main_solution_file = "$source_dir\Taskie.sln"
	$sample_solution_file = "$sample_dir\StandardApp\src\StandardApp.sln"

	$output_unitTests_dll = "$build_output_build\Taskie.UnitTests.dll"
	$output_unitTests_xml = "$build_reports_dir\UnitTestResults.xml"

	$merge_internalize_exclusions = "$build_dir\ilmerge_internalize_exclusions.txt"
	$output_merged_assembly = "$build_output_merged\Taskie.dll"

	$nuspec_template = "$build_dir\nuspec_template.nuspec"
	$nuspec_file = "$build_output_nuget\taskie.nuspec"

	$required_assemblies = @("$build_output_build\Taskie.dll",
							 "$build_output_build\StructureMap.dll")
}

task set_to_release_mode {
	$script:configuration = "Release"
}

task default -depends unit_tests

task clean {
	$transient_folders | ForEach { delete_directory $_ }
	$transient_folders | ForEach { create_directory $_ }
}

task compile -depends clean {
	$config = $script:configuration

	echo "Compiling in $config mode."
	exec { msbuild $main_solution_file /p:Configuration=$config /p:OutDir=""$build_output_build\\"" /consoleloggerparameters:ErrorsOnly }
	exec { msbuild $sample_solution_file /p:Configuration=$config /consoleloggerparameters:ErrorsOnly }
}

task unit_tests -depends compile {
	exec { & $tools_nunit $output_unitTests_dll /nologo /nodots /xml=$output_unitTests_xml }
}

task merge {
	$required_assemblies | ForEach { copy_file $_ $build_output_assemblies }

	exec { & $tools_ilmerge /targetplatform:"v4,$framework_dir" /log /out:"$output_merged_assembly" /internalize:$merge_internalize_exclusions $required_assemblies }
}

task create_deployment -depends set_to_release_mode, unit_tests, merge {
	$nuspec = [xml](Get-Content $nuspec_template)
	$nuspec.package.metadata.version = $version
	$nuspec.Save($nuspec_file)

	exec { & $tools_nuget pack $nuspec_file -b $build_output_merged -o $build_output_nuget }
}

function Get-FrameworkDirectory {
    $([System.Runtime.InteropServices.RuntimeEnvironment]::GetRuntimeDirectory().Replace("v2.0.50727", "v4.0.30319"))
}