// Decompiled with JetBrains decompiler
// Type: Intermech.Files.PublishedObject
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Files;

public sealed class PublishedObject
{
  private DBObjectState dbObject;
  private PublishedFile masterFile;
  private List<PublishedFile> objectFiles;

  public PublishedObject(
    DBObjectState dbObject,
    PublishedFile masterFile,
    List<PublishedFile> objectFiles)
  {
    if (dbObject == null)
      throw new ArgumentNullException();
    if (objectFiles == null)
      throw new ArgumentNullException();
    this.dbObject = dbObject;
    this.masterFile = masterFile;
    this.objectFiles = objectFiles;
  }

  public DBObjectState DBObject => this.dbObject;

  public PublishedFile MasterFile => this.masterFile;

  public List<PublishedFile> ObjectFiles => this.objectFiles;
}
