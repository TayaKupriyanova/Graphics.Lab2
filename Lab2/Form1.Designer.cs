namespace Lab2
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
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.Download_button = new System.Windows.Forms.Button();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.Transform_button = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox1.Location = new System.Drawing.Point(12, 18);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(400, 400);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseClick);
            // 
            // Download_button
            // 
            this.Download_button.Location = new System.Drawing.Point(886, 18);
            this.Download_button.Name = "Download_button";
            this.Download_button.Size = new System.Drawing.Size(117, 51);
            this.Download_button.TabIndex = 1;
            this.Download_button.Text = "Открыть изображение";
            this.Download_button.UseVisualStyleBackColor = true;
            this.Download_button.Click += new System.EventHandler(this.Download_button_Click);
            // 
            // pictureBox2
            // 
            this.pictureBox2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox2.Location = new System.Drawing.Point(468, 18);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(400, 400);
            this.pictureBox2.TabIndex = 2;
            this.pictureBox2.TabStop = false;
            this.pictureBox2.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pictureBox2_MouseClick);
            // 
            // Transform_button
            // 
            this.Transform_button.Location = new System.Drawing.Point(886, 89);
            this.Transform_button.Name = "Transform_button";
            this.Transform_button.Size = new System.Drawing.Size(117, 43);
            this.Transform_button.TabIndex = 3;
            this.Transform_button.Text = "Построить новое изображение";
            this.Transform_button.UseVisualStyleBackColor = true;
            this.Transform_button.Click += new System.EventHandler(this.Transform_button_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(886, 153);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(117, 43);
            this.button1.TabIndex = 4;
            this.button1.Text = "Очистить";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1040, 448);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.Transform_button);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.Download_button);
            this.Controls.Add(this.pictureBox1);
            this.MaximumSize = new System.Drawing.Size(1056, 487);
            this.MinimumSize = new System.Drawing.Size(1056, 487);
            this.Name = "Form1";
            this.Text = "Form";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button Download_button;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Button Transform_button;
        private System.Windows.Forms.Button button1;
    }
}

