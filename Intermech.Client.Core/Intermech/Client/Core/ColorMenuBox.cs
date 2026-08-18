
// Type: Intermech.Client.Core.ColorMenuBox
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Bars;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Localization;
using System;
using System.Collections;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;


namespace Intermech.Client.Core;

public class ColorMenuBox : IDisposable
{
  /// <summary>ссылка на ColorMenu</summary>
  protected ColorMenu _menu;
  /// <summary>ссылка на цвет</summary>
  protected Rclass<Color> _color;
  private string _nameImage;
  private bool isColorEmpty = true;

  /// <summary>Освободить ресурсы</summary>
  public void Dispose()
  {
    if (this._menu != null)
    {
      using (this._menu.Image)
        this._menu.Image = (Image) null;
      this._menu.Items.Clear();
      this._menu.BeforePopup -= new MenuItemBase.BeforePopupEventHandler(this.ColorMenu_BeforePopup);
      this._menu = (ColorMenu) null;
    }
    if (this._color == null)
      return;
    this._color.ValueChanged -= new EventHandler<EventArgs<Color>>(this.Color_ValueChanged);
    this._color = (Rclass<Color>) null;
  }

  /// <summary>Получает копию Image с указанным именем из коллекции.</summary>
  /// <param name="name">Требуемое имя</param>
  /// <returns>копия Image, надо удалять</returns>
  private Image NamedImage()
  {
    INamedImageList service = ServicesManager.GetService(typeof (INamedImageList)) as INamedImageList;
    return service.ImageList.Images[service.ImageIndex(this._nameImage)].Clone() as Image;
  }

