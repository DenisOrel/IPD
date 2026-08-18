// Decompiled with JetBrains decompiler
// Type: Intermech.TechAcad.Connector.IDraftObject
// Assembly: Intermech.TechAcad.Connector, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5A35A651-9A96-41F3-9839-2AAB5A952CB8
// Assembly location: D:\IPS\Client\Intermech.TechAcad.Connector.dll

using System.Runtime.InteropServices;

#nullable disable
namespace Intermech.TechAcad.Connector;

[ComVisible(true)]
[Guid("B3E009F3-DCCE-4997-86D1-05B461F4EE62")]
public interface IDraftObject
{
  [DispId(1)]
  long DraftID { get; }

  [DispId(2)]
  string Name { get; set; }

  [DispId(3)]
  ModifyMode ModifyMode { get; }

  [DispId(4)]
  ITPObjectCollection ObjectCollection { get; }

  [DispId(8)]
  ISketchCollection SketchCollection { get; }

  [DispId(5)]
  void Close(int NeedSave);

  [DispId(6)]
  string Extract(int CheckOutMode);

  [DispId(7)]
  void Save();

  [DispId(9)]
  void SaveStucture();

  [DispId(10)]
  DraftFileMode FileMode { get; }
}
