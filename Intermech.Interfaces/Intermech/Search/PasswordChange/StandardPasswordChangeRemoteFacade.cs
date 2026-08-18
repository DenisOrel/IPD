
// Type: Intermech.Search.PasswordChange.StandardPasswordChangeRemoteFacade
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Interfaces;
using Intermech.Protection;
using Intermech.Search.PasswordHashing;
using System;


namespace Intermech.Search.PasswordChange
{
    public sealed class StandardPasswordChangeRemoteFacade : IPasswordChangeRemoteFacade
    {
      private readonly IMServer _server;

      public StandardPasswordChangeRemoteFacade(IMServer server)
      {
        this._server = server != null ? server : throw new ArgumentNullException(nameof (server));
      }

      public ChangePasswordResult ChangePassword(string oldPassword, string newPassword)
      {
        oldPassword = oldPassword ?? string.Empty;
        newPassword = newPassword ?? string.Empty;
        ChangePasswordResult changePasswordResult = new ChangePasswordResult();
        string oldPasswordHash = this.GetOldPasswordHash();
        if (new PswPackage(oldPassword, PasswordHashingHelper.GetCryptMethod(oldPasswordHash)).IsValidPassword(oldPasswordHash))
        {
          using (SessionKeeper sessionKeeper = new SessionKeeper())
          {
            IDBObject dbObject = sessionKeeper.Session.GetObject(sessionKeeper.Session.UserID);
            try
            {
              dbObject.SetAttributesValues(new AttributeValues[1]
              {
                new AttributeValues(PasswordChangeConstants.PasswordAttributeTypeId, (object) new PswPackage(newPassword, this._server.CryptMethod))
              });
            }
            catch (Exception ex)
            {
              changePasswordResult.NewPasswordError = ex.Message;
            }
            IDBAttribute attributeById = dbObject.GetAttributeByID(PasswordChangeConstants.ShouldChangePasswordAfterFirstLoginAttributeTypeId);
            if (attributeById != null)
            {
              if (attributeById.AsBoolean)
                attributeById.Value = (object) false;
            }
          }
        }
        else
          changePasswordResult.IsOldPasswordWrong = true;
        return changePasswordResult;
      }

      public bool GetPasswordChangeNeed()
      {
        using (SessionKeeper sessionKeeper = new SessionKeeper())
        {
          IDBAttribute attributeById = sessionKeeper.Session.GetObject(sessionKeeper.Session.UserID).GetAttributeByID(PasswordChangeConstants.ShouldChangePasswordAfterFirstLoginAttributeTypeId);
          return attributeById != null && attributeById.AsBoolean;
        }
      }

      private string GetOldPasswordHash()
      {
        using (SessionKeeper sessionKeeper = new SessionKeeper())
          return this.GetPasswordAttribute(sessionKeeper.Session).AsString;
      }

      private IDBAttribute GetPasswordAttribute(IUserSession userSession)
      {
        return userSession.GetObject(userSession.UserID).GetAttributeByID(PasswordChangeConstants.PasswordAttributeTypeId);
      }
    }
}
