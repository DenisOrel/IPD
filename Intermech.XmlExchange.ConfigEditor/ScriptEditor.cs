// Decompiled with JetBrains decompiler
// Type: Intermech.XmlExchange.ConfigEditor.ScriptEditor
// Assembly: Intermech.XmlExchange.ConfigEditor, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D148B79A-64FF-4CB8-A129-56A9018E56E2
// Assembly location: D:\IPS\Client\Intermech.XmlExchange.ConfigEditor.dll

using Intermech.Interfaces;
using Intermech.Scripting.Common.DesignTime;
using Intermech.Scripting.Projects.DBScripts;
using Intermech.Scripting.Services;
using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Text;

#nullable disable
namespace Intermech.XmlExchange.ConfigEditor;

internal class ScriptEditor : UITypeEditor
{
  public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
  {
    return UITypeEditorEditStyle.Modal;
  }

  public override object EditValue(
    ITypeDescriptorContext context,
    IServiceProvider sp,
    object value)
  {
    string empty1 = string.Empty;
    if (value != null)
      empty1 = value.ToString();
    string empty2 = string.Empty;
    bool flag = false;
    if (context == null || context.PropertyDescriptor == null)
      return value;
    if (context.Instance is ICustomTypeDescriptor instance)
    {
      foreach (PropertyDescriptor property in instance.GetProperties())
      {
        if (property.Name == "ScriptName")
          empty2 = property.GetValue((object) null)?.ToString();
      }
    }
    foreach (Attribute attribute in context.PropertyDescriptor.Attributes)
    {
      if (attribute is ReadOnlyAttribute readOnlyAttribute)
      {
        flag = readOnlyAttribute.IsReadOnly;
        break;
      }
    }
    if (ApplicationServices.Container.GetService(typeof (IScriptPadService)) is IScriptPadService service)
    {
      DBScriptProject emptyScriptProject = service.CreateEmptyScriptProject(MetaDataHelper.GetObjectTypeID("cadd9457-306c-11d8-b4e9-00304f19f545"));
      if (!string.IsNullOrEmpty(empty2))
        emptyScriptProject.Name = empty2;
      emptyScriptProject.File.SetContentAsText(empty1, Encoding.UTF8);
      if (service.OpenScriptInDialogMode((ScriptProject) emptyScriptProject, new OpenInScriptPadParameters()
      {
        ReadOnlyMode = flag
      }) != null)
      {
        string contentAsText = emptyScriptProject.File.GetContentAsText(Encoding.UTF8);
        if (!string.Equals(empty1, contentAsText))
          return (object) contentAsText;
      }
    }
    return value;
  }
}
