// Decompiled with JetBrains decompiler
// Type: Intermech.DataFormats.DBObjectToCompare
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

#nullable disable
namespace Intermech.DataFormats;

/// <summary>
/// Формат для передачи данных об объектах, файлы которых надо сравнить
/// </summary>
public class DBObjectToCompare
{
  private long _objectId;
  private int _objectTypeId;
  private string _caption;
  private string _nameInMessages;
  private int _versionId;

  /// <summary>Идентификатор версии объекта</summary>
  public long ObjectID => this._objectId;

  /// <summary>Идентификатор типа объекта</summary>
  public int ObjectTypeID => this._objectTypeId;

  /// <summary>Заголовок объекта</summary>
  public string Caption => this._caption;

  /// <summary>
  /// Возвращает строку для отображения имени объекта в информационных сообщениях
  /// </summary>
  public string NameInMessages => this._nameInMessages;

  /// <summary>Порядковый номер версии объекта</summary>
  public int VersionID => this._versionId;

  public DBObjectToCompare(
    long objectId,
    int objectTypeId,
    int versionId,
    string caption,
    string nameInMessages)
  {
    this._objectId = objectId;
    this._objectTypeId = objectTypeId;
    this._versionId = versionId;
    this._caption = caption;
    this._nameInMessages = nameInMessages;
  }

  public override bool Equals(object obj)
  {
    return obj is DBObjectToCompare && this._objectId == (obj as DBObjectToCompare).ObjectID;
  }

  public override int GetHashCode() => this._objectId.GetHashCode();
}
