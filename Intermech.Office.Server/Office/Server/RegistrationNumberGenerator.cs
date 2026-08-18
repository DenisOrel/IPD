// Decompiled with JetBrains decompiler
// Type: Intermech.Office.Server.RegistrationNumberGenerator
// Assembly: Intermech.Office.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 414402D9-801C-4C77-86BA-4C6FCAC834BE
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Office.Server.dll

using Intermech.Diagnostics;
using Intermech.Interfaces;
using Intermech.Kernel;
using Intermech.Office.Interfaces;
using System;

#nullable disable
namespace Intermech.Office.Server;

internal abstract class RegistrationNumberGenerator
{
  [NotNull]
  protected IUserSession _Session;
  protected long _DocumentID;
  protected int _DocTypeID;
  protected OfficeDocumentTypes _OfficeDocumentType;
  protected long _ClassifierID;
  protected const string UnitSign = "{U}";

  protected RegistrationNumberGenerator(
    Guid sessionGuid,
    long documentID,
    int docTypeID,
    OfficeDocumentTypes type,
    long classifierID)
  {
    this._Session = UserSession.GetSessionByID(sessionGuid);
    this._DocumentID = documentID;
    this._DocTypeID = docTypeID;
    this._ClassifierID = classifierID;
    this._OfficeDocumentType = type;
  }

  [NotNull]
  protected string ReplaceClassificatorPart([NotNull] string template)
  {
    return StringFormula.ReplaceClassificatorPart(this._Session, template, this._ClassifierID, this._DocumentID);
  }

  [NotNull]
  protected string ReplaceObjectAttributePart([NotNull] string template)
  {
    return StringFormula.ReplaceObjectAttributePart(this._Session, template, this._DocumentID);
  }

  [NotNull]
  protected string ReplaceDatePart([NotNull] string template)
  {
    return StringFormula.ReplaceDatePart(this._Session, template);
  }

  [NotNull]
  protected CounterTemplate GetNumberCounterTemplate([NotNull] string template)
  {
    return StringFormula.GetNumberCounterTemplate(template);
  }

  [NotNull]
  public abstract string Generate();
}
