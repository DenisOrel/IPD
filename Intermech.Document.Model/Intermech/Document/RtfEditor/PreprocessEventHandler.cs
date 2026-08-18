// Decompiled with JetBrains decompiler
// Type: Intermech.Document.RtfEditor.PreprocessEventHandler
// Assembly: Intermech.Document.Model, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FEA44A44-A9AA-4CE5-9D41-60F8B1EE2840
// Assembly location: D:\IPS\Client\Intermech.Document.Model.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Model.xml

using System;
using System.ComponentModel;

#nullable disable
namespace Intermech.Document.RtfEditor;

/// <summary>Делегат предварительной обработки простого события</summary>
/// <param name="sender">Отправитель события</param>
/// <param name="e">Аргументы события</param>
/// <param name="cancelEventArgs">Аргументы предварительной обработки</param>
internal delegate void PreprocessEventHandler(
  object sender,
  EventArgs e,
  CancelEventArgs cancelEventArgs);
