
// Type: Intermech.Client.Core.FormDesigner.TabPages.FormAction
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Expert;
using Intermech.PropertyEditors;


namespace Intermech.Client.Core.FormDesigner.TabPages;

/// <summary>
/// 
/// </summary>
internal class FormAction
{
  /// <summary>Действие.</summary>
  public Forms4ActionType ActionType { get; private set; }

  /// <summary>
  /// 
  /// </summary>
  public IFolder Folder { get; private set; }

  /// <summary>Идентификатор формы.</summary>
  public long FormID { get; private set; }

  /// <summary>Идентификатор пользователя/роли.</summary>
  public long UserID { get; set; }

  /// <summary>Условие.</summary>
  public TempFormula Condition { get; set; }

  /// <summary>Конструктор.</summary>
  /// <param name="actionType">Действие</param>
  /// <param name="folder"></param>
  /// <param name="formID">Идентификатор формы</param>
  public FormAction(Forms4ActionType actionType, IFolder folder, long formID)
  {
    this.ActionType = actionType;
    this.Folder = folder;
    this.FormID = formID;
    this.UserID = 0L;
    this.Condition = (TempFormula) null;
  }
}
