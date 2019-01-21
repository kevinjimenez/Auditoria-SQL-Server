namespace AuditoriaFront
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
            this.auditar_button = new System.Windows.Forms.Button();
            this.user_database_name_input = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.resultados_text_area = new System.Windows.Forms.RichTextBox();
            this.user_text_area_input = new System.Windows.Forms.RichTextBox();
            this.Guardar = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // auditar_button
            // 
            this.auditar_button.Location = new System.Drawing.Point(11, 112);
            this.auditar_button.Margin = new System.Windows.Forms.Padding(2);
            this.auditar_button.Name = "auditar_button";
            this.auditar_button.Size = new System.Drawing.Size(105, 36);
            this.auditar_button.TabIndex = 0;
            this.auditar_button.Text = "AUDITAR IR";
            this.auditar_button.UseVisualStyleBackColor = true;
            this.auditar_button.Click += new System.EventHandler(this.Auditar_Button_Pressed);
            // 
            // user_database_name_input
            // 
            this.user_database_name_input.AccessibleName = "";
            this.user_database_name_input.Location = new System.Drawing.Point(170, 23);
            this.user_database_name_input.Margin = new System.Windows.Forms.Padding(2);
            this.user_database_name_input.Name = "user_database_name_input";
            this.user_database_name_input.Size = new System.Drawing.Size(211, 20);
            this.user_database_name_input.TabIndex = 1;
            this.user_database_name_input.TextChanged += new System.EventHandler(this.user_database_name_input_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 26);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(143, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "NOMBRE BASE DE DATOS";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // resultados_text_area
            // 
            this.resultados_text_area.Location = new System.Drawing.Point(11, 164);
            this.resultados_text_area.Margin = new System.Windows.Forms.Padding(2);
            this.resultados_text_area.Name = "resultados_text_area";
            this.resultados_text_area.Size = new System.Drawing.Size(760, 199);
            this.resultados_text_area.TabIndex = 3;
            this.resultados_text_area.Text = "";
            // 
            // user_text_area_input
            // 
            this.user_text_area_input.Location = new System.Drawing.Point(385, 26);
            this.user_text_area_input.Margin = new System.Windows.Forms.Padding(2);
            this.user_text_area_input.Name = "user_text_area_input";
            this.user_text_area_input.Size = new System.Drawing.Size(106, 19);
            this.user_text_area_input.TabIndex = 6;
            this.user_text_area_input.Text = "";
            this.user_text_area_input.Visible = false;
            this.user_text_area_input.TextChanged += new System.EventHandler(this.user_text_area_input_TextChanged);
            // 
            // Guardar
            // 
            this.Guardar.Location = new System.Drawing.Point(667, 112);
            this.Guardar.Margin = new System.Windows.Forms.Padding(2);
            this.Guardar.Name = "Guardar";
            this.Guardar.Size = new System.Drawing.Size(104, 36);
            this.Guardar.TabIndex = 7;
            this.Guardar.Text = "GENERAR LOG";
            this.Guardar.UseVisualStyleBackColor = true;
            this.Guardar.Click += new System.EventHandler(this.guardar_resultados);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(608, 26);
            this.button1.Margin = new System.Windows.Forms.Padding(2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(56, 19);
            this.button1.TabIndex = 8;
            this.button1.Text = "DBCC(3)";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.DBCC_Button_Pressed);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(366, 112);
            this.button2.Margin = new System.Windows.Forms.Padding(2);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(124, 36);
            this.button2.TabIndex = 9;
            this.button2.Text = "NUM FK FALTANTES";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(512, 26);
            this.button3.Margin = new System.Windows.Forms.Padding(2);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(83, 19);
            this.button3.TabIndex = 10;
            this.button3.Text = "PK existentes";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Visible = false;
            this.button3.Click += new System.EventHandler(this.pk_button_pressed);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(121, 112);
            this.button4.Margin = new System.Windows.Forms.Padding(2);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(106, 36);
            this.button4.TabIndex = 11;
            this.button4.Text = "PK EXISTENTES";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.relation_tables_button_pressed);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(512, 112);
            this.button5.Margin = new System.Windows.Forms.Padding(2);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(131, 36);
            this.button5.TabIndex = 12;
            this.button5.Text = "ANOMALIA DE DATOS";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.c);
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(242, 112);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(106, 36);
            this.button6.TabIndex = 13;
            this.button6.Text = "INTEGRIDAD DE OPERACIONES";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(779, 396);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.Guardar);
            this.Controls.Add(this.user_text_area_input);
            this.Controls.Add(this.resultados_text_area);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.user_database_name_input);
            this.Controls.Add(this.auditar_button);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button auditar_button;
        private System.Windows.Forms.TextBox user_database_name_input;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RichTextBox resultados_text_area;
        private System.Windows.Forms.RichTextBox user_text_area_input;
        private System.Windows.Forms.Button Guardar;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.Button button3;
		private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button6;
    }
}

