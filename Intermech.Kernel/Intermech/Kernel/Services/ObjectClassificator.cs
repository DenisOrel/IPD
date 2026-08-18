// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Services.ObjectClassificator
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.Interfaces.SelectionService;
using Intermech.Interfaces.Server;
using Intermech.Kernel.Search;
using Intermech.Kernel.Services.ClassifierService;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Globalization;


namespace Intermech.Kernel.Services;

public class ObjectClassificator : MarshalByRefObject, IObjectClassificator
{
  private readonly UserSession _userSession;
  private readonly List<long> _canceledObjects = new List<long>();
  private HybridDictionary _formulsClassificator;
  private readonly long _classifierID = -1;
  private bool _skipNonClassified;

  public bool SkipNonClassified
  {
    set => this._skipNonClassified = value;
  }

  public List<ClassifiedObjectInfo> ClassifiedObjects { get; } = new List<ClassifiedObjectInfo>();

  public long[] NonClassifiedObjects
  {
    get => this._canceledObjects.Count <= 0 ? (long[]) null : this._canceledObjects.ToArray();
  }

  public bool ObligatoryCalculated { get; }

  public ObjectClassificator(UserSession userSession, bool obligatoryCalculated, long classifierID)
  {
    this.ObligatoryCalculated = obligatoryCalculated;
    this._userSession = userSession;
    this._classifierID = classifierID;
    this._formulsClassificator = new HybridDictionary();
  }

  public AttributeValues[] GetClasificatorAttributes(long objectID)
  {
    IDBTimedEvents service = ServerServices.GetService(typeof (IDBTimedEvents)) as IDBTimedEvents;
    string sessionName1 = $"GetClasificatorAttributes_{Guid.NewGuid()}";
    string sessionName2 = sessionName1;
    IUserSession sessionTemporaryClone = service.GetSystemSessionTemporaryClone(sessionName2);
    sessionTemporaryClone.ShowPersonalObjects = true;
    sessionTemporaryClone.ShowDeletedObjects = true;
    try
    {
      IDBObject dbObject = sessionTemporaryClone.GetObject(this._classifierID);
      (dbObject as DBObject).CheckAccess(ActionType.IncludeInComposition);
      ClassifierProcessor.CheckEnableFolder((IUserSession) this._userSession, dbObject);
      this._formulsClassificator = this.CalcClassifierAttributes(sessionTemporaryClone, dbObject, new long[1]
      {
        objectID
      });
      if (this._formulsClassificator == null)
        return (AttributeValues[]) null;
      List<ClassifierFormula> fs = new List<ClassifierFormula>();
      IDictionaryEnumerator enumerator = this._formulsClassificator.GetEnumerator();
      while (enumerator.MoveNext())
      {
        foreach (string attributeValue in enumerator.Value as List<string>)
          fs.Add(new ClassifierFormula(attributeValue));
      }
      if (fs.Count == 0)
        return (AttributeValues[]) null;
      object obj = (sessionTemporaryClone as UserSession).DataManager.ExecuteScalar("SELECT F_OBJECT_TYPE FROM IMS_OBJECTS WHERE  F_OBJECT_ID = :obj_id", (sessionTemporaryClone as UserSession).DataManager.Parameter("obj_id", (object) objectID));
      DocumentTypeSettings settings = (this._userSession.GetCustomService(typeof (IDocumentTypeSettingsService)) as IDocumentTypeSettingsService).GetSettings(this._userSession.SessionGUID, Convert.ToInt32(obj));
      return this.GetAttributeValues(sessionTemporaryClone, fs, settings, dbObject);
    }
    finally
    {
      sessionTemporaryClone?.Logout(sessionName1);
    }
  }

