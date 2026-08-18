// Decompiled with JetBrains decompiler
// Type: Intermech.Compass3D.Integrator.Drawing2DDocumentRootsCheck
// Assembly: Intermech.Compass3D.Integrator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: E9700F29-129D-4EBE-8417-980BAD3DC32C
// Assembly location: D:\IPS\Client\Intermech.Compass3D.Integrator.dll

using Intermech.Tools.Data;
using Intermech.Tools.Integrators.CADInterface;
using Intermech.Tools.Settings;

#nullable disable
namespace Intermech.Compass3D.Integrator;

internal sealed class Drawing2DDocumentRootsCheck : AbstractDocumentRootsCheck
{
  protected override string DoPerformCheck(CADSettings settings, SettingsValidatorContext context)
  {
    K3DIntegratorSettings dintegratorSettings = (K3DIntegratorSettings) settings;
    if (dintegratorSettings.EnableDrawings2DSupport)
    {
      string str1 = this.CheckDocumentGroupIsBasedOnType(dintegratorSettings.PartDrawings2D, IDCache.Default.MechanicalDocuments);
      if (!string.IsNullOrEmpty(str1))
        return str1;
      string str2 = this.CheckDocumentGroupIsBasedOnType(dintegratorSettings.AssemblyDrawings2D, IDCache.Default.MechanicalDocuments);
      if (!string.IsNullOrEmpty(str2))
        return str2;
    }
    return (string) null;
  }
}
