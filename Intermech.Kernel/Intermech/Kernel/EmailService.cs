// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.EmailService
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Chilkat;
using Intermech.Diagnostics;
using Intermech.Interfaces;
using Intermech.Interfaces.Briefcase;
using Intermech.Interfaces.Server;
using Intermech.Localization;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Xml;


namespace Intermech.Kernel;

public class EmailService : LongLifeObject, IEmailService
{
  private const string UnlockCode = "NIKOLYMAILQ_k4SptFrgoS0Z";
  private const string CryptorString = "0B4CE54A-30E3-4194-AEF7-C5D1494DEC47";
  private const string XmlSettingsNode = "Settings";
  private const string XmlProxyNode = "Proxy";
  private const string XmlServerNode = "Server";
  private const string XmlAccountNode = "Account";
  private const string XmlUserNode = "User";
  private const string XmlNameAttribute = "Name";
  private const string XmlGuidAttribute = "Guid";
  private const string XmlSmtpServerAttribute = "SMTPServer";
  private const string XmlSmtpPortAttribute = "SMTPPort";
  private const string XmlSmtpsslAttribute = "SMTPSSL";
  private const string XmlPop3SslAttribute = "POPSSL";
  private const string XmlSmtpConnectionTypeAttribute = "SMTPConType";
  private const string XmlPop3ConnectionTypeAttribute = "POPConType";
  private const string XmlProxyPortAttribute = "Port";
  private const string XmlProxyServerAttribute = "Server";
  private const string XmlProxyTypeAttribute = "Type";
  private const string XmlProxyUserAttribute = "User";
  private const string XmlProxyPassAttribute = "Password";
  private const string XmlPop3ServerAttribute = "POP3Server";
  private const string XmlPop3PortAttribute = "POP3Port";
  private const string XmlEmailAttribute = "Email";
  private const string XmlLoginAttribute = "Login";
  private const string XmlUserIdAttribute = "ObjectID";
  private const string XmlOwnerUserAttribute = "Owner";
  private const string XmlPasswordAttribute = "Password";
  private const string HeaderFieldMessage = "Message-ID";
  private const string HeaderInReplyTo = "In-Reply-To";
  private const string HeaderSubject = "Subject";
  private readonly Guid _attributeEmailSettings = new Guid("cadd92b7-306c-11d8-b4e9-00304f19f545");
  private readonly Guid _objectEmailSettings = new Guid("cadd92b9-306c-11d8-b4e9-00304f19f545");
  private Dictionary<EmailServer, Dictionary<EmailAccaunt, List<AccauntUserInfo>>> _emailSettings;
  private ProxyServer _proxy;
  private static TemporaryStorage _tempStorage = new TemporaryStorage();

  public EmailService() => this.ReloadSettings();

