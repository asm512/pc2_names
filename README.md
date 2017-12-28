# PC2_Renamer
Get file name of .meb file format (Project Cars 2) from reading a hex offset 
![demo](https://github.com/peaches6/pc2_names/blob/master/media/demo.PNG?raw=true)

## Command line
A path to a folder will return all names from .meb files in that folder

A path to a singular file will return the name of said .meb

Prefixing your custom offset size with `rename`, for example `rename 15` will rename each file to it's true name

Any damaged meshes will not be output as they contain the same header result as the non-damaged meshes. If you want to process these, build it without checking for the name.

## Research (about names not decrypting model)
Opening any decrypted (from PC2's `beta` and `alpha` stage), or indeed encryped in a hex editor, will tell you that the object's true name is stored in hex offset `08` to `x` (not a set amount). As the name contains only letters and underscores, and characters after the name are typically `.` or other unknown characters, parsing the read bytes through a function which returns only letters and underscores will return the true name. The default hex offset is to read `25` bytes starting as position `8` as I have yet to come across any that exceed this length. However there is an option to read until a custom offset.

