
// Type: Intermech.IO.FilesStorage
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System;
using System.IO;


namespace Intermech.IO
{
    /// <summary>
    /// Класс, представляющий собой папку с файлами. Предназначен для замены таких штук, как изолированное хранилище файлов.
    /// Хранит файлы в иерархической структуре подпапок, поэтому может быстро работать с большим количеством файлов.
    /// </summary>
    public class FilesStorage
    {
      /// <summary>Путь и имя корневой папки хранилища</summary>
      private string _RootName;
      /// <summary>
      /// Имя папки в каталоге ApplicationData для размещения подпапок с хранилищами файлов
      /// </summary>
      public const string AppDataIPSRootFolder = "Intermech";
      /// <summary>
      /// Имя файла с инфой о том, какому процессу принадлежит данная папка
      /// </summary>
      public const string OwnerFileName = "owner.txt";
      /// <summary>
      /// Полный путь к файлу с инфой о том, какому процессу принадлежит данная папка
      /// </summary>
      private string _FullOwnerFileName;

      /// <summary>Конструктор</summary>
      /// <param name="rootPath">Путь к папке. Если пустой, то создает в ApplicationData</param>
      /// <param name="folderName">Имя папки</param>
      public FilesStorage(string rootPath, string folderName)
      {
        if (rootPath == null || rootPath == string.Empty)
          rootPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Intermech");
        this._RootName = Path.Combine(rootPath, folderName + (object) Math.Abs(FilesStorage.CurrentApplicationName.GetHashCode()));
        if (Directory.Exists(this._RootName))
          return;
        Directory.CreateDirectory(this._RootName);
      }

      /// <summary>Путь и имя корневой папки хранилища</summary>
      public string RootName => this._RootName;

      /// <summary>
      /// Возвращает полный путь и имя текущему исполняемого файла в нижнем регистре
      /// </summary>
      public static string CurrentApplicationName
      {
        get => Environment.GetCommandLineArgs()[0].Trim().ToLower();
      }

      private string FullOwnerFileName
      {
        get
        {
          if (this._FullOwnerFileName == null)
            this._FullOwnerFileName = Path.Combine(this.RootName, "owner.txt");
          return this._FullOwnerFileName;
        }
      }

      /// <summary>
      /// Метод проверяет эксклюзивный доступ к данному каталогу текущего экзешника. Выдает исключение, если каталог создан другим приложением (загруженным из другого каталога)
      /// </summary>
      public void LockFolder()
      {
        if (File.Exists(this.FullOwnerFileName))
        {
          string str = File.ReadAllText(this.FullOwnerFileName);
          if (str.Trim() != FilesStorage.CurrentApplicationName)
            throw new Exception($"Ошибка получения эксклюзивного доступа к каталогу '{this.RootName}'. Каталог заблокирован приложением {str}.");
        }
        else
          File.WriteAllText(this.FullOwnerFileName, FilesStorage.CurrentApplicationName);
      }

      /// <summary>
      /// Возвращает полный путь к файлу в хранилище по имени файла
      /// </summary>
      /// <param name="filename">Имя файла без пути</param>
      /// <returns></returns>
      public string GetFullFileName(string filename)
      {
        string withoutExtension = Path.GetFileNameWithoutExtension(filename);
        string path3 = string.Empty;
        string path2;
        if (withoutExtension.Length > 1)
        {
          path2 = withoutExtension.Substring(0, 2);
          if (withoutExtension.Length > 3)
            path3 = withoutExtension.Substring(2, 2);
        }
        else
          path2 = string.Empty;
        string str = Path.Combine(this.RootName, path2, path3);
        if (!Directory.Exists(str))
          Directory.CreateDirectory(str);
        return Path.Combine(str, filename);
      }

      /// <summary>Определяет есть ли в каталоге файл</summary>
      /// <param name="filename">Имя файла</param>
      /// <returns></returns>
      public bool FileExists(string filename)
      {
        if (Path.GetDirectoryName(filename) == string.Empty)
          filename = this.GetFullFileName(filename);
        return File.Exists(filename);
      }

      /// <summary>Удаляет файл из каталога</summary>
      /// <param name="filename">Имя файла</param>
      public void DeleteFile(string filename)
      {
        if (!(filename != this.FullOwnerFileName))
          return;
        File.Delete(this.GetFullFileName(filename));
      }

      /// <summary>Удаляет файл из каталога</summary>
      /// <param name="filename">Имя файла с путем</param>
      public void DeleteFileByFullName(string fullfilename)
      {
        if (!(fullfilename != this.FullOwnerFileName))
          return;
        File.Delete(fullfilename);
      }

      public void DeleteDirectoryByFullPath(string fulldirpath)
      {
        if (!Directory.Exists(fulldirpath))
          return;
        Directory.Delete(fulldirpath, true);
      }

      /// <summary>
      /// Возвращает массив найденных файлов по всем подкаталогам
      /// </summary>
      /// <param name="pattern">Маска поиска</param>
      /// <returns>Массив найденных файлов (или пустой, если они не нашлись)</returns>
      public string[] GetFileNames(string pattern)
      {
        return Directory.GetFiles(this.RootName, pattern, SearchOption.AllDirectories);
      }

      /// <summary>
      /// Возвращает массив найденных подкаталов на все уровни вложенности
      /// </summary>
      /// <param name="pattern">Маска поиска</param>
      /// <returns>Массив найденных подкаталогов (или пустой, если они не нашлись)</returns>
      public string[] GetDirectoryNames(string pattern)
      {
        return Directory.GetDirectories(this.RootName, pattern, SearchOption.AllDirectories);
      }

      /// <summary>Очищает каталог от файлов и подкаталогов</summary>
      public void Clear()
      {
        string[] directoryNames = this.GetDirectoryNames("*");
        string[] fileNames = this.GetFileNames("*");
        if (fileNames.Length != 0)
        {
          for (int index = 0; index < fileNames.Length; ++index)
            this.DeleteFileByFullName(fileNames[index]);
        }
        if (directoryNames.Length == 0)
          return;
        for (int index = 0; index < directoryNames.Length; ++index)
          this.DeleteDirectoryByFullPath(directoryNames[index]);
      }
    }
}
