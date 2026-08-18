// Decompiled with JetBrains decompiler
// Type: Intermech.PropertyEditors.LCStepPropertyEditor
// Assembly: Intermech.DatabaseConfigurator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: EA0AF6EA-EE29-4FDF-AAED-DB84FDED5E5C
// Assembly location: D:\IPS\Client\Intermech.DatabaseConfigurator.dll

using Intermech.Holders;

#nullable disable
namespace Intermech.PropertyEditors;

public class LCStepPropertyEditor(EventsHolder.GetListDelegate getListDelegate) : DropDownListEditor(getListDelegate)
{
  public LCStepPropertyEditor()
    : this((EventsHolder.GetListDelegate) null)
  {
  }
}
