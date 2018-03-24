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
            this.chkOverwrite = new System.Windows.Forms.CheckBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.radioOldADSR = new System.Windows.Forms.RadioButton();
            this.radioNewADSR = new System.Windows.Forms.RadioButton();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.chkPatchPatternOff = new System.Windows.Forms.CheckBox();
            this.chkPatchNoteOff = new System.Windows.Forms.CheckBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.radio3x = new System.Windows.Forms.RadioButton();
            this.radio4x = new System.Windows.Forms.RadioButton();
            this.label8 = new System.Windows.Forms.Label();
            this.radio2x = new System.Windows.Forms.RadioButton();
            this.radio1x = new System.Windows.Forms.RadioButton();
            this.Game = new System.Windows.Forms.GroupBox();
            this.radioALTTP = new System.Windows.Forms.RadioButton();
            this.radioSM = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.txtResample = new System.Windows.Forms.TextBox();
            this.chkTreble = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.lblExtraMusicData = new System.Windows.Forms.Label();
            this.lblMusicData = new System.Windows.Forms.Label();
            this.lblSamples = new System.Windows.Forms.Label();
            this.lblSamplesHeaders = new System.Windows.Forms.Label();
            this.lblInstruments = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.Game.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(205, 288);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(153, 32);
            this.button1.TabIndex = 0;
            this.button1.Text = "Convert .IT-file";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chkOverwrite);
            this.groupBox1.Controls.Add(this.groupBox5);
            this.groupBox1.Controls.Add(this.groupBox4);
            this.groupBox1.Controls.Add(this.groupBox3);
            this.groupBox1.Controls.Add(this.Game);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtResample);
            this.groupBox1.Controls.Add(this.chkTreble);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(551, 270);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Options";
            // 
            // chkOverwrite
            // 
            this.chkOverwrite.AutoSize = true;
            this.chkOverwrite.Location = new System.Drawing.Point(8, 74);
            this.chkOverwrite.Name = "chkOverwrite";
            this.chkOverwrite.Size = new System.Drawing.Size(231, 17);
            this.chkOverwrite.TabIndex = 9;
            this.chkOverwrite.Text = "Overwrite default samples/instruments/data";
            this.chkOverwrite.UseVisualStyleBackColor = true;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.radioOldADSR);
            this.groupBox5.Controls.Add(this.radioNewADSR);
            this.groupBox5.Location = new System.Drawing.Point(428, 189);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(117, 74);
            this.groupBox5.TabIndex = 8;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "ADSR Behaviour";
            // 
            // radioOldADSR
            // 
            this.radioOldADSR.AutoSize = true;
            this.radioOldADSR.Location = new System.Drawing.Point(6, 43);
            this.radioOldADSR.Name = "radioOldADSR";
            this.radioOldADSR.Size = new System.Drawing.Size(99, 17);
            this.radioOldADSR.TabIndex = 5;
            this.radioOldADSR.Text = "Old (tick based)";
            this.radioOldADSR.UseVisualStyleBackColor = true;
            // 
            // radioNewADSR
            // 
            this.radioNewADSR.AutoSize = true;
            this.radioNewADSR.Checked = true;
            this.radioNewADSR.Location = new System.Drawing.Point(6, 20);
            this.radioNewADSR.Name = "radioNewADSR";
            this.radioNewADSR.Size = new System.Drawing.Size(107, 17);
            this.radioNewADSR.TabIndex = 0;
            this.radioNewADSR.TabStop = true;
            this.radioNewADSR.Text = "New (time based)";
            this.radioNewADSR.UseVisualStyleBackColor = true;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.chkPatchPatternOff);
            this.groupBox4.Controls.Add(this.chkPatchNoteOff);
            this.groupBox4.Location = new System.Drawing.Point(7, 189);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(415, 74);
            this.groupBox4.TabIndex = 7;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "N-SPC Patches (experimental)";
            // 
            // chkPatchPatternOff
            // 
            this.chkPatchPatternOff.AutoSize = true;
            this.chkPatchPatternOff.Location = new System.Drawing.Point(6, 42);
            this.chkPatchPatternOff.Name = "chkPatchPatternOff";
            this.chkPatchPatternOff.Size = new System.Drawing.Size(181, 17);
            this.chkPatchPatternOff.TabIndex = 1;
            this.chkPatchPatternOff.Text = "Disable key-off between patterns";
            this.chkPatchPatternOff.UseVisualStyleBackColor = true;
            // 
            // chkPatchNoteOff
            // 
            this.chkPatchNoteOff.AutoSize = true;
            this.chkPatchNoteOff.Location = new System.Drawing.Point(6, 19);
            this.chkPatchNoteOff.Name = "chkPatchNoteOff";
            this.chkPatchNoteOff.Size = new System.Drawing.Size(381, 17);
            this.chkPatchNoteOff.TabIndex = 0;
            this.chkPatchNoteOff.Text = "Disable key-off between notes (can cause popping between note changes)";
            this.chkPatchNoteOff.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.radio3x);
            this.groupBox3.Controls.Add(this.radio4x);
            this.groupBox3.Controls.Add(this.label8);
            this.groupBox3.Controls.Add(this.radio2x);
            this.groupBox3.Controls.Add(this.radio1x);
            this.groupBox3.Location = new System.Drawing.Point(7, 103);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(415, 80);
            this.groupBox3.TabIndex = 6;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "N-SPC Engine Speed";
            // 
            // radio3x
            // 
            this.radio3x.AutoSize = true;
            this.radio3x.Location = new System.Drawing.Point(90, 19);
            this.radio3x.Name = "radio3x";
            this.radio3x.Size = new System.Drawing.Size(36, 17);
            this.radio3x.TabIndex = 8;
            this.radio3x.Text = "3x";
            this.radio3x.UseVisualStyleBackColor = true;
            // 
            // radio4x
            // 
            this.radio4x.AutoSize = true;
            this.radio4x.Location = new System.Drawing.Point(132, 19);
            this.radio4x.Name = "radio4x";
            this.radio4x.Size = new System.Drawing.Size(36, 17);
            this.radio4x.TabIndex = 7;
            this.radio4x.Text = "4x";
            this.radio4x.UseVisualStyleBackColor = true;
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(6, 42);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(403, 35);
            this.label8.TabIndex = 6;
            this.label8.Text = "Higher speeds will decrease gap between notes and can make fast note changes soun" +
    "d better, but it uses more data and can overload the SPC.";
            // 
            // radio2x
            // 
            this.radio2x.AutoSize = true;
            this.radio2x.Location = new System.Drawing.Point(48, 19);
            this.radio2x.Name = "radio2x";
            this.radio2x.Size = new System.Drawing.Size(36, 17);
            this.radio2x.TabIndex = 5;
            this.radio2x.Text = "2x";
            this.radio2x.UseVisualStyleBackColor = true;
            // 
            // radio1x
            // 
            this.radio1x.AutoSize = true;
            this.radio1x.Checked = true;
            this.radio1x.Location = new System.Drawing.Point(6, 19);
            this.radio1x.Name = "radio1x";
            this.radio1x.Size = new System.Drawing.Size(36, 17);
            this.radio1x.TabIndex = 0;
            this.radio1x.TabStop = true;
            this.radio1x.Text = "1x";
            this.radio1x.UseVisualStyleBackColor = true;
            // 
            // Game
            // 
            this.Game.Controls.Add(this.radioALTTP);
            this.Game.Controls.Add(this.radioSM);
            this.Game.Location = new System.Drawing.Point(428, 103);
            this.Game.Name = "Game";
            this.Game.Size = new System.Drawing.Size(117, 80);
            this.Game.TabIndex = 5;
            this.Game.TabStop = false;
            this.Game.Text = "Game";
            // 
            // radioALTTP
            // 
            this.radioALTTP.AutoSize = true;
            this.radioALTTP.Location = new System.Drawing.Point(6, 43);
            this.radioALTTP.Name = "radioALTTP";
            this.radioALTTP.Size = new System.Drawing.Size(109, 17);
            this.radioALTTP.TabIndex = 5;
            this.radioALTTP.Text = "A Link to the Past";
            this.radioALTTP.UseVisualStyleBackColor = true;
            // 
            // radioSM
            // 
            this.radioSM.AutoSize = true;
            this.radioSM.Checked = true;
            this.radioSM.Location = new System.Drawing.Point(6, 20);
            this.radioSM.Name = "radioSM";
            this.radioSM.Size = new System.Drawing.Size(91, 17);
            this.radioSM.TabIndex = 0;
            this.radioSM.TabStop = true;
            this.radioSM.Text = "Super Metroid";
            this.radioSM.UseVisualStyleBackColor = true;
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
            // txtResample
            // 
            this.txtResample.Location = new System.Drawing.Point(7, 43);
            this.txtResample.Name = "txtResample";
            this.txtResample.Size = new System.Drawing.Size(31, 20);
            this.txtResample.TabIndex = 1;
            this.txtResample.Text = "1.0";
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
            this.groupBox2.Location = new System.Drawing.Point(10, 326);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(551, 146);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Results";
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
            // lblExtraMusicData
            // 
            this.lblExtraMusicData.AutoSize = true;
            this.lblExtraMusicData.Location = new System.Drawing.Point(114, 106);
            this.lblExtraMusicData.Name = "lblExtraMusicData";
            this.lblExtraMusicData.Size = new System.Drawing.Size(0, 13);
            this.lblExtraMusicData.TabIndex = 9;
            // 
            // lblMusicData
            // 
            this.lblMusicData.AutoSize = true;
            this.lblMusicData.Location = new System.Drawing.Point(114, 83);
            this.lblMusicData.Name = "lblMusicData";
            this.lblMusicData.Size = new System.Drawing.Size(0, 13);
            this.lblMusicData.TabIndex = 8;
            // 
            // lblSamples
            // 
            this.lblSamples.AutoSize = true;
            this.lblSamples.Location = new System.Drawing.Point(114, 60);
            this.lblSamples.Name = "lblSamples";
            this.lblSamples.Size = new System.Drawing.Size(0, 13);
            this.lblSamples.TabIndex = 7;
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
            // lblInstruments
            // 
            this.lblInstruments.AutoSize = true;
            this.lblInstruments.Location = new System.Drawing.Point(114, 16);
            this.lblInstruments.Name = "lblInstruments";
            this.lblInstruments.Size = new System.Drawing.Size(0, 13);
            this.lblInstruments.TabIndex = 5;
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
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(8, 60);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(50, 13);
            this.label4.TabIndex = 2;
            this.label4.Text = "Samples:";
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
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(64, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Instruments:";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(10, 478);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(98, 36);
            this.button2.TabIndex = 3;
            this.button2.Text = "Save SPC file";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(448, 478);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(113, 36);
            this.button3.TabIndex = 4;
            this.button3.Text = "Save N-SPC data";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(575, 520);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.button1);
            this.Name = "Main";
            this.Text = "mITroid Music Converter v0.99.2";
            this.Load += new System.EventHandler(this.Main_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.Game.ResumeLayout(false);
            this.Game.PerformLayout();
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
        private System.Windows.Forms.GroupBox Game;
        private System.Windows.Forms.RadioButton radioALTTP;
        private System.Windows.Forms.RadioButton radioSM;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.RadioButton radio4x;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.RadioButton radio2x;
        private System.Windows.Forms.RadioButton radio1x;
        private System.Windows.Forms.RadioButton radio3x;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.CheckBox chkPatchPatternOff;
        private System.Windows.Forms.CheckBox chkPatchNoteOff;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.RadioButton radioOldADSR;
        private System.Windows.Forms.RadioButton radioNewADSR;
        private System.Windows.Forms.CheckBox chkOverwrite;
    }
}

