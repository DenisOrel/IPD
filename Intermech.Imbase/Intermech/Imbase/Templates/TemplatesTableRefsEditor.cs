// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.Templates.TemplatesTableRefsEditor
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Windows.Forms;
using System.Windows.Forms.Design;

#nullable disable
namespace Intermech.Imbase.Templates;

internal class TemplatesTableRefsEditor : UITypeEditor
{
  private IWindowsFormsEditorService svc;

  private void OnBtnClick(object sender, EventArgs e)
  {
    if (this.svc == null)
      return;
    this.svc.CloseDropDown();
  }

  public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
  {
    return UITypeEditorEditStyle.DropDown;
  }

  public override object EditValue(
    ITypeDescriptorContext context,
    System.IServiceProvider provider,
    object value)
  {
    this.svc = (IWindowsFormsEditorService) provider.GetService(typeof (IWindowsFormsEditorService));
    if (value == null)
      return (object) null;
    SymbolSelectRB_Ctrl symbolSelectRbCtrl = new SymbolSelectRB_Ctrl((value is TemplatesBody templatesBody ? templatesBody.Body : (string) null) ?? string.Empty);
    symbolSelectRbCtrl.Filter = templatesBody?.Filter ?? string.Empty;
    symbolSelectRbCtrl.BtnClickEvent += new EventHandler(this.OnBtnClick);
    this.svc.DropDownControl((Control) symbolSelectRbCtrl);
    symbolSelectRbCtrl.BtnClickEvent -= new EventHandler(this.OnBtnClick);
    if (symbolSelectRbCtrl.DlgRes != DialogResult.OK)
      return (object) templatesBody ?? (object) new TemplatesBody(string.Empty, UseTemplate.Ref);
    return (object) new TemplatesBody(templatesBody?.Body ?? string.Empty, UseTemplate.Ref)
    {
      Filter = symbolSelectRbCtrl.Filter
    };
  }
}
