// Decompiled with JetBrains decompiler
// Type: Intermech.AutoSelection.Client.Converters_Editors.SelectionObjectTypeEditor
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
using System.Drawing.Design;
using System.Linq;
using System.Windows.Forms;

#nullable disable
namespace Intermech.AutoSelection.Client.Converters_Editors;

internal class SelectionObjectTypeEditor : UITypeEditor
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
    if (context == null || context.Instance == null)
      return value;
    if (value == null)
      return (object) null;
    if (!(value is AS_Guid asGuid1))
      return value;
    if (!(context.Instance is AutoSelectionNodeBase instance))
      return (object) asGuid1;
    Intermech.AutoSelection.Client.AutoSelectionRule.AutoSelectionRule rule = instance.Rule;
    if (rule == null)
    {
      int num = (int) MessageBox.Show(LocalizationHolder.rm.GetString(sc_690.ssp_automatch_691()), LocalizationHolder.rm.GetString("AutoSelection.Client_38"), MessageBoxButtons.OK, MessageBoxIcon.Hand);
      return (object) asGuid1;
    }
    if (rule.ObjectType == Guid.Empty)
    {
      int num = (int) MessageBox.Show(LocalizationHolder.rm.GetString(sc_690.ssp_automatch_692()), LocalizationHolder.rm.GetString("AutoSelection.Client_38"), MessageBoxButtons.OK, MessageBoxIcon.Hand);
      return (object) asGuid1;
    }
    AutoSelectionExecObjMode execObjMode = AutoSelectionExecObjMode.CurrentObject;
    if (instance is AutoSelectionNodeItemImbase selectionNodeItemImbase1)
      execObjMode = selectionNodeItemImbase1.ExecObjMode;
    int objectTypeId1 = MetaDataHelper.GetObjectTypeID(rule.ObjectType);
    int objectTypeId2 = MetaDataHelper.GetObjectTypeID(asGuid1.Value);
    List<int> list = new List<int>();
    Dictionary<int, List<IMSApplicability>> dictionary = new Dictionary<int, List<IMSApplicability>>();
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      foreach (int parentObjType in AutoSelectionUtils.ObjectType.GetParentObjTypes(objectTypeId1, execObjMode, sessionKeeper.Session))
      {
        List<IMSApplicability> typeApplicabilities = MetaDataHelper.GetObjectTypeApplicabilities(parentObjType);
        if (typeApplicabilities == null)
          throw new Exception("MetaDataHelper.GetObjectTypeApplicabilities() is null");
        foreach (IMSApplicability imsApplicability in typeApplicabilities)
        {
          List<int> childrenIdRecursive = MetaDataHelper.GetObjectTypeChildrenIDRecursive(imsApplicability.ChildObjectTypeID);
          list.AddRange((IEnumerable<int>) childrenIdRecursive);
          foreach (int key in childrenIdRecursive)
          {
            List<IMSApplicability> imsApplicabilityList;
            if (!dictionary.TryGetValue(key, out imsApplicabilityList))
            {
              imsApplicabilityList = new List<IMSApplicability>();
              dictionary.Add(key, imsApplicabilityList);
            }
            imsApplicabilityList.Add(imsApplicability);
          }
        }
      }
      GenericListHelper.MakeUnique<int>(list);
      int index = list.BinarySearch(objectTypeId2);
      if (objectTypeId2 > 0)
      {
        if (index >= 0)
        {
          list.RemoveAt(index);
          list.Insert(0, objectTypeId2);
        }
      }
    }
    SelectorForm form = new SelectorForm(typeof (ObjectTypesFolder), LocalizationHolder.rm.GetString("AutoSelection.Client_42"), typeof (ObjectTypeFolder), false);
    form.ClearSelection();
    form.SelectorFilter = (ISelectorFilter) new TypeSelectorFilter(list.ToArray(), true, true);
    form.NodeSelectorFilter = (INodeSelectorFilter) new NodeSelectorFilter();
    if (objectTypeId2 != -1)
    {
      form.ExpandLevelsOnLoad = -1;
      form.InitSelectionAsType(new ArrayList((ICollection) new int[1]
      {
        objectTypeId2
      }), new ArrayList((ICollection) new System.Type[1]
      {
        typeof (ObjectTypeFolder)
      }));
    }
    else
      form.ExpandLevelsOnLoad = 1;
    if (form.ShowTopDialog() != DialogResult.OK || form.IDList.Count == 0)
      return (object) asGuid1;
    int id = (int) form.IDList[0];
    if (id == objectTypeId2)
      return (object) asGuid1;
    IMSObjectType objectType = MetaDataHelper.GetObjectType(id);
    if (objectType == null)
      return (object) asGuid1;
    if (instance is AutoSelectionNodeItemCommon selectionNodeItemCommon)
    {
      IMSRelationType imsRelationType = (IMSRelationType) null;
      List<IMSApplicability> source;
      if (dictionary.TryGetValue(id, out source))
      {
        IMSApplicability imsApplicability = source.FirstOrDefault<IMSApplicability>((Func<IMSApplicability, bool>) (item => item.Options.HasFlag((Enum) ApplicabilityOptions.DefaultRelation)));
        if (imsApplicability == null && source.Count == 1)
          imsApplicability = source.First<IMSApplicability>();
        imsRelationType = imsApplicability != null ? MetaDataHelper.GetRelationType(imsApplicability.RelationTypeID) : (IMSRelationType) null;
      }
      AS_Guid asGuid2 = new AS_Guid(imsRelationType != null ? imsRelationType.Guid : Guid.Empty);
      selectionNodeItemCommon.RelTypeGuid = asGuid2;
    }
    if (instance is AutoSelectionNodeItemObject selectionNodeItemObject)
      selectionNodeItemObject.ItemObjectID = new AS_Long(0L);
    else if (instance is AutoSelectionNodeItemImbase selectionNodeItemImbase2)
    {
      selectionNodeItemImbase2.ImbaseCatalogID = new AS_Long(0L);
      selectionNodeItemImbase2.ImbaseObjectID = new AS_Long(0L);
    }
    return (object) new AS_Guid(objectType.Guid);
  }
}