  private void ReloadSettings()
  {
    IDBTimedEvents service = ApplicationServices.Container.GetService<IDBTimedEvents>();
    IUserSession userSession = (IUserSession) null;
    try
    {
      userSession = service.GetSystemSessionTemporaryClone("EmailService.ReloadSettings");
      this._emailSettings = new Dictionary<EmailServer, Dictionary<EmailAccaunt, List<AccauntUserInfo>>>();
      this._proxy = (ProxyServer) null;
      IDBAttribute attributeByGuid = userSession.GetObject(this._objectEmailSettings).GetAttributeByGuid(this._attributeEmailSettings);
      if (attributeByGuid == null)
        return;
      IMemoReader memoReader = attributeByGuid as IMemoReader;
      object obj = (object) null;
      if (memoReader != null && memoReader.OpenMemo(0) > 0)
      {
        obj = (object) memoReader.ReadDataBlock();
        memoReader.CloseMemo();
      }
      if (obj == null)
        return;
      StringReader txtReader = new StringReader(new string((char[]) obj));
      XmlDocument xmlDocument = new XmlDocument();
      xmlDocument.Load((TextReader) txtReader);
      XmlNode xmlNode = xmlDocument.SelectSingleNode("//Settings");
      if (xmlNode == null || !xmlNode.HasChildNodes)
        return;
      foreach (XmlNode childNode1 in xmlNode.ChildNodes)
      {
        if (childNode1.Name == "Proxy")
        {
          XmlAttribute attribute1 = childNode1.Attributes["Server"];
          if (attribute1 != null && attribute1.Value != null)
          {
            this._proxy = new ProxyServer();
            this._proxy.ServerName = attribute1.Value;
            XmlAttribute attribute2 = childNode1.Attributes["Port"];
            if (attribute2 != null && attribute2.Value != null)
              this._proxy.Port = Convert.ToInt32(attribute2.Value);
            XmlAttribute attribute3 = childNode1.Attributes["Type"];
            if (attribute3 != null && attribute3.Value != null)
              this._proxy.Type = (ProxyType) Convert.ToInt32(attribute3.Value);
            XmlAttribute attribute4 = childNode1.Attributes["User"];
            if (attribute4 != null && attribute4.Value != null)
              this._proxy.UserName = attribute4.Value;
            XmlAttribute attribute5 = childNode1.Attributes["Password"];
            if (attribute5 != null && attribute5.Value != null)
              this._proxy.UserPassword = attribute5.Value;
          }
        }
        else if (childNode1.Name == "Server")
        {
          EmailServer key1 = new EmailServer();
          Dictionary<EmailAccaunt, List<AccauntUserInfo>> dictionary = new Dictionary<EmailAccaunt, List<AccauntUserInfo>>();
          XmlAttribute attribute6 = childNode1.Attributes["Name"];
          if (attribute6 != null && attribute6.Value != null)
            key1.Name = attribute6.Value;
          XmlAttribute attribute7 = childNode1.Attributes["Guid"];
          if (attribute7 != null && attribute7.Value != null)
            key1.Guid = new Guid(attribute7.Value);
          XmlAttribute attribute8 = childNode1.Attributes["SMTPServer"];
          if (attribute8 != null && attribute8.Value != null)
            key1.SMTPServer = attribute8.Value;
          XmlAttribute attribute9 = childNode1.Attributes["SMTPPort"];
          if (attribute9 != null && attribute9.Value != null)
            key1.SMPTPort = Convert.ToInt32(attribute9.Value);
          XmlAttribute attribute10 = childNode1.Attributes["SMTPSSL"];
          if (attribute10 != null && attribute10.Value != null)
            key1.SMPTConnectionType = Convert.ToBoolean(attribute10.Value) ? EmailConnectionTypes.SSL : EmailConnectionTypes.Simple;
          XmlAttribute attribute11 = childNode1.Attributes["SMTPConType"];
          if (attribute11 != null && attribute11.Value != null)
            key1.SMPTConnectionType = (EmailConnectionTypes) Convert.ToInt32(attribute11.Value);
          XmlAttribute attribute12 = childNode1.Attributes["POP3Server"];
          if (attribute12 != null && attribute12.Value != null)
            key1.POP3Server = attribute12.Value;
          XmlAttribute attribute13 = childNode1.Attributes["POP3Port"];
          if (attribute13 != null && attribute13.Value != null)
            key1.POP3Port = Convert.ToInt32(attribute13.Value);
          XmlAttribute attribute14 = childNode1.Attributes["POPSSL"];
          if (attribute14 != null && attribute14.Value != null)
            key1.POP3ConnectionType = Convert.ToBoolean(attribute14.Value) ? EmailConnectionTypes.SSL : EmailConnectionTypes.Simple;
          XmlAttribute attribute15 = childNode1.Attributes["POPConType"];
          if (attribute15 != null && attribute15.Value != null)
            key1.POP3ConnectionType = (EmailConnectionTypes) Convert.ToInt32(attribute15.Value);
          if (childNode1.HasChildNodes)
          {
            foreach (XmlNode childNode2 in childNode1.ChildNodes)
            {
              if (!(childNode2.Name != "Account") || !(childNode2.Name != "Accaunt"))
              {
                EmailAccaunt key2 = new EmailAccaunt();
                XmlAttribute attribute16 = childNode2.Attributes["Guid"];
                if (attribute16 != null && attribute16.Value != null)
                  key2.Guid = new Guid(attribute16.Value);
                XmlAttribute attribute17 = childNode2.Attributes["Email"];
                if (attribute17 != null && attribute17.Value != null)
                  key2.Email = attribute17.Value;
                XmlAttribute attribute18 = childNode2.Attributes["Login"];
                if (attribute18 != null && attribute18.Value != null)
                  key2.Login = attribute18.Value;
                XmlAttribute attribute19 = childNode2.Attributes["Password"];
                if (attribute19 != null && attribute19.Value != null)
                  key2.Password = Cryptor.Decrypt(attribute19.Value, "0B4CE54A-30E3-4194-AEF7-C5D1494DEC47");
                List<AccauntUserInfo> accauntUserInfoList = new List<AccauntUserInfo>();
                if (childNode2.HasChildNodes)
                {
                  for (int i = 0; i < childNode2.ChildNodes.Count; ++i)
                  {
                    XmlNode childNode3 = childNode2.ChildNodes[i];
                    if (!(childNode3.Name != "User"))
                    {
                      AccauntUserInfo accauntUserInfo = new AccauntUserInfo();
                      XmlAttribute attribute20 = childNode3.Attributes["ObjectID"];
                      if (attribute20 != null && attribute20.Value != null)
                        accauntUserInfo.UserID = Convert.ToInt64(attribute20.Value);
                      XmlAttribute attribute21 = childNode3.Attributes["Owner"];
                      if (attribute21 != null && attribute21.Value != null)
                        accauntUserInfo.Owner = Convert.ToBoolean(attribute21.Value, (IFormatProvider) CultureInfo.InvariantCulture);
                      accauntUserInfoList.Add(accauntUserInfo);
                    }
                  }
                }
                dictionary.Add(key2, accauntUserInfoList);
              }
            }
          }
          this._emailSettings.Add(key1, dictionary);
        }
      }
    }
    finally
    {
      userSession?.Logout("EmailService.ReloadSettings");
    }
  }

