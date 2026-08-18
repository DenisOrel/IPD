
// Type: Intermech.Interfaces.NormalizerSettings
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;


namespace Intermech.Interfaces
{
    /// <summary>Структура для передачи настроек нормализатору строк</summary>
    [Serializable]
    public struct NormalizerSettings(
      bool deleteSpaces,
      bool upperCase,
      bool cyrillicReplace,
      string[] deleteDuplicates,
      string[] replaceSymbols)
    {
      public bool DeleteSpaces = deleteSpaces;
      public bool UpperCase = upperCase;
      public bool CyrillicReplace = cyrillicReplace;
      public string[] DeleteDuplicates = deleteDuplicates;
      public string[] ReplaceSymbols = replaceSymbols;
    }
}
