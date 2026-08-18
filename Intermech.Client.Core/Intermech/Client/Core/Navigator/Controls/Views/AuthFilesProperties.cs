
// Type: Intermech.Client.Core.Navigator.Controls.Views.AuthFilesProperties
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Checksums;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Localization;
using Intermech.PropertyEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;


namespace Intermech.Client.Core.Navigator.Controls.Views;

/// <summary>Общие настройки ЭЦП</summary>
internal class AuthFilesProperties
{
  /// <summary>Тип контрольной суммы для расчета</summary>
  private ChecksumAlgorithm _ChecksumAlgorithm;
  /// <summary>
  /// Разрешать расчет контрольных сумм по алгоритмам, отличающимся от алгоритма по умолчанию _ChecksumAlgorithm
  /// </summary>
  private bool _EnableChecksumAlternatives = true;
  /// <summary>
  /// Если включена опция, то аутентичные файлы копируются в новые версии объектов, а также в объекты, созданные по прототипу
  /// </summary>
  private bool _CopyAuthenticalFiles;
  /// <summary>Список типов аутентичных файлов</summary>
  private string _AuthFilesExtensions = string.Empty;
  /// <summary>
  /// Атрибут для формирования суффакса в именах аутентичных файлов при сохранении на диск
  /// </summary>
  private int _AuthFilesSuffixAttributeId;
  /// <summary>Имя файла с грифом секретности</summary>
  private string _FileNameWithSecrecyStamp = string.Empty;
  /// <summary>
  /// Если включено, то добавлять к имени аутентичного файла версию объекта "[версия]"
  /// </summary>
  private bool _AddObjectVersionToAuthFilenamesWhenSave;
  internal bool _inited;

  internal void ApplyUpdates()
  {
    if (!this.CheckExtensions())
      throw new Exception(LocalizationHolder.rm.GetString("InvalidAuthExtensions"));
    IDBConfigurations service = ServicesManager.GetService(typeof (IDBConfigurations)) as IDBConfigurations;
    service.WriteInteger("CLIENT", "AUTHFILES", "ALGORITHM", (long) this._ChecksumAlgorithm, 0L);
    service.WriteBool("CLIENT", "AUTHFILES", "ENABLEALTERNATIVES", this._EnableChecksumAlternatives, 0L);
    service.WriteBool("KERNEL", "COMMON", "COPY_AUTHENTICAL_FILES", this._CopyAuthenticalFiles, 0L);
    service.WriteString("CLIENT", "AUTHFILES", "AUTHFILESEXTENSIONS", this._AuthFilesExtensions.Trim(), 0L);
    service.WriteString("CLIENT", "AUTHFILES", "FILENAMEWITHSECRECYSTAMP", this._FileNameWithSecrecyStamp.Trim(), 0L);
    service.WriteBool("CLIENT", "AUTHFILES", "VERSION2AUTHFILENAME", this._AddObjectVersionToAuthFilenamesWhenSave, 0L);
    string empty = string.Empty;
    if (this._AuthFilesSuffixAttributeId != 0)
    {
      IMSAttributeType attributeType = MetaDataHelper.GetAttributeType(this._AuthFilesSuffixAttributeId);
      if (attributeType != null)
        empty = attributeType.AttributeGuid.ToString();
    }
    service.WriteString("CLIENT", "AUTHFILES", "SUFFIXATTRIBUTEGUID", empty, 0L);
  }

  private bool CheckExtensions()
  {
    List<string> stringList = DocumentTypeSettings.SplitAdditionalFileExts(this._AuthFilesExtensions);
    for (int index = 0; index < stringList.Count; ++index)
    {
      if (!DocumentTypeSettings.IsValidDocumentFileExt(stringList[index]))
        return false;
    }
    return true;
  }

