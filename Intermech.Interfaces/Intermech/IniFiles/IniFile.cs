
// Type: Intermech.IniFiles.IniFile
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System.IO;


namespace Intermech.IniFiles
{
    /// <summary>Реализует класс, читающий ini-файлы.</summary>
    public class IniFile : IniFileBase
    {
      protected string iniFileName;

      /// <summary>Создает объект.</summary>
      /// <param name="iniFileName">Имя файла</param>
      public IniFile(string iniFileName)
      {
        this.iniFileName = iniFileName;
        using (TextReader textReader = (TextReader) new StreamReader(iniFileName))
          this.Initialize(textReader.ReadToEnd());
      }
    }
}
