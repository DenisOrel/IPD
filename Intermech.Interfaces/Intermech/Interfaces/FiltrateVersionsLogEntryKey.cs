
// Type: Intermech.Interfaces.FiltrateVersionsLogEntryKey
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;


namespace Intermech.Interfaces
{
    /// <summary>Ключ в протоколе подбора версий объектов</summary>
    [Serializable]
    public sealed class FiltrateVersionsLogEntryKey
    {
      /// <summary>Идентификатор связи</summary>
      public long F_PRJLINK_ID;
      /// <summary>Идентификатор версии объектов</summary>
      public long F_OBJECT_ID;

      /// <summary>Создать экземпляр класса</summary>
      /// <param name="prjLinkID">Идентификатор связи</param>
      /// <param name="objectID">Идентификатор версии объектов</param>
      public FiltrateVersionsLogEntryKey(long prjLinkID, long objectID)
      {
        this.F_PRJLINK_ID = prjLinkID;
        this.F_OBJECT_ID = objectID;
      }

      /// <summary>Рассчитать 32-битный хэш-код экземпляра класса</summary>
      /// <returns>32-битный хэш-код экземпляра класса</returns>
      public override int GetHashCode()
      {
        return this.F_PRJLINK_ID.GetHashCode() << 16 /*0x10*/ ^ this.F_OBJECT_ID.GetHashCode();
      }

      /// <summary>Сравнить с указанным объектом</summary>
      /// <param name="obj">Объект для сравнения</param>
      /// <returns>true - объекты равны</returns>
      public override bool Equals(object obj)
      {
        return obj is FiltrateVersionsLogEntryKey versionsLogEntryKey && this.F_PRJLINK_ID == versionsLogEntryKey.F_PRJLINK_ID && this.F_OBJECT_ID == versionsLogEntryKey.F_OBJECT_ID;
      }
    }
}
