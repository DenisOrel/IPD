
// Type: Wizard.WizardForm
// Assembly: Intermech.Controls, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B2BFE6FF-0AA3-422C-A374-1A460CB041DD
:\IPS\Client\Intermech.Controls.dll
// XML documentation location: D:\IPS\Client\Intermech.Controls.xml

using System;
using System.Collections;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.Layout;


namespace Wizard;

/// <summary>
/// Base class that implement a minimal WizardForm and behavior
/// </summary>
public class WizardForm : Form
{
  protected Button _oFinishBtn;
  protected Button _oNextBtn;
  protected Button _oPreviousBtn;
  protected Button _oCancelBtn;
  protected TabControl _oMainTabControl;
  private int _iCurrentPage;
  protected WizardForm.PageIndexChangedDlgt _dPageIndexChanged;
  protected ArrayList _oControlsInPage;
  protected Panel panel1;
  private ArrayList _oPagesActivated;
  protected bool _bAllowBack = true;
  private System.ComponentModel.Container components;

  /// <summary>
  /// Accessor to the delegate of the change of page.
  /// Set this property to know when the display page had changed
  /// </summary>
  private WizardForm.PageIndexChangedDlgt PageIndexChangedDelegate => this._dPageIndexChanged;

  public WizardForm()
  {
    this.InitializeComponent();
    this._dPageIndexChanged = new WizardForm.PageIndexChangedDlgt(this.EnablePrevNextButton);
    this._dPageIndexChanged += new WizardForm.PageIndexChangedDlgt(this.DisplayCurrentPage);
  }

