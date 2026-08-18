
// Type: Intermech.Actions.ActionData
// Assembly: Intermech.Bars, Version=4.0.14.0, Culture=neutral, PublicKeyToken=null
// MVID: E7FE806E-DF4F-43E8-8F59-6B4716E1A4DC
:\IPS\Client\Intermech.Bars.dll

using Intermech.Bars;
using System;
using System.ComponentModel;
using System.Reflection;
using System.Windows.Forms;


namespace Intermech.Actions
{
    internal class ActionData : IDisposable
    {
      private PropertyInfo _text;
      private PropertyInfo _enabled;
      private PropertyInfo _checked;
      private PropertyInfo _visible;
      private PropertyInfo _shortcut;
      private PropertyInfo _imageIndex;
      private PropertyInfo _imageList;
      private PropertyInfo _toolTipText;
      private bool _click;
      private Component _component;
      private Action _action;

      internal PropertyInfo GetProperty(System.Type baseType, string propName, System.Type needType)
      {
        PropertyInfo property = baseType.GetProperty(propName);
        if (property != (PropertyInfo) null && (!property.CanRead || !property.CanWrite) && property.PropertyType == needType)
          property = (PropertyInfo) null;
        return property;
      }

      internal void Attach(Action a, Component o, bool designMode)
      {
        this._component = o;
        this._action = a;
        System.Type type = o.GetType();
        this._text = this.GetProperty(type, "Text", typeof (string));
        this.Text = this._action.Text;
        this._enabled = this.GetProperty(type, "Enabled", typeof (bool));
        this.Enabled = this._action.Enabled;
        this._checked = !(this._component is ToolBarButton) ? this.GetProperty(type, "Checked", typeof (bool)) : this.GetProperty(type, "Pushed", typeof (bool));
        this.Checked = this._action.Checked;
        this._visible = this.GetProperty(type, "Visible", typeof (bool));
        this.Visible = this._action.Visible;
        this._shortcut = this.GetProperty(type, "Shortcut", typeof (Shortcut));
        this.Shortcut = this._action.Shortcut;
        if (!(this._component is ToolBarButton))
          this._imageList = this.GetProperty(type, "ImageList", typeof (ImageList));
        this.ImageList = this._action.Parent.ImageList;
        this._imageIndex = this.GetProperty(type, "ImageIndex", typeof (int));
        this.ImageIndex = this._action.ImageIndex;
        this._toolTipText = this.GetProperty(type, "ToolTipText", typeof (string));
        this.Hint = this._action.Hint;
        if (!designMode)
        {
          if (this._component is ToolBarButton)
          {
            System.Windows.Forms.ToolBar parent = ((ToolBarButton) this._component).Parent;
            if (parent != null)
            {
              parent.ButtonClick += new ToolBarButtonClickEventHandler(this.OnToolbarClick);
              this._click = true;
            }
          }
          else if (this._component is ButtonItemBase)
          {
            ((ButtonItemBase) this._component).Click += new EventHandler(this._action.OnExecute);
            this._click = true;
          }
          else
          {
            EventInfo eventInfo = type.GetEvent("Click");
            if (eventInfo != (EventInfo) null && eventInfo.EventHandlerType == typeof (EventHandler))
            {
              eventInfo.AddEventHandler((object) this._component, (Delegate) new EventHandler(this._action.OnExecute));
              this._click = true;
            }
          }
        }
        this._component.Disposed += new EventHandler(this._action.Parent.OnComponentDisposed);
      }

      internal void Detach()
      {
        this._text = (PropertyInfo) null;
        this._enabled = (PropertyInfo) null;
        this._checked = (PropertyInfo) null;
        this._shortcut = (PropertyInfo) null;
        if (this._component != null && this._click)
        {
          if (this._component is ToolBarButton)
          {
            System.Windows.Forms.ToolBar parent = ((ToolBarButton) this._component).Parent;
            if (parent != null)
              parent.ButtonClick -= new ToolBarButtonClickEventHandler(this.OnToolbarClick);
          }
          else if (this._component is ButtonItemBase)
            ((ButtonItemBase) this._component).Click -= new EventHandler(this._action.OnExecute);
          else
            this._component.GetType().GetEvent("Click").RemoveEventHandler((object) this._component, (Delegate) new EventHandler(this._action.OnExecute));
        }
        this._component.Disposed -= new EventHandler(this._action.Parent.OnComponentDisposed);
      }

      internal string Text
      {
        set
        {
          if (!(this._text != (PropertyInfo) null))
            return;
          if (this._component is ToolBarButton && !this._action.Parent.ShowTextOnToolBar)
          {
            this._text.SetValue((object) this._component, (object) null, (object[]) null);
          }
          else
          {
            if (!((string) this._text.GetValue((object) this._component, (object[]) null) != value))
              return;
            if (this._component is Control && ((Control) this._component).IsHandleCreated)
              ((Control) this._component).BeginInvoke((Delegate) (() => this._text.SetValue((object) this._component, (object) value, (object[]) null)));
            else
              this._text.SetValue((object) this._component, (object) value, (object[]) null);
          }
        }
      }

