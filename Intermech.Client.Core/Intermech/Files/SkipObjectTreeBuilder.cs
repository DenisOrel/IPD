
// Type: Intermech.Files.SkipObjectTreeBuilder
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;
using System.Collections.Generic;
using System.Diagnostics;


namespace Intermech.Files;

public sealed class SkipObjectTreeBuilder : IObjectListBuilder
{
  private readonly long rootObjectId;
  private readonly IDBObjectsInformationService dbObjectsInformation;

  public SkipObjectTreeBuilder(long rootObjectId, IDBObjectsInformationService dbObjectsInformation)
  {
    if (rootObjectId == 0L)
      throw new ArgumentException();
    if (dbObjectsInformation == null)
      throw new ArgumentNullException(nameof (dbObjectsInformation));
    this.rootObjectId = rootObjectId;
    this.dbObjectsInformation = dbObjectsInformation;
  }

  public List<DBObjectState> BuildList()
  {
    List<DBObjectState> dbObjectStateList = new List<DBObjectState>();
    dbObjectStateList.Add(this.dbObjectsInformation.GetObjectState(this.rootObjectId, true));
    if (TraceSupport.ObjectListBuilders.Enabled)
    {
      Trace.WriteLine("File vault: object list creation");
      Trace.WriteLine($"File vault: {this.rootObjectId}");
      Trace.WriteLine($"File vault: object list complete, count = {dbObjectStateList.Count}");
    }
    return dbObjectStateList;
  }
}
