// Decompiled with JetBrains decompiler
// Type: Intermech.Document.UI.NodeFilter
// Assembly: Intermech.Document.Model, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FEA44A44-A9AA-4CE5-9D41-60F8B1EE2840
// Assembly location: D:\IPS\Client\Intermech.Document.Model.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Model.xml

#nullable disable
namespace Intermech.Document.UI;

/// <summary>Фильтр узлов</summary>
public abstract class NodeFilter
{
  /// <summary>Проверить узел на соответствие фильтру</summary>
  /// <param name="node">Узел документа</param>
  /// <returns>true, если удовлетворяет условиям фильтра</returns>
  public abstract bool CheckNode(object node);
}
