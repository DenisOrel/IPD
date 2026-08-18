
// Type: Intermech.Client.Core.MyFullObjectElement
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;
using System.Collections;


namespace Intermech.Client.Core;

/// <summary>Класс для хранения некоторых свойств объекта</summary>
[Serializable]
public sealed class MyFullObjectElement : ICloneable
{
  /// <summary>Идентификатор объекта ("F_ID")</summary>
  public long ID;
  /// <summary>Идентификатор версии объекта ("F_OBJECT_ID")</summary>
  public long ObjectID;
  /// <summary>Идентификатор типа объекта ("F_OBJECT_TYPE")</summary>
  public int ObjectType = -1;
  /// <summary>Идентификатор связи объекта ("F_PRJLINK_ID")</summary>
  public long PrjLinkID;
  /// <summary>Идентификатор типа связи ("F_RELATION_TYPE")</summary>
  public int RelationType = -1;
  /// <summary>Заголовок объекта (CAPTION)</summary>
  public string Caption = string.Empty;
  /// <summary>Какой-либо флажок для объекта</summary>
  public bool ObjectBool;
  /// <summary>Guid объекта (F_GUID)</summary>
  public Guid ObjectGuid = Guid.Empty;
  /// <summary>Владелец объекта</summary>
  public long Owner;
  /// <summary>Значение атрибута "Сортировка"</summary>
  public long Sorting;
  /// <summary>Значение атрибута "Шаг ЖЦ"</summary>
  public int LCStepID = -1;
  /// <summary>Дополнительные пользовательские данные</summary>
  public ArrayList Tags = new ArrayList(0);
  /// <summary>Версия объекта</summary>
  public long Version;
  /// <summary>Признак базовой версии</summary>
  public long BaseVersion;

  /// <summary>Создать пустой экземпляр класса</summary>
  public MyFullObjectElement()
  {
  }

  /// <summary>Создать заполненный экземпляр класса</summary>
  /// <param name="AnID">ID объекта (F_ID)</param>
  /// <param name="AnObjectID">ID версии объекта (F_OBJECT_ID)</param>
  /// <param name="AnObjectType">ID типа объекта (F_OBJECT_TYPE)</param>
  /// <param name="APrjLinkID">ID связи (F_PRJLINK_ID)</param>
  /// <param name="ARelationType">ID типа связи (F_RELATION_TYPE)</param>
  /// <param name="ACaption">Заголовок объекта (CAPTION)</param>
  /// <param name="AnObjectBool">Какой-либо флажок для объекта</param>
  /// <param name="AnObjectGuid">Guid объекта (F_GUID)</param>
  /// <param name="Owner">Владелец объекта</param>
  /// <param name="Sorting">Значение атрибута "Сортировка"</param>
  /// <param name="LCStepID">Шаг ЖЦ</param>
  /// <param name="Version">Версия объекта</param>
  /// <param name="BaseVersion">Признак базовой версии</param>
  /// <param name="ATags">Пользовательские данные</param>
  public MyFullObjectElement(
    long AnID,
    long AnObjectID,
    int AnObjectType,
    long APrjLinkID,
    int ARelationType,
    string ACaption,
    bool AnObjectBool,
    Guid AnObjectGuid,
    long Owner,
    long Sorting,
    int LCStepID,
    long Version,
    long BaseVersion,
    params object[] ATags)
  {
    this.ID = AnID;
    this.ObjectID = AnObjectID;
    this.ObjectType = AnObjectType;
    this.PrjLinkID = APrjLinkID;
    this.RelationType = ARelationType;
    this.Caption = ACaption;
    this.ObjectBool = AnObjectBool;
    this.ObjectGuid = AnObjectGuid;
    this.Owner = Owner;
    this.Sorting = Sorting;
    this.LCStepID = LCStepID;
    this.Version = Version;
    this.BaseVersion = BaseVersion;
    if (this.Tags == null)
      this.Tags = new ArrayList(0);
    this.Tags.Clear();
    if (ATags == null || ATags.Length == 0)
      return;
    for (int index = 0; index < ATags.Length; ++index)
      this.Tags.Add(ATags[index]);
  }

  /// <summary>Очистка полей</summary>
  public void Clear()
  {
    this.ID = 0L;
    this.ObjectID = 0L;
    this.ObjectType = -1;
    this.PrjLinkID = 0L;
    this.RelationType = -1;
    this.Caption = string.Empty;
    this.ObjectBool = false;
    this.ObjectGuid = Guid.Empty;
    this.Owner = 0L;
    this.Sorting = 0L;
    this.LCStepID = -1;
    this.Version = 0L;
    this.BaseVersion = 0L;
  }

  /// <summary>Перекрытый метод для возвращения заголовка</summary>
  /// <returns></returns>
  public override string ToString()
  {
    return $"[{this.ObjectID}.{this.ID}] {this.Caption} ({this.ObjectGuid})";
  }

  /// <summary>Сделать клон объекта</summary>
  /// <returns>Вернёт 100% копию объекта</returns>
  public object Clone()
  {
    object[] objArray = (object[]) null;
    if (this.Tags.Count > 0)
    {
      objArray = new object[this.Tags.Count];
      this.Tags.CopyTo((Array) objArray);
    }
    return (object) new MyFullObjectElement(this.ID, this.ObjectID, this.ObjectType, this.PrjLinkID, this.RelationType, this.Caption, this.ObjectBool, this.ObjectGuid, this.Owner, this.Sorting, this.LCStepID, this.Version, this.BaseVersion, objArray);
  }
}
