
// Type: Intermech.PropertyEditors.TemplatePropertyClass
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System.ComponentModel;
using System.Drawing.Design;


namespace Intermech.PropertyEditors;

/// <summary>Класс шаблонов процессов для PropertyGrid</summary>
[Editor(typeof (TemplateEditor), typeof (UITypeEditor))]
public class TemplatePropertyClass : ObjectPropertyClass
{
  public TemplatePropertyClass(long aObjectID)
    : base(aObjectID)
  {
  }

  public TemplatePropertyClass(long aObjectID, string aCaption)
    : base(aObjectID, aCaption)
  {
  }
}
