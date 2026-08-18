// Decompiled with JetBrains decompiler
// Type: Intermech.Document.RtfEditor.PreprocessKeyPressEventHandler
// Assembly: Intermech.Document.Model, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FEA44A44-A9AA-4CE5-9D41-60F8B1EE2840
// Assembly location: D:\IPS\Client\Intermech.Document.Model.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Model.xml

using System.ComponentModel;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Document.RtfEditor;

/// <summary>Делегат предварительной обработки события KeyPress</summary>
/// <param name="sender">Отправитель события</param>
/// <param name="e">Аргументы события</param>
/// <param name="cancelEventArgs">Аргументы предварительной обработки</param>
public delegate void PreprocessKeyPressEventHandler(
  object sender,
  KeyPressEventArgs e,
  CancelEventArgs cancelEventArgs);