  /// <summary>Инициализация изменения цвета линии</summary>
  /// <param name="toolBarPen">ссылка на ColorMenu</param>
  /// <param name="varcolor">ссылка на цвет</param>
  public void Initialize_Text(Intermech.Bars.ToolBar toolBar, Rclass<Color> varcolor)
  {
    if (toolBar == null)
      throw new ArgumentNullException(nameof (toolBar));
    this._color = varcolor != null ? varcolor : throw new ArgumentNullException(nameof (varcolor));
    this._nameImage = "imgFontColor";
    this._menu = new ColorMenu();
    this._menu.Image = this.NamedImage();
    this._menu.Text = string.Empty;
    this._menu.ToolTipText = LocalizationHolder.rm.GetString("Client.Core_334");
    this._menu.CommandName = "RedFontColor";
    this._menu.BeginGroup = true;
    this._menu.BeforePopup += new MenuItemBase.BeforePopupEventHandler(this.ColorMenu_BeforePopup);
    toolBar.Items.Add((ToolbarItemBase) this._menu);
    \u003C\u003Ef__AnonymousType1<int, string>[] dataArray = new \u003C\u003Ef__AnonymousType1<int, string>[40]
    {
      new{ Cod = 0, Name = "Client.Core_335" },
      new{ Cod = 10040064, Name = "Client.Core_336" },
      new{ Cod = 3355392, Name = "Client.Core_337" },
      new{ Cod = 13056, Name = "Client.Core_338" },
      new{ Cod = 13158, Name = "Client.Core_339" },
      new{ Cod = 128 /*0x80*/, Name = "Client.Core_340" },
      new{ Cod = 3355545, Name = "Client.Core_341" },
      new{ Cod = 3355443 /*0x333333*/, Name = "Client.Core_342" },
      new{ Cod = 8388608 /*0x800000*/, Name = "Client.Core_343" },
      new{ Cod = 16737792, Name = "Client.Core_344" },
      new{ Cod = 8421376 /*0x808000*/, Name = "Client.Core_345" },
      new{ Cod = 32768 /*0x8000*/, Name = "Client.Core_346" },
      new{ Cod = 32896, Name = "Client.Core_347" },
      new{ Cod = (int) byte.MaxValue, Name = "Client.Core_348" },
      new{ Cod = 6710937, Name = "Client.Core_349" },
      new{ Cod = 8421504 /*0x808080*/, Name = "Client.Core_350" },
      new{ Cod = 16711680 /*0xFF0000*/, Name = "Client.Core_351" },
      new{ Cod = 16750848, Name = "Client.Core_352" },
      new{ Cod = 10079232, Name = "Client.Core_353" },
      new{ Cod = 3381606, Name = "Client.Core_354" },
      new{ Cod = 3394764, Name = "Client.Core_355" },
      new{ Cod = 3368703, Name = "Client.Core_356" },
      new{ Cod = 8388736 /*0x800080*/, Name = "Client.Core_357" },
      new{ Cod = 10066329 /*0x999999*/, Name = "Client.Core_358" },
      new{ Cod = 16711935, Name = "Client.Core_359" },
      new{ Cod = 16763904, Name = "Client.Core_360" },
      new{ Cod = 16776960, Name = "Client.Core_361" },
      new{ Cod = 65280, Name = "Client.Core_362" },
      new{ Cod = (int) ushort.MaxValue, Name = "Client.Core_363" },
      new{ Cod = 52479, Name = "Client.Core_364" },
      new{ Cod = 10040166, Name = "Client.Core_365" },
      new{ Cod = 12632256 /*0xC0C0C0*/, Name = "Client.Core_366" },
      new{ Cod = 16751052, Name = "Client.Core_367" },
      new{ Cod = 16764057, Name = "Client.Core_368" },
      new{ Cod = 16777113, Name = "Client.Core_369" },
      new{ Cod = 13434828, Name = "Client.Core_370" },
      new{ Cod = 13434879, Name = "Client.Core_371" },
      new{ Cod = 10079487, Name = "Client.Core_372" },
      new{ Cod = 13408767, Name = "Client.Core_373" },
      new{ Cod = 16777215 /*0xFFFFFF*/, Name = "Client.Core_374" }
    };
    int num = 16 /*0x10*/;
    foreach (var data in dataArray)
    {
      ColorMenuItem colorMenuItem = new ColorMenuItem();
      colorMenuItem.Text = colorMenuItem.ToolTipText = LocalizationHolder.rm.GetString(data.Name);
      colorMenuItem.Color = Color.FromArgb((int) byte.MaxValue, Color.FromArgb(data.Cod));
      colorMenuItem.Image = (Image) new Bitmap(num, num);
      using (Graphics graphics = Graphics.FromImage(colorMenuItem.Image))
      {
        using (SolidBrush solidBrush = new SolidBrush(colorMenuItem.Color))
          graphics.FillRectangle((Brush) solidBrush, new Rectangle(0, 0, num, num));
        using (Pen pen = new Pen(Color.Black))
          graphics.DrawRectangle(pen, new Rectangle(0, 0, num, num));
      }
      this._menu.Items.Add((ToolbarItemBase) colorMenuItem);
      colorMenuItem.Click += (EventHandler) ((sender, e) => this._color.Value = (sender as ColorMenuItem).Color);
    }
    TextMenuItem textMenuItem = new TextMenuItem();
    textMenuItem.Text = LocalizationHolder.rm.GetString("Client.Core_375");
    textMenuItem.Click += new EventHandler(this.colorDialogItem_Click);
    this._color.ValueChanged += new EventHandler<EventArgs<Color>>(this.Color_ValueChanged);
    this._menu.Items.Add((ToolbarItemBase) textMenuItem);
    this.UpdateChecked(this._color.Value);
  }

