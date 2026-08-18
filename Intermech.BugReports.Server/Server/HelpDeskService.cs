// Decompiled with JetBrains decompiler
// Type: Intermech.BugReports.Server.HelpDeskService
// Assembly: Intermech.BugReports.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: D5496885-D5AE-45E1-887A-E42A46AB4DD0
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.BugReports.Server.dll

using Intermech.Interfaces;
using Intermech.Interfaces.HelpDesk;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Net;
using System.Reflection;
using System.Text;

#nullable disable
namespace Intermech.BugReports.Server;

internal class HelpDeskService : LongLifeObject, IHelpDeskService
{
  private const string WorkOrderColumns = "workorder.WORKORDERID, workorder.TITLE";
  private const string AttachmentColumns = "sdeskattachment.ATTACHMENTID, sdeskattachment.ATTACHMENTNAME, sdeskattachment.ATTACHMENTKEY";
  private const string DescriptionColumns = "workordertodescription.FULLDESCRIPTION";
  private const string WorkOrderTableName = "workorder";
  private const string AttachmentTableName = "sdeskattachment";
  private const string WorkOrderAttachmentTableName = "workorderattachment";
  private const string LoginTableName = "aaalogin";
  private const string WorkOrderToDescription = "workordertodescription";
  private static readonly System.Configuration.Configuration Configuration = ConfigurationManager.OpenExeConfiguration(Assembly.GetExecutingAssembly().Location);
  private readonly string _connectionString = HelpDeskService.Configuration.ConnectionStrings.ConnectionStrings["ConnectionStringHelpDesk"].ConnectionString;
  private readonly string _urlStartPage = HelpDeskService.Configuration.AppSettings.Settings["URLStartPageHelpDesk"].Value;
  private readonly string _urlSecurity = HelpDeskService.Configuration.AppSettings.Settings["URLSecurityFormHelpDesk"].Value;
  private readonly string _urlFileDownload = HelpDeskService.Configuration.AppSettings.Settings["URLFileDownload"].Value;
  private const string SQuery = "j_username={0}&j_password={1}&domain=1&DOMAIN_NAME=IMDOMAIN&LDAPEnable=false&hidden=%D0%92%D1%8B%D0%B1%D0%B5%D1%80%D0%B8%D1%82%D0%B5+%D0%B4%D0%BE%D0%BC%D0%B5%D0%BD&hidden=%D0%94%D0%BB%D1%8F+%D0%B4%D0%BE%D0%BC%D0%B5%D0%BD%D0%B0&AdEnable=true&DomainCount=0&LocalAuth=No&LocalAuthWithDomain=IMDOMAIN&dynamicUserAddition_status=true&localAuthEnable=true&logonDomainName=IMDOMAIN&loginButton=%D0%92%D1%85%D0%BE%D0%B4";

  public bool ExistWorkOrder(long workOrderId) => this.Exist("workorder", workOrderId);

  public bool ExistAttachment(long workOrderId) => this.Exist("workorderattachment", workOrderId);

  private bool Exist(string tableName, long workOrderId)
  {
    string cmdText = string.Format("SELECT COUNT(*) FROM {0} WHERE {0}.WORKORDERID = {1}", (object) tableName, (object) workOrderId);
    using (SqlConnection sqlConnection = new SqlConnection(this._connectionString))
    {
      if (this.OpenConnection(sqlConnection))
      {
        using (SqlCommand sqlCommand = new SqlCommand(cmdText, sqlConnection))
        {
          if (Convert.ToInt64(sqlCommand.ExecuteScalar()) > 0L)
            return true;
        }
      }
    }
    return false;
  }

