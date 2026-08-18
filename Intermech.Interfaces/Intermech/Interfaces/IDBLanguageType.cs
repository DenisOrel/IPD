
// Type: Intermech.Interfaces.IDBLanguageType
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;


namespace Intermech.Interfaces
{
    /// <summary>
    /// Интерфейс для модификации и удаления языковых вариантов.
    /// </summary>
    public interface IDBLanguageType
    {
      /// <summary>Идентификатор языка</summary>
      string LanguageID { get; }

      /// <summary>Имя языка (например, русский)</summary>
      string LanguageName { get; set; }

      /// <summary>
      /// Если true, то этот язык используется в системе по-умолчанию.
      /// </summary>
      bool IsDefaultLanguage { get; set; }

      /// <summary>
      /// Присваивает языковому варианту новый глобальный идентификатор
      /// </summary>
      Guid GUID { get; set; }

      /// <summary>
      /// Удалить предметную область. DeleteMode пока не используется.
      /// </summary>
      int Delete(long DeleteMode);

      /// <summary>
      /// Идентификатор культуры для данного языкового варианта (используется для локализации интерфейса прилложений)
      /// </summary>
      string CultureID { get; set; }
    }
}