  private void Save()
  {
    IDBTimedEvents service = ApplicationServices.Container.GetService<IDBTimedEvents>();
    IUserSession userSession = (IUserSession) null;
    try
    {
      userSession = service.GetSystemSessionTemporaryClone("EmailService.Save");
      XmlDocument xmlDocument = new XmlDocument();
      XmlElement element1 = xmlDocument.CreateElement("Settings");
      xmlDocument.AppendChild((XmlNode) element1);
      int num;
      if (this._proxy != null)
      {
        XmlElement element2 = xmlDocument.CreateElement("Proxy");
        element2.SetAttribute("Server", this._proxy.ServerName);
        XmlElement xmlElement = element2;
        num = this._proxy.Port;
        string str = num.ToString();
        xmlElement.SetAttribute("Port", str);
        element2.SetAttribute("Type", Convert.ToString((int) this._proxy.Type));
        element2.SetAttribute("User", this._proxy.UserName);
        element2.SetAttribute("Password", this._proxy.UserPassword);
        element1.AppendChild((XmlNode) element2);
      }
      if (this._emailSettings.Count > 0)
      {
        foreach (KeyValuePair<EmailServer, Dictionary<EmailAccaunt, List<AccauntUserInfo>>> emailSetting in this._emailSettings)
        {
          XmlElement element3 = xmlDocument.CreateElement("Server");
          element3.SetAttribute("Guid", emailSetting.Key.Guid.ToString());
          element3.SetAttribute("Name", emailSetting.Key.Name);
          element3.SetAttribute("SMTPServer", emailSetting.Key.SMTPServer);
          XmlElement xmlElement1 = element3;
          num = emailSetting.Key.SMPTPort;
          string str1 = num.ToString();
          xmlElement1.SetAttribute("SMTPPort", str1);
          element3.SetAttribute("SMTPConType", Convert.ToString((int) emailSetting.Key.SMPTConnectionType));
          element3.SetAttribute("POP3Server", emailSetting.Key.POP3Server);
          XmlElement xmlElement2 = element3;
          num = emailSetting.Key.POP3Port;
          string str2 = num.ToString();
          xmlElement2.SetAttribute("POP3Port", str2);
          element3.SetAttribute("POPConType", Convert.ToString((int) emailSetting.Key.POP3ConnectionType));
          if (emailSetting.Value != null && emailSetting.Value.Count > 0)
          {
            foreach (KeyValuePair<EmailAccaunt, List<AccauntUserInfo>> keyValuePair in emailSetting.Value)
            {
              XmlElement element4 = xmlDocument.CreateElement("Account");
              element4.SetAttribute("Guid", keyValuePair.Key.Guid.ToString());
              element4.SetAttribute("Email", keyValuePair.Key.Email);
              element4.SetAttribute("Login", keyValuePair.Key.Login);
              element4.SetAttribute("Password", Cryptor.Encrypt(keyValuePair.Key.Password, "0B4CE54A-30E3-4194-AEF7-C5D1494DEC47"));
              foreach (AccauntUserInfo accauntUserInfo in keyValuePair.Value)
              {
                XmlElement element5 = xmlDocument.CreateElement("User");
                element5.SetAttribute("ObjectID", accauntUserInfo.UserID.ToString());
                element5.SetAttribute("Owner", Convert.ToString(accauntUserInfo.Owner, (IFormatProvider) CultureInfo.InvariantCulture));
                element4.AppendChild((XmlNode) element5);
              }
              element3.AppendChild((XmlNode) element4);
            }
          }
          element1.AppendChild((XmlNode) element3);
        }
      }
      IDBObject dbObject = userSession.GetObject(this._objectEmailSettings);
      IDBAttribute dbAttribute = dbObject.GetAttributeByGuid(this._attributeEmailSettings) ?? dbObject.Attributes.AddAttribute(MetaDataHelper.GetAttributeTypeID(this._attributeEmailSettings), false);
      StringWriter writer = new StringWriter();
      xmlDocument.Save((TextWriter) writer);
      string str3 = writer.ToString();
      IMemoWriter memoWriter = dbAttribute as IMemoWriter;
      memoWriter.OpenMemo(str3.Length);
      memoWriter.WriteDataBlock(str3.ToCharArray());
    }
    finally
    {
      userSession?.Logout("EmailService.Save");
    }
  }

  public int CountEmailsInPackage => throw new NotImplementedException();

  public long GetAttachmentLength(string fileName)
  {
    fileName = EmailService._tempStorage.GetFullFileName(fileName);
    using (FileStream fileStream = new FileStream(fileName, FileMode.Open, System.IO.FileAccess.Read))
      return fileStream.Length;
  }

  public byte[] GetAttachmentData(string fileName, int offset, int count)
  {
    fileName = EmailService._tempStorage.GetFullFileName(fileName);
    using (FileStream fileStream = new FileStream(fileName, FileMode.Open, System.IO.FileAccess.Read))
    {
      int count1 = fileStream.Length - (long) offset > (long) count ? count : (int) (fileStream.Length - (long) offset);
      byte[] buffer = new byte[count1];
      fileStream.Read(buffer, offset, count1);
      return buffer;
    }
  }

  public EmailServer GetServer(Guid serverGuid)
  {
    if (this._emailSettings.Count == 0)
      return (EmailServer) null;
    foreach (KeyValuePair<EmailServer, Dictionary<EmailAccaunt, List<AccauntUserInfo>>> emailSetting in this._emailSettings)
    {
      if (emailSetting.Key.Guid.Equals(serverGuid))
        return emailSetting.Key;
    }
    return (EmailServer) null;
  }

  public EmailServer[] Servers
  {
    get
    {
      if (this._emailSettings.Count == 0)
        return (EmailServer[]) null;
      List<EmailServer> emailServerList = new List<EmailServer>();
      foreach (KeyValuePair<EmailServer, Dictionary<EmailAccaunt, List<AccauntUserInfo>>> emailSetting in this._emailSettings)
        emailServerList.Add(emailSetting.Key);
      return emailServerList.ToArray();
    }
  }

