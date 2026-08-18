
// Type: Intermech.Search.PasswordChange.PasswordChangeConstants
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Interfaces;
using System;


namespace Intermech.Search.PasswordChange
{
    public static class PasswordChangeConstants
    {
      public static readonly Guid PasswordAttributeTypeGuid = new Guid("cad00019-306c-11d8-b4e9-00304f19f545");
      public static readonly Guid ShouldChangePasswordAfterFirstLoginAttributeTypeGuid = new Guid("cadd9558-306c-11d8-b4e9-00304f19f545");

      public static int PasswordAttributeTypeId
      {
        get => MetaDataHelper.GetAttributeTypeID(PasswordChangeConstants.PasswordAttributeTypeGuid);
      }

      public static int ShouldChangePasswordAfterFirstLoginAttributeTypeId
      {
        get
        {
          return MetaDataHelper.GetAttributeTypeID(PasswordChangeConstants.ShouldChangePasswordAfterFirstLoginAttributeTypeGuid);
        }
      }
    }
}
