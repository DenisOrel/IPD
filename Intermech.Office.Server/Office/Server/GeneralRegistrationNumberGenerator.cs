// Decompiled with JetBrains decompiler
// Type: Intermech.Office.Server.GeneralRegistrationNumberGenerator
// Assembly: Intermech.Office.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 414402D9-801C-4C77-86BA-4C6FCAC834BE
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Office.Server.dll

using Intermech.Diagnostics;
using Intermech.Interfaces;
using Intermech.Office.Interfaces;
using System;
using System.Text.RegularExpressions;

#nullable disable
namespace Intermech.Office.Server;

internal class GeneralRegistrationNumberGenerator(
  Guid sessionGuid,
  long documentID,
  int docTypeID,
  OfficeDocumentTypes type,
  long classifierID) : RegistrationNumberGenerator(sessionGuid, documentID, docTypeID, type, classifierID)
{
  [NotNull]
  public override string Generate()
  {
    RegNumberSettings template1 = RegistrationNumberHelper.GetTemplate(this._Session, this._DocTypeID, this._OfficeDocumentType);
    if (template1 == null || template1.Template == string.Empty)
      return string.Empty;
    string template2 = this.ReplaceClassificatorPart(template1.Template);
    long num1 = 0;
    if (template1.Template.ToUpper().IndexOf("{U}", StringComparison.Ordinal) >= 0 && template1.CountWithinUnit)
    {
      num1 = this._Session.GetCustomService<IOfficeRegistrationService>().GetUserUnit(this._Session.UserID);
      if (num1 == 0L)
        throw new Exception(Localization.GetString("Office.Server_10"));
      if (template1.Template.ToUpper().IndexOf("{U}", StringComparison.Ordinal) >= 0)
      {
        Match match = new Regex("\\{U\\[(?<attr>[\\w\\W]{1,})\\]\\}").Match(template1.Template);
        string anAttributeName = match.Groups["attr"].Value;
        string newValue = string.Empty;
        if (anAttributeName != string.Empty)
        {
          IDBAttribute attributeById = this._Session.GetObject(num1).GetAttributeByID((this._Session.GetAttributeType(anAttributeName, false) ?? throw new Exception(Localization.GetString("Office.Server_11", (object) anAttributeName))).AttributeID);
          if (attributeById != null)
            newValue = attributeById.AsString;
          template2 = template2.Replace(match.Value, newValue);
        }
      }
    }
    CounterTemplate numberCounterTemplate = this.GetNumberCounterTemplate(template1.Template);
    if (numberCounterTemplate.ReplaceValue != string.Empty)
    {
      int num2 = NumberCounter.Next(this._DocTypeID, this._OfficeDocumentType, template1.CountResetType, template1.CountWithinType, template1.CountWithinUnit, num1, numberCounterTemplate.StartValue, numberCounterTemplate.Increment);
      template2 = template2.Replace(numberCounterTemplate.ReplaceValue, num2.ToString(numberCounterTemplate.Template));
    }
    return this.ReplaceDatePart(this.ReplaceObjectAttributePart(template2));
  }
}
