
// Type: Intermech.Scripting.Projects.DBScripts.DBScriptRepository
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using Intermech.Scripting.Common.DesignTime;
using System;
using System.Diagnostics;
using System.IO;
using System.Text;


namespace Intermech.Scripting.Projects.DBScripts;

/// <summary>
/// Класс репозитория для сценариев, хранящихся в базе данных IPS.
/// Реализация является thread safe.
/// </summary>
public sealed class DBScriptRepository : IScriptProjectRepository
{
  private static readonly Guid ScriptTextAttribute = new Guid("cad00366-306c-11d8-b4e9-00304f19f545");
  private DBScriptFactory scriptProjectFactory;
  private Encoding utf8Encoding;
  private UTF8EncodingDetector utf8Detector;

  /// <summary>Создает объект.</summary>
  /// <param name="scriptProjectFactory">Фабрика сценарных проектов</param>
  /// <exception cref="T:System.ArgumentNullException">параметр <paramref name="scriptProjectFactory" /> не должен быть равен null</exception>
  public DBScriptRepository(DBScriptFactory scriptProjectFactory)
  {
    this.scriptProjectFactory = scriptProjectFactory != null ? scriptProjectFactory : throw new ArgumentNullException(nameof (scriptProjectFactory));
    this.utf8Encoding = Encoding.UTF8;
    this.utf8Detector = new UTF8EncodingDetector();
  }

  /// <summary>
  /// Возвращает кодировку, в которой обязаны храниться сценарии в базе данных.
  /// </summary>
  public Encoding AllowedEncoding
  {
    [DebuggerStepThrough] get => this.utf8Encoding;
  }

  /// <summary>Добавляет новый сценарий в хранилище.</summary>
  /// <param name="scriptProject">Объект сценария</param>
  /// <param name="parameters">Параметры добавления сценария</param>
  /// <exception cref="T:System.ArgumentNullException">параметр <paramref name="scriptProject" /> не должен быть равен null; параметр <paramref name="parameters" /> не должен быть равен null</exception>
  public void Add(ScriptProject scriptProject, ScriptSaveAsParameters parameters)
  {
    if (scriptProject == null)
      throw new ArgumentNullException(nameof (scriptProject));
    if (parameters == null)
      throw new ArgumentNullException(nameof (parameters));
    DBScriptProject dbScriptProject = (DBScriptProject) scriptProject;
    if (dbScriptProject.ObjectTypeId == -1)
      throw new ScriptDesignTimeException("Не задан идентификатор типа сценариев.");
    DBScriptSaveAsParameters saveAsParameters = (DBScriptSaveAsParameters) parameters;
    dbScriptProject.Name = saveAsParameters.Name;
    this.SaveScriptContent(dbScriptProject, 0L);
  }

  /// <summary>Возвращает сценарий из хранилища.</summary>
  /// <param name="key">Идентификатор сценария</param>
  /// <returns>Объект сценария</returns>
  /// <exception cref="T:System.ArgumentNullException">Параметр <paramref name="key" /> не должен быть равен null</exception>
  public ScriptProject Get(object key)
  {
    DBScriptRepositoryKey scriptRepositoryKey = DBScriptRepositoryKey.CastFrom(key);
    DBScriptProject emptyProject = (DBScriptProject) this.scriptProjectFactory.CreateEmptyProject(".cs");
    this.LoadScriptContent(emptyProject, scriptRepositoryKey.ObjectId);
    emptyProject.RepositoryKey = (object) scriptRepositoryKey;
    emptyProject.Behaviors.AddRepository((IScriptProjectRepository) this);
    return (ScriptProject) emptyProject;
  }

  /// <summary>Обновляет сценарий в хранилище.</summary>
  /// <param name="scriptProject">Объект сценария</param>
  /// <exception cref="T:System.ArgumentNullException">Параметр <paramref name="scriptProject" /> не должен быть равен null</exception>
  public void Update(ScriptProject scriptProject)
  {
    if (scriptProject == null)
      throw new ArgumentNullException(nameof (scriptProject));
    this.SaveScriptContent((DBScriptProject) scriptProject, DBScriptRepositoryKey.CastFrom(scriptProject.RepositoryKey).ObjectId);
  }

  private void LoadScriptContent(DBScriptProject dbScriptProject, long dbScriptId)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject dbObject = sessionKeeper.Session.GetObject(dbScriptId, true);
      dbScriptProject.ObjectTypeId = dbObject.ObjectType;
      string str = dbObject.Caption ?? string.Empty;
      dbScriptProject.Name = str;
      IDBAttribute attributeByGuid = dbObject.GetAttributeByGuid(DBScriptRepository.ScriptTextAttribute);
      string text = attributeByGuid == null || attributeByGuid.IsNull ? string.Empty : (string) attributeByGuid.Value;
      dbScriptProject.File.SetContentAsText(text, this.utf8Encoding);
    }
  }

  private void SaveScriptContent(DBScriptProject dbScriptProject, long dbScriptId)
  {
    string name = dbScriptProject.Name;
    string scriptContentAsText = this.GetScriptContentAsText(dbScriptProject);
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject dbObject = Consts.IsUndefinedObjectId(dbScriptId) ? sessionKeeper.Session.GetObjectCollection(dbScriptProject.ObjectTypeId).Create() : sessionKeeper.Session.GetObject(dbScriptId, true);
      dbObject.Caption = name;
      IDBAttribute attributeByGuid = dbObject.GetAttributeByGuid(DBScriptRepository.ScriptTextAttribute);
      if (attributeByGuid != null)
        attributeByGuid.Value = (object) scriptContentAsText;
      else
        dbObject.Attributes.AddAttribute(DBScriptRepository.ScriptTextAttribute, true, new object[1]
        {
          (object) scriptContentAsText
        });
      if (!dbObject.IsCreationMode)
        return;
      dbObject.CommitCreation(true);
      if (dbObject.ObjectModifyMode == ObjectModifyModes.Checkout)
        dbObject = dbObject.CheckOut(true);
      dbScriptProject.RepositoryKey = (object) new DBScriptRepositoryKey(dbObject.ObjectID);
    }
  }

  private string GetScriptContentAsText(DBScriptProject dbScriptProject)
  {
    byte[] content = dbScriptProject.File.GetContent();
    if (this.utf8Detector.Detect(content) != this.utf8Encoding)
      throw new ScriptDesignTimeException("Сценарий должен быть сохранен в кодировке UTF-8.");
    using (MemoryStream memoryStream = new MemoryStream(content, false))
    {
      using (StreamReader streamReader = new StreamReader((Stream) memoryStream, this.utf8Encoding))
        return streamReader.ReadToEnd();
    }
  }
}
