// Decompiled with JetBrains decompiler
// Type: Intermech.Vault.Interfaces.XMLConsts
// Assembly: Intermech.Interfaces.Vault, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 00798F5C-F1D9-4688-8BA7-75723F33BDBF
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Vault.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Vault.xml

#nullable disable
namespace Intermech.Vault.Interfaces;

/// <summary>
/// класс для хранения имён узлов
/// в файлах индексов
/// </summary>
public static class XMLConsts
{
  /// <summary>Стандартный заголовок без указания кодировки</summary>
  public const string xmlHeader = "<?xml version='1.0' ?>";
  /// <summary>Стандартный заголовок с кодировкой UTF8</summary>
  public const string xmlHeaderUTF8 = "<?xml version='1.0' encoding='utf-8' ?>";
  /// <summary>
  /// Шаблон пустого документа с корректным корневым узлом "IPS.FSS.V1"
  /// </summary>
  public const string xmlEmptyDoc = "<?xml version='1.0' encoding='utf-8' ?>\n<VaultSettings />\n";
  public const string VAULT_SETTINGS = "VaultSettings";
  /// <summary>ключ для срока жизни файлов истории</summary>
  public const string HISTORY_LIFE = "history";
  /// <summary>ключ для срока жизни удалённых объектов</summary>
  public const string DELETED_LIFE = "trash";
  /// <summary>ключ для флага для полного логирования работы службы</summary>
  public const string LOGGIN_KEY = "full_logging";
  /// <summary>ключ для размера тома</summary>
  public const string VOLUME_MAX_SIZE = "folder_size";
  /// <summary>ключ для пути корневого католога для хранения файлов</summary>
  public const string PASSWORD = "password";
  /// <summary>ключ корневых каталогов</summary>
  public const string ROOT_FOLDERS_KEY = "root_folders";
  /// <summary>ключ для пути корневого католога</summary>
  public const string PATH_KEY = "path";
  /// <summary>
  /// ключ для guid'а шкафа,
  /// соответствующего данному каталогу
  /// </summary>
  public const string GUID_KEY = "guid";
  /// <summary>ключ для шкафа</summary>
  public const string STORAGE_KEY = "storage";
  /// <summary>
  /// максимальный размер шкафа
  /// (в процентах от свободного диска сервера)
  /// </summary>
  public const string ROOT_MAX_SIZE_KEY = "max_size";
  public const string SYNC_MODE_OFF = "syncmodeoff";
}
