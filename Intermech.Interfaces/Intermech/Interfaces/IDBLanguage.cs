
// Type: Intermech.Interfaces.IDBLanguage
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml


namespace Intermech.Interfaces
{
    /// <summary>
    /// Интерфейс, определяющий языковое исполнение реализуещего его объекта.
    /// </summary>
    public interface IDBLanguage
    {
      /// <summary>Идентификатор языка</summary>
      string LanguageID { get; set; }

      /// <summary>Имя языка (например, русский)</summary>
      string LanguageName { get; }

      /// <summary>
      /// Если true, то этот язык используется в системе по-умолчанию.
      /// </summary>
      bool IsDefaultLanguage { get; }
    }
}