  /// <summary>
  /// This method hide the tab control and resize the tab pages.
  /// It's called automatically
  /// </summary>
  protected void InitializePages()
  {
    if (this._oMainTabControl.TabCount <= 0)
      return;
    this._oMainTabControl.Parent = (Control) null;
    TabControl oMainTabControl = this._oMainTabControl;
    Size clientSize = this._oMainTabControl.ClientSize;
    double num = (double) (clientSize.Height + this._oMainTabControl.GetTabRect(0).Height);
    clientSize = this._oMainTabControl.ClientSize;
    double height = (double) clientSize.Height;
    SizeF factor = new SizeF(1f, (float) (num / height));
    oMainTabControl.Scale(factor);
    this._oControlsInPage = new ArrayList(this._oMainTabControl.TabPages.Count);
    this._oPagesActivated = new ArrayList();
    foreach (TabPage tabPage in this._oMainTabControl.TabPages)
    {
      ArrayList arrayList = new ArrayList(tabPage.Controls.Count);
      foreach (Control control in (ArrangedElementCollection) tabPage.Controls)
        arrayList.Add((object) control);
      this._oControlsInPage.Add((object) arrayList);
      this._oPagesActivated.Add((object) false);
    }
    this.PageIndexChangedDelegate(this.CurrentPage);
  }

  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  /// <summary>
  /// Mйthode requise pour la prise en charge du concepteur - ne modifiez pas
  /// le contenu de cette mйthode avec l'йditeur de code.
  /// </summary>
  private void InitializeComponent()
  {
    this._oFinishBtn = new Button();
    this._oNextBtn = new Button();
    this._oPreviousBtn = new Button();
    this._oCancelBtn = new Button();
    this._oMainTabControl = new TabControl();
    this.panel1 = new Panel();
    this.SuspendLayout();
    this._oFinishBtn.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this._oFinishBtn.ImeMode = ImeMode.NoControl;
    this._oFinishBtn.Location = new Point(352, 264);
    this._oFinishBtn.Name = "_oFinishBtn";
    this._oFinishBtn.Size = new Size(75, 23);
    this._oFinishBtn.TabIndex = 0;
    this._oFinishBtn.Text = "Terminer";
    this._oFinishBtn.Click += new EventHandler(this._oFinishBtn_Click);
    this._oNextBtn.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this._oNextBtn.ImeMode = ImeMode.NoControl;
    this._oNextBtn.Location = new Point(272, 264);
    this._oNextBtn.Name = "_oNextBtn";
    this._oNextBtn.Size = new Size(75, 23);
    this._oNextBtn.TabIndex = 1;
    this._oNextBtn.Text = "Suivant";
    this._oNextBtn.Click += new EventHandler(this._oNextBtn_Click);
    this._oPreviousBtn.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this._oPreviousBtn.ImeMode = ImeMode.NoControl;
    this._oPreviousBtn.Location = new Point(192 /*0xC0*/, 264);
    this._oPreviousBtn.Name = "_oPreviousBtn";
    this._oPreviousBtn.Size = new Size(75, 23);
    this._oPreviousBtn.TabIndex = 2;
    this._oPreviousBtn.Text = "Precedent";
    this._oPreviousBtn.Click += new EventHandler(this._oPreviousBtn_Click);
    this._oCancelBtn.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this._oCancelBtn.DialogResult = DialogResult.Cancel;
    this._oCancelBtn.ImeMode = ImeMode.NoControl;
    this._oCancelBtn.Location = new Point(112 /*0x70*/, 264);
    this._oCancelBtn.Name = "_oCancelBtn";
    this._oCancelBtn.Size = new Size(75, 23);
    this._oCancelBtn.TabIndex = 3;
    this._oCancelBtn.Text = "Annuler";
    this._oCancelBtn.Click += new EventHandler(this._oCancelBtn_Click);
    this._oMainTabControl.Dock = DockStyle.Top;
    this._oMainTabControl.ItemSize = new Size(0, 18);
    this._oMainTabControl.Location = new Point(0, 0);
    this._oMainTabControl.Name = "_oMainTabControl";
    this._oMainTabControl.SelectedIndex = 0;
    this._oMainTabControl.Size = new Size(432, 256 /*0x0100*/);
    this._oMainTabControl.TabIndex = 4;
    this._oMainTabControl.Visible = false;
    this.panel1.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
    this.panel1.BorderStyle = BorderStyle.Fixed3D;
    this.panel1.Location = new Point(0, 256 /*0x0100*/);
    this.panel1.Name = "panel1";
    this.panel1.Size = new Size(448, 4);
    this.panel1.TabIndex = 5;
    this.AutoScaleBaseSize = new Size(5, 13);
    this.ClientSize = new Size(432, 293);
    this.Controls.Add((Control) this.panel1);
    this.Controls.Add((Control) this._oCancelBtn);
    this.Controls.Add((Control) this._oNextBtn);
    this.Controls.Add((Control) this._oFinishBtn);
    this.Controls.Add((Control) this._oPreviousBtn);
    this.Controls.Add((Control) this._oMainTabControl);
    this.Name = nameof (WizardForm);
    this.ShowInTaskbar = false;
    this.SizeGripStyle = SizeGripStyle.Hide;
    this.Text = nameof (WizardForm);
    this.Load += new EventHandler(this.WizardForm_Load);
    this.ResumeLayout(false);
  }

  /// <summary>
  /// This method is used to calculate the offset of the next displayed page.
  /// This method can be overrided to have a different behavior
  /// </summary>
  /// <param name="piCurrentPage">Index of displayed page</param>
  /// <returns>New index of the displayed page</returns>
  public virtual int ForwardOffset(int piCurrentPage) => ++piCurrentPage;

  /// <summary>
  /// This method is used to calculate the offset of the previous displayed page.
  /// This method can be overrided to have a different behavior
  /// </summary>
  /// <param name="piCurrentPage">Index of displayed page</param>
  /// <returns>New index of the displayed page</returns>
  public virtual int PreviousOffset(int piCurrentPage) => --piCurrentPage;

  private void _oNextBtn_Click(object sender, EventArgs e)
  {
    if (!this.ValidatePage(this._iCurrentPage))
      return;
    this.PageIndexChangedDelegate(this.ForwardOffset(this._iCurrentPage));
  }

  private void _oPreviousBtn_Click(object sender, EventArgs e)
  {
    this.PageIndexChangedDelegate(this.PreviousOffset(this._iCurrentPage));
  }

