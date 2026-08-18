// Decompiled with JetBrains decompiler
// Type: Intermech.GTC.Server.Processors.GtcItemObjectFactory
// Assembly: Intermech.GTC.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 9C6A94ED-A48D-4719-B6F5-18FD5E10EDC9
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.GTC.Server.dll

using Intermech.Expert;
using Intermech.GTC.Interfaces;
using Intermech.GTC.Server.BackgroundTask;
using Intermech.Interfaces;
using Intermech.Interfaces.Pdm;
using Intermech.Kernel.Search;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;

#nullable disable
namespace Intermech.GTC.Server.Processors;

internal class GtcItemObjectFactory
{
  private IUserSession _session;
  private BaseTaskForBackgroundTaskService _task;
  private IImportConfig _importConfig;
  private FileFolderPathHolder _fileFolderPathHolder;
  private IDBRelationCollection _simpleWithSortRelationCollection;
  private IFileNameGenerator _fileNameGenerator;
  private bool _hierarchy;
  private Dictionary<string, long> _existFolderGtcIdsCache;
  private Dictionary<string, long> _existItemObjectGtcIdsCache = new Dictionary<string, long>();
  private Dictionary<long, string> _bsuClassObjectsCache = new Dictionary<long, string>();

  public GtcItemObjectFactory(
    IUserSession session,
    BaseTaskForBackgroundTaskService task,
    IImportConfig importConfig,
    FileFolderPathHolder fileFolderPathHolder,
    Dictionary<string, long> existFolderGtcIdsCache,
    bool hierarchy)
  {
    this._session = session;
    this._task = task;
    this._importConfig = importConfig;
    this._fileFolderPathHolder = fileFolderPathHolder;
    this._existFolderGtcIdsCache = existFolderGtcIdsCache;
    this._hierarchy = hierarchy;
    this._simpleWithSortRelationCollection = this._session.GetRelationCollection(Intermech.GTC.Server.Const.SimpleWithSortRelationTypeId);
    this._fileNameGenerator = ServiceUtils.GetService<IFileNameGenerator>((object) this._session, true);
    this.FillBsuClassCache();
    this.FillExistItemObjectsCache();
  }

  private string GetFilePath(string fileName)
  {
    foreach (string searchPath in this._fileFolderPathHolder.GetSearchPaths())
    {
      string file = ZipExtractor.ExtractFile(Path.Combine(searchPath, fileName));
      if (File.Exists(file))
        return file;
    }
    return string.Empty;
  }

  private void FillBsuClassCache()
  {
    try
    {
      IDBObjectCollection objectCollection = this._session.GetObjectCollection(Intermech.GTC.Server.Const.ImbaseFolderObjectTypeId);
      DBRecordSetParams paramSet = new DBRecordSetParams(new ConditionStructure[2]
      {
        new ConditionStructure(Intermech.GTC.Server.Const.ClassifFolderKeyAttributeTypeId, RelationalOperators.StartString, (object) Intermech.GTC.Server.Const.ClassifFolderKey, LogicalOperators.AND, 0, false),
        new ConditionStructure(Intermech.GTC.Server.Const.BsuAttributeTypeId, RelationalOperators.NotEmpty, (object) null, LogicalOperators.NONE, 0, false)
      }, new object[2]
      {
        (object) -2,
        (object) Intermech.GTC.Server.Const.BsuAttributeTypeId
      });
      foreach (DataRow row in (InternalDataCollectionBase) objectCollection.Select(paramSet).Rows)
      {
        if (!this._bsuClassObjectsCache.ContainsKey(Convert.ToInt64(row[0])))
          this._bsuClassObjectsCache.Add(Convert.ToInt64(row[0]), row[1].ToString());
      }
    }
    catch (Exception ex)
    {
      Trace.WriteLine(ex.Message);
    }
  }

