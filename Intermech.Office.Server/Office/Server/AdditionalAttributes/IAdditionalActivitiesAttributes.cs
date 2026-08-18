// Decompiled with JetBrains decompiler
// Type: Intermech.Office.Server.AdditionalAttributes.IAdditionalActivitiesAttributes
// Assembly: Intermech.Office.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 414402D9-801C-4C77-86BA-4C6FCAC834BE
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Office.Server.dll

using Intermech.Diagnostics;
using Intermech.Interfaces;
using Intermech.Interfaces.Workflow;

#nullable disable
namespace Intermech.Office.Server.AdditionalAttributes;

internal interface IAdditionalActivitiesAttributes
{
  void CreateAttributes([NotNull] IUserSession session, [NotNull] IProcess process, [NotNull] ResolutionProcessExecuteArgs args);
}