  private AttributeValues[] GetAttributeValues(
    IUserSession session,
    List<ClassifierFormula> fs,
    DocumentTypeSettings settings,
    IDBObject classifier)
  {
    List<AttributeValues> attributes = new List<AttributeValues>(fs.Count);
    foreach (ClassifierFormula f in fs)
    {
      IDBAttributeType attributeType = session.GetAttributeType(f.AttributeGuid, false);
      if (attributeType != null)
      {
        object obj = this.GetValue(attributeType.AttributeType, f);
        if ((attributeType as IDBGuid).GUID.Equals(new Guid("cad0001f-306c-11d8-b4e9-00304f19f545")) && settings.DocumentTypeCodeInDesignation && settings.DocumentTypeCode != string.Empty)
          obj = (object) DocumentsHelper.AppendDocCode((IUserSession) this._userSession, Convert.ToString(obj), settings.DocumentTypeCode);
        AttributeValues attributeValues = new AttributeValues(attributeType.AttributeID, attributeType.AttributeType, attributeType.MultipleValued, attributeType.Computed)
        {
          AttributeGuid = attributeType.GUID,
          AttributeName = attributeType.Name
        };
        if (obj != null)
          attributeValues.Values = new object[1]{ obj };
        attributes.Add(attributeValues);
      }
    }
    this.SetProjectID(session, attributes, classifier);
    return attributes.Count > 0 ? attributes.ToArray() : (AttributeValues[]) null;
  }

  private void SetProjectID(
    IUserSession session,
    List<AttributeValues> attributes,
    IDBObject currentClassifier)
  {
    if (!session.Configurations.ReadBool("CLIENT", SelectionSettings.SectionID, SelectionSettings.SetProjectIDParamName, false, DBConfigMode.GlobalOnly))
      return;
    long classifierProjectId = this.GetClassifierProjectID(session, currentClassifier);
    if (classifierProjectId == 0L)
      return;
    attributes.Add(new AttributeValues(-14, (object) classifierProjectId));
  }

  private long GetClassifierProjectID(IUserSession session, IDBObject classifier)
  {
    if (classifier.ProjectID != 0L)
      return classifier.ProjectID;
    IDBObject parentClassifier = this.GetParentClassifier(session, classifier);
    return parentClassifier != null ? this.GetClassifierProjectID(session, parentClassifier) : 0L;
  }