  /// <summary>Инициализация изменения цвета заливки</summary>
  /// <param name="toolBar">ссылка на ColorMenu</param>
  /// <param name="varcolor">ссылка на цвет</param>
  public void Initialize_Fill(Intermech.Bars.ToolBar toolBar, Rclass<Color> varcolor)
  {
    if (toolBar == null)
      throw new ArgumentNullException(nameof (toolBar));
    this._color = varcolor != null ? varcolor : throw new ArgumentNullException(nameof (varcolor));
    this._nameImage = "imgFillColor";
    this._menu = new ColorMenu();
    this._menu.Image = this.NamedImage();
    this._menu.Text = string.Empty;
    this._menu.ToolTipText = LocalizationHolder.rm.GetString("Client.Core_334");
    this._menu.CommandName = "RedFillColor";
    this._menu.BeginGroup = true;
    this._menu.BeforePopup += new MenuItemBase.BeforePopupEventHandler(this.ColorMenu_BeforePopup);
    toolBar.Items.Add((ToolbarItemBase) this._menu);
    TextMenuItem textMenuItem1 = new TextMenuItem();
    textMenuItem1.Text = LocalizationHolder.rm.GetString("Client.Core_1616");
    textMenuItem1.Tag = (object) "noColorItem";
    this._menu.Items.Add((ToolbarItemBase) textMenuItem1);
    textMenuItem1.Click += (EventHandler) ((sender, e) => this._color.Value = Color.Empty);
    \u003C\u003Ef__AnonymousType1<int, string>[] dataArray = new \u003C\u003Ef__AnonymousType1<int, string>[64 /*0x40*/]
    {
      new{ Cod = 16777215 /*0xFFFFFF*/, Name = "Client.Core_374" },
      new
      {
        Cod = 15987699 /*0xF3F3F3*/,
        Name = "Client.Core_Grey_1"
      },
      new
      {
        Cod = 15132390 /*0xE6E6E6*/,
        Name = "Client.Core_Grey_2"
      },
      new
      {
        Cod = 14737632 /*0xE0E0E0*/,
        Name = "Client.Core_Grey_3"
      },
      new
      {
        Cod = 14277081 /*0xD9D9D9*/,
        Name = "Client.Core_Grey_4"
      },
      new
      {
        Cod = 13421772 /*0xCCCCCC*/,
        Name = "Client.Core_Grey_5"
      },
      new
      {
        Cod = 12632256 /*0xC0C0C0*/,
        Name = "Client.Core_Grey_6"
      },
      new
      {
        Cod = 11776947 /*0xB3B3B3*/,
        Name = "Client.Core_Grey_7"
      },
      new
      {
        Cod = 10921638 /*0xA6A6A6*/,
        Name = "Client.Core_Grey_8"
      },
      new
      {
        Cod = 10526880 /*0xA0A0A0*/,
        Name = "Client.Core_Grey_9"
      },
      new
      {
        Cod = 10066329 /*0x999999*/,
        Name = "Client.Core_Grey_10"
      },
      new
      {
        Cod = 9211020 /*0x8C8C8C*/,
        Name = "Client.Core_Grey_11"
      },
      new
      {
        Cod = 8421504 /*0x808080*/,
        Name = "Client.Core_Grey_12"
      },
      new
      {
        Cod = 7566195 /*0x737373*/,
        Name = "Client.Core_Grey_13"
      },
      new
      {
        Cod = 6710886 /*0x666666*/,
        Name = "Client.Core_Grey_14"
      },
      new
      {
        Cod = 6316128 /*0x606060*/,
        Name = "Client.Core_Grey_15"
      },
      new
      {
        Cod = 5855577 /*0x595959*/,
        Name = "Client.Core_Grey_16"
      },
      new
      {
        Cod = 5000268 /*0x4C4C4C*/,
        Name = "Client.Core_Grey_17"
      },
      new
      {
        Cod = 4210752 /*0x404040*/,
        Name = "Client.Core_Grey_18"
      },
      new
      {
        Cod = 3355443 /*0x333333*/,
        Name = "Client.Core_Grey_19"
      },
      new
      {
        Cod = 2500134 /*0x262626*/,
        Name = "Client.Core_Grey_20"
      },
      new
      {
        Cod = 2105376 /*0x202020*/,
        Name = "Client.Core_Grey_21"
      },
      new
      {
        Cod = 1644825 /*0x191919*/,
        Name = "Client.Core_Grey_22"
      },
      new{ Cod = 789516, Name = "Client.Core_Grey_23" },
      new{ Cod = 0, Name = "Client.Core_335" },
      new{ Cod = 10040064, Name = "Client.Core_336" },
      new{ Cod = 3355392, Name = "Client.Core_337" },
      new{ Cod = 13056, Name = "Client.Core_338" },
      new{ Cod = 13158, Name = "Client.Core_339" },
      new{ Cod = 128 /*0x80*/, Name = "Client.Core_340" },
      new{ Cod = 3355545, Name = "Client.Core_341" },
      new{ Cod = 3355443 /*0x333333*/, Name = "Client.Core_342" },
      new{ Cod = 8388608 /*0x800000*/, Name = "Client.Core_343" },
      new{ Cod = 16737792, Name = "Client.Core_344" },
      new{ Cod = 8421376 /*0x808000*/, Name = "Client.Core_345" },
      new{ Cod = 32768 /*0x8000*/, Name = "Client.Core_346" },
      new{ Cod = 32896, Name = "Client.Core_347" },
      new{ Cod = (int) byte.MaxValue, Name = "Client.Core_348" },
      new{ Cod = 6710937, Name = "Client.Core_349" },
      new{ Cod = 8421504 /*0x808080*/, Name = "Client.Core_350" },
      new{ Cod = 16711680 /*0xFF0000*/, Name = "Client.Core_351" },
      new{ Cod = 16750848, Name = "Client.Core_352" },
      new{ Cod = 10079232, Name = "Client.Core_353" },
      new{ Cod = 3381606, Name = "Client.Core_354" },
      new{ Cod = 3394764, Name = "Client.Core_355" },
      new{ Cod = 3368703, Name = "Client.Core_356" },
      new{ Cod = 8388736 /*0x800080*/, Name = "Client.Core_357" },
      new{ Cod = 10066329 /*0x999999*/, Name = "Client.Core_358" },
      new{ Cod = 16711935, Name = "Client.Core_359" },
      new{ Cod = 16763904, Name = "Client.Core_360" },
      new{ Cod = 16776960, Name = "Client.Core_361" },
      new{ Cod = 65280, Name = "Client.Core_362" },
      new{ Cod = (int) ushort.MaxValue, Name = "Client.Core_363" },
      new{ Cod = 52479, Name = "Client.Core_364" },
      new{ Cod = 10040166, Name = "Client.Core_365" },
      new{ Cod = 12632256 /*0xC0C0C0*/, Name = "Client.Core_366" },
      new{ Cod = 16751052, Name = "Client.Core_367" },
      new{ Cod = 16764057, Name = "Client.Core_368" },
      new{ Cod = 16777113, Name = "Client.Core_369" },
      new{ Cod = 13434828, Name = "Client.Core_370" },
      new{ Cod = 13434879, Name = "Client.Core_371" },
      new{ Cod = 10079487, Name = "Client.Core_372" },
      new{ Cod = 13408767, Name = "Client.Core_373" },
      new{ Cod = 16777215 /*0xFFFFFF*/, Name = "Client.Core_374" }
    };
    int num = 16 /*0x10*/;
    foreach (var data in dataArray)
    {
      ColorMenuItem colorMenuItem = new ColorMenuItem();
      colorMenuItem.Text = colorMenuItem.ToolTipText = LocalizationHolder.rm.GetString(data.Name);
      colorMenuItem.Color = Color.FromArgb((int) byte.MaxValue, Color.FromArgb(data.Cod));
      colorMenuItem.Image = (Image) new Bitmap(num, num);
      using (Graphics graphics = Graphics.FromImage(colorMenuItem.Image))
      {
        using (SolidBrush solidBrush = new SolidBrush(colorMenuItem.Color))
          graphics.FillRectangle((Brush) solidBrush, new Rectangle(0, 0, num, num));
        using (Pen pen = new Pen(Color.Black))
          graphics.DrawRectangle(pen, new Rectangle(0, 0, num, num));
      }
      this._menu.Items.Add((ToolbarItemBase) colorMenuItem);
      colorMenuItem.Click += (EventHandler) ((sender, e) => this._color.Value = (sender as ColorMenuItem).Color);
    }
    TextMenuItem textMenuItem2 = new TextMenuItem();
    textMenuItem2.Text = LocalizationHolder.rm.GetString("Client.Core_375");
    textMenuItem2.Click += new EventHandler(this.colorDialogItem_Click);
    this._color.ValueChanged += new EventHandler<EventArgs<Color>>(this.Color_ValueChanged);
    this._menu.Items.Add((ToolbarItemBase) textMenuItem2);
    this.UpdateChecked(this._color.Value);
  }

