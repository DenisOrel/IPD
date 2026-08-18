// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Briefcase.CheckAttributableType`2
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Briefcase;
using Intermech.Kernel.Search;
using System;
using System.Collections.Generic;
using System.Data;


namespace Intermech.Kernel.Briefcase;

internal abstract class CheckAttributableType<T, U> : CheckItem<T, U> where T : IDBAttributableType
{
  public CheckAttributableType(
    UserSession session,
    DataSet metaData,
    int category,
    U briefRow,
    CheckOptions options)
    : base(session, metaData, category, briefRow, options)
  {
  }

  protected abstract DataRow[] GetTypeAttributes();

  protected abstract int CheckAttribute(
    DataRow attrRow,
    IDictionary<string, bool> formulaAttributes);

  protected abstract void CheckAttributesCollection(List<int> presentAttributes);

  protected virtual List<string> GetObligatoryAttributes() => new List<string>(0);

  public void CheckAttributes()
  {
    DataRow[] typeAttributes = this.GetTypeAttributes();
    List<int> presentAttributes = new List<int>();
    Dictionary<string, bool> formulaAttributes = new Dictionary<string, bool>();
    foreach (DataRow row in (InternalDataCollectionBase) this.item.Attributes.Select(string.Empty).Rows)
    {
      IDBAttributeType attributeType = this.session.GetAttributeType(Convert.ToInt32(row["F_ATTRIBUTE_ID"]), false);
      if (attributeType != null)
        formulaAttributes.Add(attributeType.Name.ToUpper(), attributeType.ComputableAttribute);
    }
    foreach (string obligatoryAttribute in this.GetObligatoryAttributes())
      formulaAttributes.Add(obligatoryAttribute.ToUpper(), true);
    string empty1 = string.Empty;
    string empty2 = string.Empty;
    string empty3 = string.Empty;
    RelationalOperators[] enabledOperators = (RelationalOperators[]) null;
    bool computableAttribute = false;
    foreach (DataRow dataRow1 in typeAttributes)
    {
      DataRow dataRow2 = this.metaData.Tables["IMS_ATTRIBUTES"].Rows.Find((object) Convert.ToInt32(dataRow1["F_ATTRIBUTE_ID"]));
      if (dataRow2 != null)
      {
        List<FieldTypes> convertList = new List<FieldTypes>();
        AttributeCacheHelper.GetAttributeTypeValues((FieldTypes) Convert.ToInt32(dataRow2["F_ATTRIBUTE_TYPE"]), Convert.ToInt32(dataRow2["F_ATTRIBUTE_ID"]), ref empty1, ref empty3, ref convertList, ref enabledOperators, ref computableAttribute, ref empty2);
        string upper = dataRow2["F_NAME"].ToString().ToUpper();
        if (formulaAttributes.ContainsKey(upper))
          formulaAttributes[upper] = computableAttribute;
        else
          formulaAttributes.Add(upper, computableAttribute);
      }
    }
    foreach (DataRow attrRow in typeAttributes)
    {
      int num = this.CheckAttribute(attrRow, (IDictionary<string, bool>) formulaAttributes);
      if (num != 0)
        presentAttributes.Add(num);
    }
    this.CheckAttributesCollection(presentAttributes);
  }
}
