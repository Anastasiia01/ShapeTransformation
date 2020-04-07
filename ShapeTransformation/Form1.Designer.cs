namespace ShapeTransformation
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
            this.panInitial = new System.Windows.Forms.Panel();
            this.panTransformed = new System.Windows.Forms.Panel();
            this.btnInitializeShapes = new System.Windows.Forms.Button();
            this.btnApplyTransformation = new System.Windows.Forms.Button();
            this.panNoOutliers = new System.Windows.Forms.Panel();
            this.btnRemoveOutliers = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // panInitial
            // 
            this.panInitial.Location = new System.Drawing.Point(57, 29);
            this.panInitial.Name = "panInitial";
            this.panInitial.Size = new System.Drawing.Size(371, 511);
            this.panInitial.TabIndex = 0;
            // 
            // panTransformed
            // 
            this.panTransformed.Location = new System.Drawing.Point(480, 29);
            this.panTransformed.Name = "panTransformed";
            this.panTransformed.Size = new System.Drawing.Size(371, 511);
            this.panTransformed.TabIndex = 1;
            // 
            // btnInitializeShapes
            // 
            this.btnInitializeShapes.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btnInitializeShapes.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnInitializeShapes.Location = new System.Drawing.Point(119, 568);
            this.btnInitializeShapes.Name = "btnInitializeShapes";
            this.btnInitializeShapes.Size = new System.Drawing.Size(234, 43);
            this.btnInitializeShapes.TabIndex = 2;
            this.btnInitializeShapes.Text = "Initialize Shapes";
            this.btnInitializeShapes.UseVisualStyleBackColor = false;
            this.btnInitializeShapes.Click += new System.EventHandler(this.btnInitializeShapes_Click);
            // 
            // btnApplyTransformation
            // 
            this.btnApplyTransformation.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btnApplyTransformation.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnApplyTransformation.Location = new System.Drawing.Point(543, 568);
            this.btnApplyTransformation.Name = "btnApplyTransformation";
            this.btnApplyTransformation.Size = new System.Drawing.Size(242, 43);
            this.btnApplyTransformation.TabIndex = 3;
            this.btnApplyTransformation.Text = "Apply Transformation";
            this.btnApplyTransformation.UseVisualStyleBackColor = false;
            this.btnApplyTransformation.Click += new System.EventHandler(this.btnApplyTransformation_Click);
            // 
            // panNoOutliers
            // 
            this.panNoOutliers.Location = new System.Drawing.Point(906, 29);
            this.panNoOutliers.Name = "panNoOutliers";
            this.panNoOutliers.Size = new System.Drawing.Size(371, 511);
            this.panNoOutliers.TabIndex = 2;
            // 
            // btnRemoveOutliers
            // 
            this.btnRemoveOutliers.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btnRemoveOutliers.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRemoveOutliers.Location = new System.Drawing.Point(976, 568);
            this.btnRemoveOutliers.Name = "btnRemoveOutliers";
            this.btnRemoveOutliers.Size = new System.Drawing.Size(242, 43);
            this.btnRemoveOutliers.TabIndex = 4;
            this.btnRemoveOutliers.Text = "Outlier Removal";
            this.btnRemoveOutliers.UseVisualStyleBackColor = false;
            this.btnRemoveOutliers.Click += new System.EventHandler(this.btnRemoveOutliers_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1319, 635);
            this.Controls.Add(this.btnRemoveOutliers);
            this.Controls.Add(this.panNoOutliers);
            this.Controls.Add(this.btnApplyTransformation);
            this.Controls.Add(this.btnInitializeShapes);
            this.Controls.Add(this.panTransformed);
            this.Controls.Add(this.panInitial);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panInitial;
        private System.Windows.Forms.Panel panTransformed;
        private System.Windows.Forms.Button btnInitializeShapes;
        private System.Windows.Forms.Button btnApplyTransformation;
        private System.Windows.Forms.Panel panNoOutliers;
        private System.Windows.Forms.Button btnRemoveOutliers;
    }
}

