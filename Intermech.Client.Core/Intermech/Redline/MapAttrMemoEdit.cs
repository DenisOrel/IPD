
// Type: Intermech.Redline.MapAttrMemoEdit
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Client.Core.FormDesigner.Controls;
using Intermech.Map;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;


namespace Intermech.Redline;

internal sealed class MapAttrMemoEdit : AttrMemoEdit, IMapControlObject
{
  private MapControl myMapControl;
  private MapView myMapView;

  /// <summary>Clean up any resources being used.</summary>
  protected override void Dispose(bool disposing)
  {
    if (disposing)
    {
      this.myMapControl = (MapControl) null;
      this.myMapView = (MapView) null;
      this.TextChanged -= new EventHandler(this.TextBoxControl_TextChanged);
      this.KeyUp -= new KeyEventHandler(this.MapAttrMemoEdit_KeyUp);
    }
    base.Dispose(disposing);
  }

  public MapAttrMemoEdit()
  {
    this.myMapControl = (MapControl) null;
    this.myMapView = (MapView) null;
    this.TextChanged += new EventHandler(this.TextBoxControl_TextChanged);
    this.KeyUp += new KeyEventHandler(this.MapAttrMemoEdit_KeyUp);
  }

  /// <summary>для проверки окончания ввода текста</summary>
  protected override void TestEndEnter()
  {
    int endIndexChars = this.FindEndIndexChars(this.Text);
    if (endIndexChars == -1)
      return;
    this.Text = this.Text.Substring(0, endIndexChars);
    this.AcceptText();
    this.MapView.InitFocus();
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void TextBoxControl_TextChanged(object sender, EventArgs e)
  {
    if (this.MapControl == null || this.MapView == null)
      return;
    Size size = TextRenderer.MeasureText(this.Text, this.Font);
    Size view = this.MapView.ConvertDocToView(this.MapControl.Size);
    this.MapControl.Size = this.MapView.ConvertViewToDoc(new Size(Math.Max(size.Width, view.Width), Math.Max(size.Height, view.Height)));
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void MapAttrMemoEdit_KeyUp(object sender, KeyEventArgs e)
  {
    if (this.MapControl == null || this.MapView == null || !(this.MapControl.EditedObject is MapText))
      return;
    int endIndexChars = this.FindEndIndexChars(this.Text);
    if (endIndexChars == -1)
      return;
    this.Text = this.Text.Substring(0, endIndexChars);
    this.AcceptText();
    this.MapView.InitFocus();
  }

  private void AcceptText()
  {
    MapControl mapControl = this.MapControl;
    if (mapControl == null)
      return;
    if (mapControl.EditedObject is MapText editedObject)
      editedObject.DoEdit(this.MapView, editedObject.Text, this.Text);
    mapControl.DoEndEdit(this.MapView);
  }

  /// <summary>конец ввода две пустые строки</summary>
  /// <param name="str">проверяемая строка</param>
  /// <returns>положение конца строки, иначе -1</returns>
  private int FindEndIndexChars(string str)
  {
    string[] array = ((IEnumerable<string>) new string[2]
    {
      "\r\n\r\n",
      "\n\n"
    }).Where<string>((Func<string, bool>) (s =>
    {
      int num = str.LastIndexOf(s, StringComparison.Ordinal);
      return num != -1 && num == str.Length - s.Length;
    })).ToArray<string>();
    return array.Length == 0 ? -1 : str.Length - array[0].Length;
  }

  private bool HandleKey(Keys key)
  {
    switch (key)
    {
      case Keys.Tab:
      case Keys.Return:
        if (key == Keys.Return)
        {
          int endIndexChars = this.FindEndIndexChars(this.Text);
          if (endIndexChars == -1)
            return false;
          this.Text = this.Text.Substring(0, endIndexChars);
        }
        this.AcceptText();
        this.MapView.InitFocus();
        return true;
      case Keys.Escape:
        MapControl mapControl = this.MapControl;
        MapView mapView = this.MapView;
        mapControl?.DoEndEdit(this.MapView);
        mapView.InitFocus();
        return true;
      default:
        return false;
    }
  }

  protected override void OnLeave(EventArgs evt)
  {
    this.AcceptText();
    base.OnLeave(evt);
  }

  protected override bool ProcessDialogKey(Keys key)
  {
    return this.HandleKey(key) || base.ProcessDialogKey(key);
  }

  public MapControl MapControl
  {
    get => this.myMapControl;
    set
    {
      if (this.myMapControl == value)
        return;
      this.myMapControl = value;
      if (value == null || !(value.EditedObject is MapText editedObject))
        return;
      if (!editedObject.Multiline)
      {
        int firstLineBreak = editedObject.FindFirstLineBreak(editedObject.Text, 0);
        if (firstLineBreak >= 0)
          this.Text = editedObject.Text.Substring(0, firstLineBreak);
        else
          this.Text = editedObject.Text;
      }
      else
        this.Text = editedObject.Text;
      Font font = editedObject.Font;
      float size = font.Size;
      if (this.MapView != null)
        size *= this.MapView.DocScale;
      this.Font = editedObject.makeFont(font.Name, size, font.Style);
    }
  }

  public MapView MapView
  {
    get => this.myMapView;
    set => this.myMapView = value;
  }
}
