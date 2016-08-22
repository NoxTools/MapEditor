using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace Wizard
{
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
		public delegate void PageIndexChangedDlgt(int piPageIndex);
		protected PageIndexChangedDlgt _dPageIndexChanged;
		protected ArrayList _oControlsInPage;
		protected Panel panel1;
		private ArrayList _oPagesActivated;
		protected bool _bAllowBack = true;
        public event EventHandler PageEnding;
	
		/// <summary>
		/// Accessor to the delegate of the change of page.
		/// Set this property to know when the display page had changed
		/// </summary>
		PageIndexChangedDlgt PageIndexChangedDelegate
		{
			get
			{
				return _dPageIndexChanged;
			}
		}
		private System.ComponentModel.Container components = null;

		public WizardForm()
		{
			InitializeComponent();
			_dPageIndexChanged = new PageIndexChangedDlgt(EnablePrevNextButton);
			_dPageIndexChanged += new PageIndexChangedDlgt(DisplayCurrentPage);
		}
		/// <summary>
		/// This method hide the tab control and resize the tab pages.
		/// It's called automatically
		/// </summary>
		protected void InitializePages()
		{
			if (_oMainTabControl.TabCount > 0)
			{
				_oMainTabControl.Parent = null;
				// Retaille le tabcontrol, et les controles enfants
				_oMainTabControl.Scale(new SizeF(1, ((float)(_oMainTabControl.ClientSize.Height + _oMainTabControl.GetTabRect(0).Height )) / _oMainTabControl.ClientSize.Height));
				// Récupère tous les controles des pages, et les assignes à la fenètre principale
				_oControlsInPage = new ArrayList(_oMainTabControl.TabPages.Count);
				_oPagesActivated = new ArrayList();
				foreach (TabPage oTabPage in _oMainTabControl.TabPages)
				{
					ArrayList oControls = new ArrayList(oTabPage.Controls.Count);
					foreach (Control oControl in oTabPage.Controls)
					{
						oControls.Add(oControl);
					}
					_oControlsInPage.Add(oControls);
					_oPagesActivated.Add(false);
				}
				PageIndexChangedDelegate(CurrentPage);

			}
		}

		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
		/// le contenu de cette méthode avec l'éditeur de code.
		/// </summary>
		private void InitializeComponent()
		{
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(WizardForm));
			this._oFinishBtn = new System.Windows.Forms.Button();
			this._oNextBtn = new System.Windows.Forms.Button();
			this._oPreviousBtn = new System.Windows.Forms.Button();
			this._oCancelBtn = new System.Windows.Forms.Button();
			this._oMainTabControl = new System.Windows.Forms.TabControl();
			this.panel1 = new System.Windows.Forms.Panel();
			this.SuspendLayout();
			// 
			// _oFinishBtn
			// 
			this._oFinishBtn.AccessibleDescription = ((string)(resources.GetObject("_oFinishBtn.AccessibleDescription")));
			this._oFinishBtn.AccessibleName = ((string)(resources.GetObject("_oFinishBtn.AccessibleName")));
			this._oFinishBtn.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("_oFinishBtn.Anchor")));
			this._oFinishBtn.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("_oFinishBtn.BackgroundImage")));
			this._oFinishBtn.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("_oFinishBtn.Dock")));
			this._oFinishBtn.Enabled = ((bool)(resources.GetObject("_oFinishBtn.Enabled")));
			this._oFinishBtn.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("_oFinishBtn.FlatStyle")));
			this._oFinishBtn.Font = ((System.Drawing.Font)(resources.GetObject("_oFinishBtn.Font")));
			this._oFinishBtn.Image = ((System.Drawing.Image)(resources.GetObject("_oFinishBtn.Image")));
			this._oFinishBtn.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("_oFinishBtn.ImageAlign")));
			this._oFinishBtn.ImageIndex = ((int)(resources.GetObject("_oFinishBtn.ImageIndex")));
			this._oFinishBtn.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("_oFinishBtn.ImeMode")));
			this._oFinishBtn.Location = ((System.Drawing.Point)(resources.GetObject("_oFinishBtn.Location")));
			this._oFinishBtn.Name = "_oFinishBtn";
			this._oFinishBtn.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("_oFinishBtn.RightToLeft")));
			this._oFinishBtn.Size = ((System.Drawing.Size)(resources.GetObject("_oFinishBtn.Size")));
			this._oFinishBtn.TabIndex = ((int)(resources.GetObject("_oFinishBtn.TabIndex")));
			this._oFinishBtn.Text = resources.GetString("_oFinishBtn.Text");
			this._oFinishBtn.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("_oFinishBtn.TextAlign")));
			this._oFinishBtn.Visible = ((bool)(resources.GetObject("_oFinishBtn.Visible")));
			this._oFinishBtn.Click += new System.EventHandler(this._oFinishBtn_Click);
			// 
			// _oNextBtn
			// 
			this._oNextBtn.AccessibleDescription = ((string)(resources.GetObject("_oNextBtn.AccessibleDescription")));
			this._oNextBtn.AccessibleName = ((string)(resources.GetObject("_oNextBtn.AccessibleName")));
			this._oNextBtn.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("_oNextBtn.Anchor")));
			this._oNextBtn.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("_oNextBtn.BackgroundImage")));
			this._oNextBtn.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("_oNextBtn.Dock")));
			this._oNextBtn.Enabled = ((bool)(resources.GetObject("_oNextBtn.Enabled")));
			this._oNextBtn.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("_oNextBtn.FlatStyle")));
			this._oNextBtn.Font = ((System.Drawing.Font)(resources.GetObject("_oNextBtn.Font")));
			this._oNextBtn.Image = ((System.Drawing.Image)(resources.GetObject("_oNextBtn.Image")));
			this._oNextBtn.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("_oNextBtn.ImageAlign")));
			this._oNextBtn.ImageIndex = ((int)(resources.GetObject("_oNextBtn.ImageIndex")));
			this._oNextBtn.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("_oNextBtn.ImeMode")));
			this._oNextBtn.Location = ((System.Drawing.Point)(resources.GetObject("_oNextBtn.Location")));
			this._oNextBtn.Name = "_oNextBtn";
			this._oNextBtn.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("_oNextBtn.RightToLeft")));
			this._oNextBtn.Size = ((System.Drawing.Size)(resources.GetObject("_oNextBtn.Size")));
			this._oNextBtn.TabIndex = ((int)(resources.GetObject("_oNextBtn.TabIndex")));
			this._oNextBtn.Text = resources.GetString("_oNextBtn.Text");
			this._oNextBtn.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("_oNextBtn.TextAlign")));
			this._oNextBtn.Visible = ((bool)(resources.GetObject("_oNextBtn.Visible")));
			this._oNextBtn.Click += new System.EventHandler(this._oNextBtn_Click);
			// 
			// _oPreviousBtn
			// 
			this._oPreviousBtn.AccessibleDescription = ((string)(resources.GetObject("_oPreviousBtn.AccessibleDescription")));
			this._oPreviousBtn.AccessibleName = ((string)(resources.GetObject("_oPreviousBtn.AccessibleName")));
			this._oPreviousBtn.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("_oPreviousBtn.Anchor")));
			this._oPreviousBtn.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("_oPreviousBtn.BackgroundImage")));
			this._oPreviousBtn.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("_oPreviousBtn.Dock")));
			this._oPreviousBtn.Enabled = ((bool)(resources.GetObject("_oPreviousBtn.Enabled")));
			this._oPreviousBtn.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("_oPreviousBtn.FlatStyle")));
			this._oPreviousBtn.Font = ((System.Drawing.Font)(resources.GetObject("_oPreviousBtn.Font")));
			this._oPreviousBtn.Image = ((System.Drawing.Image)(resources.GetObject("_oPreviousBtn.Image")));
			this._oPreviousBtn.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("_oPreviousBtn.ImageAlign")));
			this._oPreviousBtn.ImageIndex = ((int)(resources.GetObject("_oPreviousBtn.ImageIndex")));
			this._oPreviousBtn.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("_oPreviousBtn.ImeMode")));
			this._oPreviousBtn.Location = ((System.Drawing.Point)(resources.GetObject("_oPreviousBtn.Location")));
			this._oPreviousBtn.Name = "_oPreviousBtn";
			this._oPreviousBtn.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("_oPreviousBtn.RightToLeft")));
			this._oPreviousBtn.Size = ((System.Drawing.Size)(resources.GetObject("_oPreviousBtn.Size")));
			this._oPreviousBtn.TabIndex = ((int)(resources.GetObject("_oPreviousBtn.TabIndex")));
			this._oPreviousBtn.Text = resources.GetString("_oPreviousBtn.Text");
			this._oPreviousBtn.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("_oPreviousBtn.TextAlign")));
			this._oPreviousBtn.Visible = ((bool)(resources.GetObject("_oPreviousBtn.Visible")));
			this._oPreviousBtn.Click += new System.EventHandler(this._oPreviousBtn_Click);
			// 
			// _oCancelBtn
			// 
			this._oCancelBtn.AccessibleDescription = ((string)(resources.GetObject("_oCancelBtn.AccessibleDescription")));
			this._oCancelBtn.AccessibleName = ((string)(resources.GetObject("_oCancelBtn.AccessibleName")));
			this._oCancelBtn.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("_oCancelBtn.Anchor")));
			this._oCancelBtn.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("_oCancelBtn.BackgroundImage")));
			this._oCancelBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this._oCancelBtn.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("_oCancelBtn.Dock")));
			this._oCancelBtn.Enabled = ((bool)(resources.GetObject("_oCancelBtn.Enabled")));
			this._oCancelBtn.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("_oCancelBtn.FlatStyle")));
			this._oCancelBtn.Font = ((System.Drawing.Font)(resources.GetObject("_oCancelBtn.Font")));
			this._oCancelBtn.Image = ((System.Drawing.Image)(resources.GetObject("_oCancelBtn.Image")));
			this._oCancelBtn.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("_oCancelBtn.ImageAlign")));
			this._oCancelBtn.ImageIndex = ((int)(resources.GetObject("_oCancelBtn.ImageIndex")));
			this._oCancelBtn.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("_oCancelBtn.ImeMode")));
			this._oCancelBtn.Location = ((System.Drawing.Point)(resources.GetObject("_oCancelBtn.Location")));
			this._oCancelBtn.Name = "_oCancelBtn";
			this._oCancelBtn.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("_oCancelBtn.RightToLeft")));
			this._oCancelBtn.Size = ((System.Drawing.Size)(resources.GetObject("_oCancelBtn.Size")));
			this._oCancelBtn.TabIndex = ((int)(resources.GetObject("_oCancelBtn.TabIndex")));
			this._oCancelBtn.Text = resources.GetString("_oCancelBtn.Text");
			this._oCancelBtn.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("_oCancelBtn.TextAlign")));
			this._oCancelBtn.Visible = ((bool)(resources.GetObject("_oCancelBtn.Visible")));
			this._oCancelBtn.Click += new System.EventHandler(this._oCancelBtn_Click);
			// 
			// _oMainTabControl
			// 
			this._oMainTabControl.AccessibleDescription = ((string)(resources.GetObject("_oMainTabControl.AccessibleDescription")));
			this._oMainTabControl.AccessibleName = ((string)(resources.GetObject("_oMainTabControl.AccessibleName")));
			this._oMainTabControl.Alignment = ((System.Windows.Forms.TabAlignment)(resources.GetObject("_oMainTabControl.Alignment")));
			this._oMainTabControl.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("_oMainTabControl.Anchor")));
			this._oMainTabControl.Appearance = ((System.Windows.Forms.TabAppearance)(resources.GetObject("_oMainTabControl.Appearance")));
			this._oMainTabControl.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("_oMainTabControl.BackgroundImage")));
			this._oMainTabControl.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("_oMainTabControl.Dock")));
			this._oMainTabControl.Enabled = ((bool)(resources.GetObject("_oMainTabControl.Enabled")));
			this._oMainTabControl.Font = ((System.Drawing.Font)(resources.GetObject("_oMainTabControl.Font")));
			this._oMainTabControl.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("_oMainTabControl.ImeMode")));
			this._oMainTabControl.ItemSize = ((System.Drawing.Size)(resources.GetObject("_oMainTabControl.ItemSize")));
			this._oMainTabControl.Location = ((System.Drawing.Point)(resources.GetObject("_oMainTabControl.Location")));
			this._oMainTabControl.Name = "_oMainTabControl";
			this._oMainTabControl.Padding = ((System.Drawing.Point)(resources.GetObject("_oMainTabControl.Padding")));
			this._oMainTabControl.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("_oMainTabControl.RightToLeft")));
			this._oMainTabControl.SelectedIndex = 0;
			this._oMainTabControl.ShowToolTips = ((bool)(resources.GetObject("_oMainTabControl.ShowToolTips")));
			this._oMainTabControl.Size = ((System.Drawing.Size)(resources.GetObject("_oMainTabControl.Size")));
			this._oMainTabControl.TabIndex = ((int)(resources.GetObject("_oMainTabControl.TabIndex")));
			this._oMainTabControl.Text = resources.GetString("_oMainTabControl.Text");
			this._oMainTabControl.Visible = ((bool)(resources.GetObject("_oMainTabControl.Visible")));
			// 
			// panel1
			// 
			this.panel1.AccessibleDescription = ((string)(resources.GetObject("panel1.AccessibleDescription")));
			this.panel1.AccessibleName = ((string)(resources.GetObject("panel1.AccessibleName")));
			this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("panel1.Anchor")));
			this.panel1.AutoScroll = ((bool)(resources.GetObject("panel1.AutoScroll")));
			this.panel1.AutoScrollMargin = ((System.Drawing.Size)(resources.GetObject("panel1.AutoScrollMargin")));
			this.panel1.AutoScrollMinSize = ((System.Drawing.Size)(resources.GetObject("panel1.AutoScrollMinSize")));
			this.panel1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel1.BackgroundImage")));
			this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.panel1.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("panel1.Dock")));
			this.panel1.Enabled = ((bool)(resources.GetObject("panel1.Enabled")));
			this.panel1.Font = ((System.Drawing.Font)(resources.GetObject("panel1.Font")));
			this.panel1.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("panel1.ImeMode")));
			this.panel1.Location = ((System.Drawing.Point)(resources.GetObject("panel1.Location")));
			this.panel1.Name = "panel1";
			this.panel1.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("panel1.RightToLeft")));
			this.panel1.Size = ((System.Drawing.Size)(resources.GetObject("panel1.Size")));
			this.panel1.TabIndex = ((int)(resources.GetObject("panel1.TabIndex")));
			this.panel1.Text = resources.GetString("panel1.Text");
			this.panel1.Visible = ((bool)(resources.GetObject("panel1.Visible")));
			// 
			// WizardForm
			// 
			this.AccessibleDescription = ((string)(resources.GetObject("$this.AccessibleDescription")));
			this.AccessibleName = ((string)(resources.GetObject("$this.AccessibleName")));
			this.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("$this.Anchor")));
			this.AutoScaleBaseSize = ((System.Drawing.Size)(resources.GetObject("$this.AutoScaleBaseSize")));
			this.AutoScroll = ((bool)(resources.GetObject("$this.AutoScroll")));
			this.AutoScrollMargin = ((System.Drawing.Size)(resources.GetObject("$this.AutoScrollMargin")));
			this.AutoScrollMinSize = ((System.Drawing.Size)(resources.GetObject("$this.AutoScrollMinSize")));
			this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
			this.ClientSize = ((System.Drawing.Size)(resources.GetObject("$this.ClientSize")));
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.panel1,
																		  this._oCancelBtn,
																		  this._oNextBtn,
																		  this._oFinishBtn,
																		  this._oPreviousBtn,
																		  this._oMainTabControl});
			this.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("$this.Dock")));
			this.Enabled = ((bool)(resources.GetObject("$this.Enabled")));
			this.Font = ((System.Drawing.Font)(resources.GetObject("$this.Font")));
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("$this.ImeMode")));
			this.Location = ((System.Drawing.Point)(resources.GetObject("$this.Location")));
			this.MaximumSize = ((System.Drawing.Size)(resources.GetObject("$this.MaximumSize")));
			this.MinimumSize = ((System.Drawing.Size)(resources.GetObject("$this.MinimumSize")));
			this.Name = "WizardForm";
			this.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("$this.RightToLeft")));
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			this.StartPosition = ((System.Windows.Forms.FormStartPosition)(resources.GetObject("$this.StartPosition")));
			this.Text = resources.GetString("$this.Text");
			this.Visible = ((bool)(resources.GetObject("$this.Visible")));
			this.Load += new System.EventHandler(this.WizardForm_Load);
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// This method is used to calculate the offset of the next displayed page.
		/// This method can be overrided to have a different behavior
		/// </summary>
		/// <param name="piCurrentPage">Index of displayed page</param>
		/// <returns>New index of the displayed page</returns>
		public virtual int ForwardOffset(int piCurrentPage)
		{
			return ++ piCurrentPage;
		}
		/// <summary>
		/// This method is used to calculate the offset of the previous displayed page.
		/// This method can be overrided to have a different behavior
		/// </summary>
		/// <param name="piCurrentPage">Index of displayed page</param>
		/// <returns>New index of the displayed page</returns>
		public virtual int PreviousOffset(int piCurrentPage)
		{
			return -- piCurrentPage;
		}
		private void _oNextBtn_Click(object sender, System.EventArgs e)
		{
            PageEnding(_oMainTabControl.TabPages[CurrentPage], new EventArgs());
			if (ValidatePage(_iCurrentPage))
				PageIndexChangedDelegate(ForwardOffset(_iCurrentPage));
		}

		private void _oPreviousBtn_Click(object sender, System.EventArgs e)
		{
			PageIndexChangedDelegate(PreviousOffset(_iCurrentPage));
		}
		protected void EnablePrevNextButton(int piPageIndex)
		{
			if (piPageIndex == 0)
				_oPreviousBtn.Enabled = false;
			else
				if (_bAllowBack)
					_oPreviousBtn.Enabled = true;
				else
					_oPreviousBtn.Enabled = false;					
			if (piPageIndex == _oMainTabControl.TabCount - 1)
				_oNextBtn.Enabled = false;
			else
				_oNextBtn.Enabled = true;

		}
		protected virtual void DisplayCurrentPage(int piPageIndex)
		{
			_iCurrentPage = piPageIndex;
			if (_iCurrentPage >= 0)
			{
				Text = _oMainTabControl.TabPages[piPageIndex].Text;
				int i = 0;
				foreach (ArrayList oControls in _oControlsInPage)
				{
					foreach (Control oControl in oControls)
					{
						oControl.Parent = this;
						if (i == piPageIndex)
							oControl.Show();
						else
							oControl.Hide();
					}
					i ++;
				}
				if (!(bool)_oPagesActivated[piPageIndex])
				{
					_oPagesActivated[piPageIndex] = true;
					ActivatePage(piPageIndex);
				}
				EnablePrevNextButton(piPageIndex);
			}
		}

		private void _oFinishBtn_Click(object sender, System.EventArgs e)
		{
            PageEnding(_oMainTabControl.TabPages[CurrentPage], new EventArgs());
            for (int i = CurrentPage ; i <= _oMainTabControl.TabCount ; i ++)
			{
				if (!ValidatePage(i))
					return;
			}
			DialogResult = DialogResult.OK;
			Close();
		}

		private void _oCancelBtn_Click(object sender, System.EventArgs e)
		{
			DialogResult = _oCancelBtn.DialogResult;
			Close();
		}
		/// <summary>
		/// This method is used by the wizard to knwo if it can go to the next displayed page
		/// This method must be overrided
		/// </summary>
		/// <param name="piPageNumber">Number of the current displayed page</param>
		/// <returns><c>true</c> if the page had been validated, else <c>false</c></returns>
		protected virtual bool ValidatePage(int piPageNumber)
		{
			return true;
		}

		public int CurrentPage
		{
			get
			{
				return _iCurrentPage;
			}
		}

		/// <summary>
		/// This method is called before a page is displayed by the wizard
		/// This method must be overrided
		/// </summary>
		/// <param name="piPageNumber">Number of the page to be displayed</param>
		protected virtual void ActivatePage(int piPageNumber)
		{
		
		}
		/// <summary>
		/// override of the ShowDialog of base form
		/// </summary>
		/// <param name="poOwner">Window handle of the owner of the wizard</param>
		/// <param name="piPageNumber">Number of the page displayed at startup</param>
		/// <returns></returns>
		public DialogResult ShowDialog ( System.Windows.Forms.IWin32Window poOwner, int piPageNumber )
		{
			_iCurrentPage = piPageNumber;
			return base.ShowDialog(poOwner);
		}

		private void WizardForm_Load(object sender, System.EventArgs e)
		{
			if (_oMainTabControl.TabCount > 0)
			{
				InitializePages();
				int iPageTo = CurrentPage;
				for (int i = 0 ; i <= iPageTo ; )
				{
					DisplayCurrentPage(i);
					i ++;
					if ((i - 1) > 0)
						ValidatePage(i - 1);
				}
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
			if (_oPagesActivated != null)
			{
				if (_oPagesActivated.Count > piPageNumber)
				{
					for (int i = piPageNumber ; i < _oPagesActivated.Count ; i ++)
						_oPagesActivated[i] = false;
				}
			}
		}
		/// <summary>
		/// This method allow the back button
		/// </summary>
		/// <param name="pbAllowBack"><c>true</c> to allow back button, else <c>false</c></param>
		public void AllowBack(bool pbAllowBack)
		{
			_bAllowBack = pbAllowBack;
		}
	}
}
