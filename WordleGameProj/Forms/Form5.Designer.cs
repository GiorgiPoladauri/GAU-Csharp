namespace WordleGameProj
{
    partial class Form5
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.TableLayoutPanel tableGrid;
        private System.Windows.Forms.TableLayoutPanel tableKeys;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.tableGrid = new System.Windows.Forms.TableLayoutPanel();
            this.tableKeys = new System.Windows.Forms.TableLayoutPanel();
            this.SuspendLayout();
            // 
            // tableGrid
            // 
            this.tableGrid.ColumnCount = 5;
            this.tableGrid.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableGrid.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableGrid.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableGrid.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableGrid.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableGrid.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableGrid.Location = new System.Drawing.Point(0, 0);
            this.tableGrid.Name = "tableGrid";
            this.tableGrid.RowCount = 6;
            this.tableGrid.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.6667F));
            this.tableGrid.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.6667F));
            this.tableGrid.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.6667F));
            this.tableGrid.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.6667F));
            this.tableGrid.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.6667F));
            this.tableGrid.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.6667F));
            this.tableGrid.Size = new System.Drawing.Size(500, 360);
            this.tableGrid.TabIndex = 1;
            // 
            // tableKeys
            // 
            this.tableKeys.ColumnCount = 10;
            this.tableKeys.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableKeys.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableKeys.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableKeys.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableKeys.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableKeys.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableKeys.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableKeys.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableKeys.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableKeys.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableKeys.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tableKeys.Location = new System.Drawing.Point(0, 370);
            this.tableKeys.Name = "tableKeys";
            this.tableKeys.RowCount = 3;
            this.tableKeys.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableKeys.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableKeys.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableKeys.Size = new System.Drawing.Size(500, 150);
            this.tableKeys.TabIndex = 0;
            // 
            // Form5
            // 
            this.ClientSize = new System.Drawing.Size(500, 520);
            this.Controls.Add(this.tableKeys);
            this.Controls.Add(this.tableGrid);
            this.Name = "Form5";
            this.ResumeLayout(false);

        }
    }
}
