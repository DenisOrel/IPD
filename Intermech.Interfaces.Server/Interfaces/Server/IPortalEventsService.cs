// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Server.IPortalEventsService
// Assembly: Intermech.Interfaces.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 25BF5CAD-94E4-401A-9DAC-C4D5AE12A515
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Interfaces.Server.dll

using Intermech.Interfaces.WebPortal;

#nullable disable
namespace Intermech.Interfaces.Server;

public interface IPortalEventsService
{
  event GetTaskByTypeEventHandler GetTaskByTypeEvent;

  event ObjectImportedEventHandler ObjectImportedEvent;

  event RelationImportedEventHandler RelationImportedEvent;

  event ObjectAutoPublishEventHandler ObjectAutoPublishEvent;

  event ImportTaskCompletedEventHandler ImportTaskCompletedEvent;

  event ImportTaskErrorEventHandler ImportTaskErrorEvent;

  event ObjectsPublishedEventHandler ObjectsPublishedEvent;

  event CheckPublishCompositionEventHandler CheckPublishCompositionEvent;

  event StartResolveBaseVersionConflictEventHandler StartResolveBaseVersionConflictEvent;

  event BeforeObjectRefreshEventHandler BeforeObjectRefreshEvent;

  event ReadImportedObjectAttributesEventHandler ReadImportedObjectAttributesEvent;
}
