
// Type: Intermech.Interfaces.IBrowseFolder
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml


namespace Intermech.Interfaces
{
    /// <summary>
    /// Интерфейс для просмотра файловой системы сервера. Доступен на стороне клиента через
    /// интерфейс портфеля Intermech.Interfaces.Briefcase.IServerBriefcase.
    /// </summary>
    public interface IBrowseFolder
    {
      /// <summary>Список дисков, записанный в виде C:\\.</summary>
      string[] DrivesList { get; }

      /// <summary>
      /// Получить список папок, входящих в указанную родительскую папку.
      /// </summary>
      /// <param name="parentPath">Родительская папка</param>
      /// <returns>Cписок папок, входящих в указанную родительскую папку</returns>
      string[] GetFolders(string parentPath);

      /// <summary>Обновляет список дисков сервера</summary>
      void RefreshDrivesList();

      /// <summary>
      /// Возвращает количество свободного места на диске в байтах, на котором находится папка folderPath
      /// </summary>
      /// <param name="folderPath">Полное имя папки</param>
      /// <returns>Возвращает количество свободного места на диске (в байтах)</returns>
      long GetFreeSpace(string folderPath);

      /// <summary>Создать папку по указанному пути</summary>
      /// <param name="parentPath">Родительская папка</param>
      /// <param name="name">Имя новой папки</param>
      void CreateFolder(string parentPath, string name);
    }
}
