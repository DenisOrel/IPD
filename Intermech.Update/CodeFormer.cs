// Decompiled with JetBrains decompiler
// Type: Intermech.Update.CodeFormer
// Assembly: Intermech.Update, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 825FBF29-0112-4B23-8140-950E091D8F10
// Assembly location: D:\IPS\Client\Intermech.Update.dll

using Intermech.Expressions;
using Intermech.Interfaces;
using Intermech.Interfaces.LifeCycles;
using Intermech.Interfaces.MetadataUpdates;
using Intermech.Update.CodeFormers;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Xml;

#nullable disable
namespace Intermech.Update;

internal class CodeFormer : ICodeFormer
{
  protected Queue<string> temporaries = new Queue<string>();
  protected int CategoryID;

  public bool FailOnError { get; set; } = true;

  public List<string> Errors { get; set; } = new List<string>();

  public CodeFormer(int categoryID) => this.CategoryID = categoryID;

  protected XmlNode CreateNode(XmlDocument xmlDocument, Object4Script obj)
  {
    return this.CreateNode(xmlDocument, obj, (object) string.Empty);
  }

  protected XmlNode CreateNode(XmlDocument xmlDocument, Object4Script obj, object Tag)
  {
    XmlNode element = (XmlNode) xmlDocument.CreateElement("Object");
    if (obj.ID is Guid && obj.CategoryID != 5)
    {
      Guid id = (Guid) obj.ID;
      if (!SystemGUIDs.IsSystemGUID(id) && !SystemGUIDs.IsUsersGUID(id.ToString()))
      {
        string message = $"Нельзя добавлять в скрипт объект '{obj.Caption}' ({id}) категории {obj.CategoryID}, т.к. он не является системным";
        if (this.FailOnError)
          throw new Exception(message);
        this.Errors.Add(message);
        return (XmlNode) null;
      }
    }
    XmlAttribute attribute1 = xmlDocument.CreateAttribute("Guid");
    attribute1.Value = Convert.ToString(obj.ID);
    element.Attributes.Append(attribute1);
    XmlAttribute attribute2 = xmlDocument.CreateAttribute("CategoryID");
    attribute2.Value = Convert.ToString(obj.CategoryID);
    element.Attributes.Append(attribute2);
    XmlAttribute attribute3 = xmlDocument.CreateAttribute(nameof (Tag));
    attribute3.Value = Convert.ToString(Tag, (IFormatProvider) CultureInfo.CurrentCulture);
    element.Attributes.Append(attribute3);
    return element;
  }

  protected XmlNode CreateNewNode(XmlDocument xmlDocument, bool obligatory, string id)
  {
    XmlElement element = xmlDocument.CreateElement("Property");
    XmlAttribute attribute1 = xmlDocument.CreateAttribute("Obligatory");
    attribute1.Value = Convert.ToString(obligatory);
    element.Attributes.Append(attribute1);
    XmlAttribute attribute2 = xmlDocument.CreateAttribute("Id");
    attribute2.Value = id;
    element.Attributes.Append(attribute2);
    return (XmlNode) element;
  }

