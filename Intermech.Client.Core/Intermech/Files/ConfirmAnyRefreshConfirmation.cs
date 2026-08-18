
// Type: Intermech.Files.ConfirmAnyRefreshConfirmation
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.UI.ActionConfirmations;
using System;
using System.Collections.Generic;


namespace Intermech.Files;

internal sealed class ConfirmAnyRefreshConfirmation : YesNoActionConfirmation
{
  private static readonly ActionConfirmationDescriptor descriptor = new ActionConfirmationDescriptor("ReplaceLocalFile", "Извлечение файлов в рабочую область", "Подтверждение перезаписи локальных файлов", (ICollection<Tuple<int, string>>) new Tuple<int, string>[2]
  {
    new Tuple<int, string>(1, "Да"),
    new Tuple<int, string>(0, "Нет")
  });
  private string caption;
  private string text;

  public ConfirmAnyRefreshConfirmation(string caption, string text)
    : base(ConfirmAnyRefreshConfirmation.Descriptor.Key, true)
  {
    this.caption = caption;
    this.text = text;
  }

  protected override string GetActionCaption() => this.caption;

  protected override string GetActionText() => this.text;

  internal static ActionConfirmationDescriptor Descriptor
  {
    get => ConfirmAnyRefreshConfirmation.descriptor;
  }
}
