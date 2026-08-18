
// Type: Intermech.Bars.ToolbarItemBase
// Assembly: Intermech.Bars, Version=4.0.14.0, Culture=neutral, PublicKeyToken=null
// MVID: E7FE806E-DF4F-43E8-8F59-6B4716E1A4DC
:\IPS\Client\Intermech.Bars.dll

using System;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;


namespace Intermech.Bars
{
    [Designer(typeof (ToolbarItemBaseDesigner))]
    [DesignTimeVisible(false)]
    [ToolboxItem(false)]
    [DebuggerDisplay("[{CommandName}] {Text}")]
    public abstract class ToolbarItemBase : Component
    {
      private bool _beginGroup;
      internal bool _showText;
      private bool _enabled;
      private Font _font;
      private Color _forecolor;
      private bool _stretch;
      private int _minimumSize;
      private bool _visible;
      private bool _userVisible;
      private string _text;
      private ToolbarItemBase.ItemPadding _padding;
      private object _tag;
      private ToolBarItemImportance _importance;
      private bool _locked;
      private ToolBar _toolBar;
      private Rectangle _buttonInnerBounds;
      private Rectangle _buttonBounds;
      internal Size k;
      internal Size l;
      internal bool _underChevron;
      internal int n;
      internal int o;
      internal bool _drawSeparator;
      internal Rectangle _measuredBounds;
      internal Rectangle _separatorBounds;
      private string _toolTipText;
      private string _commandName;
      private ItemMergeAction _mergeAction;
      private int _mergeIndex;
      internal int _prevMergeIndex;

      protected ToolbarItemBase()
      {
        this._beginGroup = false;
        this._showText = true;
        this._enabled = true;
        this._visible = true;
        this._userVisible = true;
        this._font = (Font) null;
        this._forecolor = SystemColors.ControlText;
        this._stretch = false;
        this._minimumSize = 0;
        this._text = string.Empty;
        this._importance = ToolBarItemImportance.Medium;
        this._locked = false;
        this._buttonInnerBounds = Rectangle.Empty;
        this._buttonBounds = new Rectangle(0, 0, 1, 1);
        this._underChevron = false;
        this.n = 0;
        this.o = 0;
        this._drawSeparator = false;
        this._toolTipText = string.Empty;
        this._commandName = string.Empty;
        this._mergeAction = ItemMergeAction.MergeChildren;
        this._mergeIndex = -1;
        this._prevMergeIndex = -1;
        this._padding = new ToolbarItemBase.ItemPadding(this);
        this._toolBar = (ToolBar) null;
      }

      internal void SetToolBar(ToolBar toolBar) => this._toolBar = toolBar;

      internal ToolbarItemBase a(ToolbarItemBaseCollection A_0)
      {
        ToolbarItemBase toolbarItemBase1 = (ToolbarItemBase) null;
        if (this._prevMergeIndex >= 0 && this._prevMergeIndex < A_0.Count)
          toolbarItemBase1 = A_0[this._prevMergeIndex];
        if (toolbarItemBase1 == null)
        {
          foreach (ToolbarItemBase toolbarItemBase2 in (CollectionBase) A_0)
          {
            if (toolbarItemBase2.Text.CompareTo(this.Text) == 0)
              return toolbarItemBase2;
          }
        }
        return toolbarItemBase1;
      }

      protected internal virtual void ApplyLayout(
        Rectangle buttonBounds,
        Graphics graphics,
        bool vertical,
        bool rightToLeft)
      {
        this._buttonBounds = buttonBounds;
        this._buttonInnerBounds = buttonBounds;
        if (buttonBounds != Rectangle.Empty)
          this._buttonInnerBounds = !vertical ? new Rectangle(buttonBounds.X + this.Padding.Left, buttonBounds.Y + this.Padding.Top, buttonBounds.Width - (this.Padding.Left + this.Padding.Right), buttonBounds.Height - (this.Padding.Top + this.Padding.Bottom)) : new Rectangle(buttonBounds.X + this.Padding.Bottom, buttonBounds.Y + this.Padding.Left, buttonBounds.Width - (this.Padding.Top + this.Padding.Bottom), buttonBounds.Height - (this.Padding.Left + this.Padding.Right));
        if (!this._drawSeparator)
          return;
        if (vertical & rightToLeft)
          this._separatorBounds = new Rectangle(buttonBounds.X, buttonBounds.Bottom + 3, buttonBounds.Width, 7);
        else if (vertical && !rightToLeft)
          this._separatorBounds = new Rectangle(buttonBounds.X, buttonBounds.Y + 2 - 7, buttonBounds.Width, 7);
        else if (!vertical & rightToLeft)
        {
          this._separatorBounds = new Rectangle(buttonBounds.Right + 3, buttonBounds.Y, 7, buttonBounds.Height);
        }
        else
        {
          if (vertical || rightToLeft)
            return;
          this._separatorBounds = new Rectangle(buttonBounds.X + 2 - 7, buttonBounds.Y, 7, buttonBounds.Height);
        }
      }

