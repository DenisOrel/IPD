using Intermech.Interfaces.Client;
using Intermech.Interfaces.Configuration;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;


namespace IMClient
{
    public class UISettingsPage : IPropertyPage, IPropertyPageSearchOptionEvents
    {
      private IServiceProvider _provider;
      private UISettingsWrapper _uiSettings;
      private ClassWrapperForPropertyGrid _object;

      public UISettingsPage(IServiceProvider provider)
      {
        this._provider = provider;
        ((IPropertyPagesService) this._provider.GetService(typeof (IPropertyPagesService)))?.AddPage(LocalizationHolder.rm.GetString("IMClient_8"), (IPropertyPage) this);
        this._uiSettings = new UISettingsWrapper((IConfigurationManager) this._provider.GetService(typeof (IConfigurationManager)));
        this._object = new ClassWrapperForPropertyGrid((object) this._uiSettings);
      }

      public string HelpTopicID
      {
        [DebuggerStepThrough] get => "703";
      }

      public void Cancel()
      {
        if (this._uiSettings == null)
          return;
        this._uiSettings.RestoreValues();
      }

      public object Control
      {
        [DebuggerStepThrough] get => (object) this._object;
      }

      public void Apply()
      {
        if (this._uiSettings == null)
          return;
        this._uiSettings.Apply();
        this._object.ResetOldValues();
      }

      public PropertyPageType Type
      {
        [DebuggerStepThrough] get => PropertyPageType.Object;
      }

      public string PageName
      {
        [DebuggerStepThrough] get => LocalizationHolder.rm.GetString("IMClient_9");
      }

      public string HeaderText
      {
        [DebuggerStepThrough] get => this.PageName;
      }

      public event EventHandler Changed;

      public List<string> GetOptionNames()
      {
        return !(this.Control is ClassWrapperForPropertyGrid control) ? new List<string>() : IPropertyPageHelper.GetOptionNames((ICustomTypeDescriptor) control);
      }
    }
}