  public void AddServer(EmailServer newServer)
  {
    if (newServer.Guid == Guid.Empty)
      newServer.Guid = Guid.NewGuid();
    foreach (KeyValuePair<EmailServer, Dictionary<EmailAccaunt, List<AccauntUserInfo>>> emailSetting in this._emailSettings)
    {
      if (emailSetting.Key.Guid.Equals(newServer.Guid))
        throw new Exception(string.Format(LocalizationHolder.rm.GetString("Kernel_1012"), (object) newServer.Name));
    }
    this._emailSettings.Add(newServer, new Dictionary<EmailAccaunt, List<AccauntUserInfo>>());
    this.Save();
  }

  public void AddAccaunt(Guid serverGuid, EmailAccaunt newAccount)
  {
    if (this._emailSettings.Count == 0)
      throw new Exception(string.Format(LocalizationHolder.rm.GetString("Kernel_1013"), (object) serverGuid));
    if (newAccount.Guid == Guid.Empty)
      newAccount.Guid = Guid.NewGuid();
    EmailService.CheckAccountProps(newAccount);
    EmailServer key = (EmailServer) null;
    foreach (KeyValuePair<EmailServer, Dictionary<EmailAccaunt, List<AccauntUserInfo>>> emailSetting in this._emailSettings)
    {
      if (emailSetting.Key.Guid.Equals(serverGuid))
      {
        foreach (KeyValuePair<EmailAccaunt, List<AccauntUserInfo>> keyValuePair in emailSetting.Value)
        {
          if (keyValuePair.Key.Email.ToUpper().Equals(newAccount.Email.ToUpper()))
            throw new Exception(string.Format(LocalizationHolder.rm.GetString("Kernel_1014"), (object) newAccount.Email));
          if (keyValuePair.Key.Login.ToUpper().Equals(newAccount.Login.ToUpper()))
            throw new Exception(string.Format(LocalizationHolder.rm.GetString("Kernel_1015"), (object) newAccount.Login));
        }
        key = emailSetting.Key;
        break;
      }
    }
    if (key == null)
      throw new Exception(string.Format(LocalizationHolder.rm.GetString("Kernel_1013"), (object) serverGuid));
    this._emailSettings[key].Add(newAccount, new List<AccauntUserInfo>());
    this.Save();
  }

  public EmailAccaunt[] GetAccaunts(Guid serverGuid)
  {
    if (this._emailSettings.Count == 0)
      return (EmailAccaunt[]) null;
    foreach (KeyValuePair<EmailServer, Dictionary<EmailAccaunt, List<AccauntUserInfo>>> emailSetting in this._emailSettings)
    {
      if (emailSetting.Key.Guid.Equals(serverGuid))
      {
        if (emailSetting.Value.Count == 0)
          return (EmailAccaunt[]) null;
        List<EmailAccaunt> emailAccauntList = new List<EmailAccaunt>(emailSetting.Value.Count);
        foreach (KeyValuePair<EmailAccaunt, List<AccauntUserInfo>> keyValuePair in emailSetting.Value)
          emailAccauntList.Add(keyValuePair.Key);
        return emailAccauntList.ToArray();
      }
    }
    return (EmailAccaunt[]) null;
  }

  public void SetServer(
    EmailServer server,
    Dictionary<EmailAccaunt, List<AccauntUserInfo>> accounts)
  {
    bool flag = false;
    foreach (KeyValuePair<EmailServer, Dictionary<EmailAccaunt, List<AccauntUserInfo>>> emailSetting in this._emailSettings)
    {
      if (emailSetting.Key.Guid.Equals(server.Guid))
      {
        emailSetting.Key.Name = server.Name;
        emailSetting.Key.POP3Port = server.POP3Port;
        emailSetting.Key.POP3Server = server.POP3Server;
        emailSetting.Key.POP3ConnectionType = server.POP3ConnectionType;
        emailSetting.Key.SMPTPort = server.SMPTPort;
        emailSetting.Key.SMTPServer = server.SMTPServer;
        emailSetting.Key.SMPTConnectionType = server.SMPTConnectionType;
        emailSetting.Value.Clear();
        foreach (KeyValuePair<EmailAccaunt, List<AccauntUserInfo>> account in accounts)
          emailSetting.Value.Add(account.Key, account.Value);
        this.Save();
        flag = true;
        break;
      }
    }
    if (!flag)
      throw new Exception(string.Format(LocalizationHolder.rm.GetString("Kernel_1016"), (object) server.Name));
  }

  public void DeleteServer(Guid serverGuid)
  {
    EmailServer key = (EmailServer) null;
    foreach (KeyValuePair<EmailServer, Dictionary<EmailAccaunt, List<AccauntUserInfo>>> emailSetting in this._emailSettings)
    {
      if (emailSetting.Key.Guid.Equals(serverGuid))
      {
        key = emailSetting.Key;
        break;
      }
    }
    if (key == null)
      return;
    this._emailSettings.Remove(key);
    this.Save();
  }

  private static void CheckAccountProps([NotNull] EmailAccaunt newAccount)
  {
    if (newAccount.Email == string.Empty)
      throw new Exception(LocalizationHolder.rm.GetString("Kernel_1017"));
    if (newAccount.Login == string.Empty)
      throw new Exception(LocalizationHolder.rm.GetString("Kernel_1018"));
  }

