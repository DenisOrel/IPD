// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.ClientConsts
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using System;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Interfaces.Client;

/// <summary>Summary description for ClientConsts.</summary>
public class ClientConsts
{
  public static readonly string FakeNodeString = "----------";
  public static readonly string PasswordString = "***************";
  public static readonly char PasswordChar = '●';
  public static readonly int UserPropertyDescriptorID = 1000;
  public static readonly int CacheLifeTime = 300;
  public static readonly string MultiValueEnumerateFormat = "00";
  /// <summary>
  /// Соообщение нотификатора: применяется для уведомления редактора файловых атрибутов об изменении файлового атрибута объекта из навигатора и других сторонних мест.
  /// </summary>
  public static readonly string NotificationFileAttribute4ObjectChanged = "FileAttribute4ObjectChanged";
  public static GetAttributeValuesModes GetAttributeValuesModes = GetAttributeValuesModes.IncludeName | GetAttributeValuesModes.IncludeAlias | GetAttributeValuesModes.IncludeObligatoryAttributes | GetAttributeValuesModes.IncludeGroupName | GetAttributeValuesModes.CheckWriteAccess | GetAttributeValuesModes.IncludeDescriptions | GetAttributeValuesModes.IncludeCaption;
  public static GetAttributeValuesModes GetAttributeValuesModesMinimum = GetAttributeValuesModes.IncludeName | GetAttributeValuesModes.IncludeAlias | GetAttributeValuesModes.IncludeGroupName | GetAttributeValuesModes.CheckWriteAccess | GetAttributeValuesModes.IncludeDescriptions | GetAttributeValuesModes.IncludeCaption;
  public static readonly Guid CategoryUsersGroupsGuid = new Guid("{cad00125-306c-11d8-b4e9-00304f19f545}");
  public static readonly Guid MeasuresGuid = new Guid("{3664C233-E64A-480D-8275-AF51F81536A2}");
  public static readonly Guid CategoryUsersRolesGuid = new Guid("9a79b82b-ff3a-4437-b998-09a71c64dbe4");
  public static int UsersGroupsCategoryID = -1;
  public static int MeasuresCategoryID = -1;
  public static int UsersRolesCategoryID = -1;
  public static int CategoryOrganizationalUnitsNode = -1;
  private static bool inDeveloperModeInitialized = false;
  public static bool inDeveloperMode = false;

  public static bool IsFakeNode(TreeNode tn)
  {
    return tn.Nodes.Count == 1 && tn.Nodes[0].Text == ClientConsts.FakeNodeString;
  }

  /// <summary>
  /// Содержит занчение true, если система работает в режиме разработчика приложений (разрешено показывать и модифицировать гуиды типов
  /// Значение получается у сессии, которая берет его с сервера, а там настройка хранится в конфиге сервера
  /// </summary>
  public static bool InDeveloperMode
  {
    get
    {
      if (!ClientConsts.inDeveloperModeInitialized)
      {
        ClientConsts.inDeveloperMode = (ServicesManager.GetService(typeof (ICurrentUserAndRole)) as ICurrentUserAndRole).DeveloperMode;
        ClientConsts.inDeveloperModeInitialized = true;
      }
      return ClientConsts.inDeveloperMode;
    }
  }

  public class ListLayoutConfig
  {
    public static readonly string ConfigFile = "Configuration.ListLayouts";
  }
}
