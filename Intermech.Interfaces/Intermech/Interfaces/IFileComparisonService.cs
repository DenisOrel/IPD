
// Type: Intermech.Interfaces.IFileComparisonService
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;


namespace Intermech.Interfaces
{
    /// <summary>
    /// Серверный сервис, используемый при выполнении команд сравнения файлов объектов
    /// </summary>
    public interface IFileComparisonService
    {
      /// <summary>
      /// Проверка, являются ли документы составными
      /// (есть ли в составе документов другие документы, включенные связью "Состав документации")
      /// </summary>
      /// <returns>true - если составные</returns>
      bool DocsAreComposite(Guid sessionGUID, long[] docIds);
    }
}
