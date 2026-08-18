// Decompiled with JetBrains decompiler
// Type: Intermech.Services.WeldingJoints.IWeldingJointsService
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.DataFormats;
using Intermech.Interfaces;

#nullable disable
namespace Intermech.Services.WeldingJoints;

public interface IWeldingJointsService
{
  bool CanUpdateWeldingSeams(int documentTypeId);

  UpdateWeldingSeamsResult UpdateWeldingSeams(long documentId);

  UpdateWeldingSeamsResult UpdateWeldingSeams(IDBTypedObjectID documentInfo);

  UpdateWeldingSeamsResult UpdateWeldingSeams(QuickObjectInfo documentInfo);
}
