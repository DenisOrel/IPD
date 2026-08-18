// Decompiled with JetBrains decompiler
// Type: Intermech.MRP.Server.UpdatesMRP
// Assembly: Intermech.MRP.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 90CF20BA-CEDA-4320-95C8-661A6AE661C2
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.MRP.Server.dll

using Intermech.Interfaces;
using Intermech.Interfaces.MetadataUpdates;

#nullable disable
namespace Intermech.MRP.Server;

internal class UpdatesMRP : IUpdatable
{
  public string[] GetUpdateScripts()
  {
    return new string[31 /*0x1F*/]
    {
      "Intermech.MRP.SubjectArea.xml",
      "Intermech.MRP.LifeSteps.xml",
      "Intermech.MRP.Attributes.xml",
      "Intermech.MRP.RelationTypes.xml",
      "Intermech.MRP.ObjectTypes.xml",
      "Intermech.MRP-PDM.ObjectTypes.xml",
      "Intermech.MRP.Objects.xml",
      "Intermech.MRP2.LifeSteps.xml",
      "Intermech.MRP2.Attributes.xml",
      "Intermech.MRP2.RelTypes.xml",
      "Intermech.MRP2.ObjectTypes.xml",
      "Intermech.MRP2.Classify.xml",
      "Intermech.MRP2.Scripts.xml",
      "Intermech.MRP2.Update190902.xml",
      "Intermech.MRP2.Update190902_2.xml",
      "Intermech.MRP2.Update190903.xml",
      "Intermech.MRP2.Update190916.xml",
      "Intermech.MRP2.Update190917.xml",
      "Intermech.MRP2.WFgroup.xml",
      "Intermech.MRP2.Objects.xml",
      "Intermech.MRP2.Update191029.xml",
      "Intermech.MRP2.Update201126.xml",
      "Intermech.MRP2.Update201223.xml",
      "Intermech.MRP2.Update210211.xml",
      "Intermech.MRP2.Update210309.xml",
      "Intermech.MRP2.Update210416.xml",
      "Intermech.MRP2.Update210908.xml",
      "Intermech.MRP2.Update210916.xml",
      "Intermech.MRP2.Update220122.xml",
      "Intermech.MRP2.Update220412.xml",
      "Intermech.MRP2.DocRelType.xml"
    };
  }

  public void BeforeExecScript(IUserSession session, string scriptName)
  {
  }

  public void AfterExecScript(IUserSession session, string scriptName)
  {
  }

  public void AfterExecAllScripts(IUserSession session)
  {
  }
}
