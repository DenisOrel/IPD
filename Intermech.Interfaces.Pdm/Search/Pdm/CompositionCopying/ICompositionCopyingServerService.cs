// Decompiled with JetBrains decompiler
// Type: Intermech.Search.Pdm.CompositionCopying.ICompositionCopyingServerService
// Assembly: Intermech.Interfaces.Pdm, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: C981BCB9-CF2A-447D-A8BE-B05ADE22BCE8
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Pdm.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Pdm.xml

using Intermech.Search.GroupAttributesChanging;
using System;
using System.Data;

#nullable disable
namespace Intermech.Search.Pdm.CompositionCopying;

public interface ICompositionCopyingServerService
{
  DataTable FindComposition(Guid userSessionGuid, FindCompositionParams @params);

  ObjectBlank[] CreateBlanks(Guid userSessionGuid, long objectVersionID, long[] copyVersionIds);

  bool CheckObjectReferenceAssociatedWithDocumentElement(
    Guid userSessionGuid,
    long documentVersionID);

  Tuple<ObjectBlank, string> CreateObject(Guid userSessionGuid, ObjectBlank blank);

  string[] CreateComposition(
    Guid userSessionGuid,
    long projectVersionId,
    Tuple<long, long>[] composition);

  void RemoveObjects(Guid userSessionGuid, long[] objectVersionIds);

  long FindObjectWithDesignation(Guid userSessionGuid, int objectTypeId, string designation);
}
