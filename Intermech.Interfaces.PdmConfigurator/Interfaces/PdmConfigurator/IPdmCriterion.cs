// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.PdmConfigurator.IPdmCriterion
// Assembly: Intermech.Interfaces.PdmConfigurator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 6A3EF664-00FF-4A8A-A8E2-24964457B937
// Assembly location: D:\IPS\Client\Intermech.Interfaces.PdmConfigurator.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.PdmConfigurator.xml

using System;
using System.Collections.Generic;
using System.Xml;

#nullable disable
namespace Intermech.Interfaces.PdmConfigurator;

/// <summary>
/// Интерфейс элементов в различных коллекциях конфигуратора составов IPS
/// </summary>
public interface IPdmCriterion : IAssignable, ICloneable, IEvaluator, IXMLStorageLoadSave
{
  /// <summary>Является ли элемент пустым или нет</summary>
  bool Empty { get; }

  /// <summary>
  /// Значение по умолчанию, если вычисление не может быть выполнено или не требуется
  /// </summary>
  PdmConfiguratorResult DefaultEvaluatorValue { get; }

  /// <summary>Вид критерия конфигуратора составов IPS</summary>
  PdmCriterionType CriterionType { get; set; }

  /// <summary>Сгенерировать пустой критерий</summary>
  /// <returns>Пустой критерий</returns>
  IPdmCriterion GenerateEmptyCriterion();

  /// <summary>
  /// Сгенерировать пустой критерий и добавить его в коллекцию (в коллекцию дочерних элементов)
  /// </summary>
  /// <returns>Добавленный пустой критерий</returns>
  IPdmCriterion AddEmptyCriterion();

  /// <summary>
  /// Сгенерировать критерий-"заглушку" и добавить его в коллекцию (в коллекцию дочерних элементов)
  /// </summary>
  /// <returns>Добавленный критерий-"заглушка"</returns>
  IPdmCriterion AddStubCriterion();

  /// <summary>
  /// Удалить критерий из коллекции (коллекции дочерних элементов)
  /// </summary>
  /// <returns>true - удаление было выполнено успешно</returns>
  bool RemoveCriterion(IPdmCriterion criterion);

  /// <summary>
  /// Проверить наличие критерия в коллекции (коллекции дочерних элементов)
  /// </summary>
  /// <param name="criterion">Искомый критерий</param>
  /// <returns>true - критерий найден в коллекции (коллекции дочерних элементов)</returns>
  bool ExistsCriterion(IPdmCriterion criterion);

  /// <summary>
  /// Отыскать первый критерий, у которого Guid опции равно указанному. Поиск выполняется в самом элементе и всех его вложенных элементах
  /// </summary>
  /// <param name="option">Guid опции</param>
  /// <returns>Первый критерий, у которого Guid опции равно указанному</returns>
  IPdmCriterion FindCriterion(Guid option);

  /// <summary>Преобразовать критерий в строку</summary>
  /// <param name="isLastItem"> является ли критерий последним в родительской коллекции</param>
  /// <param name="isSingleItem"> является ли критерий единственным в родительской коллекции</param>
  /// <returns></returns>
  string GenerateStringComments(bool isLastItem, bool isSingleItem);

  /// <summary>
  /// Отыскать все критерии не заглушки, у которых Guid опции равно указанному.
  /// Поиск выполняется в самом элементе и всех его вложенных элементах
  /// </summary>
  /// <param name="option">Guid опции</param>
  /// <returns>Все критерии, у которых Guid опции равно указанному</returns>
  List<IPdmCriterion> FindCriterionEx(Guid option);

  /// <summary>
  /// Отыскать критерий, у которого Guid и значение опции и равны указанным значениям. Поиск выполняется в самом элементе и всех его вложенных элементах
  /// </summary>
  /// <param name="option">Guid опции</param>
  /// <param name="optionValue">ID значения опции</param>
  /// <returns>Критерий, у которого Guid и значение опции и равны указанным значениям</returns>
  IPdmCriterion FindCriterion(Guid option, string optionValue);

  /// <summary>
  /// Метод вызывается перед сохранением критерия в XML-документ. При возникновении ошибки
  /// следует сгенерировать исключение
  /// </summary>
  /// <param name="holder">Контейнер, которому принадлежит данный критерий</param>
  void BeforeSave(object holder);

  /// <summary>Выполнена загрузка данных из указанного узла настроек</summary>
  /// <param name="xmlStorage">Хранилище настроек</param>
  /// <param name="node">Узел с данными</param>
  void XMLAfterLoad(XMLSettingsStorage xmlStorage, XmlNode node);

  /// <summary>
  /// Выполнено сохранение данных в состав указанного родительского узла
  /// </summary>
  /// <param name="xmlStorage">Хранилище настроек</param>
  /// <param name="parentNode">Родительский узел или null (тогда сохранение можно выполнять в корневой узел)</param>
  void XMLAfterSave(XMLSettingsStorage xmlStorage, XmlNode parentNode);
}
