// Decompiled with JetBrains decompiler
// Type: Intermech.ECO.Client.SeriesDatesForm
// Assembly: Intermech.ECO.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: BF6FF14F-986B-44C3-A04A-31D571D76B17
// Assembly location: D:\IPS\Client\Intermech.ECO.Client.dll

using DevExpress.IM.XtraTreeList;
using DevExpress.IM.XtraTreeList.Columns;
using DevExpress.IM.XtraTreeList.Nodes;
using Intermech.Client.Core;
using Intermech.Expert;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Compositions;
using Intermech.Interfaces.Contexts;
using Intermech.Interfaces.Sets;
using Intermech.Localization;
using Intermech.Navigator.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

#nullable disable
namespace Intermech.ECO.Client;

public class SeriesDatesForm : Form
{
  public SeriesDates sd;
  public long curIzdelId = -2;
  public bool noIzdels;
  public bool diapChanged;
  public bool addComplect;
  public bool writeAll;
  public Dictionary<long, SeriesDatesApplicability> changesList;
  public SeriesDatesApplicabilityCollection revSDAC;
  public List<long> objIdList;
  public Dictionary<int, int> objTypePictIndexes;
  public int sdacIndex = -1;
  private IContainer components;
  private Panel panel1;
  private Button btnOK;
  private Button btnCancel;
  private Label label1;
  private Button btnSelIzdel;
  private TreeList tlSerDates;
  private TreeListColumn colObjCaption;
  private TreeListColumn colDiap;
  private GroupBox groupBox1;
  private ImageList IL;
  private ListView lv;
  private ColumnHeader colHeaderObj;
  private ColumnHeader colHeaderID;
  private Label label7;
  private Button btnChangeDiap;
  private CheckBox cbAddComplect;
  private Label lblDiapText;
  private Button btnChange;
  private GroupBox groupBox2;
  private Panel panel2;
  private Panel panel3;
  private Splitter splitter1;
  private TreeListColumn colType;
  private ComboBox comboIzdel;
  private ImageList ilObjTypes;
  private Button btnDelIzdel;

  public SeriesDatesForm() => this.InitializeComponent();

  public DialogResult Execute(long editingContextID, ref SeriesDatesApplicabilityCollection sdac)
  {
    this.sd = new SeriesDates(editingContextID);
    this.PerformEditingContext();
    this.revSDAC = (SeriesDatesApplicabilityCollection) sdac.Clone();
    this.SynchronizeSDRev();
    this.FillMRUHeadIzdel();
    this.changesList = new Dictionary<long, SeriesDatesApplicability>();
    this.ShowCurrDiap();
    this.cbAddComplect.Checked = ECOPlugin.FindPlugin().eps.Current.WriteComplect;
    int num = (int) this.ShowDialog();
    if (num != 1)
      return (DialogResult) num;
    sdac = this.revSDAC;
    this.addComplect = this.cbAddComplect.Checked;
    this.WriteChanges();
    return (DialogResult) num;
  }

  public ISet getCurrDiaps
  {
    get
    {
      return this.sdacIndex < 0 || this.sdacIndex >= this.revSDAC.Items.Count ? (ISet) null : this.revSDAC.Items[this.sdacIndex].Set;
    }
  }

