
// Type: Intermech.Client.Core.Organizer.NavigationBar
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;


namespace Intermech.Client.Core.Organizer;

[Designer(typeof (NavigationBarDesigner))]
public class NavigationBar : ContainerControl, ISupportInitialize
{
  private NavigationLayout _navLayout = new NavigationLayout();
  private EventHandler _layoutChanged;
  private BandCollection _bands = new BandCollection();
  private NavigationBand _activeBand;
  private ControlEventHandler _bandAdded;
  private BandEventHandler _activeBandChanging;
  private EventHandler _activeBandChanged;
  private ButtonCollection _buttons = new ButtonCollection();
  private int _buttonHeight = 32 /*0x20*/;
  private int _miniButtonWidth = 25;
  private int _largeButtonsCount;
  private int _headerHeight = 27;
  private int _footerHeight = 32 /*0x20*/;
  private bool _initializing = true;
  private bool _isCollapsed;
  private readonly object _threadLock = new object();

  /// <summary>
  /// 
  /// </summary>
  [DefaultValue(null)]
  public NavigationBand ActiveBand
  {
    get => this._activeBand;
    set
    {
      if (this._activeBand == value)
        return;
      BandEventArgs e = new BandEventArgs(value);
      if (this._activeBandChanging != null)
        this._activeBandChanging((object) this, e);
      if (e.Canceled)
        return;
      foreach (NavigationBand band in (CollectionBase) this._bands)
      {
        if (band != value && band.Button != null)
          band.Button.Active = false;
      }
      if (value != null && value.Button != null)
        value.Button.Active = true;
      this._activeBand = value;
      this.OnLayout(new LayoutEventArgs((Control) this, nameof (ActiveBand)));
      if (this._activeBandChanged != null)
        this._activeBandChanged((object) this, (EventArgs) e);
      this.Invalidate();
    }
  }

  /// <summary>Коллекция панелей.</summary>
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public BandCollection Bands
  {
    get => this._bands;
    set => this._bands = value;
  }

  /// <summary>
  /// 
  /// </summary>
  [NotifyParentProperty(true)]
  [DefaultValue(32 /*0x20*/)]
  public int ButtonHeight
  {
    get => this._buttonHeight;
    set
    {
      this._buttonHeight = value;
      if (this._initializing)
        return;
      this.OnLayout(new LayoutEventArgs((Control) this, nameof (ButtonHeight)));
    }
  }

  /// <summary>
  /// 
  /// </summary>
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public ButtonCollection Buttons
  {
    get => this._buttons;
    set => this._buttons = value;
  }

  /// <summary>
  /// 
  /// </summary>
  [NotifyParentProperty(true)]
  [DefaultValue(false)]
  public bool IsCollapsed
  {
    get => this._isCollapsed;
    set
    {
      bool isCollapsed = this._isCollapsed;
      this._isCollapsed = value;
      this._navLayout.SwitchCollapsion(value, isCollapsed);
      if (!this._initializing)
        this.OnLayout(new LayoutEventArgs((Control) this, nameof (IsCollapsed)));
      this.Invalidate();
    }
  }

  /// <summary>
  /// 
  /// </summary>
  [NotifyParentProperty(true)]
  [DefaultValue(32 /*0x20*/)]
  public int FooterHeight
  {
    get => this._footerHeight;
    set
    {
      this._footerHeight = value;
      if (!this._initializing)
        this.OnLayout(new LayoutEventArgs((Control) this, nameof (FooterHeight)));
      this.Invalidate();
    }
  }

  /// <summary>
  /// 
  /// </summary>
  [NotifyParentProperty(true)]
  [DefaultValue(27)]
  public int HeaderHeight
  {
    get => this._headerHeight;
    set
    {
      this._headerHeight = value;
      if (!this._initializing)
        this.OnLayout(new LayoutEventArgs((Control) this, nameof (HeaderHeight)));
      this.Invalidate();
    }
  }

