
// Type: IMClient.OptimizationSettingsPage




using Intermech.Interfaces.Client;
using Intermech.Interfaces.Configuration;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;


namespace IMClient
{
    internal sealed class OptimizationSettingsPage : IPropertyPage, IPropertyPageSearchOptionEvents
    {
      private readonly IServiceProvider _provider;
      private readonly OptimizationSettingsWrapper _optSettings;
      private readonly ClassWrapperForPropertyGrid _object;

      public OptimizationSettingsPage(IServiceProvider provider)
      {
        this._provider = provider;
        ((IPropertyPagesService) this._provider.GetService(typeof (IPropertyPagesService)))?.AddPage(LocalizationHolder.rm.GetString("IMClient_12"), (IPropertyPage) this);
        this._optSettings = new OptimizationSettingsWrapper((IConfigurationManager) this._provider.GetService(typeof (IConfigurationManager)));
        this._object = new ClassWrapperForPropertyGrid((object) this._optSettings);
      }

      public string HelpTopicID
      {
        [DebuggerStepThrough] get => "1694";
      }

      public void Cancel()
      {
        if (this._optSettings == null)
          return;
        this._optSettings.RestoreValues();
      }

      public object Control
      {
        [DebuggerStepThrough] get => (object) this._object;
      }

      public void Apply()
      {
        if (this._optSettings == null)
          return;
        this._optSettings.Apply();
        this._object.ResetOldValues();
      }

      public PropertyPageType Type
      {
        [DebuggerStepThrough] get => PropertyPageType.Object;
      }

      public string PageName
      {
        [DebuggerStepThrough] get => LocalizationHolder.rm.GetString("IMClient_13");
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
