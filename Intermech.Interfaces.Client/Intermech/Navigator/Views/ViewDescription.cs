// Decompiled with JetBrains decompiler
// Type: Intermech.Navigator.Views.ViewDescription
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using Intermech.Interfaces.Client;

#nullable disable
namespace Intermech.Navigator.Views;

public sealed class ViewDescription
{
  /// <summary>раздел справки для контрола по умолчанию</summary>
  public const string DefaultHelpTopicId = "670";

  /// <summary>путь к файлу справки по умолчанию</summary>
  public static string DefaultHelpPath => HelpProvidersClass.HelpPath;

  /// <summary>
  /// Возвращает название закладки, которое будет отображаться на экране. Навигатор
  /// получает значение этого свойства после того, как закладка будет проинициализирована
  /// в методе Initialize.
  /// </summary>
  public string Caption { get; set; }

  /// <summary>
  /// Возвращает индекс иконки, которая будет отображаться на экране,
  /// в именованном списке иконок. Навигатор получает значение этого свойства после того,
  /// как закладка будет проинициализирована в методе Initialize.
  /// </summary>
  public int ImageIndex { get; set; }

  /// <summary>
  /// Возвращает индекс расположения закладки среди других закладок
  /// при выводе на экран. Навигатор сортирует отображаемые закладки в
  /// порядке возрастания этого значения. Значение этого свойства
  /// навигатор получает после того, как закладка будет проинициализирована в
  /// методе Initialize.
  /// </summary>
  public int OrderID { get; set; }

  /// <summary>возвращает id раздела хелпа для данной закладки</summary>
  public string HelpTopicId { get; set; } = "670";

  /// <summary>Путь к файлу справки</summary>
  public string HelpPath { get; set; } = ViewDescription.DefaultHelpPath;
}
