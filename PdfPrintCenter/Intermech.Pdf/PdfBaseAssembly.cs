// Decompiled with JetBrains decompiler
// Type: Syncfusion.PdfBaseAssembly
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using System;
using System.Reflection;

#nullable disable
namespace Syncfusion;

public class PdfBaseAssembly
{
  public static readonly Assembly Assembly = typeof (PdfBaseAssembly).Assembly;
  public static readonly string Name;
  public const string RootNamespace = "Syncfusion.Pdf";

  static PdfBaseAssembly()
  {
    string fullName = PdfBaseAssembly.Assembly.FullName;
    PdfBaseAssembly.Name = fullName.Substring(0, fullName.IndexOf(","));
  }

  public static Assembly AssemblyResolver(object sender, ResolveEventArgs e)
  {
    Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
    for (int index = 0; index < assemblies.Length; ++index)
    {
      if (assemblies[index].GetName().Name == e.Name)
        return assemblies[index];
    }
    return (Assembly) null;
  }
}