  protected void EnablePrevNextButton(int piPageIndex)
  {
    if (piPageIndex == 0)
      this._oPreviousBtn.Enabled = false;
    else if (this._bAllowBack)
      this._oPreviousBtn.Enabled = true;
    else
      this._oPreviousBtn.Enabled = false;
    if (piPageIndex == this._oMainTabControl.TabCount - 1)
      this._oNextBtn.Enabled = false;
    else
      this._oNextBtn.Enabled = true;
  }

  protected virtual void DisplayCurrentPage(int piPageIndex)
  {
    this._iCurrentPage = piPageIndex;
    if (this._iCurrentPage < 0)
      return;
    this.Text = this._oMainTabControl.TabPages[piPageIndex].Text;
    int num = 0;
    foreach (ArrayList arrayList in this._oControlsInPage)
    {
      foreach (Control control in arrayList)
      {
        control.Parent = (Control) this;
        if (num == piPageIndex)
          control.Show();
        else
          control.Hide();
      }
      ++num;
    }
    if (!(bool) this._oPagesActivated[piPageIndex])
    {
      this._oPagesActivated[piPageIndex] = (object) true;
      this.ActivatePage(piPageIndex);
    }
    this.EnablePrevNextButton(piPageIndex);
  }

  private void _oFinishBtn_Click(object sender, EventArgs e)
  {
    for (int currentPage = this.CurrentPage; currentPage <= this._oMainTabControl.TabCount; ++currentPage)
    {
      if (!this.ValidatePage(currentPage))
        return;
    }
    this.DialogResult = DialogResult.OK;
    this.Close();
  }

  private void _oCancelBtn_Click(object sender, EventArgs e)
  {
    this.DialogResult = this._oCancelBtn.DialogResult;
    this.Close();
  }

  /// <summary>
  /// This method is used by the wizard to knwo if it can go to the next displayed page
  /// This method must be overrided
  /// </summary>
  /// <param name="piPageNumber">Number of the current displayed page</param>
  /// <returns><c>true</c> if the page had been validated, else <c>false</c></returns>
  protected virtual bool ValidatePage(int piPageNumber) => true;

  public int CurrentPage => this._iCurrentPage;

  /// <summary>
  /// This method is called before a page is displayed by the wizard
  /// This method must be overrided
  /// </summary>
  /// <param name="piPageNumber">Number of the page to be displayed</param>
  protected virtual void ActivatePage(int piPageNumber)
  {
  }

  /// <summary>override of the ShowDialog of base form</summary>
  /// <param name="poOwner">Window handle of the owner of the wizard</param>
  /// <param name="piPageNumber">Number of the page displayed at startup</param>
  /// <returns></returns>
  public DialogResult ShowDialog(IWin32Window poOwner, int piPageNumber)
  {
    this._iCurrentPage = piPageNumber;
    return this.ShowDialog(poOwner);
  }

  private void WizardForm_Load(object sender, EventArgs e)
  {
    if (this._oMainTabControl.TabCount <= 0)
      return;
    this.InitializePages();
    int currentPage = this.CurrentPage;
    int piPageIndex = 0;
    while (piPageIndex <= currentPage)
    {
      this.DisplayCurrentPage(piPageIndex);
      ++piPageIndex;
      if (piPageIndex - 1 > 0)
        this.ValidatePage(piPageIndex - 1);
    }
  }

  /// <summary>
  /// This method is used to unactivate a page that had been previously activated
  /// by the <c>ActivatePage</c> method. Use this method to force activation of a page
  /// in case of use of the back button
  /// </summary>
  /// <param name="piPageNumber">Number of the page to deactivate</param>
  public void UnActivatePage(int piPageNumber)
  {
    if (this._oPagesActivated == null || this._oPagesActivated.Count <= piPageNumber)
      return;
    for (int index = piPageNumber; index < this._oPagesActivated.Count; ++index)
      this._oPagesActivated[index] = (object) false;
  }

  /// <summary>This method allow the back button</summary>
  /// <param name="pbAllowBack"><c>true</c> to allow back button, else <c>false</c></param>
  public void AllowBack(bool pbAllowBack) => this._bAllowBack = pbAllowBack;

  public delegate void PageIndexChangedDlgt(int piPageIndex);
}