  /// <summary>
  /// 
  /// </summary>
  [NotifyParentProperty(true)]
  [DefaultValue(0)]
  public int LargeButtonsCount
  {
    get => this._largeButtonsCount;
    set
    {
      if (!this._initializing)
      {
        this._largeButtonsCount = value < 0 ? 0 : (value > this._navLayout.VisibleButtons ? this._navLayout.VisibleButtons : value);
        this.OnLayout(new LayoutEventArgs((Control) this, nameof (LargeButtonsCount)));
      }
      else
        this._largeButtonsCount = value;
      this.Invalidate();
    }
  }

  /// <summary>
  /// 
  /// </summary>
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public NavigationLayout NaviLayout
  {
    get => this._navLayout;
    set
    {
      this._navLayout = value != null ? value : throw new ArgumentNullException();
      this._navLayout.Bar = this;
    }
  }

  /// <summary>
  /// 
  /// </summary>
  [NotifyParentProperty(true)]
  [DefaultValue(25)]
  public int SmallButtonWidth
  {
    get => this._miniButtonWidth;
    set => this._miniButtonWidth = value;
  }

  /// <summary>
  /// 
  /// </summary>
  public NavigationBar()
  {
    this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
    this.SetStyle(ControlStyles.UserPaint, true);
    this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
    this.SetStyle(ControlStyles.ResizeRedraw, true);
    this._navLayout.Bar = this;
    this._bands.ItemAdded += new CollectionEventHandler(this.On_bands_ItemAdded);
    this._bands.ItemRemoved += new CollectionEventHandler(this.On_bands_ItemRemoved);
  }

  /// <summary>
  /// 
  /// </summary>
  public event EventHandler ActiveBandChanged
  {
    add
    {
      lock (this._threadLock)
        this._activeBandChanged += value;
    }
    remove
    {
      lock (this._threadLock)
        this._activeBandChanged -= value;
    }
  }

  /// <summary>
  /// 
  /// </summary>
  public event BandEventHandler ActiveBandChanging
  {
    add
    {
      lock (this._threadLock)
        this._activeBandChanging += value;
    }
    remove
    {
      lock (this._threadLock)
        this._activeBandChanging -= value;
    }
  }

