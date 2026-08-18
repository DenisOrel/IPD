
// Type: Intermech.Interfaces.RedliningFiles
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.IO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;


namespace Intermech.Interfaces
{
    /// <summary>Класс для описания  настроек файлов замечаний</summary>
    [Serializable]
    public class RedliningFiles
    {
      /// <summary>наименование</summary>
      private string name = string.Empty;
      /// <summary>маска для поиска</summary>
      private string mask = string.Empty;
      /// <summary>папка для поиска файлов замечаний</summary>
      private string folder = string.Empty;
      /// <summary>макрос  для имени файла без расширения</summary>
      public static readonly string NAME = "%name%";
      /// <summary>макрос  для имени файла с раширением</summary>
      public static readonly string FULLNAME = "%fullname%";

      /// <summary>наименование</summary>
      public string Name => this.name;

      /// <summary>маска для поиска</summary>
      public string Mask => this.mask;

      /// <summary>папка для поиска файлов замечаний</summary>
      public string Folder => this.folder;

      /// <summary>
      /// 
      /// </summary>
      /// <param name="name">наименование</param>
      /// <param name="mask">маска для поиска </param>
      /// <param name="folder">папка для поиска файлов замечаний </param>
      public RedliningFiles(string name, string mask, string folder)
      {
        this.name = name;
        this.mask = mask;
        this.folder = folder;
      }

      /// <summary>Заполнить поля класса на основе строки из настроек</summary>
      /// <param name="settingsString">строка из настроек</param>
      public RedliningFiles(string settingsString)
      {
        string[] strArray = settingsString.Split('|');
        if (strArray.Length != 3)
          return;
        this.name = strArray[0];
        this.mask = strArray[1];
        this.folder = strArray[2];
      }

      /// <summary>строка для записи в настройки</summary>
      /// <returns></returns>
      public override string ToString() => $"{this.name}|{this.mask}|{this.folder}";

      /// <summary>проверить, является ли файл редлайнингом данного типа</summary>
      /// <param name="mainFilePath">относительный путь основного файла</param>
      /// <param name="verifiableFilePath">относительный путь проверяемого файла</param>
      /// <returns></returns>
      public bool CheckRedliningFile(string mainFilePath, string verifiableFilePath)
      {
        if (string.IsNullOrEmpty(mainFilePath) || string.IsNullOrEmpty(verifiableFilePath) || mainFilePath == verifiableFilePath || this.mask == string.Empty)
          return false;
        string withoutExtension = Path.GetFileNameWithoutExtension(mainFilePath);
        string fileName = Path.GetFileName(mainFilePath);
        return RegexHelper.ToRegex(Path.Combine(Path.GetDirectoryName(mainFilePath), Path.Combine(this.folder, this.mask.Replace(RedliningFiles.NAME, withoutExtension).Replace(RedliningFiles.FULLNAME, fileName))), true).IsMatch(verifiableFilePath, 0);
      }

      /// <summary>
      /// Получить нормализованное значение маски поиска.
      /// Удаляются дубликаты макросов,причём приоритет остаться в маске отдан макросу %NAME%.
      /// </summary>
      /// <param name="value">Нормализуемая маска</param>
      /// <returns>Нормализованная маска</returns>
      public string NormalizedMask(string value)
      {
        if (string.IsNullOrEmpty(value))
          return string.Empty;
        string lowerInvariant = value.ToLowerInvariant();
        int num1 = StringsHelper.ContainsCount(lowerInvariant, RedliningFiles.NAME.ToLowerInvariant());
        int num2 = StringsHelper.ContainsCount(lowerInvariant, RedliningFiles.FULLNAME.ToLowerInvariant());
        if (num1 < 1 || num2 < 1)
          return value;
        string str = value.Replace(RedliningFiles.FULLNAME.ToUpperInvariant(), string.Empty).Replace(RedliningFiles.FULLNAME.ToLowerInvariant(), string.Empty);
        int startIndex = str.ToLowerInvariant().IndexOf(RedliningFiles.NAME.ToLowerInvariant(), StringComparison.InvariantCultureIgnoreCase);
        return str.Replace(RedliningFiles.NAME.ToUpperInvariant(), string.Empty).Replace(RedliningFiles.NAME.ToLowerInvariant(), string.Empty).Insert(startIndex, RedliningFiles.NAME);
      }

      /// <summary>
      /// Отыскать для указанного файла (полный абсолютный путь) все файлы "Red Line",
      /// подходящие под текущие настройки
      /// </summary>
      /// <param name="mainFilePath">Полный абсолютный путь к основному файлу, для которого требуется найти файлы "Red Line"</param>
      /// <returns>Список файлов "Red Line" (может быть пустым)</returns>
      public List<string> FindRedliningFiles(string mainFilePath)
      {
        List<string> redliningFiles = new List<string>();
        if (string.IsNullOrEmpty(mainFilePath))
          return redliningFiles;
        FileInfo fileInfo = new FileInfo(mainFilePath);
        string str1 = Path.Combine(fileInfo.DirectoryName, this.folder);
        string withoutExtension = Path.GetFileNameWithoutExtension(fileInfo.FullName);
        string name = fileInfo.Name;
        string str2 = this.NormalizedMask(this.mask).Replace(RedliningFiles.NAME, withoutExtension).Replace(RedliningFiles.FULLNAME, name);
        DirectoryInfo directoryInfo = new DirectoryInfo(str1);
        if (!directoryInfo.Exists)
          return redliningFiles;
        FileInfo[] files = directoryInfo.GetFiles(str2, SearchOption.AllDirectories);
        if (files != null)
        {
          Regex regex = RegexHelper.ToRegex(Path.Combine(str1, str2), true);
          for (int index = 0; index < files.Length; ++index)
          {
            if (regex.IsMatch(files[index].FullName, 0))
              redliningFiles.Add(files[index].FullName);
          }
        }
        return redliningFiles;
      }
    }
}
