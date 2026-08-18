// Decompiled with JetBrains decompiler
// Type: Intermech.TechAcad.Connector.IApplication
// Assembly: Intermech.TechAcad.Connector, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5A35A651-9A96-41F3-9839-2AAB5A952CB8
// Assembly location: D:\IPS\Client\Intermech.TechAcad.Connector.dll

using System.Runtime.InteropServices;

#nullable disable
namespace Intermech.TechAcad.Connector;

[InterfaceType(ComInterfaceType.InterfaceIsDual)]
[ComVisible(true)]
[Guid("B421972C-42B0-4F34-B63A-E0E8A94CBF45")]
public interface IApplication
{
  [DispId(2)]
  int Loaded { get; }

  [DispId(5)]
  int Version { get; }

  [DispId(1)]
  ITPObjectCollection ObjCollection { get; }

  [DispId(6)]
  ITPObject ActiveTPObject { get; set; }

  [DispId(3)]
  string GetSettingParams { get; }

  [DispId(4)]
  void SetInterfaceObject(object IO);

  [DispId(7)]
  IDraftObject GetDraftByFileName(string fileName);

  [DispId(8)]
  string ApplicationName { get; }
}
