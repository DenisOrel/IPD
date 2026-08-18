// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Services.PortalServices.ICustomObjectAnalyzer
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.Interfaces.WebPortal;
using System.Collections.Generic;
using System.Data;


namespace Intermech.Kernel.Services.PortalServices;

internal interface ICustomObjectAnalyzer
{
  PublishCompositionObject GetObjectInfo(IUserSession session, IDBObject dBObject);

  PublishCompositionObject GetObjectInfo(IUserSession session, long objectID, bool isRoot);

  void GetRecordInfo(
    IUserSession session,
    List<PublishCompositionObject> objects,
    DataRow row,
    FieldsMapper fieldsMapper,
    List<int> enabledRelationTypes,
    out PublishCompositionObject pco,
    out PublishCompositionRelation pcr);
}
