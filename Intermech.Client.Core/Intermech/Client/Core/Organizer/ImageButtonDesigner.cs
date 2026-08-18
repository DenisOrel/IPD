
// Type: Intermech.Client.Core.Organizer.ImageButtonDesigner
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;
using System.Collections;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.Design;


namespace Intermech.Client.Core.Organizer;

/// <summary>
/// 
/// </summary>
[Serializable]
internal class ImageButtonDesigner : ControlDesigner
{
  private Image _currentImg;

  /// <summary>Выделение контрола.</summary>
  public override SelectionRules SelectionRules => ~SelectionRules.AllSizeable;

  /// <summary>Инициализация контрола.</summary>
  /// <param name="defaultValues"></param>
  public override void InitializeNewComponent(IDictionary defaultValues)
  {
    base.InitializeNewComponent(defaultValues);
    this.Control.Size = new Size(24, 24);
    this.Control.Text = string.Empty;
    if (!(this.Control is ImageButton))
      return;
    this._currentImg = (this.Control as ImageButton).Image;
  }

  /// <summary>Отрисовка контрола.</summary>
  /// <param name="pe"></param>
  protected override void OnPaintAdornments(PaintEventArgs pe)
  {
    base.OnPaintAdornments(pe);
    pe.Graphics.DrawImage(this._currentImg, 0, 0);
  }

  /// <summary>Удаление ненужных свойств.</summary>
  /// <param name="Properties">Набор свойств</param>
  protected override void PostFilterProperties(IDictionary Properties)
  {
    Properties.Remove((object) "BackColor");
    Properties.Remove((object) "BackgroundImage");
    Properties.Remove((object) "BackgroundImageLayout");
    Properties.Remove((object) "ContextMenuStrip");
    Properties.Remove((object) "ForeColor");
    Properties.Remove((object) "RightToLeft");
    Properties.Remove((object) "Text");
  }
}
