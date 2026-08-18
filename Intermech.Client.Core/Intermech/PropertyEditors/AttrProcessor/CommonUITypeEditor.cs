
// Type: Intermech.PropertyEditors.AttrProcessor.CommonUITypeEditor
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Localization;
using System.ComponentModel;
using System.Drawing.Design;
using System.Windows.Forms;
using System.Windows.Forms.Design;


namespace Intermech.PropertyEditors.AttrProcessor;

/// <summary>
/// Редактор-обертка контрола для редактирования в PropertyGrid'ах
/// контрол должен поддерживать интерфейс IAttributeUITypeEditorControl
/// </summary>
internal class CommonUITypeEditor : UITypeEditor
{
  private IWindowsFormsEditorService edSvc;
  private IAttributeEditorControl iAttributeEditorControl;

  public CommonUITypeEditor(IAttributeEditorControl iAttributeEditorControl)
  {
    this.iAttributeEditorControl = iAttributeEditorControl != null ? iAttributeEditorControl : throw new AttributeProcessorException(LocalizationHolder.rm.GetString("Client.Core_923"));
  }

  public override bool IsDropDownResizable => this.iAttributeEditorControl.IsDropDownResizable;

  public override object EditValue(
    ITypeDescriptorContext context,
    System.IServiceProvider provider,
    object value)
  {
    object obj = value;
    bool flag = false;
    this.iAttributeEditorControl.RefreshControl();
    switch (this.GetEditStyle())
    {
      case UITypeEditorEditStyle.Modal:
        if (!(this.iAttributeEditorControl is Form))
          throw new AttributeProcessorException(LocalizationHolder.rm.GetString("Client.Core_924"));
        if ((this.iAttributeEditorControl as Form).ShowDialog() == DialogResult.OK)
        {
          flag = true;
          break;
        }
        break;
      case UITypeEditorEditStyle.DropDown:
        if (!(this.iAttributeEditorControl is Control))
          throw new AttributeProcessorException(LocalizationHolder.rm.GetString("Client.Core_925"));
        this.edSvc = (IWindowsFormsEditorService) provider.GetService(typeof (IWindowsFormsEditorService));
        if (this.edSvc != null)
        {
          this.iAttributeEditorControl.OnCloseDemand += new CloseDemandHandler(this.iAttributeEditorControl_OnCloseDemand);
          try
          {
            this.edSvc.DropDownControl(this.iAttributeEditorControl as Control);
          }
          finally
          {
            this.iAttributeEditorControl.OnCloseDemand -= new CloseDemandHandler(this.iAttributeEditorControl_OnCloseDemand);
          }
          flag = true;
          break;
        }
        break;
    }
    if (flag)
    {
      AttributeProcessor attributeProcessor1 = (AttributeProcessor) this.iAttributeEditorControl.AttributeProcessor;
      int? index1 = this.iAttributeEditorControl.Index;
      if (!index1.HasValue)
      {
        obj = (object) attributeProcessor1.GetValues(this.iAttributeEditorControl.AttributeId);
      }
      else
      {
        AttributeProcessor attributeProcessor2 = attributeProcessor1;
        int attributeId = this.iAttributeEditorControl.AttributeId;
        index1 = this.iAttributeEditorControl.Index;
        int index2 = index1.Value;
        obj = attributeProcessor2.GetValue(attributeId, index2);
      }
    }
    return obj;
  }

  private void iAttributeEditorControl_OnCloseDemand(object sender, CloseControlEventArgs args)
  {
    if (this.edSvc == null || args.Cancel)
      return;
    this.edSvc.CloseDropDown();
  }

  public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
  {
    return this.iAttributeEditorControl.GetEditStyle(context);
  }

  public override bool GetPaintValueSupported(ITypeDescriptorContext context)
  {
    return this.iAttributeEditorControl.GetPaintValueSupported(context);
  }

  public override void PaintValue(PaintValueEventArgs e)
  {
    this.iAttributeEditorControl.PaintValue(e);
  }
}
