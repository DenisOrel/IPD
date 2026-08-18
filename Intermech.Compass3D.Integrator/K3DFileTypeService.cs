// Decompiled with JetBrains decompiler
// Type: Intermech.Compass3D.Integrator.K3DFileTypeService
// Assembly: Intermech.Compass3D.Integrator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: E9700F29-129D-4EBE-8417-980BAD3DC32C
// Assembly location: D:\IPS\Client\Intermech.Compass3D.Integrator.dll

using Intermech.IO;
using Intermech.Tools.Integrators;

#nullable disable
namespace Intermech.Compass3D.Integrator;

internal sealed class K3DFileTypeService(IIntegrator owner) : NameBasedFileTypesService(owner)
{
  protected override PathCollection GetFileExtensions()
  {
    PathCollection fileExtensions = base.GetFileExtensions();
    fileExtensions.Add(CompassConsts.AssemblyFileExtension);
    fileExtensions.Add(CompassConsts.PartFileExtension);
    fileExtensions.Add(CompassConsts.DrawingFileExtension);
    return fileExtensions;
  }
}
