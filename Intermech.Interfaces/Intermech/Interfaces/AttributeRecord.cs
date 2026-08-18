
// Type: Intermech.Interfaces.AttributeRecord
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;


namespace Intermech.Interfaces
{
    /// <summary>
    /// Базовый класс, для атрибутов объектов и связей (используется при перекачке данных)
    /// </summary>
    [Serializable]
    public class AttributeRecord : ICloneable, IComparable<AttributeRecord>
    {
      /// <summary>Идентификатор атрибута</summary>
      public int AttributeId;
      /// <summary>
      /// Идентификатор версии объекта или связи, которым принадлежит этот атрибут
      /// </summary>
      public long AttributableId;
      /// <summary>Номер в списке</summary>
      public int InlistId;
      /// <summary>Значение для целых чисел</summary>
      public object IntegerValue;
      /// <summary>При передаче ссылки на объект - GUID этого объекта</summary>
      public object IntegerGuid;
      /// <summary>Значение для вещественных чисел</summary>
      public object DoubleValue;
      /// <summary>?</summary>
      public object DoubleGuid;
      /// <summary>Значение для строк</summary>
      public object StringValue;
      /// <summary>Значение для дат</summary>
      public object DateValue;
      /// <summary>Реальный размер блоба</summary>
      public object FileSize;
      /// <summary>Метод запаковки блоба</summary>
      public object ArcMethod;
      /// <summary>Примечание</summary>
      public object FileNote;
      /// <summary>
      /// путь к файлу с блоб/мемо (локальный); не заполняется при экспорте портфеля
      /// </summary>
      public string Path2File;
      /// <summary>Флаг нового атрибута</summary>
      public bool IsNew;
      /// <summary>Тип файла в файловом шкафу</summary>
      public object FileType;
      /// <summary>
      /// Автор файла
      /// !!! ACHTUNG !!!
      /// При использовании программой миграции тут указывать ObjectID, во всех остальных случаях ObjectGuid
      /// !!!!!!!!!!!!!!!
      /// </summary>
      public object FileAuthor;
      /// <summary>Значение для атрибута</summary>
      /// <remarks>Для XmlDataExchange only</remarks>
      public object ValueData;
      /// <summary>Список значений дополнительных полей</summary>
      /// <remarks>Для XmlDataExchange</remarks>
      [OptionalField]
      public Dictionary<string, object> ExtraData;

      public AttributeRecord() => this.IsNew = true;

      public AttributeRecord(int attributeId)
      {
        this.AttributeId = attributeId;
        this.InlistId = 0;
        this.IsNew = true;
      }

      public AttributeRecord(int attributeId, long attributableId)
        : this(attributeId)
      {
        this.AttributableId = attributableId;
      }

      public AttributeRecord(AttributeRecord rec, long attributableId)
        : this(rec.AttributeId, attributableId, rec.InlistId, rec.IntegerValue, rec.IntegerGuid, rec.DoubleValue, rec.DoubleGuid, rec.StringValue, rec.DateValue, rec.FileSize, rec.ArcMethod, rec.FileNote, rec.Path2File, rec.FileType, rec.FileAuthor)
      {
      }

      public AttributeRecord(
        int attributeId,
        long attributableId,
        int inlistId,
        object integerValue,
        object integerGuid,
        object doubleValue,
        object doubleGuid,
        object stringValue,
        object dateValue,
        object fileSize,
        object arcMethod,
        object fileNote,
        string path2File,
        object fileType,
        object fileAuthor)
      {
        this.AttributeId = attributeId;
        this.AttributableId = attributableId;
        this.InlistId = inlistId;
        this.IntegerValue = integerValue;
        this.IntegerGuid = integerGuid;
        this.DoubleValue = doubleValue;
        this.DoubleGuid = doubleGuid;
        this.StringValue = stringValue;
        this.DateValue = dateValue;
        this.FileSize = fileSize;
        this.ArcMethod = arcMethod;
        this.FileNote = fileNote;
        this.Path2File = path2File;
        this.FileType = fileType;
        this.FileAuthor = fileAuthor;
        this.IsNew = true;
      }

      public AttributeRecord(
        int attributeId,
        long attributableId,
        int inlistId,
        object integerValue,
        object integerGuid,
        object doubleValue,
        object doubleGuid,
        object stringValue,
        object dateValue,
        object fileSize = null,
        object arcMethod = null,
        object fileNote = null,
        string path2File = null)
        : this(attributeId, attributableId, inlistId, integerValue, integerGuid, doubleValue, doubleGuid, stringValue, dateValue, fileSize, arcMethod, fileNote, path2File, (object) FileTypes.ftNormal, (object) null)
      {
      }

      /// <summary>Создание копии объекта</summary>
      /// <returns></returns>
      public virtual object Clone() => this.MemberwiseClone();

      /// <summary>Сравнение элементов</summary>
      /// <param name="other"></param>
      /// <returns></returns>
      public virtual int CompareTo(AttributeRecord other)
      {
        if (other == null)
          return -1;
        int num = this.AttributeId.CompareTo(other.AttributeId);
        return num != 0 ? num : this.InlistId.CompareTo(other.InlistId);
      }

      /// <summary>Проверка объекта на "пустые" значения</summary>
      /// <returns></returns>
      public virtual bool IsEmpyValues()
      {
        return this.IntegerValue == null && this.IntegerGuid == null && this.DoubleValue == null && this.DoubleGuid == null && string.IsNullOrEmpty(this.StringValue as string) && this.DateValue == null && this.FileSize == null && this.ArcMethod == null && this.FileNote == null && string.IsNullOrEmpty(this.Path2File) && (this.FileType == null || this.FileType.Equals((object) FileTypes.ftNormal)) && this.FileAuthor == null && this.ValueData == null;
      }
    }
}
