namespace MoralAlignmentTest
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Label lblQuestion;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.RadioButton radioButton3;
        private System.Windows.Forms.RadioButton radioButton4;
        private System.Windows.Forms.RadioButton radioButton5;
        private System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.Label lblResult;
        private System.Windows.Forms.LinkLabel linkMoreInfo;


        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            lblQuestion = new Label();
            radioButton1 = new RadioButton();
            radioButton2 = new RadioButton();
            radioButton3 = new RadioButton();
            radioButton4 = new RadioButton();
            radioButton5 = new RadioButton();
            btnBack = new Button();
            btnNext = new Button();
            btnReset = new Button();
            lblResult = new Label();
            linkMoreInfo = new System.Windows.Forms.LinkLabel();
            // 
            // linkMoreInfo
            // 
            linkMoreInfo.AutoSize = true;
            linkMoreInfo.Location = new System.Drawing.Point(15, 295);
            linkMoreInfo.Name = "linkMoreInfo";
            linkMoreInfo.Size = new System.Drawing.Size(100, 20);
            linkMoreInfo.TabIndex = 11;
            linkMoreInfo.TabStop = true;
            linkMoreInfo.Text = "More Info";
            linkMoreInfo.Visible = false;
            linkMoreInfo.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkMoreInfo_LinkClicked);
            pictureBox1 = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // lblQuestion
            // 
            lblQuestion.AutoSize = true;
            lblQuestion.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblQuestion.Location = new Point(12, 9);
            lblQuestion.MaximumSize = new Size(560, 0);
            lblQuestion.Name = "lblQuestion";
            lblQuestion.Size = new Size(72, 20);
            lblQuestion.TabIndex = 0;
            lblQuestion.Text = "Question";
            // 
            // radioButton1
            // 
            radioButton1.Location = new Point(15, 50);
            radioButton1.Name = "radioButton1";
            radioButton1.Size = new Size(550, 30);
            radioButton1.TabIndex = 1;
            radioButton1.TabStop = true;
            radioButton1.Text = "Answer 1";
            radioButton1.UseVisualStyleBackColor = true;
            // 
            // radioButton2
            // 
            radioButton2.Location = new Point(15, 85);
            radioButton2.Name = "radioButton2";
            radioButton2.Size = new Size(550, 30);
            radioButton2.TabIndex = 2;
            radioButton2.TabStop = true;
            radioButton2.Text = "Answer 2";
            radioButton2.UseVisualStyleBackColor = true;
            // 
            // radioButton3
            // 
            radioButton3.Location = new Point(15, 120);
            radioButton3.Name = "radioButton3";
            radioButton3.Size = new Size(550, 30);
            radioButton3.TabIndex = 3;
            radioButton3.TabStop = true;
            radioButton3.Text = "Answer 3";
            radioButton3.UseVisualStyleBackColor = true;
            // 
            // radioButton4
            // 
            radioButton4.Location = new Point(15, 155);
            radioButton4.Name = "radioButton4";
            radioButton4.Size = new Size(550, 30);
            radioButton4.TabIndex = 4;
            radioButton4.TabStop = true;
            radioButton4.Text = "Answer 4";
            radioButton4.UseVisualStyleBackColor = true;
            // 
            // radioButton5
            // 
            radioButton5.Location = new Point(15, 190);
            radioButton5.Name = "radioButton5";
            radioButton5.Size = new Size(550, 30);
            radioButton5.TabIndex = 5;
            radioButton5.TabStop = true;
            radioButton5.Text = "Answer 5";
            radioButton5.UseVisualStyleBackColor = true;
            // 
            // btnBack
            // 
            btnBack.Location = new Point(15, 230);
            btnBack.Name = "btnBack";
            btnBack.Size = new Size(75, 30);
            btnBack.TabIndex = 6;
            btnBack.Text = "Back";
            btnBack.UseVisualStyleBackColor = true;
            btnBack.Click += btnBack_Click;
            // 
            // btnNext
            // 
            btnNext.Location = new Point(120, 230);
            btnNext.Name = "btnNext";
            btnNext.Size = new Size(75, 30);
            btnNext.TabIndex = 7;
            btnNext.Text = "Next";
            btnNext.UseVisualStyleBackColor = true;
            btnNext.Click += btnNext_Click;
            // 
            // btnReset
            // 
            btnReset.Location = new Point(470, 230);
            btnReset.Name = "btnReset";
            btnReset.Size = new Size(95, 30);
            btnReset.TabIndex = 8;
            btnReset.Text = "Reset";
            btnReset.UseVisualStyleBackColor = true;
            btnReset.Click += btnReset_Click;
            // 
            // lblResult
            // 
            lblResult.AutoSize = true;
            lblResult.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblResult.Location = new Point(12, 275);
            lblResult.MaximumSize = new Size(560, 0);
            lblResult.Name = "lblResult";
            lblResult.Size = new Size(49, 19);
            lblResult.TabIndex = 9;
            lblResult.Text = "Result";
            // 
            // pictureBox1
            // 
            pictureBox1.BorderStyle = BorderStyle.Fixed3D;
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(514, 9);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(58, 71);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 10;
            pictureBox1.TabStop = false;
            pictureBox1.Click += pictureBox1_Click;
            // 
            // Form1
            // 
            ClientSize = new Size(584, 311);
            Controls.Add(pictureBox1);
            Controls.Add(lblResult);
            Controls.Add(linkMoreInfo);
            Controls.Add(btnReset);
            Controls.Add(btnNext);
            Controls.Add(btnBack);
            Controls.Add(radioButton5);
            Controls.Add(radioButton4);
            Controls.Add(radioButton3);
            Controls.Add(radioButton2);
            Controls.Add(radioButton1);
            Controls.Add(lblQuestion);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Moral Alignment Test";
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        private PictureBox pictureBox1;
    }
}
