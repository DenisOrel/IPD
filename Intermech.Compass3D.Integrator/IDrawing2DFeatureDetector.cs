// Decompiled with JetBrains decompiler
// Type: Intermech.Compass3D.Integrator.IDrawing2DFeatureDetector
// Assembly: Intermech.Compass3D.Integrator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: E9700F29-129D-4EBE-8417-980BAD3DC32C
// Assembly location: D:\IPS\Client\Intermech.Compass3D.Integrator.dll

using Intermech.CADInterface.Proxies;

#nullable disable
namespace Intermech.Compass3D.Integrator;

internal interface IDrawing2DFeatureDetector
{
  bool IsDrawing2D(CADDocumentProxy document);
}
