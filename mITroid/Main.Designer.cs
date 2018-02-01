namespace mITroid
{
    partial class Main
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
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chkTreble = new System.Windows.Forms.CheckBox();
            this.txtResample = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.lblInstruments = new System.Windows.Forms.Label();
            this.lblSamplesHeaders = new System.Windows.Forms.Label();
            this.lblSamples = new System.Windows.Forms.Label();
            this.lblMusicData = new System.Windows.Forms.Label();
            this.lblExtraMusicData = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(240, 101);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(98, 46);
            this.button1.TabIndex = 0;
            this.button1.Text = "Convert .IT-file";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtResample);
            this.groupBox1.Controls.Add(this.chkTreble);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(551, 83);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Options";
            // 
            // chkTreble
            // 
            this.chkTreble.AutoSize = true;
            this.chkTreble.Checked = true;
            this.chkTreble.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkTreble.Location = new System.Drawing.Point(7, 20);
            this.chkTreble.Name = "chkTreble";
            this.chkTreble.Size = new System.Drawing.Size(285, 17);
            this.chkTreble.TabIndex = 0;
            this.chkTreble.Text = "Enhance treble to compensate for SNES gaussian filter";
            this.chkTreble.UseVisualStyleBackColor = true;
            // 
            // txtResample
            // 
            this.txtResample.Location = new System.Drawing.Point(7, 43);
            this.txtResample.Name = "txtResample";
            this.txtResample.Size = new System.Drawing.Size(31, 20);
            this.txtResample.TabIndex = 1;
            this.txtResample.Text = "1.0";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(44, 46);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(338, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Resample factor (0.0-4.0) - Below 1 upsamples, above 1 downsamples";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.lblExtraMusicData);
            this.groupBox2.Controls.Add(this.lblMusicData);
            this.groupBox2.Controls.Add(this.lblSamples);
            this.groupBox2.Controls.Add(this.lblSamplesHeaders);
            this.groupBox2.Controls.Add(this.lblInstruments);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Location = new System.Drawing.Point(12, 153);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(551, 146);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Results";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(64, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Instruments:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 38);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(86, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "Sample headers:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(8, 60);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(50, 13);
            this.label4.TabIndex = 2;
            this.label4.Text = "Samples:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(8, 83);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(62, 13);
            this.label5.TabIndex = 3;
            this.label5.Text = "Music data:";
            this.label5.Click += new System.EventHandler(this.label5_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(8, 106);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(88, 13);
            this.label6.TabIndex = 4;
            this.label6.Text = "Extra music data:";
            // 
            // lblInstruments
            // 
            this.lblInstruments.AutoSize = true;
            this.lblInstruments.Location = new System.Drawing.Point(114, 16);
            this.lblInstruments.Name = "lblInstruments";
            this.lblInstruments.Size = new System.Drawing.Size(0, 13);
            this.lblInstruments.TabIndex = 5;
            // 
            // lblSamplesHeaders
            // 
            this.lblSamplesHeaders.AutoSize = true;
            this.lblSamplesHeaders.Location = new System.Drawing.Point(114, 38);
            this.lblSamplesHeaders.Name = "lblSamplesHeaders";
            this.lblSamplesHeaders.Size = new System.Drawing.Size(0, 13);
            this.lblSamplesHeaders.TabIndex = 6;
            this.lblSamplesHeaders.Click += new System.EventHandler(this.label8_Click);
            // 
            // lblSamples
            // 
            this.lblSamples.AutoSize = true;
            this.lblSamples.Location = new System.Drawing.Point(114, 60);
            this.lblSamples.Name = "lblSamples";
            this.lblSamples.Size = new System.Drawing.Size(0, 13);
            this.lblSamples.TabIndex = 7;
            // 
            // lblMusicData
            // 
            this.lblMusicData.AutoSize = true;
            this.lblMusicData.Location = new System.Drawing.Point(114, 83);
            this.lblMusicData.Name = "lblMusicData";
            this.lblMusicData.Size = new System.Drawing.Size(0, 13);
            this.lblMusicData.TabIndex = 8;
            // 
            // lblExtraMusicData
            // 
            this.lblExtraMusicData.AutoSize = true;
            this.lblExtraMusicData.Location = new System.Drawing.Point(114, 106);
            this.lblExtraMusicData.Name = "lblExtraMusicData";
            this.lblExtraMusicData.Size = new System.Drawing.Size(0, 13);
            this.lblExtraMusicData.TabIndex = 9;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(12, 326);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(98, 36);
            this.button2.TabIndex = 3;
            this.button2.Text = "Save SPC file";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(450, 326);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(113, 36);
            this.button3.TabIndex = 4;
            this.button3.Text = "Save N-SPC data";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(8, 125);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(304, 13);
            this.label7.TabIndex = 5;
            this.label7.Text = "Extra music data is patterns relocated to unused sample space.";
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(575, 375);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.button1);
            this.Name = "Main";
            this.Text = "mITroid Music Converter";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtResample;
        private System.Windows.Forms.CheckBox chkTreble;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label lblSamplesHeaders;
        private System.Windows.Forms.Label lblInstruments;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblExtraMusicData;
        private System.Windows.Forms.Label lblMusicData;
        private System.Windows.Forms.Label lblSamples;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Label label7;
    }
}

