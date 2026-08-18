// Decompiled with JetBrains decompiler
// Type: Intermech.Compass3D.Integrator.Drawing2DDocumentGroupsCheck
// Assembly: Intermech.Compass3D.Integrator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: E9700F29-129D-4EBE-8417-980BAD3DC32C
// Assembly location: D:\IPS\Client\Intermech.Compass3D.Integrator.dll

using Intermech.Tools.Integrators.CADInterface;
using Intermech.Tools.Settings;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Compass3D.Integrator;

internal sealed class Drawing2DDocumentGroupsCheck(IEnumerable<string> groupNames) : 
  DocumentGroupsCheck(groupNames)
{
  protected override string DoPerformCheck(CADSettings settings, SettingsValidatorContext context)
  {
    return ((K3DIntegratorSettings) settings).EnableDrawings2DSupport ? base.DoPerformCheck(settings, context) : (string) null;
  }
}