      internal bool Enabled
      {
        set
        {
          if (!(this._enabled != (PropertyInfo) null) || (bool) this._enabled.GetValue((object) this._component, (object[]) null) == value)
            return;
          if (this._component is Control && ((Control) this._component).IsHandleCreated)
            ((Control) this._component).BeginInvoke((Delegate) (() => this._enabled.SetValue((object) this._component, (object) value, (object[]) null)));
          else
            this._enabled.SetValue((object) this._component, (object) value, (object[]) null);
        }
      }

      internal bool Checked
      {
        set
        {
          if (!(this._checked != (PropertyInfo) null) || (bool) this._checked.GetValue((object) this._component, (object[]) null) == value)
            return;
          if (this._component is Control && ((Control) this._component).IsHandleCreated)
            ((Control) this._component).BeginInvoke((Delegate) (() => this._checked.SetValue((object) this._component, (object) value, (object[]) null)));
          else
            this._checked.SetValue((object) this._component, (object) value, (object[]) null);
        }
      }

      internal bool Visible
      {
        set
        {
          if (!(this._visible != (PropertyInfo) null) || (bool) this._visible.GetValue((object) this._component, (object[]) null) == value)
            return;
          if (this._component is Control && ((Control) this._component).IsHandleCreated)
            ((Control) this._component).BeginInvoke((Delegate) (() => this._visible.SetValue((object) this._component, (object) value, (object[]) null)));
          else
            this._visible.SetValue((object) this._component, (object) value, (object[]) null);
        }
      }

      internal Shortcut Shortcut
      {
        set
        {
          if (!(this._shortcut != (PropertyInfo) null) || (Shortcut) this._shortcut.GetValue((object) this._component, (object[]) null) == value)
            return;
          if (this._component is Control && ((Control) this._component).IsHandleCreated)
            ((Control) this._component).BeginInvoke((Delegate) (() => this._shortcut.SetValue((object) this._component, (object) value, (object[]) null)));
          else
            this._shortcut.SetValue((object) this._component, (object) value, (object[]) null);
        }
      }

      internal ImageList ImageList
      {
        set
        {
          if (this._component is ToolBarButton)
          {
            ToolBarButton component = (ToolBarButton) this._component;
            if (component.Parent == null || component.Parent.ImageList == value)
              return;
            component.Parent.ImageList = value;
          }
          else
          {
            if (!(this._imageList != (PropertyInfo) null) || (ImageList) this._imageList.GetValue((object) this._component, (object[]) null) == value)
              return;
            if (this._component is Control && ((Control) this._component).IsHandleCreated)
              ((Control) this._component).BeginInvoke((Delegate) (() => this._imageList.SetValue((object) this._component, (object) value, (object[]) null)));
            else
              this._imageList.SetValue((object) this._component, (object) value, (object[]) null);
          }
        }
      }

      internal int ImageIndex
      {
        set
        {
          if (!(this._imageIndex != (PropertyInfo) null) || (int) this._imageIndex.GetValue((object) this._component, (object[]) null) == value)
            return;
          if (this._component is Control && ((Control) this._component).IsHandleCreated)
            ((Control) this._component).BeginInvoke((Delegate) (() => this._imageIndex.SetValue((object) this._component, (object) value, (object[]) null)));
          else
            this._imageIndex.SetValue((object) this._component, (object) value, (object[]) null);
        }
      }

      private void OnToolbarClick(object sender, ToolBarButtonClickEventArgs e)
      {
        if (e.Button != this._component)
          return;
        this._action.OnExecute(sender, (EventArgs) e);
      }

      public void Dispose() => this.Detach();

      internal void FinishInit()
      {
        if (!(this._component is ToolBarButton) || this._click)
          return;
        System.Windows.Forms.ToolBar parent = ((ToolBarButton) this._component).Parent;
        if (parent == null)
          return;
        parent.ButtonClick += new ToolBarButtonClickEventHandler(this.OnToolbarClick);
        this._click = true;
      }

      internal string ShowTextOnToolBar
      {
        set
        {
          if (!(this._component is ToolBarButton))
            return;
          this.Text = value;
        }
      }

      internal string Hint
      {
        set
        {
          if (this._toolTipText != (PropertyInfo) null)
          {
            if (!((string) this._toolTipText.GetValue((object) this._component, (object[]) null) != value))
              return;
            this._toolTipText.SetValue((object) this._component, (object) value, (object[]) null);
          }
          else
          {
            if (!(this._component is Control))
              return;
            if (((Control) this._component).IsHandleCreated)
            {
              ((Control) this._component).BeginInvoke((Delegate) (() =>
              {
                Control component = (Control) this._component;
                ToolTip toolTip = this._action._owner._toolTip;
                if (!(toolTip.GetToolTip(component) != value))
                  return;
                toolTip.SetToolTip(component, value);
              }));
            }
            else
            {
              Control component = (Control) this._component;
              ToolTip toolTip = this._action._owner._toolTip;
              if (!(toolTip.GetToolTip(component) != value))
                return;
              toolTip.SetToolTip(component, value);
            }
          }
        }
      }

      public delegate void InvokeDelegate();
    }
}
