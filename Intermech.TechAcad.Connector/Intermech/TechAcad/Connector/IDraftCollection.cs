// Decompiled with JetBrains decompiler
// Type: Intermech.TechAcad.Connector.IDraftCollection
// Assembly: Intermech.TechAcad.Connector, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5A35A651-9A96-41F3-9839-2AAB5A952CB8
// Assembly location: D:\IPS\Client\Intermech.TechAcad.Connector.dll

using System.Runtime.InteropServices;

#nullable disable
namespace Intermech.TechAcad.Connector;

[ComVisible(true)]
[Guid("DC023347-94F9-4BB5-B25B-6ECE970695EE")]
public interface IDraftCollection
{
  [DispId(3)]
  int ReadOnly { get; }

  [DispId(2)]
  int ItemCount { get; }

  [DispId(1)]
  IDraftObject get_Item(int Index);

  [DispId(4)]
  IDraftObject Add();

  [DispId(5)]
  void Remove(int Index);

  [DispId(6)]
  IDraftObject get_ItemByID(long draftID);
}
