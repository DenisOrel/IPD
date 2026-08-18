// Decompiled with JetBrains decompiler
// Type: Intermech.TwainScanner.Program
// Assembly: Intermech.TwainScanner, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0CEE3C76-D3AF-4F98-AB07-F18794839283
// Assembly location: D:\IPS\Client\Intermech.TwainScanner.exe
// XML documentation location: D:\IPS\Client\Intermech.TwainScanner.xml

using Intermech.TwainScanner.VintaSoftScanner;
using System;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

#nullable disable
namespace Intermech.TwainScanner;

internal static class Program
{
  /// <summary>The main entry point for the application.</summary>
  [STAThread]
  private static void Main(string[] args)
  {
    Application.EnableVisualStyles();
    Application.SetCompatibleTextRenderingDefault(false);
    AppDomain.CurrentDomain.AssemblyResolve += new ResolveEventHandler(Program.CurrentDomain_AssemblyResolve);
    if (args != null && args.Length != 0 && args[0] == "r")
      NamedPipesServer.Instance.Init();
    else
      Application.Run((Form) new MainForm());
  }

  private static Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
  {
    if (!new AssemblyName(args.Name).Name.Contains("Vintasoft.Twain"))
      return (Assembly) null;
    Stream manifestResourceStream = typeof (Program).Assembly.GetManifestResourceStream("Intermech.TwainScanner.VintaSoftScanner.Vintasoft.Twain.dll");
    using (manifestResourceStream)
    {
      byte[] numArray = new byte[manifestResourceStream.Length];
      manifestResourceStream.Read(numArray, 0, numArray.Length);
      return Assembly.Load(numArray);
    }
  }
}
