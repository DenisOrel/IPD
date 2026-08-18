
// Type: Intermech.Interfaces.IFormDesignerFormEventsHandler
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml


namespace Intermech.Interfaces
{
    /// <summary>Обработчик событий для формы.</summary>
    public interface IFormDesignerFormEventsHandler : IFormDesignerEventHandlerBase
    {
      /// <summary>Окончание загрузки данных.</summary>
      /// <remark>Срабатывает после загрузки всех данных. Также возникает после отмены изменений</remark>
      /// <param name="form">Форма редактирования</param>
      void DataLoaded(object form);

      /// <summary>Закрытие формы.</summary>
      /// <remark>Срабатывает после закрытия формы</remark>
      /// <param name="form">Форма редактирования</param>
      void Deactivate(object form);

      /// <summary>Модификация данных.</summary>
      /// <remark>Срабатывает после модификации данных (при первой модификации после загрузки)</remark>
      /// <param name="form">Форма редактирования</param>
      void Modified(object form);
    }
}
