// Decompiled with JetBrains decompiler
// Type: Intermech.XmlExchange.ConfigEditor.LcStepSelected
// Assembly: Intermech.XmlExchange.ConfigEditor, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D148B79A-64FF-4CB8-A129-56A9018E56E2
// Assembly location: D:\IPS\Client\Intermech.XmlExchange.ConfigEditor.dll

using Intermech.Interfaces;
using Intermech.XmlExchange.ConfigEditor.PropertiesDescription.ImportItem;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing.Design;
using System.Windows.Forms;
using System.Windows.Forms.Design;

#nullable disable
namespace Intermech.XmlExchange.ConfigEditor;

internal class LcStepSelected : UITypeEditor
{
  public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
  {
    return UITypeEditorEditStyle.DropDown;
  }

  public override object EditValue(
    ITypeDescriptorContext context,
    System.IServiceProvider provider,
    object value)
  {
    if (context == null)
      return value;
    Dictionary<Guid, string> dictionary = new Dictionary<Guid, string>();
    if (context.Instance is GridViewSettingsImportObjectType instance)
    {
      Guid result1 = Guid.Empty;
      PropertyDescriptor propertyDescriptor = instance.GetProperties(new Attribute[0]).Find("Guid", true);
      if (propertyDescriptor != null)
        Guid.TryParse(propertyDescriptor.GetValue((object) null)?.ToString(), out result1);
      IMSObjectType objectType = MetaDataHelper.GetObjectType(result1);
      if (objectType != null)
      {
        using (SessionKeeper sessionKeeper = new SessionKeeper())
        {
          IUserSession session = sessionKeeper.Session;
          if (session != null)
          {
            DataSet schema = session.GetLifecycleStepCollection(objectType.ObjectTypeID).GetSchema();
            if (schema != null)
            {
              foreach (DataRow row in (InternalDataCollectionBase) schema.Tables["IMS_LC_STEPS"].Rows)
              {
                Guid result2;
                if (row["F_GUID"] != DBNull.Value && row["F_LC_NAME"] != DBNull.Value && Guid.TryParse(row["F_GUID"].ToString(), out result2))
                  dictionary.Add(result2, row["F_LC_NAME"].ToString());
              }
            }
          }
        }
      }
    }
    Guid result = Guid.Empty;
    if (value != null)
      Guid.TryParse(value.ToString(), out result);
    IWindowsFormsEditorService formsEditorService = (IWindowsFormsEditorService) null;
    if (provider != null)
      formsEditorService = provider.GetService(typeof (IWindowsFormsEditorService)) as IWindowsFormsEditorService;
    if (formsEditorService != null)
    {
      ListBox listBox = new ListBox();
      listBox.BorderStyle = BorderStyle.None;
      listBox.IntegralHeight = true;
      listBox.Click += new EventHandler(this.listBox_Click);
      listBox.Tag = (object) formsEditorService;
      foreach (KeyValuePair<Guid, string> keyValuePair in dictionary)
        listBox.Items.Add((object) keyValuePair.Value);
      string str;
      if (result != Guid.Empty && dictionary.TryGetValue(result, out str))
        listBox.SelectedItem = (object) str;
      formsEditorService.DropDownControl((Control) listBox);
      if (listBox.SelectedItem != null)
      {
        foreach (KeyValuePair<Guid, string> keyValuePair in dictionary)
        {
          if (keyValuePair.Value == listBox.SelectedItem.ToString())
            return (object) keyValuePair.Key;
        }
      }
    }
    return value;
  }

  private void listBox_Click(object sender, EventArgs e)
  {
    if (!(sender is ListBox listBox) || listBox.SelectedItem == null || !(listBox.Tag is IWindowsFormsEditorService tag))
      return;
    tag.CloseDropDown();
  }
}
