// Decompiled with JetBrains decompiler
// Type: Intermech.DatabaseConfigurator.IDatabaseConfiguratorService
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

#nullable disable
namespace Intermech.DatabaseConfigurator;

/// <summary>Интерфейс службы DatabaseConfiguratorControl</summary>
public interface IDatabaseConfiguratorService
{
  /// <summary>Добавление атрибута.</summary>
  /// <param name="caption">заголовок окна</param>
  /// <param name="attrGroup">список групп, в которые включить. -1 =&gt; ни в какую. null =&gt; будет запрос.</param>
  /// <returns>id созданного атрибута, 0 - не создан</returns>
  int AddAttribute(string caption, int[] attrGroup);

  /// <summary>Редактирование атрибута</summary>
  /// <param name="caption">заголовок окна</param>
  /// <param name="attributeId">id атрибута</param>
  /// <returns>true если был изменен</returns>
  bool EditAttribute(string caption, int attributeId);

  /// <summary>
  /// зарегистрировать класс работы с пользовательским PropertyDescriptorCollection для категории
  /// </summary>
  /// <param name="category">категория</param>
  /// <param name="iCategoryProps">интерфейс ICategoryProps</param>
  /// <returns>зарегистрированный идентификатор обработчика ICategoryProps</returns>
  int RegisterCategoryProps(int category, ICategoryProps iCategoryProps);

  /// <summary>
  /// разрегистрировать работы с пользовательским PropertyDescriptorCollection
  /// </summary>
  /// <param name="propsId">ранее зарегистрированный идентификатор обработчика</param>
  void UnregisterCategoryProps(int propsId);

  /// <summary>
  /// Зарегистрировать закладку в конфигураторе для типов документов
  /// </summary>
  /// <param name="view"></param>
  void RegisterDocumentAdditionalView(IAdditionalView view);

  /// <summary>
  /// Дополнительные закладки в конфигураторе для типов документов
  /// </summary>
  IAdditionalView[] DocumentAdditionalViews { get; }
}
