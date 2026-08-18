// Decompiled with JetBrains decompiler
// Type: Intermech.Compass3D.Integrator.K3DLibraryComponentConfigurationDetector
// Assembly: Intermech.Compass3D.Integrator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: E9700F29-129D-4EBE-8417-980BAD3DC32C
// Assembly location: D:\IPS\Client\Intermech.Compass3D.Integrator.dll

using Intermech.CADInterface.Proxies;
using Intermech.IO;
using System.IO;

#nullable disable
namespace Intermech.Compass3D.Integrator;

internal sealed class K3DLibraryComponentConfigurationDetector : InMemoryConfigurationFeatureDetector
{
  private ModelConfigurationProxy configuration;

  public K3DLibraryComponentConfigurationDetector(ModelConfigurationProxy configuration)
  {
    this.configuration = configuration;
  }

  protected override void DoDetect()
  {
    string rawFullPath = this.configuration.RawFullPath;
    if (string.IsNullOrEmpty(rawFullPath))
      return;
    if (rawFullPath.StartsWith(">>"))
    {
      this.SetDetectedData(true, Path.Combine("C:\\K3D\\LocalComponents", rawFullPath.Remove(0, 2).TrimStart()));
    }
    else
    {
      string withoutExtension = Path.GetFileNameWithoutExtension(rawFullPath);
      string firstPath = Path.GetExtension(rawFullPath);
      if (PathUtils.IsSamePath(firstPath, ".l3d"))
      {
        this.SetDetectedData(true, rawFullPath);
      }
      else
      {
        if (!PathUtils.IsSamePath(firstPath, CompassConsts.PartFileExtension) || withoutExtension.Length < 4 || !PathUtils.IsSamePath(withoutExtension.Substring(0, 4), "PLib") || !K3DInstallation.ContainsFile(rawFullPath))
          return;
        this.SetDetectedData(true, rawFullPath);
      }
    }
  }
}
