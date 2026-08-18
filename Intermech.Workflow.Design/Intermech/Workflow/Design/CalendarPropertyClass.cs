// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Design.CalendarPropertyClass
// Assembly: Intermech.Workflow.Design, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: AF8177C8-0B57-4C67-8EA5-DF33FBCB2FBD
// Assembly location: D:\IPS\Client\Intermech.Workflow.Design.dll
// XML documentation location: D:\IPS\Client\Intermech.Workflow.Design.xml

using Intermech.Interfaces.Workflow;
using Intermech.PropertyEditors;
using System.ComponentModel;
using System.Drawing.Design;

#nullable disable
namespace Intermech.Workflow.Design;

/// <summary>Класс шаблонов процессов для PropertyGrid</summary>
[Editor(typeof (CalendarEditor), typeof (UITypeEditor))]
public class CalendarPropertyClass : ObjectPropertyClass
{
  public CalendarPropertyClass(long aObjectID)
    : base(aObjectID)
  {
  }

  public CalendarPropertyClass(long aObjectID, string aCaption)
    : base(aObjectID, aCaption)
  {
  }

  public override string ToString()
  {
    string str = base.ToString();
    if (this.ObjectID == 0L)
      str = LocalizationHolder.GetString("EmptyMsg", false);
    return str;
  }
}
