
// Type: Intermech.IniFiles.InMemoryIniFile
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml


namespace Intermech.IniFiles
{
    /// <summary>
    /// Реализует класс, читающий содержимое ini-файла из строки.
    /// </summary>
    public class InMemoryIniFile : IniFileBase
    {
      /// <summary>Создает объект.</summary>
      /// <param name="iniFileContent">Содержимое ini-файла</param>
      public InMemoryIniFile(string iniFileContent) => this.Initialize(iniFileContent);
    }
}
