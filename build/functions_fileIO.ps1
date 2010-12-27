function delete_directory($directory_name)
{
	rm -Force -Recurse $directory_name -ErrorAction SilentlyContinue
}

function create_directory($directory_name)
{
	ni $directory_name -ItemType Directory -ErrorAction SilentlyContinue | Out-Null
}

function copy_file($source_file, $destination_directory)
{
	cp $source_file -Destination $destination_directory
}