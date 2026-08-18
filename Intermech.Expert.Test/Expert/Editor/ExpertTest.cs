// Decompiled with JetBrains decompiler
// Type: Intermech.Expert.Editor.ExpertTest
// Assembly: Intermech.Expert.Test, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 494A2DB2-0ED6-480D-BF40-DFD41733278B
// Assembly location: D:\IPS\Client\Intermech.Expert.Test.dll

using Intermech.Bars;
using Intermech.Expert.Editor.Table;
using Intermech.Expert.User;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Expert;
using Intermech.Interfaces.Plugins;
using Intermech.Localization;
using Intermech.Navigator;
using System;

#nullable disable
namespace Intermech.Expert.Editor;

public class ExpertTest : IPackage
{
  private IServiceProvider _serviceProvider;
  private FormEditor fEd;

  public string Name => "Тест экспертной системы";

  public void Load(IServiceProvider serviceProvider)
  {
    this._serviceProvider = serviceProvider;
    MenuBar menuBar = ((BarManager) serviceProvider.GetService(typeof (BarManager))).MenuBar;
    MenuBarItem menuBarItem = new MenuBarItem(LocalizationHolder.rm.GetString("Expert.Editor_184"));
    MenuButtonItem menuButtonItem1 = new MenuButtonItem(LocalizationHolder.rm.GetString("Expert.Editor_185"));
    menuButtonItem1.Click += new EventHandler(this.menuItem_Click);
    menuBarItem.Items.Add((ToolbarItemBase) menuButtonItem1);
    MenuButtonItem menuButtonItem2 = new MenuButtonItem(LocalizationHolder.rm.GetString("Expert.Editor_186"));
    menuButtonItem2.Click += new EventHandler(this.formItem_Click);
    menuBarItem.Items.Add((ToolbarItemBase) menuButtonItem2);
    MenuButtonItem menuButtonItem3 = new MenuButtonItem(LocalizationHolder.rm.GetString("Expert.Editor_187"));
    menuButtonItem3.Click += new EventHandler(this.serverItem_Click);
    menuBarItem.Items.Add((ToolbarItemBase) menuButtonItem3);
    menuBar.Items.Add((ToolbarItemBase) menuBarItem);
    MenuButtonItem menuButtonItem4 = new MenuButtonItem(LocalizationHolder.rm.GetString("Expert.Editor_188"));
    menuButtonItem4.Click += new EventHandler(this.tableTestItem_Click);
    menuBarItem.Items.Add((ToolbarItemBase) menuButtonItem4);
    MenuButtonItem menuButtonItem5 = new MenuButtonItem(LocalizationHolder.rm.GetString("Expert.Editor_614"));
    menuButtonItem5.Click += new EventHandler(this.testExpertItem_Click);
    menuBarItem.Items.Add((ToolbarItemBase) menuButtonItem5);
    MenuButtonItem menuButtonItem6 = new MenuButtonItem(LocalizationHolder.rm.GetString("Expert.Editor_615"));
    menuButtonItem6.Click += new EventHandler(this.fixExpertItem_Click);
    menuBarItem.Items.Add((ToolbarItemBase) menuButtonItem6);
    MenuButtonItem menuButtonItem7 = new MenuButtonItem("Исправить идентификаторы одного объекта");
    menuButtonItem7.Click += new EventHandler(this.fixOneItem_Click);
    menuBarItem.Items.Add((ToolbarItemBase) menuButtonItem7);
    MenuButtonItem menuButtonItem8 = new MenuButtonItem("Заполнить GUID'ы в формулах");
    menuButtonItem8.Click += new EventHandler(this.сreateGUIDsItem_Click);
    menuBarItem.Items.Add((ToolbarItemBase) menuButtonItem8);
    MenuButtonItem menuButtonItem9 = new MenuButtonItem("Заполнить GUID'ы в одной формуле");
    menuButtonItem9.Click += new EventHandler(this.сreateGUIDsOneItem_Click);
    menuBarItem.Items.Add((ToolbarItemBase) menuButtonItem9);
    MenuButtonItem menuButtonItem10 = new MenuButtonItem("Apply Results TEST");
    menuButtonItem10.Click += new EventHandler(this.testApplyItem_Click);
    menuBarItem.Items.Add((ToolbarItemBase) menuButtonItem10);
  }

  private void testApplyItem_Click(object sender, EventArgs e)
  {
    new ES_ApplyResForm().Execute(ES_ApplyResForm._GetSampleList());
  }

  private void сreateGUIDsItem_Click(object sender, EventArgs e)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      new StringListForm().Execute((sessionKeeper.Session.GetCustomService(typeof (IExpertServer)) as IExpertServer).CreateGUIDs());
  }

  private void сreateGUIDsOneItem_Click(object sender, EventArgs e)
  {
    long[] numArray = SelectionWindow.SelectObjects("Expert formulas", "Select one formula to test ID conversion", ExpertConsts.Consts.objBaseFormula, SelectionOptions.Default);
    if (numArray == null || numArray.Length == 0)
      return;
    long objId = numArray[0];
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      new StringListForm().Execute((sessionKeeper.Session.GetCustomService(typeof (IExpertServer)) as IExpertServer).CreateGIUDsOne(objId));
  }

  private void fixOneItem_Click(object sender, EventArgs e)
  {
    long[] numArray = SelectionWindow.SelectObjects("Expert objects", "Select one object to test ID conversion", ExpertConsts.Consts.objObject, SelectionOptions.Default);
    if (numArray == null || numArray.Length == 0)
      return;
    long objId = numArray[0];
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      new StringListForm().Execute((sessionKeeper.Session.GetCustomService(typeof (IExpertServer)) as IExpertServer).FixIdentsOne(objId));
  }

  private void fixExpertItem_Click(object sender, EventArgs e)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      new StringListForm().Execute((sessionKeeper.Session.GetCustomService(typeof (IExpertServer)) as IExpertServer).FixIdentsComplete());
  }

  private void testExpertItem_Click(object sender, EventArgs e) => new TestESForm().Execute();

  public void Unload()
  {
  }

  private void menuItem_Click(object sender, EventArgs e)
  {
    TempFormula tf = new TempFormula();
    tf.Init();
    if (this.fEd == null)
      this.fEd = new FormEditor();
    this.fEd.Execute(ref tf, "");
  }

  private void formItem_Click(object sender, EventArgs e) => new DemoStend().Execute();

  private void serverItem_Click(object sender, EventArgs e) => new TestServer().Execute();

  private void tableTestItem_Click(object sender, EventArgs e)
  {
    using (TableTest tableTest = new TableTest())
    {
      int num = (int) tableTest.ShowDialog();
    }
  }
}
