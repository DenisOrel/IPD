
// Type: Intermech.Actions.ActionList
// Assembly: Intermech.Bars, Version=4.0.14.0, Culture=neutral, PublicKeyToken=null
// MVID: E7FE806E-DF4F-43E8-8F59-6B4716E1A4DC
:\IPS\Client\Intermech.Bars.dll

using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;


namespace Intermech.Actions
{
    [ToolboxBitmap(typeof (ActionList))]
    [DefaultProperty("Actions")]
    [ProvideProperty("Action", typeof (Component))]
    public class ActionList : Component, IExtenderProvider
    {
      private ActionCollection _actions;
      private ImageList _imageList;
      private object _tag;
      private bool _showTextOnToolBar;
      private bool _init;
      private Hashtable _components = new Hashtable();
      internal ToolTip _toolTip;
      private IContainer components;

      public ActionList(IContainer container)
      {
        container.Add((IComponent) this);
        this.InitializeComponent();
        this.Init();
      }

      public ActionList()
      {
        this.InitializeComponent();
        this.Init();
      }

      [Browsable(false)]
      public int Count => this._actions.Count;

      [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
      [Category("Behavior")]
      [Description("The collection of Actions that makes up this ActionList")]
      public ActionCollection Actions
      {
        get => this._actions;
        set => this._actions = value;
      }

      [Category("Behavior")]
      [Description("The ImageList from which this ActionList will get all of the action images.")]
      public ImageList ImageList
      {
        get => this._imageList;
        set
        {
          this._imageList = value;
          foreach (Action action in this.Actions)
            action.ImageList = this._imageList;
        }
      }

      [Category("Data")]
      [Description("User defined data associated with this ActionList.")]
      public object Tag
      {
        get => this._tag;
        set => this._tag = value;
      }

      [Category("Behavior")]
      [Description("User defined data associated with this ActionList.")]
      public bool ShowTextOnToolBar
      {
        get => this._showTextOnToolBar;
        set
        {
          this._showTextOnToolBar = value;
          foreach (Action action in this.Actions)
            action.ShowTextOnToolBar = value;
        }
      }

      [ExtenderProvidedProperty]
      [TypeConverter(typeof (ActionConverter))]
      [Description("Action object that is associated with the control.")]
      [Category("Behavior")]
      public Action GetAction(Component comp)
      {
        return (Action) this._components[(object) comp] ?? this.Actions.Null;
      }

      [ExtenderProvidedProperty]
      public void SetAction(Component comp, Action value)
      {
        if (value != null)
        {
          Action component = (Action) this._components[(object) comp];
          if (component != null)
          {
            if (value == component)
              return;
            component.SetComponent(comp, false);
            this._components.Remove((object) comp);
          }
          if (value == this.Actions.Null)
            return;
          value._owner = this;
          value.SetComponent(comp, true);
          this._components.Add((object) comp, (object) value);
        }
        else
          this._components.Remove((object) comp);
      }

      public bool ShouldSerializeAction(object o)
      {
        foreach (Action action in this.Actions)
        {
          if (action.HandleComponent((Component) o))
            return true;
        }
        return false;
      }

      public bool CanExtend(object target)
      {
        return target is Component && !(target is ActionList) && !(target is Action);
      }

      private void InitializeComponent()
      {
        this.components = (IContainer) new System.ComponentModel.Container();
        this._toolTip = new ToolTip(this.components);
      }

      private void Init()
      {
        this._actions = new ActionCollection(this);
        if (this.DesignMode)
          return;
        Application.Idle += new EventHandler(this.OnIdle);
      }

      private void OnIdle(object sender, EventArgs e)
      {
        if (!this._init)
        {
          foreach (Action action in this.Actions)
            action.FinishInit();
          this._init = true;
        }
        foreach (Action action in this.Actions)
          action.OnUpdate((object) this, e);
      }

      internal void OnComponentDisposed(object sender, EventArgs e)
      {
        Component component1 = (Component) sender;
        Action component2 = (Action) this._components[(object) component1];
        if (component2 == null)
          return;
        component2.SetComponent(component1, false);
        this._components.Remove((object) component1);
      }

      protected override void Dispose(bool disposing)
      {
        if (disposing)
        {
          if (!this.DesignMode)
            Application.Idle -= new EventHandler(this.OnIdle);
          if (this.components != null)
            this.components.Dispose();
        }
        base.Dispose(disposing);
      }
    }
}
