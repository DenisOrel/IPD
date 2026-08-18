// Decompiled with JetBrains decompiler
// Type: Intermech.Expert.Editor.CreateEventArgs
// Assembly: Intermech.Expert.Editor, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 3CFAE7BC-E854-46EE-B57C-5E15FC8B5CD5
// Assembly location: D:\IPS\Client\Intermech.Expert.Editor.dll
// XML documentation location: D:\IPS\Client\Intermech.Expert.Editor.xml

using Intermech.Docking;
using System;

#nullable disable
namespace Intermech.Expert.Editor;

public class CreateEventArgs : EventArgs
{
  public long protoObjID;
  public int[] linkTypesID;
  public long[] relatedObjIDs;
  public DateTime startRelationTime;
  public bool IsVersion;
  public DockControl control;

  public CreateEventArgs(
    long pOid,
    int[] l_IDs,
    long[] relObjIds,
    DateTime sRelTime,
    bool iV,
    DockControl con)
  {
    this.protoObjID = pOid;
    this.linkTypesID = (int[]) l_IDs.Clone();
    this.relatedObjIDs = (long[]) relObjIds.Clone();
    this.startRelationTime = sRelTime;
    this.IsVersion = iV;
    this.control = con;
  }
}
