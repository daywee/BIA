﻿namespace Lesson02
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
            this.SuspendLayout();
            // 
            // renderContainer
            // 
            this.renderContainer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.renderContainer.Location = new System.Drawing.Point(12, 56);
            this.renderContainer.Name = "renderContainer";
            this.renderContainer.Size = new System.Drawing.Size(776, 382);
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
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.generationTitleLabel);
            this.Controls.Add(this.generationLabel);
            this.Controls.Add(this.newPopulationButton);
            this.Controls.Add(this.evolveButton);
            this.Controls.Add(this.functionsComboBox);
            this.Controls.Add(this.renderContainer);
            this.Name = "Form1";
            this.Text = "Form1";
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
    }
}

