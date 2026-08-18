// Decompiled with JetBrains decompiler
// Type: Intermech.Pdm.Server.UpdatePDM
// Assembly: Intermech.Pdm.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: EC8EF964-D01E-4AAA-8100-7A99DC670202
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Pdm.Server.dll

using Intermech.Diagnostics;
using Intermech.Interfaces;
using Intermech.Interfaces.Configuration;
using Intermech.Interfaces.MetadataUpdates;
using Intermech.Interfaces.Server;
using Intermech.Kernel;
using System;
using System.Collections.Generic;
using System.IO;

#nullable disable
namespace Intermech.Pdm.Server;

public class UpdatePDM : IUpdatable
{
  private const string orderPointScript = "Intermech.PDM.OrderPointNew.xml";
  private const string weldingJointsScript = "Intermech.PDM.WeldingJoints.xml";

  public string[] GetUpdateScripts()
  {
    List<string> stringList = new List<string>()
    {
      "Intermech.PDM.ContextCompositionAtts.xml",
      "Intermech.PDM.SubstitutionAttrs.xml",
      "Intermech.PDM.InstanceAttrs.xml",
      "Intermech.PDM.Instances.xml",
      "Intermech.PDM.SearchSchemes.AttributeTypes.xml",
      "Intermech.PDM.SearchSchemes.ObjectTypes.xml",
      "Intermech.PdmConfigurator.LCSchema.xml",
      "Intermech.PdmConfigurator.AttributeTypes.xml",
      "Intermech.PdmConfigurator.ObjectTypes.xml",
      "Intermech.PDM.SearchSchemes.RelationTypes.xml",
      "Intermech.PdmConfigurator.Objects.xml",
      "Intermech.PDM.AttributeTypes.xml",
      "Intermech.PDM.ObjectTypes.xml",
      "Intermech.Pdm.Materials.xml",
      "Intermech.Pdm.SeriesAndDates.xml",
      "Intermech.Search.Pdm.PreciseAssemblyUnits.Updates.xml",
      "Intermech.Search.Pdm.Analogs.Updates.xml",
      "Intermech.PDM.ComponentSelection.xml",
      "Intermech.Search.Mbom.Updates.xml",
      "Intermech.Search.MSOfficeAddins.Updates.xml",
      "Intermech.PDM.WeldingJoints.xml",
      "Intermech.PDM.Requirements.xml",
      "Intermech.PDM.CompareCompositionRules.xml"
    };
    if (UpdatePDM.IsScriptExist("Intermech.PDM.OrderPointNew.xml"))
      stringList.Add("Intermech.PDM.OrderPointNew.xml");
    return stringList.ToArray();
  }

  public void BeforeExecScript(IUserSession session, string scriptName)
  {
    if (!(scriptName == "Intermech.PDM.WeldingJoints.xml"))
      return;
    this.TryRenameUserAttributeType(session, "Длина шва", "Длина шва IMBASE", "Длина шва (IMBASE)", "Длина шва (из IMBASE)");
  }

  public void AfterExecScript(IUserSession session, string scriptName)
  {
  }

  public void AfterExecAllScripts(IUserSession session)
  {
  }

  private void TryRenameUserAttributeType(
    IUserSession session,
    string attributeName,
    params string[] possibleNewNames)
  {
    IDBAttributeType attributeType = session.GetAttributeType(attributeName, false);
    if (attributeType == null)
      return;
    if (SystemGUIDs.IsSystemGUID(attributeType.GUID))
      return;
    try
    {
      this.RenameAttributeType(session, attributeType, possibleNewNames);
    }
    catch (Exception ex)
    {
      EventLogHelper service = (EventLogHelper) ApplicationServices.Container.GetService(typeof (IEventLogHelper));
      if (service == null)
        return;
      string preamble = $"Ошибка изменения имени атрибута с '{attributeName}' на '{possibleNewNames[0]}'.";
      service.AddToTrace(ExceptionServices.GetExtendedExceptionText(ex, preamble), Consts.traceError, string.Empty);
    }
  }

  private void RenameAttributeType(
    IUserSession session,
    IDBAttributeType dbAttributeType,
    string[] possibleNewNames)
  {
    foreach (string possibleNewName in possibleNewNames)
    {
      if (!this.IsAttributeNameInUse(session, possibleNewName))
      {
        dbAttributeType.Name = possibleNewName;
        break;
      }
    }
  }

  private bool IsAttributeNameInUse(IUserSession session, string attributeName)
  {
    return session.GetAttributeType(attributeName, false) != null;
  }

  private static bool IsScriptExist(string scriptName)
  {
    return File.Exists(Path.Combine(KernelUpdate.GetUpdateFolderPath(ServerServices.GetService(typeof (IConfigurationManager)) as IConfigurationManager), scriptName));
  }
}
