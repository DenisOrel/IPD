
// Type: Intermech.Interfaces.IDBSubjectAreaCollection
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;


namespace Intermech.Interfaces
{
    public interface IDBSubjectAreaCollection : IDBCollection
    {
      /// <summary>
      /// Возвращает строку с правильными идентификаторами предметных областей,
      /// выбрасывая из нее все идентификаторы, отсутствующие в данной базе.
      /// </summary>
      /// <param name="anAreaID">Исходная строка идентификаторов</param>
      /// <returns></returns>
      string GetValidAreaID(string anAreaID);

      /// <summary>
      /// Проверяет на валидность идентификаторы предметных областей
      /// и выдает исключение InvalidAreaIDException
      /// </summary>
      void ValidateAriasID(string anAreaID);

      /// <summary>
      /// Проверяет на допустимость присвоения метаданным строки с идентификаторами предметных областей
      /// </summary>
      void ValidateAriasString(string anAreaID);

      /// <summary>
      /// Возвращает строку с названиями предметных областей, идентификаторы которых
      /// переданы в параметре areas. Например, "Машиностроение, Строительство".
      /// Если там пусто, то вернет строку "Все".
      /// </summary>
      string GetAreasCaption(string areas);

      /// <summary>
      /// Создает новую предметную область и возвращает ее идентификатор.
      /// </summary>
      /// <param name="areaName">Наименование предметное области</param>
      /// <param name="areaNote">Описание предметное области</param>
      /// <param name="guid">Глобальный идентификатор предметной области</param>
      /// <returns></returns>
      char Create(string areaName, string areaNote, Guid guid);
    }
}
