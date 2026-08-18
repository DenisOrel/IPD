
// Type: Intermech.Client.Core.FormDesigner.Controls.AttrMaskedTextEditControlDesigner
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;
using System.Collections;
using System.Drawing;
using System.Windows.Forms.Design;


namespace Intermech.Client.Core.FormDesigner.Controls;

/// <summary>
/// 
/// </summary>
[Serializable]
internal class AttrMaskedTextEditControlDesigner : ControlDesigner
{
  public override SelectionRules SelectionRules
  {
    get
    {
      return SelectionRules.Moveable | SelectionRules.Visible | SelectionRules.LeftSizeable | SelectionRules.RightSizeable;
    }
  }

  /// <summary>Инициализация контрола.</summary>
  /// <param name="defaultValues"></param>
  public override void InitializeNewComponent(IDictionary defaultValues)
  {
    base.InitializeNewComponent(defaultValues);
    this.Control.Size = new Size(this.Control.Width, 22);
  }
}
