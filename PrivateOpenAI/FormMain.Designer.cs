namespace PrivateOpenAI
{
    partial class formMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(formMain));
            buttonGo = new Button();
            textboxPrompt = new TextBox();
            textboxResponse = new TextBox();
            labelStatus = new Label();
            textboxTemp = new TextBox();
            label1 = new Label();
            SuspendLayout();
            // 
            // buttonGo
            // 
            buttonGo.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            buttonGo.Location = new Point(661, 766);
            buttonGo.Name = "buttonGo";
            buttonGo.Size = new Size(120, 23);
            buttonGo.TabIndex = 0;
            buttonGo.Text = "Go";
            buttonGo.UseVisualStyleBackColor = true;
            buttonGo.Click += button1_Click;
            // 
            // textboxPrompt
            // 
            textboxPrompt.Location = new Point(12, 12);
            textboxPrompt.Multiline = true;
            textboxPrompt.Name = "textboxPrompt";
            textboxPrompt.ScrollBars = ScrollBars.Both;
            textboxPrompt.Size = new Size(769, 238);
            textboxPrompt.TabIndex = 0;
            textboxPrompt.DoubleClick += textboxPrompt_DoubleClick;
            textboxPrompt.KeyDown += textboxPrompt_KeyDown;
            // 
            // textboxResponse
            // 
            textboxResponse.Location = new Point(12, 256);
            textboxResponse.Multiline = true;
            textboxResponse.Name = "textboxResponse";
            textboxResponse.ScrollBars = ScrollBars.Both;
            textboxResponse.Size = new Size(769, 504);
            textboxResponse.TabIndex = 2;
            textboxResponse.DoubleClick += textboxResponse_DoubleClick;
            // 
            // labelStatus
            // 
            labelStatus.AutoSize = true;
            labelStatus.Location = new Point(12, 771);
            labelStatus.Name = "labelStatus";
            labelStatus.Size = new Size(29, 15);
            labelStatus.TabIndex = 3;
            labelStatus.Text = "Idle.";
            // 
            // textboxTemp
            // 
            textboxTemp.Location = new Point(468, 768);
            textboxTemp.MaxLength = 4;
            textboxTemp.Name = "textboxTemp";
            textboxTemp.Size = new Size(37, 23);
            textboxTemp.TabIndex = 4;
            textboxTemp.Text = "1.0";
            textboxTemp.TextChanged += textboxTemp_TextChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(386, 771);
            label1.Name = "label1";
            label1.Size = new Size(76, 15);
            label1.TabIndex = 5;
            label1.Text = "Temperature:";
            // 
            // formMain
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(796, 797);
            Controls.Add(label1);
            Controls.Add(textboxTemp);
            Controls.Add(labelStatus);
            Controls.Add(textboxResponse);
            Controls.Add(textboxPrompt);
            Controls.Add(buttonGo);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "formMain";
            Text = "Private OpenAI";
            Load += formMain_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button buttonGo;
        private TextBox textboxPrompt;
        private TextBox textboxResponse;
        private Label labelStatus;
        private TextBox textboxTemp;
        private Label label1;
    }
}