  protected XmlNode CreateProperty(
    XmlDocument xmlDocument,
    bool obligatory,
    string id,
    object value)
  {
    XmlNode newNode = this.CreateNewNode(xmlDocument, obligatory, id);
    switch (value)
    {
      case UpdateScriptAttributeValue[] _:
        for (int index = 0; index < (value as UpdateScriptAttributeValue[]).Length; ++index)
        {
          UpdateScriptAttributeValue scriptAttributeValue = (value as UpdateScriptAttributeValue[])[index];
          if (scriptAttributeValue != null)
          {
            XmlNode element = (XmlNode) xmlDocument.CreateElement("PropValue");
            XmlAttribute attribute1 = xmlDocument.CreateAttribute("Value");
            attribute1.Value = Convert.ToString(index);
            element.Attributes.Append(attribute1);
            XmlAttribute attribute2 = xmlDocument.CreateAttribute("IntegerValue");
            attribute2.Value = scriptAttributeValue.IntegerValue == long.MinValue ? string.Empty : Convert.ToString(scriptAttributeValue.IntegerValue);
            element.Attributes.Append(attribute2);
            XmlAttribute attribute3 = xmlDocument.CreateAttribute("DoubleValue");
            attribute3.Value = scriptAttributeValue.DoubleValue == double.MinValue ? string.Empty : Convert.ToString(scriptAttributeValue.DoubleValue, (IFormatProvider) CultureInfo.InvariantCulture);
            element.Attributes.Append(attribute3);
            XmlAttribute attribute4 = xmlDocument.CreateAttribute("StringValue");
            attribute4.Value = Convert.ToString(scriptAttributeValue.StringValue);
            element.Attributes.Append(attribute4);
            XmlAttribute attribute5 = xmlDocument.CreateAttribute("DateValue");
            attribute5.Value = scriptAttributeValue.DateTimeValue == DateTime.MinValue ? string.Empty : Convert.ToString(scriptAttributeValue.DateTimeValue, (IFormatProvider) CultureInfo.InvariantCulture);
            element.Attributes.Append(attribute5);
            XmlAttribute attribute6 = xmlDocument.CreateAttribute("TagValue");
            attribute6.Value = Convert.ToString(scriptAttributeValue.Tag, (IFormatProvider) CultureInfo.InvariantCulture);
            element.Attributes.Append(attribute6);
            newNode.AppendChild(element);
          }
        }
        break;
      case UpdateScriptAccessRight[] _:
        foreach (UpdateScriptAccessRight scriptAccessRight in value as UpdateScriptAccessRight[])
        {
          XmlNode element = (XmlNode) xmlDocument.CreateElement("PropValue");
          XmlAttribute attribute7 = xmlDocument.CreateAttribute("RightType");
          attribute7.Value = Convert.ToString(scriptAccessRight.RightType);
          element.Attributes.Append(attribute7);
          XmlAttribute attribute8 = xmlDocument.CreateAttribute("RightID");
          attribute8.Value = Convert.ToString(scriptAccessRight.RightID);
          element.Attributes.Append(attribute8);
          XmlAttribute attribute9 = xmlDocument.CreateAttribute("UserID");
          attribute9.Value = Convert.ToString((object) scriptAccessRight.UserID);
          element.Attributes.Append(attribute9);
          XmlAttribute attribute10 = xmlDocument.CreateAttribute("OwnerID");
          attribute10.Value = Convert.ToString((object) scriptAccessRight.OwnerID);
          element.Attributes.Append(attribute10);
          XmlAttribute attribute11 = xmlDocument.CreateAttribute("BeginDate");
          attribute11.Value = scriptAccessRight.BeginDate == DateTime.MinValue ? string.Empty : Convert.ToString(scriptAccessRight.BeginDate, (IFormatProvider) CultureInfo.InvariantCulture);
          element.Attributes.Append(attribute11);
          XmlAttribute attribute12 = xmlDocument.CreateAttribute("EndDate");
          attribute12.Value = scriptAccessRight.EndDate == DateTime.MinValue ? string.Empty : Convert.ToString(scriptAccessRight.EndDate, (IFormatProvider) CultureInfo.InvariantCulture);
          element.Attributes.Append(attribute12);
          newNode.AppendChild(element);
        }
        break;
      default:
        XmlNode element1 = (XmlNode) xmlDocument.CreateElement("PropValue");
        XmlAttribute attribute = xmlDocument.CreateAttribute("Value");
        attribute.Value = Convert.ToString(value, (IFormatProvider) CultureInfo.InvariantCulture);
        element1.Attributes.Append(attribute);
        newNode.AppendChild(element1);
        break;
    }
    return newNode;
  }

  protected List<ScriptNode> GetProperties4AttrOptions(AttributeOptions options)
  {
    List<ScriptNode> properties4AttrOptions = new List<ScriptNode>(UpdateScriptHelper.AllowedAttributeOptions.Length);
    foreach (AttributeOptions allowedAttributeOption in UpdateScriptHelper.AllowedAttributeOptions)
      properties4AttrOptions.Add((ScriptNode) new ObjectProperty4Script((object) $"{"F_OPTIONS"}{(int) allowedAttributeOption}", EnumDescConverter.GetEnumDescription((Enum) allowedAttributeOption), (object) (int) (options & allowedAttributeOption)));
    return properties4AttrOptions;
  }

  protected UpdateScriptAccessRight[] GetSecurity(IUserSession session, IDBSecurity seсurity)
  {
    return new List<UpdateScriptAccessRight>().ToArray();
  }

