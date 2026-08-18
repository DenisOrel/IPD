
// Type: Intermech.Actions.Action
// Assembly: Intermech.Bars, Version=4.0.14.0, Culture=neutral, PublicKeyToken=null
// MVID: E7FE806E-DF4F-43E8-8F59-6B4716E1A4DC
:\IPS\Client\Intermech.Bars.dll

using Intermech.Actions.Design;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing.Design;
using System.Windows.Forms;


namespace Intermech.Actions
{
    [DesignTimeVisible(false)]
    [ToolboxItem(false)]
    [DefaultEvent("Execute")]
    [DefaultProperty("Text")]
    public class Action : Component
    {
      private object _tag;
      private string _text;
      private int _imageIndex = -1;
      private Hashtable _components = new Hashtable();
      internal ActionList _owner;
      private bool _enabled = true;
      private bool _checked;
      private bool _visible = true;
      private Shortcut _shortcut;
      private string _hint;
      private System.ComponentModel.Container components;

      public Action(IContainer container)
      {
        container.Add((IComponent) this);
        this.InitializeComponent();
      }

      public Action() => this.InitializeComponent();

      [Category("Misc")]
      [Localizable(true)]
      [Description("The text used in controls associated to this Action.")]
      public string Text
      {
        get => this._text;
        set
        {
          this._text = value;
          IDictionaryEnumerator enumerator = this._components.GetEnumerator();
          while (enumerator.MoveNext())
            ((ActionData) enumerator.Value).Text = this._text;
        }
      }

      [Category("Behavior")]
      [DefaultValue(true)]
      [Description("Indicates whether the associated components are enabled.")]
      public bool Enabled
      {
        get => this._enabled;
        set
        {
          this._enabled = value;
          IDictionaryEnumerator enumerator = this._components.GetEnumerator();
          while (enumerator.MoveNext())
            ((ActionData) enumerator.Value).Enabled = this._enabled;
        }
      }

      [Category("Behavior")]
      [Description("Indicates whether the associated components are checked.")]
      [DefaultValue(false)]
      public bool Checked
      {
        get => this._checked;
        set
        {
          this._checked = value;
          IDictionaryEnumerator enumerator = this._components.GetEnumerator();
          while (enumerator.MoveNext())
            ((ActionData) enumerator.Value).Checked = this._checked;
        }
      }

      [Category("Misc")]
      [Description("Indicates the shorcut for this Action.")]
      [DefaultValue(Shortcut.None)]
      public Shortcut Shortcut
      {
        get => this._shortcut;
        set
        {
          this._shortcut = value;
          IDictionaryEnumerator enumerator = this._components.GetEnumerator();
          while (enumerator.MoveNext())
            ((ActionData) enumerator.Value).Shortcut = this._shortcut;
        }
      }

      [Category("Behavior")]
      [Description("Indicates the shorcut for this Action.")]
      [DefaultValue(true)]
      public bool Visible
      {
        get => this._visible;
        set
        {
          this._visible = value;
          IDictionaryEnumerator enumerator = this._components.GetEnumerator();
          while (enumerator.MoveNext())
            ((ActionData) enumerator.Value).Visible = this._visible;
        }
      }

      [TypeConverter(typeof (StringConverter))]
      [Category("Data")]
      [Description("User defined data associated with this Action.")]
      [DefaultValue(null)]
      public object Tag
      {
        get => this._tag;
        set => this._tag = value;
      }

      [Category("Misc")]
      [Localizable(true)]
      [Description("Indicates the index of the image in the parent ActionList's ImageList this Action will use to obtains its image.")]
      [TypeConverter(typeof (ImageIndexConverter))]
      [Editor(typeof (ImageIndexEditor), typeof (UITypeEditor))]
      [DefaultValue(-1)]
      public int ImageIndex
      {
        get => this._imageIndex;
        set
        {
          this._imageIndex = value;
          IDictionaryEnumerator enumerator = this._components.GetEnumerator();
          while (enumerator.MoveNext())
            ((ActionData) enumerator.Value).ImageIndex = this._imageIndex;
        }
      }

      [Category("Misc")]
      [Localizable(true)]
      [Description("Indicates the text that appears as a ToolTip for a control.")]
      [DefaultValue("")]
      public string Hint
      {
        get => this._hint;
        set
        {
          this._hint = value;
          IDictionaryEnumerator enumerator = this._components.GetEnumerator();
          while (enumerator.MoveNext())
            ((ActionData) enumerator.Value).Hint = this._hint;
        }
      }

      [Browsable(false)]
      public ActionList Parent => this._owner;

      [Description("Triggered when the action is executed")]
      public event EventHandler Execute;

      [Description("Triggered when the application is idle or when the action list updates.")]
      public event EventHandler Update;

      [Browsable(false)]
      internal ImageList ImageList
      {
        set
        {
          IDictionaryEnumerator enumerator = this._components.GetEnumerator();
          while (enumerator.MoveNext())
            ((ActionData) enumerator.Value).ImageList = value;
        }
      }

      internal void OnExecute(object sender, EventArgs e)
      {
        if (this.Execute == null)
          return;
        this.Execute((object) this, e);
      }

      internal void OnUpdate(object sender, EventArgs e)
      {
        if (this.Update == null)
          return;
        this.Update((object) this, e);
      }

      internal void SetComponent(Component comp, bool add)
      {
        ActionData component = (ActionData) this._components[(object) comp];
        if (add)
        {
          if (component != null)
            return;
          ActionData actionData = new ActionData();
          actionData.Attach(this, comp, this.DesignMode);
          this._components[(object) comp] = (object) actionData;
        }
        else
        {
          if (component == null)
            return;
          component.Detach();
          this._components.Remove((object) comp);
        }
      }

      internal bool HandleComponent(Component comp) => this._components[(object) comp] != null;

      [Browsable(false)]
      internal bool ShowTextOnToolBar
      {
        set
        {
          string text = value ? this.Text : (string) null;
          IDictionaryEnumerator enumerator = this._components.GetEnumerator();
          while (enumerator.MoveNext())
            ((ActionData) enumerator.Value).ShowTextOnToolBar = text;
        }
      }

      private void InitializeComponent() => this.components = new System.ComponentModel.Container();

      internal void FinishInit()
      {
        IDictionaryEnumerator enumerator = this._components.GetEnumerator();
        while (enumerator.MoveNext())
          ((ActionData) enumerator.Value).FinishInit();
      }
    }
}
