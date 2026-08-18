// Decompiled with JetBrains decompiler
// Type: Intermech.Commands.ObjectCommandFactory
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

#nullable disable
namespace Intermech.Commands;

public static class ObjectCommandFactory
{
  public static ObjectCommand CreateObjectCommandByName(string name, bool throwIfNotCreated)
  {
    return CommandFactory.CreateCommand<ObjectCommand>(name, throwIfNotCreated);
  }

  public static ObjectCommand CreateOpenCommand(bool throwIfNotCreated)
  {
    return ObjectCommandFactory.CreateObjectCommandByName("Open", throwIfNotCreated);
  }

  public static ObjectCommand CreateOpenWithCommand(bool throwIfNotCreated)
  {
    return ObjectCommandFactory.CreateObjectCommandByName("OpenWith", throwIfNotCreated);
  }

  public static ObjectCommand CreateEditCommand(bool throwIfNotCreated)
  {
    return ObjectCommandFactory.CreateObjectCommandByName("Edit", throwIfNotCreated);
  }

  public static ObjectCommand CreateViewCommand(bool throwIfNotCreated)
  {
    return ObjectCommandFactory.CreateObjectCommandByName("View", throwIfNotCreated);
  }

  public static ObjectCommand CreatePrintCommand(bool throwIfNotCreated)
  {
    return ObjectCommandFactory.CreateObjectCommandByName("Print", throwIfNotCreated);
  }

  public static ObjectCommand CreateSaveChangesCommand(bool throwIfNotCreated)
  {
    return ObjectCommandFactory.CreateObjectCommandByName("SaveChanges", throwIfNotCreated);
  }

  public static ObjectCopyCommand CreateObjectCopyCommandByName(string name, bool throwIfNotCreated)
  {
    return CommandFactory.CreateCommand<ObjectCopyCommand>(name, throwIfNotCreated);
  }

  public static ObjectCopyCommand CreateCheckoutCommand(bool throwIfNotCreated)
  {
    return ObjectCommandFactory.CreateObjectCopyCommandByName("Checkout", throwIfNotCreated);
  }

  public static ObjectCopyCommand CreateCheckinCommand(bool throwIfNotCreated)
  {
    return ObjectCommandFactory.CreateObjectCopyCommandByName("Checkin", throwIfNotCreated);
  }

  public static ObjectCopyCommand CreateCancelChangesCommand(bool throwIfNotCreated)
  {
    return ObjectCommandFactory.CreateObjectCopyCommandByName("CancelChanges", throwIfNotCreated);
  }
}
