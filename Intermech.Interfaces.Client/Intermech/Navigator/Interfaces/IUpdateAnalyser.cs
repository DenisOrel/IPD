// Decompiled with JetBrains decompiler
// Type: Intermech.Navigator.Interfaces.IUpdateAnalyser
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

#nullable disable
namespace Intermech.Navigator.Interfaces;

/// <summary>
/// Интерфейс анализатора, позволяющий выполнить обработать интерфейс плана действий
/// </summary>
public interface IUpdateAnalyser
{
  /// <summary>
  /// Метод вызывается перед анализом списка элементов для выполнения предварительных действий
  /// </summary>
  /// <param name="plan">Интерфейс плана по обновлению элементов пространства навигации</param>
  void Preprocess(IUpdatePlan plan);

  /// <summary>
  /// Метод вызывается для анализа каждого элемента из списка
  /// </summary>
  /// <param name="nodeID">Идентификатор очередного элемента пространства навигации</param>
  /// <param name="plan">Интерфейс плана по обновлению элементов пространства навигации</param>
  void Process(INodeID nodeID, IUpdatePlan plan);

  /// <summary>
  /// Метод вызывается после анализа списка элементов для выполнения окончательных действий
  /// </summary>
  /// <param name="plan">Интерфейс плана по обновлению элементов пространства навигации</param>
  void Postprocess(IUpdatePlan plan);
}
