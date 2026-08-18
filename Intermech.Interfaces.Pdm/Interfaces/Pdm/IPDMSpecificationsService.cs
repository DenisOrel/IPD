// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Pdm.IPDMSpecificationsService
// Assembly: Intermech.Interfaces.Pdm, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: C981BCB9-CF2A-447D-A8BE-B05ADE22BCE8
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Pdm.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Pdm.xml

using System.Collections.Generic;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Interfaces.Pdm;

/// <summary>
/// Служба модуля расширения "Intermech.Pdm" для работы со спецификациями
/// </summary>
public interface IPDMSpecificationsService
{
  /// <summary>
  /// Получить список специфицируемых объектов для указанной спецификации
  /// </summary>
  /// <param name="specID">Идентификатор версии спецификации</param>
  /// <returns>Список специфицируемых объектов для указанной спецификации</returns>
  List<long> GetSpecifyingObjects(long specID);

  /// <summary>
  /// Получить объект с указанным значением атрибута "Обозначение"
  /// </summary>
  /// <param name="objectType">Идентификатор типа объекта</param>
  /// <param name="designation">Обозначение</param>
  /// <returns>Идентификатор версии объекта или Intermech.Consts.UnknownObjectId</returns>
  long GetObjectWithDesignation(int objectType, string designation);

  /// <summary>Получить спецификацию для указанной версии объекта</summary>
  /// <param name="objectID">Идентификатор версии объекта</param>
  /// <returns>Идентификатор версии спецификации или Intermech.Consts.UnknownObjectId</returns>
  long GetObjectSpecification(long objectID);

  /// <summary>Вызвать форму для создания исполнений по прототипам</summary>
  /// <param name="prototypeID">Идентификатор версии объекта-прототипа</param>
  /// <param name="newObjects">Если задан список, в него будут добавлены идентификаторы новых исполнений</param>
  /// <param name="defMainDesign">Значение атрибута "Обозначение" по умолчанию для нового главного исполнения</param>
  /// <param name="articlesName">Значение атрибута "Наименование" для новых исполнений</param>
  /// <returns>Результаты работы формы</returns>
  DialogResult CreateArticlesForm(
    long prototypeID,
    List<long> newObjects,
    string defMainDesign,
    string articlesName);
}
