
// Type: Intermech.Commands.BasicCommandsProvider
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;


namespace Intermech.Commands;

internal static class BasicCommandsProvider
{
  public static void Init()
  {
    CommandFactory.OnCreateCommand += new EventHandler<CreateCommandEventArgs>(BasicCommandsProvider.OnCreateObjectCommand);
    CommandFactory.OnCreateCommand += new EventHandler<CreateCommandEventArgs>(BasicCommandsProvider.OnCreateObjectCopyCommand);
  }

  private static void OnCreateObjectCopyCommand(object sender, CreateCommandEventArgs e)
  {
    if (!(e.CommandType == typeof (ObjectCopyCommand)) || e.Command != null)
      return;
    switch (e.CommandName)
    {
      case "Checkout":
        e.Command = (Command) new CheckoutCommand();
        break;
      case "Checkin":
        e.Command = (Command) new CheckinCommand();
        break;
      case "CancelChanges":
        e.Command = (Command) new CancelChangesCommand();
        break;
    }
  }

  private static void OnCreateObjectCommand(object sender, CreateCommandEventArgs e)
  {
    if (!(e.CommandType == typeof (ObjectCommand)) || e.Command != null || !(e.CommandName == "SaveChanges"))
      return;
    e.Command = (Command) new SaveChangesCommand();
  }
}
