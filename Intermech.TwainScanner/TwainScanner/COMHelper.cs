// Decompiled with JetBrains decompiler
// Type: Intermech.TwainScanner.COMHelper
// Assembly: Intermech.TwainScanner, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0CEE3C76-D3AF-4F98-AB07-F18794839283
// Assembly location: D:\IPS\Client\Intermech.TwainScanner.exe
// XML documentation location: D:\IPS\Client\Intermech.TwainScanner.xml

using Microsoft.Win32;
using System;
using System.Reflection;

#nullable disable
namespace Intermech.TwainScanner;

internal class COMHelper
{
  /// <summary>Register the component as a local server.</summary>
  /// <param name="t"></param>
  public static void RegasmRegisterLocalServer(Type t)
  {
    COMHelper.GuardNullType(t, nameof (t));
    using (RegistryKey registryKey = Registry.ClassesRoot.OpenSubKey("CLSID\\" + t.GUID.ToString("B"), true))
    {
      registryKey.DeleteSubKeyTree("InprocServer32");
      using (RegistryKey subKey = registryKey.CreateSubKey("LocalServer32"))
        subKey.SetValue("", (object) Assembly.GetExecutingAssembly().Location, RegistryValueKind.String);
    }
  }

  /// <summary>Unregister the component.</summary>
  /// <param name="t"></param>
  public static void RegasmUnregisterLocalServer(Type t)
  {
    COMHelper.GuardNullType(t, nameof (t));
    Registry.ClassesRoot.DeleteSubKeyTree("CLSID\\" + t.GUID.ToString("B"));
  }

  private static void GuardNullType(Type t, string param)
  {
    if (t == (Type) null)
      throw new ArgumentException("The CLR type must be specified.", param);
  }
}
