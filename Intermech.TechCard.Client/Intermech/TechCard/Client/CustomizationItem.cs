// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.CustomizationItem
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using System;

#nullable disable
namespace Intermech.TechCard.Client;

/// <summary>
/// Элемент для настройки видимости столбцов в DevExpess.TreeList
/// </summary>
public class CustomizationItem
{
  private Guid _attrGuid;
  private string _attrName;

  /// <summary>
  /// 
  /// </summary>
  private void InitData()
  {
    this._attrGuid = Guid.Empty;
    this._attrName = string.Empty;
  }

  /// <summary>Конструктор</summary>
  public CustomizationItem() => this.InitData();

  /// <summary>Конструктор</summary>
  public CustomizationItem(Guid attrGuid, string attrName)
  {
    this._attrGuid = attrGuid;
    this._attrName = attrName;
  }

  /// <summary>Guid атрибута</summary>
  public Guid AttrGuid
  {
    get => this._attrGuid;
    set => this._attrGuid = value;
  }

  /// <summary>Имя атрибута</summary>
  public string AttrName
  {
    get => this._attrName;
    set => this._attrName = value;
  }

  /// <summary>Получение строкового представления класса</summary>
  /// <returns></returns>
  public override string ToString() => this._attrName;
}
