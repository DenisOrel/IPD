
// Type: Intermech.Interfaces.Sets.IEditableString
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml


namespace Intermech.Interfaces.Sets
{
    /// <summary>
    /// Интерфейс, позволяющий получать/задавать содержимое класса в виде строки, удобной для редактирования пользователем
    /// </summary>
    public interface IEditableString
    {
      /// <summary>
      /// Содержимое в виде строки, удобном для редактирования пользователем
      /// </summary>
      string AsEditableString { get; set; }
    }
}
