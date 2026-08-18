
// Type: Intermech.IO.PathNormalizer
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System;
using System.IO;


namespace Intermech.IO
{
    public class PathNormalizer
    {
      private string basePath;

      public PathNormalizer(string basePath)
      {
        this.basePath = !string.IsNullOrEmpty(basePath) ? basePath : throw new ArgumentException("Не указан базовый путь для нормализации путей файлов и папок.", nameof (basePath));
      }

      public string Normalize(string path)
      {
        if (!string.IsNullOrEmpty(path))
        {
          if (path.Length >= 2 && this.IsDirectorySeparator(path, 0) && !this.IsDirectorySeparator(path, 1))
            path = path.Substring(1);
          if (!Path.IsPathRooted(path))
            path = Path.Combine(this.basePath, path);
          if (path.Contains(".\\") || path.Contains("..\\"))
            path = Path.GetFullPath(path);
        }
        return path;
      }

      private bool IsDirectorySeparator(string path, int charIndex)
      {
        char ch = path[charIndex];
        return (int) ch == (int) Path.DirectorySeparatorChar || (int) ch == (int) Path.AltDirectorySeparatorChar;
      }
    }
}
