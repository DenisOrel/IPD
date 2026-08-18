// Decompiled with JetBrains decompiler
// Type: Intermech.Office.Server.PrivateRegistrationNumberGenerator
// Assembly: Intermech.Office.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 414402D9-801C-4C77-86BA-4C6FCAC834BE
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Office.Server.dll

using Intermech.Diagnostics;
using Intermech.Interfaces;
using Intermech.Office.Interfaces;
using System;

#nullable disable
namespace Intermech.Office.Server;

internal sealed class PrivateRegistrationNumberGenerator : RegistrationNumberGenerator
{
  private readonly long _unitID;

  public PrivateRegistrationNumberGenerator(
    Guid sessionGuid,
    long documentID,
    int docTypeID,
    OfficeDocumentTypes type,
    long classifierID,
    long unitID)
    : base(sessionGuid, documentID, docTypeID, type, classifierID)
  {
    this._unitID = unitID;
  }

  [NotNull]
  public override string Generate()
  {
    RegNumberSettings template1 = RegistrationNumberHelper.GetTemplate(this._Session, this._DocTypeID, this._OfficeDocumentType, this._unitID);
    if ((template1 != null ? (!template1.AutoGenerateRegNumber ? 1 : 0) : 1) != 0 || template1.Template == string.Empty)
      return string.Empty;
    string template2 = this.ReplaceClassificatorPart(template1.Template);
    CounterTemplate numberCounterTemplate = this.GetNumberCounterTemplate(template1.Template);
    if (numberCounterTemplate.ReplaceValue != string.Empty)
    {
      int num = NumberCounter.NextPrivate(this._DocTypeID, this._OfficeDocumentType, template1.CountResetType, template1.CountWithinType, this._unitID, numberCounterTemplate.StartValue, numberCounterTemplate.Increment);
      template2 = template2.Replace(numberCounterTemplate.ReplaceValue, num.ToString(numberCounterTemplate.Template));
    }
    return this.ReplaceDatePart(this.ReplaceObjectAttributePart(template2));
  }
}