  internal void LoadCurrentValues()
  {
    IDBConfigurations service = ServicesManager.GetService(typeof (IDBConfigurations)) as IDBConfigurations;
    this._ChecksumAlgorithm = (ChecksumAlgorithm) service.ReadInteger("CLIENT", "AUTHFILES", "ALGORITHM", 0L, DBConfigMode.GlobalOnly);
    this._EnableChecksumAlternatives = service.ReadBool("CLIENT", "AUTHFILES", "ENABLEALTERNATIVES", true, DBConfigMode.GlobalOnly);
    this._CopyAuthenticalFiles = service.ReadBool("KERNEL", "COMMON", "COPY_AUTHENTICAL_FILES", false, DBConfigMode.GlobalOnly);
    this._AuthFilesExtensions = service.ReadString("CLIENT", "AUTHFILES", "AUTHFILESEXTENSIONS", "", DBConfigMode.GlobalOnly);
    this._FileNameWithSecrecyStamp = service.ReadString("CLIENT", "AUTHFILES", "FILENAMEWITHSECRECYSTAMP", "<Гриф документа> <Наименование>.<Расширение файла>", DBConfigMode.GlobalOnly);
    this._AddObjectVersionToAuthFilenamesWhenSave = service.ReadBool("CLIENT", "AUTHFILES", "VERSION2AUTHFILENAME", false, DBConfigMode.GlobalOnly);
    string g = service.ReadString("CLIENT", "AUTHFILES", "SUFFIXATTRIBUTEGUID", AuthFilesHolder.DefaultParamAuthFilesSuffixAttributeGuid, DBConfigMode.GlobalOnly);
    this._AuthFilesSuffixAttributeId = 0;
    if (!(g != string.Empty))
      return;
    IMSAttributeType attributeType = MetaDataHelper.GetAttributeType(new Guid(g));
    if (attributeType == null)
      return;
    this._AuthFilesSuffixAttributeId = attributeType.AttributeID;
  }

  private void CheckInited()
  {
    if (this._inited)
      return;
    this.LoadCurrentValues();
    this._inited = true;
  }

  [CustomDescription("ChecksumAlgorithmDescription")]
  [CustomDisplayName("ChecksumAlgorithmCaption")]
  [DefaultValue(ChecksumAlgorithm.Crc32)]
  public ChecksumAlgorithm ChecksumAlgorithm
  {
    get
    {
      this.CheckInited();
      return this._ChecksumAlgorithm;
    }
    set => this._ChecksumAlgorithm = value;
  }

  [CustomDescription("EnableChecksumAlternativesDescription")]
  [CustomDisplayName("EnableChecksumAlternativesCaption")]
  [TypeConverter(typeof (YesNoBooleanConverter))]
  [DefaultValue(true)]
  public bool EnableChecksumAlternatives
  {
    get
    {
      this.CheckInited();
      return this._EnableChecksumAlternatives;
    }
    set => this._EnableChecksumAlternatives = value;
  }

  [CustomDescription("CopyAuthenticalFilesDescription")]
  [CustomDisplayName("CopyAuthenticalFilesCaption")]
  [TypeConverter(typeof (YesNoBooleanConverter))]
  [DefaultValue(true)]
  public bool CopyAuthenticalFiles
  {
    get
    {
      this.CheckInited();
      return this._CopyAuthenticalFiles;
    }
    set => this._CopyAuthenticalFiles = value;
  }

  [CustomDescription("AuthFilesExtensionsDescription")]
  [CustomDisplayName("AuthFilesExtensions")]
  [TypeConverter(typeof (string))]
  [DefaultValue("")]
  public string AuthFilesExtensions
  {
    get
    {
      this.CheckInited();
      return this._AuthFilesExtensions;
    }
    set => this._AuthFilesExtensions = value;
  }

  [CustomDescription("AuthFilesSuffixAttributeDescription")]
  [CustomDisplayName("AuthFilesSuffixAttribute")]
  [TypeConverter(typeof (AttributePropertyClass))]
  [Editor(typeof (AttributeEditor), typeof (UITypeEditor))]
  [DefaultValue(null)]
  public AttributePropertyClass AuthFilesSuffixAttribute
  {
    get
    {
      this.CheckInited();
      return this._AuthFilesSuffixAttributeId == 0 ? (AttributePropertyClass) null : new AttributePropertyClass(this._AuthFilesSuffixAttributeId);
    }
    set => this._AuthFilesSuffixAttributeId = value == null ? 0 : value.Attribute;
  }

  [CustomDescription("FileNameWithSecrecyStampDescription")]
  [CustomDisplayName("FileNameWithSecrecyStamp")]
  [TypeConverter(typeof (string))]
  [DefaultValue("")]
  public string FileNameWithSecrecyStamp
  {
    get
    {
      this.CheckInited();
      return this._FileNameWithSecrecyStamp;
    }
    set => this._FileNameWithSecrecyStamp = value;
  }

  [CustomDescription("AddObjectVersionToAuthFilenamesWhenSaveDescription")]
  [CustomDisplayName("AddObjectVersionToAuthFilenamesWhenSaveCaption")]
  [TypeConverter(typeof (YesNoBooleanConverter))]
  [DefaultValue(false)]
  public bool AddObjectVersionToAuthFilenamesWhenSave
  {
    get
    {
      this.CheckInited();
      return this._AddObjectVersionToAuthFilenamesWhenSave;
    }
    set => this._AddObjectVersionToAuthFilenamesWhenSave = value;
  }
}
