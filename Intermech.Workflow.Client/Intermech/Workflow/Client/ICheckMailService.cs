// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Client.ICheckMailService
// Assembly: Intermech.Workflow.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 69C148DA-C200-403A-9CDB-2C809AA0D654
// Assembly location: D:\IPS\Client\Intermech.Workflow.Client.dll

using System.Windows.Forms;

#nullable disable
namespace Intermech.Workflow.Client;

public interface ICheckMailService
{
  void StartListener();

  void CountUnreadMail(bool showForm);

  void BeginUpdate();

  void EndUpdate(int count);

  FormWindowState PreviousMainFormState { get; set; }

  void GoToMail();

  void ShowDebug();
}
