// Decompiled with JetBrains decompiler
// Type: Intermech.ExternalSystemIntegration.Server.ExtSysIntPlugin
// Assembly: Intermech.ExternalSystemIntegration.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: DA51A3A9-E549-4754-B561-351EB1444903
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.ExternalSystemIntegration.Server.dll

using Intermech.ExternalSystemIntegration.Interfaces;
using Intermech.ExternalSystemIntegration.Server.Helpers;
using Intermech.ExternalSystemIntegration.Server.Settings;
using Intermech.Interfaces;
using Intermech.Interfaces.MetadataUpdates;
using Intermech.Interfaces.Plugins;
using Intermech.Interfaces.Server;
using System;

#nullable disable
namespace Intermech.ExternalSystemIntegration.Server;

public class ExtSysIntPlugin : IPackage, IUpdatable
{
  private static Guid _pluginGuid = new Guid("5964F5A2-8135-446D-AA3A-62540CAB801C");
  internal IEventLogHelper _eventLogHelper;
  private RequestProcessTask _RequestTask;
  private ResponceProcessTask _ResponceTask;

  internal static Guid PluginGuid => ExtSysIntPlugin._pluginGuid;

  public string Name => Const.FullPluginName;

  public void Load(IServiceProvider serviceProvider)
  {
    IDBTimedEvents service1 = ServerServices.GetService(typeof (IDBTimedEvents)) as IDBTimedEvents;
    IServerSession sessionTemporaryClone = service1.GetSystemSessionTemporaryClone("ExtSystemIntegrator") as IServerSession;
    ICustomServices service2 = ServerServices.GetService(typeof (ICustomServices)) as ICustomServices;
    try
    {
      CommonSettingsHolder serviceInstance1 = new CommonSettingsHolder();
      serviceInstance1.ReadSettings(sessionTemporaryClone.SessionGUID);
      ServerServices.AddService(typeof (ICommonSettingsHolder), (object) serviceInstance1);
      service2.AddService(typeof (ICommonSettingsHolder), (object) serviceInstance1);
      RequestObjectHelperService serviceInstance2 = new RequestObjectHelperService();
      ServerServices.AddService(typeof (IRequestObjectHelperService), (object) serviceInstance2);
      service2.AddService(typeof (IRequestObjectHelperService), (object) serviceInstance2);
      XmlParserService serviceInstance3 = new XmlParserService();
      ServerServices.AddService(typeof (IXMLParser), (object) serviceInstance3);
      service2.AddService(typeof (IXMLParser), (object) serviceInstance3);
      ICreatorContainer service3 = ServerServices.GetService(typeof (IDBObjectService)) as ICreatorContainer;
      service3.AddCreator((object) Const.RequestSchemeObjTypeGuid, (object) new RequestSchemeObjectCreator());
      service3.AddCreator((object) Const.ResponceSchemeObjTypeGuid, (object) new ResponceSchemeObjectCreator());
      service3.AddCreator((object) Const.TypeSettingItemObjTypeGuid, (object) new ObjTypeSettingItemObjectCreator());
      service3.AddCreator((object) Const.RequestConfigObjTypeGuid, (object) new RequestConfigObjectCreator());
      service3.AddCreator((object) Const.ResponceConfigObjTypeGuid, (object) new ResponceConfigObjectCreator());
      service3.AddCreator((object) Const.RequestObjTypeGuid, (object) new RequestObjectCreator());
      service3.AddCreator((object) Const.ResponceObjTypeGuid, (object) new ResponceObjectCreator());
      this._RequestTask = new RequestProcessTask();
      service1.RegisterService((object) this._RequestTask);
      this._ResponceTask = new ResponceProcessTask();
      service1.RegisterService((object) this._ResponceTask);
    }
    finally
    {
      sessionTemporaryClone?.Logout("ExtSystemIntegrator");
    }
  }

  public void Unload()
  {
  }

  public string[] GetUpdateScripts()
  {
    return new string[1]
    {
      "Intermech.ExternalSystemIntegration.Server.xml"
    };
  }

  public void BeforeExecScript(IUserSession session, string scriptName)
  {
  }

  public void AfterExecAllScripts(IUserSession session)
  {
  }

  public void AfterExecScript(IUserSession session, string scriptName)
  {
  }

  private static void AddAttributeToObjectType(
    IUserSession Asession,
    Guid AObjectypeGuid,
    string AAttributeName,
    InheritModes AInheritModes,
    RequiredModes ARequiredModes,
    string AValidationRule,
    ComputeValueModes AComputeValueModes,
    string AFormula,
    UniqueValueModes AUniqueValueModes,
    int ALevelID,
    object ADefaultValue,
    OptimizationModes AOptimizationModes,
    bool AIsContent,
    AttributeOptions AAttributeOptions,
    string AMask,
    int AMasterAttributeID,
    int ASourceAttributeID)
  {
    IDBObjectType objectType = Asession.GetObjectType(AObjectypeGuid);
    if (objectType == null)
      return;
    IDBAttributeType attributeType = Asession.GetAttributeType(AAttributeName);
    if (attributeType == null || objectType.Attributes.GetAttributeByID(attributeType.AttributeID) != null)
      return;
    (objectType.Attributes as IDBAttribute4ObjectTypeCollection).Create(new Attribute4ObjectTypeProperties(attributeType.AttributeID, objectType.ObjectType, AInheritModes, ARequiredModes, AValidationRule, AComputeValueModes, AFormula, AUniqueValueModes, ALevelID, ADefaultValue, AOptimizationModes, AIsContent, AAttributeOptions, AMask, AMasterAttributeID, ASourceAttributeID));
  }
}
