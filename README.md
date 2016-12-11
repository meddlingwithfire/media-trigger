# Media Trigger
Plays a media asset when commands are typed on the keyboard.

## Requirements
1. Windows OS
1. .NET Framework 4.5 installed

## Setup
1. Copy the \bin\[Debug|Release]\ files to your local computer.
1. Create your records file that contains the mappings between identifiers and media assets. Must be a comma-delimited list. See the records.txt in the project for an example. Paths in the records.txt file can be absolute. If the paths are not absolute, then the `AssetFileRoot` .exe.config value is prepended to the path.
1. Edit the MediaTrigger.exe.config file. Update the `RecordFilePath` setting to point to where your records file is - must be an absolute path. Update the `AssetFileRoot` setting to point to the root directory where your media assets are stored.
1. Run the MediaTrigger.exe application. It runs silently - no window will appear. To stop the application, you will need to enter `quit` on the keyboard followed by the enter key.

## Notes
1. Media assets are run using the [Windows Media Player control](https://msdn.microsoft.com/en-us/library/windows/desktop/dd564582(v=vs.85).aspx). Anything that your computer can play in Windows Media Player should be able to run in this application.
1. The application generates a set of rolling log files. Look for a log.txt file next to the .exe after running the application. You can configure the logging settings by editing the MediaTrigger.exe.config file's `log4net` section. See the [documentation](http://logging.apache.org/log4net/release/manual/configuration.html).