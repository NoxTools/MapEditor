using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Net.Mail;
using System.IO;

namespace NoxShared
{
	/// <summary>
	/// Summary description for ExceptionDialog.
	/// </summary>
	public class ExceptionDialog : System.Windows.Forms.Form
	{
		private const string defaultFrom = "user@domain";
		private const string defaultTo = "kirillmurz@yandex.ru";

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox boxEmailTo;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Button buttonSend;
		private System.Windows.Forms.TextBox boxFrom;
		private System.Windows.Forms.TextBox boxMessage;
		private System.Windows.Forms.TextBox boxNotes;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Button buttonCancel;

		public ExceptionDialog(Exception ex, string sourceReason)
		{
			InitializeComponent();

			//fill the text box
			ArrayList text = new ArrayList();
			text.Add("Version: " + Application.ProductVersion);
			text.Add(sourceReason + "\r\n");
			text.Add(ex.Message);
			text.Add(ex.StackTrace);
			text.Add("");
			boxMessage.Lines = (string[]) text.ToArray(typeof(string));
			boxMessage.Select(boxMessage.Text.Length, 0);

			//use default email addresses
			boxFrom.Text = defaultFrom;
			boxEmailTo.Text = defaultTo;

			//save the message to disk
			string msg = ex.Message + "\r\n" + ex.Source + "\r\n" + ex.StackTrace + "\r\n\r\n";
			if (ex.InnerException != null) msg += ex.InnerException.Message + "\r\n" + ex.InnerException.Source + "\r\n" + ex.InnerException.StackTrace + "\r\n\r\n";
			Logger.Log(msg);
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ExceptionDialog));
			this.boxMessage = new System.Windows.Forms.TextBox();
			this.buttonSend = new System.Windows.Forms.Button();
			this.boxEmailTo = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.boxFrom = new System.Windows.Forms.TextBox();
			this.buttonCancel = new System.Windows.Forms.Button();
			this.boxNotes = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// boxMessage
			// 
			resources.ApplyResources(this.boxMessage, "boxMessage");
			this.boxMessage.Name = "boxMessage";
			this.boxMessage.ReadOnly = true;
			// 
			// buttonSend
			// 
			resources.ApplyResources(this.buttonSend, "buttonSend");
			this.buttonSend.Name = "buttonSend";
			this.buttonSend.Click += new System.EventHandler(this.buttonSend_Click);
			// 
			// boxEmailTo
			// 
			resources.ApplyResources(this.boxEmailTo, "boxEmailTo");
			this.boxEmailTo.Name = "boxEmailTo";
			// 
			// label1
			// 
			resources.ApplyResources(this.label1, "label1");
			this.label1.Name = "label1";
			// 
			// label3
			// 
			resources.ApplyResources(this.label3, "label3");
			this.label3.Name = "label3";
			// 
			// boxFrom
			// 
			resources.ApplyResources(this.boxFrom, "boxFrom");
			this.boxFrom.Name = "boxFrom";
			// 
			// buttonCancel
			// 
			this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			resources.ApplyResources(this.buttonCancel, "buttonCancel");
			this.buttonCancel.Name = "buttonCancel";
			// 
			// boxNotes
			// 
			resources.ApplyResources(this.boxNotes, "boxNotes");
			this.boxNotes.Name = "boxNotes";
			// 
			// label2
			// 
			resources.ApplyResources(this.label2, "label2");
			this.label2.Name = "label2";
			// 
			// ExceptionDialog
			// 
			resources.ApplyResources(this, "$this");
			this.CancelButton = this.buttonCancel;
			this.Controls.Add(this.label2);
			this.Controls.Add(this.boxNotes);
			this.Controls.Add(this.buttonCancel);
			this.Controls.Add(this.boxFrom);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.boxEmailTo);
			this.Controls.Add(this.buttonSend);
			this.Controls.Add(this.boxMessage);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "ExceptionDialog";
			this.ResumeLayout(false);
			this.PerformLayout();
		}
		#endregion

		private void buttonSend_Click(object sender, System.EventArgs e)
		{
			if (boxFrom.Text == defaultFrom || boxFrom.Text.Split('@').Length != 2)
			{
				MessageBox.Show("Please enter your email address.");
				return;
			}

			Hide();

			MailMessage msg = new MailMessage();
			msg.From = new MailAddress(boxFrom.Text);
			msg.To.Add(boxEmailTo.Text);
			msg.Subject = "NoxMapEditor Crash Report";
			msg.Body = boxMessage.Text + (boxNotes.Text == "" ? "" : "\n\nNotes:\n" + boxNotes.Text);

			bool sent = false;
			try
			{
				foreach (string server in DnsLib.DnsApi.GetMXRecords(boxEmailTo.Text.Split('@')[1]))
				{
					SmtpClient smtpClient = new SmtpClient(server);

					smtpClient.Send(msg);
					sent = true;
					break;
				}
			}
			catch (Exception) { }
			if (!sent) MessageBox.Show("Couldn't send mail message.");
		}
	}
}
