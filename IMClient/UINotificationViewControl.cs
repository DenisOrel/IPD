
// Type: IMClient.UINotificationViewControl




using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using WpfBindingErrors;


namespace IMClient
{
    internal class UINotificationViewControl : UserControl, IComponentConnector
    {
      internal ListView ItemsListView;
      private bool _contentLoaded;

      public UINotificationViewControl()
      {
        this.InitializeComponent();
        DesignerProperties.GetIsInDesignMode((DependencyObject) this);
      }

      [Conditional("RELEASE")]
      private void EnableBindingExceptionThrower()
      {
        if (BindingExceptionThrower.IsAttached)
          return;
        BindingExceptionThrower.Attach();
      }

      [GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
      [DebuggerNonUserCode]
      public void InitializeComponent()
      {
        if (this._contentLoaded)
          return;
        this._contentLoaded = true;
        Application.LoadComponent((object) this, new Uri("/IMClient;component/views/uinotificationviewcontrol.xaml", UriKind.Relative));
      }

      [EditorBrowsable(EditorBrowsableState.Never)]
      [DebuggerNonUserCode]
      [GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
      void IComponentConnector.Connect(int connectionId, object target)
      {
        if (connectionId == 1)
          this.ItemsListView = (ListView) target;
        else
          this._contentLoaded = true;
      }
    }
}
