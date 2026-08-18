// Decompiled with JetBrains decompiler
// Type: Intermech.TechAcad.Connector.ISketchCollection
// Assembly: Intermech.TechAcad.Connector, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5A35A651-9A96-41F3-9839-2AAB5A952CB8
// Assembly location: D:\IPS\Client\Intermech.TechAcad.Connector.dll

using System.Runtime.InteropServices;

#nullable disable
namespace Intermech.TechAcad.Connector;

[ComVisible(true)]
[Guid("49AF3125-FEC4-416E-AB06-E7C00FCDB223")]
public interface ISketchCollection
{
  [DispId(5)]
  int ReadOnly { get; }

  [DispId(1)]
  int Count { get; }

  [DispId(2)]
  ISketchObject get_Item(int Index);

  [DispId(3)]
  ISketchObject Add(string Name, IDraftObject Draft, ITPObject TPObject);

  [DispId(4)]
  void Remove(int Index);

  [DispId(6)]
  void Link(ITPObject TPObject, ISketchObject Sketch);

  [DispId(7)]
  int GetIndexByID(string sketchID);
}
