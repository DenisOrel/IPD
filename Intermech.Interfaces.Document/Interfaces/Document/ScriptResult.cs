// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Document.ScriptResult
// Assembly: Intermech.Interfaces.Document, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: BAA3ECE6-453D-42EC-A7D3-172F1348C93D
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Document.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Document.xml

using System;

#nullable disable
namespace Intermech.Interfaces.Document;

/// <summary>Результат работы скрипта</summary>
[Serializable]
public class ScriptResult
{
  public bool Result { get; set; }

  public ImDocumentData DocumentData { get; set; }

  public ScriptResult(bool result, ImDocumentData documentData)
  {
    this.Result = result;
    this.DocumentData = documentData;
  }
}
