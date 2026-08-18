
// Type: Intermech.Interfaces.FileNameHelper
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System.Collections.Generic;
using System.IO;


namespace Intermech.Interfaces
{
    /// <summary>Хелпер для работы с именами файлов</summary>
    public class FileNameHelper
    {
      public static readonly char[] InvalidFileNameChars = Path.GetInvalidFileNameChars();
      public static readonly char[] TrimFileNameChars = new char[2]
      {
        '_',
        ' '
      };
      private static char[] InvalidProtoFileNameChars = (char[]) null;

      public static char[] GetInvalidProtoFileNameChars()
      {
        if (FileNameHelper.InvalidProtoFileNameChars == null)
        {
          List<char> charList = new List<char>((IEnumerable<char>) Path.GetInvalidFileNameChars());
          charList.Remove('\\');
          FileNameHelper.InvalidProtoFileNameChars = charList.ToArray();
        }
        return FileNameHelper.InvalidProtoFileNameChars;
      }

      public static string ReplaceInvalidFileNameChars(string filename)
      {
        if (!string.IsNullOrEmpty(filename))
        {
          int length = filename.IndexOfAny(FileNameHelper.InvalidFileNameChars);
          if (length >= 0)
          {
            for (; length >= 0; length = filename.IndexOfAny(FileNameHelper.InvalidFileNameChars))
              filename = $"{filename.Substring(0, length)}_{filename.Substring(length + 1)}";
            filename = filename.Trim(FileNameHelper.TrimFileNameChars);
          }
        }
        return filename;
      }

      /// <summary>
      /// Метод заменяет в имени файла недопустимые символы на подчеркивание. Исключение составляет символ '\' согласно записи N1344600, который может применяться при задании имени файла по прототипу
      /// </summary>
      /// <param name="filename">Исходное имя файла</param>
      /// <returns>Имя после замены недопустимых символов</returns>
      public static string ReplaceInvalidProtoFileNameChars(string filename)
      {
        if (!string.IsNullOrEmpty(filename))
        {
          int length = filename.IndexOfAny(FileNameHelper.GetInvalidProtoFileNameChars());
          if (length >= 0)
          {
            for (; length >= 0; length = filename.IndexOfAny(FileNameHelper.InvalidFileNameChars))
              filename = $"{filename.Substring(0, length)}_{filename.Substring(length + 1)}";
            filename = filename.Trim(FileNameHelper.TrimFileNameChars);
          }
        }
        return filename;
      }
    }
}
