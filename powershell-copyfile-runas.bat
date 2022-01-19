Rem powershell -Command "Start-Process cmd -ArgumentList('/K', 'copy /Y \"$(TargetPath)\" \"C:\Program Files (x86)\EKStudio\NoteWidget\$(TargetFileName)\"')" -Verb RunAs
set source=%1
set target=%2

powershell -Command "Start-Process cmd -ArgumentList('/c', 'copy /Y \"%source%\" \"%target%\"')" -Verb RunAs
