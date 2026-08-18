
// Type: Intermech.Interfaces.WebPortal.IImportRulesService
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml


namespace Intermech.Interfaces.WebPortal
{
    /// <summary>Общие правила импорта из портала</summary>
    public interface IImportRulesService : ITransferSettingsService
    {
      /// <summary>
      /// Пользователь, которого назначать владельцем импортируемых из портала объектов в случае,
      /// если невозможно определить настоящего владельца.
      /// </summary>
      long DefaultObjectOwner { get; set; }

      /// <summary>
      /// Бизнес процесс для импортированной версии объекта для согласования взаимозаменяемости с имеющейся базовой версией этого объекта.
      /// </summary>
      long BaseVersionTemplate { get; set; }

      /// <summary>
      /// Папка для импорта ярлыков, для которых невозможно определить входимость в структуре Imbase
      /// </summary>
      long DefaultImbaseFolder { get; set; }

      /// <summary>
      /// Процесс об обновлении (создании) объектов в результате импорта
      /// </summary>
      long ImportCompleteTemplate { get; set; }

      /// <summary>Процесс об ошибке импорта</summary>
      long ImportErrorTemplate { get; set; }

      /// <summary>Централизованная НСИ</summary>
      bool CentralizedNSI { get; set; }

      /// <summary>Обновлять атрибут Архив</summary>
      bool RewriteArchive { get; set; }

      /// <summary>
      /// Переименовывать имя файла, если уже существует объект с таким именем файла
      /// </summary>
      bool RenameCoincidenceFileNames { get; set; }
    }
}
