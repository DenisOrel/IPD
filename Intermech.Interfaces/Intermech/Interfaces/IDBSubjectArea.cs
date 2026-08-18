
// Type: Intermech.Interfaces.IDBSubjectArea
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml


namespace Intermech.Interfaces
{
    /// <summary>
    /// Интерфейс, определяющий предметные области реализуещего его объекта.
    /// </summary>
    public interface IDBSubjectArea
    {
      /// <summary>
      /// Строка символов, определяющих предметные области, к которым относится данных
      /// объект. Если пусто, то относится ко всем областям.
      /// </summary>
      string SubjectAreas { get; set; }

      /// <summary>
      /// Возвращает строку с названиями предметных областей, к которым
      /// относится данный объект. Например, "Машиностроение, Строительство".
      /// Если в SubjectAreas пусто, то вернет строку "Все".
      /// </summary>
      string SubjectAreasCaption { get; }
    }
}
