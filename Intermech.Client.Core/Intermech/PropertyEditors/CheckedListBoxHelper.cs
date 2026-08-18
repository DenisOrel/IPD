
// Type: Intermech.PropertyEditors.CheckedListBoxHelper
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;
using System.Windows.Forms;


namespace Intermech.PropertyEditors;

internal class CheckedListBoxHelper : IDisposable
{
  private CheckedListBox _clb;
  private bool _updating;
  private bool _valid;

  public CheckedListBoxHelper(CheckedListBox listBox, bool handleCheck)
  {
    this._clb = listBox;
    if (handleCheck)
      this._clb.ItemCheck += new ItemCheckEventHandler(this._clb_ItemCheck);
    this._updating = false;
    this._clb.KeyPress += new KeyPressEventHandler(this._clb_KeyPress);
    this._valid = true;
  }

  private void _clb_ItemCheck(object sender, ItemCheckEventArgs e)
  {
    if (this._updating)
      return;
    try
    {
      this._updating = true;
      int count = this._clb.Items.Count;
      if (e.Index == 0)
      {
        for (int index = 1; index < count; ++index)
          this._clb.SetItemChecked(index, e.NewValue != CheckState.Checked);
      }
      else
      {
        if (e.Index <= 0)
          return;
        if (e.NewValue == CheckState.Checked)
        {
          this._clb.SetItemChecked(0, false);
        }
        else
        {
          bool flag = false;
          for (int index = 1; index < count; ++index)
          {
            if (index != e.Index)
              flag = flag || this._clb.GetItemChecked(index);
          }
          if (flag)
            return;
          this._clb.SetItemChecked(0, true);
        }
      }
    }
    finally
    {
      this._updating = false;
    }
  }

  public void Dispose()
  {
    this._updating = true;
    this._clb.ItemCheck -= new ItemCheckEventHandler(this._clb_ItemCheck);
    this._clb.KeyPress -= new KeyPressEventHandler(this._clb_KeyPress);
  }

  public bool Break => !this._valid;

  private void _clb_KeyPress(object sender, KeyPressEventArgs e)
  {
    if (e.KeyChar != '\u001B')
      return;
    this._valid = false;
  }
}
