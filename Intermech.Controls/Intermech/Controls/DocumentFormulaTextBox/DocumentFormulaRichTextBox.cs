
// Type: Intermech.Controls.DocumentFormulaTextBox.DocumentFormulaRichTextBox
// Assembly: Intermech.Controls, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B2BFE6FF-0AA3-422C-A374-1A460CB041DD
:\IPS\Client\Intermech.Controls.dll
// XML documentation location: D:\IPS\Client\Intermech.Controls.xml

using System.Windows.Forms;


namespace Intermech.Controls.DocumentFormulaTextBox;

public class DocumentFormulaRichTextBox : RichTextBox
{
  public DocumentFormulaRichTextBox()
  {
    if (this.DesignMode)
      return;
    this.ContextMenuStrip = (ContextMenuStrip) new DocumentFormulaToolStrip((TextBoxBase) this);
  }
}
