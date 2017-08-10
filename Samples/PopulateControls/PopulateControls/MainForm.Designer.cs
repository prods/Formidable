namespace PopulateControls
{
    partial class MainForm
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
            this.tbText = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.dgMembers = new System.Windows.Forms.DataGridView();
            this.clmFirstName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmLastName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmAge = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmRegistrationDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sstMain = new System.Windows.Forms.StatusStrip();
            this.tsOngoingOperation = new System.Windows.Forms.ToolStripStatusLabel();
            ((System.ComponentModel.ISupportInitialize)(this.dgMembers)).BeginInit();
            this.sstMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // tbText
            // 
            this.tbText.Location = new System.Drawing.Point(18, 55);
            this.tbText.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tbText.Name = "tbText";
            this.tbText.Size = new System.Drawing.Size(241, 31);
            this.tbText.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 20);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(206, 25);
            this.label1.TabIndex = 1;
            this.label1.Text = "Number of Members";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 19.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(14, 242);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(134, 61);
            this.label2.TabIndex = 2;
            this.label2.Text = "Text";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(18, 105);
            this.button1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(241, 36);
            this.button1.TabIndex = 3;
            this.button1.Text = "Get Members";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // dgMembers
            // 
            this.dgMembers.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgMembers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgMembers.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.clmFirstName,
            this.clmLastName,
            this.clmAge,
            this.clmRegistrationDate});
            this.dgMembers.Location = new System.Drawing.Point(293, 55);
            this.dgMembers.Name = "dgMembers";
            this.dgMembers.RowTemplate.Height = 33;
            this.dgMembers.Size = new System.Drawing.Size(1371, 956);
            this.dgMembers.TabIndex = 4;
            // 
            // clmFirstName
            // 
            this.clmFirstName.HeaderText = "First Name";
            this.clmFirstName.Name = "clmFirstName";
            // 
            // clmLastName
            // 
            this.clmLastName.HeaderText = "LastName";
            this.clmLastName.Name = "clmLastName";
            // 
            // clmAge
            // 
            this.clmAge.HeaderText = "Age";
            this.clmAge.Name = "clmAge";
            // 
            // clmRegistrationDate
            // 
            this.clmRegistrationDate.HeaderText = "Registration Date";
            this.clmRegistrationDate.Name = "clmRegistrationDate";
            this.clmRegistrationDate.ReadOnly = true;
            // 
            // sstMain
            // 
            this.sstMain.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.sstMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsOngoingOperation});
            this.sstMain.Location = new System.Drawing.Point(0, 1041);
            this.sstMain.Name = "sstMain";
            this.sstMain.Size = new System.Drawing.Size(1676, 22);
            this.sstMain.TabIndex = 5;
            // 
            // tsOngoingOperation
            // 
            this.tsOngoingOperation.Name = "tsOngoingOperation";
            this.tsOngoingOperation.Size = new System.Drawing.Size(0, 33);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1676, 1063);
            this.Controls.Add(this.sstMain);
            this.Controls.Add(this.dgMembers);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbText);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "MainForm";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.dgMembers)).EndInit();
            this.sstMain.ResumeLayout(false);
            this.sstMain.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbText;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DataGridView dgMembers;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmFirstName;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmLastName;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmAge;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmRegistrationDate;
        private System.Windows.Forms.StatusStrip sstMain;
        private System.Windows.Forms.ToolStripStatusLabel tsOngoingOperation;
    }
}

