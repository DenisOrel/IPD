
// Type: IMClient.Splash.SplashScreenService




using Intermech.Controls;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;


namespace IMClient.Splash
{
    internal class SplashScreenService : Form, ISplashService
    {
      private bool _needClose;
      private bool _hided;
      private MemoryStream _BannerStream;
      private IContainer components;
      private FormOpacityAnimator formOpacityAnimator1;
      private ControlForeColorAnimator controlForeColorAnimator1;
      private ColorProgressBar progressBar1;
      private Label lbStepDescription;
      private Label lbStepName;
      private Label lbVersion;
      private PictureBox bannerBox;

      public SplashScreenService()
      {
        this.InitializeComponent();
        Application.EnterThreadModal += new EventHandler(this.Application_EnterThreadModal);
        Application.LeaveThreadModal += new EventHandler(this.Application_LeaveThreadModal);
        this.lbVersion.Text = "IPS v." + this.GetType().Assembly.GetName().Version.ToString();
      }

      internal void SetBanner(byte[] bannerArr)
      {
        if (bannerArr == null)
          return;
        this._BannerStream = new MemoryStream(bannerArr);
      }

      private void Application_LeaveThreadModal(object sender, EventArgs e)
      {
        int num = this._hided ? 1 : 0;
      }

      private void Application_EnterThreadModal(object sender, EventArgs e)
      {
      }

      protected override void OnClosed(EventArgs e)
      {
        this.formOpacityAnimator1.Stop();
        base.OnClosed(e);
        Application.EnterThreadModal -= new EventHandler(this.Application_EnterThreadModal);
        Application.LeaveThreadModal -= new EventHandler(this.Application_LeaveThreadModal);
      }

      private void Form2_Shown(object sender, EventArgs e)
      {
        if (this._BannerStream == null)
        {
          this.bannerBox.Visible = false;
        }
        else
        {
          try
          {
            this.bannerBox.BackgroundImage = Image.FromStream((Stream) this._BannerStream);
            int num1 = this.bannerBox.BackgroundImage.Width;
            if (num1 > 480)
              num1 = 480;
            int num2 = this.Right - num1 - this.Left;
            int num3 = this.bannerBox.BackgroundImage.Height;
            if (num3 > 192 /*0xC0*/)
              num3 = 192 /*0xC0*/;
            this.bannerBox.Left = num2;
            this.bannerBox.Width = this.bannerBox.BackgroundImage.Width;
            this.bannerBox.Height = num3;
          }
          catch (Exception ex)
          {
            this.bannerBox.Visible = false;
            (ServicesManager.GetService(typeof (IOutputView)) as IOutputView).WriteString("Ошибки", "Ошибка отображения баннера в окне загрузки клиента: " + ex.Message);
          }
        }
        this.formOpacityAnimator1.Start();
      }

      private void formOpacityAnimator1_AnimationFinished(object sender, EventArgs e)
      {
        try
        {
          if (this.InvokeRequired)
          {
            this.Invoke((Delegate) new EventHandler(this.formOpacityAnimator1_AnimationFinished), sender, (object) e);
          }
          else
          {
            if (this._needClose)
              this.Close();
            this.controlForeColorAnimator1.StartColor = Color.FromArgb(0, Color.White);
            this.controlForeColorAnimator1.EndColor = Color.FromArgb((int) byte.MaxValue, Color.White);
            this.controlForeColorAnimator1.Start(false);
          }
        }
        catch
        {
        }
      }

      private void controlForeColorAnimator1_AnimationFinished(object sender, EventArgs e)
      {
      }

      private void AnimateHide()
      {
        this._needClose = true;
        this.formOpacityAnimator1.EndOpacity = 0.01;
        this.formOpacityAnimator1.Intervall = 10;
        this.formOpacityAnimator1.Start(true);
      }

      public int Steps
      {
        get => this.progressBar1.Maximum;
        set
        {
          this.progressBar1.Maximum = value;
          Application.DoEvents();
        }
      }

      public int Position
      {
        get => this.progressBar1.Value;
        set
        {
          this.progressBar1.Value = value;
          Application.DoEvents();
        }
      }

      public string StepName
      {
        get => this.lbStepName.Text;
        set
        {
          this.lbStepName.Text = value;
          Application.DoEvents();
        }
      }

      public string StepDescription
      {
        get => this.lbStepDescription.Text;
        set
        {
          this.lbStepDescription.Text = value;
          Application.DoEvents();
        }
      }

      public void StepIt()
      {
        ++this.progressBar1.Value;
        Application.DoEvents();
      }

      public void CloseSplash() => this.AnimateHide();

      public void ShowSplash()
      {
        this._hided = false;
        this.BringToFront();
        Application.DoEvents();
      }

