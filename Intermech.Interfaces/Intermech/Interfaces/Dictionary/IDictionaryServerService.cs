
// Type: Intermech.Interfaces.Dictionary.IDictionaryServerService
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;
using System.Collections.Generic;


namespace Intermech.Interfaces.Dictionary
{
    /// <summary>Interface for dictionary server service</summary>
    public interface IDictionaryServerService : IDictionaryService
    {
      /// <summary>Get attribute's description</summary>
      /// <param name="attr"></param>
      /// <returns></returns>
      string GetDescription(IDBAttribute attr);

      /// <summary>
      /// Функция проверяет есть ли в формуле formula ссылка на атрибут attrType
      /// </summary>
      bool IsAttributeExistsInValue(IDBAttributeType attrType, string formula);

      /// <summary>Расшифровка значений атрибутов.</summary>
      /// <param name="sessionGuid">Глобальный идентификатор сессии пользователя</param>
      /// <param name="parseList">Список атрибутов, которые необходимо расшифровать</param>
      /// <param name="forParseDict">Список атрибутов, которые могут участвовать в расшифровке других атрибутов</param>
      /// <returns>Список атрибутов с расшифрованными значениями</returns>
      List<AttributeValues> ParseAttributes(
        Guid sessionGuid,
        List<AttributeValues> parseList,
        System.Collections.Generic.Dictionary<string, AttributeValues> forParseDict);
    }
}
