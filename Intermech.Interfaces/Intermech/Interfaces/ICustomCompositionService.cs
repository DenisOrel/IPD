
// Type: Intermech.Interfaces.ICustomCompositionService
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Remoting.Compression;
using System;


namespace Intermech.Interfaces
{
    /// <summary>
    ///  Базовый интерфейс для получения составов/применяемости объектов БД
    /// </summary>
    public interface ICustomCompositionService
    {
      /// <summary>Получить по указанной серверной задаче её состояние</summary>
      /// <param name="selectGUID">GUID серверной задачи</param>
      /// <returns>Состояние серверной задачи</returns>
      [RemotingCompression(true)]
      CompositionInfo GetInfo(Guid selectGUID);

      /// <summary>Прервать работу серверной задачи</summary>
      /// <param name="selectGUID">GUID прерываемой серверной задачи</param>
      void CancelSelect(Guid selectGUID);
    }
}
