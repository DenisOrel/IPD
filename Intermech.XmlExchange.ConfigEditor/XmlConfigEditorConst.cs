// Decompiled with JetBrains decompiler
// Type: Intermech.XmlExchange.ConfigEditor.XmlConfigEditorConst
// Assembly: Intermech.XmlExchange.ConfigEditor, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D148B79A-64FF-4CB8-A129-56A9018E56E2
// Assembly location: D:\IPS\Client\Intermech.XmlExchange.ConfigEditor.dll

#nullable disable
namespace Intermech.XmlExchange.ConfigEditor;

internal class XmlConfigEditorConst
{
  public const string EditorExportSettings = "Редактор конфигураций экспорта/импорта XML";
  public const string XmlExportSettings = "Конфигурация XML-экспорта";
  public const string XmlImportSettings = "Конфигурация XML-импорта";
  public const string NewAttributeType = "Новый тип атрибута";
  public const string NewObjectType = "Новый тип объекта";
  public const string NewRelationType = "Новый тип связи";
  public const string AnyObjType = "Любой тип объекта";
  public const string RelationTypes = "Допустимые типы связей";
  public const string AttributeTypeNoInBase = "Тип атрибута отсутствует в базе";
  public const string ObjectTypeNoInBase = "Тип объекта отсутствует в базе";
  public const string RelationTypeNoInBase = "Тип связи отсутствует в базе";

  public class Message
  {
    public const string SaveConfigAndExit = "Сохранить конфигурацию при закрытии редактора?";
    public const string SettingsNoSave = "Настройки не сохранены";
    public const string SaveChanges = "Сохранить изменения?";
    public const string ResetChangesSettings = "Все внесенные изменения будут удалены! Продолжить?";
    public const string FileDataIsNull = "Файл конфигурации не содержит данных. Создать новую конфигурацию?";
    public const string FileNoConfigFormat = "Данные файла не соответствует формату конфигурации.";
    public const string AddDuplicateAtrType = "Попытка повторного добавления типа атрибута \"{0}\".";
    public const string AddDuplicateObjType = "Попытка повторного добавления типа объекта \"{0}\".";
    public const string AddDuplicateRelType = "Попытка повторного добавления типа связи \"{0}\".";
    public const string Error = "Ошибка";
    public const string ErrorCastToType = "Ошибка приведения объекта \"{0}\" к типу: {1}";
    public const string ErrorNullReference = "При приведении к типу \"{0}\" ссылка не содержит объекта";
  }

  public class RootNodeExport
  {
    public const string NodeBaseExportSettings = "Базовые настройки выгрузки";
    public const string NodeAttrSettings = "Общие атрибуты";
    public const string NodeObjSettings = "Типы объектов";
    public const string NodeRelSettings = "Типы связей";
    public const string NodeApplSettings = "Настройки выгрузки составов";
    public const string NodeExportScripts = "Скрипты задачи выгрузки";
    public const string NodeExportExtensions = "Расширения задачи выгрузки";
  }

  public class RootNodeImport
  {
    public const string NodeRulesCreate = "Правила создания объектов";
    public const string NodeRulesImport = "Правила импорта объектов";
    public const string NodeRulesSearch = "Правила поиска объектов";
    public const string NodeImbaseImportSettings = "Настройки импорта объектов Imbase";
    public const string NodeExtentions = "Модули расширения импорта";
    public const string NodeScripts = "Скрипты импорта";
    public const string NodeActionsScripts = "Скрипты событий импорта";
    public const string NodeXmlExportSettings = "Сопоставление типов";
  }

  public class PageName
  {
    public const string Settings = "Настройки";
    public const string Attributes = "Атрибуты";
    public const string SearchAttributes = "Атрибуты поиска";
    public const string ApplPartTypes = "Дочерние типы";
    public const string DefAttributes = "Атрибуты по умолчанию";
  }

  public class ImportModificationNode
  {
    public const string ModificationRule = "ModificationRule";
    public const string ModificationRules = "ModificationRules";
    public const string ModificationObjRule = "ModificationObjRule";
    public const string Action = "Action";
    public const string Actions = "Actions";
    public const string AttrDescription = "description";
    public const string AttrOrder = "order";
    public const string AttrReltype = "reltype";
    public const string AttrProjtype = "projtype";
    public const string AttrParttypе = "parttypе";
    public const string AttrMode = "mode";
  }

  public class FileDialog
  {
    public const string TitleOpenFile = "Открыть файл конфигурации";
    public const string TitleSaveAsFile = "Сохранить файл конфигурации";
    public const string FilterFile = "Файл конфигурации(*.blb)|*.blb|Файл конфигурации(*.xml)|*.xml|Все файлы(*.*)|*.*";
  }

  public class ContextMenuName
  {
    public const string CreateItem = "Создать элемент";
    public const string DeleteItem = "Удалить элемент";
    public const string AddAttrType = "Добавить тип атрибута";
    public const string CustomAttrType = "Пользовательский тип атрибута";
    public const string AddObjType = "Добавить тип объекта";
    public const string CustomObjType = "Пользовательский тип объекта";
    public const string AddRelType = "Добавить тип связи";
    public const string CustomRelType = "Пользовательский тип связи";
    public const string ChangeType = "Изменить тип";
    public const string RemoveType = "Исключить тип";
    public const string AddRule = "Добавить правило";
    public const string Create = "Создать";
    public const string AddInObject = "Добавить из объекта";
    public const string Move = "Переместить";
    public const string MoveInStart = "В начало";
    public const string MoveUp = "На один уровень вверх";
    public const string MoveDown = "На один уровень вниз";
    public const string MoveEnd = "В конец";
  }
}