      public virtual ToolbarItemBase CloneItem()
      {
        ToolbarItemBase clonedItem = this.CreateClonedItem();
        clonedItem.BeginGroup = this.BeginGroup;
        clonedItem.Enabled = this.Enabled;
        clonedItem.Importance = this.Importance;
        clonedItem.Padding.Left = this.Padding.Left;
        clonedItem.Padding.Top = this.Padding.Top;
        clonedItem.Padding.Right = this.Padding.Right;
        clonedItem.Padding.Bottom = this.Padding.Bottom;
        clonedItem.Tag = this.Tag;
        clonedItem.Text = this.Text;
        clonedItem.Font = this.Font;
        clonedItem.ForeColor = this.ForeColor;
        clonedItem.CommandName = this.CommandName;
        clonedItem.ToolTipText = this.ToolTipText;
        clonedItem.Visible = this.Visible;
        clonedItem.Stretch = this.Stretch;
        return clonedItem;
      }

      public ToolbarItemBase Detach()
      {
        if (this._toolBar != null)
          this._toolBar.Items.Remove(this);
        return this;
      }

      protected virtual ToolbarItemBase CreateClonedItem()
      {
        return (ToolbarItemBase) Activator.CreateInstance(this.GetType());
      }

      protected override void Dispose(bool disposing)
      {
        if (disposing && this.ToolBar != null && this.ToolBar.Items.Contains(this))
          this.ToolBar.Items.Remove(this);
        base.Dispose(disposing);
      }

      public virtual void Invalidate()
      {
        if (this._toolBar == null)
          return;
        Rectangle buttonBounds = this._buttonBounds;
        buttonBounds.Inflate(5, 5);
        this._toolBar.Invalidate(buttonBounds);
      }

      internal virtual void LayoutNeeded()
      {
        if (this._toolBar == null)
          return;
        this._toolBar.DoLayout();
      }

      [DefaultValue(false)]
      [Description("Indicates whether the item will be preceeded by a separator.")]
      [Category("Appearance")]
      public virtual bool BeginGroup
      {
        get => this._beginGroup;
        set
        {
          this._beginGroup = value;
          this.LayoutNeeded();
        }
      }

      [Browsable(false)]
      public Rectangle ButtonBounds => this._buttonBounds;

      [Browsable(false)]
      public Rectangle ButtonInnerBounds => this._buttonInnerBounds;

      [DefaultValue(true)]
      [Description("Gets or sets a value indicating whether the item is enabled.")]
      [Category("Behavior")]
      public virtual bool Enabled
      {
        get => this._enabled;
        set
        {
          if (this._enabled == value)
            return;
          this._enabled = value;
          if (this.ToolBar != null && !value)
            this.ToolBar.b(this);
          this.Invalidate();
        }
      }

      internal virtual Font DefaultFont
      {
        get => this.ToolBar != null ? this.ToolBar.Font : Control.DefaultFont;
      }

      [AmbientValue(null)]
      [Category("Appearance")]
      [Description("Indicates the font that is used to draw the item.")]
      public Font Font
      {
        get => this._font != null ? this._font : this.DefaultFont;
        set
        {
          this._font = value;
          this.LayoutNeeded();
        }
      }

      private bool ShouldSerializeFont() => this._font != null;

      [Category("Appearance")]
      [Description("The foreground color used to display text in this item.")]
      [DefaultValue(typeof (Color), "ControlText")]
      public Color ForeColor
      {
        get => this._forecolor;
        set
        {
          if (!(this._forecolor != value))
            return;
          this._forecolor = value;
          this.Invalidate();
        }
      }

      [Description("Indicates the importance of this item. Items with lower importance values will be hidden first when short of space.")]
      [Category("Behavior")]
      [DefaultValue(ToolBarItemImportance.Medium)]
      public virtual ToolBarItemImportance Importance
      {
        get => this._importance;
        set
        {
          this._importance = value;
          this.LayoutNeeded();
        }
      }

      [Category("Behavior")]
      [DefaultValue(false)]
      public bool Locked
      {
        get => this._locked;
        set => this._locked = value;
      }

      [Category("Merging")]
      [DefaultValue(typeof (ItemMergeAction), "MergeChildren")]
      [Description("How to merge this item with the equivalent collection of items on a merge target.")]
      public ItemMergeAction MergeAction
      {
        get => this._mergeAction;
        set
        {
          this._mergeAction = value != ItemMergeAction.MergeChildren || this is MenuItemBase ? value : throw new ArgumentException("MergeChildren is only valid on menu items.");
        }
      }

