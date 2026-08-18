
// Type: Intermech.Interfaces.ISpecHandleAttributes
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;


namespace Intermech.Interfaces
{
    /// <summary>
    /// Служба атрибутов для которых требуется специальная обработка значений при операциях импорта
    /// </summary>
    public interface ISpecHandleAttributes
    {
      /// <summary>
      /// Событие, генерируется в момент импорта атрибута для связи.
      /// </summary>
      event SpecHandleAttributeEventHandler SpecHandleRelationAttributeEvent;

      /// <summary>
      /// Событие, генерируется в момент импорта атрибута для объекта
      /// </summary>
      event SpecHandleAttributeEventHandler SpecHandleObjectAttributeEvent;

      /// <summary>Сгенерировать событие для атрибута объекта</summary>
      /// <param name="e"></param>
      void FireEventForObjectAttribute(SpecHandleAttributeEventArgs e);

      /// <summary>Сгенерировать событие для атрибута связи</summary>
      /// <param name="e"></param>
      void FireEventForRelationAttribute(SpecHandleAttributeEventArgs e);

      /// <summary>
      /// Регистрировать необновляемый атрибут (СУЩЕСВУЮЩИЙ у объекта/связи атрибут, который не будет обновляться при импорте)
      /// </summary>
      /// <param name="attributeGuid">Глобальный идентификатор атрибута</param>
      void RegisterNotUpdatingAttribute(Guid attributeGuid);

      /// <summary>Флаг того, что атрибут является не обновляемым</summary>
      /// <param name="attributeGuid">Глобальный идентификатор атрибута</param>
      /// <returns></returns>
      bool IsNotUpdatingAttribute(Guid attributeGuid);
    }
}
