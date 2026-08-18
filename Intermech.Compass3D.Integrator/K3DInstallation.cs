// Decompiled with JetBrains decompiler
// Type: Intermech.Compass3D.Integrator.K3DInstallation
// Assembly: Intermech.Compass3D.Integrator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: E9700F29-129D-4EBE-8417-980BAD3DC32C
// Assembly location: D:\IPS\Client\Intermech.Compass3D.Integrator.dll

using System;
using System.IO;

#nullable disable
namespace Intermech.Compass3D.Integrator;

internal static class K3DInstallation
{
  public static bool ContainsFile(string path)
  {
    if (path == null)
      throw new ArgumentNullException(nameof (path));
    return Path.IsPathRooted(path) && path.IndexOf("ASCON", StringComparison.CurrentCultureIgnoreCase) >= 0 && path.IndexOf("Kompas-3D", StringComparison.CurrentCultureIgnoreCase) >= 0;
  }
}
