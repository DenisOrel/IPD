
// Type: Intermech.Interfaces.IDBLanguageCollection
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;


namespace Intermech.Interfaces
{
    public interface IDBLanguageCollection : IDBCollection
    {
      /// <summary>
      /// Проверяет на валидность строку идентификаторов языков
      /// и выдает исключение InvalidLanguageIDException
      /// </summary>
      void CheckValidLanguageID(string languageIDs);

      /// <summary>
      /// Создает новый языковой вариант с именем languageName и возвращает его
      /// символьный идентификатор. cultureID - идентификатор локализации (например, en - англоязычная локализация)
      /// </summary>
      char Create(string languageName, Guid guid, string cultureID);

      /// <summary>
      /// Возвращает идентификатор языка, заданного в системе по умолчанию
      /// </summary>
      string DefaultLanguageID { get; }
    }
}