  public void CheckAccaunt(Guid serverGuid, EmailAccaunt newAccount)
  {
    if (this._emailSettings.Count == 0)
      throw new Exception(string.Format(LocalizationHolder.rm.GetString("Kernel_1013"), (object) serverGuid));
    EmailService.CheckAccountProps(newAccount);
    bool flag = false;
    foreach (KeyValuePair<EmailServer, Dictionary<EmailAccaunt, List<AccauntUserInfo>>> emailSetting in this._emailSettings)
    {
      if (emailSetting.Key.Guid.Equals(serverGuid))
      {
        foreach (KeyValuePair<EmailAccaunt, List<AccauntUserInfo>> keyValuePair in emailSetting.Value)
        {
          if (keyValuePair.Key.Guid != newAccount.Guid)
          {
            if (keyValuePair.Key.Email.ToUpper().Equals(newAccount.Email.ToUpper()))
              throw new Exception(string.Format(LocalizationHolder.rm.GetString("Kernel_1014"), (object) newAccount.Email));
            if (keyValuePair.Key.Login.ToUpper().Equals(newAccount.Login.ToUpper()))
              throw new Exception(string.Format(LocalizationHolder.rm.GetString("Kernel_1015"), (object) newAccount.Login));
          }
        }
        flag = true;
        break;
      }
    }
    if (!flag)
      throw new Exception(string.Format(LocalizationHolder.rm.GetString("Kernel_1013"), (object) serverGuid));
  }

  public List<AccauntUserInfo> GetAccauntUsers(Guid serverGuid, Guid accountGuid)
  {
    foreach (KeyValuePair<EmailServer, Dictionary<EmailAccaunt, List<AccauntUserInfo>>> emailSetting in this._emailSettings)
    {
      Guid guid = emailSetting.Key.Guid;
      if (guid.Equals(serverGuid))
      {
        foreach (KeyValuePair<EmailAccaunt, List<AccauntUserInfo>> keyValuePair in emailSetting.Value)
        {
          guid = keyValuePair.Key.Guid;
          if (guid.Equals(accountGuid))
            return keyValuePair.Value;
        }
      }
    }
    return (List<AccauntUserInfo>) null;
  }

  public EmailAccaunt[] GetAccaunts(long userID, bool ownered)
  {
    List<EmailAccaunt> emailAccauntList = new List<EmailAccaunt>();
    foreach (KeyValuePair<EmailServer, Dictionary<EmailAccaunt, List<AccauntUserInfo>>> emailSetting in this._emailSettings)
    {
      foreach (KeyValuePair<EmailAccaunt, List<AccauntUserInfo>> keyValuePair in emailSetting.Value)
      {
        foreach (AccauntUserInfo accauntUserInfo in keyValuePair.Value)
        {
          if (accauntUserInfo.UserID == userID)
          {
            if (!ownered)
            {
              emailAccauntList.Add(keyValuePair.Key);
              break;
            }
            if (accauntUserInfo.Owner)
            {
              emailAccauntList.Add(keyValuePair.Key);
              break;
            }
            break;
          }
        }
      }
    }
    return emailAccauntList.Count != 0 ? emailAccauntList.ToArray() : (EmailAccaunt[]) null;
  }

  [CanBeNull]
  public EmailAccaunt[] GetAccaunts(long userID)
  {
    List<EmailAccaunt> emailAccauntList = new List<EmailAccaunt>();
    foreach (KeyValuePair<EmailServer, Dictionary<EmailAccaunt, List<AccauntUserInfo>>> emailSetting in this._emailSettings)
    {
      foreach (KeyValuePair<EmailAccaunt, List<AccauntUserInfo>> keyValuePair in emailSetting.Value)
      {
        if (keyValuePair.Value.Any<AccauntUserInfo>((Func<AccauntUserInfo, bool>) (accountUserInfo => accountUserInfo.UserID == userID)))
          emailAccauntList.Add(keyValuePair.Key);
      }
    }
    return emailAccauntList.Count != 0 ? emailAccauntList.ToArray() : (EmailAccaunt[]) null;
  }

  public bool UpdateAccaunt(Guid accountGuid, string newLogin, string newPassword)
  {
    if (newLogin == string.Empty)
      throw new Exception(LocalizationHolder.rm.GetString("Kernel_1018"));
    foreach (KeyValuePair<EmailServer, Dictionary<EmailAccaunt, List<AccauntUserInfo>>> emailSetting in this._emailSettings)
    {
      foreach (KeyValuePair<EmailAccaunt, List<AccauntUserInfo>> keyValuePair in emailSetting.Value)
      {
        if (keyValuePair.Key.Guid.Equals(accountGuid))
        {
          keyValuePair.Key.Login = newLogin;
          keyValuePair.Key.Password = newPassword;
          this.Save();
          return true;
        }
      }
    }
    return false;
  }

  public EmailAccaunt GetAccaunt(string email)
  {
    foreach (KeyValuePair<EmailServer, Dictionary<EmailAccaunt, List<AccauntUserInfo>>> emailSetting in this._emailSettings)
    {
      foreach (KeyValuePair<EmailAccaunt, List<AccauntUserInfo>> keyValuePair in emailSetting.Value)
      {
        if (keyValuePair.Key.Email.ToUpper().Equals(email.ToUpper()))
          return keyValuePair.Key;
      }
    }
    return (EmailAccaunt) null;
  }

  public string SendMessage(
    Guid sessionGuid,
    Guid accountGuid,
    string toEmail,
    string subject,
    string message)
  {
    return this.SendMessage(sessionGuid, (object) accountGuid, toEmail, subject, message, 0L, (int[]) null);
  }