  /// <summary>
  /// 
  /// </summary>
  public event ControlEventHandler BandAdded
  {
    add
    {
      lock (this._threadLock)
        this._bandAdded += value;
    }
    remove
    {
      lock (this._threadLock)
        this._bandAdded -= value;
    }
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void OnBand_VisibleChanged(object sender, EventArgs e)
  {
    if (sender is NavigationBand navigationBand && navigationBand.Button != null)
      navigationBand.Button.Visible = navigationBand.Visible;
    this.OnLayout(new LayoutEventArgs((Control) this, "Band.Visible"));
    this.Invalidate();
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void On_bands_ItemAdded(object sender, ChildCollectionEventArgs e)
  {
    this.AddBand(e.Item as NavigationBand);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void On_bands_ItemRemoved(object sender, ChildCollectionEventArgs e)
  {
    this.RemoveBand(e.Item as NavigationBand);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void On_button_Click(object sender, EventArgs e)
  {
    if (!(sender is NavigationButton navigationButton))
      return;
    foreach (NavigationBand band in (CollectionBase) this._bands)
    {
      if (band.Button == navigationButton)
      {
        this.ActiveBand = band;
        break;
      }
    }
  }

  /// <summary>
  /// 
  /// </summary>
  public event EventHandler LayoutChanged
  {
    add
    {
      lock (this._threadLock)
        this._layoutChanged += value;
    }
    remove
    {
      lock (this._threadLock)
        this._layoutChanged -= value;
    }
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="e"></param>
  protected override void OnControlAdded(ControlEventArgs e)
  {
    base.OnControlAdded(e);
    if (!(e.Control is NavigationBand) || e.Control is NavigationCollapsedBand)
      return;
    this.AddBand(e.Control as NavigationBand);
    this.OnLayout(new LayoutEventArgs((Control) this, "Bands"));
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="e"></param>
  protected override void OnControlRemoved(ControlEventArgs e)
  {
    base.OnControlRemoved(e);
    if (!(e.Control is NavigationBand))
      return;
    this.RemoveBand(e.Control as NavigationBand);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="e"></param>
  protected override void OnLayout(LayoutEventArgs e)
  {
    base.OnLayout(e);
    if (this.NaviLayout == null || this._initializing)
      return;
    this.NaviLayout.Layout((object) this, e);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="e"></param>
  protected override void OnMouseDown(MouseEventArgs e)
  {
    base.OnMouseDown(e);
    this._navLayout.Notify("MouseDown", (object) e);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="e"></param>
  protected override void OnMouseLeave(EventArgs e)
  {
    base.OnMouseLeave(e);
    this._navLayout.Notify("MouseLeave", (object) e);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="e"></param>
  protected override void OnMouseMove(MouseEventArgs e)
  {
    base.OnMouseMove(e);
    this._navLayout.Notify("MouseMove", (object) e);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="e"></param>
  protected override void OnMouseUp(MouseEventArgs e)
  {
    base.OnMouseUp(e);
    this._navLayout.Notify("MouseUp", (object) e);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="e"></param>
  protected override void OnPaint(PaintEventArgs e)
  {
    base.OnPaint(e);
    if (this._initializing)
      return;
    this._navLayout.Draw(e.Graphics);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="e"></param>
  protected override void OnPaintBackground(PaintEventArgs e)
  {
    base.OnPaintBackground(e);
    if (this._initializing)
      return;
    this._navLayout.DrawBackground(e.Graphics);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="e"></param>
  protected override void OnResize(EventArgs e)
  {
    base.OnResize(e);
    if (this._isCollapsed && this.Width != 33)
      this.Width = 33;
    this.OnLayout(new LayoutEventArgs((Control) this, "Size"));
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="band">The new band</param>
  internal void AddBand(NavigationBand band)
  {
    if (!this.Controls.Contains((Control) band))
      this.Controls.Add((Control) band);
    if (!this._bands.Contains(band))
      this._bands.SilentAdd(band);
    this.AddButton(band);
    band.VisibleChanged += new EventHandler(this.OnBand_VisibleChanged);
    if (this._bandAdded == null)
      return;
    this._bandAdded((object) this, new ControlEventArgs((Control) band));
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="band"></param>
  private void AddButton(NavigationBand band)
  {
    if (band.Button == null)
    {
      NavigationButton navigationButton = new NavigationButton();
      navigationButton.SmallImage = band.SmallImage;
      navigationButton.Image = band.Image;
      navigationButton.Text = band.Text;
      navigationButton.Click += new EventHandler(this.On_button_Click);
      band.Button = navigationButton;
    }
    if (!this.Controls.Contains((Control) band.Button))
      this.Controls.Add((Control) band.Button);
    if (this._buttons.Contains(band.Button))
      return;
    this._buttons.Add(band.Button);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="band"></param>
  private void RemoveBand(NavigationBand band)
  {
    if (band.Button != null)
    {
      band.Button.Click -= new EventHandler(this.On_button_Click);
      band.VisibleChanged -= new EventHandler(this.OnBand_VisibleChanged);
    }
    if (this.Controls.Contains((Control) band.Button))
      this.Controls.Remove((Control) band.Button);
    if (this._buttons.Contains(band.Button))
      this._buttons.Remove(band.Button);
    if (this.Controls.Contains((Control) band))
      this.Controls.Remove((Control) band);
    if (!this._bands.Contains(band))
      return;
    this._bands.Remove(band);
  }

  /// <summary>
  /// 
  /// </summary>
  public void BeginInit() => this._initializing = true;

  /// <summary>
  /// 
  /// </summary>
  public void EndInit()
  {
    this._initializing = false;
    this.Invalidate();
  }
}
