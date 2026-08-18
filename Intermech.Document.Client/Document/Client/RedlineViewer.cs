// Decompiled with JetBrains decompiler
// Type: Intermech.Document.Client.RedlineViewer
// Assembly: Intermech.Document.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 143DCF5E-E3F9-48A6-BC7A-E754B20C8CE6
// Assembly location: D:\IPS\Client\Intermech.Document.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Client.xml

using Intermech.Document.Model;
using Intermech.Map;
using Intermech.Redline;

#nullable disable
namespace Intermech.Document.Client;

public class RedlineViewer
{
  private long objectId = -1;
  private ImDocument doc;

  public long ObjectId
  {
    get => this.objectId;
    set => this.objectId = value;
  }

  public ImDocument Document
  {
    get => this.doc;
    set => this.doc = value;
  }

  private Redliner GetRedliner()
  {
    MapView mapView = new MapView();
    ImDocumentShowObject documentShowObject = new ImDocumentShowObject(this.Document);
    return (Redliner) null;
  }
}
