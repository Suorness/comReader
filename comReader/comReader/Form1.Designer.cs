namespace comReader
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.button1 = new System.Windows.Forms.Button();
            this.treu = new System.Windows.Forms.NotifyIcon(this.components);
            this.listDataLog = new System.Windows.Forms.ListBox();
            this.listDevice = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(542, 29);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "bnStart";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // treu
            // 
            this.treu.Icon = ((System.Drawing.Icon)(resources.GetObject("treu.Icon")));
            this.treu.Text = "treu";
            this.treu.MouseClick += new System.Windows.Forms.MouseEventHandler(this.treu_MouseClick);
            // 
            // listDataLog
            // 
            this.listDataLog.FormattingEnabled = true;
            this.listDataLog.Location = new System.Drawing.Point(30, 48);
            this.listDataLog.Name = "listDataLog";
            this.listDataLog.Size = new System.Drawing.Size(385, 173);
            this.listDataLog.TabIndex = 1;
            // 
            // listDevice
            // 
            this.listDevice.FormattingEnabled = true;
            this.listDevice.Location = new System.Drawing.Point(456, 110);
            this.listDevice.Name = "listDevice";
            this.listDevice.Size = new System.Drawing.Size(120, 95);
            this.listDevice.TabIndex = 2;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(665, 261);
            this.Controls.Add(this.listDevice);
            this.Controls.Add(this.listDataLog);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Resize += new System.EventHandler(this.Form1_Resize_1);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.NotifyIcon treu;
        private System.Windows.Forms.ListBox listDataLog;
        private System.Windows.Forms.ListBox listDevice;
    }
}

