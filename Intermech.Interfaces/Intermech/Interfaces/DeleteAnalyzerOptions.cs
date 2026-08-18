
// Type: Intermech.Interfaces.DeleteAnalyzerOptions
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;


namespace Intermech.Interfaces
{
    /// <summary>
    /// Набор параметров для анализа списка удаляемых объектов
    /// </summary>
    [Flags]
    [Serializable]
    public enum DeleteAnalyzerOptions
    {
      /// <summary>Никаких опций нет</summary>
      None = 0,
      /// <summary>
      /// Отыскивать связанные объекты - например, конструкторскую документацию
      /// для изделий, исполнения, т.п.
      /// </summary>
      FindLinkedObjects = 1,
      /// <summary>
      /// Выполнять поиск и анализ всех версий удаляемых объектов
      /// </summary>
      FindAllVersions = 2,
      /// <summary>Значения по умолчанию</summary>
      Defaults = FindAllVersions | FindLinkedObjects, // 0x00000003
    }
}
