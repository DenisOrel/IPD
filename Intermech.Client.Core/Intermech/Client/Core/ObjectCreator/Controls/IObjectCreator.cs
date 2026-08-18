
// Type: Intermech.Client.Core.ObjectCreator.Controls.IObjectCreator
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;


namespace Intermech.Client.Core.ObjectCreator.Controls;

/// <summary>
/// 
/// </summary>
/// <remarks>Признак и метод дополнительных действий после комита объекта вынесены в отдельный интерфейс, чтобы не наследовать от ObjectCreatorControl много ненужного</remarks>
public interface IObjectCreator
{
  bool SaveAfterCommitCreation { get; }

  bool SaveAfterCommit(IUserSession session, long newObjectID);
}