  private void FillExistItemObjectsCache()
  {
    IDBAttribute attributeById = this._session.GetObject(this._importConfig.CatalogId).GetAttributeByID(Intermech.GTC.Server.Const.ClassificatorKeyAttributeTypeId);
    if (attributeById == null || attributeById.AsString == string.Empty)
      return;
    ConditionStructure[] conditions = new ConditionStructure[1]
    {
      new ConditionStructure(Intermech.GTC.Server.Const.ClassificatorKeyAttributeTypeId, RelationalOperators.StartString, (object) attributeById.AsString, LogicalOperators.NONE, 0, true)
    };
    ColumnDescriptor[] columns = new ColumnDescriptor[2]
    {
      new ColumnDescriptor((object) -2, AttributeSourceTypes.Object, ColumnContents.ID, ColumnNameMapping.Default, SortOrders.ASC, 0),
      new ColumnDescriptor((object) Intermech.GTC.Server.Const.GtcVersionIdAttributeTypeId, AttributeSourceTypes.Object, ColumnContents.String, ColumnNameMapping.Default, SortOrders.ASC, 0)
    };
    foreach (DataRow row in (InternalDataCollectionBase) DataHelper.GetObjectData(MetaDataHelper.GetObjectTypeChildrenIDRecursive(Intermech.GTC.Server.Const.BaseItemObjectTypeId).ToArray(), this._session, (IEnumerable<ConditionStructure>) conditions, (IEnumerable<ColumnDescriptor>) columns).Rows)
    {
      long int64 = Convert.ToInt64(row[0]);
      string key = row[1].ToString();
      if (!this._existItemObjectGtcIdsCache.ContainsKey(key))
        this._existItemObjectGtcIdsCache.Add(key, int64);
    }
  }

  private long GetObjectIdByBsuCode(string bsuCode)
  {
    return !this._bsuClassObjectsCache.ContainsValue(bsuCode) ? 0L : this._bsuClassObjectsCache.FirstOrDefault<KeyValuePair<long, string>>((System.Func<KeyValuePair<long, string>, bool>) (x => x.Value == bsuCode)).Key;
  }

  private long GetDafaultObjectMeasureId(IDBAttribute attribute)
  {
    MeasureDescriptor defaultMeasure = MeasureHelper.GetDefaultMeasure(attribute.AttributeType.SizeType);
    return defaultMeasure == null ? 0L : defaultMeasure.MeasureID;
  }

  private void SetAttributeValues(IDBAttribute attribute, object[] aPropValues)
  {
    List<object> objectList = new List<object>();
    foreach (object aPropValue in aPropValues)
    {
      if (attribute.DataType == FieldTypes.ftString)
      {
        switch (aPropValue)
        {
          case string _:
          case double _:
            objectList.Add((object) aPropValue.ToString());
            continue;
          default:
            continue;
        }
      }
      else if (attribute.DataType == FieldTypes.ftDouble)
      {
        switch (aPropValue)
        {
          case double num:
            objectList.Add((object) num);
            continue;
          case string _:
            double result1;
            if (double.TryParse(aPropValue.ToString(), NumberStyles.Any, (IFormatProvider) CultureInfo.InvariantCulture, out result1))
            {
              objectList.Add((object) result1);
              continue;
            }
            continue;
          default:
            continue;
        }
      }
      else if (attribute.DataType == FieldTypes.ftInteger)
      {
        switch (aPropValue)
        {
          case double _:
          case string _:
            int result2;
            if (int.TryParse(aPropValue.ToString(), out result2))
            {
              objectList.Add((object) result2);
              continue;
            }
            continue;
          default:
            continue;
        }
      }
      else if (attribute.DataType == FieldTypes.ftMeasured)
      {
        long dafaultObjectMeasureId = this.GetDafaultObjectMeasureId(attribute);
        if (dafaultObjectMeasureId == 0L)
          throw new Exception(ServiceHolder.Rm.GetString("GTC_16"));
        switch (aPropValue)
        {
          case double aValue:
            objectList.Add((object) new MeasuredValue(aValue, dafaultObjectMeasureId));
            continue;
          case string _:
            double result3;
            if (double.TryParse(aPropValue.ToString(), NumberStyles.Any, (IFormatProvider) CultureInfo.InvariantCulture, out result3))
            {
              objectList.Add((object) new MeasuredValue(result3, dafaultObjectMeasureId));
              continue;
            }
            continue;
          default:
            continue;
        }
      }
    }
    if (objectList.Count <= 0)
      return;
    if (objectList.Count > 1 && (attribute.AttributeType.MultipleValued == MultiValueModes.SingleValue || attribute.AttributeType.MultipleValued == MultiValueModes.SingleValueFromList))
      attribute.Value = objectList[0];
    else
      attribute.Values = objectList.ToArray();
  }