      public void HideSplash()
      {
        this._hided = true;
        this.SendToBack();
        Application.DoEvents();
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
        ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (SplashScreenService));
        this.lbStepName = new Label();
        this.lbStepDescription = new Label();
        this.progressBar1 = new ColorProgressBar();
        this.lbVersion = new Label();
        this.formOpacityAnimator1 = new FormOpacityAnimator(this.components);
        this.controlForeColorAnimator1 = new ControlForeColorAnimator(this.components);
        this.bannerBox = new PictureBox();
        this.formOpacityAnimator1.BeginInit();
        this.controlForeColorAnimator1.BeginInit();
        ((ISupportInitialize) this.bannerBox).BeginInit();
        this.SuspendLayout();
        this.lbStepName.BackColor = Color.Transparent;
        this.lbStepName.ForeColor = Color.White;
        componentResourceManager.ApplyResources((object) this.lbStepName, "lbStepName");
        this.lbStepName.Name = "lbStepName";
        this.lbStepDescription.BackColor = Color.Transparent;
        this.lbStepDescription.ForeColor = Color.PowderBlue;
        componentResourceManager.ApplyResources((object) this.lbStepDescription, "lbStepDescription");
        this.lbStepDescription.Name = "lbStepDescription";
        this.progressBar1.BackColor = Color.Transparent;
        this.progressBar1.BarColor = Color.AliceBlue;
        this.progressBar1.BorderColor = Color.LightSkyBlue;
        this.progressBar1.DarkPercent = 0.6f;
        this.progressBar1.GradientMode = ColorProgressBar.GradientModes.Vertical;
        componentResourceManager.ApplyResources((object) this.progressBar1, "progressBar1");
        this.progressBar1.Maximum = 10;
        this.progressBar1.Name = "progressBar1";
        this.progressBar1.Step = 1;
        this.progressBar1.TabStop = false;
        this.progressBar1.Value = 0;
        this.lbVersion.BackColor = Color.Transparent;
        this.lbVersion.ForeColor = Color.White;
        componentResourceManager.ApplyResources((object) this.lbVersion, "lbVersion");
        this.lbVersion.Name = "lbVersion";
        this.formOpacityAnimator1.CurrentStep = 0.0;
        this.formOpacityAnimator1.CurrentValue = (object) 1.0;
        this.formOpacityAnimator1.EndOpacity = 0.99;
        this.formOpacityAnimator1.EndValue = (object) 0.99;
        this.formOpacityAnimator1.Form = (Form) this;
        this.formOpacityAnimator1.Intervall = 50;
        this.formOpacityAnimator1.StartOpacity = 0.1;
        this.formOpacityAnimator1.StartValue = (object) 0.1;
        this.formOpacityAnimator1.StepSize = 10.0;
        this.formOpacityAnimator1.AnimationFinished += new EventHandler(this.formOpacityAnimator1_AnimationFinished);
        this.controlForeColorAnimator1.CurrentStep = 0.0;
        this.controlForeColorAnimator1.CurrentValue = (object) Color.Empty;
        this.controlForeColorAnimator1.EndColor = Color.FromArgb(0, 0, 0, 0);
        this.controlForeColorAnimator1.EndValue = (object) Color.FromArgb(0, 0, 0, 0);
        this.controlForeColorAnimator1.Intervall = 25;
        this.controlForeColorAnimator1.LoopAnimation = true;
        this.controlForeColorAnimator1.StartColor = Color.FromArgb(0, 0, 0, 0);
        this.controlForeColorAnimator1.StartValue = (object) Color.FromArgb(0, 0, 0, 0);
        this.controlForeColorAnimator1.AnimationFinished += new EventHandler(this.controlForeColorAnimator1_AnimationFinished);
        componentResourceManager.ApplyResources((object) this.bannerBox, "bannerBox");
        this.bannerBox.Name = "bannerBox";
        this.bannerBox.TabStop = false;
        componentResourceManager.ApplyResources((object) this, "$this");
        this.AutoScaleMode = AutoScaleMode.Font;
        this.Controls.Add((Control) this.bannerBox);
        this.Controls.Add((Control) this.lbVersion);
        this.Controls.Add((Control) this.progressBar1);
        this.Controls.Add((Control) this.lbStepDescription);
        this.Controls.Add((Control) this.lbStepName);
        this.DoubleBuffered = true;
        this.FormBorderStyle = FormBorderStyle.None;
        this.Name = nameof (SplashScreenService);
        this.Opacity = 0.1;
        this.ShowIcon = false;
        this.ShowInTaskbar = false;
        this.Shown += new EventHandler(this.Form2_Shown);
        this.formOpacityAnimator1.EndInit();
        this.controlForeColorAnimator1.EndInit();
        ((ISupportInitialize) this.bannerBox).EndInit();
        this.ResumeLayout(false);
      }

      public delegate void InvokeDelegate();
    }
}
