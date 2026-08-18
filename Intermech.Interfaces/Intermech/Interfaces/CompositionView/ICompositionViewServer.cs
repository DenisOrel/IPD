
// Type: Intermech.Interfaces.CompositionView.ICompositionViewServer
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml


namespace Intermech.Interfaces.CompositionView
{
    /// <summary>
    /// Интерфейс для сохранения и загрузки общих настроек
    /// под системной сессией (т.е. для всех пользователей)
    /// </summary>
    public interface ICompositionViewServer
    {
      /// <summary>Сохранить данные о настройках кнопок</summary>
      /// <param name="data">информация о кнопках</param>
      void SaveButtonsSettings(byte[] data);

      /// <summary>Загрузить данные о настройках кнопок</summary>
      /// <returns>информация о настройках</returns>
      byte[] LoadButtonsSettings();
    }
}
