
// Type: MWControls.EditorTextDir
// Assembly: Intermech.Controls, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B2BFE6FF-0AA3-422C-A374-1A460CB041DD
:\IPS\Client\Intermech.Controls.dll
// XML documentation location: D:\IPS\Client\Intermech.Controls.xml

using MWCommon;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Windows.Forms.Design;


namespace MWControls;

/// <summary>
/// EditorTextDir is used in conjunction with the EditorTextDirUI Control.
/// </summary>
public class EditorTextDir : UITypeEditor
{
  private PropertyDescriptor pd;
  private object oInstance;
  private IWindowsFormsEditorService iwfes;

  /// <summary>
  /// This enables the button for the dropdown to appear in the properties window.
  /// </summary>
  /// <param name="itdc">Standard ITypeDescriptorContext object.</param>
  /// <returns>The desired UITypeEditorEditStyle (in a DropDown).</returns>
  public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext itdc)
  {
    return itdc != null && itdc.Instance != null ? UITypeEditorEditStyle.DropDown : base.GetEditStyle(itdc);
  }

  /// <summary>
  /// This takes care of the actual value-change of the property.
  /// </summary>
  /// <param name="itdc">Standard ITypeDescriptorContext object.</param>
  /// <param name="isp">Standard IServiceProvider object.</param>
  /// <param name="value">The value as an object.</param>
  /// <returns>The new value as an object.</returns>
  public override object EditValue(ITypeDescriptorContext itdc, System.IServiceProvider isp, object value)
  {
    if (itdc != null && itdc.Instance != null && isp != null)
    {
      this.iwfes = (IWindowsFormsEditorService) isp.GetService(typeof (IWindowsFormsEditorService));
      if (this.iwfes != null)
      {
        if (value is TextDir)
        {
          int num = (int) itdc.PropertyDescriptor.GetValue(itdc.Instance);
          this.pd = itdc.PropertyDescriptor;
          this.oInstance = itdc.Instance;
        }
        EditorTextDirUI editorTextDirUi = new EditorTextDirUI();
        editorTextDirUi.IWFES = this.iwfes;
        editorTextDirUi.ITDC = itdc;
        editorTextDirUi.TextDir = (TextDir) value;
        editorTextDirUi.TextDirChanged += new EditorTextDirUI.TextDirEventHandler(this.ValueChanged);
        this.iwfes.DropDownControl((Control) editorTextDirUi);
        value = (object) editorTextDirUi.TextDir;
      }
    }
    return value;
  }

  /// <summary>True if Custom Painting or false otherwise.</summary>
  /// <param name="itdc">Standard ITypeDescriptorContext object.</param>
  /// <returns>True if Custom Painting or false otherwise.</returns>
  public override bool GetPaintValueSupported(ITypeDescriptorContext itdc) => true;

  /// <summary>
  /// Paint the value in Visual Studio's (or wherever it is used) Property Window.
  /// </summary>
  /// <param name="e">Standard PaintValueEventArgs object.</param>
  public override void PaintValue(PaintValueEventArgs e)
  {
    GraphicsState gstate = e.Graphics.Save();
    TextDir td = (TextDir) e.Value;
    StringFormat genericDefault = StringFormat.GenericDefault;
    genericDefault.Alignment = StringAlignment.Center;
    genericDefault.LineAlignment = StringAlignment.Center;
    switch (td)
    {
      case TextDir.UpsideDown:
        e.Graphics.RotateTransform(180f);
        Graphics graphics = e.Graphics;
        Rectangle bounds = e.Bounds;
        double dx = (double) -bounds.Width;
        bounds = e.Bounds;
        double dy = (double) -bounds.Height;
        graphics.TranslateTransform((float) dx, (float) dy);
        break;
      case TextDir.Left:
        e.Graphics.RotateTransform(270f);
        e.Graphics.TranslateTransform((float) -e.Bounds.Height, 0.0f);
        break;
      case TextDir.Right:
        e.Graphics.RotateTransform(90f);
        e.Graphics.TranslateTransform(0.0f, (float) -e.Bounds.Width);
        break;
    }
    using (SolidBrush solidBrush = new SolidBrush(Color.Black))
      e.Graphics.DrawString("A", new Font("Arial", 8f), (Brush) solidBrush, (RectangleF) this.GetModifiedClientRectangle(td, e.Bounds), genericDefault);
    e.Graphics.Restore(gstate);
    base.PaintValue(e);
  }

  /// <summary>
  /// Standard ValueChanged EventHandler for EditorTextDirUI etdui.
  /// </summary>
  /// <param name="sender">Standard sender object.</param>
  /// <param name="e">Standard TextDirEventArgs object.</param>
  private void ValueChanged(object sender, TextDirEventArgs e)
  {
    if (this.pd == null || this.oInstance == null)
      return;
    this.pd.SetValue(this.oInstance, (object) e.NewTextDir);
  }

  /// <summary>
  /// Gets a Rectangle that is sized for being used to paint the value properly in Visual Studio's
  /// 	(or wherever it is used) Property Window.
  /// </summary>
  /// <param name="td">TextDir to base the Rectangle size and position on.</param>
  /// <param name="rct">Starting Rectangle.</param>
  /// <returns></returns>
  private Rectangle GetModifiedClientRectangle(TextDir td, Rectangle rct)
  {
    switch (td)
    {
      case TextDir.Normal:
        return new Rectangle(rct.X + 1, rct.Y, rct.Width, rct.Height);
      case TextDir.UpsideDown:
        return new Rectangle(rct.X - 2, rct.Y - 1, rct.Width, rct.Height);
      case TextDir.Left:
        return new Rectangle(rct.X - 5, rct.Y + 4, rct.Width, rct.Height);
      case TextDir.Right:
        return new Rectangle(rct.X - 2, rct.Y + 1, rct.Width, rct.Height);
      default:
        return rct;
    }
  }
}
