// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.ClipboardObjectsList
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using Intermech.DataFormats;
using Intermech.Interfaces.Client;
using Intermech.Localization;
using Intermech.Navigator.Interfaces;
using System.Collections;

#nullable disable
namespace Intermech.Interfaces;

/// <summary>
/// Список объектов, помещённых в буфер обмена
/// Поддерживает интерфейс ICutCopy, чтобы иметь возможность отличать вырезаные объекты от скопированых
/// </summary>
public class ClipboardObjectsList : DBObjectTypedIDCollection, ICutCopy, IIOSourceInfo
{
  /// <summary>
  /// Признак того, что в буфер обмена помещены не скопированые, а вырезаные объекты
  /// </summary>
  private bool _isCut;
  /// <summary>Информация об источнике объектов</summary>
  private IIOSource _source;
  private IDBTypedObjectID _parent;

  /// <summary>Создать список объектов</summary>
  /// <param name="idList">Список объектов</param>
  /// <param name="isCut">true - объекты были помещены в список с помощью команды Вырезать</param>
  public ClipboardObjectsList(ArrayList idList, bool isCut)
    : this(idList, isCut, (IIOSource) null, (IDBTypedObjectID) null)
  {
  }

  /// <summary>Создать список объектов</summary>
  /// <param name="idList">Список объектов</param>
  /// <param name="isCut">true - объекты были помещены в список с помощью команды Вырезать</param>
  /// <param name="source">Информация об источнике объектов idList</param>
  public ClipboardObjectsList(
    ArrayList idList,
    bool isCut,
    IIOSource source,
    IDBTypedObjectID parent)
    : base(idList)
  {
    this._isCut = isCut;
    this._source = source;
    this._parent = parent;
  }

  /// <summary> Признак того, что в буфер обмена помещены не скопированые, а вырезаные объекты </summary>
  public bool IsCut
  {
    get => this._isCut;
    set => this._isCut = value;
  }

  /// <summary> Индекс иконки </summary>
  public int ImageIndex
  {
    get
    {
      INamedImageList service = (INamedImageList) ServicesManager.ServiceContainer.GetService(typeof (INamedImageList));
      return !this._isCut ? service.ImageIndex("imgCopy") : service.ImageIndex("imgCut");
    }
  }

  /// <summary>Источник объектов</summary>
  public IIOSource Source => this._source;

  public IDBTypedObjectID Parent => this._parent;

  /// <summary> Преобразование в строку </summary>
  /// <returns> Строковое представление того, что находиться в буфере обмена </returns>
  public override string ToString()
  {
    return this.Count <= 0 ? LocalizationHolder.rm.GetString("Interfaces.Client_58") : this[0].ToString();
  }
}
