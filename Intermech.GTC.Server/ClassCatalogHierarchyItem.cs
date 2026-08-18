// Decompiled with JetBrains decompiler
// Type: Intermech.GTC.Server.ClassCatalogHierarchyItem
// Assembly: Intermech.GTC.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 9C6A94ED-A48D-4719-B6F5-18FD5E10EDC9
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.GTC.Server.dll

using System.Collections.Generic;

#nullable disable
namespace Intermech.GTC.Server;

internal class ClassCatalogHierarchyItem
{
  public string Name { get; set; }

  public string BsuCode { get; set; }

  public string ParentBsuCode { get; set; }

  public List<ClassCatalogHierarchyItem> Children { get; internal set; }

  public ClassCatalogHierarchyItem(string aName, string aBsuCode, string aParentBsuCode)
  {
    this.Children = new List<ClassCatalogHierarchyItem>();
    this.Name = aName;
    this.BsuCode = aBsuCode;
    this.ParentBsuCode = aParentBsuCode;
  }
}
