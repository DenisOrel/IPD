
// Type: Intermech.Interfaces.IFormDesignerActionHandler
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml


namespace Intermech.Interfaces
{
    /// <summary>
    /// Интерфейс для обработки дополнительных действий по нажатию на кнопку.
    /// </summary>
    public interface IFormDesignerActionHandler
    {
      /// <summary>Проверка состояния кнопки Enabled/Disabled.</summary>
      /// <param name="button">Кнопка (AttrButton)</param>
      /// <param name="form">Форма (DesForm)</param>
      /// <returns>true - если кнопка должна быть Enabled</returns>
      bool ButtonEnabled(object button, object form);

      /// <summary>Собитие на нажатие кнопки.</summary>
      /// <param name="button">Кнопка (AttrButton)</param>
      /// <param name="form">Форма (DesForm)</param>
      void ButtonPressed(object button, object form);
    }
}
