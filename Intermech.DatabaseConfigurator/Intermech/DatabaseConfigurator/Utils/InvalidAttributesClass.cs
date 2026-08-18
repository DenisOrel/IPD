// Decompiled with JetBrains decompiler
// Type: Intermech.DatabaseConfigurator.Utils.InvalidAttributesClass
// Assembly: Intermech.DatabaseConfigurator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: EA0AF6EA-EE29-4FDF-AAED-DB84FDED5E5C
// Assembly location: D:\IPS\Client\Intermech.DatabaseConfigurator.dll

using System.Data;

#nullable disable
namespace Intermech.DatabaseConfigurator.Utils;

public class InvalidAttributesClass
{
  private int objectType;
  private int allObjectsCount;
  private DataTable tableOfAttributes;

  public DataTable TableOfAttributes => this.tableOfAttributes;

  public int AllObjectsCount => this.allObjectsCount;

  public int ObjectType => this.objectType;

  public InvalidAttributesClass(int _objectType, int _objectsCount, DataTable _tableOfAttributes)
  {
    this.objectType = _objectType;
    this.allObjectsCount = _objectsCount;
    this.tableOfAttributes = _tableOfAttributes;
  }
}
