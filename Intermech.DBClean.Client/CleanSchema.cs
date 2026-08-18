// Decompiled with JetBrains decompiler
// Type: Intermech.DBClean.Client.CleanSchema
// Assembly: Intermech.DBClean.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 973F13FD-72F3-4555-9BF9-74AC5C606885
// Assembly location: D:\IPS\Client\Intermech.DBClean.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.DBClean.Client.xml

using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.DBClean.Client;

[Serializable]
public class CleanSchema
{
  private List<int> objectTypes;
  private List<int> attributes;
  private List<ImBaseCatalog> catalogs = new List<ImBaseCatalog>();

  public List<int> ObjectTypes
  {
    get => this.objectTypes;
    set => this.objectTypes = value;
  }

  public List<int> Attributes
  {
    get => this.attributes;
    set => this.attributes = value;
  }

  public List<ImBaseCatalog> Catalogs
  {
    get => this.catalogs;
    set => this.catalogs = value;
  }
}
