// Decompiled with JetBrains decompiler
// Type: Intermech.AutoSelection.Client.Converters_Editors.SelectionRelationTypeEditor
// Assembly: Intermech.AutoSelection.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0149601B-82FF-44EF-927D-3DECB2C1F37D
// Assembly location: D:\IPS\Client\Intermech.AutoSelection.Client.dll

using ImSSP;
using Intermech.AutoSelection.Client.AutoSelectionNode;
using Intermech.Extensions.WinForms;
using Intermech.Interfaces;
using Intermech.PropertyEditors;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing.Design;
using System.Windows.Forms;

#nullable disable
namespace Intermech.AutoSelection.Client.Converters_Editors;

internal class SelectionRelationTypeEditor : UITypeEditor
{
  public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
  {
    return UITypeEditorEditStyle.Modal;
  }

  public override object EditValue(
    ITypeDescriptorContext context,
    System.IServiceProvider provider,
    object value)
  {
    if (!(value is AS_Guid asGuid))
      return value;
    if (context == null || context.Instance == null)
      return (object) asGuid;
    if (!(context.Instance is AutoSelectionNodeItemCommon instance))
      return (object) asGuid;
    if (instance.ObjTypeGuid.Value == Guid.Empty)
    {
      int num = (int) MessageBox.Show(LocalizationHolder.rm.GetString(sc_693.ssp_automatch_694()), LocalizationHolder.rm.GetString("AutoSelection.Client_38"), MessageBoxButtons.OK, MessageBoxIcon.Hand);
      return (object) asGuid;
    }
    Intermech.AutoSelection.Client.AutoSelectionRule.AutoSelectionRule rule = instance.Rule;
    if (rule == null)
    {
      int num = (int) MessageBox.Show(LocalizationHolder.rm.GetString(sc_693.ssp_automatch_695()), LocalizationHolder.rm.GetString("AutoSelection.Client_38"), MessageBoxButtons.OK, MessageBoxIcon.Hand);
      return (object) asGuid;
    }
    if (rule.ObjectType == Guid.Empty)
    {
      int num = (int) MessageBox.Show(LocalizationHolder.rm.GetString(sc_693.ssp_automatch_696()), LocalizationHolder.rm.GetString("AutoSelection.Client_38"), MessageBoxButtons.OK, MessageBoxIcon.Hand);
      return (object) asGuid;
    }
    AutoSelectionExecObjMode execObjMode = instance.ExecObjMode;
    List<int> list = new List<int>();
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IUserSession session = sessionKeeper.Session;
      int objectTypeId = MetaDataHelper.GetObjectTypeID(instance.ObjTypeGuid.Value);
      List<int> parentObjTypes = AutoSelectionUtils.ObjectType.GetParentObjTypes(MetaDataHelper.GetObjectTypeID(rule.ObjectType), execObjMode, session);
      IDBRelationsApplicabilityCollection applicabilityCollection = session.GetRelationsApplicabilityCollection();
      foreach (int inObjectType in parentObjTypes)
      {
        DataTable applicabilitiesList = applicabilityCollection.GetApplicabilitiesList(-1, objectTypeId, inObjectType);
        if (applicabilitiesList != null)
        {
          foreach (DataRow row in (InternalDataCollectionBase) applicabilitiesList.Rows)
            list.Add(Convert.ToInt32(row["F_RELATION_TYPE"]));
        }
      }
      if (list.Count == 0)
        return (object) asGuid;
      GenericListHelper.MakeUnique<int>(list);
    }
    int relationTypeId = MetaDataHelper.GetRelationTypeID(asGuid.Value);
    int index = list.IndexOf(relationTypeId);
    if (index != -1)
    {
      list.RemoveAt(index);
      list.Add(relationTypeId);
    }
    SelectorForm form = new SelectorForm(typeof (RelationTypesFolder), LocalizationHolder.rm.GetString("AutoSelection.Client_43"), typeof (RelationTypeFolder), false);
    if (relationTypeId != -1)
      form.InitSelectionAsType(new ArrayList((ICollection) new int[1]
      {
        relationTypeId
      }), new ArrayList((ICollection) new System.Type[1]
      {
        typeof (RelationTypesFolder)
      }));
    else
      form.ExpandLevelsOnLoad = 1;
    form.SelectorFilter = (ISelectorFilter) new TypeSelectorFilter(list.ToArray(), true, true);
    if (form.ShowTopDialog() != DialogResult.OK || form.IDList.Count == 0)
      return (object) asGuid;
    IMSRelationType relationType = MetaDataHelper.GetRelationType((int) form.IDList[0]);
    return relationType != null ? (object) new AS_Guid(relationType.Guid) : (object) asGuid;
  }
}
