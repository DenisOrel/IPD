// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Workflow.IValidatedItem
// Assembly: Intermech.Interfaces.Workflow, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2DC6A606-08B5-470B-B668-CAC7730D0728
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Workflow.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Workflow.xml

#nullable disable
namespace Intermech.Interfaces.Workflow;

/// <summary>
/// Реализован у элементов, которые могут стать невалидными при передаче через портфель. Используется для подсветки невалидных элементов в списках.
/// </summary>
public interface IValidatedItem
{
  bool Invalid { get; }
}
