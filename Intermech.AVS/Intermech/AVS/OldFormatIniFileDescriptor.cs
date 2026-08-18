// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.OldFormatIniFileDescriptor
// Assembly: Intermech.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2C1E6CF8-5894-477E-BC90-F77341E46DAF
// Assembly location: D:\IPS\Client\Intermech.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.AVS.xml

using Intermech.IniFiles;
using Intermech.Interfaces;
using Intermech.Interfaces.AVS;
using Intermech.Kernel.Search;
using System;
using System.Data;
using System.IO;
using System.Text;

#nullable disable
namespace Intermech.AVS;

/// <summary> Класс, описывающий ini файл старого AVS </summary>
public class OldFormatIniFileDescriptor
{
  private int _index = -1;
  private string _name = string.Empty;
  private string _extention = string.Empty;
  private OldFormatIniFiles _oldFormatIniFiles;
  private string _fileName = string.Empty;

  /// <summary> Конструктор </summary>
  internal OldFormatIniFileDescriptor(
    OldFormatIniFiles oldFormatIniFiles,
    int index,
    string name,
    string extention,
    string fileName)
  {
    this._oldFormatIniFiles = oldFormatIniFiles;
    this._index = index;
    this._name = name;
    this._extention = extention;
    this._fileName = fileName;
  }

  /// <summary> Получение содержимого ini-файла в виде строки </summary>
  public static string GetIniFileContent(string file)
  {
    return File.Exists(file) ? File.ReadAllText(file) : string.Empty;
  }

  /// <summary> Получение содержимого ini-файла в виде строки из БАЗЫ </summary>
  public string GetIniFileContent()
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject dbObject = sessionKeeper.Session.GetObject(this._oldFormatIniFiles.OldFormatIniHolderID);
      if (dbObject != null)
      {
        IDBAttribute attributeById = dbObject.GetAttributeByID(AvsIDCache.Attr_OldAVSSettingsIniFiles);
        if (attributeById != null)
        {
          using (MemoryStream aDestStream = new MemoryStream())
          {
            attributeById.Index = this._index;
            new BlobProcReader(attributeById, 0, (Stream) aDestStream, (BlobProcCustomClass.ProgressEventHandler) null, (BlobProcCustomClass.ThreadFinishEventHandler) null).ReadData(sessionKeeper.Session);
            aDestStream.Position = 0L;
            if (aDestStream.Length != 0L)
            {
              byte[] numArray = new byte[60000];
              int offset = 0;
              int num = Math.Min((int) aDestStream.Length, 60000);
              StringBuilder stringBuilder = new StringBuilder((int) aDestStream.Length);
              int count;
              while ((count = aDestStream.Read(numArray, offset, num)) > 0)
              {
                stringBuilder.Append(Encoding.Default.GetString(numArray, 0, count));
                offset += count;
                num = Math.Min(num, (int) (aDestStream.Length - (long) offset));
                if (num == 0)
                  break;
              }
              return stringBuilder.ToString();
            }
          }
        }
      }
    }
    return string.Empty;
  }

  /// <summary> Получить список разделов описаных в данном ini-файле </summary>
  public SectionDescriptorsList GetSectionsList()
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      int num = -1;
      if (this.OldFormatIniFiles.OldFormatIniHolderID == AVSDocument.ObjID_OldAVSSettingsSpecifications)
        num = AvsIDCache.ObjType_SpecificationSection;
      else if (this.OldFormatIniFiles.OldFormatIniHolderID == AVSDocument.ObjID_OldAVSSettingsVedomosti)
        num = AvsIDCache.ObjType_VedomostiSection;
      if (num != -1)
      {
        DataTable dataTable = sessionKeeper.Session.GetObjectCollection(AvsIDCache.ObjType_SpecificationSection).Select(new DBRecordSetParams(new ConditionStructure[0], new ColumnDescriptor[2]
        {
          new ColumnDescriptor((object) ObligatoryObjectAttributes.F_OBJECT_ID, AttributeSourceTypes.Object, ColumnContents.ID, ColumnNameMapping.ID, SortOrders.NONE, 0),
          new ColumnDescriptor((object) ObligatoryObjectAttributes.CAPTION, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.ID, SortOrders.NONE, 0)
        }));
        if (dataTable != null)
        {
          SectionDescriptorsList sectionsList = new SectionDescriptorsList(dataTable.Rows.Count);
          foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
          {
            if (!(row[0] is DBNull))
              sectionsList.AddNew((long) Convert.ToInt32(row[0]), Convert.ToString(row[1]));
          }
          return sectionsList;
        }
      }
    }
    return (SectionDescriptorsList) null;
  }

  /// <summary> Получение старого Ini файла с настройками в виде XML из БАЗЫ</summary>
  public InMemoryIniFile GetConfigFile()
  {
    string iniFileContent = this.GetIniFileContent();
    return !(iniFileContent != string.Empty) ? (InMemoryIniFile) null : new InMemoryIniFile(iniFileContent);
  }

  /// <summary> Получение старого Ini файла с настройками в виде XML из ФАЙЛА</summary>
  public static InMemoryIniFile GetConfigFile(string file)
  {
    string iniFileContent = OldFormatIniFileDescriptor.GetIniFileContent(file);
    return !(iniFileContent != string.Empty) ? (InMemoryIniFile) null : new InMemoryIniFile(iniFileContent);
  }

  /// <summary> Порядковый номер ini-файла </summary>
  public int Index => this._index;

  /// <summary> Название формата файла </summary>
  public string Name => this._name;

  /// <summary> Расширения документа старого AVS на который распостраняется данные настройки </summary>
  public string Extension => this._extention;

  /// <summary> Ссылка на контейнер атрибутов, в котором хранятся ini-файлы </summary>
  private OldFormatIniFiles OldFormatIniFiles => this._oldFormatIniFiles;
}
