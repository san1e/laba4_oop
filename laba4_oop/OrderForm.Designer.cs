namespace laba4_oop
{
    partial class OrderForm
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
            this.components = new System.ComponentModel.Container();
            this.cafeNameTextBox = new System.Windows.Forms.TextBox();
            this.orderDateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.addDishButton = new System.Windows.Forms.Button();
            this.editDishButton = new System.Windows.Forms.Button();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.addChefButton = new System.Windows.Forms.Button();
            this.editChefButton = new System.Windows.Forms.Button();
            this.saveButton = new System.Windows.Forms.Button();
            this.chefComboBox = new System.Windows.Forms.ComboBox();
            this.dishComboBox = new System.Windows.Forms.ComboBox();
            this.dishesListBox = new System.Windows.Forms.ListBox();
            this.orderListBox = new System.Windows.Forms.ListBox();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // cafeNameTextBox
            // 
            this.cafeNameTextBox.Location = new System.Drawing.Point(12, 12);
            this.cafeNameTextBox.Name = "cafeNameTextBox";
            this.cafeNameTextBox.Size = new System.Drawing.Size(236, 20);
            this.cafeNameTextBox.TabIndex = 0;
            this.cafeNameTextBox.Text = "CafeName";
            this.cafeNameTextBox.TextChanged += new System.EventHandler(this.cafeNameTextBox_TextChanged);
            // 
            // orderDateTimePicker
            // 
            this.orderDateTimePicker.Location = new System.Drawing.Point(12, 38);
            this.orderDateTimePicker.Name = "orderDateTimePicker";
            this.orderDateTimePicker.Size = new System.Drawing.Size(236, 20);
            this.orderDateTimePicker.TabIndex = 1;
            this.orderDateTimePicker.ValueChanged += new System.EventHandler(this.orderDateTimePicker_ValueChanged);
            // 
            // addDishButton
            // 
            this.addDishButton.Location = new System.Drawing.Point(12, 243);
            this.addDishButton.Name = "addDishButton";
            this.addDishButton.Size = new System.Drawing.Size(75, 23);
            this.addDishButton.TabIndex = 3;
            this.addDishButton.Text = "Додати";
            this.addDishButton.UseVisualStyleBackColor = true;
            this.addDishButton.Click += new System.EventHandler(this.addDishButton_Click);
            // 
            // editDishButton
            // 
            this.editDishButton.Location = new System.Drawing.Point(173, 243);
            this.editDishButton.Name = "editDishButton";
            this.editDishButton.Size = new System.Drawing.Size(75, 23);
            this.editDishButton.TabIndex = 4;
            this.editDishButton.Text = "Редагувати";
            this.editDishButton.UseVisualStyleBackColor = true;
            this.editDishButton.Click += new System.EventHandler(this.editDishButton_Click);
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // addChefButton
            // 
            this.addChefButton.Location = new System.Drawing.Point(12, 272);
            this.addChefButton.Name = "addChefButton";
            this.addChefButton.Size = new System.Drawing.Size(97, 23);
            this.addChefButton.TabIndex = 5;
            this.addChefButton.Text = "Додати повара";
            this.addChefButton.UseVisualStyleBackColor = true;
            this.addChefButton.Click += new System.EventHandler(this.addChefButton_Click);
            // 
            // editChefButton
            // 
            this.editChefButton.Location = new System.Drawing.Point(115, 272);
            this.editChefButton.Name = "editChefButton";
            this.editChefButton.Size = new System.Drawing.Size(133, 23);
            this.editChefButton.TabIndex = 6;
            this.editChefButton.Text = "Редагувати повара";
            this.editChefButton.UseVisualStyleBackColor = true;
            this.editChefButton.Click += new System.EventHandler(this.editChefButton_Click);
            // 
            // saveButton
            // 
            this.saveButton.Location = new System.Drawing.Point(12, 301);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(236, 23);
            this.saveButton.TabIndex = 7;
            this.saveButton.Text = "Зберегти кафе та дату";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // chefComboBox
            // 
            this.chefComboBox.FormattingEnabled = true;
            this.chefComboBox.Location = new System.Drawing.Point(12, 330);
            this.chefComboBox.Name = "chefComboBox";
            this.chefComboBox.Size = new System.Drawing.Size(236, 21);
            this.chefComboBox.TabIndex = 8;
            this.chefComboBox.Text = "Chefs";
            // 
            // dishComboBox
            // 
            this.dishComboBox.FormattingEnabled = true;
            this.dishComboBox.Location = new System.Drawing.Point(14, 358);
            this.dishComboBox.Name = "dishComboBox";
            this.dishComboBox.Size = new System.Drawing.Size(233, 21);
            this.dishComboBox.TabIndex = 9;
            this.dishComboBox.Text = "Dish";
            // 
            // dishesListBox
            // 
            this.dishesListBox.FormattingEnabled = true;
            this.dishesListBox.Location = new System.Drawing.Point(12, 64);
            this.dishesListBox.Name = "dishesListBox";
            this.dishesListBox.Size = new System.Drawing.Size(236, 173);
            this.dishesListBox.TabIndex = 2;
            // 
            // orderListBox
            // 
            this.orderListBox.FormattingEnabled = true;
            this.orderListBox.Location = new System.Drawing.Point(254, 64);
            this.orderListBox.Name = "orderListBox";
            this.orderListBox.Size = new System.Drawing.Size(416, 173);
            this.orderListBox.TabIndex = 10;
            // 
            // OrderForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(767, 406);
            this.Controls.Add(this.orderListBox);
            this.Controls.Add(this.dishComboBox);
            this.Controls.Add(this.chefComboBox);
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.editChefButton);
            this.Controls.Add(this.addChefButton);
            this.Controls.Add(this.editDishButton);
            this.Controls.Add(this.addDishButton);
            this.Controls.Add(this.dishesListBox);
            this.Controls.Add(this.orderDateTimePicker);
            this.Controls.Add(this.cafeNameTextBox);
            this.Name = "OrderForm";
            this.Text = "Замовлення";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.OrderForm_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox cafeNameTextBox;
        private System.Windows.Forms.DateTimePicker orderDateTimePicker;
        private System.Windows.Forms.Button addDishButton;
        private System.Windows.Forms.Button editDishButton;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.Button addChefButton;
        private System.Windows.Forms.Button editChefButton;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.ComboBox chefComboBox;
        private System.Windows.Forms.ComboBox dishComboBox;
        private System.Windows.Forms.ListBox dishesListBox;
        private System.Windows.Forms.ListBox orderListBox;
    }
}