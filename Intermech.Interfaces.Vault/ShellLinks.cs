// Decompiled with JetBrains decompiler
// Type: Intermech.Vault.Interfaces.ShellLinks
// Assembly: Intermech.Interfaces.Vault, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 00798F5C-F1D9-4688-8BA7-75723F33BDBF
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Vault.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Vault.xml

using System;
using System.Runtime.InteropServices.ComTypes;

#nullable disable
namespace Intermech.Vault.Interfaces;

public class ShellLinks
{
  public static readonly Guid CLSID_ShellLink = new Guid("{00021401-0000-0000-C000-000000000046}");

  public static bool CreateShortcut(string shShortcut, string targetFilename)
  {
    return ShellLinks.CreateShortcut(shShortcut, targetFilename, string.Empty);
  }

  public static bool CreateShortcut(string shShortcut, string targetFilename, string workingFolder)
  {
    object instance = Activator.CreateInstance(Type.GetTypeFromCLSID(ShellLinks.CLSID_ShellLink));
    IShellLinkW shellLinkW = instance as IShellLinkW;
    shellLinkW.SetPath(targetFilename);
    shellLinkW.SetWorkingDirectory(workingFolder);
    (instance as IPersistFile).Save(shShortcut, true);
    return true;
  }
}
