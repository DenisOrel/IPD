
// Type: Intermech.Scripting.Projects.DBScripts.DBScriptSaveChangesBehavior
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Controls;
using Intermech.Scripting.Common.DesignTime;
using System.Windows.Forms;


namespace Intermech.Scripting.Projects.DBScripts;

/// <summary>
/// Базовый класс поведения сценариев IPS во время загрузки/сохранения в IDE.
/// Реализация не является thread safe.
/// </summary>
/// <summary>Создает объект.</summary>
/// <param name="scriptProject">Проект сценария</param>
/// <exception cref="T:System.ArgumentNullException">Параметр <paramref name="scriptProject" /> не должен быть равен null</exception>
public class DBScriptSaveChangesBehavior(DBScriptProject scriptProject) : 
  DBScriptBehavior(scriptProject),
  IScriptSaveChangesBehavior
{
  /// <summary>
  /// Обработчик сохранения новых сценариев, а также существующих сценариев с новым именем.
  /// Метод должен запросить у пользователя необходимые параметры и вернуть их в виде контейнера.
  /// Пользователь может отказаться от сохранения сценария, в этом случае метод должен вернуть null.
  /// </summary>
  /// <returns>Параметры сохранения сценария или null</returns>
  public ScriptSaveAsParameters TrySaveAs()
  {
    return this.ScriptProject.IsNew ? (ScriptSaveAsParameters) this.TrySaveNewScriptProject() : (ScriptSaveAsParameters) null;
  }

  /// <summary>
  /// Обработчик сохранения новых сценариев.
  /// Метод должен запросить у пользователя необходимые параметры и вернуть их в виде контейнера.
  /// Пользователь может отказаться от сохранения сценария, в этом случае метод должен вернуть null.
  /// </summary>
  /// <returns>Параметры сохранения сценария или null</returns>
  protected virtual DBScriptSaveAsParameters TrySaveNewScriptProject()
  {
    string name = this.ScriptProject.Name;
    if (string.IsNullOrEmpty(name) || this.CanChangeNewScriptName())
    {
      name = this.TryEditScriptName(name);
      if (name == null)
        return (DBScriptSaveAsParameters) null;
    }
    return new DBScriptSaveAsParameters(name);
  }

  /// <summary>
  /// Определяет, можно ли изменять имя сценария при сохранении.
  /// </summary>
  /// <returns>Признак возможности изменять имя сценария при сохранении</returns>
  protected virtual bool CanChangeNewScriptName() => true;

  private string TryEditScriptName(string name)
  {
    using (InputQueryForm inputQueryForm = new InputQueryForm())
    {
      inputQueryForm.Text = "Сохрание сценария";
      inputQueryForm.QueryLabel = "Укажите имя сохраняемого сценария";
      inputQueryForm.QueryText = name;
      if (inputQueryForm.ShowDialog() == DialogResult.OK)
        return inputQueryForm.QueryText.Trim();
    }
    return (string) null;
  }

  /// <summary>
  /// Обработчик события, вызывающегося перед сохранением изменений.
  /// Метод вызывается и для новых, и для измененных существующих сценариев.
  /// </summary>
  /// <param name="e">Аргументы события</param>
  public virtual void BeforeSave(ScriptBeforeSaveEventArgs e)
  {
  }

  /// <summary>
  /// Обработчик события, вызывающегося после успешного сохранения изменений.
  /// Метод вызывается и для новых, и для измененных существующих сценариев.
  /// </summary>
  /// <param name="e">Аргументы события</param>
  public virtual void AfterSave(ScriptAfterSaveEventArgs e)
  {
  }
}
