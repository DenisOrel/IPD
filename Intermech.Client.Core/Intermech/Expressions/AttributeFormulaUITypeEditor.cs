
// Type: Intermech.Expressions.AttributeFormulaUITypeEditor
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Holders;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Kernel.Search;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;


namespace Intermech.Expressions;

public class AttributeFormulaUITypeEditor : ExpressionUITypeEditor
{
  private IList variables;
  private int attributeId;
  private int id;
  private AttributableElements attributableElements;
  private bool validationEditorFlag;
  private EventsHolder.GetAttributeTypeDelegate getAttributeTypeDelegate;

  /// <summary>
  /// Флаг позволяет добавлять в редактор дополнительный параметр "Value", используемый в правилах валидации
  /// (в формулах "Value" отсутствует)
  /// </summary>
  public bool ValidationEditorFlag => this.validationEditorFlag;

  public AttributeFormulaUITypeEditor()
  {
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="getTypeIdDelegate">делегат для запроса типа атрибута, если атрибут еще не создан (виртуальное редактирование)</param>
  public AttributeFormulaUITypeEditor(
    EventsHolder.GetAttributeTypeDelegate getAttributeTypeDelegate)
  {
    this.getAttributeTypeDelegate = getAttributeTypeDelegate;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="aAttributeId"></param>
  /// <param name="aAttributableElements">редактируется формула для атрибута на типе объекта или связи</param>
  /// <param name="aId">id типа объекта-связи</param>
  public AttributeFormulaUITypeEditor(
    int aAttributeId,
    AttributableElements aAttributableElements,
    int aId)
    : this(aAttributeId, aAttributableElements, aId, false)
  {
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="aAttributeId"></param>
  /// <param name="aAttributableElements">редактируется формула для атрибута на типе объекта или связи</param>
  /// <param name="aId">id типа объекта-связи</param>
  /// <param name="aValidationEditorFlag">Флаг позволяет добавлять в редактор дополнительный параметр "Value", используемый в правилах валидации (в формулах "Value" отсутствует)</param>
  public AttributeFormulaUITypeEditor(
    int aAttributeId,
    AttributableElements aAttributableElements,
    int aId,
    bool aValidationEditorFlag)
  {
    this.attributeId = aAttributeId;
    this.attributableElements = aAttributableElements;
    this.id = aId;
    this.validationEditorFlag = aValidationEditorFlag;
  }

  public override object EditValue(
    ITypeDescriptorContext context,
    IServiceProvider provider,
    object value)
  {
    if (value == null)
      value = (object) string.Empty;
    if (value.GetType() != typeof (string))
      return value;
    string expression = value as string;
    if (this.variables == null)
      this.variables = this.CollectVariables();
    return ExpressionEditor.EditExpression(ref expression, (ICollection) this.variables, (CreateVariableEventHandler) null) ? (object) expression : value;
  }

  private IList CollectVariables()
  {
    List<Variable> al = new List<Variable>();
    IClientMetadataCache service = ServicesManager.GetService(typeof (IClientMetadataCache)) as IClientMetadataCache;
    FieldTypes resultType = FieldTypes.ftUnknown;
    if (this.attributeId != 0)
      resultType = service.GetAttributeType(this.attributeId).AttributeType;
    else if (this.getAttributeTypeDelegate != null)
      resultType = this.getAttributeTypeDelegate((object) this);
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBAttributeTypeCollection attributeTypeCollection = sessionKeeper.Session.GetAttributeTypeCollection(-1);
      if (this.attributableElements == AttributableElements.None)
      {
        if (attributeTypeCollection != null)
        {
          DataTable dt = attributeTypeCollection.Select("");
          this.CreateVarList(al, resultType, dt, false);
        }
      }
      else
      {
        if (attributeTypeCollection != null)
        {
          DataTable dt = attributeTypeCollection.Select("");
          this.CreateVarList(al, resultType, dt, true);
        }
        IDBAttribute4TypeCollection attribute4TypeCollection = (IDBAttribute4TypeCollection) null;
        if (this.attributableElements == AttributableElements.Object)
        {
          IDBObjectType objectType = sessionKeeper.Session.GetObjectType(this.id);
          if (objectType != null)
            attribute4TypeCollection = objectType.Attributes;
        }
        if (this.attributableElements == AttributableElements.Relation)
        {
          IDBRelationType relationType = sessionKeeper.Session.GetRelationType(this.id);
          if (relationType != null)
            attribute4TypeCollection = relationType.Attributes;
        }
        if (attribute4TypeCollection != null)
        {
          foreach (DataRow row in (InternalDataCollectionBase) attribute4TypeCollection.Select("").Rows)
          {
            int int32 = Convert.ToInt32(row["F_ATTRIBUTE_ID"]);
            if (int32 > 0 && this.attributeId != int32)
            {
              Variable attributeInfo = this.GetAttributeInfo(int32, resultType);
              if (attributeInfo != null)
                al.Add(attributeInfo);
            }
          }
        }
      }
      if (this.validationEditorFlag)
      {
        if (this.attributeId != 0)
        {
          Variable attributeInfo = this.GetAttributeInfo(this.attributeId, resultType);
          if (attributeInfo != null)
          {
            attributeInfo.ResetName("Value");
            al.Add(attributeInfo);
          }
        }
      }
    }
    return (IList) al;
  }

  private Variable GetAttributeInfo(int attId, FieldTypes resultType)
  {
    Variable attributeInfo = (Variable) null;
    IDBAttributeTypeInfo attributeType = (ServicesManager.GetService(typeof (IClientMetadataCache)) as IClientMetadataCache).GetAttributeType(attId);
    if (attributeType != null && this.Check4Usability(attributeType.AttributeType))
    {
      FieldTypes fieldType = attributeType.AttributeType;
      Type type;
      if (fieldType != FieldTypes.ftSystem)
      {
        type = Intermech.Navigator.DBObjects.Helper.ConvertType(fieldType);
      }
      else
      {
        if (!Enum.IsDefined(typeof (ObligatoryObjectAttributes), (object) attId))
          return (Variable) null;
        fieldType = ObligatoryObjectAttributesHelper.GetInFormulaDataType((ObligatoryObjectAttributes) attId, resultType);
        if (fieldType == FieldTypes.ftUnknown)
          return (Variable) null;
        type = Intermech.Navigator.DBObjects.Helper.ConvertType(fieldType);
      }
      if (type == (Type) null)
        type = typeof (string);
      attributeInfo = new Variable(attributeType.Name, type, fieldType);
    }
    return attributeInfo;
  }

  private void CreateVarList(
    List<Variable> al,
    FieldTypes resultType,
    DataTable dt,
    bool obligatoryOnly)
  {
    al.Capacity = dt.Rows.Count;
    DataView defaultView = dt.DefaultView;
    defaultView.Sort = "[F_NAME]";
    int count = defaultView.Count;
    for (int recordIndex = 0; recordIndex < count; ++recordIndex)
    {
      DataRow row = defaultView[recordIndex].Row;
      FieldTypes fieldTypes = (FieldTypes) Convert.ToInt32(row["F_ATTRIBUTE_TYPE"]);
      int int32 = Convert.ToInt32(row["F_ATTRIBUTE_ID"]);
      if ((!obligatoryOnly || int32 <= 0) && (int32 > 0 || ObligatoryObjectAttributesHelper.CanUseInFormula((ObligatoryObjectAttributes) int32)) && (this.attributeId == 0 || this.attributeId != int32) && this.Check4Usability(fieldTypes))
      {
        Type type;
        if (fieldTypes != FieldTypes.ftSystem)
          type = Intermech.Navigator.DBObjects.Helper.ConvertType(fieldTypes);
        else if (Enum.IsDefined(typeof (ObligatoryObjectAttributes), (object) int32))
        {
          fieldTypes = ObligatoryObjectAttributesHelper.GetInFormulaDataType((ObligatoryObjectAttributes) int32, resultType);
          if (fieldTypes != FieldTypes.ftUnknown)
            type = Intermech.Navigator.DBObjects.Helper.ConvertType(fieldTypes);
          else
            continue;
        }
        else
          continue;
        if (type == (Type) null)
          type = typeof (string);
        Variable variable = new Variable(row["F_NAME"].ToString(), type, fieldTypes);
        al.Add(variable);
      }
    }
  }

  private bool Check4Usability(FieldTypes ft)
  {
    return ft != FieldTypes.ftBlob && ft != FieldTypes.ftShortBlob && ft != FieldTypes.ftFile;
  }
}
