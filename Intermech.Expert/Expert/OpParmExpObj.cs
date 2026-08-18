// Decompiled with JetBrains decompiler
// Type: Intermech.Expert.OpParmExpObj
// Assembly: Intermech.Expert, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 23A627F6-725A-4579-B6EF-74B0D09DF1F0
// Assembly location: D:\IPS\Client\Intermech.Expert.dll
// XML documentation location: D:\IPS\Client\Intermech.Expert.xml

using Intermech.Interfaces;
using Intermech.Kernel.Search;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Xml;

#nullable disable
namespace Intermech.Expert;

/// <summary>Used to reference expert system objects</summary>
public class OpParmExpObj : OpParm
{
  public string objTypeGUID = "";
  public string objTypeText = "";
  public TempFormula cond;
  public TempFormula objCond;
  public string folderGUID = "";
  public string folderName = "";

  public OpParmExpObj()
  {
  }

  public OpParmExpObj(ref OpParmData opData)
    : base(ref opData)
  {
    this.SetData(ref opData);
  }

  public override void SetData(ref OpParmData opData)
  {
    this.objTypeGUID = opData.s1;
    this.objTypeText = opData.s2;
    this.folderGUID = opData.s3;
    this.cond = opData.tf.Count != 0 ? (TempFormula) opData.tf.Clone() : (TempFormula) null;
    if (opData.tf2.Count == 0)
      this.objCond = (TempFormula) null;
    else
      this.objCond = (TempFormula) opData.tf2.Clone();
  }

  public override void FillOpParmData(ref OpParmData opData)
  {
    opData.Clear();
    opData.s1 = this.objTypeGUID;
    opData.s2 = this.objTypeText;
    opData.s3 = this.folderGUID;
    opData.s4 = (string) null;
    if (this.cond != null)
      opData.tf = (TempFormula) this.cond.Clone();
    if (this.objCond == null)
      return;
    opData.tf2 = (TempFormula) this.objCond.Clone();
  }

  public override void WriteToXML(ref XmlTextWriter writer)
  {
    writer.WriteElementString("GUID", this.objTypeGUID);
    writer.WriteElementString("Text", this.objTypeText);
    writer.WriteElementString("FolderGuid", this.folderGUID);
    if (this.cond != null)
    {
      this.cond.FillObjectLinks();
      this.cond.WriteToXML(ref writer);
    }
    if (this.objCond == null)
      return;
    this.objCond.FillObjectLinks();
    this.objCond.WriteToXML(ref writer, "FormInnerCond");
  }

  public override void LoadFromXML(XmlNode node, int opTag)
  {
    if (!node.HasChildNodes)
      return;
    foreach (XmlNode childNode in node.ChildNodes)
    {
      if (childNode.Name == "GUID")
        this.objTypeGUID = childNode.InnerText;
      else if (childNode.Name == "Text")
        this.objTypeText = childNode.InnerText;
      else if (childNode.Name == "FolderGuid")
        this.folderGUID = childNode.InnerText;
      else if (childNode.NodeType == XmlNodeType.Element && childNode.Name == "Formula")
        this.cond = new TempFormula(childNode);
      else if (childNode.NodeType == XmlNodeType.Element && childNode.Name == "FormInnerCond")
        this.objCond = new TempFormula(childNode);
    }
  }

  public override bool FixIdents(List<IdGuid> attrs, List<IdGuid> objs)
  {
    bool flag = false;
    if (this.cond != null)
      flag = this.cond.FixIDs(attrs, objs);
    if (this.objCond != null)
      flag |= this.objCond.FixIDs(attrs, objs);
    return flag;
  }

  public override bool CollectGUIDs(List<IdGuid> attrs, List<IdGuid> objs)
  {
    bool flag = false;
    if (this.cond != null)
      flag = this.cond.CollectGUIDs(attrs, objs);
    if (this.objCond != null)
      flag |= this.objCond.CollectGUIDs(attrs, objs);
    return flag;
  }

  public override bool FixIdentsComplete(IUserSession ius)
  {
    bool flag = false;
    if (this.cond != null)
      flag = this.cond.FixIdentsComplete(ius);
    if (this.objCond != null)
      flag |= this.objCond.FixIdentsComplete(ius);
    return flag;
  }

  public override bool CollectExpObjInfo(ExpObjInfo eoi, IUserSession ius)
  {
    bool flag = eoi.AddObjType(this.objTypeGUID);
    if (this.cond != null)
      flag = flag && this.cond.CollectExpObjInfo(eoi, ius);
    if (this.objCond != null)
      flag = flag && this.objCond.CollectExpObjInfo(eoi, ius);
    return flag;
  }

  public static List<TempFormula> LoadFolderConds(IUserSession ius, string folderGUID)
  {
    List<TempFormula> tempFormulaList = (List<TempFormula>) null;
    if (folderGUID != "")
    {
      try
      {
        Guid objectGUID = Guid.Parse(folderGUID);
        if (ius.GetObject(objectGUID, false) is IExpertCond expertCond)
        {
          expertCond.Load();
          tempFormulaList = new List<TempFormula>();
        }
        IDBRelationCollection relationCollection = ius.GetRelationCollection(ExpertConsts.Consts.linkSimpleSortId);
        ColumnDescriptor[] columns = new ColumnDescriptor[1]
        {
          new ColumnDescriptor((object) ObligatoryObjectAttributes.F_OBJECT_ID)
        };
        while (expertCond != null)
        {
          tempFormulaList.Insert(0, expertCond.GetTempFormula());
          DBRecordSetParams paramSet = new DBRecordSetParams((ConditionStructure[]) null, columns);
          DataTable dataTable = relationCollection.EntersIn(paramSet, expertCond.ID);
          if (dataTable != null && dataTable.Rows.Count > 0)
          {
            long int64 = Convert.ToInt64(dataTable.Rows[0][0]);
            expertCond = ius.GetObject(int64, false) as IExpertCond;
            expertCond.Load();
          }
          else
            expertCond = (IExpertCond) null;
        }
      }
      catch
      {
        return tempFormulaList;
      }
    }
    return tempFormulaList;
  }

  public static string ComposeCondsString(List<TempFormula> formList)
  {
    if (formList == null)
      return "";
    StringBuilder stringBuilder = new StringBuilder();
    for (int index = 0; index < formList.Count; ++index)
      stringBuilder.AppendLine(formList[index].ToString());
    return stringBuilder.ToString();
  }

  /// <summary>
  /// Обработать событие слияния атрибутов - заменить один атрибут на другой.
  /// </summary>
  /// <param name="fromAttribute">Заменяемый атрибут</param>
  /// <param name="toAttribute">Заменяющий атрибут</param>
  /// <param name="session">Пользовательская сессия</param>
  /// <returns>true, если что-то изменилось при переводе</returns>
  public override bool PerformAttrCombine(
    IDBAttributeType fromAttribute,
    IDBAttributeType toAttribute,
    IUserSession session)
  {
    bool flag = false;
    if (this.cond != null)
      flag = this.cond.PerformAttrChange(fromAttribute, toAttribute);
    if (this.objCond != null)
      flag = this.objCond.PerformAttrChange(fromAttribute, toAttribute) | flag;
    return flag;
  }
}