  private int AddPlibAttribute(
    long objectId,
    string aAttBsuCode,
    long classFolderObjId,
    object[] aPropValues,
    out string errorMsg)
  {
    errorMsg = string.Empty;
    int num = 0;
    IMSAttributeType imsAttributeType = MetaDataHelper.GetAttributeTypesList().FirstOrDefault<IMSAttributeType>((System.Func<IMSAttributeType, bool>) (x => x.Alias == aAttBsuCode));
    if (imsAttributeType == null)
    {
      errorMsg = string.Format(ServiceHolder.Rm.GetString("GTC_18"), (object) aAttBsuCode);
      return num;
    }
    try
    {
      IDBObject dbObject = this._session.GetObject(objectId, true);
      IDBAttribute attribute = dbObject.Attributes.AddAttribute(imsAttributeType.AttributeID, false);
      this.SetAttributeValues(attribute, aPropValues);
      if (classFolderObjId != 0L)
      {
        string caption = this._session.GetObject(classFolderObjId, true).Caption;
        if (!string.IsNullOrEmpty(caption))
        {
          Dictionary<int, string> categoriesDictionary = AttributeCategoriesHelper.GetAttributeCategoriesDictionary(dbObject);
          if (!categoriesDictionary.ContainsKey(attribute.AttributeID))
            categoriesDictionary.Add(attribute.AttributeID, caption);
          AttributeCategoriesHelper.SetAttributeCategoriesDictionary(dbObject, categoriesDictionary);
        }
      }
      num = attribute.AttributeID;
    }
    catch (Exception ex)
    {
      errorMsg = string.Format(ServiceHolder.Rm.GetString("GTC_19"), (object) string.Join(Environment.NewLine, aPropValues), (object) imsAttributeType.Name, (object) ex.Message);
    }
    return num;
  }

  private int AddExternalLibAttribute(
    long objectId,
    string aAttributeName,
    string libraryType,
    string libraryName,
    object[] aPropValues,
    out string errorMsg)
  {
    errorMsg = string.Empty;
    int num = 0;
    IDBAttributeType attributeType = this._session.GetAttributeType(aAttributeName, false);
    if (attributeType == null)
    {
      errorMsg = string.Format(ServiceHolder.Rm.GetString("GTC_20"), (object) aAttributeName);
      return num;
    }
    try
    {
      IDBObject dbObject = this._session.GetObject(objectId, true);
      IDBAttribute attribute = dbObject.Attributes.AddAttribute(attributeType.AttributeID, false);
      this.SetAttributeValues(attribute, aPropValues);
      string str = libraryName != string.Empty ? libraryName : libraryType;
      if (!string.IsNullOrEmpty(str))
      {
        Dictionary<int, string> categoriesDictionary = AttributeCategoriesHelper.GetAttributeCategoriesDictionary(dbObject);
        if (!categoriesDictionary.ContainsKey(attribute.AttributeID))
          categoriesDictionary.Add(attribute.AttributeID, str);
        AttributeCategoriesHelper.SetAttributeCategoriesDictionary(dbObject, categoriesDictionary);
      }
      num = attribute.AttributeID;
    }
    catch (Exception ex)
    {
      errorMsg = string.Format(ServiceHolder.Rm.GetString("GTC_19"), (object) string.Join(Environment.NewLine, aPropValues), (object) attributeType.Name, (object) ex.Message);
    }
    return num;
  }

