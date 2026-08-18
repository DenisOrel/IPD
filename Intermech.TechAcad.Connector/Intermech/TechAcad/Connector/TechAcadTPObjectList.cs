// Decompiled with JetBrains decompiler
// Type: Intermech.TechAcad.Connector.TechAcadTPObjectList
// Assembly: Intermech.TechAcad.Connector, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5A35A651-9A96-41F3-9839-2AAB5A952CB8
// Assembly location: D:\IPS\Client\Intermech.TechAcad.Connector.dll

using Intermech.Runtime.ComInterop.LocalServer;
using System.Collections.Generic;

#nullable disable
namespace Intermech.TechAcad.Connector;

internal class TechAcadTPObjectList : SingleThreadedObject, ITPObjectCollection
{
  private readonly List<TechAcadTPObject> _items = new List<TechAcadTPObject>();

  internal List<TechAcadTPObject> Items => this._items;

  public int ItemCount => this._items.Count;

  public ITPObject get_Item(int index) => (ITPObject) this._items[index];
}
