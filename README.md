![screenshot](screenshot.png?raw=true "Title")
# dir, a silly text adventure

There are scammers out there who scares people with fake virus messages, and then execute a "dir /s" command, saying now they are scanning the client's computer, then trying to sell them useless and expensive support plans. Scambaiters call these people and have some fun with them.

This is a small program, intended to replace the "dir" command, which then shows a very simple text adventure. This can be used to replace the real dir command, for example in a VM for scambaiting. For testing it from a command prompt, you have to execute it like ".\dir", to avoid calling the built-in "dir" command.

# installation process

You can compile the program with Visual Studio 2015, or just copy the pre-compiled dir.exe to c:\windows\system32. In cmd.exe, there is a built-in "dir" command, so you have to take ownership of c:\windows\system32\cmd.exe in order to modify it, then you can find the unicode string "DIR" at position 0x28428 (in the German Windows 10 version): the bytes 0x44, 0x00, 0x49, 0x00, 0x52, 0x00. You can change e.g. the 0x52 to 0x53 (e.g. with Ultraedit or any other hex editor, like 010editor) to make it "DIS". Now when someone calls "dir", the new dir.exe program from system32 is executed. On first start of dir.exe, Windows automatically downloaded .NET 3.5.
