	Dim WshShell

	Set objShell = CreateObject("WScript.Shell") 

	Dim objMyShortcut
	Dim iconFolderName
	Dim iconFullPath
	Dim SelectedLanguage
	Dim strTargetPath
	
	Set objMyShortcut = objShell.CreateShortcut(objShell.SpecialFolders("Desktop") + "\RIA - FxOnline.lnk")
	'get .ico path name
	iconFolderName = ".\."
	objMyShortcut.IconLocation = "C:\Users\Theos\Documents\GitHub\Spartan2016.1\FxonlineInstaller\Ria.FxonlineInstaller\bin\Release\ria.ico"

	'Get IE 32-bit path	
	If OSArc = 64 Then
		strTargetPath = "%ProgramFiles(x86)%\Internet Explorer\iexplore.exe"
	Else 
		strTargetPath = "%ProgramFiles%\Internet Explorer\iexplore.exe"
	End If
	
	objMyShortcut.TargetPath = strTargetPath
	objMyShortcut.Arguments = "https://fxonline.riaenvia.net"
	objMyShortcut.Save
	
	installjava = Null
	puticon = true


	Set objMyShortcut = objShell.CreateShortcut(objShell.SpecialFolders("Desktop") + "\Live Help RIA.lnk")
	'get .ico path name
	iconFolderName = ".\."
	objMyShortcut.IconLocation = "C:\Users\Theos\Documents\GitHub\Spartan2016.1\FxonlineInstaller\Ria.FxonlineInstaller\bin\Release\logmein.ico"
	strPath = "http://www.logmein123.com/"
	objMyShortcut.TargetPath = strPath
	
	objMyShortcut.Save