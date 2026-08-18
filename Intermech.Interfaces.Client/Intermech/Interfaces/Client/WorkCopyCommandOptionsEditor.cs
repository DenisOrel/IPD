// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.WorkCopyCommandOptionsEditor
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using System.Windows.Forms;

#nullable disable
namespace Intermech.Interfaces.Client;

/// <summary>
/// Базовый класс для редакторов дополнительных опций выполнения для команд checkout, save changes, checkin, cancel changes.
/// </summary>
public class WorkCopyCommandOptionsEditor : UserControl
{
  /// <summary>Применяет изменения, сделанные в редакторе опций.</summary>
  public virtual void ApplyChanges()
  {
  }
}