  public DataTable HelpDeskDataTable(long workOrderId, bool withAttachment)
  {
    string str1 = "workorder.WORKORDERID, workorder.TITLE";
    string str2 = "workorder";
    string str3 = $"WorkOrderToDescription.WORKORDERID = '{workOrderId}' AND ";
    string str4 = $"{str1}, {"workordertodescription.FULLDESCRIPTION"}";
    string str5 = $"{str2}, {"workordertodescription"}";
    string str6 = "";
    if (withAttachment)
    {
      str4 = $"{str4}, {"sdeskattachment.ATTACHMENTID, sdeskattachment.ATTACHMENTNAME, sdeskattachment.ATTACHMENTKEY"}";
      str5 = $"{str5}, {"sdeskattachment"}";
      str6 = $"sdeskattachment.ATTACHMENTID IN (SELECT workorderattachment.ATTACHMENTID FROM workorderattachment WHERE workorderattachment.WORKORDERID = '{workOrderId}') AND ";
    }
    string cmdText = $"SELECT DISTINCT {str4} FROM {str5} WHERE {str6}{str3}workorder.WORKORDERID = '{workOrderId}'";
    using (SqlConnection sqlConnection = new SqlConnection(this._connectionString))
    {
      if (this.OpenConnection(sqlConnection))
      {
        using (SqlCommand selectCommand = new SqlCommand(cmdText, sqlConnection))
        {
          DataTable dataTable = new DataTable("workOrder");
          new SqlDataAdapter(selectCommand).Fill(dataTable);
          this.CloseConnection(sqlConnection);
          return dataTable;
        }
      }
      this.CloseConnection(sqlConnection);
      return new DataTable("workOrder");
    }
  }

  public byte[] GetFile(int attachmentId, string key, string userName, string userPassword)
  {
    HttpWebRequest httpWebRequest1 = (HttpWebRequest) WebRequest.Create(this._urlStartPage);
    httpWebRequest1.UserAgent = "Mozilla/5.0 (Windows NT 6.1) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/37.0";
    httpWebRequest1.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8";
    httpWebRequest1.Headers.Add("Accept-Language", "ru");
    string header;
    using (HttpWebResponse response = (HttpWebResponse) httpWebRequest1.GetResponse())
      header = string.IsNullOrEmpty(response.Headers["Set-Cookie"]) ? "" : response.Headers["Set-Cookie"];
    HttpWebRequest httpWebRequest2 = (HttpWebRequest) WebRequest.Create(this._urlSecurity);
    httpWebRequest2.UserAgent = "Mozilla/5.0 (Windows NT 6.1) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/37.0";
    httpWebRequest2.Method = "POST";
    httpWebRequest2.ContentType = "application/x-www-form-urlencoded";
    httpWebRequest2.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8";
    if (!string.IsNullOrEmpty(header))
      httpWebRequest2.Headers.Add(HttpRequestHeader.Cookie, header);
    string s = $"j_username={userName}&j_password={userPassword}&domain=1&DOMAIN_NAME=IMDOMAIN&LDAPEnable=false&hidden=%D0%92%D1%8B%D0%B1%D0%B5%D1%80%D0%B8%D1%82%D0%B5+%D0%B4%D0%BE%D0%BC%D0%B5%D0%BD&hidden=%D0%94%D0%BB%D1%8F+%D0%B4%D0%BE%D0%BC%D0%B5%D0%BD%D0%B0&AdEnable=true&DomainCount=0&LocalAuth=No&LocalAuthWithDomain=IMDOMAIN&dynamicUserAddition_status=true&localAuthEnable=true&logonDomainName=IMDOMAIN&loginButton=%D0%92%D1%85%D0%BE%D0%B4";
    byte[] bytes = Encoding.GetEncoding(1251).GetBytes(s);
    httpWebRequest2.ContentLength = (long) bytes.Length;
    httpWebRequest2.GetRequestStream().Write(bytes, 0, bytes.Length);
    using (HttpWebResponse response = (HttpWebResponse) httpWebRequest2.GetResponse())
      header = string.IsNullOrEmpty(response.Headers["Set-Cookie"]) ? "" : response.Headers["Set-Cookie"];
    HttpWebRequest httpWebRequest3 = (HttpWebRequest) WebRequest.Create($"{this._urlFileDownload}&ID={attachmentId}&KEY={key}&delete=false");
    httpWebRequest3.UserAgent = "Mozilla/5.0 (Windows NT 6.1) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/37.0";
    httpWebRequest3.Method = "GET";
    httpWebRequest3.ContentType = "application/x-www-form-urlencoded";
    httpWebRequest3.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8";
    if (!string.IsNullOrEmpty(header))
      httpWebRequest3.Headers.Add(HttpRequestHeader.Cookie, header);
    using (HttpWebResponse response = (HttpWebResponse) httpWebRequest3.GetResponse())
      return this.StreamToBytes(response.GetResponseStream());
  }

