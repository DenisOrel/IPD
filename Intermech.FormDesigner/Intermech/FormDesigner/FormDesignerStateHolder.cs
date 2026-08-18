// Decompiled with JetBrains decompiler
// Type: Intermech.FormDesigner.FormDesignerStateHolder
// Assembly: Intermech.FormDesigner, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: D8D2DB66-E218-49D4-A1F2-016208B123B1
// Assembly location: D:\IPS\Client\Intermech.FormDesigner.dll
// XML documentation location: D:\IPS\Client\Intermech.FormDesigner.xml

using Intermech.Interfaces.Client;
using System.Diagnostics;

#nullable disable
namespace Intermech.FormDesigner;

/// <summary>
/// Класс реализует интерфейс, позволяющий отслеживать текущее состояние службы, управляющей формами редактирования данных.
/// </summary>
internal class FormDesignerStateHolder : IFormDesignerStateHolder
{
  /// <summary>Объект для потокобезопасного доступа</summary>
  private object _syncRoot = new object();
  /// <summary>Состояние</summary>
  private FormDesignerState _state;

  /// <summary>
  /// Состояние службы по работе с формами редактирования данных.
  /// </summary>
  public FormDesignerState State
  {
    [DebuggerStepThrough] get
    {
      lock (this._syncRoot)
        return this._state;
    }
    set
    {
      lock (this._syncRoot)
        this._state = value;
    }
  }
}
