namespace UASKI
{
    partial class Gl_Form
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.Menu_Step1 = new System.Windows.Forms.ListBox();
            this.Menu_Step2 = new System.Windows.Forms.ListBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tabControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // Menu_Step1
            // 
            this.Menu_Step1.CausesValidation = false;
            this.Menu_Step1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Menu_Step1.FormattingEnabled = true;
            this.Menu_Step1.ItemHeight = 24;
            this.Menu_Step1.Location = new System.Drawing.Point(-1, 1);
            this.Menu_Step1.Name = "Menu_Step1";
            this.Menu_Step1.Size = new System.Drawing.Size(538, 172);
            this.Menu_Step1.TabIndex = 0;
            this.Menu_Step1.SelectedIndexChanged += new System.EventHandler(this.Menu_Step1_SelectedIndexChanged);
            this.Menu_Step1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Menu_Step1_KeyDown);
            // 
            // Menu_Step2
            // 
            this.Menu_Step2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Menu_Step2.FormattingEnabled = true;
            this.Menu_Step2.ItemHeight = 24;
            this.Menu_Step2.Location = new System.Drawing.Point(543, 1);
            this.Menu_Step2.Name = "Menu_Step2";
            this.Menu_Step2.Size = new System.Drawing.Size(633, 172);
            this.Menu_Step2.TabIndex = 1;
            this.Menu_Step2.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Menu_Step2_KeyDown);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(-1, 170);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1189, 627);
            this.tabControl1.TabIndex = 2;
            // 
            // tabPage1
            // 
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1181, 601);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(0, 0);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // Gl_Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1188, 797);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.Menu_Step2);
            this.Controls.Add(this.Menu_Step1);
            this.Name = "Gl_Form";
            this.Text = "UASKI";
            this.tabControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox Menu_Step1;
        private System.Windows.Forms.ListBox Menu_Step2;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
    }
}

