
// Type: Intermech.IO.SymbolicLinkManager
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System;
using System.IO;


namespace Intermech.IO
{
    /// <summary>
    /// Базовый класс для менеджера операций с символическими ссылками. Реализация должна быть thread safe.
    /// </summary>
    public abstract class SymbolicLinkManager
    {
      /// <summary>
      /// Возвращает признак, что операции с символическими ссылками поддерживаются операционной системой.
      /// </summary>
      public abstract bool IsSupported { get; }

      /// <summary>Создает символическую ссылку.</summary>
      /// <param name="symlinkPath">Абсолютный путь символической ссылки</param>
      /// <param name="targetPath">Путь к цели символической ссылки - файлу или каталогу. Может быть в абсолютной или относительной форме</param>
      /// <exception cref="T:ArgumentNullException">symlinkPath || targetPath</exception>
      /// <exception cref="T:ArgumentException">Путь к символической ссылке задан не в абсолютной форме</exception>
      /// <exception cref="T:IOException">Ошибка при создании символической ссылки</exception>
      public void CreateLink(string symlinkPath, string targetPath)
      {
        if (symlinkPath == null)
          throw new ArgumentNullException(nameof (symlinkPath));
        if (!Path.IsPathRooted(symlinkPath))
          throw new ArgumentException("Требуется путь в абсолютной форме.", nameof (symlinkPath));
        if (targetPath == null)
          throw new ArgumentNullException(nameof (targetPath));
        this.DoCreateLink(symlinkPath, targetPath);
      }

      /// <summary>Создает символическую ссылку.</summary>
      /// <param name="symlinkPath">Абсолютный путь символической ссылки</param>
      /// <param name="targetPath">Путь к цели символической ссылки - файлу или каталогу. Может быть в абсолютной или относительной форме</param>
      /// <exception cref="T:IOException">Ошибка при создании символической ссылки</exception>
      protected abstract void DoCreateLink(string symlinkPath, string targetPath);

      /// <summary>
      /// Возвращает путь к цели для указанной символической ссылки. Метод может вернуть null, если указанный путь не является символической ссылкой.
      /// </summary>
      /// <param name="symlinkPath">Абсолютный путь символической ссылки</param>
      /// <returns>Путь к цели символической ссылки в абсолютной форме или null, если указанный путь не является символической ссылкой</returns>
      /// <exception cref="T:ArgumentNullException">symlinkPath</exception>
      /// <exception cref="T:ArgumentException">Путь к символической ссылке задан не в абсолютной форме</exception>
      /// <exception cref="T:IOException">Ошибка при операции с символической ссылкой</exception>
      public string GetLinkTarget(string symlinkPath)
      {
        if (symlinkPath == null)
          throw new ArgumentNullException(nameof (symlinkPath));
        return Path.IsPathRooted(symlinkPath) ? this.DoGetLinkTarget(symlinkPath) : throw new ArgumentException("Требуется путь в абсолютной форме.", nameof (symlinkPath));
      }

      /// <summary>
      /// Возвращает путь к цели для указанной символической ссылки. Метод должен вернуть null, если указанный путь не является символической ссылкой
      /// </summary>
      /// <param name="symlinkPath">Абсолютный путь символической ссылки</param>
      /// <returns>Путь к цели символической ссылки в абсолютной форме или null, если указанный путь не является символической ссылкой</returns>
      /// <exception cref="T:IOException">Ошибка при операции с символической ссылкой</exception>
      protected abstract string DoGetLinkTarget(string symlinkPath);
    }
}
