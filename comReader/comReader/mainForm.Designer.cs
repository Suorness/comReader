﻿namespace comReader
{
    partial class mainForm
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
            this.trayIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.bnStop = new System.Windows.Forms.Button();
            this.bnStart = new System.Windows.Forms.Button();
            this.listEvent = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.timerUpdateLog = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // trayIcon
            // 
            this.trayIcon.Text = "Security manager\r\nClick to expand";
            this.trayIcon.MouseClick += new System.Windows.Forms.MouseEventHandler(this.trayIcon_MouseClick);
            // 
            // bnStop
            // 
            this.bnStop.Location = new System.Drawing.Point(696, 32);
            this.bnStop.Name = "bnStop";
            this.bnStop.Size = new System.Drawing.Size(182, 23);
            this.bnStop.TabIndex = 0;
            this.bnStop.Text = "Остановить получение данных";
            this.bnStop.UseVisualStyleBackColor = true;
            this.bnStop.Click += new System.EventHandler(this.bnStop_Click);
            // 
            // bnStart
            // 
            this.bnStart.Enabled = false;
            this.bnStart.Location = new System.Drawing.Point(696, 71);
            this.bnStart.Name = "bnStart";
            this.bnStart.Size = new System.Drawing.Size(182, 23);
            this.bnStart.TabIndex = 1;
            this.bnStart.Text = "Возобновить получение данных";
            this.bnStart.UseVisualStyleBackColor = true;
            this.bnStart.Click += new System.EventHandler(this.bnStart_Click);
            // 
            // listEvent
            // 
            this.listEvent.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.listEvent.FormattingEnabled = true;
            this.listEvent.HorizontalScrollbar = true;
            this.listEvent.ItemHeight = 16;
            this.listEvent.Location = new System.Drawing.Point(12, 58);
            this.listEvent.Name = "listEvent";
            this.listEvent.ScrollAlwaysVisible = true;
            this.listEvent.Size = new System.Drawing.Size(662, 164);
            this.listEvent.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(12, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(145, 17);
            this.label1.TabIndex = 3;
            this.label1.Text = "Последние события:";
            // 
            // timerUpdateLog
            // 
            this.timerUpdateLog.Enabled = true;
            this.timerUpdateLog.Interval = 1000;
            this.timerUpdateLog.Tick += new System.EventHandler(this.timerUpdateLog_Tick);
            // 
            // mainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(903, 261);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.listEvent);
            this.Controls.Add(this.bnStart);
            this.Controls.Add(this.bnStop);
            this.Name = "mainForm";
            this.Text = "Security manager";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.mainForm_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.mainForm_FormClosed);
            this.Load += new System.EventHandler(this.mainForm_Load);
            this.Resize += new System.EventHandler(this.mainForm_Resize);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NotifyIcon trayIcon;
        private System.Windows.Forms.Button bnStop;
        private System.Windows.Forms.Button bnStart;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.ListBox listEvent;
        private System.Windows.Forms.Timer timerUpdateLog;
    }
}

