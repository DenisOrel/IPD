// Decompiled with JetBrains decompiler
// Type: Intermech.Office.Server.AdditionalAttributes.IsControlAdditionalAttribute
// Assembly: Intermech.Office.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 414402D9-801C-4C77-86BA-4C6FCAC834BE
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Office.Server.dll

using Intermech.Diagnostics;
using Intermech.Interfaces;
using Intermech.Office.Interfaces;

#nullable disable
namespace Intermech.Office.Server.AdditionalAttributes;

internal class IsControlAdditionalAttribute : AdditionalActivitiesAttributes
{
  private readonly bool _control;

  public IsControlAdditionalAttribute(bool control) => this._control = control;

  protected override void AddValue([NotNull] IDBAttribute attribute)
  {
    attribute.AsBoolean = this._control;
  }

  protected override int AdditionalAttribute => OfficeConsts.AttrIsControlResolutionID;
}