  public ClassifiedError ClassifyObjects(long[] objectIDs)
  {
    IDBTimedEvents service = ServerServices.ServiceContainer.GetService<IDBTimedEvents>();
    IUserSession session = (IUserSession) null;
    string sessionName = $"ClassifyObjects_{Guid.NewGuid()}";
    try
    {
      session = service.GetSystemSessionTemporaryClone(sessionName);
      session.ShowPersonalObjects = true;
      session.ShowDeletedObjects = true;
      IDBObject dbObject1 = session.GetObject(this._classifierID);
      (dbObject1 as DBObject).CheckAccess(ActionType.IncludeInComposition);
      ClassifierProcessor.CheckEnableFolder((IUserSession) this._userSession, dbObject1);
      this._formulsClassificator = this.CalcClassifierAttributes(session, dbObject1, objectIDs);
      if (this._formulsClassificator == null)
        return new ClassifiedError(true);
      HybridDictionary hybridDictionary = new HybridDictionary();
      IDictionaryEnumerator enumerator = this._formulsClassificator.GetEnumerator();
      while (enumerator.MoveNext())
      {
        List<ClassifierFormula> classifierFormulaList = new List<ClassifierFormula>();
        foreach (string attributeValue in enumerator.Value as List<string>)
          classifierFormulaList.Add(new ClassifierFormula(attributeValue));
        hybridDictionary.Add(enumerator.Key, (object) classifierFormulaList);
      }
      long[] array = this.ClassifiedObjects.ConvertAll<long>((Converter<ClassifiedObjectInfo, long>) (info => info.ObjectID)).ToArray();
      if (array.Length != 0)
        objectIDs = DataSetProcessor.DifferenceArray(objectIDs, array);
      if (this._canceledObjects.Count > 0)
        objectIDs = DataSetProcessor.DifferenceArray(objectIDs, this._canceledObjects.ToArray());
      if (this._formulsClassificator != null && this._formulsClassificator.Count > 0)
      {
        this._userSession.DataManager.BeginTransaction();
        bool flag = false;
        try
        {
          for (int index = 0; index < objectIDs.Length; ++index)
          {
            IDBObject dbObject2 = this._userSession.GetObject(objectIDs[index], false);
            if (dbObject2 != null)
            {
              List<ClassifierFormula> fs = hybridDictionary[(object) dbObject2.ObjectType] as List<ClassifierFormula>;
              DocumentTypeSettings settings = (this._userSession.GetCustomService(typeof (IDocumentTypeSettingsService)) as IDocumentTypeSettingsService).GetSettings(this._userSession.SessionGUID, dbObject2.ObjectType);
              AttributeValues[] origAttributeValues = (AttributeValues[]) null;
              AttributeValues[] attributeValuesArray = (AttributeValues[]) null;
              try
              {
                attributeValuesArray = this.GetAttributeValues(session, fs, settings, dbObject1);
                if (attributeValuesArray != null)
                {
                  if (attributeValuesArray.Length != 0)
                  {
                    GetAttributeValuesModes modes = GetAttributeValuesModes.IncludeName | GetAttributeValuesModes.IncludeGuid | GetAttributeValuesModes.IncludeCaption;
                    origAttributeValues = dbObject2.GetAttributesValues(modes);
                    AttributeValues[] collection = dbObject2.SetAttributesValues(attributeValuesArray, false, true, true, modes);
                    if (collection != null)
                    {
                      if (collection.Length != 0)
                      {
                        List<AttributeValues> attributeValuesList = new List<AttributeValues>((IEnumerable<AttributeValues>) attributeValuesArray);
                        attributeValuesList.AddRange((IEnumerable<AttributeValues>) collection);
                        attributeValuesArray = attributeValuesList.ToArray();
                      }
                    }
                  }
                }
              }
              catch (Exception ex)
              {
                this._canceledObjects.Add(dbObject2.ObjectID);
                if (this._skipNonClassified)
                {
                  if (!this.ObligatoryCalculated)
                    goto label_28;
                }
                flag = true;
                return new ClassifiedError(dbObject2.ObjectID, dbObject2.Caption, ex);
              }
label_28:
              this.ClassifiedObjects.Add(new ClassifiedObjectInfo(dbObject2.ObjectID, dbObject2.ObjectType, attributeValuesArray, origAttributeValues));
            }
          }
        }
        finally
        {
          if (flag)
            this._userSession.DataManager.Rollback();
          else
            this._userSession.DataManager.Commit();
        }
      }
      return new ClassifiedError(true);
    }
    finally
    {
      session?.Logout(sessionName);
    }
  }

  private object GetValue(FieldTypes type, ClassifierFormula formula)
  {
    object obj = (object) null;
    string str = formula.GetValue();
    switch (type)
    {
      case FieldTypes.ftString:
        obj = (object) str;
        break;
      case FieldTypes.ftInteger:
        if (str != string.Empty)
        {
          obj = (object) Convert.ToInt32(str);
          break;
        }
        break;
      case FieldTypes.ftDouble:
        if (str != string.Empty)
        {
          obj = (object) Convert.ToDouble(str, (IFormatProvider) CultureInfo.InvariantCulture);
          break;
        }
        break;
      case FieldTypes.ftDateTime:
        if (str != string.Empty)
        {
          obj = (object) Convert.ToDateTime(str, (IFormatProvider) CultureInfo.InvariantCulture);
          break;
        }
        break;
      case FieldTypes.ftObjectLink:
        if (str != string.Empty)
        {
          obj = (object) Convert.ToInt64(str);
          break;
        }
        break;
      case FieldTypes.ftBoolean:
        if (str != string.Empty)
        {
          obj = (object) (str == "1");
          break;
        }
        break;
      case FieldTypes.ftAutoInc:
        if (str != string.Empty)
        {
          obj = (object) Convert.ToInt32(str);
          break;
        }
        break;
    }
    return obj;
  }

