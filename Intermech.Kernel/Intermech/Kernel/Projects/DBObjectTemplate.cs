// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Projects.DBObjectTemplate
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;


namespace Intermech.Kernel.Projects;

public class DBObjectTemplate(UserSession uSession, DataTable objectParams) : DBObject(uSession, objectParams)
{
  public override AttributeValues[] GetAttributesValues(GetAttributeValuesModes modes)
  {
    AttributeValues[] attributesValues = base.GetAttributesValues(GetAttributeValuesModes.IncludeGuid | GetAttributeValuesModes.CheckWriteAccess | modes);
    if (!this.IsCreationMode)
    {
      AttributeValues[] attributeValuesArray = attributesValues;
      int index = 0;
      while (index < attributeValuesArray.Length && !(attributeValuesArray[index].AttributeGuid == new Guid("cad001a0-306c-11d8-b4e9-00304f19f545")))
        ++index;
    }
    return attributesValues;
  }

  protected override void DoAfterSetAdditionalAttributeValue(IDBAttribute attribute)
  {
    IDBAttributeType attributeType = this.UserSession.GetAttributeType(new Guid("cad001a0-306c-11d8-b4e9-00304f19f545"));
    if (attributeType.AttributeID == attribute.AttributeID)
    {
      bool flag1 = false;
      List<int> intList = new List<int>();
      IDBObjectType objectType1 = this.UserSession.GetObjectType(this.ObjectType);
      IDBAttributeCollection attributes = this.Attributes;
      IDBObjectType objectType2 = this.UserSession.GetObjectType(new Guid(attribute.AsString));
      DataTable dataTable = objectType2.Attributes.Select((string) null, (object[]) null);
      foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
      {
        bool flag2 = true;
        int int32 = Convert.ToInt32(row["F_ATTRIBUTE_ID"]);
        RequiredModes required = objectType2.Attributes.GetAttributeByID(int32).Required;
        for (int AttrIndex = 0; AttrIndex < attributes.Count; ++AttrIndex)
        {
          IDBAttribute dbAttribute = attributes[AttrIndex];
          if (int32 == dbAttribute.AttributeID)
          {
            flag2 = false;
            break;
          }
        }
        if (flag2 && required != RequiredModes.Manual)
          this.Attributes.AddAttribute(int32, false);
      }
      for (int AttrIndex = 0; AttrIndex < attributes.Count; ++AttrIndex)
      {
        IDBAttribute dbAttribute = attributes[AttrIndex];
        bool flag3 = true;
        if (objectType1.Attributes.GetAttributeByID(dbAttribute.AttributeID) != null && objectType1.Attributes.GetAttributeByID(dbAttribute.AttributeID).Required != RequiredModes.Manual)
        {
          flag1 = false;
          break;
        }
        foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
        {
          int int32 = Convert.ToInt32(row["F_ATTRIBUTE_ID"]);
          if (dbAttribute.AttributeID == int32 || dbAttribute.AttributeID == attributeType.AttributeID)
          {
            flag3 = false;
            break;
          }
        }
        if (flag3)
          intList.Add(dbAttribute.AttributeID);
      }
      foreach (int num in intList)
      {
        if (this.GetAttributeByID(num) != null && MetaDataHelper.GetAttributeType(num).Formula != string.Empty)
          this.GetAttributeByID(num).Delete(0L);
      }
      foreach (int attributeID in intList)
      {
        if (this.GetAttributeByID(attributeID) != null)
          this.GetAttributeByID(attributeID).Delete(0L);
      }
    }
    base.DoAfterSetAdditionalAttributeValue(attribute);
  }
}