  private void PerformEditingContext()
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      this.sd.LoadContextObjects(sessionKeeper.Session);
      IUserSession session = sessionKeeper.Session;
      IDBEditingContextsObject editingContextsObject = (IDBEditingContextsObject) session.GetObject(this.sd.contextId, false);
      if (editingContextsObject != null)
      {
        this.objIdList = editingContextsObject.GetEditingContextsObjectContainer(true, true).GetVersionsID(true, session.UserID);
        for (int index = this.objIdList.Count - 1; index >= 0; --index)
        {
          long objId = this.objIdList[index];
          IDBObject objectActualCopy = session.GetObjectActualCopy(objId, false);
          if ((objectActualCopy == null ? 1 : (!SeriesDates.HasSeriesDatesApplicability(session, objectActualCopy.ObjectType) ? 1 : 0)) != 0)
            this.objIdList.RemoveAt(index);
        }
        foreach (long key in this.sd.izdList.Keys.ToList<long>())
          this.AddEverything(this.sd.izdList[key], session);
        if (this.sd.izdList.Count == 0)
        {
          SeriesDates.MainIzdel mi = new SeriesDates.MainIzdel(session, -1L);
          this.sd.izdList.Add(-1L, mi);
          this.AddEverything(mi, session);
          this.noIzdels = true;
        }
      }
      this.FillObjTypePicts();
    }
  }

  private void AddEverything(SeriesDates.MainIzdel mi, IUserSession session)
  {
    foreach (long objId in this.objIdList)
    {
      IDBObject objectActualCopy = session.GetObjectActualCopy(objId, false);
      if (objectActualCopy != null)
      {
        long key = Math.Abs(objId);
        if (!mi.allRecs.ContainsKey(key))
        {
          string caption = objectActualCopy.Caption;
          long objectId = objectActualCopy.ObjectID;
          string str = "??";
          IDBAttribute attributeByGuid = objectActualCopy.GetAttributeByGuid(new Guid("cad00770-306c-11d8-b4e9-00304f19f545"));
          if (attributeByGuid != null && attributeByGuid.Value != DBNull.Value)
            str = Convert.ToString(attributeByGuid.Value);
          string cNo = str;
          int objectType = objectActualCopy.ObjectType;
          string Desc = caption;
          SeriesDates.SeriesDatesRec sdrMain = new SeriesDates.SeriesDatesRec(objectId, cNo, objectType, (SeriesDatesApplicability) null, Desc);
          mi.allRecs.Add(key, sdrMain);
          this.ExpandAllVersions(session, objId, sdrMain);
        }
        else
        {
          SeriesDates.SeriesDatesRec allRec = mi.allRecs[key];
          this.ExpandAllVersions(session, objId, allRec);
        }
      }
    }
  }

  private void ExpandAllVersions(
    IUserSession session,
    long verId,
    SeriesDates.SeriesDatesRec sdrMain)
  {
    foreach (long allObjectVersions in session.GetAllObjectVersionsList(verId, false, false, false))
    {
      if (Math.Abs(allObjectVersions) != Math.Abs(verId))
      {
        IDBObject dbObject = session.GetObject(allObjectVersions, false);
        if (dbObject != null)
        {
          bool flag = false;
          foreach (SeriesDates.VersionRec otherVersion in sdrMain.otherVersions)
          {
            if (otherVersion.verId == allObjectVersions)
            {
              flag = true;
              break;
            }
          }
          if (!flag)
          {
            string cNo = "??";
            IDBAttribute attributeByGuid = dbObject.GetAttributeByGuid(new Guid("cad00770-306c-11d8-b4e9-00304f19f545"));
            if (attributeByGuid != null && attributeByGuid.Value != DBNull.Value)
              cNo = Convert.ToString(attributeByGuid.Value);
            SeriesDates.VersionRec versionRec = new SeriesDates.VersionRec(allObjectVersions, cNo, (SeriesDatesApplicability) null, dbObject.Caption);
            sdrMain.otherVersions.Add(versionRec);
          }
        }
      }
    }
  }

  private void SynchronizeSDRev()
  {
    List<long> longList = new List<long>();
    foreach (SeriesDatesApplicability datesApplicability in this.revSDAC.Items)
      longList.Add(Math.Abs(datesApplicability.MainObjectID));
    foreach (long key1 in this.sd.izdList.Keys.ToList<long>())
    {
      SeriesDates.MainIzdel izd = this.sd.izdList[key1];
      if (izd.allRecs.Count > 0)
      {
        long key2 = izd.allRecs.Keys.ToList<long>()[0];
        SeriesDatesApplicability sda = izd.allRecs[key2].sda;
        if (sda != null)
        {
          long num = Math.Abs(sda.MainObjectID);
          int index = longList.IndexOf(num);
          if (index < 0)
          {
            longList.Add(num);
            this.revSDAC.Items.Add(sda.Clone() as SeriesDatesApplicability);
          }
          else
            this.revSDAC.Items[index] = sda.Clone() as SeriesDatesApplicability;
        }
      }
    }
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      foreach (SeriesDatesApplicability datesApplicability in this.revSDAC.Items)
      {
        if (datesApplicability.MainObjectID != -1L && datesApplicability.MainObjectID != 0L)
        {
          long key = Math.Abs(datesApplicability.MainObjectID);
          if (!this.sd.izdList.ContainsKey(key))
          {
            SeriesDates.MainIzdel mainIzdel = new SeriesDates.MainIzdel(sessionKeeper.Session, datesApplicability.MainObjectID);
            this.sd.izdList.Add(key, mainIzdel);
          }
        }
      }
    }
  }

  private void FillMRUHeadIzdel()
  {
    this.comboIzdel.Items.Clear();
    this.comboIzdel.BeginUpdate();
    try
    {
      foreach (long key in this.sd.izdList.Keys.ToList<long>())
      {
        SeriesDates.MainIzdel izd = this.sd.izdList[key];
        this.comboIzdel.Items.Add(izd.izdelId == -1L ? (object) LocalizationHolder.rm.GetString("ECO.Client_276") : (object) $"{izd.Description} [{Convert.ToString(izd.izdelId)}]");
      }
    }
    finally
    {
      this.comboIzdel.EndUpdate();
    }
    bool flag = true;
    if (this.comboIzdel.Items.Count > 0 && this.comboIzdel.SelectedIndex != 0)
    {
      this.comboIzdel.SelectedIndex = 0;
      flag = false;
    }
    if (!flag)
      return;
    this.lv.Items.Clear();
    this.ShowCurrDiap();
  }

  private void mruHeadIzdel_SelectedIndexChanged(object sender, EventArgs e)
  {
    if (this.comboIzdel.SelectedIndex < 0)
      return;
    List<long> list = this.sd.izdList.Keys.ToList<long>();
    if (this.comboIzdel.SelectedIndex >= list.Count)
      return;
    long num = list[this.comboIzdel.SelectedIndex];
    if (num == this.curIzdelId)
      return;
    this.curIzdelId = num;
    this.FillSeriesDates();
    for (int index = 0; index < this.revSDAC.Items.Count; ++index)
    {
      SeriesDatesApplicability datesApplicability = this.revSDAC.Items[index];
      if (Math.Abs(this.curIzdelId) == Math.Abs(datesApplicability.MainObjectID))
      {
        this.sdacIndex = index;
        break;
      }
    }
    this.ShowCurrDiap();
  }

  private void FillSeriesDates()
  {
    this.tlSerDates.Nodes.Clear();
    this.tlSerDates.BeginUpdate();
    try
    {
      SeriesDates.MainIzdel izd = this.sd.izdList[this.curIzdelId];
      foreach (long key in izd.allRecs.Keys.ToList<long>())
      {
        if (key != -1L)
        {
          SeriesDates.SeriesDatesRec allRec = izd.allRecs[key];
          string str1 = $"{allRec.Description} [{Convert.ToString(allRec.verId)}] {{{allRec.changeNo}}}";
          string sdaString1 = this.GetSDAString(allRec.sda);
          string str2 = "";
          if (allRec.objTypeId == -1 && allRec.verId != -1L)
          {
            using (SessionKeeper sessionKeeper = new SessionKeeper())
            {
              IDBObject dbObject = sessionKeeper.Session.GetObject(allRec.verId, false);
              if (dbObject != null)
                allRec.objTypeId = dbObject.ObjectType;
            }
          }
          if (allRec.objTypeId != -1)
            str2 = MetaDataHelper.GetObjectTypeName(allRec.objTypeId);
          TreeListNode treeListNode1 = this.tlSerDates.AppendNode((object) new object[3]
          {
            (object) str1,
            (object) str2,
            (object) sdaString1
          }, -1, -1, -1, -1);
          treeListNode1.Tag = (object) new SeriesDatesForm.TreeKey(allRec.verId);
          int num = this.objTypePictIndexes.ContainsKey(allRec.objTypeId) ? this.objTypePictIndexes[allRec.objTypeId] : -1;
          treeListNode1.ImageIndex = num;
          treeListNode1.SelectImageIndex = num;
          for (int index = 0; index < allRec.otherVersions.Count; ++index)
          {
            SeriesDates.VersionRec otherVersion = allRec.otherVersions[index];
            string str3 = $"{otherVersion.Description} [{Convert.ToString(otherVersion.verId)}] {{{otherVersion.changeNo}}}";
            string sdaString2 = this.GetSDAString(otherVersion.sda);
            TreeListNode treeListNode2 = this.tlSerDates.AppendNode((object) new object[3]
            {
              (object) str3,
              (object) str2,
              (object) sdaString2
            }, treeListNode1.Id, -1, -1, -1);
            treeListNode2.Tag = (object) new SeriesDatesForm.TreeKey(allRec.verId, index);
            treeListNode2.ImageIndex = num;
            treeListNode2.SelectImageIndex = num;
          }
        }
      }
      this.sd.CheckOneIzdForErrors(izd, false);
      this.FillErrors();
    }
    finally
    {
      this.tlSerDates.EndUpdate();
    }
  }

  private string GetSDAString(SeriesDatesApplicability sda)
  {
    if (sda == null || sda.Set == null)
      return "";
    return sda.Applicability == ApplicabilityBy.Series ? this.GetSeriesRangeString(sda.Set as Intermech.Interfaces.Sets.Set<int>) : this.GetDatesRangeString(sda.Set as Intermech.Interfaces.Sets.Set<DateTime>);
  }

  private string GetSeriesRangeString(Intermech.Interfaces.Sets.Set<int> set)
  {
    StringBuilder stringBuilder = new StringBuilder();
    for (int index = 0; index < set.Ranges.Count; ++index)
    {
      Int32Range range = (Int32Range) set.Ranges[index];
      if (range.MinValue == int.MinValue || range.MaxValue == int.MaxValue)
      {
        if (range.IsLeftOpen && !range.IsRightOpen)
          stringBuilder.Append(LocalizationHolder.rm.GetString("ECO.Client_271") + Convert.ToString(range.MaxValue));
        if (!range.IsLeftOpen && range.IsRightOpen)
          stringBuilder.Append(LocalizationHolder.rm.GetString("ECO.Client_268") + Convert.ToString(range.MinValue));
      }
      else
        stringBuilder.Append($"{LocalizationHolder.rm.GetString("ECO.Client_268")}{Convert.ToString(range.MinValue)} {LocalizationHolder.rm.GetString("ECO.Client_271")}{Convert.ToString(range.MaxValue)}");
      if (index < set.Ranges.Count - 1)
        stringBuilder.Append(", ");
    }
    return stringBuilder.ToString();
  }

  private string GetDatesRangeString(Intermech.Interfaces.Sets.Set<DateTime> set)
  {
    StringBuilder stringBuilder = new StringBuilder();
    for (int index = 0; index < set.Ranges.Count; ++index)
    {
      DateTimeRange range = (DateTimeRange) set.Ranges[index];
      if (range.IsOpen)
      {
        if (range.IsLeftOpen && !range.IsRightOpen)
          stringBuilder.Append(LocalizationHolder.rm.GetString("ECO.Client_271") + range.MaxValue.ToShortDateString());
        if (!range.IsLeftOpen && range.IsRightOpen)
          stringBuilder.Append(LocalizationHolder.rm.GetString("ECO.Client_268") + range.MinValue.ToShortDateString());
      }
      else
        stringBuilder.Append($"{LocalizationHolder.rm.GetString("ECO.Client_268")}{range.MinValue.ToShortDateString()} {LocalizationHolder.rm.GetString("ECO.Client_271")}{range.MaxValue.ToShortDateString()}");
      if (index < set.Ranges.Count - 1)
        stringBuilder.Append(", ");
    }
    return stringBuilder.ToString();
  }

  private void ShowCurrDiap()
  {
    ISet getCurrDiaps = this.getCurrDiaps;
    StringBuilder stringBuilder1 = new StringBuilder();
    if (getCurrDiaps is Intermech.Interfaces.Sets.Set<int>)
    {
      this.cbAddComplect.Visible = true;
      Intermech.Interfaces.Sets.Set<int> set = getCurrDiaps as Intermech.Interfaces.Sets.Set<int>;
      for (int index = 0; index < set.Ranges.Count; ++index)
      {
        Int32Range range = (Int32Range) set.Ranges[index];
        if (range.MinValue == int.MinValue || range.MaxValue == int.MaxValue)
        {
          if (range.IsLeftOpen && !range.IsRightOpen)
          {
            stringBuilder1.Append(LocalizationHolder.rm.GetString("ECO.Client_271") + Convert.ToString(range.MaxValue));
            if (this.cbAddComplect.Checked)
              stringBuilder1.Append(" " + LocalizationHolder.rm.GetString("ECO.Client_269"));
          }
          if (!range.IsLeftOpen && range.IsRightOpen)
          {
            stringBuilder1.Append(LocalizationHolder.rm.GetString("ECO.Client_268") + Convert.ToString(range.MinValue));
            if (this.cbAddComplect.Checked)
              stringBuilder1.Append(" " + LocalizationHolder.rm.GetString("ECO.Client_270"));
          }
        }
        else
        {
          stringBuilder1.Append($"{LocalizationHolder.rm.GetString("ECO.Client_268")}{Convert.ToString(range.MinValue)} {LocalizationHolder.rm.GetString("ECO.Client_271")}{Convert.ToString(range.MaxValue)}");
          if (this.cbAddComplect.Checked)
            stringBuilder1.Append(" " + LocalizationHolder.rm.GetString("ECO.Client_269"));
        }
        if (index < set.Ranges.Count - 1)
          stringBuilder1.Append(", ");
      }
    }
    if (getCurrDiaps is Intermech.Interfaces.Sets.Set<DateTime>)
    {
      this.cbAddComplect.Visible = false;
      Intermech.Interfaces.Sets.Set<DateTime> set = getCurrDiaps as Intermech.Interfaces.Sets.Set<DateTime>;
      for (int index = 0; index < set.Ranges.Count; ++index)
      {
        DateTimeRange range = (DateTimeRange) set.Ranges[index];
        DateTime dateTime;
        if (range.IsOpen)
        {
          if (range.IsLeftOpen && !range.IsRightOpen)
          {
            StringBuilder stringBuilder2 = stringBuilder1;
            string str1 = LocalizationHolder.rm.GetString("ECO.Client_271");
            dateTime = range.MaxValue;
            string shortDateString = dateTime.ToShortDateString();
            string str2 = str1 + shortDateString;
            stringBuilder2.Append(str2);
          }
          if (!range.IsLeftOpen && range.IsRightOpen)
          {
            StringBuilder stringBuilder3 = stringBuilder1;
            string str3 = LocalizationHolder.rm.GetString("ECO.Client_268");
            dateTime = range.MinValue;
            string shortDateString = dateTime.ToShortDateString();
            string str4 = str3 + shortDateString;
            stringBuilder3.Append(str4);
          }
        }
        else
        {
          StringBuilder stringBuilder4 = stringBuilder1;
          string[] strArray = new string[5]
          {
            LocalizationHolder.rm.GetString("ECO.Client_268"),
            null,
            null,
            null,
            null
          };
          dateTime = range.MinValue;
          strArray[1] = dateTime.ToShortDateString();
          strArray[2] = " ";
          strArray[3] = LocalizationHolder.rm.GetString("ECO.Client_271");
          dateTime = range.MaxValue;
          strArray[4] = dateTime.ToShortDateString();
          string str = string.Concat(strArray);
          stringBuilder4.Append(str);
        }
        if (index < set.Ranges.Count - 1)
          stringBuilder1.Append(", ");
      }
    }
    this.lblDiapText.Text = stringBuilder1.ToString();
  }

  private void FillObjTypePicts()
  {
    if (this.objTypePictIndexes == null)
      this.objTypePictIndexes = new Dictionary<int, int>();
    else
      this.objTypePictIndexes.Clear();
    ECOPlugin.serviceProvider.GetService(typeof (ICategoryTypeIconService));
    foreach (long key in this.sd.izdList.Keys.ToList<long>())
    {
      foreach (SeriesDates.SeriesDatesRec seriesDatesRec in this.sd.izdList[key].allRecs.Values)
      {
        int objTypeId = seriesDatesRec.objTypeId;
        if (objTypeId != -1 && !this.objTypePictIndexes.ContainsKey(objTypeId))
        {
          Image image32x16 = Images32x16_Cache.GetImage32x16(4, objTypeId, (NavigatorTreeNode) null);
          this.objTypePictIndexes.Add(objTypeId, this.ilObjTypes.Images.Count);
          this.ilObjTypes.Images.Add(image32x16);
        }
      }
    }
  }

  private void FillErrors()
  {
    this.lv.BeginUpdate();
    this.lv.Items.Clear();
    try
    {
      foreach (SeriesDates.ErrInfo err in this.sd.errList)
      {
        ListViewItem listViewItem = (ListViewItem) null;
        List<string> stringList = new List<string>();
        stringList.Add(Convert.ToString(err.primaryVerId));
        if (err.errMessage == "")
          err.ComposeMessage(this.sd);
        stringList.Add(err.errMessage);
        switch (err.errType)
        {
          case SeriesDates.SeriesDatesErrType.sdeMixedSerieDate:
            listViewItem = new ListViewItem(stringList.ToArray(), 3);
            break;
          case SeriesDates.SeriesDatesErrType.sdeApplicabilityIntersects:
            listViewItem = new ListViewItem(stringList.ToArray(), 4);
            break;
          case SeriesDates.SeriesDatesErrType.sdeIntersects:
            listViewItem = new ListViewItem(stringList.ToArray(), 4);
            break;
          case SeriesDates.SeriesDatesErrType.sdeEmptyDiap:
            listViewItem = new ListViewItem(stringList.ToArray(), 6);
            break;
          case SeriesDates.SeriesDatesErrType.sdeHole:
            listViewItem = new ListViewItem(stringList.ToArray(), 5);
            break;
        }
        if (listViewItem != null)
        {
          listViewItem.Tag = (object) err;
          this.lv.Items.Add(listViewItem);
        }
      }
    }
    finally
    {
      this.lv.EndUpdate();
    }
  }

  private SeriesDates.ErrInfo CurrErrInfo()
  {
    if (this.lv.SelectedIndices == null || this.lv.SelectedIndices.Count == 0)
      return (SeriesDates.ErrInfo) null;
    int selectedIndex = this.lv.SelectedIndices[0];
    return selectedIndex < 0 || selectedIndex >= this.sd.errList.Count ? (SeriesDates.ErrInfo) null : this.sd.errList[selectedIndex];
  }

  private ISet GetErrSet(SeriesDates.ErrInfo ei)
  {
    SeriesDates.MainIzdel izd = this.sd.izdList[Math.Abs(ei.mainObjectId)];
    if (izd == null)
      return (ISet) null;
    SeriesDates.SeriesDatesRec allRec = izd.allRecs[Math.Abs(ei.primaryVerId)];
    if (allRec == null)
      return (ISet) null;
    return ei.secondIndex != -1 ? allRec.otherVersions[ei.secondIndex].sda.Set : allRec.sda.Set;
  }

  private void btnSelIzdel_Click(object sender, EventArgs e)
  {
    if (ExpertConsts.Consts == null)
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
        ExpertConsts.Init(sessionKeeper.Session);
    }
    long[] numArray = Intermech.Navigator.SelectionWindow.SelectObjects(LocalizationHolder.rm.GetString("ECO.Client_263"), LocalizationHolder.rm.GetString("ECO.Client_264"), ExpertConsts.Consts.objIzdelie, SelectionOptions.Default);
    if (numArray == null || numArray.Length == 0)
      return;
    long num = numArray[0];
    List<long> list1 = this.sd.izdList.Keys.ToList<long>();
    if (list1.Contains(Math.Abs(num)))
    {
      if (this.curIzdelId == Math.Abs(num))
        return;
      this.comboIzdel.SelectedIndex = list1.IndexOf(Math.Abs(num));
    }
    else
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        SeriesDates.MainIzdel mi = new SeriesDates.MainIzdel(sessionKeeper.Session, num);
        this.sd.izdList.Add(Math.Abs(num), mi);
        this.AddEverything(mi, sessionKeeper.Session);
        List<long> list2 = this.sd.izdList.Keys.ToList<long>();
        string str = $"{mi.Description} [{Convert.ToString(mi.izdelId)}]";
        this.comboIzdel.BeginUpdate();
        try
        {
          if (this.noIzdels)
          {
            this.comboIzdel.Items.RemoveAt(0);
            this.noIzdels = false;
            this.sd.izdList.Remove(-1L);
            list2.Remove(-1L);
          }
          int index = list2.IndexOf(Math.Abs(num));
          SeriesDatesApplicability datesApplicability = new SeriesDatesApplicability(mainObjectID: num);
          this.revSDAC.Items.Insert(index, datesApplicability);
          this.comboIzdel.Items.Insert(index, (object) str);
          this.comboIzdel.SelectedIndex = index;
        }
        finally
        {
          this.comboIzdel.EndUpdate();
        }
      }
    }
  }

  private void btnDelIzdel_Click(object sender, EventArgs e)
  {
    int selectedIndex = this.comboIzdel.SelectedIndex;
    if (selectedIndex < 0)
      return;
    SeriesDates.MainIzdel izd = this.sd.izdList[this.curIzdelId];
    if (izd.izdelId == -1L || MessageBox.Show(string.Format(LocalizationHolder.rm.GetString("ECO.Client_277"), (object) izd.Description, (object) izd.izdelId), LocalizationHolder.rm.GetString("ECO.Client_48"), MessageBoxButtons.OKCancel) != DialogResult.OK)
      return;
    this.sd.izdList.Remove(this.curIzdelId);
    if (this.sd.izdList.Count == 0)
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        SeriesDates.MainIzdel mi = new SeriesDates.MainIzdel(sessionKeeper.Session, -1L);
        this.sd.izdList.Add(-1L, mi);
        this.AddEverything(mi, sessionKeeper.Session);
        this.noIzdels = true;
      }
    }
    using (new SessionKeeper())
    {
      for (int index = 0; index < this.revSDAC.Items.Count; ++index)
      {
        if (this.revSDAC.Items[index].MainObjectID == this.curIzdelId)
        {
          this.revSDAC.Items.RemoveAt(index);
          break;
        }
      }
    }
    if (this.noIzdels)
    {
      this.FillMRUHeadIzdel();
    }
    else
    {
      this.comboIzdel.Items.RemoveAt(selectedIndex);
      if (selectedIndex < this.comboIzdel.Items.Count)
        this.comboIzdel.SelectedIndex = selectedIndex;
      else
        this.comboIzdel.SelectedIndex = this.comboIzdel.Items.Count - 1;
    }
    this.writeAll = true;
  }

  private void btnChangeDiap_Click(object sender, EventArgs e)
  {
    ISet getCurrDiaps = this.getCurrDiaps;
    if (this.curIzdelId == -1L || this.noIzdels)
    {
      int num = (int) MessageBox.Show(LocalizationHolder.rm.GetString("ECO.Client_272"), LocalizationHolder.rm.GetString("ECO.Client_117"), MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
    }
    else
    {
      if (!new SerDateDiap().Execute(ref getCurrDiaps))
        return;
      this.diapChanged = true;
      ApplicabilityBy applicability = getCurrDiaps is Intermech.Interfaces.Sets.Set<int> ? ApplicabilityBy.Series : ApplicabilityBy.Date;
      if (this.sdacIndex >= 0 && this.sdacIndex < this.revSDAC.Items.Count)
      {
        SeriesDatesApplicability datesApplicability = this.revSDAC.Items[this.sdacIndex];
        datesApplicability.Applicability = applicability;
        datesApplicability.Set = getCurrDiaps;
      }
      else
      {
        this.revSDAC.Items.Add(new SeriesDatesApplicability(applicability, this.curIzdelId, getCurrDiaps));
        this.sdacIndex = this.revSDAC.Items.Count - 1;
      }
      SeriesDates.MainIzdel izd = this.sd.izdList[this.curIzdelId];
      foreach (long key in izd.allRecs.Keys.ToList<long>())
      {
        SeriesDates.SeriesDatesRec allRec = izd.allRecs[key];
        SeriesDatesApplicability datesApplicability = new SeriesDatesApplicability(applicability, this.curIzdelId, getCurrDiaps);
        allRec.sda = datesApplicability;
        if (this.changesList.ContainsKey(allRec.verId))
          this.changesList[allRec.verId] = datesApplicability;
        else
          this.changesList.Add(allRec.verId, datesApplicability);
      }
      this.ShowCurrDiap();
      this.FillSeriesDates();
    }
  }

  private void lv_SelectedIndexChanged(object sender, EventArgs e)
  {
  }

  private void btnChange_Click(object sender, EventArgs e)
  {
    if (this.tlSerDates.Selection == null || this.tlSerDates.Selection.Count == 0)
      return;
    if (this.curIzdelId == -1L || this.noIzdels)
    {
      int num1 = (int) MessageBox.Show(LocalizationHolder.rm.GetString("ECO.Client_272"), LocalizationHolder.rm.GetString("ECO.Client_117"), MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
    }
    else
    {
      TreeListNode treeListNode = this.tlSerDates.Selection[0];
      SeriesDatesForm.TreeKey tag = (SeriesDatesForm.TreeKey) treeListNode.Tag;
      SeriesDates.MainIzdel izd = this.sd.izdList[this.curIzdelId];
      SeriesDates.SeriesDatesRec allRec = izd.allRecs[Math.Abs(tag.primaryVerId)];
      ISet set = (ISet) null;
      SeriesDatesApplicability datesApplicability = tag.secondIndex != -1 ? allRec.otherVersions[tag.secondIndex].sda : allRec.sda;
      if (datesApplicability != null)
        set = datesApplicability.Set;
      if (tag.secondIndex == -1)
      {
        int num2 = (int) MessageBox.Show(LocalizationHolder.rm.GetString("ECO.Client_273"), LocalizationHolder.rm.GetString("ECO.Client_117"), MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
      }
      else
      {
        if (!new SerDateDiap().Execute(ref set))
          return;
        ApplicabilityBy applicability = set is Intermech.Interfaces.Sets.Set<int> ? ApplicabilityBy.Series : ApplicabilityBy.Date;
        SeriesDates.VersionRec otherVersion1 = allRec.otherVersions[tag.secondIndex];
        if (otherVersion1.sda == null)
        {
          otherVersion1.sda = new SeriesDatesApplicability(applicability, this.curIzdelId, set);
        }
        else
        {
          otherVersion1.sda.Applicability = applicability;
          otherVersion1.sda.Set = set;
        }
        treeListNode[(object) 2] = (object) set.DisplayString;
        treeListNode.StateImageIndex = 2;
        SeriesDates.VersionRec otherVersion2 = allRec.otherVersions[tag.secondIndex];
        if (!this.changesList.ContainsKey(otherVersion2.verId))
          this.changesList.Add(otherVersion2.verId, otherVersion2.sda);
        else
          this.changesList[otherVersion2.verId] = otherVersion2.sda;
        this.sd.CheckOneIzdForErrors(izd, false);
        this.FillErrors();
      }
    }
  }

  private void cbAddComplect_CheckedChanged(object sender, EventArgs e) => this.ShowCurrDiap();

  private bool WriteChanges()
  {
    if (this.noIzdels && !this.writeAll)
      return true;
    bool flag1 = true;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      if (sessionKeeper.Session.GetCustomService(typeof (IDBTransactions)) is IDBTransactions customService)
        customService.StartTransaction();
      bool flag2 = true;
      try
      {
        if (this.writeAll)
        {
          Dictionary<long, SeriesDatesApplicabilityCollection> dictionary = new Dictionary<long, SeriesDatesApplicabilityCollection>();
          foreach (long key1 in this.sd.izdList.Keys.ToList<long>())
          {
            SeriesDates.MainIzdel izd = this.sd.izdList[key1];
            foreach (long key2 in izd.allRecs.Keys.ToList<long>())
            {
              SeriesDatesApplicabilityCollection applicabilityCollection1 = (SeriesDatesApplicabilityCollection) null;
              if (!dictionary.TryGetValue(Math.Abs(key2), out applicabilityCollection1))
              {
                applicabilityCollection1 = new SeriesDatesApplicabilityCollection();
                dictionary[Math.Abs(key2)] = applicabilityCollection1;
              }
              SeriesDates.SeriesDatesRec allRec = izd.allRecs[key2];
              applicabilityCollection1.Items.Add(allRec.sda);
              foreach (SeriesDates.VersionRec otherVersion in allRec.otherVersions)
              {
                SeriesDatesApplicabilityCollection applicabilityCollection2 = (SeriesDatesApplicabilityCollection) null;
                if (!dictionary.TryGetValue(Math.Abs(otherVersion.verId), out applicabilityCollection2))
                {
                  applicabilityCollection2 = new SeriesDatesApplicabilityCollection();
                  dictionary[Math.Abs(otherVersion.verId)] = applicabilityCollection2;
                }
                applicabilityCollection2.Items.Add(otherVersion.sda);
              }
            }
          }
          foreach (long key in dictionary.Keys)
          {
            SeriesDatesApplicabilityCollection applicabilityCollection = dictionary[key];
            if (applicabilityCollection != null)
              applicabilityCollection.SaveToObject((IDBAttributable) ((sessionKeeper.Session.GetObject(-key, false) ?? sessionKeeper.Session.GetObject(key, false)) ?? throw new Exception(string.Format(LocalizationHolder.rm.GetString("ECO.Client_265"), (object) key))));
          }
        }
        else
        {
          foreach (long num in this.changesList.Keys.ToList<long>())
          {
            SeriesDatesApplicabilityCollection applicabilityCollection = new SeriesDatesApplicabilityCollection();
            applicabilityCollection.Items.Add(this.changesList[num]);
            foreach (long key3 in this.sd.izdList.Keys.ToList<long>())
            {
              if (key3 != this.curIzdelId)
              {
                SeriesDates.MainIzdel izd = this.sd.izdList[key3];
                foreach (long key4 in izd.allRecs.Keys.ToList<long>())
                {
                  if (key4 == Math.Abs(num))
                  {
                    applicabilityCollection.Items.Add(izd.allRecs[key4].sda);
                  }
                  else
                  {
                    foreach (SeriesDates.VersionRec otherVersion in izd.allRecs[key4].otherVersions)
                    {
                      if (Math.Abs(otherVersion.verId) == Math.Abs(num))
                        applicabilityCollection.Items.Add(otherVersion.sda);
                    }
                  }
                }
              }
            }
            IDBObject dbObject = sessionKeeper.Session.GetObjectActualCopy(num, false) ?? sessionKeeper.Session.GetObject(Math.Abs(num), false);
            if (dbObject == null || !applicabilityCollection.SaveToObject((IDBAttributable) dbObject))
              throw new Exception(string.Format(LocalizationHolder.rm.GetString("ECO.Client_265"), (object) num));
          }
        }
        IDBAttribute dbAttribute = sessionKeeper.Session.GetObject(this.sd.contextId).Attributes.AddAttribute(RevHelper.idAttrRevSeriesDates, false);
        if (dbAttribute != null)
          dbAttribute.Value = (object) this.revSDAC.ToString();
      }
      catch (Exception ex)
      {
        flag2 = false;
        throw;
      }
      finally
      {
        if (customService != null)
        {
          if (flag2)
          {
            customService.Commit();
          }
          else
          {
            customService.Rollback();
            flag1 = false;
          }
        }
      }
    }
    return flag1;
  }

  internal void ReportSDAC(SeriesDatesApplicabilityCollection sdac, IUserSession session)
  {
    session.EventLog.AddToTrace("========= sdac report ==========", 0, "D:\\SER_TRACE.TXT");
    session.EventLog.AddToTrace("Count = " + Convert.ToString(sdac.Items.Count), 0, "D:\\SER_TRACE.TXT");
    foreach (SeriesDatesApplicability datesApplicability in sdac.Items)
      session.EventLog.AddToTrace($"mainObjId = {Convert.ToString(datesApplicability.MainObjectID)} toString = {datesApplicability.ToString()}", 0, "D:\\SER_TRACE.TXT");
    session.EventLog.AddToTrace("ToString() = " + Convert.ToString(sdac.ToString()), 0, "D:\\SER_TRACE.TXT");
    session.EventLog.AddToTrace("========= sdac end rep ==========", 0, "D:\\SER_TRACE.TXT");
  }

  private void SeriesDatesForm_HelpButtonClicked(object sender, CancelEventArgs e)
  {
    HelpProvidersClass.ShowHelpTopic(2974);
  }

  private void SeriesDatesForm_HelpRequested(object sender, HelpEventArgs hlpevent)
  {
    HelpProvidersClass.ShowHelpTopic(2974);
  }

  private void tlSerDates_ShowTreeListMenu(object sender, TreeListMenuEventArgs e)
  {
    e.Allow = false;
  }

  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  private void InitializeComponent()
  {
    this.components = (IContainer) new System.ComponentModel.Container();
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (SeriesDatesForm));
    ListViewItem listViewItem1 = new ListViewItem(new string[3]
    {
      "363462536",
      "111",
      "222"
    }, 1);
    ListViewItem listViewItem2 = new ListViewItem(new string[3]
    {
      "79679679",
      "573",
      "65685"
    }, 2);
    ListViewItem listViewItem3 = new ListViewItem("rtyerye", 0);
    ListViewItem listViewItem4 = new ListViewItem("ghkgkh", 3);
    this.panel1 = new Panel();
    this.label7 = new Label();
    this.btnOK = new Button();
    this.btnCancel = new Button();
    this.label1 = new Label();
    this.btnSelIzdel = new Button();
    this.tlSerDates = new TreeList();
    this.colObjCaption = new TreeListColumn();
    this.colType = new TreeListColumn();
    this.colDiap = new TreeListColumn();
    this.ilObjTypes = new ImageList(this.components);
    this.IL = new ImageList(this.components);
    this.groupBox1 = new GroupBox();
    this.btnChangeDiap = new Button();
    this.cbAddComplect = new CheckBox();
    this.lblDiapText = new Label();
    this.lv = new ListView();
    this.colHeaderID = new ColumnHeader();
    this.colHeaderObj = new ColumnHeader();
    this.btnChange = new Button();
    this.groupBox2 = new GroupBox();
    this.panel2 = new Panel();
    this.btnDelIzdel = new Button();
    this.comboIzdel = new ComboBox();
    this.panel3 = new Panel();
    this.splitter1 = new Splitter();
    this.panel1.SuspendLayout();
    this.tlSerDates.BeginInit();
    this.groupBox1.SuspendLayout();
    this.groupBox2.SuspendLayout();
    this.panel2.SuspendLayout();
    this.panel3.SuspendLayout();
    this.SuspendLayout();
    this.panel1.Controls.Add((Control) this.label7);
    this.panel1.Controls.Add((Control) this.btnOK);
    this.panel1.Controls.Add((Control) this.btnCancel);
    this.panel1.Dock = DockStyle.Bottom;
    this.panel1.Location = new Point(0, 466);
    this.panel1.Name = "panel1";
    this.panel1.Size = new Size(833, 30);
    this.panel1.TabIndex = 0;
    this.label7.AutoSize = true;
    this.label7.Location = new Point(12, 8);
    this.label7.Name = "label7";
    this.label7.Size = new Size(373, 13);
    this.label7.TabIndex = 2;
    this.label7.Text = "Все изменения объектов будут произведены после нажатия кнопки ОК";
    this.btnOK.Anchor = AnchorStyles.Top | AnchorStyles.Right;
    this.btnOK.DialogResult = DialogResult.OK;
    this.btnOK.Location = new Point(665, 3);
    this.btnOK.Name = "btnOK";
    this.btnOK.Size = new Size(75, 23);
    this.btnOK.TabIndex = 1;
    this.btnOK.Text = "ОК";
    this.btnOK.UseVisualStyleBackColor = true;
    this.btnCancel.Anchor = AnchorStyles.Top | AnchorStyles.Right;
    this.btnCancel.DialogResult = DialogResult.Cancel;
    this.btnCancel.Location = new Point(746, 3);
    this.btnCancel.Name = "btnCancel";
    this.btnCancel.Size = new Size(75, 23);
    this.btnCancel.TabIndex = 0;
    this.btnCancel.Text = "Отмена";
    this.btnCancel.UseVisualStyleBackColor = true;
    this.label1.AutoSize = true;
    this.label1.Location = new Point(5, 8);
    this.label1.Name = "label1";
    this.label1.Size = new Size(100, 13);
    this.label1.TabIndex = 1;
    this.label1.Text = "Головное изделие";
    this.btnSelIzdel.Anchor = AnchorStyles.Top | AnchorStyles.Right;
    this.btnSelIzdel.Location = new Point(715, 22);
    this.btnSelIzdel.Name = "btnSelIzdel";
    this.btnSelIzdel.Size = new Size(75, 23);
    this.btnSelIzdel.TabIndex = 3;
    this.btnSelIzdel.Text = "Выбор...";
    this.btnSelIzdel.UseVisualStyleBackColor = true;
    this.btnSelIzdel.Click += new EventHandler(this.btnSelIzdel_Click);
    this.tlSerDates.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
    this.tlSerDates.BehaviorOptions = BehaviorOptionsFlags.MoveOnEdit | BehaviorOptionsFlags.ExpandNodeOnDrag | BehaviorOptionsFlags.ShowToolTips | BehaviorOptionsFlags.ResizeNodes | BehaviorOptionsFlags.AutoSelectAllInEditor | BehaviorOptionsFlags.AutoNodeHeight | BehaviorOptionsFlags.AutoChangeParent | BehaviorOptionsFlags.CloseEditorOnLostFocus | BehaviorOptionsFlags.KeepSelectedOnClick | BehaviorOptionsFlags.SmartMouseHover;
    this.tlSerDates.Columns.AddRange(new TreeListColumn[3]
    {
      this.colObjCaption,
      this.colType,
      this.colDiap
    });
    this.tlSerDates.Location = new Point(7, 19);
    this.tlSerDates.Name = "tlSerDates";
    this.tlSerDates.SelectImageList = this.ilObjTypes;
    this.tlSerDates.Size = new Size(796, 156);
    this.tlSerDates.TabIndex = 5;
    this.tlSerDates.ShowTreeListMenu += new TreeListMenuEventHandler(this.tlSerDates_ShowTreeListMenu);
    this.colObjCaption.Caption = "Заголовок объекта";
    this.colObjCaption.FieldName = "treeListColumn1";
    this.colObjCaption.Name = "colObjCaption";
    this.colObjCaption.Options = ColumnOptions.CanResized;
    this.colObjCaption.VisibleIndex = 0;
    this.colObjCaption.Width = 330;
    this.colType.Caption = "Тип объекта";
    this.colType.FieldName = "treeListColumn1";
    this.colType.Name = "colType";
    this.colType.VisibleIndex = 1;
    this.colDiap.Caption = "Диапазон";
    this.colDiap.FieldName = "treeListColumn1";
    this.colDiap.Name = "colDiap";
    this.colDiap.Options = ColumnOptions.CanResized;
    this.colDiap.VisibleIndex = 2;
    this.colDiap.Width = 30;
    this.ilObjTypes.ColorDepth = ColorDepth.Depth8Bit;
    this.ilObjTypes.ImageSize = new Size(32 /*0x20*/, 16 /*0x10*/);
    this.ilObjTypes.TransparentColor = Color.Transparent;
    this.IL.ImageStream = (ImageListStreamer) componentResourceManager.GetObject("IL.ImageStream");
    this.IL.TransparentColor = Color.Magenta;
    this.IL.Images.SetKeyName(0, "error.bmp");
    this.IL.Images.SetKeyName(1, "Warning.bmp");
    this.IL.Images.SetKeyName(2, "Save.bmp");
    this.IL.Images.SetKeyName(3, "Mixed.bmp");
    this.IL.Images.SetKeyName(4, "Intersect.bmp");
    this.IL.Images.SetKeyName(5, "gap.bmp");
    this.IL.Images.SetKeyName(6, "Question.bmp");
    this.IL.Images.SetKeyName(7, "удалить.bmp");
    this.groupBox1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
    this.groupBox1.Controls.Add((Control) this.btnChangeDiap);
    this.groupBox1.Controls.Add((Control) this.cbAddComplect);
    this.groupBox1.Controls.Add((Control) this.lblDiapText);
    this.groupBox1.Location = new Point(10, 4);
    this.groupBox1.Name = "groupBox1";
    this.groupBox1.Size = new Size(811, 44);
    this.groupBox1.TabIndex = 6;
    this.groupBox1.TabStop = false;
    this.groupBox1.Text = "Диапазон действия извещения и входящих в него версий";
    this.btnChangeDiap.Anchor = AnchorStyles.Top | AnchorStyles.Right;
    this.btnChangeDiap.Location = new Point(687, 14);
    this.btnChangeDiap.Name = "btnChangeDiap";
    this.btnChangeDiap.Size = new Size(118, 23);
    this.btnChangeDiap.TabIndex = 2;
    this.btnChangeDiap.Text = "Изменить...";
    this.btnChangeDiap.UseVisualStyleBackColor = true;
    this.btnChangeDiap.Click += new EventHandler(this.btnChangeDiap_Click);
    this.cbAddComplect.AutoSize = true;
    this.cbAddComplect.Location = new Point(13, 20);
    this.cbAddComplect.Name = "cbAddComplect";
    this.cbAddComplect.Size = new Size(135, 17);
    this.cbAddComplect.TabIndex = 1;
    this.cbAddComplect.Text = "добавить \"комплект\"";
    this.cbAddComplect.UseVisualStyleBackColor = true;
    this.cbAddComplect.CheckedChanged += new EventHandler(this.cbAddComplect_CheckedChanged);
    this.lblDiapText.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
    this.lblDiapText.Location = new Point(154, 20);
    this.lblDiapText.Name = "lblDiapText";
    this.lblDiapText.Size = new Size(527, 16 /*0x10*/);
    this.lblDiapText.TabIndex = 0;
    this.lblDiapText.Text = "(Не задан)";
    this.lv.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
    this.lv.Columns.AddRange(new ColumnHeader[2]
    {
      this.colHeaderID,
      this.colHeaderObj
    });
    this.lv.FullRowSelect = true;
    this.lv.GridLines = true;
    this.lv.HideSelection = false;
    this.lv.Items.AddRange(new ListViewItem[4]
    {
      listViewItem1,
      listViewItem2,
      listViewItem3,
      listViewItem4
    });
    this.lv.Location = new Point(10, 54);
    this.lv.MultiSelect = false;
    this.lv.Name = "lv";
    this.lv.ShowItemToolTips = true;
    this.lv.Size = new Size(817, 140);
    this.lv.SmallImageList = this.IL;
    this.lv.TabIndex = 8;
    this.lv.UseCompatibleStateImageBehavior = false;
    this.lv.View = View.Details;
    this.lv.SelectedIndexChanged += new EventHandler(this.lv_SelectedIndexChanged);
    this.colHeaderID.Text = "ИД версии";
    this.colHeaderID.Width = 80 /*0x50*/;
    this.colHeaderObj.Text = "Описание ошибки";
    this.colHeaderObj.Width = 650;
    this.btnChange.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
    this.btnChange.Location = new Point(6, 181);
    this.btnChange.Name = "btnChange";
    this.btnChange.Size = new Size(367, 23);
    this.btnChange.TabIndex = 11;
    this.btnChange.Text = "Изменить диапазон версии, не входящей в это извещение";
    this.btnChange.UseVisualStyleBackColor = true;
    this.btnChange.Click += new EventHandler(this.btnChange_Click);
    this.groupBox2.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
    this.groupBox2.Controls.Add((Control) this.tlSerDates);
    this.groupBox2.Controls.Add((Control) this.btnChange);
    this.groupBox2.Location = new Point(12, 53);
    this.groupBox2.Name = "groupBox2";
    this.groupBox2.Size = new Size(809, 210);
    this.groupBox2.TabIndex = 12;
    this.groupBox2.TabStop = false;
    this.groupBox2.Text = "Версии объектов для выбранного головного изделия в данном контексте редактирования";
    this.panel2.Controls.Add((Control) this.btnDelIzdel);
    this.panel2.Controls.Add((Control) this.comboIzdel);
    this.panel2.Controls.Add((Control) this.groupBox2);
    this.panel2.Controls.Add((Control) this.label1);
    this.panel2.Controls.Add((Control) this.btnSelIzdel);
    this.panel2.Dock = DockStyle.Top;
    this.panel2.Location = new Point(0, 0);
    this.panel2.Name = "panel2";
    this.panel2.Size = new Size(833, 266);
    this.panel2.TabIndex = 13;
    this.btnDelIzdel.Anchor = AnchorStyles.Top | AnchorStyles.Right;
    this.btnDelIzdel.ImageIndex = 7;
    this.btnDelIzdel.ImageList = this.IL;
    this.btnDelIzdel.Location = new Point(794, 22);
    this.btnDelIzdel.Name = "btnDelIzdel";
    this.btnDelIzdel.Size = new Size(27, 23);
    this.btnDelIzdel.TabIndex = 14;
    this.btnDelIzdel.UseVisualStyleBackColor = true;
    this.btnDelIzdel.Click += new EventHandler(this.btnDelIzdel_Click);
    this.comboIzdel.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
    this.comboIzdel.DropDownStyle = ComboBoxStyle.DropDownList;
    this.comboIzdel.FormattingEnabled = true;
    this.comboIzdel.Location = new Point(12, 24);
    this.comboIzdel.Name = "comboIzdel";
    this.comboIzdel.Size = new Size(697, 21);
    this.comboIzdel.TabIndex = 13;
    this.comboIzdel.SelectedIndexChanged += new EventHandler(this.mruHeadIzdel_SelectedIndexChanged);
    this.panel3.Controls.Add((Control) this.groupBox1);
    this.panel3.Controls.Add((Control) this.lv);
    this.panel3.Dock = DockStyle.Fill;
    this.panel3.Location = new Point(0, 266);
    this.panel3.Name = "panel3";
    this.panel3.Size = new Size(833, 200);
    this.panel3.TabIndex = 14;
    this.splitter1.Dock = DockStyle.Top;
    this.splitter1.Location = new Point(0, 266);
    this.splitter1.Name = "splitter1";
    this.splitter1.Size = new Size(833, 3);
    this.splitter1.TabIndex = 15;
    this.splitter1.TabStop = false;
    this.AcceptButton = (IButtonControl) this.btnOK;
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.CancelButton = (IButtonControl) this.btnCancel;
    this.ClientSize = new Size(833, 496);
    this.Controls.Add((Control) this.splitter1);
    this.Controls.Add((Control) this.panel3);
    this.Controls.Add((Control) this.panel2);
    this.Controls.Add((Control) this.panel1);
    this.HelpButton = true;
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.Name = nameof (SeriesDatesForm);
    this.StartPosition = FormStartPosition.CenterParent;
    this.Text = "Диапазон серий или дат";
    this.HelpButtonClicked += new CancelEventHandler(this.SeriesDatesForm_HelpButtonClicked);
    this.HelpRequested += new HelpEventHandler(this.SeriesDatesForm_HelpRequested);
    this.panel1.ResumeLayout(false);
    this.panel1.PerformLayout();
    this.tlSerDates.EndInit();
    this.groupBox1.ResumeLayout(false);
    this.groupBox1.PerformLayout();
    this.groupBox2.ResumeLayout(false);
    this.panel2.ResumeLayout(false);
    this.panel2.PerformLayout();
    this.panel3.ResumeLayout(false);
    this.ResumeLayout(false);
  }

  private class TreeKey
  {
    public long primaryVerId = -1;
    public int secondIndex = -1;

    public TreeKey(long verId) => this.primaryVerId = verId;

    public TreeKey(long verId, int secIndex)
    {
      this.primaryVerId = verId;
      this.secondIndex = secIndex;
    }
  }
}