  private List<int> GetEnabledTypesForFolder(IDBObject folder)
  {
    List<int> enabledTypesForFolder = new List<int>();
    IDBAttribute attributeByGuid = folder.GetAttributeByGuid(new Guid("cadd9c3f-306c-11d8-b4e9-00304f19f545"), false);
    if (attributeByGuid == null || attributeByGuid.IsNull)
      return enabledTypesForFolder;
    for (int index = 0; index < attributeByGuid.ValuesCount; ++index)
    {
      attributeByGuid.Index = index;
      if (!attributeByGuid.IsNull)
      {
        int objectTypeId = MetaDataHelper.GetObjectTypeID(new Guid(attributeByGuid.AsString));
        if (objectTypeId != -1)
          enabledTypesForFolder.Add(objectTypeId);
      }
    }
    return enabledTypesForFolder;
  }

  private HybridDictionary CalcClassifierAttributes(
    IUserSession session,
    IDBObject classifier,
    long[] objectIDs)
  {
    HybridDictionary returned = new HybridDictionary();
    List<int> enabledTypesForFolder = this.GetEnabledTypesForFolder(classifier);
    foreach (long objectId in objectIDs)
    {
      QuickObjectInfo objectInfo = session.GetObjectInfo(objectId);
      if (!objectInfo.Empty && !returned.Contains((object) objectInfo.ObjectTypeID))
      {
        if (enabledTypesForFolder.Count > 0 && !enabledTypesForFolder.Contains(objectInfo.ObjectTypeID))
          throw new Exception($"Классификация в {classifier.NameInMessages} для объектов типа {MetaDataHelper.GetObjectTypeName(objectInfo.ObjectTypeID)} недоступна");
        returned.Add((object) objectInfo.ObjectTypeID, (object) new List<string>());
      }
    }
    IDBAttribute attributeByGuid1 = classifier.GetAttributeByGuid(new Guid("cad001d7-306c-11d8-b4e9-00304f19f545"));
    if (attributeByGuid1 == null)
      return (HybridDictionary) null;
    object[] values = attributeByGuid1.Values;
    long rootClassifier = ServerServices.ServiceContainer.GetService<ISelectionsService>().GetRootClassifier((object) session, classifier);
    IDBAttribute attributeByGuid2 = (rootClassifier != classifier.ObjectID ? (IDBAttributable) session.GetObject(rootClassifier) : (IDBAttributable) classifier).GetAttributeByGuid(new Guid("cad014c6-306c-11d8-b4e9-00304f19f545"), false);
    List<int> collection;
    if (attributeByGuid2 != null && attributeByGuid2.ValuesCount > 0)
    {
      collection = new List<int>(attributeByGuid2.ValuesCount + 1);
      for (int index = 0; index < attributeByGuid2.ValuesCount; ++index)
      {
        if (CompareValuesHelper.NormalizedValue(attributeByGuid2.Values[index]) != null && GuidHelper.IsGuid(Convert.ToString(attributeByGuid2.Values[index])))
        {
          int objectTypeId = MetaDataHelper.GetObjectTypeID((string) attributeByGuid2.Values[index]);
          if (objectTypeId != -1 && !collection.Contains(objectTypeId))
            collection.Add(objectTypeId);
        }
      }
    }
    else
      collection = new List<int>(1);
    for (int index = 0; index < values.Length; ++index)
    {
      string str = Convert.ToString(values[index]);
      if (!string.IsNullOrEmpty(str))
      {
        ClassifierFormula classifierFormula = new ClassifierFormula(str);
        if (classifierFormula.AttributeGuid != Guid.Empty)
        {
          IDBAttributeType attributeType = session.GetAttributeType(classifierFormula.AttributeGuid, false);
          if (attributeType != null)
          {
            if (attributeType.AttributeType != FieldTypes.ftString)
              this.AddAttributeToHybridDictionary(returned, str, -1);
            else if (classifierFormula.Formula.Length > 0 && classifierFormula.Formula[0] == '^')
            {
              if (StringFormula.NumberCounterPresent(str))
              {
                IDictionaryEnumerator enumerator = returned.GetEnumerator();
                while (enumerator.MoveNext())
                {
                  ClassifierFormula formula = new ClassifierFormula(str);
                  formula.Formula = formula.Formula.Remove(0, 1);
                  List<int> objTypes = new List<int>((IEnumerable<int>) collection);
                  if (objTypes.Count == 0)
                  {
                    int objType4Attribute = this.GetParentObjType4Attribute(session, (int) enumerator.Key, attributeType.AttributeID, out formula.Private);
                    if (!objTypes.Contains(objType4Attribute))
                      objTypes.Add(objType4Attribute);
                  }
                  ICalculator calculator = CalculatorFactory.GetCalculator(session, formula, string.Empty);
                  formula.Formula = calculator.Calculate(session, objTypes);
                  this.AddAttributeToHybridDictionary(returned, formula.ToString(), (int) enumerator.Key);
                }
              }
              else
              {
                classifierFormula.Formula = classifierFormula.Formula.Remove(0, 1);
                this.AddAttributeToHybridDictionary(returned, classifierFormula.ToString(), -1);
              }
            }
            else
            {
              string parentFormula = this.GetParentFormula(session, classifier, classifierFormula.AttributeGuid);
              IDictionaryEnumerator enumerator = returned.GetEnumerator();
              while (enumerator.MoveNext())
              {
                ClassifierFormula formula = new ClassifierFormula(str);
                List<int> objTypes = new List<int>((IEnumerable<int>) collection);
                if (objTypes.Count == 0)
                {
                  int objType4Attribute = this.GetParentObjType4Attribute(session, (int) enumerator.Key, attributeType.AttributeID, out formula.Private);
                  if (objType4Attribute != -1 && !objTypes.Contains(objType4Attribute))
                    objTypes.Add(objType4Attribute);
                }
                ICalculator calculator = CalculatorFactory.GetCalculator(session, formula, parentFormula);
                formula.Formula = calculator.Calculate(session, objTypes);
                this.AddAttributeToHybridDictionary(returned, formula.ToString(), (int) enumerator.Key);
              }
            }
          }
        }
      }
    }
    return returned;
  }

