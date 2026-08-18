// Decompiled with JetBrains decompiler
// Type: Intermech.Compass3D.Integrator.Drawing2DComponentConfigurationSlowDetector
// Assembly: Intermech.Compass3D.Integrator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: E9700F29-129D-4EBE-8417-980BAD3DC32C
// Assembly location: D:\IPS\Client\Intermech.Compass3D.Integrator.dll

using Intermech.CADInterface.Proxies;
using Intermech.Data;
using Intermech.Tools.Integrators.CADInterface;
using Interop.CADInterface;
using System.IO;

#nullable disable
namespace Intermech.Compass3D.Integrator;

internal sealed class Drawing2DComponentConfigurationSlowDetector : 
  InMemoryConfigurationFeatureDetector
{
  private ModelConfigurationProxy configuration;

  public Drawing2DComponentConfigurationSlowDetector(ModelConfigurationProxy configuration)
  {
    this.configuration = configuration;
  }

  protected override void DoDetect()
  {
    if (!this.IsVirtualConfiguration())
      return;
    this.SetDetectedData(true, Path.Combine(Path.GetTempPath(), "VirtualComponents.m3d"));
  }

  private bool IsVirtualConfiguration()
  {
    ValueRecord parameter = new ParametersContainerProxy((IParametersContainerProvider) new ExplicitParametersContainerProvider((IParametersContainer) this.configuration.RawObject)).TryGetParameter(CADVirtualAttributes.IsVirtualObject);
    return parameter != null && parameter.DataType == typeof (bool) && parameter.Read<bool>(false) && string.IsNullOrEmpty(this.configuration.RawFullPath);
  }
}
