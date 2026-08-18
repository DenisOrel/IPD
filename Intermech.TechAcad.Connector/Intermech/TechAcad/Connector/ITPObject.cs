// Decompiled with JetBrains decompiler
// Type: Intermech.TechAcad.Connector.ITPObject
// Assembly: Intermech.TechAcad.Connector, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5A35A651-9A96-41F3-9839-2AAB5A952CB8
// Assembly location: D:\IPS\Client\Intermech.TechAcad.Connector.dll

using System.Runtime.InteropServices;

#nullable disable
namespace Intermech.TechAcad.Connector;

[ComVisible(true)]
[Guid("E2A27EE5-7FFD-4FC9-BBAA-B64D21F6CBD3")]
public interface ITPObject
{
  [DispId(1)]
  long ObjID { get; }

  [DispId(2)]
  string Name { get; }

  [DispId(3)]
  string Designation { get; }

  [DispId(4)]
  int Active { get; set; }

  [DispId(6)]
  string Comment { get; set; }

  [DispId(8)]
  ITPObjectType TPObjectType { get; }

  [DispId(9)]
  ITPObjectCollection ObjCollection { get; }

  [DispId(10)]
  IDraftCollection DraftCollection { get; }

  [DispId(5)]
  ISketchCollection SketchCollection { get; }

  [DispId(11)]
  IDraftCollection ArticleDraftCollection { get; }

  [DispId(12)]
  ITPObject ParentObject { get; }
}