  public string SendMessage(
    Guid sessionGuid,
    Guid accountGuid,
    string toEmail,
    string subject,
    string message,
    long objectID)
  {
    return this.SendMessage(sessionGuid, (object) accountGuid, toEmail, subject, message, objectID, (int[]) null);
  }

  public string SendMessage(
    Guid sessionGuid,
    object accountID,
    string toEmail,
    string subject,
    string message,
    long objectID,
    int[] attachmentIdxs)
  {
    switch (accountID)
    {
      case string str when EmailHelper.IsEmail(str):
        return this.SendMessage(sessionGuid, (this.GetAccaunt(str) ?? throw new Exception(string.Format(LocalizationHolder.rm.GetString("Kernel_1019"), (object) str))).Guid, toEmail, subject, message, objectID, attachmentIdxs, false);
      case Guid accountGuid:
        return this.SendMessage(sessionGuid, accountGuid, toEmail, subject, message, objectID, attachmentIdxs, false);
      default:
        throw new Exception(string.Format(LocalizationHolder.rm.GetString("Kernel_1020"), accountID));
    }
  }

  private string SendMessage(
    Guid sessionGuid,
    Guid accountGuid,
    string toEmail,
    string subject,
    string message,
    long objectID,
    int[] attachmentIdxs,
    bool checkAccounts)
  {
    IUserSession sessionById = UserSession.GetSessionByID(sessionGuid);
    if (checkAccounts)
    {
      EmailAccaunt[] accaunts = this.GetAccaunts(sessionById.UserID);
      if (accaunts == null || accaunts.Length == 0)
        throw new Exception(LocalizationHolder.rm.GetString("Kernel_1021"));
    }
    EmailService.ConnectionData connectionData = this.GetConnectionData(sessionById, accountGuid, true, checkAccounts);
    string[] source = !(toEmail == string.Empty) ? toEmail.Split(';') : throw new Exception(LocalizationHolder.rm.GetString("Kernel_1023"));
    using (IEnumerator<string> enumerator = ((IEnumerable<string>) source).Where<string>((Func<string, bool>) (addressee => !EmailHelper.IsEmail(addressee))).GetEnumerator())
    {
      if (enumerator.MoveNext())
      {
        string current = enumerator.Current;
        throw new Exception(string.Format(LocalizationHolder.rm.GetString("Kernel_1024"), (object) current));
      }
    }
    MailMan mailMan = this.GetMailMan(connectionData, true);
    try
    {
      Email email = new Email();
      email.Subject = subject;
      email.SetHtmlBody(message);
      email.From = connectionData.Email;
      foreach (string emailAddress in source)
        email.AddTo(string.Empty, emailAddress);
      if (objectID != 0L)
      {
        IDBAttribute attributeByGuid = sessionById.GetObject(objectID).GetAttributeByGuid(new Guid("cad0004b-306c-11d8-b4e9-00304f19f545"), true);
        if (attachmentIdxs == null)
        {
          List<int> intList = new List<int>(attributeByGuid.ValuesCount);
          for (int index = 0; index < attributeByGuid.ValuesCount; ++index)
          {
            attributeByGuid.Index = index;
            if (!attributeByGuid.IsNull)
              intList.Add(index);
          }
          attachmentIdxs = intList.ToArray();
        }
        int index1 = 0;
        foreach (int num in attachmentIdxs)
        {
          attributeByGuid.Index = num;
          IBlobReader blobReader = attributeByGuid as IBlobReader;
          BlobInformation blobInformation = blobReader.OpenBlob(0);
          try
          {
            byte[] numArray = blobReader.ReadDataBlock(0);
            if (blobInformation.ArcMethod == ArcMethods.ZLibPacked)
            {
              using (MemoryStream outStream = new MemoryStream())
              {
                using (MemoryStream inStream = new MemoryStream(numArray))
                  ServiceUtils.GetService<IPackedStream>((object) ApplicationServices.Container, true).UnpackStream((System.IO.Stream) outStream, (System.IO.Stream) inStream);
                numArray = outStream.ToArray();
              }
            }
            if (!email.AddDataAttachment(blobInformation.FileName, numArray))
              throw new Exception(email.LastErrorText);
            email.AddAttachmentHeader(index1, "content-disposition", $"inline; filename=\"{blobInformation.FileName}\"");
            ++index1;
          }
          finally
          {
            blobReader.CloseBlob();
          }
        }
      }
      string fieldValue = Guid.NewGuid().ToString();
      email.AddHeaderField("Message-ID", fieldValue);
      if (!mailMan.SendEmail(email))
        throw new Exception(mailMan.LastErrorText);
      return fieldValue;
    }
    finally
    {
      mailMan.CloseSmtpConnection();
    }
  }

