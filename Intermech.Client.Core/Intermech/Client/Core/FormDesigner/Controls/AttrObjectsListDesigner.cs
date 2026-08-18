
// Type: Intermech.Client.Core.FormDesigner.Controls.AttrObjectsListDesigner
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;
using System.Collections;
using System.Drawing;
using System.Windows.Forms.Design;


namespace Intermech.Client.Core.FormDesigner.Controls;

[Serializable]
internal class AttrObjectsListDesigner : ControlDesigner
{
  /// <summary>Инициализация контрола.</summary>
  /// <param name="defaultValues"></param>
  public override void InitializeNewComponent(IDictionary defaultValues)
  {
    base.InitializeNewComponent(defaultValues);
    this.Control.Size = new Size(200, 150);
  }
}