  /// <summary>Инициализация изменения цвета линии</summary>
  /// <param name="toolBarPen">ссылка на ColorMenu</param>
  /// <param name="varcolor">ссылка на цвет</param>
  public void Initialize_Pen(Intermech.Bars.ToolBar toolBar, Rclass<Color> varcolor)
  {
    if (toolBar == null)
      throw new ArgumentNullException(nameof (toolBar));
    this._color = varcolor != null ? varcolor : throw new ArgumentNullException(nameof (varcolor));
    this._nameImage = "imgLinecolor";
    this._menu = new ColorMenu();
    this._menu.Image = this.NamedImage();
    this._menu.Text = string.Empty;
    this._menu.ToolTipText = LocalizationHolder.rm.GetString("Client.Core_334");
    this._menu.CommandName = "RedLineColor";
    this._menu.BeginGroup = true;
    this._menu.BeforePopup += new MenuItemBase.BeforePopupEventHandler(this.ColorMenu_BeforePopup);
    toolBar.Items.Add((ToolbarItemBase) this._menu);
    \u003C\u003Ef__AnonymousType1<int, string>[] dataArray = new \u003C\u003Ef__AnonymousType1<int, string>[40]
    {
      new{ Cod = 0, Name = "Client.Core_335" },
      new{ Cod = 10040064, Name = "Client.Core_336" },
      new{ Cod = 3355392, Name = "Client.Core_337" },
      new{ Cod = 13056, Name = "Client.Core_338" },
      new{ Cod = 13158, Name = "Client.Core_339" },
      new{ Cod = 128 /*0x80*/, Name = "Client.Core_340" },
      new{ Cod = 3355545, Name = "Client.Core_341" },
      new{ Cod = 3355443 /*0x333333*/, Name = "Client.Core_342" },
      new{ Cod = 8388608 /*0x800000*/, Name = "Client.Core_343" },
      new{ Cod = 16737792, Name = "Client.Core_344" },
      new{ Cod = 8421376 /*0x808000*/, Name = "Client.Core_345" },
      new{ Cod = 32768 /*0x8000*/, Name = "Client.Core_346" },
      new{ Cod = 32896, Name = "Client.Core_347" },
      new{ Cod = (int) byte.MaxValue, Name = "Client.Core_348" },
      new{ Cod = 6710937, Name = "Client.Core_349" },
      new{ Cod = 8421504 /*0x808080*/, Name = "Client.Core_350" },
      new{ Cod = 16711680 /*0xFF0000*/, Name = "Client.Core_351" },
      new{ Cod = 16750848, Name = "Client.Core_352" },
      new{ Cod = 10079232, Name = "Client.Core_353" },
      new{ Cod = 3381606, Name = "Client.Core_354" },
      new{ Cod = 3394764, Name = "Client.Core_355" },
      new{ Cod = 3368703, Name = "Client.Core_356" },
      new{ Cod = 8388736 /*0x800080*/, Name = "Client.Core_357" },
      new{ Cod = 10066329 /*0x999999*/, Name = "Client.Core_358" },
      new{ Cod = 16711935, Name = "Client.Core_359" },
      new{ Cod = 16763904, Name = "Client.Core_360" },
      new{ Cod = 16776960, Name = "Client.Core_361" },
      new{ Cod = 65280, Name = "Client.Core_362" },
      new{ Cod = (int) ushort.MaxValue, Name = "Client.Core_363" },
      new{ Cod = 52479, Name = "Client.Core_364" },
      new{ Cod = 10040166, Name = "Client.Core_365" },
      new{ Cod = 12632256 /*0xC0C0C0*/, Name = "Client.Core_366" },
      new{ Cod = 16751052, Name = "Client.Core_367" },
      new{ Cod = 16764057, Name = "Client.Core_368" },
      new{ Cod = 16777113, Name = "Client.Core_369" },
      new{ Cod = 13434828, Name = "Client.Core_370" },
      new{ Cod = 13434879, Name = "Client.Core_371" },
      new{ Cod = 10079487, Name = "Client.Core_372" },
      new{ Cod = 13408767, Name = "Client.Core_373" },
      new{ Cod = 16777215 /*0xFFFFFF*/, Name = "Client.Core_374" }
    };
    int num = 16 /*0x10*/;
    foreach (var data in dataArray)
    {
      ColorMenuItem colorMenuItem = new ColorMenuItem();
      colorMenuItem.Text = colorMenuItem.ToolTipText = LocalizationHolder.rm.GetString(data.Name);
      colorMenuItem.Color = Color.FromArgb((int) byte.MaxValue, Color.FromArgb(data.Cod));
      colorMenuItem.Image = (Image) new Bitmap(num, num);
      using (Graphics graphics = Graphics.FromImage(colorMenuItem.Image))
      {
        using (SolidBrush solidBrush = new SolidBrush(colorMenuItem.Color))
          graphics.FillRectangle((Brush) solidBrush, new Rectangle(0, 0, num, num));
        using (Pen pen = new Pen(Color.Black))
          graphics.DrawRectangle(pen, new Rectangle(0, 0, num, num));
      }
      this._menu.Items.Add((ToolbarItemBase) colorMenuItem);
      colorMenuItem.Click += (EventHandler) ((sender, e) => this._color.Value = (sender as ColorMenuItem).Color);
    }
    TextMenuItem textMenuItem = new TextMenuItem();
    textMenuItem.Text = LocalizationHolder.rm.GetString("Client.Core_375");
    textMenuItem.Click += new EventHandler(this.colorDialogItem_Click);
    this._color.ValueChanged += new EventHandler<EventArgs<Color>>(this.Color_ValueChanged);
    this._menu.Items.Add((ToolbarItemBase) textMenuItem);
    this.UpdateChecked(this._color.Value);
  }