  [NotNull]
  private MailMan GetMailMan([NotNull] EmailService.ConnectionData connectiondata, bool smtp)
  {
    MailMan mailMan = new MailMan();
    mailMan.AutoFix = false;
    if (!mailMan.UnlockComponent("NIKOLYMAILQ_k4SptFrgoS0Z"))
      throw new Exception("Chilkat unlock failed");
    if (smtp)
    {
      mailMan.SmtpHost = connectiondata.Server;
      mailMan.SmtpPort = connectiondata.Port;
      mailMan.SmtpUsername = connectiondata.UserName;
      mailMan.SmtpPassword = connectiondata.Password;
      mailMan.SmtpSsl = connectiondata.ConnectionType == EmailConnectionTypes.SSL;
      mailMan.StartTLS = connectiondata.ConnectionType == EmailConnectionTypes.STARTTLS;
    }
    else
    {
      mailMan.MailHost = connectiondata.Server;
      mailMan.MailPort = connectiondata.Port;
      mailMan.PopUsername = connectiondata.UserName;
      mailMan.PopPassword = connectiondata.Password;
      mailMan.PopSsl = connectiondata.ConnectionType == EmailConnectionTypes.SSL;
      mailMan.StartTLS = connectiondata.ConnectionType == EmailConnectionTypes.STARTTLS;
    }
    if (this.Proxy != null && this.Proxy.Type != ProxyType.None && this.Proxy.ServerName != string.Empty)
    {
      if (this.Proxy.Type == ProxyType.HTTP)
      {
        mailMan.HttpProxyHostname = this.Proxy.ServerName;
        if (this.Proxy.Port > 0)
          mailMan.HttpProxyPort = this.Proxy.Port;
        if (this.Proxy.UserName != string.Empty)
          mailMan.HttpProxyUsername = this.Proxy.UserName;
        if (this.Proxy.UserPassword != string.Empty)
          mailMan.HttpProxyPassword = this.Proxy.UserPassword;
      }
      else if (this.Proxy.Type == ProxyType.SOCKS4 || this.Proxy.Type == ProxyType.SOCKS5)
      {
        mailMan.SocksHostname = this.Proxy.ServerName;
        mailMan.SocksVersion = this.Proxy.Type == ProxyType.SOCKS4 ? 4 : 5;
        if (this.Proxy.Port > 0)
          mailMan.SocksPort = this.Proxy.Port;
        if (this.Proxy.UserName != string.Empty)
          mailMan.SocksUsername = this.Proxy.UserName;
        if (this.Proxy.UserPassword != string.Empty)
          mailMan.SocksPassword = this.Proxy.UserPassword;
      }
    }
    return mailMan;
  }

  public byte[] GetAttachmentData(
    Guid sessionGuid,
    Guid accountGuid,
    string uidl,
    [NotNull] string fileName)
  {
    IUserSession sessionById = UserSession.GetSessionByID(sessionGuid);
    EmailService.ConnectionData connectionData = this.GetConnectionData(sessionById, accountGuid, false, sessionById.UserID != sessionById.IdentHelper.SystemID);
    MailMan mailMan = this.GetMailMan(connectionData, false);
    try
    {
      Email email = mailMan.FetchEmail(uidl);
      if (email == null)
        throw new Exception(EmailService.NotConnectedMessage(mailMan, connectionData));
      for (int index = 0; index < email.NumAttachments; ++index)
      {
        if (email.GetAttachmentFilename(index) == fileName)
          return email.GetAttachmentData(index);
      }
      return (byte[]) null;
    }
    finally
    {
      mailMan.Pop3EndSession();
    }
  }

  public void ClearInbox(Guid sessionGuid, Guid accountGuid, List<string> deleteList)
  {
    IUserSession sessionById = UserSession.GetSessionByID(sessionGuid);
    MailMan mailMan = this.GetMailMan(this.GetConnectionData(sessionById, accountGuid, false, sessionById.UserID != sessionById.IdentHelper.SystemID), false);
    try
    {
      foreach (string delete in deleteList)
        mailMan.DeleteByUidl(delete);
    }
    finally
    {
      mailMan.Pop3EndSession();
    }
  }

  [NotNull]
  private static string NotConnectedMessage(
    [NotNull] MailMan mailman,
    [NotNull] EmailService.ConnectionData connectiondata)
  {
    System.Text.StringBuilder stringBuilder = new System.Text.StringBuilder();
    stringBuilder.AppendLine(string.Format(LocalizationHolder.rm.GetString("Kernel_1025"), (object) connectiondata.Server, (object) connectiondata.Port));
    stringBuilder.AppendLine(mailman.LastErrorText);
    return stringBuilder.ToString();
  }

  public void ClearTempFiles(List<string> files)
  {
    EmailService._tempStorage.Delete(files.ToArray());
  }

  public void CheckAccauntConnection(Guid sessionGuid, Guid accountGuid)
  {
    IUserSession sessionById = UserSession.GetSessionByID(sessionGuid);
    EmailService.ConnectionData connectionData = this.GetConnectionData(sessionById, accountGuid, false, sessionById.UserID != sessionById.IdentHelper.SystemID);
    MailMan mailMan1 = this.GetMailMan(connectionData, false);
    try
    {
      mailMan1.Pop3Noop();
    }
    finally
    {
      mailMan1.Pop3EndSession();
    }
    MailMan mailMan2 = this.GetMailMan(connectionData, true);
    try
    {
      mailMan2.SmtpNoop();
    }
    finally
    {
      mailMan2.CloseSmtpConnection();
    }
  }

  public List<EmailMessage> GetInboxMessages(
    Guid sessionGuid,
    Guid accountGuid,
    List<string> presentMessageIDs)
  {
    IUserSession sessionById = UserSession.GetSessionByID(sessionGuid);
    MailMan mailMan = this.GetMailMan(this.GetConnectionData(sessionById, accountGuid, false, sessionById.UserID != sessionById.IdentHelper.SystemID), false);
    try
    {
      return EmailService.ReadMessage(mailMan, EmailService.ReadMessagesPacket(mailMan), presentMessageIDs);
    }
    finally
    {
      mailMan.Pop3EndSession();
    }
  }