      [DefaultValue(-1)]
      [Category("Merging")]
      [Description("The index of the matching menu item on the target.")]
      public int MergeIndex
      {
        get => this._mergeIndex;
        set => this._mergeIndex = value;
      }

      internal virtual IButtonsSite Owner => (IButtonsSite) this._toolBar;

      [Category("Layout")]
      [Description("Controls the amount of space between the highlight and the item content.")]
      [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
      public ToolbarItemBase.ItemPadding Padding => this._padding;

      [Description("The minimum amount of toolbar space the item will occupy.")]
      [DefaultValue(0)]
      [Category("Behavior")]
      public virtual int MinimumSize
      {
        get => this._minimumSize;
        set
        {
          this._minimumSize = value >= 0 ? value : throw new ArgumentException("Value must be positive.");
          this.LayoutNeeded();
        }
      }

      [DefaultValue(typeof (object), null)]
      [Browsable(true)]
      [TypeConverter(typeof (StringConverter))]
      public object Tag
      {
        get => this._tag;
        set => this._tag = value;
      }

      [DefaultValue(false)]
      [Category("Layout")]
      [Description("Indicates whether the item will stretch to fill all available space in its container.")]
      public bool Stretch
      {
        get => this._stretch;
        set
        {
          if (value == this._stretch)
            return;
          this._stretch = value;
          this.LayoutNeeded();
        }
      }

      [DefaultValue("")]
      [Localizable(true)]
      [Category("Appearance")]
      [Description("The text associated with this toolbar item.")]
      public virtual string Text
      {
        get => this._text;
        set
        {
          this._text = value;
          this.LayoutNeeded();
        }
      }

      [Browsable(false)]
      public ToolBar ToolBar => this._toolBar;

      [Category("Appearance")]
      [Localizable(true)]
      [Description("Gets or sets the text that appears as a ToolTip for the toolbar item.")]
      [DefaultValue("")]
      public virtual string ToolTipText
      {
        get
        {
          if (this._toolTipText != null && this._toolTipText.Length > 0)
            return this._toolTipText;
          return this is MenuItemBase ? string.Empty : this._text;
        }
        set => this._toolTipText = value;
      }

      private bool ShouldSerializeToolTipText() => !string.IsNullOrEmpty(this._toolTipText);

      [DefaultValue(-1)]
      [Category("Appearance")]
      [Description("The index in parent collection.")]
      [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
      public virtual int Index
      {
        get => this.Owner == null ? -1 : this.Owner.Items.IndexOf(this);
        set
        {
          if (this.Owner == null)
            return;
          this.Owner.Items.Move(this, value);
        }
      }

      private bool ShouldSerializeIndex() => false;

      [DefaultValue("")]
      [Localizable(true)]
      [Category("Appearance")]
      [Description("The command associated with this item.")]
      public string CommandName
      {
        get
        {
          string commandName = this._commandName;
          if (commandName != null && commandName.Length != 0)
            return commandName;
          if (this.Site != null)
            commandName = this.Site.Name;
          if (commandName == null)
            commandName = string.Empty;
          return commandName;
        }
        set => this._commandName = value;
      }

      public virtual string CommandPath => this._commandName;

      [DefaultValue(true)]
      [Category("Behavior")]
      [Description("Indicates whether this item is visible or not.")]
      public virtual bool Visible
      {
        get
        {
          if (this._locked)
            return this._visible;
          return this._visible && this._userVisible;
        }
        set
        {
          if (this._visible == value)
            return;
          this._visible = value;
          this.LayoutNeeded();
        }
      }

      internal bool IsVisible => this._visible;

      internal bool IsUserVisible => this._userVisible;

      internal void SetUserVisible(bool value, bool visible)
      {
        this._userVisible = value;
        this._visible = visible;
        this.LayoutNeeded();
      }

      [TypeConverter(typeof (ExpandableObjectConverter))]
      public class ItemPadding
      {
        private ToolbarItemBase _item;
        private int _top;
        private int _left;
        private int _bottom;
        private int _right;

        internal ItemPadding(ToolbarItemBase A_0)
        {
          this._top = 3;
          this._left = 3;
          this._bottom = 2;
          this._right = 3;
          this._item = A_0;
        }

        [DefaultValue(2)]
        public int Bottom
        {
          get => this._bottom;
          set
          {
            this._bottom = value;
            this._item.LayoutNeeded();
          }
        }

        [DefaultValue(3)]
        public int Left
        {
          get => this._left;
          set
          {
            this._left = value;
            this._item.LayoutNeeded();
          }
        }

        [DefaultValue(3)]
        public int Right
        {
          get => this._right;
          set
          {
            this._right = value;
            this._item.LayoutNeeded();
          }
        }

        [DefaultValue(3)]
        public int Top
        {
          get => this._top;
          set
          {
            this._top = value;
            this._item.LayoutNeeded();
          }
        }
      }
    }
}
