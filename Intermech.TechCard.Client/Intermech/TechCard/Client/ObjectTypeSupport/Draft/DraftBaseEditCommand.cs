// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.ObjectTypeSupport.Draft.DraftBaseEditCommand
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.Interfaces;
using Intermech.TechCard.Client.Commands.Edit;

#nullable disable
namespace Intermech.TechCard.Client.ObjectTypeSupport.Draft;

/// <summary>Базовый класс команд редактирования эскиза</summary>
internal abstract class DraftBaseEditCommand : SimpleEditCommand
{
  public DraftBaseEditCommand(string name)
    : base(name)
  {
    this._checkProjLink = true;
  }

  protected override BaseCommandResult DoEditCommand(IDBObject dbObj, int index)
  {
    this.DoEditCommand(dbObj);
    return BaseCommandResult.Terminate;
  }

  /// <summary>Реализация команды</summary>
  /// <param name="dbObj"></param>
  protected abstract void DoEditCommand(IDBObject dbObj);
}
