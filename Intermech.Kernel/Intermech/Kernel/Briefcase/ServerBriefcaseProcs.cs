// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Briefcase.ServerBriefcaseProcs
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using System.IO;


namespace Intermech.Kernel.Briefcase;

internal class ServerBriefcaseProcs
{
  public static string VerifyBriefcaseFolderSyntax(string folder)
  {
    if (folder.Length > 0 && (int) folder[folder.Length - 1] != (int) Path.DirectorySeparatorChar)
      folder += Path.DirectorySeparatorChar.ToString();
    return folder;
  }
}
