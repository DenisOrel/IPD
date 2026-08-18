// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.SectionDescriptor
// Assembly: Intermech.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2C1E6CF8-5894-477E-BC90-F77341E46DAF
// Assembly location: D:\IPS\Client\Intermech.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.AVS.xml

using Intermech.Interfaces;
using Intermech.Interfaces.AVS;
using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.AVS;

/// <summary> Дескриптор раздела конструкторской документации (спецификации, ведомости) </summary>
public class SectionDescriptor
{
  private long _id = -1;
  private string _caption = string.Empty;
  private SectionDescriptorsList _sectionDescriptorsList;
  private List<string> _oldIniFilesWithThisSectionList;

  /// <summary> Конструктор </summary>
  public SectionDescriptor(SectionDescriptorsList sectionDescriptorsList, long id, string caption)
  {
    this._sectionDescriptorsList = sectionDescriptorsList;
    this._id = id;
    this._caption = caption;
  }

  /// <summary> Создать новый дескриптор по идентификатору </summary>
  public SectionDescriptor CreateNew(SectionDescriptorsList sectionDescriptorsList, long id)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject dbObject = sessionKeeper.Session.GetObject(id);
      if (dbObject != null)
        return new SectionDescriptor(sectionDescriptorsList, id, dbObject.Caption);
    }
    return (SectionDescriptor) null;
  }

  /// <summary>
  /// Получить список ini-файлов с настройками старого AVS, где был описан данный раздел.
  /// В старом AVS каждый ini файл описывал настройки, применяемые к какому-то типу документа
  /// </summary>
  public List<OldFormatIniFileDescriptor> GetOldFormatSpecificationsIniFiles()
  {
    List<string> withThisSectionList = this.OldIniFilesWithThisSectionList;
    if (withThisSectionList != null)
    {
      List<OldFormatIniFileDescriptor> iniFileDescriptorList = new List<OldFormatIniFileDescriptor>(withThisSectionList.Count);
      foreach (string key in withThisSectionList)
      {
        if (OldFormatIniFiles.OldSpecificationSettings.FilenameToIniFileDescriptorDictionary.Contains((object) key))
          iniFileDescriptorList.Add((OldFormatIniFileDescriptor) OldFormatIniFiles.OldSpecificationSettings.FilenameToIniFileDescriptorDictionary[(object) key]);
      }
    }
    return (List<OldFormatIniFileDescriptor>) null;
  }

  /// <summary> Идентификатор раздела </summary>
  public long id => this._id;

  /// <summary> Заголовок раздела </summary>
  public string Caption => this._caption;

  /// <summary> Ссылка на список дескрипторов разделов </summary>
  public SectionDescriptorsList SectionDescriptorsList => this._sectionDescriptorsList;

  /// <summary>
  /// Список имён ini-файлов с настройками старого AVS, где был описан данный раздел.
  /// В старом AVS каждый ini файл описывал настройки, применяемые к какому-то типу документа
  /// </summary>
  public List<string> OldIniFilesWithThisSectionList
  {
    get
    {
      if (this._oldIniFilesWithThisSectionList == null)
      {
        using (SessionKeeper sessionKeeper = new SessionKeeper())
        {
          IDBObject dbObject = sessionKeeper.Session.GetObject(this._id);
          if (dbObject != null)
          {
            IDBAttribute attributeById = dbObject.GetAttributeByID(AvsIDCache.Attr_OldAvsIniFileNames);
            if (attributeById != null)
            {
              this._oldIniFilesWithThisSectionList = new List<string>(attributeById.ValuesCount);
              foreach (object obj in attributeById.Values)
                this._oldIniFilesWithThisSectionList.Add(Convert.ToString(obj));
            }
          }
        }
      }
      return this._oldIniFilesWithThisSectionList;
    }
  }
}
