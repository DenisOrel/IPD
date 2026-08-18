// Decompiled with JetBrains decompiler
// Type: Intermech.Document.DBCore.LoadDocumentFromDBObjectDelegate
// Assembly: Intermech.Document.DBCore, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 50CF4D99-832B-4258-9FE1-B244E517D790
// Assembly location: D:\IPS\Client\Intermech.Document.DBCore.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Document;

#nullable disable
namespace Intermech.Document.DBCore;

public delegate ImDocumentData LoadDocumentFromDBObjectDelegate(
  IDBObject docObject,
  int fileIndex,
  bool failIfNotFound);
