namespace TaskSplitter11
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.btnCreate = new System.Windows.Forms.Button();
            this.btnCreateBlank = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            //
            // btnCreate
            //
            this.btnCreate.Location = new System.Drawing.Point(12, 12);
            this.btnCreate.Name = "btnCreate";
            this.btnCreate.Size = new System.Drawing.Size(114, 46);
            this.btnCreate.TabIndex = 1;
            this.btnCreate.Text = "Create Separator";
            this.btnCreate.UseVisualStyleBackColor = true;
            this.btnCreate.Click += new System.EventHandler(this.btnCreate_Click);
            //
            // btnCreateBlank
            //
            this.btnCreateBlank.Location = new System.Drawing.Point(12, 64);
            this.btnCreateBlank.Name = "btnCreateBlank";
            this.btnCreateBlank.Size = new System.Drawing.Size(114, 46);
            this.btnCreateBlank.TabIndex = 3;
            this.btnCreateBlank.Text = "Create Separator (Blank)";
            this.btnCreateBlank.UseVisualStyleBackColor = true;
            this.btnCreateBlank.Click += new System.EventHandler(this.btnCreateBlank_Click);
            //
            // label1
            //
            this.label1.Location = new System.Drawing.Point(132, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(337, 46);
            this.label1.TabIndex = 2;
            this.label1.Text = "Creates a separator with the default icon";
            //
            // label2
            //
            this.label2.Location = new System.Drawing.Point(132, 70);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(337, 46);
            this.label2.TabIndex = 4;
            this.label2.Text = "Creates a separator with a blank/transparent icon";
            //
            // MainForm
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(478, 124);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnCreateBlank);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnCreate);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TaskSeparator11";
            this.ResumeLayout(false);

        }

        #endregion

        private Button btnCreate;
        private Button btnCreateBlank;
        private Label label1;
        private Label label2;
    }
}