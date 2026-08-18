
// Type: Intermech.Interfaces.IDBAttributeType4
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml


namespace Intermech.Interfaces
{
    /// <summary>
    /// Интерфейс, описывающий общие свойства атрибута в контексте определенного типа
    /// объектов или связей
    /// </summary>
    public interface IDBAttributeType4 : IDBAttributeType
    {
      /// <summary>
      /// Свойство описывает допустимость и обязательность атрибута для данного
      /// типа объектов и связей (см. описание RequiredModes)
      /// </summary>
      RequiredModes Required { get; set; }
    }
}