  private byte[] StreamToBytes(Stream stream)
  {
    byte[] buffer = new byte[4096 /*0x1000*/];
    using (MemoryStream memoryStream = new MemoryStream())
    {
      int count;
      while ((count = stream.Read(buffer, 0, buffer.Length)) > 0)
        memoryStream.Write(buffer, 0, count);
      return memoryStream.ToArray();
    }
  }

  public Dictionary<bool, string> AuthenticationHelpDesk(string userName, string userPassword)
  {
    Dictionary<bool, string> dictionary = new Dictionary<bool, string>();
    if (!this.CheckLogin(userName))
    {
      dictionary.Add(false, "Неправильное \"Имя пользователя\"");
      return dictionary;
    }
    if (!this.CheckPassword(userName, userPassword))
    {
      dictionary.Add(false, "Неправильный \"Пароль\"");
      return dictionary;
    }
    dictionary.Add(true, "");
    return dictionary;
  }

  private bool CheckLogin(string login)
  {
    string cmdText = string.Format("SELECT aaalogin.LOGIN_ID FROM {0} WHERE {0}.NAME = '{1}'", (object) "aaalogin", (object) login);
    using (SqlConnection sqlConnection = new SqlConnection(this._connectionString))
    {
      if (this.OpenConnection(sqlConnection))
      {
        using (SqlCommand sqlCommand = new SqlCommand(cmdText, sqlConnection))
        {
          if (Convert.ToInt64(sqlCommand.ExecuteScalar()) > 0L)
            return true;
        }
      }
    }
    return false;
  }

  private bool CheckPassword(string userLogin, string password)
  {
    HttpWebRequest httpWebRequest1 = (HttpWebRequest) WebRequest.Create(this._urlStartPage);
    httpWebRequest1.UserAgent = "Mozilla/5.0 (Windows NT 6.1) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/37.0";
    httpWebRequest1.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8";
    httpWebRequest1.Headers.Add("Accept-Language", "ru");
    string header;
    using (HttpWebResponse response = (HttpWebResponse) httpWebRequest1.GetResponse())
      header = string.IsNullOrEmpty(response.Headers["Set-Cookie"]) ? "" : response.Headers["Set-Cookie"];
    HttpWebRequest httpWebRequest2 = (HttpWebRequest) WebRequest.Create(this._urlSecurity);
    httpWebRequest2.UserAgent = "Mozilla/5.0 (Windows NT 6.1) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/37.0";
    httpWebRequest2.Method = "POST";
    httpWebRequest2.ContentType = "application/x-www-form-urlencoded";
    httpWebRequest2.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8";
    httpWebRequest2.AllowAutoRedirect = false;
    if (!string.IsNullOrEmpty(header))
      httpWebRequest2.Headers.Add(HttpRequestHeader.Cookie, header);
    string s = $"j_username={userLogin}&j_password={password}&domain=1&DOMAIN_NAME=IMDOMAIN&LDAPEnable=false&hidden=%D0%92%D1%8B%D0%B1%D0%B5%D1%80%D0%B8%D1%82%D0%B5+%D0%B4%D0%BE%D0%BC%D0%B5%D0%BD&hidden=%D0%94%D0%BB%D1%8F+%D0%B4%D0%BE%D0%BC%D0%B5%D0%BD%D0%B0&AdEnable=true&DomainCount=0&LocalAuth=No&LocalAuthWithDomain=IMDOMAIN&dynamicUserAddition_status=true&localAuthEnable=true&logonDomainName=IMDOMAIN&loginButton=%D0%92%D1%85%D0%BE%D0%B4";
    byte[] bytes = Encoding.GetEncoding(1251).GetBytes(s);
    httpWebRequest2.ContentLength = (long) bytes.Length;
    httpWebRequest2.GetRequestStream().Write(bytes, 0, bytes.Length);
    using (HttpWebResponse response = (HttpWebResponse) httpWebRequest2.GetResponse())
    {
      using (StreamReader streamReader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
      {
        if (streamReader.ReadToEnd() == "")
          return true;
      }
    }
    return false;
  }

  private bool OpenConnection(SqlConnection sqlConnection)
  {
    try
    {
      sqlConnection.Open();
      return true;
    }
    catch (SqlException ex)
    {
      return false;
    }
  }

  private bool CloseConnection(SqlConnection sqlConnection)
  {
    try
    {
      sqlConnection.Close();
      return true;
    }
    catch (SqlException ex)
    {
      return false;
    }
  }
}
