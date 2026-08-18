// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.OldFormatIniFiles
// Assembly: Intermech.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2C1E6CF8-5894-477E-BC90-F77341E46DAF
// Assembly location: D:\IPS\Client\Intermech.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.AVS.xml

using Intermech.Interfaces;
using Intermech.Interfaces.AVS;
using System;
using System.Collections.Specialized;

#nullable disable
namespace Intermech.AVS;

/// <summary> Класс, описывающий ini файлы старого AVS </summary>
public class OldFormatIniFiles
{
  private static OldFormatIniFiles _oldSpecificationSettings = OldFormatIniFiles.Create(AVSDocument.ObjID_OldAVSSettingsSpecifications);
  private static OldFormatIniFiles _oldVedomostiSettings = OldFormatIniFiles.Create(AVSDocument.ObjID_OldAVSSettingsVedomosti);
  private string _defaultOldFormatStr = string.Empty;
  private long _oldFormatIniHolderID;
  private OldFormatIniFileDescriptor _defaultOldFormatIniFile;
  private OldFormatIniFileDescriptor[] _iniFileDescriptors;
  private HybridDictionary _extentionToIniFileDescriptorDictionary = new HybridDictionary();
  private HybridDictionary _filenameToIniFileDescriptorDictionary = new HybridDictionary();

  /// <summary> Конструктор </summary>
  /// <param name="oldFormatIniHolderID"> Идентификатор объекта, в котором хранятся настройки </param>
  private OldFormatIniFiles(long oldFormatIniHolderID, IDBObject iDBObject)
  {
    this._oldFormatIniHolderID = oldFormatIniHolderID;
    this.Init(iDBObject);
  }

  /// <summary> Создание объекта со старыми настройками </summary>
  /// <param name="oldFormatIniHolderID"> Идентификатор объекта, в котором хранятся настройки </param>
  /// <returns> созданый контейнер настроек, null если объкт с настройками отсутствует в БД </returns>
  private static OldFormatIniFiles Create(long oldFormatIniHolderID)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject iDBObject = sessionKeeper.Session.GetObject(oldFormatIniHolderID);
      if (iDBObject != null)
        return new OldFormatIniFiles(oldFormatIniHolderID, iDBObject);
    }
    return (OldFormatIniFiles) null;
  }

  /// <summary> Инициализирует список ini-файлов старой версии </summary>
  /// <param name="iDBObject">Интерфейс объекта типа "Контейнер атрибутов"</param>
  public void Init(IDBObject iDBObject)
  {
    IDBAttribute byId1 = iDBObject.Attributes.FindByID(AvsIDCache.Attr_OldAVSSettingsDefaultIniFile);
    if (byId1 != null)
      this._defaultOldFormatStr = byId1.AsString;
    IDBAttribute byId2 = iDBObject.Attributes.FindByID(AvsIDCache.Attr_OldAVSSettingsIniFiles);
    IDBAttribute byId3 = iDBObject.Attributes.FindByID(AvsIDCache.Attr_OldAVSSettingsIniFiles);
    IDBAttribute byId4 = iDBObject.Attributes.FindByID(AvsIDCache.Attr_OldAVSSettingsFileTypes);
    string empty1 = string.Empty;
    string empty2 = string.Empty;
    string empty3 = string.Empty;
    BlobInformation.EmptyBlobInformation();
    int valuesCount = byId3 != null ? byId3.ValuesCount : 0;
    if (byId2 == null)
      return;
    this._iniFileDescriptors = new OldFormatIniFileDescriptor[byId2.ValuesCount];
    for (int index = 0; index < byId2.ValuesCount; ++index)
    {
      string str = index < valuesCount ? Convert.ToString(byId4.Values[index]).ToUpper() : string.Empty;
      string description = byId2.Descriptions[index];
      byId2.Index = index;
      string fileName = (byId2 is IBlobReader blobReader ? blobReader.OpenBlob(-1) : BlobInformation.EmptyBlobInformation()).FileName;
      this._iniFileDescriptors[index] = new OldFormatIniFileDescriptor(this, index, description, str, fileName);
      if (description.Equals(this._defaultOldFormatStr))
        this._defaultOldFormatIniFile = this._iniFileDescriptors[index];
      this._extentionToIniFileDescriptorDictionary[(object) str] = (object) this._iniFileDescriptors[index];
      this._filenameToIniFileDescriptorDictionary[(object) fileName] = (object) this._iniFileDescriptors[index];
    }
  }

  /// <summary> Получение ini-файла для файлов с некоторым расширением </summary>
  public OldFormatIniFileDescriptor GetIniFileByExtention(string extention)
  {
    if (this._defaultOldFormatIniFile != null && this._defaultOldFormatIniFile.Extension.Equals(extention))
      return this._defaultOldFormatIniFile;
    if (this._iniFileDescriptors != null)
    {
      foreach (OldFormatIniFileDescriptor iniFileDescriptor in this._iniFileDescriptors)
      {
        if (iniFileDescriptor.Extension.Equals(extention))
          return iniFileDescriptor;
      }
    }
    return (OldFormatIniFileDescriptor) null;
  }

  /// <summary> Настройки старых спецификаций </summary>
  public static OldFormatIniFiles OldSpecificationSettings
  {
    get => OldFormatIniFiles._oldSpecificationSettings;
  }

  /// <summary> Настройки старых ведомостей </summary>
  public static OldFormatIniFiles OldVedomostiSettings => OldFormatIniFiles._oldVedomostiSettings;

  /// <summary> Расширение по-умолчанию </summary>
  public string DefaultOldFormatStr => this._defaultOldFormatStr;

  /// <summary> Массив дескрипторов ini-файлов со старыми настройками </summary>
  public OldFormatIniFileDescriptor[] IniFileDescriptors => this._iniFileDescriptors;

  /// <summary> Идентификатор объекта, в котором хранятся настройки </summary>
  public long OldFormatIniHolderID => this._oldFormatIniHolderID;

  /// <summary> Старый ini-файл, используемый по-уомлчанию </summary>
  public OldFormatIniFileDescriptor DefaultOldFormatIniFile => this._defaultOldFormatIniFile;

  /// <summary> Словарь, где ключом является расширение старого файла, а значением - ini файл, котором хранятся настройки для его открытия </summary>
  public HybridDictionary ExtentionToIniFileDescriptorDictionary
  {
    get => this._extentionToIniFileDescriptorDictionary;
  }

  /// <summary> Словарь, где ключом является имя ini файла, а значением - его дескриптор </summary>
  public HybridDictionary FilenameToIniFileDescriptorDictionary
  {
    get => this._filenameToIniFileDescriptorDictionary;
  }
}
