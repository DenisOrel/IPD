
// Type: Intermech.Files.FileAttributeActionAdapter`1
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.ControlFlow;
using Intermech.Interfaces;
using System;
using System.Collections.Generic;


namespace Intermech.Files;

/// <summary>
/// Реализует адаптацию к интерфейсу IFileAttributeAction для объектов типа IAction.
/// </summary>
public sealed class FileAttributeActionAdapter<TAction> : IFileAttributeAction where TAction : IAction
{
  private readonly TAction action;

  public FileAttributeActionAdapter(TAction action)
  {
    this.action = (object) action != null ? action : throw new ArgumentNullException(nameof (action));
  }

  public void Perform(IDBAttribute dbFileAttribute, List<string> initialFileNames)
  {
    this.action.Perform();
  }

  public TAction Action => this.action;
}
