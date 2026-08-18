
// Type: Intermech.Interfaces.IIDLinkTranslate
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;


namespace Intermech.Interfaces
{
    /// <summary>
    /// Служба предоставляет набор ф-ций для работы с атрибутами, которые содержат ссылку на объект,
    /// но используют тип ftInteger (используется при миграции данных портфелем и службой автообновления)
    /// </summary>
    public interface IIDLinkTranslate
    {
      /// <summary>
      /// Событие, генерируемое в момент запроса по глобальному идентификатору объекта
      /// информации о том, является ли он целочисленной ссылкой на объект. Подписчику необходимо сначала проверить
      /// флаг Handled и если он false, то провести свою проверку атрибута. Если в результате проверки однозначно определили
      /// тот или иной результат - записать его в флаг IsIDLink и установить флаг Handled в true.
      /// </summary>
      event IsIDLinkEventHandler IsIDLinkEvent;

      /// <summary>
      /// Функция возвращает информацию о том является ли атрибут с глобальным идентификатором
      /// attributeGuid целочисленной ссылкой на объект
      /// </summary>
      /// <param name="attributeGuid">Глобальный идентификатор атрибута</param>
      /// <returns></returns>
      bool IsIDLink(Guid attributeGuid);

      /// <summary>
      /// Функция возвращает информацию о том является ли атрибут с глобальным идентификатором
      /// attributeID целочисленной ссылкой на объект
      /// </summary>
      /// <param name="attributeID">Идентификатор атрибута</param>
      /// <returns></returns>
      bool IsIDLink(int attributeID);
    }
}
