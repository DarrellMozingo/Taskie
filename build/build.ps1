$ErrorActionPreference = "Stop"
$framework = "4.0"

include ".\functions_fileIO.ps1"

properties {
	$version = "0.120"
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

	echo "Building Taskie solution."
	exec { msbuild $main_solution_file /p:Configuration=$config /p:OutDir=""$build_output_build\\"" /consoleloggerparameters:ErrorsOnly }

	echo "Building Sample solution."
	exec { msbuild $sample_solution_file /p:Configuration=$config /consoleloggerparameters:ErrorsOnly }
}

task unit_tests -depends compile {
	exec { & $tools_nunit $output_unitTests_dll /nologo /nodots /xml=$output_unitTests_xml }
}

task merge -depends set_to_release_mode, unit_tests {
	$required_assemblies | ForEach { copy_file $_ $build_output_assemblies }

	exec { & $tools_ilmerge /targetplatform:"v4,$framework_dir" /log /out:"$output_merged_assembly" /internalize:$merge_internalize_exclusions $required_assemblies }
}

task create_nuget_package -depends merge {
	$nuspec = [xml](Get-Content $nuspec_template)
	$nuspec.package.metadata.version = $version
	$nuspec.Save($nuspec_file)

	exec { & $tools_nuget pack $nuspec_file -b $build_output_merged -o $build_output_nuget }
}

task publish_nuget_package -depends create_nuget_package {
	$key_file = "$env:USERPROFILE\Documents\My Dropbox\Programming\NuGet_key.txt"

	if (file_does_not_exist $key_file) {
		throw "Couldn't find the NuGet key at $keyfile. Are you supposed to be running this?"
	}

	$key = Get-Content $key_file

	$packages = dir "$build_output_nuget\*.nupkg"
	echo "Packages to upload:"
	$packages | ForEach { echo "$_" }

	$yes = New-Object System.Management.Automation.Host.ChoiceDescription "&Yes", "Upload the package."
	$no = New-Object System.Management.Automation.Host.ChoiceDescription "&No", "Don't upload the package."
	$options = [System.Management.Automation.Host.ChoiceDescription[]]($no, $yes)

	$result = $host.ui.PromptForChoice("Upload packages", "Do you want to upload these packages to the live server?", $options, 0) 

	if ($result -eq 0) {
		"Upload aborted."
	}
	elseif ($result -eq 1) {
		$packages | ForEach { 
		    $package = "$_"
		    echo "Uploading $package..."
		    exec { & $tools_nuget push -source "http://packages.nuget.org/v1/" $package $key }
		}
	}
}

function Get-FrameworkDirectory {
    $([System.Runtime.InteropServices.RuntimeEnvironment]::GetRuntimeDirectory().Replace("v2.0.50727", "v4.0.30319"))
}