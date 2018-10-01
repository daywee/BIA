namespace Lesson02
{
    partial class Form1
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
            this.renderContainer = new System.Windows.Forms.Panel();
            this.functionsComboBox = new System.Windows.Forms.ComboBox();
            this.evolveButton = new System.Windows.Forms.Button();
            this.newPopulationButton = new System.Windows.Forms.Button();
            this.generationLabel = new System.Windows.Forms.Label();
            this.generationTitleLabel = new System.Windows.Forms.Label();
            this.evolveFiftyTimesButton = new System.Windows.Forms.Button();
            this.meanLabel = new System.Windows.Forms.Label();
            this.standardDeviationTrackBar = new System.Windows.Forms.TrackBar();
            this.meanTrackBar = new System.Windows.Forms.TrackBar();
            this.standardDeviationLabel = new System.Windows.Forms.Label();
            this.meanLabel2 = new System.Windows.Forms.Label();
            this.standardDeviationValueLabel = new System.Windows.Forms.Label();
            this.meanValueLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.standardDeviationTrackBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.meanTrackBar)).BeginInit();
            this.SuspendLayout();
            // 
            // renderContainer
            // 
            this.renderContainer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.renderContainer.Location = new System.Drawing.Point(12, 115);
            this.renderContainer.Name = "renderContainer";
            this.renderContainer.Size = new System.Drawing.Size(1436, 631);
            this.renderContainer.TabIndex = 0;
            // 
            // functionsComboBox
            // 
            this.functionsComboBox.FormattingEnabled = true;
            this.functionsComboBox.Location = new System.Drawing.Point(13, 13);
            this.functionsComboBox.Name = "functionsComboBox";
            this.functionsComboBox.Size = new System.Drawing.Size(193, 21);
            this.functionsComboBox.TabIndex = 1;
            // 
            // evolveButton
            // 
            this.evolveButton.Location = new System.Drawing.Point(337, 13);
            this.evolveButton.Name = "evolveButton";
            this.evolveButton.Size = new System.Drawing.Size(75, 23);
            this.evolveButton.TabIndex = 2;
            this.evolveButton.Text = "Evolve";
            this.evolveButton.UseVisualStyleBackColor = true;
            // 
            // newPopulationButton
            // 
            this.newPopulationButton.Location = new System.Drawing.Point(212, 13);
            this.newPopulationButton.Name = "newPopulationButton";
            this.newPopulationButton.Size = new System.Drawing.Size(119, 23);
            this.newPopulationButton.TabIndex = 3;
            this.newPopulationButton.Text = "Start new population";
            this.newPopulationButton.UseVisualStyleBackColor = true;
            // 
            // generationLabel
            // 
            this.generationLabel.AutoSize = true;
            this.generationLabel.Location = new System.Drawing.Point(752, 16);
            this.generationLabel.Name = "generationLabel";
            this.generationLabel.Size = new System.Drawing.Size(13, 13);
            this.generationLabel.TabIndex = 4;
            this.generationLabel.Text = "0";
            // 
            // generationTitleLabel
            // 
            this.generationTitleLabel.AutoSize = true;
            this.generationTitleLabel.Location = new System.Drawing.Point(684, 16);
            this.generationTitleLabel.Name = "generationTitleLabel";
            this.generationTitleLabel.Size = new System.Drawing.Size(62, 13);
            this.generationTitleLabel.TabIndex = 5;
            this.generationTitleLabel.Text = "Generation:";
            // 
            // evolveFiftyTimesButton
            // 
            this.evolveFiftyTimesButton.Location = new System.Drawing.Point(418, 13);
            this.evolveFiftyTimesButton.Name = "evolveFiftyTimesButton";
            this.evolveFiftyTimesButton.Size = new System.Drawing.Size(75, 23);
            this.evolveFiftyTimesButton.TabIndex = 6;
            this.evolveFiftyTimesButton.Text = "Evolve 50x";
            this.evolveFiftyTimesButton.UseVisualStyleBackColor = true;
            // 
            // meanLabel
            // 
            this.meanLabel.AutoSize = true;
            this.meanLabel.Location = new System.Drawing.Point(684, 36);
            this.meanLabel.Name = "meanLabel";
            this.meanLabel.Size = new System.Drawing.Size(34, 13);
            this.meanLabel.TabIndex = 7;
            this.meanLabel.Text = "Mean";
            // 
            // standardDeviationTrackBar
            // 
            this.standardDeviationTrackBar.LargeChange = 10;
            this.standardDeviationTrackBar.Location = new System.Drawing.Point(147, 42);
            this.standardDeviationTrackBar.Maximum = 100;
            this.standardDeviationTrackBar.Name = "standardDeviationTrackBar";
            this.standardDeviationTrackBar.Size = new System.Drawing.Size(158, 45);
            this.standardDeviationTrackBar.SmallChange = 10;
            this.standardDeviationTrackBar.TabIndex = 8;
            this.standardDeviationTrackBar.TickFrequency = 10;
            this.standardDeviationTrackBar.Value = 10;
            // 
            // meanTrackBar
            // 
            this.meanTrackBar.LargeChange = 10;
            this.meanTrackBar.Location = new System.Drawing.Point(397, 48);
            this.meanTrackBar.Maximum = 50;
            this.meanTrackBar.Minimum = -50;
            this.meanTrackBar.Name = "meanTrackBar";
            this.meanTrackBar.Size = new System.Drawing.Size(158, 45);
            this.meanTrackBar.SmallChange = 10;
            this.meanTrackBar.TabIndex = 9;
            this.meanTrackBar.TickFrequency = 10;
            // 
            // standardDeviationLabel
            // 
            this.standardDeviationLabel.AutoSize = true;
            this.standardDeviationLabel.Location = new System.Drawing.Point(9, 48);
            this.standardDeviationLabel.Name = "standardDeviationLabel";
            this.standardDeviationLabel.Size = new System.Drawing.Size(132, 13);
            this.standardDeviationLabel.TabIndex = 10;
            this.standardDeviationLabel.Text = "Standard deviation (sigma)";
            // 
            // meanLabel2
            // 
            this.meanLabel2.AutoSize = true;
            this.meanLabel2.Location = new System.Drawing.Point(334, 48);
            this.meanLabel2.Name = "meanLabel2";
            this.meanLabel2.Size = new System.Drawing.Size(57, 13);
            this.meanLabel2.TabIndex = 11;
            this.meanLabel2.Text = "Mean (mu)";
            // 
            // standardDeviationValueLabel
            // 
            this.standardDeviationValueLabel.AutoSize = true;
            this.standardDeviationValueLabel.Location = new System.Drawing.Point(220, 80);
            this.standardDeviationValueLabel.Name = "standardDeviationValueLabel";
            this.standardDeviationValueLabel.Size = new System.Drawing.Size(13, 13);
            this.standardDeviationValueLabel.TabIndex = 12;
            this.standardDeviationValueLabel.Text = "1";
            // 
            // meanValueLabel
            // 
            this.meanValueLabel.AutoSize = true;
            this.meanValueLabel.Location = new System.Drawing.Point(469, 80);
            this.meanValueLabel.Name = "meanValueLabel";
            this.meanValueLabel.Size = new System.Drawing.Size(13, 13);
            this.meanValueLabel.TabIndex = 13;
            this.meanValueLabel.Text = "0";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1460, 758);
            this.Controls.Add(this.meanValueLabel);
            this.Controls.Add(this.standardDeviationValueLabel);
            this.Controls.Add(this.meanLabel2);
            this.Controls.Add(this.standardDeviationLabel);
            this.Controls.Add(this.meanTrackBar);
            this.Controls.Add(this.standardDeviationTrackBar);
            this.Controls.Add(this.meanLabel);
            this.Controls.Add(this.evolveFiftyTimesButton);
            this.Controls.Add(this.generationTitleLabel);
            this.Controls.Add(this.generationLabel);
            this.Controls.Add(this.newPopulationButton);
            this.Controls.Add(this.evolveButton);
            this.Controls.Add(this.functionsComboBox);
            this.Controls.Add(this.renderContainer);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.standardDeviationTrackBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.meanTrackBar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel renderContainer;
        private System.Windows.Forms.ComboBox functionsComboBox;
        private System.Windows.Forms.Button evolveButton;
        private System.Windows.Forms.Button newPopulationButton;
        private System.Windows.Forms.Label generationLabel;
        private System.Windows.Forms.Label generationTitleLabel;
        private System.Windows.Forms.Button evolveFiftyTimesButton;
        private System.Windows.Forms.Label meanLabel;
        private System.Windows.Forms.TrackBar standardDeviationTrackBar;
        private System.Windows.Forms.TrackBar meanTrackBar;
        private System.Windows.Forms.Label standardDeviationLabel;
        private System.Windows.Forms.Label meanLabel2;
        private System.Windows.Forms.Label standardDeviationValueLabel;
        private System.Windows.Forms.Label meanValueLabel;
    }
}