  /// <summary></summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void colorDialogItem_Click(object sender, EventArgs e)
  {
    using (ColorDialog colorDialog = new ColorDialog())
    {
      colorDialog.Color = (Color) this._color;
      colorDialog.CustomColors = new int[1]
      {
        (int) colorDialog.Color.B << 16 /*0x10*/ | (int) colorDialog.Color.G << 8 | (int) colorDialog.Color.R
      };
      if (colorDialog.ShowDialog() != DialogResult.OK)
        return;
      this._color.Value = colorDialog.Color;
    }
  }

  private void Color_ValueChanged(object sender, EventArgs<Color> e)
  {
    if (this._menu == null)
      return;
    this.UpdateChecked(e.Value);
  }

  /// <summary>обновить отметку цвета</summary>
  /// <param name="color">новый цвет</param>
  private void UpdateChecked(Color color)
  {
    if (this._menu == null)
      return;
    bool isColorEmpty = this.isColorEmpty;
    this.isColorEmpty = color == Color.Empty;
    if (this.isColorEmpty != isColorEmpty)
    {
      using (this._menu.Image)
        this._menu.Image = this.NamedImage();
    }
    this.ChaneImageColor(color);
    ColorMenuItem colorMenuItem = (ColorMenuItem) null;
    foreach (MenuButtonItem menuButtonItem in (CollectionBase) this._menu.Items)
    {
      if (menuButtonItem.Tag as string == "noColorItem")
        menuButtonItem.Checked = color == Color.Empty;
      if (menuButtonItem is ColorMenuItem && (menuButtonItem as ColorMenuItem).Color == color)
        colorMenuItem = menuButtonItem as ColorMenuItem;
    }
    this._menu.ToolTipText = colorMenuItem != null ? colorMenuItem.ToolTipText : LocalizationHolder.rm.GetString("Client.Core_375");
    if (color == Color.Empty)
      this._menu.ToolTipText = LocalizationHolder.rm.GetString("Client.Core_1616");
    this._menu.Invalidate();
  }