  private int GetParentObjType4Attribute(
    IUserSession session,
    int childTypeID,
    int attributeTypeID,
    out bool isPrivate)
  {
    isPrivate = false;
    IDBObjectType objectType = session.GetObjectType(childTypeID);
    IDBAttributeType4Object attributeById = (IDBAttributeType4Object) objectType.Attributes.GetAttributeByID(attributeTypeID, false);
    if (attributeById == null)
      return -1;
    if (attributeById.InheritMode == InheritModes.Inherited)
      return this.GetParentObjType4Attribute(session, objectType.ParentTypeID, attributeTypeID, out isPrivate);
    if (attributeById.InheritMode == InheritModes.Private)
      isPrivate = true;
    return childTypeID;
  }

  private void AddAttributeToHybridDictionary(
    HybridDictionary returned,
    string formula,
    int objType)
  {
    IDictionaryEnumerator enumerator = returned.GetEnumerator();
    if (objType == -1)
    {
      while (enumerator.MoveNext())
        (enumerator.Value as List<string>).Add(formula);
    }
    else
    {
      while (enumerator.MoveNext())
      {
        if ((int) enumerator.Key == objType)
          (enumerator.Value as List<string>).Add(formula);
      }
    }
  }

  private IDBObject GetParentClassifier(IUserSession session, IDBObject childClassifier)
  {
    IDBObject parentClassifier = (IDBObject) null;
    IDBAttribute attributeByGuid = childClassifier.GetAttributeByGuid(new Guid("cad0014d-306c-11d8-b4e9-00304f19f545"), false);
    string str = attributeByGuid != null ? Convert.ToString(attributeByGuid.Value) : string.Empty;
    if (str.Length > 2)
    {
      IDBObjectType objectType1 = session.GetObjectType(childClassifier.ObjectType);
      int objectType2 = childClassifier.ObjectType;
      if (objectType1.ParentTypeID >= 0)
        objectType2 = objectType1.ParentTypeID;
      string conditionValue = str.Remove(str.Length - 2, 2);
      DataTable dataTable1 = session.GetObjectCollection(objectType2).SelectWithLocalObjects(new DBRecordSetParams(new ConditionStructure[1]
      {
        new ConditionStructure(new Guid("cad0014d-306c-11d8-b4e9-00304f19f545"), RelationalOperators.Equal, (object) conditionValue, LogicalOperators.AND, 0)
      }, new ColumnDescriptor[1]
      {
        new ColumnDescriptor((object) -2, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Index, SortOrders.NONE, 0)
      }));
      if (dataTable1.Rows.Count == 1)
        parentClassifier = session.GetObject(Convert.ToInt64(dataTable1.Rows[0][0]));
      else if (dataTable1.Rows.Count > 1)
      {
        IDBRelationCollection relationCollection = session.GetRelationCollection(MetaDataHelper.GetRelationTypeID(new Guid("cad00151-306c-11d8-b4e9-00304f19f545")));
        if (childClassifier.ObjectType == MetaDataHelper.GetObjectTypeID("cad00150-306c-11d8-b4e9-00304f19f545"))
          relationCollection.ChildObjectTypes = (IList<int>) new int[2]
          {
            MetaDataHelper.GetObjectTypeID("cad00157-306c-11d8-b4e9-00304f19f545"),
            childClassifier.ObjectType
          };
        DataTable dataTable2 = relationCollection.EntersIn(new DBRecordSetParams((ConditionStructure[]) null, new object[1]
        {
          (object) -2
        }), childClassifier.ID);
        if (dataTable2.Rows.Count > 0)
          parentClassifier = session.GetObject(Convert.ToInt64(dataTable2.Rows[0][0]));
      }
    }
    return parentClassifier;
  }

