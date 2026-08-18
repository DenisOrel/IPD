
// Type: Intermech.Interfaces.IBlackWidthService
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;


namespace Intermech.Interfaces
{
    /// <summary>служба для работы с настройками толщины для цвета в Acad</summary>
    public interface IBlackWidthService
    {
      /// <summary>Событие об изменении цвета на странице</summary>
      event EventHandler Changed;

      /// <summary>Событие будет дёргаться при необходимости</summary>
      void OnChanged();

      /// <summary>получить по индексу сам класс настроек толщины для цвета в Acad</summary>
      /// <param name="index">индекс цвета в Acad(1..255)</param>
      /// <returns>класс настроек толщины</returns>
      ColorWidth this[byte index] { get; }

      /// <summary>все цвета привести к чёрному</summary>
      bool AllColorToBlack { get; set; }

      /// <summary>сохранить настройки</summary>
      void SaveSettings();
    }
}
