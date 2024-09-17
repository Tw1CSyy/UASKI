namespace UASKI
{
    partial class Form1
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
            this.SuspendLayout();
            // 
            // Menu_Step1
            // 
            this.Menu_Step1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Menu_Step1.FormattingEnabled = true;
            this.Menu_Step1.ItemHeight = 24;
            this.Menu_Step1.Items.AddRange(new object[] {
            "Правка",
            "Корритеровка"});
            this.Menu_Step1.Location = new System.Drawing.Point(-1, 1);
            this.Menu_Step1.Name = "Menu_Step1";
            this.Menu_Step1.Size = new System.Drawing.Size(181, 676);
            this.Menu_Step1.TabIndex = 0;
            // 
            // Menu_Step2
            // 
            this.Menu_Step2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Menu_Step2.FormattingEnabled = true;
            this.Menu_Step2.ItemHeight = 24;
            this.Menu_Step2.Location = new System.Drawing.Point(180, 1);
            this.Menu_Step2.Name = "Menu_Step2";
            this.Menu_Step2.Size = new System.Drawing.Size(181, 676);
            this.Menu_Step2.TabIndex = 1;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1188, 678);
            this.Controls.Add(this.Menu_Step2);
            this.Controls.Add(this.Menu_Step1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox Menu_Step1;
        private System.Windows.Forms.ListBox Menu_Step2;
    }
}