  /// <summary> обновить цвет полоски в картинке</summary>
  /// <param name="color">новый цвет</param>
  private void ChaneImageColor(Color color)
  {
    Image image = this._menu.Image;
    Size size = image.Size;
    using (Graphics graphics = Graphics.FromImage(image))
    {
      if (color != Color.Empty)
      {
        using (SolidBrush solidBrush = new SolidBrush(color))
          graphics.FillRectangle((Brush) solidBrush, 0, size.Height - 4, size.Width, size.Height - 2);
      }
      else
      {
        using (SolidBrush solidBrush = new SolidBrush(Color.Blue))
          graphics.FillRectangle((Brush) solidBrush, 0, 0, size.Width, size.Height);
        using (SolidBrush solidBrush = new SolidBrush(Color.Red))
          graphics.FillPolygon((Brush) solidBrush, new Point[3]
          {
            new Point(0, 0),
            new Point(size.Width, size.Height),
            new Point(0, size.Height)
          });
        graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
        graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
        graphics.CompositingQuality = CompositingQuality.HighQuality;
        graphics.DrawString("?", new Font("Arial", 13f), Brushes.White, (RectangleF) new Rectangle(0, 0, size.Width, size.Height), new StringFormat()
        {
          Alignment = StringAlignment.Center,
          LineAlignment = StringAlignment.Center
        });
      }
    }
  }

  private void ColorMenu_BeforePopup(object sender, MenuPopupEventArgs e)
  {
    (sender as ColorMenu).ForeColor = (Color) this._color;
    foreach (MenuButtonItem menuButtonItem in (CollectionBase) this._menu.Items)
    {
      if (menuButtonItem.Tag as string == "noColorItem")
        menuButtonItem.Checked = (Color) this._color == Color.Empty;
    }
  }
}
