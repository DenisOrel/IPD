// Decompiled with JetBrains decompiler
// Type: Intermech.FormDesigner.Server.FormsCacheSynchronizer
// Assembly: Intermech.FormDesigner.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ABD17B9B-52A2-4551-9041-386497DBE670
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.FormDesigner.Server.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Server;
using Intermech.Kernel.Services;
using System;

#nullable disable
namespace Intermech.FormDesigner.Server;

internal class FormsCacheSynchronizer : CustomServerSynchronizer, IFormsCacheSynchronizer
{
  public FormsCacheSynchronizer()
    : base(new Guid("DEF24C6F-0B03-4535-8583-30CCAC51C4E7"), "Служба синхронизации кэшей форм редактирования данных IPS")
  {
  }

  public override void ExecuteEvent(SynchonizerEventProperties eventProps, IUserSession session)
  {
    string[] strArray = eventProps.StringInfo.Split(';');
    if (strArray.Length == 0 || !(strArray[0] == "0"))
      return;
    if (ServerServices.ServiceContainer.GetService(typeof (IFormDesignerService)) is IFormDesignerService service1)
      service1.FlushCache();
    if (!(ServerServices.ServiceContainer.GetService(typeof (IServerFormsCache)) is IServerFormsCache service2))
      return;
    service2.Clear();
  }

  public void AddEvent(string strInfo, IDbManager db)
  {
    if (!this.IsRegistered)
      return;
    this.Manager.AddSynchronizerEvent(this.GetEventProps(strInfo), db);
  }
}
