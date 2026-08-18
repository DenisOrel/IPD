// Decompiled with JetBrains decompiler
// Type: Intermech.Document.UI.MethodInvoke_ChildNodeEvent
// Assembly: Intermech.Document.Model, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FEA44A44-A9AA-4CE5-9D41-60F8B1EE2840
// Assembly location: D:\IPS\Client\Intermech.Document.Model.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Model.xml

using Intermech.Interfaces.Document;

#nullable disable
namespace Intermech.Document.UI;

/// <summary>Обработчик события ChildNodeRemoved в документе </summary>
/// <param name="sender">Объект вызвавший событие</param>
/// <param name="e">Аргументы события</param>
internal delegate void MethodInvoke_ChildNodeEvent(object sender, ChildNode_EventArgs e);
