// Decompiled with JetBrains decompiler
// Type: Intermech.AltiumDesigner.Integrator.FileTypeService
// Assembly: Intermech.AltiumDesigner.Integrator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 4CE9F573-7E4B-4FE9-9600-ADBDE2EC9D6B
// Assembly location: D:\IPS\Client\Intermech.AltiumDesigner.Integrator.dll

using Intermech.IO;
using Intermech.Tools.Integrators;

#nullable disable
namespace Intermech.AltiumDesigner.Integrator;

internal sealed class FileTypeService(IIntegrator owner) : NameBasedFileTypesService(owner)
{
  protected override PathCollection GetFileExtensions()
  {
    PathCollection fileExtensions = base.GetFileExtensions();
    fileExtensions.Add(".PrjPcb");
    fileExtensions.Add(".SchDoc");
    fileExtensions.Add(".PcbDoc");
    return fileExtensions;
  }
}
