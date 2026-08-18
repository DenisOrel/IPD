
// Type: SuperTooltips.CustomTypeEditorProvider
// Assembly: Intermech.Bars, Version=4.0.14.0, Culture=neutral, PublicKeyToken=null
// MVID: E7FE806E-DF4F-43E8-8F59-6B4716E1A4DC
:\IPS\Client\Intermech.Bars.dll

using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Windows.Forms;
using System.Windows.Forms.Design;


namespace SuperTooltips
{
    public class CustomTypeEditorProvider : 
      ITypeDescriptorContext,
      System.IServiceProvider,
      IWindowsFormsEditorService
    {
      private IContainer _container;
      private object _instance;
      private System.IServiceProvider _provider;
      private PropertyDescriptor _propDescriptor;

      public CustomTypeEditorProvider(IContainer container, System.IServiceProvider provider)
      {
        this._container = container;
        this._provider = provider;
      }

      public void CloseDropDown() => throw new Exception("The method or operation is not implemented.");

      public void DropDownControl(Control control)
      {
        throw new Exception("The method or operation is not implemented.");
      }

      public object GetService(System.Type serviceType) => this._provider.GetService(serviceType);

      public void OnComponentChanged()
      {
        if (!(this._provider.GetService(typeof (IComponentChangeService)) is IComponentChangeService service))
          return;
        service.OnComponentChanged(this._instance, (MemberDescriptor) this._propDescriptor, (object) null, (object) null);
      }

      public bool OnComponentChanging() => true;

      public void SetInstance(object instance, PropertyDescriptor desc)
      {
        this._instance = instance;
        this._propDescriptor = desc;
      }

      public DialogResult ShowDialog(Form dialog)
      {
        IntPtr focus = Win32API.GetFocus();
        IUIService service = (IUIService) this.GetService(typeof (IUIService));
        DialogResult dialogResult = service == null ? dialog.ShowDialog() : service.ShowDialog(dialog);
        if (focus != IntPtr.Zero)
          Win32API.SetFocus(focus);
        return dialogResult;
      }

      public IContainer Container => this._container;

      public object Instance => this._instance;

      public PropertyDescriptor PropertyDescriptor => this._propDescriptor;
    }
}
