using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

[assembly: AssemblyTitle("Galileo.Client.CLI")]
[assembly: AssemblyDescription("Galileo Console Client")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("dotBunny")]
[assembly: AssemblyProduct("Galileo")]
[assembly: AssemblyCopyright("2018 dotBunny Inc.")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]
[assembly: ComVisible(false)]

[assembly: AssemblyVersion("0.6.0.0")]

#if DEBUG
[assembly: InternalsVisibleTo("Galileo.Tests")]
#endif