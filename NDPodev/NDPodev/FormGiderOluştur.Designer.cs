
namespace NDPodev
{
    partial class FormGiderOluştur
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
            this.label1 = new System.Windows.Forms.Label();
            this.btnGiderEkle = new System.Windows.Forms.Button();
            this.txtYiyecek = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtDogalgaz = new System.Windows.Forms.TextBox();
            this.txtSu = new System.Windows.Forms.TextBox();
            this.txtElektrik = new System.Windows.Forms.TextBox();
            this.txtElemanMaası = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(111, 63);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(73, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Yiyecekler";
            // 
            // btnGiderEkle
            // 
            this.btnGiderEkle.Location = new System.Drawing.Point(295, 332);
            this.btnGiderEkle.Name = "btnGiderEkle";
            this.btnGiderEkle.Size = new System.Drawing.Size(155, 39);
            this.btnGiderEkle.TabIndex = 1;
            this.btnGiderEkle.Text = "Gider Kaydı Ekle";
            this.btnGiderEkle.UseVisualStyleBackColor = true;
            this.btnGiderEkle.Click += new System.EventHandler(this.btnGiderEkle_Click);
            // 
            // txtYiyecek
            // 
            this.txtYiyecek.Location = new System.Drawing.Point(114, 94);
            this.txtYiyecek.Name = "txtYiyecek";
            this.txtYiyecek.Size = new System.Drawing.Size(153, 22);
            this.txtYiyecek.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(111, 188);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(25, 17);
            this.label2.TabIndex = 3;
            this.label2.Text = "Su";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(506, 188);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(68, 17);
            this.label3.TabIndex = 4;
            this.label3.Text = "Doğalgaz";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(506, 63);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(54, 17);
            this.label4.TabIndex = 5;
            this.label4.Text = "Elektrik";
            // 
            // txtDogalgaz
            // 
            this.txtDogalgaz.Location = new System.Drawing.Point(505, 228);
            this.txtDogalgaz.Name = "txtDogalgaz";
            this.txtDogalgaz.Size = new System.Drawing.Size(100, 22);
            this.txtDogalgaz.TabIndex = 6;
            // 
            // txtSu
            // 
            this.txtSu.Location = new System.Drawing.Point(114, 228);
            this.txtSu.Name = "txtSu";
            this.txtSu.Size = new System.Drawing.Size(100, 22);
            this.txtSu.TabIndex = 7;
            // 
            // txtElektrik
            // 
            this.txtElektrik.Location = new System.Drawing.Point(505, 94);
            this.txtElektrik.Name = "txtElektrik";
            this.txtElektrik.Size = new System.Drawing.Size(100, 22);
            this.txtElektrik.TabIndex = 8;
            // 
            // txtElemanMaası
            // 
            this.txtElemanMaası.Location = new System.Drawing.Point(295, 176);
            this.txtElemanMaası.Name = "txtElemanMaası";
            this.txtElemanMaası.Size = new System.Drawing.Size(153, 22);
            this.txtElemanMaası.TabIndex = 10;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(292, 145);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(147, 17);
            this.label5.TabIndex = 9;
            this.label5.Text = "Toplam Eleman Maaşı";
            // 
            // FormGiderOluştur
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.txtElemanMaası);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtElektrik);
            this.Controls.Add(this.txtSu);
            this.Controls.Add(this.txtDogalgaz);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtYiyecek);
            this.Controls.Add(this.btnGiderEkle);
            this.Controls.Add(this.label1);
            this.Name = "FormGiderOluştur";
            this.Text = "FormGiderOluştur";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnGiderEkle;
        private System.Windows.Forms.TextBox txtYiyecek;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtDogalgaz;
        private System.Windows.Forms.TextBox txtSu;
        private System.Windows.Forms.TextBox txtElektrik;
        private System.Windows.Forms.TextBox txtElemanMaası;
        private System.Windows.Forms.Label label5;
    }
}