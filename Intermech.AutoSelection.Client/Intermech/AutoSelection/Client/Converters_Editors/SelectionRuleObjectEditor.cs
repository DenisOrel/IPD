// Decompiled with JetBrains decompiler
// Type: Intermech.AutoSelection.Client.Converters_Editors.SelectionRuleObjectEditor
// Assembly: Intermech.AutoSelection.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0149601B-82FF-44EF-927D-3DECB2C1F37D
// Assembly location: D:\IPS\Client\Intermech.AutoSelection.Client.dll

using ImSSP;
using Intermech.AutoSelection.Client.AutoSelectionNode;
using Intermech.DataFormats;
using Intermech.Interfaces;
using Intermech.Interfaces.AutoSelection;
using Intermech.Interfaces.Client;
using Intermech.Navigator;
using Intermech.Navigator.DBObjects;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Views;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using System.Windows.Forms;

#nullable disable
namespace Intermech.AutoSelection.Client.Converters_Editors;

internal class SelectionRuleObjectEditor : UITypeEditor
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
    if (!(value is AS_Guid) || context == null || context.Instance == null || !(context.Instance is AutoSelectionNodeBase instance))
      return value;
    Intermech.AutoSelection.Client.AutoSelectionRule.AutoSelectionRule rule = instance.Rule;
    if (rule == null)
    {
      int num = (int) MessageBox.Show(Intermech.AutoSelection.Client.LocalizationHolder.rm.GetString(sc_697.ssp_automatch_698()), Intermech.AutoSelection.Client.LocalizationHolder.rm.GetString("AutoSelection.Client_38"), MessageBoxButtons.OK, MessageBoxIcon.Hand);
      return value;
    }
    if (rule.ObjectType == Guid.Empty)
    {
      int num = (int) MessageBox.Show(Intermech.AutoSelection.Client.LocalizationHolder.rm.GetString(sc_697.ssp_automatch_699()), Intermech.AutoSelection.Client.LocalizationHolder.rm.GetString("AutoSelection.Client_38"), MessageBoxButtons.OK, MessageBoxIcon.Hand);
      return value;
    }
    List<long> excludeRuleList = new List<long>()
    {
      rule.RuleID
    };
    List<long> availabledRules;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      availabledRules = AutoSelectionUtils.Common.GetAvailabledRules(rule.ObjectType, excludeRuleList, sessionKeeper.Session);
      if (availabledRules != null)
      {
        if (availabledRules.Count != 0)
          goto label_13;
      }
      IDBObjectType objectType = sessionKeeper.Session.GetObjectType(rule.ObjectType);
      int num = (int) MessageBox.Show(string.Format(Intermech.AutoSelection.Client.LocalizationHolder.rm.GetString(sc_697.ssp_automatch_700()), (object) objectType.ObjectTypeName), "", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
      return value;
    }
label_13:
    Guid guid = new Guid("{C1538373-DF6D-4578-AEA8-05A7A79CA8E8}");
    IGuidMapper service1 = (IGuidMapper) ServicesManager.GetService(typeof (IGuidMapper));
    int num1 = service1.Register(guid);
    try
    {
      IFactory service2 = ServiceUtils.GetService<IFactory>((object) ApplicationServices.Container, true);
      service2.AddNodeType(num1, typeof (ObjectsListNode));
      service2.AddViewsProvider(num1, (IViewsProvider) new AdvObjectsPropertiesProvider());
      if (!(SelectionWindow.Select(Intermech.AutoSelection.Client.LocalizationHolder.rm.GetString("AutoSelection.Client_45"), (IDescriptor) new ListDescriptor(num1, AutoSelectionConsts.objTypeRuleID, Intermech.AutoSelection.Client.LocalizationHolder.rm.GetString("AutoSelection.Client_70"), (IList) availabledRules), typeof (IDBObjectID), SelectionOptions.SelectObjects | SelectionOptions.DisableMultiselect) is IDBObjectID[] dbObjectIdArray) || dbObjectIdArray.Length == 0)
        return value;
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        IDBObject dbObject = sessionKeeper.Session.GetObject(dbObjectIdArray[0].Value, false);
        return dbObject == null ? value : (object) new AS_Guid(((IDBGuid) dbObject).GUID);
      }
    }
    finally
    {
      service1.Unregister(num1);
    }
  }
}
