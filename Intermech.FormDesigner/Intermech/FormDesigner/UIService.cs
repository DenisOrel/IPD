// Decompiled with JetBrains decompiler
// Type: Intermech.FormDesigner.UIService
// Assembly: Intermech.FormDesigner, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: D8D2DB66-E218-49D4-A1F2-016208B123B1
// Assembly location: D:\IPS\Client\Intermech.FormDesigner.dll
// XML documentation location: D:\IPS\Client\Intermech.FormDesigner.xml

using System;
using System.Collections;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.Design;

#nullable disable
namespace Intermech.FormDesigner;

/// <summary>
/// 
/// </summary>
internal class UIService : IUIService, IDisposable
{
  private FormDesignerControl _mainForm;
  private Hashtable _styles = new Hashtable();
  private Font _font;

  /// <summary>Конструктор.</summary>
  /// <param name="mainForm"></param>
  public UIService(FormDesignerControl mainForm)
  {
    this._mainForm = mainForm;
    this._font = new Font("Tahoma", 8.25f, FontStyle.Regular);
    this._styles.Add((object) "DialogFont", (object) this._font);
    this._styles.Add((object) "HighlightColor", (object) Color.FromArgb((int) byte.MaxValue, 251, 233));
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="component"></param>
  /// <returns></returns>
  public bool CanShowComponentEditor(object component) => false;

  /// <summary>
  /// 
  /// </summary>
  /// <returns></returns>
  public IWin32Window GetDialogOwnerWindow() => (IWin32Window) this._mainForm;

  /// <summary>
  /// 
  /// </summary>
  public void SetUIDirty()
  {
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="component"></param>
  /// <param name="parent"></param>
  /// <returns></returns>
  public bool ShowComponentEditor(object component, IWin32Window parent) => false;

  /// <summary>
  /// 
  /// </summary>
  /// <param name="form"></param>
  /// <returns></returns>
  public DialogResult ShowDialog(Form form) => form.ShowDialog((IWin32Window) this._mainForm);

  /// <summary>
  /// 
  /// </summary>
  /// <param name="ex"></param>
  /// <param name="message"></param>
  public void ShowError(Exception ex, string message)
  {
    int num = (int) MessageBox.Show((IWin32Window) this._mainForm, $"Piped error: {message}{Environment.NewLine}{Environment.NewLine}{ex.ToString()}");
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="ex"></param>
  public void ShowError(Exception ex)
  {
    int num = (int) MessageBox.Show((IWin32Window) this._mainForm, "Piped error: " + ex.ToString());
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="message"></param>
  public void ShowError(string message)
  {
    int num = (int) MessageBox.Show((IWin32Window) this._mainForm, "Piped error: " + message);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="message"></param>
  /// <param name="caption"></param>
  /// <param name="buttons"></param>
  /// <returns></returns>
  public DialogResult ShowMessage(string message, string caption, MessageBoxButtons buttons)
  {
    return MessageBox.Show((IWin32Window) this._mainForm, message, caption, buttons, MessageBoxIcon.Asterisk);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="message"></param>
  /// <param name="caption"></param>
  public void ShowMessage(string message, string caption)
  {
    int num = (int) MessageBox.Show((IWin32Window) this._mainForm, message, caption);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="message"></param>
  public void ShowMessage(string message)
  {
    int num = (int) MessageBox.Show((IWin32Window) this._mainForm, message);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="toolWindow"></param>
  /// <returns></returns>
  public bool ShowToolWindow(Guid toolWindow) => false;

  /// <summary>
  /// 
  /// </summary>
  public IDictionary Styles => (IDictionary) this._styles;

  /// <summary>
  /// 
  /// </summary>
  public void Dispose()
  {
    if (this._font == null)
      return;
    this._font.Dispose();
    this._font = (Font) null;
  }
}
