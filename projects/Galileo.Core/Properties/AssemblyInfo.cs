using System.Reflection;
using System.Runtime.CompilerServices;

[assembly: AssemblyTitle("Galileo.Core")]
[assembly: AssemblyDescription("Galileo Core Library")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("dotBunny")]
[assembly: AssemblyProduct("Galileo: It's My First Time.")]
[assembly: AssemblyCopyright("2018 dotBunny Inc.")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]

[assembly: AssemblyVersion("0.9.3.0")]

[assembly: InternalsVisibleTo("Galileo.Client")]
#if DEBUG
[assembly: InternalsVisibleTo("Galileo.Tests")]
#endif