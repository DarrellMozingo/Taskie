$ErrorActionPreference = "Stop"

include ".\functions_fileIO.ps1"

properties { # General
	$solution_name = "Taskie"
	$configuration = "Debug"
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
	$build_reports_dir = "$build_artifacts_dir\reports"

	$transient_folders = @($build_artifacts_dir, 
						   $build_output_dir, 
						   $build_reports_dir)
}

properties { # Executables
	$tools_nunit = "$tools_dir\nunit\nunit-console-x86.exe"
}

properties { # Files
	$main_solution_file = "$source_dir\$solution_name.sln"
	$sample_solution_file = "$sample_dir\StandardApp\src\StandardApp.sln"

	$output_unitTests_dll = "$build_output_dir\$solution_name.UnitTests.dll"
	$output_unitTests_xml = "$build_reports_dir\UnitTestResults.xml"
}

task set_to_release_mode {
	$configuration = "Release"
}

task default -depends unit_tests

task clean {
	$transient_folders | ForEach { delete_directory $_ }
	$transient_folders | ForEach { create_directory $_ }
}

task compile -depends clean {
	echo "Building in $configuration mode."
	exec { msbuild $main_solution_file /p:Configuration=$configuration /p:OutDir=""$build_output_dir\\"" /consoleloggerparameters:ErrorsOnly }
	exec { msbuild $sample_solution_file /p:Configuration=$configuration /consoleloggerparameters:ErrorsOnly }
}

task unit_tests -depends compile {
	exec { & $tools_nunit $output_unitTests_dll /nologo /xml=$output_unitTests_xml }
}