  public string GetMessageID(Guid sessionGuid, Guid accountGuid, string subject)
  {
    IUserSession sessionById = UserSession.GetSessionByID(sessionGuid);
    EmailService.ConnectionData connectionData = this.GetConnectionData(sessionById, accountGuid, false, sessionById.UserID != sessionById.IdentHelper.SystemID);
    MailMan mailMan = this.GetMailMan(connectionData, false);
    try
    {
      Email byHeader = (mailMan.GetAllHeaders(0) ?? throw new Exception(EmailService.NotConnectedMessage(mailMan, connectionData))).FindByHeader("Subject", subject);
      return byHeader != null ? byHeader.GetHeaderField("Message-ID") : string.Empty;
    }
    finally
    {
      mailMan.Pop3EndSession();
    }
  }

  public ProxyServer Proxy
  {
    get => this._proxy;
    set
    {
      this._proxy = value;
      this.Save();
    }
  }

  [NotNull]
  private static List<EmailMessage> ReadMessage(
    MailMan mailman,
    [NotNull] StringArray sa,
    [CanBeNull] List<string> presentMessageIDs)
  {
    List<EmailMessage> emailMessageList = new List<EmailMessage>();
    for (int index1 = 0; index1 < sa.Count; ++index1)
    {
      using (Email email = mailman.FetchEmail(sa.GetString(index1)))
      {
        if (email == null)
          throw new Exception(mailman.LastErrorText);
        if (presentMessageIDs != null)
        {
          if (presentMessageIDs.IndexOf(email.Uidl) >= 0)
            continue;
        }
        EmailMessage emailMessage = new EmailMessage();
        emailMessage.From = email.FromName;
        emailMessage.FromEmail = email.FromAddress;
        emailMessage.Subject = email.Subject;
        emailMessage.Message = email.Body;
        emailMessage.Date = email.EmailDate;
        emailMessage.MessagetID = email.Uidl;
        emailMessage.InReplyTo = email.GetHeaderField("In-Reply-To");
        int numAttachments = email.NumAttachments;
        emailMessage.FileNames = new List<EmailAttachment>(numAttachments);
        if (numAttachments > 0)
        {
          for (int index2 = 0; index2 < numAttachments; ++index2)
          {
            EmailAttachment emailAttachment = new EmailAttachment(email.GetAttachmentFilename(index2));
            emailMessage.FileNames.Add(emailAttachment);
            byte[] attachmentData = email.GetAttachmentData(index2);
            if (attachmentData.Length != 0)
            {
              using (FileStream fileStream = new FileStream(EmailService._tempStorage.GetFullFileName(emailAttachment.StotageFileName), FileMode.Append, System.IO.FileAccess.Write))
              {
                try
                {
                  fileStream.Write(attachmentData, 0, attachmentData.Length);
                }
                finally
                {
                  fileStream.Flush();
                  fileStream.Close();
                }
              }
            }
          }
        }
        emailMessageList.Add(emailMessage);
      }
    }
    return emailMessageList;
  }

  [NotNull]
  private static StringArray ReadMessagesPacket([NotNull] MailMan mailman)
  {
    return mailman.GetUidls() ?? throw new Exception(mailman.LastErrorText);
  }

  [NotNull]
  private EmailService.ConnectionData GetConnectionData(
    IUserSession session,
    Guid accountGuid,
    bool smtp,
    bool checkAccount)
  {
    foreach (KeyValuePair<EmailServer, Dictionary<EmailAccaunt, List<AccauntUserInfo>>> emailSetting in this._emailSettings)
    {
      foreach (KeyValuePair<EmailAccaunt, List<AccauntUserInfo>> keyValuePair in emailSetting.Value)
      {
        if (keyValuePair.Key.Guid.Equals(accountGuid))
        {
          if (checkAccount && keyValuePair.Value.All<AccauntUserInfo>((Func<AccauntUserInfo, bool>) (accountUserInfo => accountUserInfo.UserID != session.UserID)))
            throw new Exception(string.Format(LocalizationHolder.rm.GetString("Kernel_1026"), (object) keyValuePair.Key.Email));
          return new EmailService.ConnectionData(keyValuePair.Key.Email, keyValuePair.Key.Login, keyValuePair.Key.Password, smtp ? emailSetting.Key.SMTPServer : emailSetting.Key.POP3Server, smtp ? emailSetting.Key.SMPTPort : emailSetting.Key.POP3Port, smtp ? emailSetting.Key.SMPTConnectionType : emailSetting.Key.POP3ConnectionType);
        }
      }
    }
    throw new Exception(string.Format(LocalizationHolder.rm.GetString("Kernel_1022"), (object) accountGuid));
  }

  private class ConnectionData
  {
    public readonly string Email;
    public readonly string UserName;
    public readonly string Password;
    public readonly string Server;
    public readonly int Port;
    public readonly EmailConnectionTypes ConnectionType;

    public ConnectionData(
      string email,
      string userName,
      string password,
      string server,
      int port,
      EmailConnectionTypes connectionType)
    {
      this.Email = email;
      this.UserName = userName;
      this.Password = password;
      this.Server = server;
      this.Port = port;
      this.ConnectionType = connectionType;
    }
  }
}
