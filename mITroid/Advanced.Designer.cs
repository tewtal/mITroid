namespace mITroid
{
    partial class Advanced
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnLoadInfoFromRom = new System.Windows.Forms.Button();
            this.label12 = new System.Windows.Forms.Label();
            this.txtMusicIndex = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtInstrumentIndexOffset = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtSampleIndexOffset = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtSongIndex = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtEchoBufferLen = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtEchoBuffer = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtSamples = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtPatternData = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtInstrumentHeaders = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtSampleHeaders = new System.Windows.Forms.TextBox();
            this.cboGamePreset = new System.Windows.Forms.ComboBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lstSPCData = new System.Windows.Forms.ListBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.chkRepointEchoBuffer = new System.Windows.Forms.CheckBox();
            this.chkRepointMusicData = new System.Windows.Forms.CheckBox();
            this.chkDisableKeyOffPatterns = new System.Windows.Forms.CheckBox();
            this.chkDisableKeyOffNotes = new System.Windows.Forms.CheckBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.btnRemove = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.lstInputFiles = new System.Windows.Forms.ListBox();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.chkExportPatches = new System.Windows.Forms.CheckBox();
            this.chkExportMusicIndex = new System.Windows.Forms.CheckBox();
            this.btnWriteNSPC = new System.Windows.Forms.Button();
            this.btnCreateSPC = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.txtSPCTemplate = new System.Windows.Forms.TextBox();
            this.chkExportMusicData = new System.Windows.Forms.CheckBox();
            this.chkExportSamples = new System.Windows.Forms.CheckBox();
            this.chkExportInstruments = new System.Windows.Forms.CheckBox();
            this.chkExportSampleHeaders = new System.Windows.Forms.CheckBox();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.chkOptimizePatterns = new System.Windows.Forms.CheckBox();
            this.chkSetupPattern = new System.Windows.Forms.CheckBox();
            this.chkOptimizeTracks = new System.Windows.Forms.CheckBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txtResample = new System.Windows.Forms.TextBox();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.radio05x = new System.Windows.Forms.RadioButton();
            this.radio3x = new System.Windows.Forms.RadioButton();
            this.radio4x = new System.Windows.Forms.RadioButton();
            this.radio2x = new System.Windows.Forms.RadioButton();
            this.radio1x = new System.Windows.Forms.RadioButton();
            this.chkEnhanceTreble = new System.Windows.Forms.CheckBox();
            this.btnGenerate = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.groupBox8.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnLoadInfoFromRom);
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Controls.Add(this.txtMusicIndex);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.txtInstrumentIndexOffset);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.txtSampleIndexOffset);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.txtSongIndex);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.txtEchoBufferLen);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.txtEchoBuffer);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txtSamples);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtPatternData);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtInstrumentHeaders);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtSampleHeaders);
            this.groupBox1.Location = new System.Drawing.Point(14, 80);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBox1.Size = new System.Drawing.Size(294, 401);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "SPC RAM Mapping";
            // 
            // btnLoadInfoFromRom
            // 
            this.btnLoadInfoFromRom.Location = new System.Drawing.Point(11, 346);
            this.btnLoadInfoFromRom.Name = "btnLoadInfoFromRom";
            this.btnLoadInfoFromRom.Size = new System.Drawing.Size(276, 36);
            this.btnLoadInfoFromRom.TabIndex = 20;
            this.btnLoadInfoFromRom.Text = "Load mapping from patched PJBoy engine ROM";
            this.btnLoadInfoFromRom.UseVisualStyleBackColor = true;
            this.btnLoadInfoFromRom.Visible = false;
            this.btnLoadInfoFromRom.Click += new System.EventHandler(this.btnLoadInfoFromRom_Click);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(7, 147);
            this.label12.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(71, 15);
            this.label12.TabIndex = 19;
            this.label12.Text = "Music index";
            // 
            // txtMusicIndex
            // 
            this.txtMusicIndex.Location = new System.Drawing.Point(174, 143);
            this.txtMusicIndex.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txtMusicIndex.Name = "txtMusicIndex";
            this.txtMusicIndex.Size = new System.Drawing.Size(112, 23);
            this.txtMusicIndex.TabIndex = 18;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(7, 115);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(130, 15);
            this.label9.TabIndex = 17;
            this.label9.Text = "Instrument index offset";
            // 
            // txtInstrumentIndexOffset
            // 
            this.txtInstrumentIndexOffset.Location = new System.Drawing.Point(174, 112);
            this.txtInstrumentIndexOffset.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txtInstrumentIndexOffset.Name = "txtInstrumentIndexOffset";
            this.txtInstrumentIndexOffset.Size = new System.Drawing.Size(112, 23);
            this.txtInstrumentIndexOffset.TabIndex = 16;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(7, 85);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(111, 15);
            this.label10.TabIndex = 15;
            this.label10.Text = "Sample index offset";
            // 
            // txtSampleIndexOffset
            // 
            this.txtSampleIndexOffset.Location = new System.Drawing.Point(174, 82);
            this.txtSampleIndexOffset.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txtSampleIndexOffset.Name = "txtSampleIndexOffset";
            this.txtSampleIndexOffset.Size = new System.Drawing.Size(112, 23);
            this.txtSampleIndexOffset.TabIndex = 14;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(7, 207);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(92, 15);
            this.label7.TabIndex = 13;
            this.label7.Text = "Start song index";
            // 
            // txtSongIndex
            // 
            this.txtSongIndex.Location = new System.Drawing.Point(174, 203);
            this.txtSongIndex.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txtSongIndex.Name = "txtSongIndex";
            this.txtSongIndex.Size = new System.Drawing.Size(112, 23);
            this.txtSongIndex.TabIndex = 12;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(7, 297);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(105, 15);
            this.label6.TabIndex = 11;
            this.label6.Text = "Echo buffer length";
            // 
            // txtEchoBufferLen
            // 
            this.txtEchoBufferLen.Location = new System.Drawing.Point(174, 293);
            this.txtEchoBufferLen.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txtEchoBufferLen.Name = "txtEchoBufferLen";
            this.txtEchoBufferLen.Size = new System.Drawing.Size(112, 23);
            this.txtEchoBufferLen.TabIndex = 10;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(7, 267);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(68, 15);
            this.label5.TabIndex = 9;
            this.label5.Text = "Echo buffer";
            // 
            // txtEchoBuffer
            // 
            this.txtEchoBuffer.Location = new System.Drawing.Point(174, 263);
            this.txtEchoBuffer.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txtEchoBuffer.Name = "txtEchoBuffer";
            this.txtEchoBuffer.Size = new System.Drawing.Size(112, 23);
            this.txtEchoBuffer.TabIndex = 8;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(7, 237);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(51, 15);
            this.label4.TabIndex = 7;
            this.label4.Text = "Samples";
            // 
            // txtSamples
            // 
            this.txtSamples.Location = new System.Drawing.Point(174, 233);
            this.txtSamples.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txtSamples.Name = "txtSamples";
            this.txtSamples.Size = new System.Drawing.Size(112, 23);
            this.txtSamples.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 177);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(106, 15);
            this.label3.TabIndex = 5;
            this.label3.Text = "Music pattern data";
            // 
            // txtPatternData
            // 
            this.txtPatternData.Location = new System.Drawing.Point(174, 173);
            this.txtPatternData.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txtPatternData.Name = "txtPatternData";
            this.txtPatternData.Size = new System.Drawing.Size(112, 23);
            this.txtPatternData.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 55);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 15);
            this.label2.TabIndex = 3;
            this.label2.Text = "Instruments";
            // 
            // txtInstrumentHeaders
            // 
            this.txtInstrumentHeaders.Location = new System.Drawing.Point(174, 52);
            this.txtInstrumentHeaders.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txtInstrumentHeaders.Name = "txtInstrumentHeaders";
            this.txtInstrumentHeaders.Size = new System.Drawing.Size(112, 23);
            this.txtInstrumentHeaders.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 25);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(90, 15);
            this.label1.TabIndex = 1;
            this.label1.Text = "Sample headers";
            // 
            // txtSampleHeaders
            // 
            this.txtSampleHeaders.Location = new System.Drawing.Point(174, 22);
            this.txtSampleHeaders.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txtSampleHeaders.Name = "txtSampleHeaders";
            this.txtSampleHeaders.Size = new System.Drawing.Size(112, 23);
            this.txtSampleHeaders.TabIndex = 0;
            // 
            // cboGamePreset
            // 
            this.cboGamePreset.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboGamePreset.FormattingEnabled = true;
            this.cboGamePreset.Items.AddRange(new object[] {
            "Super Metroid",
            "Super Metroid (overwrite)",
            "A Link to the Past",
            "A Link to the Past (overwrite)",
            "Custom"});
            this.cboGamePreset.Location = new System.Drawing.Point(7, 22);
            this.cboGamePreset.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.cboGamePreset.Name = "cboGamePreset";
            this.cboGamePreset.Size = new System.Drawing.Size(279, 23);
            this.cboGamePreset.TabIndex = 1;
            this.cboGamePreset.SelectedIndexChanged += new System.EventHandler(this.cboGamePreset_SelectedIndexChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.cboGamePreset);
            this.groupBox2.Location = new System.Drawing.Point(14, 14);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBox2.Size = new System.Drawing.Size(294, 59);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Game preset";
            // 
            // lstSPCData
            // 
            this.lstSPCData.FormattingEnabled = true;
            this.lstSPCData.ItemHeight = 15;
            this.lstSPCData.Location = new System.Drawing.Point(7, 22);
            this.lstSPCData.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.lstSPCData.Name = "lstSPCData";
            this.lstSPCData.Size = new System.Drawing.Size(513, 274);
            this.lstSPCData.TabIndex = 3;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.lstSPCData);
            this.groupBox3.Location = new System.Drawing.Point(315, 14);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBox3.Size = new System.Drawing.Size(527, 303);
            this.groupBox3.TabIndex = 4;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Generated SPC Data";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.chkRepointEchoBuffer);
            this.groupBox4.Controls.Add(this.chkRepointMusicData);
            this.groupBox4.Controls.Add(this.chkDisableKeyOffPatterns);
            this.groupBox4.Controls.Add(this.chkDisableKeyOffNotes);
            this.groupBox4.Location = new System.Drawing.Point(315, 324);
            this.groupBox4.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBox4.Size = new System.Drawing.Size(527, 83);
            this.groupBox4.TabIndex = 5;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Patches";
            // 
            // chkRepointEchoBuffer
            // 
            this.chkRepointEchoBuffer.AutoSize = true;
            this.chkRepointEchoBuffer.Location = new System.Drawing.Point(234, 48);
            this.chkRepointEchoBuffer.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.chkRepointEchoBuffer.Name = "chkRepointEchoBuffer";
            this.chkRepointEchoBuffer.Size = new System.Drawing.Size(131, 19);
            this.chkRepointEchoBuffer.TabIndex = 8;
            this.chkRepointEchoBuffer.Text = "Repoint echo buffer";
            this.chkRepointEchoBuffer.UseVisualStyleBackColor = true;
            // 
            // chkRepointMusicData
            // 
            this.chkRepointMusicData.AutoSize = true;
            this.chkRepointMusicData.Location = new System.Drawing.Point(234, 22);
            this.chkRepointMusicData.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.chkRepointMusicData.Name = "chkRepointMusicData";
            this.chkRepointMusicData.Size = new System.Drawing.Size(169, 19);
            this.chkRepointMusicData.TabIndex = 7;
            this.chkRepointMusicData.Text = "Repoint music pattern data";
            this.chkRepointMusicData.UseVisualStyleBackColor = true;
            // 
            // chkDisableKeyOffPatterns
            // 
            this.chkDisableKeyOffPatterns.AutoSize = true;
            this.chkDisableKeyOffPatterns.Location = new System.Drawing.Point(10, 48);
            this.chkDisableKeyOffPatterns.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.chkDisableKeyOffPatterns.Name = "chkDisableKeyOffPatterns";
            this.chkDisableKeyOffPatterns.Size = new System.Drawing.Size(199, 19);
            this.chkDisableKeyOffPatterns.TabIndex = 6;
            this.chkDisableKeyOffPatterns.Text = "Disable key-off between patterns";
            this.chkDisableKeyOffPatterns.UseVisualStyleBackColor = true;
            // 
            // chkDisableKeyOffNotes
            // 
            this.chkDisableKeyOffNotes.AutoSize = true;
            this.chkDisableKeyOffNotes.Location = new System.Drawing.Point(10, 22);
            this.chkDisableKeyOffNotes.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.chkDisableKeyOffNotes.Name = "chkDisableKeyOffNotes";
            this.chkDisableKeyOffNotes.Size = new System.Drawing.Size(185, 19);
            this.chkDisableKeyOffNotes.TabIndex = 0;
            this.chkDisableKeyOffNotes.Text = "Disable key-off between notes";
            this.chkDisableKeyOffNotes.UseVisualStyleBackColor = true;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.btnRemove);
            this.groupBox5.Controls.Add(this.btnAdd);
            this.groupBox5.Controls.Add(this.lstInputFiles);
            this.groupBox5.Location = new System.Drawing.Point(13, 549);
            this.groupBox5.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBox5.Size = new System.Drawing.Size(294, 123);
            this.groupBox5.TabIndex = 6;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Input";
            // 
            // btnRemove
            // 
            this.btnRemove.Location = new System.Drawing.Point(201, 77);
            this.btnRemove.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(86, 29);
            this.btnRemove.TabIndex = 7;
            this.btnRemove.Text = "Remove";
            this.btnRemove.UseVisualStyleBackColor = true;
            this.btnRemove.Visible = false;
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(7, 77);
            this.btnAdd.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(86, 29);
            this.btnAdd.TabIndex = 1;
            this.btnAdd.Text = "Open IT file";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // lstInputFiles
            // 
            this.lstInputFiles.FormattingEnabled = true;
            this.lstInputFiles.ItemHeight = 15;
            this.lstInputFiles.Location = new System.Drawing.Point(7, 22);
            this.lstInputFiles.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.lstInputFiles.Name = "lstInputFiles";
            this.lstInputFiles.Size = new System.Drawing.Size(279, 49);
            this.lstInputFiles.TabIndex = 0;
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.chkExportPatches);
            this.groupBox6.Controls.Add(this.chkExportMusicIndex);
            this.groupBox6.Controls.Add(this.btnWriteNSPC);
            this.groupBox6.Controls.Add(this.btnCreateSPC);
            this.groupBox6.Controls.Add(this.label8);
            this.groupBox6.Controls.Add(this.txtSPCTemplate);
            this.groupBox6.Controls.Add(this.chkExportMusicData);
            this.groupBox6.Controls.Add(this.chkExportSamples);
            this.groupBox6.Controls.Add(this.chkExportInstruments);
            this.groupBox6.Controls.Add(this.chkExportSampleHeaders);
            this.groupBox6.Location = new System.Drawing.Point(315, 549);
            this.groupBox6.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBox6.Size = new System.Drawing.Size(530, 129);
            this.groupBox6.TabIndex = 7;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Output";
            // 
            // chkExportPatches
            // 
            this.chkExportPatches.AutoSize = true;
            this.chkExportPatches.Checked = true;
            this.chkExportPatches.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkExportPatches.Location = new System.Drawing.Point(305, 42);
            this.chkExportPatches.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.chkExportPatches.Name = "chkExportPatches";
            this.chkExportPatches.Size = new System.Drawing.Size(104, 19);
            this.chkExportPatches.TabIndex = 9;
            this.chkExportPatches.Text = "Export patches";
            this.chkExportPatches.UseVisualStyleBackColor = true;
            // 
            // chkExportMusicIndex
            // 
            this.chkExportMusicIndex.AutoSize = true;
            this.chkExportMusicIndex.Checked = true;
            this.chkExportMusicIndex.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkExportMusicIndex.Location = new System.Drawing.Point(305, 16);
            this.chkExportMusicIndex.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.chkExportMusicIndex.Name = "chkExportMusicIndex";
            this.chkExportMusicIndex.Size = new System.Drawing.Size(127, 19);
            this.chkExportMusicIndex.TabIndex = 8;
            this.chkExportMusicIndex.Text = "Export music index";
            this.chkExportMusicIndex.UseVisualStyleBackColor = true;
            // 
            // btnWriteNSPC
            // 
            this.btnWriteNSPC.Location = new System.Drawing.Point(399, 94);
            this.btnWriteNSPC.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnWriteNSPC.Name = "btnWriteNSPC";
            this.btnWriteNSPC.Size = new System.Drawing.Size(124, 29);
            this.btnWriteNSPC.TabIndex = 7;
            this.btnWriteNSPC.Text = "Write NSPC-data";
            this.btnWriteNSPC.UseVisualStyleBackColor = true;
            this.btnWriteNSPC.Click += new System.EventHandler(this.btnWriteNSPC_Click);
            // 
            // btnCreateSPC
            // 
            this.btnCreateSPC.Location = new System.Drawing.Point(7, 94);
            this.btnCreateSPC.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnCreateSPC.Name = "btnCreateSPC";
            this.btnCreateSPC.Size = new System.Drawing.Size(118, 29);
            this.btnCreateSPC.TabIndex = 6;
            this.btnCreateSPC.Text = "Create SPC-file";
            this.btnCreateSPC.UseVisualStyleBackColor = true;
            this.btnCreateSPC.Click += new System.EventHandler(this.btnCreateSPC_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(7, 69);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(127, 15);
            this.label8.TabIndex = 5;
            this.label8.Text = "SPC template filename";
            // 
            // txtSPCTemplate
            // 
            this.txtSPCTemplate.Location = new System.Drawing.Point(302, 66);
            this.txtSPCTemplate.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txtSPCTemplate.Name = "txtSPCTemplate";
            this.txtSPCTemplate.Size = new System.Drawing.Size(218, 23);
            this.txtSPCTemplate.TabIndex = 4;
            // 
            // chkExportMusicData
            // 
            this.chkExportMusicData.AutoSize = true;
            this.chkExportMusicData.Checked = true;
            this.chkExportMusicData.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkExportMusicData.Location = new System.Drawing.Point(172, 42);
            this.chkExportMusicData.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.chkExportMusicData.Name = "chkExportMusicData";
            this.chkExportMusicData.Size = new System.Drawing.Size(121, 19);
            this.chkExportMusicData.TabIndex = 3;
            this.chkExportMusicData.Text = "Export music data";
            this.chkExportMusicData.UseVisualStyleBackColor = true;
            // 
            // chkExportSamples
            // 
            this.chkExportSamples.AutoSize = true;
            this.chkExportSamples.Checked = true;
            this.chkExportSamples.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkExportSamples.Location = new System.Drawing.Point(172, 16);
            this.chkExportSamples.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.chkExportSamples.Name = "chkExportSamples";
            this.chkExportSamples.Size = new System.Drawing.Size(106, 19);
            this.chkExportSamples.TabIndex = 2;
            this.chkExportSamples.Text = "Export samples";
            this.chkExportSamples.UseVisualStyleBackColor = true;
            // 
            // chkExportInstruments
            // 
            this.chkExportInstruments.AutoSize = true;
            this.chkExportInstruments.Checked = true;
            this.chkExportInstruments.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkExportInstruments.Location = new System.Drawing.Point(10, 42);
            this.chkExportInstruments.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.chkExportInstruments.Name = "chkExportInstruments";
            this.chkExportInstruments.Size = new System.Drawing.Size(126, 19);
            this.chkExportInstruments.TabIndex = 1;
            this.chkExportInstruments.Text = "Export instruments";
            this.chkExportInstruments.UseVisualStyleBackColor = true;
            // 
            // chkExportSampleHeaders
            // 
            this.chkExportSampleHeaders.AutoSize = true;
            this.chkExportSampleHeaders.Checked = true;
            this.chkExportSampleHeaders.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkExportSampleHeaders.Location = new System.Drawing.Point(10, 16);
            this.chkExportSampleHeaders.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.chkExportSampleHeaders.Name = "chkExportSampleHeaders";
            this.chkExportSampleHeaders.Size = new System.Drawing.Size(145, 19);
            this.chkExportSampleHeaders.TabIndex = 0;
            this.chkExportSampleHeaders.Text = "Export sample headers";
            this.chkExportSampleHeaders.UseVisualStyleBackColor = true;
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.chkOptimizePatterns);
            this.groupBox7.Controls.Add(this.chkSetupPattern);
            this.groupBox7.Controls.Add(this.chkOptimizeTracks);
            this.groupBox7.Controls.Add(this.label11);
            this.groupBox7.Controls.Add(this.txtResample);
            this.groupBox7.Controls.Add(this.groupBox8);
            this.groupBox7.Controls.Add(this.chkEnhanceTreble);
            this.groupBox7.Controls.Add(this.btnGenerate);
            this.groupBox7.Location = new System.Drawing.Point(315, 414);
            this.groupBox7.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBox7.Size = new System.Drawing.Size(530, 129);
            this.groupBox7.TabIndex = 8;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "N-SPC Generation";
            this.groupBox7.Enter += new System.EventHandler(this.groupBox7_Enter);
            // 
            // chkOptimizePatterns
            // 
            this.chkOptimizePatterns.AutoSize = true;
            this.chkOptimizePatterns.Location = new System.Drawing.Point(10, 73);
            this.chkOptimizePatterns.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.chkOptimizePatterns.Name = "chkOptimizePatterns";
            this.chkOptimizePatterns.Size = new System.Drawing.Size(221, 19);
            this.chkOptimizePatterns.TabIndex = 12;
            this.chkOptimizePatterns.Text = "Optimize pattern data (Experimental)";
            this.chkOptimizePatterns.UseVisualStyleBackColor = true;
            // 
            // chkSetupPattern
            // 
            this.chkSetupPattern.AutoSize = true;
            this.chkSetupPattern.Checked = true;
            this.chkSetupPattern.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkSetupPattern.Location = new System.Drawing.Point(10, 98);
            this.chkSetupPattern.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.chkSetupPattern.Name = "chkSetupPattern";
            this.chkSetupPattern.Size = new System.Drawing.Size(133, 19);
            this.chkSetupPattern.TabIndex = 11;
            this.chkSetupPattern.Text = "Create setup pattern";
            this.chkSetupPattern.UseVisualStyleBackColor = true;
            // 
            // chkOptimizeTracks
            // 
            this.chkOptimizeTracks.AutoSize = true;
            this.chkOptimizeTracks.Checked = true;
            this.chkOptimizeTracks.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkOptimizeTracks.Location = new System.Drawing.Point(10, 48);
            this.chkOptimizeTracks.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.chkOptimizeTracks.Name = "chkOptimizeTracks";
            this.chkOptimizeTracks.Size = new System.Drawing.Size(160, 19);
            this.chkOptimizeTracks.TabIndex = 10;
            this.chkOptimizeTracks.Text = "Optimize duplicate tracks";
            this.chkOptimizeTracks.UseVisualStyleBackColor = true;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(380, 63);
            this.label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(92, 15);
            this.label11.TabIndex = 9;
            this.label11.Text = "Resample factor";
            // 
            // txtResample
            // 
            this.txtResample.Location = new System.Drawing.Point(485, 59);
            this.txtResample.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txtResample.Name = "txtResample";
            this.txtResample.Size = new System.Drawing.Size(35, 23);
            this.txtResample.TabIndex = 8;
            this.txtResample.Text = "1.0";
            // 
            // groupBox8
            // 
            this.groupBox8.Controls.Add(this.radio05x);
            this.groupBox8.Controls.Add(this.radio3x);
            this.groupBox8.Controls.Add(this.radio4x);
            this.groupBox8.Controls.Add(this.radio2x);
            this.groupBox8.Controls.Add(this.radio1x);
            this.groupBox8.Location = new System.Drawing.Point(262, 9);
            this.groupBox8.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBox8.Size = new System.Drawing.Size(258, 46);
            this.groupBox8.TabIndex = 7;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = "Engine Speed";
            // 
            // radio05x
            // 
            this.radio05x.AutoSize = true;
            this.radio05x.Location = new System.Drawing.Point(203, 22);
            this.radio05x.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.radio05x.Name = "radio05x";
            this.radio05x.Size = new System.Drawing.Size(46, 19);
            this.radio05x.TabIndex = 9;
            this.radio05x.Text = "0.5x";
            this.radio05x.UseVisualStyleBackColor = true;
            // 
            // radio3x
            // 
            this.radio3x.AutoSize = true;
            this.radio3x.Location = new System.Drawing.Point(105, 22);
            this.radio3x.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.radio3x.Name = "radio3x";
            this.radio3x.Size = new System.Drawing.Size(37, 19);
            this.radio3x.TabIndex = 8;
            this.radio3x.Text = "3x";
            this.radio3x.UseVisualStyleBackColor = true;
            // 
            // radio4x
            // 
            this.radio4x.AutoSize = true;
            this.radio4x.Location = new System.Drawing.Point(154, 22);
            this.radio4x.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.radio4x.Name = "radio4x";
            this.radio4x.Size = new System.Drawing.Size(37, 19);
            this.radio4x.TabIndex = 7;
            this.radio4x.Text = "4x";
            this.radio4x.UseVisualStyleBackColor = true;
            // 
            // radio2x
            // 
            this.radio2x.AutoSize = true;
            this.radio2x.Location = new System.Drawing.Point(56, 22);
            this.radio2x.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.radio2x.Name = "radio2x";
            this.radio2x.Size = new System.Drawing.Size(37, 19);
            this.radio2x.TabIndex = 5;
            this.radio2x.Text = "2x";
            this.radio2x.UseVisualStyleBackColor = true;
            // 
            // radio1x
            // 
            this.radio1x.AutoSize = true;
            this.radio1x.Checked = true;
            this.radio1x.Location = new System.Drawing.Point(7, 22);
            this.radio1x.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.radio1x.Name = "radio1x";
            this.radio1x.Size = new System.Drawing.Size(37, 19);
            this.radio1x.TabIndex = 0;
            this.radio1x.TabStop = true;
            this.radio1x.Text = "1x";
            this.radio1x.UseVisualStyleBackColor = true;
            // 
            // chkEnhanceTreble
            // 
            this.chkEnhanceTreble.AutoSize = true;
            this.chkEnhanceTreble.Checked = true;
            this.chkEnhanceTreble.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkEnhanceTreble.Location = new System.Drawing.Point(10, 22);
            this.chkEnhanceTreble.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.chkEnhanceTreble.Name = "chkEnhanceTreble";
            this.chkEnhanceTreble.Size = new System.Drawing.Size(104, 19);
            this.chkEnhanceTreble.TabIndex = 1;
            this.chkEnhanceTreble.Text = "Enhance treble";
            this.chkEnhanceTreble.UseVisualStyleBackColor = true;
            // 
            // btnGenerate
            // 
            this.btnGenerate.Location = new System.Drawing.Point(325, 87);
            this.btnGenerate.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnGenerate.Name = "btnGenerate";
            this.btnGenerate.Size = new System.Drawing.Size(198, 36);
            this.btnGenerate.TabIndex = 0;
            this.btnGenerate.Text = "Generate N-SPC Data";
            this.btnGenerate.UseVisualStyleBackColor = true;
            this.btnGenerate.Click += new System.EventHandler(this.btnGenerate_Click);
            // 
            // Advanced
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(859, 684);
            this.Controls.Add(this.groupBox7);
            this.Controls.Add(this.groupBox6);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "Advanced";
            this.Text = "Advanced";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            this.groupBox8.ResumeLayout(false);
            this.groupBox8.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtSongIndex;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtEchoBufferLen;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtEchoBuffer;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtSamples;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtPatternData;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtInstrumentHeaders;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtSampleHeaders;
        private System.Windows.Forms.ComboBox cboGamePreset;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ListBox lstSPCData;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.CheckBox chkDisableKeyOffPatterns;
        private System.Windows.Forms.CheckBox chkDisableKeyOffNotes;
        private System.Windows.Forms.CheckBox chkRepointEchoBuffer;
        private System.Windows.Forms.CheckBox chkRepointMusicData;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Button btnRemove;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.ListBox lstInputFiles;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.CheckBox chkExportMusicData;
        private System.Windows.Forms.CheckBox chkExportSamples;
        private System.Windows.Forms.CheckBox chkExportInstruments;
        private System.Windows.Forms.CheckBox chkExportSampleHeaders;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.Button btnGenerate;
        private System.Windows.Forms.Button btnWriteNSPC;
        private System.Windows.Forms.Button btnCreateSPC;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtSPCTemplate;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtInstrumentIndexOffset;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtSampleIndexOffset;
        private System.Windows.Forms.CheckBox chkEnhanceTreble;
        private System.Windows.Forms.GroupBox groupBox8;
        private System.Windows.Forms.RadioButton radio3x;
        private System.Windows.Forms.RadioButton radio4x;
        private System.Windows.Forms.RadioButton radio2x;
        private System.Windows.Forms.RadioButton radio1x;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtResample;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtMusicIndex;
        private System.Windows.Forms.CheckBox chkExportPatches;
        private System.Windows.Forms.CheckBox chkExportMusicIndex;
        private System.Windows.Forms.CheckBox chkOptimizeTracks;
        private System.Windows.Forms.CheckBox chkSetupPattern;
        private System.Windows.Forms.RadioButton radio05x;
        private System.Windows.Forms.CheckBox chkOptimizePatterns;
        private System.Windows.Forms.Button btnLoadInfoFromRom;
    }
}