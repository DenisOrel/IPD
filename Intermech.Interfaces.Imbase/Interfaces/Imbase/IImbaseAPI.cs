// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Imbase.IImbaseAPI
// Assembly: Intermech.Interfaces.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: A581041C-8E97-4E18-8E61-00F942ADD7DC
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Imbase.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Imbase.xml

using System.Runtime.InteropServices;

#nullable disable
namespace Intermech.Interfaces.Imbase;

/// <summary>Интерфейс эмулятора Imbase API.</summary>
[ComVisible(true)]
[Guid("D520DE3F-AFE6-45A2-80B5-9966FEFF5BBD")]
[InterfaceType(ComInterfaceType.InterfaceIsDual)]
public interface IImbaseAPI
{
  /// <summary>Возвращает версию API</summary>
  /// <returns>номер версии</returns>
  int GetVersion();

  int SelectFromTable(
    string catalogDef,
    string objectDef,
    string filter,
    string showFields,
    string sortOrder,
    int recordsCount,
    string comment,
    ref byte[] dataSetPacket);

  /// <summary>
  /// Создает в базе данных объект из записи таблицы и ссылки на таблицу
  /// </summary>
  /// <param name="recordId">Идентификатор записи таблицы</param>
  /// <param name="linkId">Идентификатор ярлыка таблицы</param>
  /// <param name="objectGuid">GUID созданного объекта</param>
  /// <returns>1 в случае успеха</returns>
  int CreateObject(long recordId, long linkId, ref string objectGuid);

  /// <summary>
  /// Создает в базе данных объект из записи таблицы и ссылки на таблицу
  /// </summary>
  /// <param name="tempKey">Временный ключ вида IKCCC.RRRR</param>
  /// <param name="objectGuid">Ключ созданного объекта в виде IG[GUID созданного объекта]</param>
  /// <returns>1 в случае успеха</returns>
  int CreateObjectFromTempKey(string tempKey, ref string objectGuid);

  /// <summary>
  /// Показывает окно с карточкой объекта/объектов для указанного GUID.
  /// </summary>
  /// <param name="guids">GUID одного или нескольких объектов, разделенных ','</param>
  int ShowPropertyWindow(string guids);

  /// <summary>Эмуляция функции MT@</summary>
  /// <param name="command">параметры командной строки</param>
  /// <param name="fileData">строчки данных( содержимое файла $$$tmp.$$$)</param>
  /// <returns></returns>
  int MaterialEntry(string command, string fileData, ref string result);

  int GetKeyInfo(
    string key,
    ref string tableRecord,
    ref string catalogRecord,
    ref string keysList);

  int ShowTables(
    int showFlags,
    string fieldNames,
    ref string tableRecord,
    ref string catalogRecord,
    ref string keysList);

  int SelectTable(
    long catalogId,
    string prompt,
    ref long tableId,
    ref string fullList,
    ref long recordKey);

  int SelectFolder(long catalogId, string prompt, ref long folderId, ref string fullList);

  /// <summary>
  /// Возвращает код состояния выполнения последней ф-ции API
  /// </summary>
  /// <returns></returns>
  int ErrorCode();

  /// <summary>
  /// Возвращает текст сообщения ошибки выполнения последней ф-ции API
  /// </summary>
  /// <returns></returns>
  string ErrorMessage();

  /// <summary>Поиск значения по индексам</summary>
  /// <param name="fieldName">Имя атрибута</param>
  /// <param name="fieldValue">значение для поиска</param>
  /// <param name="imbaseKey">временный ключ IMBASE или пустая строка, если ничего не найдено</param>
  /// <returns>0 - успех, !=0 - ошибка</returns>
  int FindItemByValue(string fieldName, string fieldValue, out string imbaseKey);

  IIPSImbaseFolder SelectCadmechTemplate(int bSelectTemplateFolder);

  IIPSImbaseCatalog FindCatalog(int catalogIndex);

  long AddBlob(string blobName, string blobData);

  string GetBlobData(long blobId);

  IIPSImbaseRawTable CreateTable(string origalTableName, string newTableName, int copyData);

  IIPSImbaseRawTable FindTableByName(string tableName);

  void UpdateBlob(long blobId, string data);

  IIPSImbaseFolder GetFolderById(long folderId);

  /// <summary>Создает таблицу в IPS</summary>
  /// <param name="tableInfo">Запись из IM_TABLES</param>
  /// <param name="structData">Записи из IM_FIELDS</param>
  /// <param name="tableData">Записи из таблицы</param>
  /// <param name="addInfo">Запись из Каталога</param>
  /// <returns></returns>
  int CreateTable(string tableInfo, string structData, string tableData, string addInfo);

  /// <summary>Получение базовых версий объекта по гуидам версии.</summary>
  /// <param name="objectGuids">массив GUIDов объектов</param>
  /// <param name="baseData"> массив данных о базовых версиях ( новый гуид и заголовок)</param>
  /// <returns></returns>
  int GetBaseVersionGuids(string[] objectGuids, out object[] baseData);
}
