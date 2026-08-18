// Decompiled with JetBrains decompiler
// Type: Intermech.TechAcad.Connector.ITPObjectCollection
// Assembly: Intermech.TechAcad.Connector, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5A35A651-9A96-41F3-9839-2AAB5A952CB8
// Assembly location: D:\IPS\Client\Intermech.TechAcad.Connector.dll

using System.Runtime.InteropServices;

#nullable disable
namespace Intermech.TechAcad.Connector;

[ComVisible(true)]
[Guid("AD23F909-5A14-4404-92AD-01E54B029F07")]
public interface ITPObjectCollection
{
  [DispId(3)]
  int ItemCount { get; }

  [DispId(2)]
  ITPObject get_Item(int Index);
}
