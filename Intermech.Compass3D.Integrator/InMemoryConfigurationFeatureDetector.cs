// Decompiled with JetBrains decompiler
// Type: Intermech.Compass3D.Integrator.InMemoryConfigurationFeatureDetector
// Assembly: Intermech.Compass3D.Integrator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: E9700F29-129D-4EBE-8417-980BAD3DC32C
// Assembly location: D:\IPS\Client\Intermech.Compass3D.Integrator.dll

using System;

#nullable disable
namespace Intermech.Compass3D.Integrator;

internal abstract class InMemoryConfigurationFeatureDetector : IInMemoryConfigurationFeatureDetector
{
  private bool isDetected;
  private bool isInMemory;
  private string documentVirtualPath;

  public bool IsInMemory
  {
    get
    {
      this.LazyDetect();
      return this.isInMemory;
    }
  }

  public string DocumentVirtualPath
  {
    get
    {
      this.LazyDetect();
      return this.documentVirtualPath;
    }
  }

  private void LazyDetect()
  {
    if (this.isDetected)
      return;
    this.isDetected = true;
    this.DoDetect();
  }

  protected void SetDetectedData(bool isInMemory, string documentVirtualPath)
  {
    if (documentVirtualPath == null)
      throw new ArgumentNullException(nameof (documentVirtualPath));
    this.isInMemory = isInMemory;
    this.documentVirtualPath = documentVirtualPath;
  }

  protected abstract void DoDetect();
}
