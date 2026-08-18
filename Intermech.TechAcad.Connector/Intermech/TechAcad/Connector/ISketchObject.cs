// Decompiled with JetBrains decompiler
// Type: Intermech.TechAcad.Connector.ISketchObject
// Assembly: Intermech.TechAcad.Connector, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5A35A651-9A96-41F3-9839-2AAB5A952CB8
// Assembly location: D:\IPS\Client\Intermech.TechAcad.Connector.dll

using System.Runtime.InteropServices;

#nullable disable
namespace Intermech.TechAcad.Connector;

[ComVisible(true)]
[Guid("441746B1-4244-4434-998E-83827D89C998")]
public interface ISketchObject
{
  [DispId(1)]
  string SketchID { get; }

  [DispId(2)]
  string Name { get; }

  [DispId(4)]
  long OrderID { get; set; }

  [DispId(3)]
  int ReadOnly { get; }

  [DispId(5)]
  IDraftObject DraftObject { get; }
}
