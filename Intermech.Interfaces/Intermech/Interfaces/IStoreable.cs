
// Type: Intermech.Interfaces.IStoreable
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml


namespace Intermech.Interfaces
{
    /// <summary>
    /// Интерфейс, позволяющий объекту читать/записывать своё содержимое в объекты базы данных
    /// </summary>
    public interface IStoreable
    {
      /// <summary>Загрузить информацию из объекта/связи базы данных</summary>
      /// <param name="obj">Источник</param>
      /// <returns>true - информация загружена успешно, false - были ошибки</returns>
      bool LoadFromObject(IDBAttributable obj);

      /// <summary>Записать информацию в указанный элемент базы данных</summary>
      /// <param name="obj">Элемент-назначение</param>
      /// <returns>true - вся информация записана успешно, false - были ошибки</returns>
      bool SaveToObject(IDBAttributable obj);
    }
}
