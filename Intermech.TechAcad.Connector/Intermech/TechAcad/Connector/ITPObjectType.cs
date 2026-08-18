// Decompiled with JetBrains decompiler
// Type: Intermech.TechAcad.Connector.ITPObjectType
// Assembly: Intermech.TechAcad.Connector, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5A35A651-9A96-41F3-9839-2AAB5A952CB8
// Assembly location: D:\IPS\Client\Intermech.TechAcad.Connector.dll

using System.Runtime.InteropServices;

#nullable disable
namespace Intermech.TechAcad.Connector;

[ComVisible(true)]
[Guid("57ED348D-C24C-4BF7-9590-CDAE4E856599")]
public interface ITPObjectType
{
  [DispId(1)]
  int ObjTypeID { get; }

  [DispId(2)]
  string Name { get; }

  [DispId(3)]
  string ObjTypeGuid { get; }
}
