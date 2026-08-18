// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.VarsHelper
// Assembly: Intermech.Interfaces.Workflow, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2DC6A606-08B5-470B-B668-CAC7730D0728
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Workflow.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Workflow.xml

using Intermech.Interfaces;
using System;
using System.Data;

#nullable disable
namespace Intermech.Workflow;

public static class VarsHelper
{
  public static int CreateVariableType(
    IUserSession session,
    string name,
    VarType type,
    Guid guid = default (Guid),
    VarKind varKind = VarKind.User)
  {
    IDBAttributeTypeCollection attributeTypeCollection = session.GetAttributeTypeCollection(0);
    FType fieldTypeEx = MiscFunx.GetFieldTypeEx(type);
    AttributeTypeProperties attrProperties = new AttributeTypeProperties();
    AttributeTypePropertiesValidator validator = attributeTypeCollection.GetValidator(fieldTypeEx.FieldType);
    if (validator.SizeType.Length > 1)
      attrProperties.SizeType = validator.SizeType[1];
    attrProperties.FieldType = fieldTypeEx.FieldType;
    attrProperties.Name = name;
    attrProperties.LanguageID = validator.LanguageID;
    attrProperties.AreaID = validator.AreaID;
    attrProperties.ShortName = "";
    attrProperties.Alias = "";
    attrProperties.AttributeGuid = guid;
    if (type == VarType.DateTime)
      attrProperties.DefaultValue = (object) Consts.CurrentDateFunction;
    if (type == VarType.StringList)
      attrProperties.MultiValueMode = MultiValueModes.SingleValueFromList;
    else if (type == VarType.Text)
      attrProperties.MultiValueMode = MultiValueModes.MultiValues;
    if (!fieldTypeEx.LinkedObjectType.Equals(Guid.Empty))
    {
      IDBObjectType objectType = session.GetObjectType(fieldTypeEx.LinkedObjectType, false);
      if (objectType != null)
        attrProperties.SizeType = (long) objectType.ObjectType;
    }
    int attributeID = attributeTypeCollection.Create(attrProperties);
    if (varKind == VarKind.Global)
      session.GetAttributesGroup(wfConsts.GlobalVariablesGroupID).IncludeAttribute(attributeID);
    else
      session.GetAttributesGroup(wfConsts.WorkflowVarsGroupID).IncludeAttribute(attributeID);
    return attributeID;
  }

  public static int CreateVariableType(
    IUserSession session,
    string name,
    VarType type,
    Guid guid,
    StringList possibleValues,
    VarKind varKind = VarKind.User)
  {
    int variableType = VarsHelper.CreateVariableType(session, name, type, guid, varKind);
    if (variableType != 0 && possibleValues != null && possibleValues.Count > 0 && type != VarType.Text)
    {
      IDBAttributeType attributeType = session.GetAttributeType(variableType);
      using (DataTable possibleValues1 = attributeType.GetPossibleValues())
      {
        possibleValues1.Rows.Clear();
        for (int index = 0; index < possibleValues.Count; ++index)
          possibleValues1.Rows.Add((object) index, (object) possibleValues[index], (object) "");
        attributeType.SetPossibleValues(possibleValues1);
      }
    }
    return variableType;
  }
}
