namespace StepTreeControl
{
    partial class StepPropertiesForm
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
            this.cmbOperType = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chbIs1cErrorEqualRuntimeError = new System.Windows.Forms.CheckBox();
            this.chbIsOperErrorEqualRuntimeError = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.edRepeatTimesOnLackOf1cResponse = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.pckrRepeatingIntervalOn1cWaiting = new System.Windows.Forms.DateTimePicker();
            this.pckrWaitingForSuccessInterval = new System.Windows.Forms.DateTimePicker();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmbOperType
            // 
            this.cmbOperType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbOperType.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.cmbOperType.FormattingEnabled = true;
            this.cmbOperType.Location = new System.Drawing.Point(121, 12);
            this.cmbOperType.Name = "cmbOperType";
            this.cmbOperType.Size = new System.Drawing.Size(365, 24);
            this.cmbOperType.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(103, 16);
            this.label1.TabIndex = 1;
            this.label1.Text = "Тип операции:";
            // 
            // btnOk
            // 
            this.btnOk.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnOk.Location = new System.Drawing.Point(262, 163);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(109, 34);
            this.btnOk.TabIndex = 2;
            this.btnOk.Text = "Ok";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnCancel.Location = new System.Drawing.Point(377, 163);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(109, 34);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "Отмена";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(12, 55);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(366, 16);
            this.label2.TabIndex = 4;
            this.label2.Text = "Интервал ожидания успешного выполнения операции:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chbIs1cErrorEqualRuntimeError);
            this.groupBox1.Controls.Add(this.chbIsOperErrorEqualRuntimeError);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.groupBox1.Location = new System.Drawing.Point(15, 120);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(232, 72);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Считать ошибкой выполнения:";
            this.groupBox1.Visible = false;
            // 
            // chbIs1cErrorEqualRuntimeError
            // 
            this.chbIs1cErrorEqualRuntimeError.AutoSize = true;
            this.chbIs1cErrorEqualRuntimeError.Location = new System.Drawing.Point(6, 43);
            this.chbIs1cErrorEqualRuntimeError.Name = "chbIs1cErrorEqualRuntimeError";
            this.chbIs1cErrorEqualRuntimeError.Size = new System.Drawing.Size(96, 20);
            this.chbIs1cErrorEqualRuntimeError.TabIndex = 1;
            this.chbIs1cErrorEqualRuntimeError.Text = "Ошибка 1С";
            this.chbIs1cErrorEqualRuntimeError.UseVisualStyleBackColor = true;
            // 
            // chbIsOperErrorEqualRuntimeError
            // 
            this.chbIsOperErrorEqualRuntimeError.AccessibleDescription = "";
            this.chbIsOperErrorEqualRuntimeError.AutoSize = true;
            this.chbIsOperErrorEqualRuntimeError.Location = new System.Drawing.Point(6, 19);
            this.chbIsOperErrorEqualRuntimeError.Name = "chbIsOperErrorEqualRuntimeError";
            this.chbIsOperErrorEqualRuntimeError.Size = new System.Drawing.Size(227, 20);
            this.chbIsOperErrorEqualRuntimeError.TabIndex = 0;
            this.chbIsOperErrorEqualRuntimeError.Text = "Ошибка выполнения операции";
            this.chbIsOperErrorEqualRuntimeError.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.Location = new System.Drawing.Point(40, 93);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(328, 16);
            this.label3.TabIndex = 7;
            this.label3.Text = "Если не был получен ответ от 1С повторить раз:";
            // 
            // edRepeatTimesOnLackOf1cResponse
            // 
            this.edRepeatTimesOnLackOf1cResponse.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.edRepeatTimesOnLackOf1cResponse.Location = new System.Drawing.Point(397, 87);
            this.edRepeatTimesOnLackOf1cResponse.Name = "edRepeatTimesOnLackOf1cResponse";
            this.edRepeatTimesOnLackOf1cResponse.Size = new System.Drawing.Size(89, 22);
            this.edRepeatTimesOnLackOf1cResponse.TabIndex = 8;
            this.edRepeatTimesOnLackOf1cResponse.Text = "5";
            this.edRepeatTimesOnLackOf1cResponse.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.edRepeatTimesOnLackOf1cResponse_KeyPress);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label4.Location = new System.Drawing.Point(267, 120);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(101, 16);
            this.label4.TabIndex = 9;
            this.label4.Text = "с интервалом:";
            // 
            // pckrRepeatingIntervalOn1cWaiting
            // 
            this.pckrRepeatingIntervalOn1cWaiting.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.pckrRepeatingIntervalOn1cWaiting.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.pckrRepeatingIntervalOn1cWaiting.Location = new System.Drawing.Point(397, 120);
            this.pckrRepeatingIntervalOn1cWaiting.Name = "pckrRepeatingIntervalOn1cWaiting";
            this.pckrRepeatingIntervalOn1cWaiting.ShowUpDown = true;
            this.pckrRepeatingIntervalOn1cWaiting.Size = new System.Drawing.Size(89, 22);
            this.pckrRepeatingIntervalOn1cWaiting.TabIndex = 10;
            // 
            // pckrWaitingForSuccessInterval
            // 
            this.pckrWaitingForSuccessInterval.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.pckrWaitingForSuccessInterval.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.pckrWaitingForSuccessInterval.Location = new System.Drawing.Point(397, 50);
            this.pckrWaitingForSuccessInterval.Name = "pckrWaitingForSuccessInterval";
            this.pckrWaitingForSuccessInterval.ShowUpDown = true;
            this.pckrWaitingForSuccessInterval.Size = new System.Drawing.Size(89, 22);
            this.pckrWaitingForSuccessInterval.TabIndex = 11;
            // 
            // StepPropertiesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(498, 208);
            this.Controls.Add(this.pckrWaitingForSuccessInterval);
            this.Controls.Add(this.pckrRepeatingIntervalOn1cWaiting);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.edRepeatTimesOnLackOf1cResponse);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmbOperType);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "StepPropertiesForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Свойство текущего шага";
            this.Load += new System.EventHandler(this.StepPropertiesForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbOperType;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox chbIs1cErrorEqualRuntimeError;
        private System.Windows.Forms.CheckBox chbIsOperErrorEqualRuntimeError;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox edRepeatTimesOnLackOf1cResponse;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker pckrRepeatingIntervalOn1cWaiting;
        private System.Windows.Forms.DateTimePicker pckrWaitingForSuccessInterval;
    }
}