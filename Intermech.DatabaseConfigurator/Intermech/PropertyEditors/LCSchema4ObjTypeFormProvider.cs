// Decompiled with JetBrains decompiler
// Type: Intermech.PropertyEditors.LCSchema4ObjTypeFormProvider
// Assembly: Intermech.DatabaseConfigurator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: EA0AF6EA-EE29-4FDF-AAED-DB84FDED5E5C
// Assembly location: D:\IPS\Client\Intermech.DatabaseConfigurator.dll

using System;

#nullable disable
namespace Intermech.PropertyEditors;

public class LCSchema4ObjTypeFormProvider : ILCSchema4ObjTypeFormProvider
{
  public ITabPageForm GetForm(Guid aInstGuid) => (ITabPageForm) new LCSchema4ObjTypeForm(aInstGuid);
}
