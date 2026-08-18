
// Type: Intermech.Interfaces.OSHelper
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System.IO;


namespace Intermech.Interfaces
{
    public class OSHelper
    {
      /// <summary>Заменить запрещенные символы для имен файлов</summary>
      /// <param name="fileName">Имя файла</param>
      /// <returns></returns>
      public static string ReplaceForbiddenSymbols(string fileName)
      {
        return OSHelper.ReplaceForbiddenSymbols(fileName, '_');
      }

      /// <summary>Заменить запрещенные символы для имен файлов</summary>
      /// <param name="fileName">Имя файла</param>
      /// <param name="ch">Символ под замену</param>
      /// <returns></returns>
      public static string ReplaceForbiddenSymbols(string fileName, char ch)
      {
        foreach (char invalidPathChar in Path.GetInvalidPathChars())
          fileName = fileName.Replace(invalidPathChar, ch);
        foreach (char invalidFileNameChar in Path.GetInvalidFileNameChars())
          fileName = fileName.Replace(invalidFileNameChar, ch);
        return fileName;
      }
    }
}