  private string GetParentFormula(IUserSession session, IDBObject classifier, Guid attrGuid)
  {
    string parentFormula = string.Empty;
    IDBObject parentClassifier = this.GetParentClassifier(session, classifier);
    if (parentClassifier == null)
      return parentFormula;
    IDBAttribute attributeByGuid = parentClassifier.GetAttributeByGuid(new Guid("cad001d7-306c-11d8-b4e9-00304f19f545"), false);
    if (attributeByGuid != null && !attributeByGuid.IsNull && attributeByGuid.ValuesCount > 0)
    {
      for (int index = 0; index < attributeByGuid.ValuesCount; ++index)
      {
        if (CompareValuesHelper.NormalizedValue(attributeByGuid.Values[index]) != null)
        {
          ClassifierFormula classifierFormula = new ClassifierFormula(Convert.ToString(attributeByGuid.Values[index]));
          if (classifierFormula.AttributeGuid.Equals(attrGuid))
          {
            if (classifierFormula.Formula.Length > 0)
            {
              if (classifierFormula.Formula[0] != '^')
              {
                if (StringFormula.NumberCounterPresent(classifierFormula.Formula) && !classifierFormula.Formula.Contains(ClassifierFormula.CalculateSeparator))
                  classifierFormula.Formula += ClassifierFormula.CalculateSeparator;
                parentFormula = this.GetParentFormula(session, parentClassifier, attrGuid) + classifierFormula.Formula;
                break;
              }
              break;
            }
            parentFormula = this.GetParentFormula(session, parentClassifier, attrGuid);
            break;
          }
        }
      }
    }
    return parentFormula;
  }
}
