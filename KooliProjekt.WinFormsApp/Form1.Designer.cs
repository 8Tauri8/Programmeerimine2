namespace KooliProjekt.WinFormsApp
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();

            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            TodoListsGrid = new DataGridView();
            IdLabel = new Label();
            IdField = new TextBox();
            NewButton = new Button();
            SaveButton = new Button();
            DeleteButton = new Button();

            WeightLabel = new Label();
            WeightField = new TextBox();
            BloodPressureLabel = new Label();
            BloodPressureField = new TextBox();
            BloodSugarLabel = new Label();
            BloodSugarField = new TextBox();

            ((System.ComponentModel.ISupportInitialize)TodoListsGrid).BeginInit();
            SuspendLayout();

            // 
            // TodoListsGrid
            // 
            TodoListsGrid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            TodoListsGrid.Location = new Point(5, 6);
            TodoListsGrid.MultiSelect = false;
            TodoListsGrid.Name = "TodoListsGrid";
            TodoListsGrid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            TodoListsGrid.Size = new Size(419, 432);
            TodoListsGrid.TabIndex = 0;

            // 
            // IdLabel
            // 
            IdLabel.AutoSize = true;
            IdLabel.Location = new Point(460, 16);
            IdLabel.Name = "IdLabel";
            IdLabel.Size = new Size(21, 15);
            IdLabel.TabIndex = 1;
            IdLabel.Text = "ID:";

            // 
            // IdField
            // 
            IdField.Location = new Point(507, 13);
            IdField.Name = "IdField";
            IdField.Size = new Size(281, 23);
            IdField.TabIndex = 2;

            // 
            // WeightLabel
            // 
            WeightLabel.AutoSize = true;
            WeightLabel.Location = new Point(460, 56);
            WeightLabel.Name = "WeightLabel";
            WeightLabel.Size = new Size(52, 15);
            WeightLabel.TabIndex = 3;
            WeightLabel.Text = "Weight:";

            // 
            // WeightField
            // 
            WeightField.Location = new Point(507, 53);
            WeightField.Name = "WeightField";
            WeightField.Size = new Size(281, 23);
            WeightField.TabIndex = 4;

            // 
            // BloodPressureLabel
            // 
            BloodPressureLabel.AutoSize = true;
            BloodPressureLabel.Location = new Point(460, 86);
            BloodPressureLabel.Name = "BloodPressureLabel";
            BloodPressureLabel.Size = new Size(93, 15);
            BloodPressureLabel.TabIndex = 5;
            BloodPressureLabel.Text = "Blood Pressure:";

            // 
            // BloodPressureField
            // 
            BloodPressureField.Location = new Point(507, 83);
            BloodPressureField.Name = "BloodPressureField";
            BloodPressureField.Size = new Size(281, 23);
            BloodPressureField.TabIndex = 6;

            // 
            // BloodSugarLabel
            // 
            BloodSugarLabel.AutoSize = true;
            BloodSugarLabel.Location = new Point(460, 116);
            BloodSugarLabel.Name = "BloodSugarLabel";
            BloodSugarLabel.Size = new Size(75, 15);
            BloodSugarLabel.TabIndex = 7;
            BloodSugarLabel.Text = "Blood Sugar:";

            // 
            // BloodSugarField
            // 
            BloodSugarField.Location = new Point(507, 113);
            BloodSugarField.Name = "BloodSugarField";
            BloodSugarField.Size = new Size(281, 23);
            BloodSugarField.TabIndex = 8;

            // 
            // NewButton
            // 
            NewButton.Location = new Point(522, 146);
            NewButton.Name = "NewButton";
            NewButton.Size = new Size(75, 23);
            NewButton.TabIndex = 9;
            NewButton.Text = "New";
            NewButton.UseVisualStyleBackColor = true;

            // 
            // SaveButton
            // 
            SaveButton.Location = new Point(603, 146);
            SaveButton.Name = "SaveButton";
            SaveButton.Size = new Size(75, 23);
            SaveButton.TabIndex = 10;
            SaveButton.Text = "Save";
            SaveButton.UseVisualStyleBackColor = true;

            // 
            // DeleteButton
            // 
            DeleteButton.Location = new Point(684, 146);
            DeleteButton.Name = "DeleteButton";
            DeleteButton.Size = new Size(75, 23);
            DeleteButton.TabIndex = 11;
            DeleteButton.Text = "Delete";
            DeleteButton.UseVisualStyleBackColor = true;

            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(DeleteButton);
            Controls.Add(SaveButton);
            Controls.Add(NewButton);
            Controls.Add(BloodSugarField);
            Controls.Add(BloodSugarLabel);
            Controls.Add(BloodPressureField);
            Controls.Add(BloodPressureLabel);
            Controls.Add(WeightField);
            Controls.Add(WeightLabel);
            Controls.Add(IdField);
            Controls.Add(IdLabel);
            Controls.Add(TodoListsGrid);
            Name = "Form1";
            Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)TodoListsGrid).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        private DataGridView TodoListsGrid;
        private Label IdLabel;
        private TextBox IdField;
        private Button NewButton;
        private Button SaveButton;
        private Button DeleteButton;

        private Label WeightLabel;
        private TextBox WeightField;
        private Label BloodPressureLabel;
        private TextBox BloodPressureField;
        private Label BloodSugarLabel;
        private TextBox BloodSugarField;
    }
}
