
// Type: AxKGAXLib._DKGAXEvents_OnKgErrorLoadDocumentEvent
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml


namespace AxKGAXLib;

public class _DKGAXEvents_OnKgErrorLoadDocumentEvent
{
  public int docID;
  public string fileName;
  public int errorID;

  public _DKGAXEvents_OnKgErrorLoadDocumentEvent(int docID, string fileName, int errorID)
  {
    this.docID = docID;
    this.fileName = fileName;
    this.errorID = errorID;
  }
}
