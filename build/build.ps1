$ErrorActionPreference = "Stop"

include ".\functions_fileIO.ps1"

properties { # General
	$solution_name = "Taskie"
	$script:configuration = "Debug"
}

properties { # Directories
	$executing_directory = new-object System.IO.DirectoryInfo $pwd
	$base_dir = $executing_directory.Parent.FullName

	$source_dir = "$base_dir\src"
	$sample_dir = "$source_dir\Samples"

	$build_dir = "$base_dir\build"
	$tools_dir = "$base_dir\tools"
	$build_tools_dir = "$build_dir\tools"

	$build_artifacts_dir = "$base_dir\artifacts"
	$build_output_dir = "$build_artifacts_dir\output"
	$build_output_assemblies = "$build_output_dir\assemblies"
	$build_output_nuget = "$build_output_dir\NuGet"
	$build_reports_dir = "$build_artifacts_dir\reports"

	$transient_folders = @($build_artifacts_dir, 
						   $build_output_dir, 
						   $build_output_assemblies,
						   $build_output_nuget,
						   $build_reports_dir)
}

properties { # Executables
	$tools_nunit = "$tools_dir\NUnit\nunit-console-x86.exe"
	$tools_nuget = "$build_tools_dir\NuGet\NuGet.exe"
}

properties { # Files
	$main_solution_file = "$source_dir\$solution_name.sln"
	$sample_solution_file = "$sample_dir\StandardApp\src\StandardApp.sln"

	$output_unitTests_dll = "$build_output_dir\$solution_name.UnitTests.dll"
	$output_unitTests_xml = "$build_reports_dir\UnitTestResults.xml"

	$required_assemblies = @("$build_output_dir\$solution_name.dll",
							 "$build_output_dir\StructureMap.dll")
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
	exec { msbuild $main_solution_file /p:Configuration=$config /p:OutDir=""$build_output_dir\\"" /consoleloggerparameters:ErrorsOnly }
	exec { msbuild $sample_solution_file /p:Configuration=$config /consoleloggerparameters:ErrorsOnly }
}

task unit_tests -depends compile {
	exec { & $tools_nunit $output_unitTests_dll /nologo /xml=$output_unitTests_xml }
}

task create_deployment -depends set_to_release_mode, unit_tests {
	$required_assemblies | ForEach { copy_file $_ $build_output_assemblies }
}

#<?xml version="1.0" encoding="utf-8"?>
#<package>
#  <metadata>
#    <id>taskie</id>
#    <version>$version</version>
#    <authors>Darrell Mozingo</authors>
#    <description>Taskie provides an easy way to create and manage scheduled tasks.</description>
#    <language>en-US</language>
#  </metadata> 
#</package>