  protected void CheckGuid(string objectName, Guid guid)
  {
    string errorMessage;
    if (!CodeFormer.IsGuidAllowableForScript(objectName, guid, out errorMessage))
      throw new Exception(errorMessage);
  }

  public static bool IsGuidAllowableForScript(
    string objectName,
    Guid guid,
    out string errorMessage)
  {
    errorMessage = "";
    bool flag = true;
    if (!SystemGUIDs.IsSystemGUID(guid) && !SystemGUIDs.IsUsersGUID(guid.ToString()))
    {
      flag = false;
      errorMessage = $"{objectName} с глобальным идентификатором {guid} нельзя добавлять в скрипты автообновления, т.к. в скрипты можно помещать только системные или пользовательские объекты и метаданные.";
    }
    return flag;
  }

  protected object GetAttributeProperty(IUserSession session, int attributeID)
  {
    object attributeProperty = (object) null;
    if (attributeID > 0)
    {
      IDBAttributeType attributeType = session.GetAttributeType(attributeID, false);
      if (attributeType != null)
        attributeProperty = (object) (attributeType as IDBGuid).GUID;
    }
    return attributeProperty;
  }

  protected object GetRelationProperty(IUserSession session, int relationID)
  {
    object relationProperty = (object) null;
    if (relationID >= 0)
    {
      IDBRelationType relationType = session.GetRelationType(relationID, false);
      if (relationType != null)
        relationProperty = (object) (relationType as IDBGuid).GUID;
    }
    return relationProperty;
  }

  protected object GetObjectTypeProperty(IUserSession session, int objtypeID)
  {
    object objectTypeProperty = (object) null;
    if (objtypeID > 0)
    {
      IDBObjectType objectType = session.GetObjectType(objtypeID, false);
      if (objectType != null)
      {
        objectTypeProperty = (object) (objectType as IDBGuid).GUID;
        this.CheckGuid("Тип объектов", (Guid) objectTypeProperty);
      }
    }
    return objectTypeProperty;
  }

  protected object GetSchemaProperty(IUserSession session, int schemaID)
  {
    object schemaProperty = (object) null;
    if (schemaID > 0)
    {
      IDBLCSchema lcSchema = session.GetLCSchema(schemaID, false);
      if (lcSchema != null)
      {
        schemaProperty = (object) (lcSchema as IDBGuid).GUID;
        this.CheckGuid("Схему ЖЦ", (Guid) schemaProperty);
      }
    }
    return schemaProperty;
  }

  protected object GetLevelProperty(IUserSession session, int levelID)
  {
    object levelProperty = (object) null;
    if (levelID > 0)
    {
      IDBLifecycleLevelType lifecycleLevel = session.GetLifecycleLevel(levelID, false);
      if (lifecycleLevel != null)
      {
        levelProperty = (object) lifecycleLevel.GUID;
        this.CheckGuid("Уровень продвижения", (Guid) levelProperty);
      }
    }
    return levelProperty;
  }

  protected object GetLCStepProperty(IUserSession session, int stepID)
  {
    object lcStepProperty = (object) null;
    if (stepID > 0)
    {
      IDBLifecycleStep lifecycleStep = session.GetLifecycleStep(stepID, false);
      if (lifecycleStep != null)
      {
        lcStepProperty = (object) (lifecycleStep as IDBGuid).GUID;
        this.CheckGuid("Шаг ЖЦ", (Guid) lcStepProperty);
      }
    }
    return lcStepProperty;
  }

  protected object GetLanguageProperty(IUserSession session, string languageID)
  {
    object languageProperty = (object) null;
    if (languageID != string.Empty)
    {
      IDBLanguageType language = session.GetLanguage(languageID, false);
      if (language != null)
      {
        languageProperty = (object) language.GUID;
        this.CheckGuid("Языковой вариант", (Guid) languageProperty);
      }
    }
    return languageProperty;
  }

