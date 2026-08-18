// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Design.BaseRecentList
// Assembly: Intermech.Workflow.Design, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: AF8177C8-0B57-4C67-8EA5-DF33FBCB2FBD
// Assembly location: D:\IPS\Client\Intermech.Workflow.Design.dll
// XML documentation location: D:\IPS\Client\Intermech.Workflow.Design.xml

using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Navigator.DBObjects;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Workflow.Design;

public class BaseRecentList
{
  protected int _maxcount;
  protected List<long> _list = new List<long>();
  protected string _name = "";
  protected List<string> _captions = new List<string>();
  private ICurrentUserAndRole _currentUserAndRoleService;
  private IDBConfigurations _configurations;

  public BaseRecentList(string name, int maxcount)
  {
    this._maxcount = maxcount;
    this._name = name;
  }

  public string Name => this._name;

  public List<string> Captions => this._captions;

  public List<long> IDs => this._list;

  public int Count => this._list.Count;

  public ICurrentUserAndRole CurrentUserAndRoleService
  {
    get
    {
      return this._currentUserAndRoleService ?? (this._currentUserAndRoleService = ApplicationServices.Container.GetService(typeof (ICurrentUserAndRole)) as ICurrentUserAndRole);
    }
  }

  public IDBConfigurations Configurations
  {
    get
    {
      return this._configurations ?? (this._configurations = ApplicationServices.Container.GetService(typeof (IDBConfigurations)) as IDBConfigurations);
    }
  }

  public void LoadCaptions(IUserSession userSession)
  {
    this.Captions.Clear();
    for (int index = this._list.Count - 1; index >= 0; --index)
    {
      ObjectSystemPropertiesEx systemPropertiesEx = userSession.GetObjectSystemPropertiesEx(this._list[index], false);
      if (systemPropertiesEx != null && this.CurrentUserAndRoleService != null)
      {
        if (!this.CurrentUserAndRoleService.IsAdmin)
        {
          bool flag = false;
          IDBAttribute objectAttributeById = userSession.GetObjectAttributeByID(this._list[index], wfConsts.AttrIsDebugID);
          if (objectAttributeById != null)
            flag = objectAttributeById.AsBoolean;
          if (systemPropertiesEx.IsBaseVersion && !flag)
          {
            string str = CaptionTransform.GetCaption(systemPropertiesEx.Caption, (long) systemPropertiesEx.VersionID);
            if (str.Trim() == "")
              str = "(Noname)";
            this.Captions.Insert(0, str);
          }
          else
            this._list.RemoveAt(index);
        }
        else
        {
          string str = CaptionTransform.GetCaption(systemPropertiesEx.Caption, (long) systemPropertiesEx.VersionID);
          if (str.Trim() == "")
            str = "(Noname)";
          this.Captions.Insert(0, str);
        }
      }
      else
        this._list.RemoveAt(index);
    }
  }

  public int Load(IUserSession userSession)
  {
    try
    {
      this._list.Clear();
      byte[] config_file = new byte[0];
      try
      {
        this.Configurations?.LoadConfigData(this.Name, out BlobInformation _, out config_file);
      }
      catch (Exception ex)
      {
        int num = (int) MessageBox.Show("Ошибка", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Hand);
      }
      if (config_file.Length == 0)
        return 0;
      MemoryStream memoryStream = new MemoryStream(config_file);
      memoryStream.Position = 0L;
      using (MemoryStream serializationStream = memoryStream)
        this._list = new BinaryFormatter().Deserialize((Stream) serializationStream) as List<long>;
      List<long> list = this._list;
      // ISSUE: explicit non-virtual call
      return list != null ? __nonvirtual (list.Count) : 0;
    }
    finally
    {
      this.LoadCaptions(userSession);
    }
  }

  public void Save()
  {
    using (MemoryStream serializationStream = new MemoryStream())
    {
      new BinaryFormatter().Serialize((Stream) serializationStream, (object) this._list);
      this.Configurations?.WriteConfigData(new BlobInformation(serializationStream.Length, serializationStream.Length, DateTime.Now, this.Name, ArcMethods.NotPacked, "b"), serializationStream.ToArray());
    }
  }
}
