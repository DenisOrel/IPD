// Decompiled with JetBrains decompiler
// Type: Intermech.Document.RtfEditor.DisposeInvoker
// Assembly: Intermech.Document.Model, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FEA44A44-A9AA-4CE5-9D41-60F8B1EE2840
// Assembly location: D:\IPS\Client\Intermech.Document.Model.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Model.xml

#nullable disable
namespace Intermech.Document.RtfEditor;

/// <summary>Вспомогательный делегат для вызова Dispose из других потоков</summary>
/// <param name="disposing"></param>
internal delegate void DisposeInvoker(bool disposing);
