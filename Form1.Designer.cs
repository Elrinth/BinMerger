namespace BinMerger
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
      this.txtLog = new System.Windows.Forms.TextBox();
      this.Log = new System.Windows.Forms.Label();
      this.txtFolderBinCues = new System.Windows.Forms.TextBox();
      this.label1 = new System.Windows.Forms.Label();
      this.button1 = new System.Windows.Forms.Button();
      this.btnStart = new System.Windows.Forms.Button();
      this.btnClearLog = new System.Windows.Forms.Button();
      this.label2 = new System.Windows.Forms.Label();
      this.label3 = new System.Windows.Forms.Label();
      this.SuspendLayout();
      // 
      // txtLog
      // 
      this.txtLog.Location = new System.Drawing.Point(28, 95);
      this.txtLog.Multiline = true;
      this.txtLog.Name = "txtLog";
      this.txtLog.Size = new System.Drawing.Size(611, 211);
      this.txtLog.TabIndex = 0;
      // 
      // Log
      // 
      this.Log.AutoSize = true;
      this.Log.Location = new System.Drawing.Point(28, 79);
      this.Log.Name = "Log";
      this.Log.Size = new System.Drawing.Size(25, 13);
      this.Log.TabIndex = 1;
      this.Log.Text = "Log";
      // 
      // txtFolderBinCues
      // 
      this.txtFolderBinCues.Location = new System.Drawing.Point(198, 42);
      this.txtFolderBinCues.Name = "txtFolderBinCues";
      this.txtFolderBinCues.Size = new System.Drawing.Size(211, 20);
      this.txtFolderBinCues.TabIndex = 2;
      this.txtFolderBinCues.TextChanged += new System.EventHandler(this.txtFolderBinCues_TextChanged);
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(25, 45);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(158, 13);
      this.label1.TabIndex = 3;
      this.label1.Text = "Root folder to find .bin/.cue files";
      // 
      // button1
      // 
      this.button1.Location = new System.Drawing.Point(415, 39);
      this.button1.Name = "button1";
      this.button1.Size = new System.Drawing.Size(75, 23);
      this.button1.TabIndex = 4;
      this.button1.Text = "Browse...";
      this.button1.UseVisualStyleBackColor = true;
      this.button1.Click += new System.EventHandler(this.button1_Click);
      // 
      // btnStart
      // 
      this.btnStart.Enabled = false;
      this.btnStart.Location = new System.Drawing.Point(564, 40);
      this.btnStart.Name = "btnStart";
      this.btnStart.Size = new System.Drawing.Size(75, 23);
      this.btnStart.TabIndex = 5;
      this.btnStart.Text = "Start";
      this.btnStart.UseVisualStyleBackColor = true;
      this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
      // 
      // btnClearLog
      // 
      this.btnClearLog.Location = new System.Drawing.Point(564, 69);
      this.btnClearLog.Name = "btnClearLog";
      this.btnClearLog.Size = new System.Drawing.Size(75, 23);
      this.btnClearLog.TabIndex = 6;
      this.btnClearLog.Text = "Clear Log";
      this.btnClearLog.UseVisualStyleBackColor = true;
      this.btnClearLog.Click += new System.EventHandler(this.btnClearLog_Click);
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(25, 9);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(382, 13);
      this.label2.TabIndex = 7;
      this.label2.Text = "Select a folder on your computer where you have .bin/.cue combination games.";
      // 
      // label3
      // 
      this.label3.AutoSize = true;
      this.label3.Location = new System.Drawing.Point(25, 22);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(537, 13);
      this.label3.TabIndex = 8;
      this.label3.Text = "This will check all subdirectories for this and combine the .bins into single bin" +
    " and update the .cue to refer to that.";
      // 
      // Form1
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(672, 325);
      this.Controls.Add(this.label3);
      this.Controls.Add(this.label2);
      this.Controls.Add(this.btnClearLog);
      this.Controls.Add(this.btnStart);
      this.Controls.Add(this.button1);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.txtFolderBinCues);
      this.Controls.Add(this.Log);
      this.Controls.Add(this.txtLog);
      this.Name = "Form1";
      this.Text = "Bin Merger";
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.TextBox txtLog;
    private System.Windows.Forms.Label Log;
    private System.Windows.Forms.TextBox txtFolderBinCues;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Button button1;
    private System.Windows.Forms.Button btnStart;
    private System.Windows.Forms.Button btnClearLog;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.Label label3;
  }
}

