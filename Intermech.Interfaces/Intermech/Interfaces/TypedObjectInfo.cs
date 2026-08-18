
// Type: Intermech.Interfaces.TypedObjectInfo
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Localization;
using System;
using System.Text;


namespace Intermech.Interfaces
{
    /// <summary>
    /// Класс, позволяющий задать идентификатор версии объекта и его тип
    /// </summary>
    [Serializable]
    public sealed class TypedObjectInfo
    {
      /// <summary>Идентификатор версии объекта</summary>
      public long F_OBJECT_ID;
      /// <summary>Идентификатор типа объекта</summary>
      public int F_OBJECT_TYPE = -1;

      /// <summary>Создать экземпляр класса</summary>
      /// <param name="objID">Идентификатор версии объекта</param>
      /// <param name="objType">Идентификатор типа объекта</param>
      public TypedObjectInfo(long objID, int objType)
      {
        this.F_OBJECT_ID = objID;
        this.F_OBJECT_TYPE = objType;
      }

      /// <summary>Сравнить с указанным объектом</summary>
      /// <param name="obj">Объект для сравнения</param>
      /// <returns>true - объекты равны</returns>
      public override bool Equals(object obj)
      {
        return obj is TypedObjectInfo typedObjectInfo && this.F_OBJECT_ID == typedObjectInfo.F_OBJECT_ID && this.F_OBJECT_TYPE == typedObjectInfo.F_OBJECT_TYPE;
      }

      /// <summary>Вернуть 32-битный хэш-код экземпляра класса</summary>
      /// <returns>32-битный хэш-код экземпляра класса</returns>
      public override int GetHashCode()
      {
        return this.F_OBJECT_ID.GetHashCode() << 16 /*0x10*/ & this.F_OBJECT_TYPE.GetHashCode();
      }

      /// <summary>Вернуть строковое представление экземпляра класса</summary>
      /// <returns>Строковое представление экземпляра класса</returns>
      public override string ToString()
      {
        StringBuilder stringBuilder = new StringBuilder();
        if (this.F_OBJECT_ID != 0L)
          stringBuilder.Append(string.Format(LocalizationHolder.rm.GetString("Interfaces_706"), (object) this.F_OBJECT_ID));
        if (this.F_OBJECT_TYPE != -1)
        {
          string objectTypeName = MetaDataHelper.GetObjectTypeName(this.F_OBJECT_TYPE);
          if (!string.IsNullOrEmpty(objectTypeName))
          {
            if (stringBuilder.Length > 0)
              stringBuilder.Append(", ");
            stringBuilder.Append(string.Format(LocalizationHolder.rm.GetString("Interfaces_707"), (object) objectTypeName, (object) this.F_OBJECT_TYPE));
          }
        }
        return stringBuilder.ToString();
      }
    }
}
