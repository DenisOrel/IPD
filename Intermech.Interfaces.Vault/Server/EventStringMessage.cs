// Decompiled with JetBrains decompiler
// Type: Intermech.Vault.Interfaces.Server.EventStringMessage
// Assembly: Intermech.Interfaces.Vault, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 00798F5C-F1D9-4688-8BA7-75723F33BDBF
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Vault.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Vault.xml

#nullable disable
namespace Intermech.Vault.Interfaces.Server;

/// <summary>класс с сообщениям для записи в лог</summary>
public static class EventStringMessage
{
  /// <summary>{0}: Создание шкафа с GUID={1}</summary>
  public static readonly string CREATE_STORAGE = LocalizationHolder.rm.GetString("Vault.Interfaces_4");
  /// <summary>Шкаф с GUID=\"{0}"\ уже существует</summary>
  public static readonly string STORAGE_ALREADY_EXISTS = LocalizationHolder.rm.GetString("Vault.Interfaces_5");
  /// <summary>"{0}:  Подключение к шкафу {1}..."</summary>
  public static readonly string LOG_IN_STORAGE = LocalizationHolder.rm.GetString("Vault.Interfaces_6");
  /// <summary>"{0}:  Отключение от шкафа {1}"</summary>
  public static readonly string LOGOUT_FROM_STORAGE = LocalizationHolder.rm.GetString("Vault.Interfaces_7");
  /// <summary>
  /// Пользователь {0} не подключен  к службе для работы со шкафом {1}
  /// </summary>
  public static readonly string USER_NOT_LOGGING = LocalizationHolder.rm.GetString("Vault.Interfaces_8");
  /// <summary>{0}: Получение информации о файловом шкафу с Guid={1}</summary>
  public static readonly string FILE_STORAGE_INFO = LocalizationHolder.rm.GetString("Vault.Interfaces_9");
  /// <summary>Администратор. Изменение размера тома - {0} б.</summary>
  public static readonly string VOLUME_SIZE_CHANGE = LocalizationHolder.rm.GetString("Vault.Interfaces_10");
  /// <summary>{0}: Папка {1} не найдена</summary>
  public static readonly string FOLDER_NOT_EXISTS = LocalizationHolder.rm.GetString("Vault.Interfaces_11");
  /// <summary>{0}: Переподключение к шкафу с GUID={1}</summary>
  public static readonly string RELOG_IN_STORAGE = LocalizationHolder.rm.GetString("Vault.Interfaces_12");
  /// <summary>
  /// Администратор. Изменение времени жизни удалённых файлов - {0} дн.
  /// </summary>
  public static readonly string DELETED_LIFETIME_CHANGE = LocalizationHolder.rm.GetString("Vault.Interfaces_13");
  /// <summary>
  /// Администратор. Изменение времени жизни файлов истории - {0} дн.
  /// </summary>
  public static readonly string HISTORY_LIFETIME_CHANGE = LocalizationHolder.rm.GetString("Vault.Interfaces_14");
  /// <summary>
  /// Администратор. Выключение полного логирования работы службы
  /// </summary>
  public static readonly string FULL_LOGGING_START = LocalizationHolder.rm.GetString("Vault.Interfaces_15");
  /// <summary>"{0}, {1}: Отключение клиента по тайм-ауту"</summary>
  public static readonly string TIME_OUT = LocalizationHolder.rm.GetString("Vault.Interfaces_16");
  /// <summary>"{0}, {1}: Завершение транзакции"</summary>
  public static readonly string COMMIT_TRANSACTION = LocalizationHolder.rm.GetString("Vault.Interfaces_17");
  /// <summary>"{0}, {1}: Откат транзакции"</summary>
  public static readonly string ROLLBACK_TRANSACTION = LocalizationHolder.rm.GetString("Vault.Interfaces_18");
  /// <summary>"{0}, {1}: Начало транзакции"</summary>
  public static readonly string START_TRANSACTION = LocalizationHolder.rm.GetString("Vault.Interfaces_19");
  /// <summary>"{0}, {1}: Удаление мусора"</summary>
  public static readonly string DELETE_TRASH = LocalizationHolder.rm.GetString("Vault.Interfaces_20");
  /// <summary>"{0}, {1}: Получение информации о файле с ID={2}"</summary>
  public static readonly string GET_FILE_INFO = LocalizationHolder.rm.GetString("Vault.Interfaces_21");
  /// <summary>"{0}, {1}: Изменение пароля"</summary>
  public static readonly string PASSWORD_CHANGE = LocalizationHolder.rm.GetString("Vault.Interfaces_22");
  /// <summary>"{0}: Удаление шкафа с Guid={1}"</summary>
  public static readonly string DELETE_STORAGE = LocalizationHolder.rm.GetString("Vault.Interfaces_23");
  /// <summary>{0}, {1}: Добавление файла с blobID={2}</summary>
  public static readonly string ADD_FILE = LocalizationHolder.rm.GetString("Vault.Interfaces_24");
  /// <summary>{0}, {1}: Чтение файла с BlobID={2}</summary>
  public static readonly string READ_FILE = LocalizationHolder.rm.GetString("Vault.Interfaces_25");
  /// <summary>"{0}, {2}: Удаление файла с blobID={1}"</summary>
  public static readonly string DELETE_FILE = LocalizationHolder.rm.GetString("Vault.Interfaces_26");
  /// <summary>
  /// "{0}, {2}: Изменение id объекта (связи), к которому принадлежит файла с blobID={1}";
  /// </summary>
  public static readonly string UPDATE_FILE = LocalizationHolder.rm.GetString("Vault.Interfaces_27");
  /// <summary>
  /// "Файл с blobID={0} нельзя удалить т.к. он в данный момент читается"
  /// </summary>
  public static readonly string CANNOT_DELETE_LOCKED_FILE = LocalizationHolder.rm.GetString("Vault.Interfaces_28");
  /// <summary>"Текущий том для папки {0} не найден"</summary>
  public static readonly string CANNOT_FIND_CURRENT_VALUE = LocalizationHolder.rm.GetString("Vault.Interfaces_29");
  /// <summary>"Размер файла превышает максимальный размер тома"</summary>
  public static readonly string BIG_FILE_SIZE = LocalizationHolder.rm.GetString("Vault.Interfaces_30");
  /// <summary>
  /// "Невозможно подключиться к шкафу {0}. Неверный пароль"
  /// </summary>
  public static readonly string CANNOT_LOGIN = LocalizationHolder.rm.GetString("Vault.Interfaces_31");
  /// <summary>"Папка корневого хранилища не найдена"</summary>
  public static readonly string CANNOT_FIND_ROOT_FOLDER = LocalizationHolder.rm.GetString("Vault.Interfaces_32");
  /// <summary>"Папка шкафа не найдена!"</summary>
  public static readonly string CANNOT_FIND_STORAGE_FOLDER = LocalizationHolder.rm.GetString("Vault.Interfaces_33");
  /// <summary>"Невозможно создать шкаф {0}. Неверный пароль"</summary>
  public static readonly string INVALID_PASSWORD = LocalizationHolder.rm.GetString("Vault.Interfaces_34");
  /// <summary>{0}, {1} Ошибка при создании шкафа: {2}"</summary>
  public static readonly string CREATE_STORAGE_ERROR = LocalizationHolder.rm.GetString("Vault.Interfaces_35");
  /// <summary>"{0} {1}: Файл с BlobID={2} не найден"</summary>
  public static readonly string CANNOT_FIND_FILE = LocalizationHolder.rm.GetString("Vault.Interfaces_36");
  /// <summary>{0}, {1}: Ошибка чтения файла с blobID={2}. {3}</summary>
  public static readonly string FILE_READ_ERROR = LocalizationHolder.rm.GetString("Vault.Interfaces_37");
  /// <summary>
  /// "Нельзя изменять положение корневого хранилища, т.к. к нему подключены пользователи"
  /// </summary>
  public static readonly string CANNOT_CHANGE_ROOT_POSITION = LocalizationHolder.rm.GetString("Vault.Interfaces_38");
  /// <summary>"Изменение корневого католога {0}"</summary>
  public static readonly string ROOT_FOLDER_CHANGE = LocalizationHolder.rm.GetString("Vault.Interfaces_39");
  /// <summary>
  /// "{0}, {1}: Файл истории {3} для версии объекта objectID={2} не найден"
  /// </summary>
  public static readonly string CANNOT_FIND_HISTORY_FILE = LocalizationHolder.rm.GetString("Vault.Interfaces_40");
}
