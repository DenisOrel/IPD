
// Type: Intermech.Interfaces.IObjectsInfoCache
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;


namespace Intermech.Interfaces
{
    /// <summary>
    /// Интерфейс для извлечения из кэша заголовков и кратких описаний объектов по их идентификаторам в базе данных
    /// </summary>
    public interface IObjectsInfoCache
    {
      /// <summary>
      /// По идентификатору версии объекта получить заголовок объекта
      /// </summary>
      /// <param name="objectID">Идентификатор версии объекта</param>
      /// <returns>Заголовок объекта</returns>
      string GetObjectCaption(long objectID);

      /// <summary>По Guid версии объекта получить его заголовок</summary>
      /// <param name="objectGuid">Guid версии объекта</param>
      /// <returns>Заголовок объекта</returns>
      string GetObjectCaption(Guid objectGuid);

      /// <summary>
      /// По идентификатору версии объекта получить его краткое описание
      /// </summary>
      /// <param name="objectID">Идентификатор версии объекта</param>
      /// <returns>Краткое описание версии объекта</returns>
      QuickObjectInfo GetObjectInfo(long objectID);

      /// <summary>По Guid версии объекта получить его краткое описание</summary>
      /// <param name="objectGuid">Guid версии объекта</param>
      /// <returns>Краткое описание версии объекта</returns>
      QuickObjectInfo GetObjectInfo(Guid objectGuid);

      /// <summary>
      /// По идентификатору объекта (не версии) получить заголовок базовой версии объекта
      /// </summary>
      /// <param name="ID">Идентификатор объекта (не версии)</param>
      /// <returns></returns>
      string GetObjectCaptionByID(long ID);

      /// <summary>
      /// По идентификатору объекта получить краткое описание базовой версии объекта
      /// </summary>
      /// <param name="ID">Идентификатор объекта (не версии)</param>
      /// <returns>Краткое описание базовой версии объекта</returns>
      QuickObjectInfo GetObjectInfoByID(long ID);

      /// <summary>Сбросить содержимое кэша</summary>
      void Reset();
    }
}
