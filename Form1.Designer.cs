namespace MediaTrigger
{
	partial class Form1
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
			this._formMediaPlayer = new AxWMPLib.AxWindowsMediaPlayer();
			((System.ComponentModel.ISupportInitialize)(this._formMediaPlayer)).BeginInit();
			this.SuspendLayout();
			// 
			// _formMediaPlayer
			// 
			this._formMediaPlayer.Enabled = true;
			this._formMediaPlayer.Location = new System.Drawing.Point(-1, 0);
			this._formMediaPlayer.Name = "_formMediaPlayer";
			this._formMediaPlayer.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("_formMediaPlayer.OcxState")));
			this._formMediaPlayer.Size = new System.Drawing.Size(285, 261);
			this._formMediaPlayer.TabIndex = 0;
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(284, 261);
			this.Controls.Add(this._formMediaPlayer);
			this.Name = "Form1";
			this.Opacity = 0D;
			this.Text = "Form1";
			((System.ComponentModel.ISupportInitialize)(this._formMediaPlayer)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private AxWMPLib.AxWindowsMediaPlayer _formMediaPlayer;

	}
}

