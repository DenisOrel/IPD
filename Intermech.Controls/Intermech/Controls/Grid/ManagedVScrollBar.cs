
// Type: Intermech.Controls.Grid.ManagedVScrollBar
// Assembly: Intermech.Controls, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B2BFE6FF-0AA3-422C-A374-1A460CB041DD
:\IPS\Client\Intermech.Controls.dll
// XML documentation location: D:\IPS\Client\Intermech.Controls.xml

using System;
using System.Windows.Forms;


namespace Intermech.Controls.Grid;

internal class ManagedVScrollBar : VScrollBar
{
  public ManagedVScrollBar()
  {
    this.TabStop = false;
    this.GotFocus += new EventHandler(this.ReflectFocus);
  }

  public void ReflectFocus(object source, EventArgs e) => this.Parent.Focus();

  private void InitializeComponent()
  {
  }

  public int mTop
  {
    set
    {
      if (this.Top == value)
        return;
      this.Top = value;
    }
  }

  public int mLeft
  {
    set
    {
      if (value == this.Left)
        return;
      this.Left = value;
    }
  }

  public int mWidth
  {
    get => !this.Visible ? 0 : this.Width;
    set
    {
      if (this.Width == value)
        return;
      this.Width = value;
    }
  }

  public int mHeight
  {
    get => !this.Visible ? 0 : this.Height;
    set
    {
      if (this.Height == value)
        return;
      this.Height = value;
    }
  }

  public bool mVisible
  {
    set
    {
      if (this.Visible == value)
        return;
      this.Visible = value;
    }
  }

  public int mSmallChange
  {
    set
    {
      if (this.SmallChange == value)
        return;
      this.SmallChange = value;
    }
  }

  public int mLargeChange
  {
    set
    {
      if (this.LargeChange == value)
        return;
      this.LargeChange = value;
    }
  }

  public int mMaximum
  {
    set
    {
      if (this.Maximum == value)
        return;
      this.Maximum = value;
    }
  }
}
