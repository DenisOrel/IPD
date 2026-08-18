// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Workflow.IScheme
// Assembly: Intermech.Interfaces.Workflow, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2DC6A606-08B5-470B-B668-CAC7730D0728
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Workflow.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Workflow.xml

using Intermech.Workflow;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Interfaces.Workflow;

public interface IScheme : 
  IMailObject,
  IDBObject,
  IDBAttributable,
  IDBSessionable,
  IPluginsData,
  IDBSecurityCollection,
  IDBSecurity,
  ISchemeActivityCreator
{
  /// <summary>Название действия</summary>
  string Name { get; set; }

  /// <summary>Описание действия</summary>
  string Description { get; set; }

  /// <summary>Получить действие по идентификатору</summary>
  /// <param name="id"></param>
  /// <returns></returns>
  IActivity GetActivity(long id);

  void DeleteObjects(long[] ids);

  void DeleteObject(long id);

  /// <summary>
  /// Создает новую переменную и добавляет в список использующихся
  /// </summary>
  /// <param name="Name"></param>
  /// <param name="type"></param>
  /// <param name="addInfo"></param>
  /// <returns>Идентификатор типа переменной</returns>
  int AddVariable(string Name, VarType type, object[] addInfo);

  /// <summary>
  /// Создает новую глобальную переменную и добавляет в список использующихся
  /// </summary>
  /// <param name="name">Имя переменной</param>
  /// <param name="type">Тип переменной</param>
  /// <param name="addInfo">Значения какими нужно проинициализировать переменную</param>
  /// <param name="attributeTypeID">Идентификатор создаваемоего атрибута, 0 в случае если новый атрибут или идентификатор если изменение уже созданного</param>
  /// <returns>Идентификатор типа переменной</returns>
  int AddGlobalVariable(string name, VarType type, object[] addInfo, int attributeTypeID);

  /// <summary>Добавляет переменную в список использующихся</summary>
  /// <param name="TypeID"></param>
  /// <param name="addInfo">Идентификатор типа переменной</param>
  /// <returns></returns>
  int UseVariable(int TypeID, object[] addInfo);

  /// <summary>Исключает переменную из списка переменных</summary>
  /// <param name="TypeID"></param>
  void DeleteVariable(int TypeID);

  /// <summary>Исключает глобальную переменную из списка переменных</summary>
  /// <param name="typeID"></param>
  void DeleteGlobalVariable(int typeID);

  /// <summary>if NewID == 0, save the scheme into the new scheme</summary>
  /// <param name="newID"></param>
  /// <returns>ObjectID of the new scheme</returns>
  long SaveAs(long newID, string name);

  /// <summary>Проверка корректности шаблона</summary>
  /// <returns></returns>
  string Validate(bool checkSubProcessSchemes = true, List<long> checkedSchemesList = null);

  /// <summary>
  /// Системный метод проверки корректности шаблона. Используется редактором шаблонов. Пользователю использовать метод без параметров!.
  /// </summary>
  /// <param name="blankActIDs">список новых созданных действий</param>
  /// <param name="blankLinkIDs">список новых созданных ссылок</param>
  /// <param name="deleted">список удалённых объектов</param>
  /// <param name="checkSubProcessSchemes">проверять ли подпроцессы на наличие отладочных шаблонов, по умолчанию Да</param>
  /// <returns></returns>
  string Validate(
    long[] blankActIDs,
    long[] blankLinkIDs,
    long[] deleted,
    bool checkSubProcessSchemes = true);

  /// <summary>Является ли шаблон валидным</summary>
  /// <returns></returns>
  bool IsValid();

  /// <summary>Список всех действий</summary>
  IActivity[] Activities { get; }

  /// <summary>Список всех ссылок</summary>
  IDBObject[] AllLinks { get; }

  /// <summary>Список переменных</summary>
  IVariables Variables { get; }

  /// <summary>Список глобальных переменных</summary>
  IVariables GlobalVariables { get; }

  /// <summary>
  /// Идентификатор объекта задачи ImProject (если имеется), для согласования которой запущен текущий процесс.
  /// Если такой задачи нет, возвращает 0
  /// </summary>
  long LinkedTaskObjectID { get; }

  IActivity StartActivity { get; }
}