  protected string GetSubjectAreaProperty(IUserSession session, string areaID)
  {
    string str = areaID.Trim();
    if (str == string.Empty)
      return string.Empty;
    StringBuilder stringBuilder = new StringBuilder();
    for (int index = 0; index < areaID.Length; ++index)
    {
      char aSubjectAreaTypeID = str[index];
      IDBSubjectAreaType subjectAreaType = session.GetSubjectAreaType(aSubjectAreaTypeID);
      if (index > 0)
        stringBuilder.Append('|');
      Guid guid = (subjectAreaType as IDBGuid).GUID;
      this.CheckGuid("Предметную область", guid);
      stringBuilder.Append(guid.ToString());
    }
    return stringBuilder.ToString();
  }

  protected object GetDefaultValueProperty(
    IUserSession session,
    object defaultValue,
    FieldTypes fieldType)
  {
    object defaultValueProperty = (object) null;
    if (CompareValuesHelper.NormalizedValue(defaultValue) != null)
    {
      switch (fieldType)
      {
        case FieldTypes.ftDateTime:
          if (defaultValue.ToString().Equals(Consts.CurrentDateFunction))
          {
            defaultValueProperty = defaultValue.ToString().Equals(Consts.CurrentDateFunction) ? (object) "NOW" : defaultValue;
            break;
          }
          break;
        case FieldTypes.ftObjectLink:
          if (defaultValue.ToString().Equals(Consts.CurrentUserFunction))
          {
            defaultValueProperty = (object) "CURRENT";
            break;
          }
          IDBObject dbObject1 = session.GetObject(Convert.ToInt64(defaultValue), false);
          if (dbObject1 != null)
          {
            defaultValueProperty = (object) dbObject1.ObjectGUID;
            break;
          }
          break;
        case FieldTypes.ftMeasured:
          MeasuredValue measuredValue = MeasureHelper.ConvertToMeasuredValue(Convert.ToString(defaultValue, (IFormatProvider) CultureInfo.InvariantCulture));
          if (measuredValue != null)
          {
            IDBObject dbObject2 = session.GetObject(measuredValue.MeasureID, false);
            if (dbObject2 != null)
            {
              defaultValueProperty = (object) $"{Convert.ToString(measuredValue.Value, (IFormatProvider) CultureInfo.InvariantCulture)} {dbObject2.ObjectGUID}";
              break;
            }
            break;
          }
          break;
        default:
          defaultValueProperty = defaultValue;
          break;
      }
    }
    return defaultValueProperty;
  }

  protected string GetFormulaProperty(IUserSession session, string attrFormula)
  {
    string text = attrFormula;
    if (text != string.Empty)
    {
      ExpressionVariablesCollection variables = new Parser()
      {
        AutoDetectVariables = true,
        Validate = false
      }.Parse(text).Variables;
      for (int index = 0; index < variables.Count; ++index)
      {
        if (!(variables[index].Name.ToUpper() == "VALUE"))
        {
          IDBAttributeType attributeType = session.GetAttributeType(variables[index].Name, false);
          if (attributeType != null)
            text = text.Replace(variables[index].Name, $"{(attributeType as IDBGuid).GUID}");
        }
      }
    }
    return text;
  }

  public virtual XmlNode GenerateNode(
    IUserSession session,
    XmlDocument xmlDocument,
    Object4Script obj,
    string path4Files)
  {
    this.temporaries.Clear();
    object Tag = (object) null;
    if (obj.Tag != null)
      Tag = obj.Tag;
    XmlNode node1 = this.CreateNode(xmlDocument, obj, Tag);
    if (node1 == null)
      return (XmlNode) null;
    foreach (ScriptNode property in obj.Properties)
    {
      if (property is ObjectProperty4Script)
      {
        string id = Convert.ToString((property as ObjectProperty4Script).PropertyID);
        object obj1 = (property as ObjectProperty4Script).Value;
        node1.AppendChild(this.CreateProperty(xmlDocument, (property as ObjectProperty4Script).Obligatory, id, obj1));
      }
      else if (property is Object4Script)
      {
        XmlNode node2 = this.GenerateNode(session, xmlDocument, property as Object4Script, path4Files);
        if (node2 != null)
          node1.AppendChild(node2);
      }
    }
    return node1;
  }

  public virtual List<ScriptNode> GetProperties(IUserSession session, object dbObject)
  {
    return new List<ScriptNode>(0);
  }

  public IEnumerable<string> TempFilePaths
  {
    get
    {
      while (this.temporaries.Count > 0)
        yield return this.temporaries.Dequeue();
    }
  }
}