  public string CreateObject(
    string p21Path,
    string parentFolderGtcId,
    Tuple<string, string>[] files = null)
  {
    string str1 = string.Empty;
    StringBuilder stringBuilder = new StringBuilder();
    IDBObject dbObject = (IDBObject) null;
    try
    {
      GtcItemDataHolder gtcItemDataHolder = new GtcItemDataHolder(p21Path);
      long num1;
      if (this._existItemObjectGtcIdsCache.TryGetValue(gtcItemDataHolder.ItemVersionId, out num1))
        throw new ObjectAlreadyExists(num1, MetaDataHelper.GetAttributeTypeName(Intermech.GTC.Server.Const.GtcIdAttributeTypeId), this._session.GetObject(num1).NameInMessages, string.Empty);
      int index = gtcItemDataHolder.ObjectTypeId;
      if (index.Equals(-1))
        throw new Exception(ServiceHolder.Rm.GetString("GTC_23"));
      dbObject = this._session.GetObjectCollection(gtcItemDataHolder.ObjectTypeId).Create();
      IDBAttribute dbAttribute1 = dbObject != null ? dbObject.Attributes.AddAttribute(MetaDataHelper.GetAttributeTypeID("cad0001f-306c-11d8-b4e9-00304f19f545"), false) : throw new Exception(ServiceHolder.Rm.GetString("GTC_24"));
      if (dbAttribute1 != null)
        dbAttribute1.AsString = gtcItemDataHolder.Designation;
      IDBAttribute dbAttribute2 = dbObject.Attributes.AddAttribute(MetaDataHelper.GetAttributeTypeID("cad00020-306c-11d8-b4e9-00304f19f545"), false);
      if (dbAttribute2 != null)
        dbAttribute2.AsString = gtcItemDataHolder.Name;
      IDBAttribute dbAttribute3 = dbObject.Attributes.AddAttribute(Intermech.GTC.Server.Const.GtcVersionIdAttributeTypeId, false);
      if (dbAttribute3 != null)
        dbAttribute3.AsString = gtcItemDataHolder.ItemVersionId;
      dbObject.Attributes.AddAttribute(Intermech.GTC.Server.Const.GtcOrganizationAttributeTypeId, false).AsString = gtcItemDataHolder.Organization;
      string[] alternativeIdentification = gtcItemDataHolder.AlternativeIdentification;
      if (alternativeIdentification.Length != 0)
        dbObject.Attributes.AddAttribute(Intermech.GTC.Server.Const.AlternativeIdentiificationAttributeTypeId, false).Values = Array.ConvertAll<string, object>(alternativeIdentification, (Converter<string, object>) (x => (object) x));
      Dictionary<string, int> dictionary = new Dictionary<string, int>();
      Tuple<string, string, string[], object[]>[] plibProperties = gtcItemDataHolder.PlibProperties;
      for (index = 0; index < plibProperties.Length; ++index)
      {
        Tuple<string, string, string[], object[]> tuple = plibProperties[index];
        string errorMsg;
        int num2 = this.AddPlibAttribute(dbObject.ObjectID, tuple.Item1, this.GetObjectIdByBsuCode(tuple.Item2), tuple.Item4, out errorMsg);
        if (num2 != 0)
        {
          if (!dictionary.ContainsKey(tuple.Item1))
            dictionary.Add(tuple.Item1, num2);
        }
        else
          stringBuilder.AppendLine($"{errorMsg}");
      }
      if (!this._importConfig.OnlyPlibAttributes)
      {
        Tuple<string, string, string, string[], object[]>[] externalLibProperties = gtcItemDataHolder.ExternalLibProperties;
        for (index = 0; index < externalLibProperties.Length; ++index)
        {
          Tuple<string, string, string, string[], object[]> tuple = externalLibProperties[index];
          string errorMsg;
          int num3 = this.AddExternalLibAttribute(dbObject.ObjectID, tuple.Item1, tuple.Item2, tuple.Item3, ((IEnumerable<object>) tuple.Item5).ToArray<object>(), out errorMsg);
          if (num3 != 0)
          {
            if (!dictionary.ContainsKey(tuple.Item1))
              dictionary.Add(tuple.Item1, num3);
          }
          else
            stringBuilder.AppendLine($"{tuple.Item1}: {errorMsg}");
        }
      }
      if (gtcItemDataHolder.Coating != string.Empty)
      {
        IDBAttribute dbAttribute4 = dbObject.Attributes.AddAttribute(Intermech.GTC.Server.Const.CoatingTypeAttributeTypeId, false);
        if (dbAttribute4 != null)
          dbAttribute4.AsString = gtcItemDataHolder.Coating;
      }
      if (gtcItemDataHolder.Effectivity != string.Empty)
      {
        IDBAttribute dbAttribute5 = dbObject.Attributes.AddAttribute(Intermech.GTC.Server.Const.EffectivityTypeAttributeTypeId, false);
        if (dbAttribute5 != null)
          dbAttribute5.AsString = gtcItemDataHolder.Effectivity;
      }
      List<string> stringList = new List<string>();
      Tuple<string, string, string>[] propRelationShip = gtcItemDataHolder.PropRelationShip;
      for (index = 0; index < propRelationShip.Length; ++index)
      {
        Tuple<string, string, string> tuple = propRelationShip[index];
        int num4;
        int num5;
        if (dictionary.TryGetValue(tuple.Item1, out num4) && dictionary.TryGetValue(tuple.Item2, out num5))
          stringList.Add($"{num4}={num5}={Math.Abs(dbObject.ObjectID)}");
      }
      IDBAttribute dbAttribute6 = dbObject.Attributes.AddAttribute(Intermech.GTC.Server.Const.AttrsRelationshipTypeAttributeTypeId, false);
      if (dbAttribute6 != null && stringList.Count > 0)
        dbAttribute6.Values = Array.ConvertAll<string, object>(stringList.ToArray(), (Converter<string, object>) (x => (object) x));
      IDBAttribute aIDBAttribute = dbObject.Attributes.AddAttribute(MetaDataHelper.GetAttributeTypeID("cad0004b-306c-11d8-b4e9-00304f19f545"), false);
      if (aIDBAttribute != null)
      {
        Tuple<string, string>[] array1 = ((IEnumerable<Tuple<string, string>>) gtcItemDataHolder.Files).Select<Tuple<string, string>, Tuple<string, string>>((System.Func<Tuple<string, string>, Tuple<string, string>>) (x => new Tuple<string, string>(this.GetFilePath(x.Item1), x.Item2))).Where<Tuple<string, string>>((System.Func<Tuple<string, string>, bool>) (x => File.Exists(x.Item1))).ToArray<Tuple<string, string>>();
        if (files != null)
          array1 = ((IEnumerable<Tuple<string, string>>) array1).Union<Tuple<string, string>>((IEnumerable<Tuple<string, string>>) files).ToArray<Tuple<string, string>>();
        Tuple<string, string>[] array2 = ((IEnumerable<Tuple<string, string>>) array1).Distinct<Tuple<string, string>>((IEqualityComparer<Tuple<string, string>>) new FileInfoComparer()).ToArray<Tuple<string, string>>();
        int num6 = 0;
        Tuple<string, string>[] tupleArray = array2;
        for (index = 0; index < tupleArray.Length; ++index)
        {
          Tuple<string, string> tuple = tupleArray[index];
          string Extention = Path.GetExtension(tuple.Item1);
          if (Extention.Length > 1)
            Extention = Extention.Substring(1, Extention.Length - 1);
          if (num6 > 0)
            num6 = aIDBAttribute.AddValue((object) null);
          aIDBAttribute.Index = num6;
          try
          {
            using (FileStream aSourceStream = new FileStream(tuple.Item1, FileMode.Open, FileAccess.Read))
            {
              BlobInformation aBlobInformation = new BlobInformation(0L, 0L, DateTime.Now, this._fileNameGenerator.GenerateFileName((object) this._session, "", Extention), ArcMethods.ZLibPacked, tuple.Item2, FileTypes.ftNormal, this._session.UserID);
              new BlobProcWriter(aIDBAttribute, 0, aBlobInformation, (Stream) aSourceStream, (BlobProcCustomClass.ProgressEventHandler) null, (BlobProcCustomClass.ThreadFinishEventHandler) null).WriteData(this._session);
              ++num6;
            }
          }
          catch (Exception ex)
          {
            if (aIDBAttribute.Index > 0)
              aIDBAttribute.DeleteValue();
            else
              aIDBAttribute.Clear();
            stringBuilder.AppendLine(ex.Message);
          }
        }
      }
      if (this._hierarchy)
      {
        long num7;
        this._simpleWithSortRelationCollection.Create(this._existFolderGtcIdsCache.TryGetValue(parentFolderGtcId, out num7) ? num7 : this._importConfig.CatalogId, dbObject.ObjectID);
      }
      else
        this._simpleWithSortRelationCollection.Create(this._importConfig.CatalogId, dbObject.ObjectID);
      dbObject.CommitCreation(true);
      this._task.Result.CreatedObjects.Add(dbObject.ObjectID);
    }
    catch (ObjectAlreadyExists ex)
    {
      stringBuilder.AppendLine(ex.Message);
    }
    catch (Exception ex)
    {
      stringBuilder.AppendLine(ex.Message);
    }
    if (stringBuilder.Length > 0)
    {
      string str2;
      if (dbObject == null)
        str2 = $"{p21Path}:{Environment.NewLine}{stringBuilder}";
      else
        str2 = $"{p21Path}:{Environment.NewLine}{dbObject.NameInMessages}({dbObject.ObjectID}):{Environment.NewLine}{stringBuilder}";
      str1 = str2;
    }
    return str1;
  }
}
