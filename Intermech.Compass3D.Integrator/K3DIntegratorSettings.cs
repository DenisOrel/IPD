// Decompiled with JetBrains decompiler
// Type: Intermech.Compass3D.Integrator.K3DIntegratorSettings
// Assembly: Intermech.Compass3D.Integrator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: E9700F29-129D-4EBE-8417-980BAD3DC32C
// Assembly location: D:\IPS\Client\Intermech.Compass3D.Integrator.dll

using Intermech.Tools.Integrators.CADInterface;
using System.Diagnostics;

#nullable disable
namespace Intermech.Compass3D.Integrator;

internal sealed class K3DIntegratorSettings : CADSettings
{
  private bool enableDrawings2DSupport;
  private DocumentGroup partDrawings2D;
  private DocumentGroup assemblyDrawings2D;

  public K3DIntegratorSettings()
  {
    this.enableDrawings2DSupport = false;
    this.partDrawings2D = new DocumentGroup("PartDrawing2D", "Чертежи деталей 2D", new string[0]);
    this.FileDocumentGroups.Add(this.partDrawings2D);
    this.assemblyDrawings2D = new DocumentGroup("AssemblyDrawing2D", "Сборочные чертежи 2D", new string[0]);
    this.FileDocumentGroups.Add(this.assemblyDrawings2D);
  }

  public bool EnableDrawings2DSupport
  {
    [DebuggerStepThrough] get => this.enableDrawings2DSupport;
    [DebuggerStepThrough] set => this.enableDrawings2DSupport = value;
  }

  public DocumentGroup PartDrawings2D
  {
    [DebuggerStepThrough] get => this.partDrawings2D;
  }

  public DocumentGroup AssemblyDrawings2D
  {
    [DebuggerStepThrough] get => this.assemblyDrawings2D;
  }
}
