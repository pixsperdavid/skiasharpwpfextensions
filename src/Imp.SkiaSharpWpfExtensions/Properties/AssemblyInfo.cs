using System.Reflection;
using System.Runtime.InteropServices;

[assembly: AssemblyTitle("Imp.SkiaSharpWpfExtensions")]
[assembly: AssemblyDescription("Useful classes for implementing SkiaSharp in WPF")]

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
[assembly: AssemblyProduct("Imp.SkiaSharpWpfExtensions")]
[assembly: AssemblyCopyright("Copyright © David Butler / The Impersonal Stereo 2016")]

[assembly: ComVisible(false)]
[assembly: Guid("28f307b6-84ac-4984-b0af-3b49de29a39a")]