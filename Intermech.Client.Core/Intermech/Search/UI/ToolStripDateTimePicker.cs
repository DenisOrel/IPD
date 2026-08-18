
// Type: Intermech.Search.UI.ToolStripDateTimePicker
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.Design;


namespace Intermech.Search.UI;

[ToolStripItemDesignerAvailability(ToolStripItemDesignerAvailability.ToolStrip)]
public sealed class ToolStripDateTimePicker : ToolStripControlHost
{
  private const int MinPreferredSize = 75;
  private static readonly Size DefaultSizeConstant = new Size(100, 22);

  public ToolStripDateTimePicker()
    : base((Control) new ToolStripDateTimePicker.ToolStripDateTimePickerControl())
  {
    (this.Control as ToolStripDateTimePicker.ToolStripDateTimePickerControl).Owner = this;
  }

  public ToolStripDateTimePicker(string name)
    : this()
  {
    this.Name = name;
  }

  [EditorBrowsable(EditorBrowsableState.Never)]
  public ToolStripDateTimePicker(Control control)
    : base(control)
  {
    throw new NotSupportedException();
  }

  public DateTimePicker DateTimePicker => this.Control as DateTimePicker;

  protected override Size DefaultSize => ToolStripDateTimePicker.DefaultSizeConstant;

  public override Size GetPreferredSize(Size constrainingSize)
  {
    Size preferredSize = base.GetPreferredSize(constrainingSize);
    preferredSize.Width = Math.Max(preferredSize.Width, 75);
    return preferredSize;
  }

  private sealed class ToolStripDateTimePickerControl : DateTimePicker
  {
    public ToolStripDateTimePicker Owner { get; set; }

    public override string Text
    {
      get => base.Text;
      set
      {
        try
        {
          base.Text = value;
        }
        catch (Exception ex)
        {
        }
      }
    }
  }
}
