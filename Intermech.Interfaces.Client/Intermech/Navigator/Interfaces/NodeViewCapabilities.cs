// Decompiled with JetBrains decompiler
// Type: Intermech.Navigator.Interfaces.NodeViewCapabilities
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

#nullable disable
namespace Intermech.Navigator.Interfaces;

/// <summary>
/// Содержит сведения о возможностях вида, который для получения выводимой информации использует элементы навигации.
/// </summary>
public class NodeViewCapabilities
{
  /// <summary>Содержимое узла</summary>
  private ContentType _contentType;
  /// <summary>Коллекция колонок атрибутов узла</summary>
  private NodeColumnCollection _columns;
  /// <summary>Можно ли дополнять</summary>
  private bool _canAppend;

  /// <summary>Конструктор</summary>
  /// <param name="contentType">Содержимое узла</param>
  /// <param name="columns">Коллекция колонок атрибутов узла</param>
  /// <param name="canAppend">Можно ли дополнять</param>
  public NodeViewCapabilities(
    ContentType contentType,
    NodeColumnCollection columns,
    bool canAppend)
  {
    this._contentType = contentType;
    this._columns = columns;
    this._canAppend = canAppend;
  }

  /// <summary>Содержимое узла</summary>
  public ContentType ContentType => this._contentType;

  /// <summary>Коллекция колонок атрибутов узла</summary>
  public NodeColumnCollection Columns => this._columns;

  /// <summary>Можно ли дополнять</summary>
  public bool CanAppend => this._canAppend;
}
