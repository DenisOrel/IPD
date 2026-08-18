
// Type: Intermech.Controls.TabControlAdvanced
// Assembly: Intermech.Controls, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B2BFE6FF-0AA3-422C-A374-1A460CB041DD
:\IPS\Client\Intermech.Controls.dll
// XML documentation location: D:\IPS\Client\Intermech.Controls.xml

using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;


namespace Intermech.Controls;

/// <summary> Более продвинутый TabControl </summary>
public class TabControlAdvanced : TabControl
{
  private TabControlAdvanced.TCITEM_T CreateTCITEM(TabPage tabPage)
  {
    TabControlAdvanced.TCITEM_T tcitem = new TabControlAdvanced.TCITEM_T();
    tcitem.mask = 0;
    tcitem.pszText = (string) null;
    tcitem.cchTextMax = 0;
    tcitem.lParam = IntPtr.Zero;
    string text = tabPage.Text;
    if (text != null)
    {
      tcitem.mask |= 1;
      tcitem.pszText = text;
      tcitem.cchTextMax = 0;
    }
    tcitem.iImage = 0;
    return tcitem;
  }

  [DllImport("user32.dll", CharSet = CharSet.Ansi)]
  private static extern IntPtr SendMessage(
    HandleRef hWnd,
    int msg,
    int wParam,
    TabControlAdvanced.TCITEM_T lParam);

  [DllImport("user32.dll", CharSet = CharSet.Ansi)]
  private static extern IntPtr SendMessage(HandleRef hWnd, int msg, IntPtr wParam, IntPtr lParam);

  public void ReinitAmpersant()
  {
    int index = 0;
    foreach (TabPage tabPage in this.TabPages)
    {
      this.ReinitAmpersant(tabPage, index);
      ++index;
    }
  }

  private void ReinitAmpersant(TabPage tabPage, int index)
  {
    if (!this.IsHandleCreated)
      this.CreateHandle();
    if (this.IsHandleCreated)
    {
      TabControlAdvanced.TCITEM_T tcitem = this.CreateTCITEM(tabPage);
      TabControlAdvanced.SendMessage(new HandleRef((object) this, this.Handle), 4870, index == -1 ? this.TabPages.IndexOf(tabPage) : index, tcitem);
    }
    if (this.DesignMode && this.IsHandleCreated)
      TabControlAdvanced.SendMessage(new HandleRef((object) this, this.Handle), 4876, (IntPtr) (index == -1 ? this.TabPages.IndexOf(tabPage) : index), IntPtr.Zero);
    ++index;
  }

  /// <summary> Обработка нажатия клавиш быстрого доступа </summary>
  /// <param name="charCode"> Код клавиши быстрого доступа </param>
  /// <returns> true if the character was processed as a mnemonic by the control; otherwise, false.  </returns>
  protected override bool ProcessMnemonic(char charCode)
  {
    foreach (TabPage tabPage in this.TabPages)
    {
      if (Control.IsMnemonic(charCode, tabPage.Text))
      {
        this.SelectedTab = tabPage;
        return true;
      }
    }
    return base.ProcessMnemonic(charCode);
  }

  /// <summary>Перекрытие метода вызова оконной процедуры</summary>
  /// <param name="m"></param>
  protected override void WndProc(ref Message m)
  {
    if (!this.ShowTabHeaders && m.Msg == 4904 && !this.DesignMode)
      m.Result = (IntPtr) 1;
    else
      base.WndProc(ref m);
  }

  /// <summary>Показать/скрыть заголовки табов</summary>
  public bool ShowTabHeaders { get; set; } = true;

  [StructLayout(LayoutKind.Sequential)]
  private class TCITEM_T
  {
    public int mask;
    public int dwState;
    public int dwStateMask;
    public string pszText;
    public int cchTextMax;
    public int iImage;
    public IntPtr lParam;
  }
}
