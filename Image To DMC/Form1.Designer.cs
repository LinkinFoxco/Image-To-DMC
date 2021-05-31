
namespace Image_To_DMC
{
    partial class ConvertisseurForm
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
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.ChoisirUneImageButton = new System.Windows.Forms.Button();
            this.ImagePreviewBox = new System.Windows.Forms.PictureBox();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.ConvertirButton = new System.Windows.Forms.Button();
            this.TotalPerleInputBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.TotalPerlesFinalInputBox = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.ImagePreviewBox)).BeginInit();
            this.SuspendLayout();
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // ChoisirUneImageButton
            // 
            this.ChoisirUneImageButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChoisirUneImageButton.Location = new System.Drawing.Point(12, 255);
            this.ChoisirUneImageButton.Name = "ChoisirUneImageButton";
            this.ChoisirUneImageButton.Size = new System.Drawing.Size(202, 37);
            this.ChoisirUneImageButton.TabIndex = 0;
            this.ChoisirUneImageButton.Text = "Choisir une image";
            this.ChoisirUneImageButton.UseVisualStyleBackColor = true;
            this.ChoisirUneImageButton.Click += new System.EventHandler(this.button1_Click);
            // 
            // ImagePreviewBox
            // 
            this.ImagePreviewBox.InitialImage = null;
            this.ImagePreviewBox.Location = new System.Drawing.Point(428, 12);
            this.ImagePreviewBox.Name = "ImagePreviewBox";
            this.ImagePreviewBox.Size = new System.Drawing.Size(454, 280);
            this.ImagePreviewBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.ImagePreviewBox.TabIndex = 2;
            this.ImagePreviewBox.TabStop = false;
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(12, 310);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(870, 36);
            this.progressBar.TabIndex = 3;
            this.progressBar.Visible = false;
            // 
            // ConvertirButton
            // 
            this.ConvertirButton.Enabled = false;
            this.ConvertirButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ConvertirButton.Location = new System.Drawing.Point(220, 255);
            this.ConvertirButton.Name = "ConvertirButton";
            this.ConvertirButton.Size = new System.Drawing.Size(202, 37);
            this.ConvertirButton.TabIndex = 4;
            this.ConvertirButton.Text = "Convertir";
            this.ConvertirButton.UseVisualStyleBackColor = true;
            this.ConvertirButton.Click += new System.EventHandler(this.button2_Click);
            // 
            // TotalPerleInputBox
            // 
            this.TotalPerleInputBox.Enabled = false;
            this.TotalPerleInputBox.Location = new System.Drawing.Point(121, 12);
            this.TotalPerleInputBox.Name = "TotalPerleInputBox";
            this.TotalPerleInputBox.Size = new System.Drawing.Size(124, 20);
            this.TotalPerleInputBox.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(91, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Total perles voulu";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 51);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(84, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Total perles final";
            // 
            // TotalPerlesFinalInputBox
            // 
            this.TotalPerlesFinalInputBox.Enabled = false;
            this.TotalPerlesFinalInputBox.Location = new System.Drawing.Point(121, 48);
            this.TotalPerlesFinalInputBox.Name = "TotalPerlesFinalInputBox";
            this.TotalPerlesFinalInputBox.Size = new System.Drawing.Size(124, 20);
            this.TotalPerlesFinalInputBox.TabIndex = 9;
            // 
            // ConvertisseurForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(894, 358);
            this.Controls.Add(this.TotalPerlesFinalInputBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.TotalPerleInputBox);
            this.Controls.Add(this.ConvertirButton);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.ImagePreviewBox);
            this.Controls.Add(this.ChoisirUneImageButton);
            this.Name = "ConvertisseurForm";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.ImagePreviewBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button ChoisirUneImageButton;
        private System.Windows.Forms.PictureBox ImagePreviewBox;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Button ConvertirButton;
        private System.Windows.Forms.TextBox TotalPerleInputBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox TotalPerlesFinalInputBox;
    }
}

