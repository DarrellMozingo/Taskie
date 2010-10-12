function delete_directory($directory_name)
{
	rm -Force -Recurse $directory_name -ErrorAction SilentlyContinue
}

function create_directory($directory_name)
{
	ni $directory_name -ItemType Directory -ErrorAction SilentlyContinue | Out-Null
}