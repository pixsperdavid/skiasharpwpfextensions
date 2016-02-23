using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows;

[assembly: AssemblyTitle("Imp.SkiaSharpWpfExtensions.Example")]
[assembly: AssemblyDescription("Example application to demonstrate Imp.SkiaSharpWpfExtensions")]

#if DEBUG
#if WIN32
[assembly: AssemblyConfiguration("Debug|x86")]
#else
[assembly: AssemblyConfiguration("Debug|x64")]
#endif
#else
#if WIN32
[assembly: AssemblyConfiguration("Release|x86")]
#else
[assembly: AssemblyConfiguration("Release|x64")]
#endif
#endif

[assembly: AssemblyCompany("The Impersonal Stereo")]
[assembly: AssemblyProduct("Imp.SkiaSharpWpfExtensions.Example")]
[assembly: AssemblyCopyright("Copyright © David Butler / The Impersonal Stereo 2016")]

[assembly: ComVisible(false)]

[assembly: ThemeInfo(
	ResourceDictionaryLocation.None, //where theme specific resource dictionaries are located
									 //(used if a resource is not found in the page, 
									 // or application resource dictionaries)
	ResourceDictionaryLocation.SourceAssembly //where the generic resource dictionary is located
											  //(used if a resource is not found in the page, 
											  // app, or any theme specific resource dictionaries)
)]