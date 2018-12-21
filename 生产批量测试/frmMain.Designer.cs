namespace 生产批量测试
{
    partial class frmMain
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.gpComm = new System.Windows.Forms.GroupBox();
            this.lbBaudrate = new System.Windows.Forms.Label();
            this.combPort = new System.Windows.Forms.ComboBox();
            this.lbPort = new System.Windows.Forms.Label();
            this.btnPort = new System.Windows.Forms.Button();
            this.gpDocument = new System.Windows.Forms.GroupBox();
            this.btSaveDoc = new System.Windows.Forms.Button();
            this.btLoadDoc = new System.Windows.Forms.Button();
            this.btClearDoc = new System.Windows.Forms.Button();
            this.btAddDoc = new System.Windows.Forms.Button();
            this.gpRun = new System.Windows.Forms.GroupBox();
            this.combFreq = new System.Windows.Forms.ComboBox();
            this.lbFreq = new System.Windows.Forms.Label();
            this.combProtoVer = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.combChanelGrp = new System.Windows.Forms.ComboBox();
            this.lbChannel = new System.Windows.Forms.Label();
            this.combCurrDis = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtRssiThreshold = new System.Windows.Forms.TextBox();
            this.lbRetryTime = new System.Windows.Forms.Label();
            this.combRetryTime = new System.Windows.Forms.ComboBox();
            this.btSaveResult = new System.Windows.Forms.Button();
            this.txtVersion = new System.Windows.Forms.TextBox();
            this.combPower = new System.Windows.Forms.ComboBox();
            this.chkRssi = new System.Windows.Forms.CheckBox();
            this.chkSaveLog = new System.Windows.Forms.CheckBox();
            this.chkLoopTest = new System.Windows.Forms.CheckBox();
            this.chkCurrent = new System.Windows.Forms.CheckBox();
            this.chkReadPwr = new System.Windows.Forms.CheckBox();
            this.combProcotol = new System.Windows.Forms.ComboBox();
            this.chkReadAmmter = new System.Windows.Forms.CheckBox();
            this.chkReadVer = new System.Windows.Forms.CheckBox();
            this.btTestStop = new System.Windows.Forms.Button();
            this.btTestCtrl = new System.Windows.Forms.Button();
            this.lvwDocument = new System.Windows.Forms.ListView();
            this.colNo = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colCollectAdr = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colAmmeterAdr = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colVersion = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colAmmeterVal = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colReadPower = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colTx = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colRx = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colRssi = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colResult = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.cntMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.menuItemAddDoc = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemDelDoc = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemEditDoc = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemClearDoc = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.menuItemLoadDoc = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemSaveDoc = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.menuItemSaveResult = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemRunCmd = new System.Windows.Forms.ToolStripMenuItem();
            this.menuSubItemReadVer = new System.Windows.Forms.ToolStripMenuItem();
            this.menuSubItemReadAmmeter = new System.Windows.Forms.ToolStripMenuItem();
            this.menuSubItemReadPower = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemCopy = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusTest = new System.Windows.Forms.StatusStrip();
            this.toolStripProgressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.toolStripMacAddress = new System.Windows.Forms.ToolStripStatusLabel();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.lbCurrChk = new System.Windows.Forms.Label();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.menuItemTestStart = new System.Windows.Forms.ToolStripMenuItem();
            this.gpComm.SuspendLayout();
            this.gpDocument.SuspendLayout();
            this.gpRun.SuspendLayout();
            this.cntMenuStrip.SuspendLayout();
            this.statusTest.SuspendLayout();
            this.SuspendLayout();
            // 
            // gpComm
            // 
            this.gpComm.Controls.Add(this.lbBaudrate);
            this.gpComm.Controls.Add(this.combPort);
            this.gpComm.Controls.Add(this.lbPort);
            this.gpComm.Controls.Add(this.btnPort);
            this.gpComm.Font = new System.Drawing.Font("微软雅黑", 9.75F);
            this.gpComm.Location = new System.Drawing.Point(12, 3);
            this.gpComm.Name = "gpComm";
            this.gpComm.Size = new System.Drawing.Size(231, 72);
            this.gpComm.TabIndex = 10;
            this.gpComm.TabStop = false;
            this.gpComm.Text = "端口设置";
            // 
            // lbBaudrate
            // 
            this.lbBaudrate.AutoSize = true;
            this.lbBaudrate.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbBaudrate.Location = new System.Drawing.Point(21, 49);
            this.lbBaudrate.Name = "lbBaudrate";
            this.lbBaudrate.Size = new System.Drawing.Size(56, 17);
            this.lbBaudrate.TabIndex = 3;
            this.lbBaudrate.Text = "波特率：";
            // 
            // combPort
            // 
            this.combPort.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.combPort.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.combPort.FormattingEnabled = true;
            this.combPort.Location = new System.Drawing.Point(73, 17);
            this.combPort.Name = "combPort";
            this.combPort.Size = new System.Drawing.Size(92, 25);
            this.combPort.TabIndex = 1;
            this.combPort.Click += new System.EventHandler(this.cmboPort_Click);
            // 
            // lbPort
            // 
            this.lbPort.AutoSize = true;
            this.lbPort.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbPort.Location = new System.Drawing.Point(21, 21);
            this.lbPort.Name = "lbPort";
            this.lbPort.Size = new System.Drawing.Size(56, 17);
            this.lbPort.TabIndex = 0;
            this.lbPort.Text = "端   口：";
            this.lbPort.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnPort
            // 
            this.btnPort.BackColor = System.Drawing.SystemColors.Control;
            this.btnPort.ForeColor = System.Drawing.SystemColors.WindowText;
            this.btnPort.Location = new System.Drawing.Point(171, 17);
            this.btnPort.Name = "btnPort";
            this.btnPort.Size = new System.Drawing.Size(54, 49);
            this.btnPort.TabIndex = 2;
            this.btnPort.Text = "打开\r\n串口";
            this.btnPort.UseVisualStyleBackColor = false;
            this.btnPort.Click += new System.EventHandler(this.btnPort_Click);
            // 
            // gpDocument
            // 
            this.gpDocument.Controls.Add(this.btSaveDoc);
            this.gpDocument.Controls.Add(this.btLoadDoc);
            this.gpDocument.Controls.Add(this.btClearDoc);
            this.gpDocument.Controls.Add(this.btAddDoc);
            this.gpDocument.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.gpDocument.Location = new System.Drawing.Point(12, 81);
            this.gpDocument.Name = "gpDocument";
            this.gpDocument.Size = new System.Drawing.Size(231, 93);
            this.gpDocument.TabIndex = 11;
            this.gpDocument.TabStop = false;
            this.gpDocument.Text = "档案管理";
            // 
            // btSaveDoc
            // 
            this.btSaveDoc.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btSaveDoc.Location = new System.Drawing.Point(135, 54);
            this.btSaveDoc.Name = "btSaveDoc";
            this.btSaveDoc.Size = new System.Drawing.Size(85, 26);
            this.btSaveDoc.TabIndex = 6;
            this.btSaveDoc.Text = "保存档案";
            this.btSaveDoc.UseVisualStyleBackColor = true;
            this.btSaveDoc.Click += new System.EventHandler(this.btSaveDoc_Click);
            // 
            // btLoadDoc
            // 
            this.btLoadDoc.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btLoadDoc.Location = new System.Drawing.Point(135, 22);
            this.btLoadDoc.Name = "btLoadDoc";
            this.btLoadDoc.Size = new System.Drawing.Size(85, 26);
            this.btLoadDoc.TabIndex = 4;
            this.btLoadDoc.Text = "导入档案";
            this.btLoadDoc.UseVisualStyleBackColor = true;
            this.btLoadDoc.Click += new System.EventHandler(this.btLoadDoc_Click);
            // 
            // btClearDoc
            // 
            this.btClearDoc.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btClearDoc.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btClearDoc.Location = new System.Drawing.Point(24, 54);
            this.btClearDoc.Name = "btClearDoc";
            this.btClearDoc.Size = new System.Drawing.Size(85, 26);
            this.btClearDoc.TabIndex = 5;
            this.btClearDoc.Text = "清空档案";
            this.btClearDoc.UseVisualStyleBackColor = true;
            this.btClearDoc.Click += new System.EventHandler(this.btClearDoc_Click);
            // 
            // btAddDoc
            // 
            this.btAddDoc.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btAddDoc.Location = new System.Drawing.Point(24, 22);
            this.btAddDoc.Name = "btAddDoc";
            this.btAddDoc.Size = new System.Drawing.Size(85, 26);
            this.btAddDoc.TabIndex = 3;
            this.btAddDoc.Text = "添加档案";
            this.btAddDoc.UseVisualStyleBackColor = true;
            this.btAddDoc.Click += new System.EventHandler(this.btAddDoc_Click);
            // 
            // gpRun
            // 
            this.gpRun.Controls.Add(this.lbCurrChk);
            this.gpRun.Controls.Add(this.combFreq);
            this.gpRun.Controls.Add(this.lbFreq);
            this.gpRun.Controls.Add(this.combProtoVer);
            this.gpRun.Controls.Add(this.label1);
            this.gpRun.Controls.Add(this.combChanelGrp);
            this.gpRun.Controls.Add(this.lbChannel);
            this.gpRun.Controls.Add(this.combCurrDis);
            this.gpRun.Controls.Add(this.label2);
            this.gpRun.Controls.Add(this.txtRssiThreshold);
            this.gpRun.Controls.Add(this.lbRetryTime);
            this.gpRun.Controls.Add(this.combRetryTime);
            this.gpRun.Controls.Add(this.btSaveResult);
            this.gpRun.Controls.Add(this.txtVersion);
            this.gpRun.Controls.Add(this.combPower);
            this.gpRun.Controls.Add(this.chkRssi);
            this.gpRun.Controls.Add(this.chkSaveLog);
            this.gpRun.Controls.Add(this.chkLoopTest);
            this.gpRun.Controls.Add(this.chkCurrent);
            this.gpRun.Controls.Add(this.chkReadPwr);
            this.gpRun.Controls.Add(this.combProcotol);
            this.gpRun.Controls.Add(this.chkReadAmmter);
            this.gpRun.Controls.Add(this.chkReadVer);
            this.gpRun.Controls.Add(this.btTestStop);
            this.gpRun.Controls.Add(this.btTestCtrl);
            this.gpRun.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.gpRun.Location = new System.Drawing.Point(12, 180);
            this.gpRun.Name = "gpRun";
            this.gpRun.Size = new System.Drawing.Size(231, 479);
            this.gpRun.TabIndex = 12;
            this.gpRun.TabStop = false;
            this.gpRun.Text = "运行命令";
            // 
            // combFreq
            // 
            this.combFreq.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.combFreq.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.combFreq.FormattingEnabled = true;
            this.combFreq.Location = new System.Drawing.Point(146, 59);
            this.combFreq.Name = "combFreq";
            this.combFreq.Size = new System.Drawing.Size(75, 25);
            this.combFreq.TabIndex = 28;
            this.combFreq.SelectedIndexChanged += new System.EventHandler(this.combFreq_SelectedIndexChanged);
            // 
            // lbFreq
            // 
            this.lbFreq.AutoSize = true;
            this.lbFreq.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbFreq.Location = new System.Drawing.Point(113, 62);
            this.lbFreq.Name = "lbFreq";
            this.lbFreq.Size = new System.Drawing.Size(32, 17);
            this.lbFreq.TabIndex = 27;
            this.lbFreq.Text = "频点";
            // 
            // combProtoVer
            // 
            this.combProtoVer.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.combProtoVer.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.combProtoVer.FormattingEnabled = true;
            this.combProtoVer.Items.AddRange(new object[] {
            "北网版本",
            "尼泊尔版本",
            "瑞典版本"});
            this.combProtoVer.Location = new System.Drawing.Point(83, 28);
            this.combProtoVer.Name = "combProtoVer";
            this.combProtoVer.Size = new System.Drawing.Size(138, 25);
            this.combProtoVer.TabIndex = 28;
            this.combProtoVer.SelectedIndexChanged += new System.EventHandler(this.combProtoVer_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(21, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 17);
            this.label1.TabIndex = 27;
            this.label1.Text = "协议版本";
            // 
            // combChanelGrp
            // 
            this.combChanelGrp.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.combChanelGrp.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.combChanelGrp.FormattingEnabled = true;
            this.combChanelGrp.Location = new System.Drawing.Point(66, 59);
            this.combChanelGrp.Name = "combChanelGrp";
            this.combChanelGrp.Size = new System.Drawing.Size(43, 25);
            this.combChanelGrp.TabIndex = 28;
            this.combChanelGrp.SelectedIndexChanged += new System.EventHandler(this.combChannelGrp_SelectedIndexChanged);
            // 
            // lbChannel
            // 
            this.lbChannel.AutoSize = true;
            this.lbChannel.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbChannel.Location = new System.Drawing.Point(22, 62);
            this.lbChannel.Name = "lbChannel";
            this.lbChannel.Size = new System.Drawing.Size(44, 17);
            this.lbChannel.TabIndex = 27;
            this.lbChannel.Text = "信道组";
            // 
            // combCurrDis
            // 
            this.combCurrDis.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.combCurrDis.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.combCurrDis.FormattingEnabled = true;
            this.combCurrDis.Items.AddRange(new object[] {
            "4E88N",
            "4E88M",
            "4E18D",
            "4E18C"});
            this.combCurrDis.Location = new System.Drawing.Point(98, 267);
            this.combCurrDis.Name = "combCurrDis";
            this.combCurrDis.Size = new System.Drawing.Size(63, 25);
            this.combCurrDis.TabIndex = 26;
            this.combCurrDis.SelectedIndexChanged += new System.EventHandler(this.combCurrDis_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(185, 239);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 17);
            this.label2.TabIndex = 22;
            this.label2.Text = "dBm";
            // 
            // txtRssiThreshold
            // 
            this.txtRssiThreshold.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtRssiThreshold.Location = new System.Drawing.Point(127, 236);
            this.txtRssiThreshold.Name = "txtRssiThreshold";
            this.txtRssiThreshold.Size = new System.Drawing.Size(52, 23);
            this.txtRssiThreshold.TabIndex = 21;
            this.txtRssiThreshold.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtRssiThreshold.Leave += new System.EventHandler(this.textRssiThreshold_Leave);
            // 
            // lbRetryTime
            // 
            this.lbRetryTime.AutoSize = true;
            this.lbRetryTime.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbRetryTime.Location = new System.Drawing.Point(101, 305);
            this.lbRetryTime.Name = "lbRetryTime";
            this.lbRetryTime.Size = new System.Drawing.Size(56, 17);
            this.lbRetryTime.TabIndex = 19;
            this.lbRetryTime.Text = "重试次数";
            // 
            // combRetryTime
            // 
            this.combRetryTime.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.combRetryTime.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.combRetryTime.FormattingEnabled = true;
            this.combRetryTime.Items.AddRange(new object[] {
            "1",
            "2",
            "3"});
            this.combRetryTime.Location = new System.Drawing.Point(158, 302);
            this.combRetryTime.Name = "combRetryTime";
            this.combRetryTime.Size = new System.Drawing.Size(63, 25);
            this.combRetryTime.TabIndex = 18;
            this.combRetryTime.SelectedIndexChanged += new System.EventHandler(this.combRetryTime_SelectedIndexChanged);
            // 
            // btSaveResult
            // 
            this.btSaveResult.Enabled = false;
            this.btSaveResult.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btSaveResult.Location = new System.Drawing.Point(25, 304);
            this.btSaveResult.Name = "btSaveResult";
            this.btSaveResult.Size = new System.Drawing.Size(68, 25);
            this.btSaveResult.TabIndex = 17;
            this.btSaveResult.Text = "保存结果";
            this.btSaveResult.UseVisualStyleBackColor = true;
            this.btSaveResult.Click += new System.EventHandler(this.btSaveResult_Click);
            // 
            // txtVersion
            // 
            this.txtVersion.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtVersion.Location = new System.Drawing.Point(24, 126);
            this.txtVersion.Multiline = true;
            this.txtVersion.Name = "txtVersion";
            this.txtVersion.Size = new System.Drawing.Size(197, 32);
            this.txtVersion.TabIndex = 8;
            this.txtVersion.Leave += new System.EventHandler(this.txtVersion_Leave);
            // 
            // combPower
            // 
            this.combPower.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.combPower.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.combPower.FormattingEnabled = true;
            this.combPower.Items.AddRange(new object[] {
            "20dBm",
            "17dBm",
            "11dBm",
            "5dBm",
            "2dBm"});
            this.combPower.Location = new System.Drawing.Point(107, 201);
            this.combPower.Name = "combPower";
            this.combPower.Size = new System.Drawing.Size(113, 25);
            this.combPower.TabIndex = 12;
            this.combPower.SelectedIndexChanged += new System.EventHandler(this.combPower_SelectedIndexChanged);
            // 
            // chkRssi
            // 
            this.chkRssi.AutoSize = true;
            this.chkRssi.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.chkRssi.Location = new System.Drawing.Point(25, 238);
            this.chkRssi.Name = "chkRssi";
            this.chkRssi.Size = new System.Drawing.Size(99, 21);
            this.chkRssi.TabIndex = 11;
            this.chkRssi.Text = "场强阀值判定";
            this.chkRssi.UseVisualStyleBackColor = true;
            this.chkRssi.CheckedChanged += new System.EventHandler(this.chkRssi_CheckedChanged);
            // 
            // chkSaveLog
            // 
            this.chkSaveLog.AutoSize = true;
            this.chkSaveLog.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.chkSaveLog.Location = new System.Drawing.Point(127, 335);
            this.chkSaveLog.Name = "chkSaveLog";
            this.chkSaveLog.Size = new System.Drawing.Size(75, 21);
            this.chkSaveLog.TabIndex = 11;
            this.chkSaveLog.Text = "保存日志";
            this.chkSaveLog.UseVisualStyleBackColor = true;
            this.chkSaveLog.CheckedChanged += new System.EventHandler(this.chkCurrent_CheckedChanged);
            // 
            // chkLoopTest
            // 
            this.chkLoopTest.AutoSize = true;
            this.chkLoopTest.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.chkLoopTest.Location = new System.Drawing.Point(25, 335);
            this.chkLoopTest.Name = "chkLoopTest";
            this.chkLoopTest.Size = new System.Drawing.Size(75, 21);
            this.chkLoopTest.TabIndex = 11;
            this.chkLoopTest.Text = "循环测试";
            this.chkLoopTest.UseVisualStyleBackColor = true;
            this.chkLoopTest.CheckedChanged += new System.EventHandler(this.chkCurrent_CheckedChanged);
            // 
            // chkCurrent
            // 
            this.chkCurrent.AutoSize = true;
            this.chkCurrent.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.chkCurrent.Location = new System.Drawing.Point(24, 271);
            this.chkCurrent.Name = "chkCurrent";
            this.chkCurrent.Size = new System.Drawing.Size(75, 21);
            this.chkCurrent.TabIndex = 11;
            this.chkCurrent.Text = "电流判定";
            this.chkCurrent.UseVisualStyleBackColor = true;
            this.chkCurrent.CheckedChanged += new System.EventHandler(this.chkCurrent_CheckedChanged);
            // 
            // chkReadPwr
            // 
            this.chkReadPwr.AutoSize = true;
            this.chkReadPwr.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.chkReadPwr.Location = new System.Drawing.Point(24, 203);
            this.chkReadPwr.Name = "chkReadPwr";
            this.chkReadPwr.Size = new System.Drawing.Size(75, 21);
            this.chkReadPwr.TabIndex = 11;
            this.chkReadPwr.Text = "读取功率";
            this.chkReadPwr.UseVisualStyleBackColor = true;
            this.chkReadPwr.CheckedChanged += new System.EventHandler(this.chkReadPwr_CheckedChanged);
            // 
            // combProcotol
            // 
            this.combProcotol.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.combProcotol.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.combProcotol.FormattingEnabled = true;
            this.combProcotol.Items.AddRange(new object[] {
            "DLT645-07 总电能",
            "DLT645-97 总电能",
            "自定义 34 34 D3 37"});
            this.combProcotol.Location = new System.Drawing.Point(83, 164);
            this.combProcotol.Name = "combProcotol";
            this.combProcotol.Size = new System.Drawing.Size(138, 25);
            this.combProcotol.TabIndex = 10;
            this.combProcotol.SelectedIndexChanged += new System.EventHandler(this.combProcotol_SelectedIndexChanged);
            // 
            // chkReadAmmter
            // 
            this.chkReadAmmter.AutoSize = true;
            this.chkReadAmmter.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.chkReadAmmter.Location = new System.Drawing.Point(24, 166);
            this.chkReadAmmter.Name = "chkReadAmmter";
            this.chkReadAmmter.Size = new System.Drawing.Size(51, 21);
            this.chkReadAmmter.TabIndex = 9;
            this.chkReadAmmter.Text = "抄表";
            this.chkReadAmmter.UseVisualStyleBackColor = true;
            this.chkReadAmmter.CheckedChanged += new System.EventHandler(this.chkReadAmmter_CheckedChanged);
            // 
            // chkReadVer
            // 
            this.chkReadVer.AutoSize = true;
            this.chkReadVer.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.chkReadVer.Location = new System.Drawing.Point(24, 99);
            this.chkReadVer.Name = "chkReadVer";
            this.chkReadVer.Size = new System.Drawing.Size(75, 21);
            this.chkReadVer.TabIndex = 7;
            this.chkReadVer.Text = "读取版本";
            this.chkReadVer.UseVisualStyleBackColor = true;
            this.chkReadVer.CheckedChanged += new System.EventHandler(this.chkReadVer_CheckedChanged);
            // 
            // btTestStop
            // 
            this.btTestStop.BackColor = System.Drawing.Color.Gray;
            this.btTestStop.Font = new System.Drawing.Font("宋体", 15F);
            this.btTestStop.Location = new System.Drawing.Point(23, 416);
            this.btTestStop.Name = "btTestStop";
            this.btTestStop.Size = new System.Drawing.Size(197, 38);
            this.btTestStop.TabIndex = 16;
            this.btTestStop.Text = "停止测试";
            this.btTestStop.UseVisualStyleBackColor = false;
            this.btTestStop.Click += new System.EventHandler(this.btTestStop_Click);
            // 
            // btTestCtrl
            // 
            this.btTestCtrl.BackColor = System.Drawing.Color.GreenYellow;
            this.btTestCtrl.Font = new System.Drawing.Font("宋体", 15F);
            this.btTestCtrl.Location = new System.Drawing.Point(23, 370);
            this.btTestCtrl.Name = "btTestCtrl";
            this.btTestCtrl.Size = new System.Drawing.Size(197, 40);
            this.btTestCtrl.TabIndex = 15;
            this.btTestCtrl.Text = "开始测试";
            this.btTestCtrl.UseVisualStyleBackColor = false;
            this.btTestCtrl.Click += new System.EventHandler(this.btTestCtrl_Click);
            // 
            // lvwDocument
            // 
            this.lvwDocument.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvwDocument.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colNo,
            this.colCollectAdr,
            this.colAmmeterAdr,
            this.colVersion,
            this.colAmmeterVal,
            this.colReadPower,
            this.colTx,
            this.colRx,
            this.colRssi,
            this.colResult});
            this.lvwDocument.ContextMenuStrip = this.cntMenuStrip;
            this.lvwDocument.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lvwDocument.FullRowSelect = true;
            this.lvwDocument.GridLines = true;
            this.lvwDocument.Location = new System.Drawing.Point(249, 3);
            this.lvwDocument.Name = "lvwDocument";
            this.lvwDocument.Size = new System.Drawing.Size(922, 656);
            this.lvwDocument.TabIndex = 8;
            this.lvwDocument.UseCompatibleStateImageBehavior = false;
            this.lvwDocument.View = System.Windows.Forms.View.Details;
            this.lvwDocument.DoubleClick += new System.EventHandler(this.lvwDocument_DoubleClick);
            this.lvwDocument.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lvwDocument_KeyDown);
            this.lvwDocument.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lvwDocument_MouseDown);
            // 
            // colNo
            // 
            this.colNo.Text = "序号";
            this.colNo.Width = 40;
            // 
            // colCollectAdr
            // 
            this.colCollectAdr.Text = "采集器地址";
            this.colCollectAdr.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.colCollectAdr.Width = 100;
            // 
            // colAmmeterAdr
            // 
            this.colAmmeterAdr.Text = "电能表地址";
            this.colAmmeterAdr.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.colAmmeterAdr.Width = 100;
            // 
            // colVersion
            // 
            this.colVersion.Text = "版本描述";
            this.colVersion.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.colVersion.Width = 270;
            // 
            // colAmmeterVal
            // 
            this.colAmmeterVal.Text = "电表读数 静(5/12)动(5/12)";
            this.colAmmeterVal.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.colAmmeterVal.Width = 160;
            // 
            // colReadPower
            // 
            this.colReadPower.Text = "读取功率";
            this.colReadPower.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.colReadPower.Width = 65;
            // 
            // colTx
            // 
            this.colTx.Text = "发送";
            this.colTx.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.colTx.Width = 40;
            // 
            // colRx
            // 
            this.colRx.Text = "接收";
            this.colRx.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.colRx.Width = 40;
            // 
            // colRssi
            // 
            this.colRssi.Text = "信号强度";
            this.colRssi.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.colRssi.Width = 65;
            // 
            // colResult
            // 
            this.colResult.Text = "结果";
            this.colResult.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.colResult.Width = 45;
            // 
            // cntMenuStrip
            // 
            this.cntMenuStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.cntMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemAddDoc,
            this.menuItemDelDoc,
            this.menuItemEditDoc,
            this.menuItemClearDoc,
            this.toolStripMenuItem1,
            this.menuItemLoadDoc,
            this.menuItemSaveDoc,
            this.toolStripMenuItem2,
            this.menuItemSaveResult,
            this.toolStripSeparator1,
            this.menuItemTestStart,
            this.menuItemRunCmd,
            this.toolStripSeparator2,
            this.menuItemCopy});
            this.cntMenuStrip.Name = "cntMenuStrip";
            this.cntMenuStrip.Size = new System.Drawing.Size(149, 248);
            this.cntMenuStrip.Opened += new System.EventHandler(this.cntMenuStrip_Opened);
            // 
            // menuItemAddDoc
            // 
            this.menuItemAddDoc.Name = "menuItemAddDoc";
            this.menuItemAddDoc.Size = new System.Drawing.Size(148, 22);
            this.menuItemAddDoc.Text = "添加档案";
            this.menuItemAddDoc.Click += new System.EventHandler(this.menuItemAddDoc_Click);
            // 
            // menuItemDelDoc
            // 
            this.menuItemDelDoc.Name = "menuItemDelDoc";
            this.menuItemDelDoc.Size = new System.Drawing.Size(148, 22);
            this.menuItemDelDoc.Text = "删除档案";
            this.menuItemDelDoc.Click += new System.EventHandler(this.menuItemDelDoc_Click);
            // 
            // menuItemEditDoc
            // 
            this.menuItemEditDoc.Name = "menuItemEditDoc";
            this.menuItemEditDoc.Size = new System.Drawing.Size(148, 22);
            this.menuItemEditDoc.Text = "编辑档案";
            this.menuItemEditDoc.Click += new System.EventHandler(this.menuItemEditDoc_Click);
            // 
            // menuItemClearDoc
            // 
            this.menuItemClearDoc.Name = "menuItemClearDoc";
            this.menuItemClearDoc.Size = new System.Drawing.Size(148, 22);
            this.menuItemClearDoc.Text = "清空档案";
            this.menuItemClearDoc.Click += new System.EventHandler(this.menuItemClearDoc_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(145, 6);
            // 
            // menuItemLoadDoc
            // 
            this.menuItemLoadDoc.Name = "menuItemLoadDoc";
            this.menuItemLoadDoc.Size = new System.Drawing.Size(148, 22);
            this.menuItemLoadDoc.Text = "导入档案";
            this.menuItemLoadDoc.Click += new System.EventHandler(this.menuItemLoadDoc_Click);
            // 
            // menuItemSaveDoc
            // 
            this.menuItemSaveDoc.Name = "menuItemSaveDoc";
            this.menuItemSaveDoc.Size = new System.Drawing.Size(148, 22);
            this.menuItemSaveDoc.Text = "保存档案";
            this.menuItemSaveDoc.Click += new System.EventHandler(this.menuItemSaveDoc_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(145, 6);
            // 
            // menuItemSaveResult
            // 
            this.menuItemSaveResult.Name = "menuItemSaveResult";
            this.menuItemSaveResult.Size = new System.Drawing.Size(148, 22);
            this.menuItemSaveResult.Text = "保存结果";
            this.menuItemSaveResult.Click += new System.EventHandler(this.menuItemSaveResult_Click);
            // 
            // menuItemRunCmd
            // 
            this.menuItemRunCmd.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuSubItemReadVer,
            this.menuSubItemReadAmmeter,
            this.menuSubItemReadPower});
            this.menuItemRunCmd.Name = "menuItemRunCmd";
            this.menuItemRunCmd.Size = new System.Drawing.Size(148, 22);
            this.menuItemRunCmd.Text = "运行单条命令";
            this.menuItemRunCmd.DropDownOpened += new System.EventHandler(this.menuItemRunCmd_DropDownOpened);
            // 
            // menuSubItemReadVer
            // 
            this.menuSubItemReadVer.Name = "menuSubItemReadVer";
            this.menuSubItemReadVer.Size = new System.Drawing.Size(152, 22);
            this.menuSubItemReadVer.Text = "读取版本";
            this.menuSubItemReadVer.Click += new System.EventHandler(this.menuSubItemReadVer_Click);
            // 
            // menuSubItemReadAmmeter
            // 
            this.menuSubItemReadAmmeter.Name = "menuSubItemReadAmmeter";
            this.menuSubItemReadAmmeter.Size = new System.Drawing.Size(152, 22);
            this.menuSubItemReadAmmeter.Text = "抄表";
            this.menuSubItemReadAmmeter.Click += new System.EventHandler(this.menuSubItemReadAmmeter_Click);
            // 
            // menuSubItemReadPower
            // 
            this.menuSubItemReadPower.Name = "menuSubItemReadPower";
            this.menuSubItemReadPower.Size = new System.Drawing.Size(152, 22);
            this.menuSubItemReadPower.Text = "读取功率";
            this.menuSubItemReadPower.Click += new System.EventHandler(this.menuSubItemReadPower_Click);
            // 
            // menuItemCopy
            // 
            this.menuItemCopy.Name = "menuItemCopy";
            this.menuItemCopy.Size = new System.Drawing.Size(148, 22);
            this.menuItemCopy.Text = "复制";
            this.menuItemCopy.Click += new System.EventHandler(this.menuItemCopy_Click);
            // 
            // toolStripStatus
            // 
            this.toolStripStatus.ActiveLinkColor = System.Drawing.SystemColors.WindowText;
            this.toolStripStatus.AutoSize = false;
            this.toolStripStatus.Name = "toolStripStatus";
            this.toolStripStatus.Size = new System.Drawing.Size(500, 21);
            this.toolStripStatus.Text = "状态";
            this.toolStripStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // statusTest
            // 
            this.statusTest.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusTest.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatus,
            this.toolStripProgressBar,
            this.toolStripMacAddress});
            this.statusTest.Location = new System.Drawing.Point(0, 658);
            this.statusTest.Name = "statusTest";
            this.statusTest.Size = new System.Drawing.Size(1171, 26);
            this.statusTest.TabIndex = 9;
            // 
            // toolStripProgressBar
            // 
            this.toolStripProgressBar.AutoSize = false;
            this.toolStripProgressBar.Name = "toolStripProgressBar";
            this.toolStripProgressBar.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.toolStripProgressBar.Size = new System.Drawing.Size(300, 20);
            this.toolStripProgressBar.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            // 
            // toolStripMacAddress
            // 
            this.toolStripMacAddress.AutoSize = false;
            this.toolStripMacAddress.ForeColor = System.Drawing.Color.Navy;
            this.toolStripMacAddress.Name = "toolStripMacAddress";
            this.toolStripMacAddress.Size = new System.Drawing.Size(200, 21);
            this.toolStripMacAddress.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileName = "openFileDialog";
            // 
            // lbCurrChk
            // 
            this.lbCurrChk.AutoSize = true;
            this.lbCurrChk.Font = new System.Drawing.Font("微软雅黑", 7.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbCurrChk.Location = new System.Drawing.Point(163, 262);
            this.lbCurrChk.Name = "lbCurrChk";
            this.lbCurrChk.Size = new System.Drawing.Size(63, 32);
            this.lbCurrChk.TabIndex = 29;
            this.lbCurrChk.Text = "  00100014\r\n~00250046";
            this.lbCurrChk.Visible = false;
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(145, 6);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(145, 6);
            // 
            // menuItemTestStart
            // 
            this.menuItemTestStart.Name = "menuItemTestStart";
            this.menuItemTestStart.Size = new System.Drawing.Size(148, 22);
            this.menuItemTestStart.Text = "开始测试";
            this.menuItemTestStart.Click += new System.EventHandler(this.menuItemTestStart_Click);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1171, 684);
            this.Controls.Add(this.gpComm);
            this.Controls.Add(this.gpDocument);
            this.Controls.Add(this.gpRun);
            this.Controls.Add(this.lvwDocument);
            this.Controls.Add(this.statusTest);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmMain_FormClosed);
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmMain_KeyDown);
            this.gpComm.ResumeLayout(false);
            this.gpComm.PerformLayout();
            this.gpDocument.ResumeLayout(false);
            this.gpRun.ResumeLayout(false);
            this.gpRun.PerformLayout();
            this.cntMenuStrip.ResumeLayout(false);
            this.statusTest.ResumeLayout(false);
            this.statusTest.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox gpComm;
        private System.Windows.Forms.Label lbBaudrate;
        private System.Windows.Forms.ComboBox combPort;
        private System.Windows.Forms.Label lbPort;
        private System.Windows.Forms.Button btnPort;
        private System.Windows.Forms.GroupBox gpDocument;
        private System.Windows.Forms.Button btSaveDoc;
        private System.Windows.Forms.Button btLoadDoc;
        private System.Windows.Forms.Button btClearDoc;
        private System.Windows.Forms.Button btAddDoc;
        private System.Windows.Forms.GroupBox gpRun;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtRssiThreshold;
        private System.Windows.Forms.Label lbRetryTime;
        private System.Windows.Forms.ComboBox combRetryTime;
        private System.Windows.Forms.Button btSaveResult;
        private System.Windows.Forms.TextBox txtVersion;
        private System.Windows.Forms.ComboBox combPower;
        private System.Windows.Forms.CheckBox chkReadPwr;
        private System.Windows.Forms.ComboBox combProcotol;
        private System.Windows.Forms.CheckBox chkReadAmmter;
        private System.Windows.Forms.CheckBox chkReadVer;
        private System.Windows.Forms.Button btTestStop;
        private System.Windows.Forms.Button btTestCtrl;
        private System.Windows.Forms.ListView lvwDocument;
        private System.Windows.Forms.ColumnHeader colCollectAdr;
        private System.Windows.Forms.ColumnHeader colAmmeterAdr;
        private System.Windows.Forms.ColumnHeader colVersion;
        private System.Windows.Forms.ColumnHeader colAmmeterVal;
        private System.Windows.Forms.ColumnHeader colReadPower;
        private System.Windows.Forms.ColumnHeader colTx;
        private System.Windows.Forms.ColumnHeader colRx;
        private System.Windows.Forms.ColumnHeader colRssi;
        private System.Windows.Forms.ColumnHeader colResult;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatus;
        private System.Windows.Forms.StatusStrip statusTest;
        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar;
        private System.Windows.Forms.ToolStripStatusLabel toolStripMacAddress;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private System.Windows.Forms.ContextMenuStrip cntMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem menuItemAddDoc;
        private System.Windows.Forms.ToolStripMenuItem menuItemDelDoc;
        private System.Windows.Forms.ToolStripMenuItem menuItemEditDoc;
        private System.Windows.Forms.ToolStripMenuItem menuItemClearDoc;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem menuItemLoadDoc;
        private System.Windows.Forms.ToolStripMenuItem menuItemSaveDoc;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem menuItemSaveResult;
        private System.Windows.Forms.ToolStripMenuItem menuItemRunCmd;
        private System.Windows.Forms.ToolStripMenuItem menuSubItemReadVer;
        private System.Windows.Forms.ToolStripMenuItem menuSubItemReadAmmeter;
        private System.Windows.Forms.ToolStripMenuItem menuSubItemReadPower;
        private System.Windows.Forms.ComboBox combCurrDis;
        private System.Windows.Forms.ToolStripMenuItem menuItemCopy;
        private System.Windows.Forms.ComboBox combChanelGrp;
        private System.Windows.Forms.Label lbChannel;
        private System.Windows.Forms.ComboBox combFreq;
        private System.Windows.Forms.Label lbFreq;
        private System.Windows.Forms.CheckBox chkRssi;
        private System.Windows.Forms.CheckBox chkCurrent;
        private System.Windows.Forms.ColumnHeader colNo;
        private System.Windows.Forms.ComboBox combProtoVer;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox chkSaveLog;
        private System.Windows.Forms.CheckBox chkLoopTest;
        private System.Windows.Forms.Label lbCurrChk;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem menuItemTestStart;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
    }
}

