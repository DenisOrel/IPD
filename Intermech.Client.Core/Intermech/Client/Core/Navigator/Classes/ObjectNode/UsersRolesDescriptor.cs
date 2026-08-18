
// Type: Intermech.Client.Core.Navigator.Classes.ObjectNode.UsersRolesDescriptor
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces.Client;
using Intermech.Localization;
using Intermech.Navigator.DBObjects;
using Intermech.Navigator.Persistence;
using System;


namespace Intermech.Client.Core.Navigator.Classes.ObjectNode;

/// <summary>Дескриптор для Ролей с отображением пользователей</summary>
public class UsersRolesDescriptor : TopObjectsDescriptor
{
  private static int _UserRoleTypeID;

  /// <summary>Создает дескриптор.</summary>
  public UsersRolesDescriptor()
    : base(ClientConsts.UsersRolesCategoryID, 0, LocalizationHolder.rm.GetString("Client.Core_733"), UsersRolesDescriptor.GetUserRoleTypeID())
  {
  }

  /// <summary>
  /// Специальный конструктор, используемый для десериализации дескриптора.
  /// </summary>
  /// <param name="state">Сериализованное представление дескриптора</param>
  protected UsersRolesDescriptor(PersistentState state)
    : base(state)
  {
  }

  /// <summary>Выполняет сериализацию дескриптора.</summary>
  /// <param name="state"></param>
  public override void GetObjectData(PersistentState state)
  {
  }

  private static int GetUserRoleTypeID()
  {
    if (UsersRolesDescriptor._UserRoleTypeID == 0)
      UsersRolesDescriptor._UserRoleTypeID = (ServicesManager.GetService(typeof (IClientMetadataCache)) as IClientMetadataCache).GetObjectType(new Guid("cad00007-306c-11d8-b4e9-00304f19f545"), true).ObjectType;
    return UsersRolesDescriptor._UserRoleTypeID;
  }
}
