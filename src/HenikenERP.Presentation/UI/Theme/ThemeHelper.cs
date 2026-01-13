using System;
using System.Drawing;
using System.Windows.Forms;
using System.Linq;

namespace HenikenERP.Presentation.UI.Theme
{
    /// <summary>
    /// Helper class to apply theme to WinForms controls
    /// </summary>
    public static class ThemeHelper
    {
        /// <summary>
        /// Recursively applies theme to a control and all its children
        /// </summary>
        public static void ApplyTheme(Control control)
        {
            if (control == null) return;

            // Apply theme based on control type
            ApplyControlTheme(control);

            // Recursively apply to child controls
            foreach (Control child in control.Controls)
            {
                ApplyTheme(child);
            }
        }

        /// <summary>
        /// Applies theme to a single control based on its type
        /// </summary>
        private static void ApplyControlTheme(Control control)
        {
            switch (control)
            {
                case Form form:
                    ApplyFormTheme(form);
                    break;
                case Panel panel:
                    ApplyPanelTheme(panel);
                    break;
                case Label label:
                    ApplyLabelTheme(label);
                    break;
                case Button button:
                    ApplyButtonTheme(button);
                    break;
                case TextBox textBox:
                    ApplyTextBoxTheme(textBox);
                    break;
                case ComboBox comboBox:
                    ApplyComboBoxTheme(comboBox);
                    break;
                case DataGridView dataGridView:
                    ApplyDataGridViewTheme(dataGridView);
                    break;
                case GroupBox groupBox:
                    ApplyGroupBoxTheme(groupBox);
                    break;
                case TabControl tabControl:
                    ApplyTabControlTheme(tabControl);
                    break;
                case MenuStrip menuStrip:
                    ApplyMenuStripTheme(menuStrip);
                    break;
                case ToolStrip toolStrip:
                    ApplyToolStripTheme(toolStrip);
                    break;
            }
        }

        private static void ApplyFormTheme(Form form)
        {
            form.BackColor = ThemeColors.Background;
            form.Font = ThemeFonts.BodyMedium;
            form.ForeColor = ThemeColors.TextPrimary;

            // Apply app icon if available (top-level forms only).
            // MDI child forms showing icons in the menubar can look noisy/duplicated.
            try
            {
                if (form.MdiParent == null)
                {
                    var icon = BrandingAssets.TryGetAppIcon();
                    if (icon != null)
                    {
                        form.Icon = icon;
                    }
                }
            }
            catch { /* ignore */ }
        }

        private static void ApplyPanelTheme(Panel panel)
        {
            // Don't override custom backcolors set in designer
            if (panel.BackColor == SystemColors.Control)
            {
                panel.BackColor = ThemeColors.Surface;
            }
            panel.ForeColor = ThemeColors.TextPrimary;
        }

        private static void ApplyLabelTheme(Label label)
        {
            label.Font = ThemeFonts.Label;
            label.ForeColor = ThemeColors.TextPrimary;
            label.BackColor = Color.Transparent;
        }

        private static void ApplyButtonTheme(Button button)
        {
            // Skip if button has custom styling (use tag to mark custom buttons)
            if (button.Tag?.ToString() == "CustomStyle") return;

            button.Font = ThemeFonts.ButtonMedium;
            button.FlatStyle = FlatStyle.Flat;
            button.FlatAppearance.BorderSize = 0;
            button.Cursor = Cursors.Hand;
            button.UseVisualStyleBackColor = false;

            // Determine button style based on name or default to primary
            bool isSecondary = button.Name.ToLower().Contains("cancel") || 
                              button.Name.ToLower().Contains("secondary") ||
                              button.Text.ToLower().Contains("hủy") ||
                              button.Text.ToLower().Contains("thoát");

            if (isSecondary)
            {
                // Secondary button style
                button.BackColor = ThemeColors.Surface;
                button.ForeColor = ThemeColors.Primary;
                button.FlatAppearance.BorderColor = ThemeColors.Border;
                button.FlatAppearance.BorderSize = 1;
                button.FlatAppearance.MouseOverBackColor = ThemeColors.Background;
                button.FlatAppearance.MouseDownBackColor = ThemeColors.GridRowAlternate;
            }
            else
            {
                // Primary button style
                button.BackColor = ThemeColors.Primary;
                button.ForeColor = ThemeColors.TextOnPrimary;
                button.FlatAppearance.MouseOverBackColor = ThemeColors.PrimaryHover;
                button.FlatAppearance.MouseDownBackColor = ThemeColors.PrimaryPressed;
            }
        }

        private static void ApplyTextBoxTheme(TextBox textBox)
        {
            textBox.Font = ThemeFonts.BodyMedium;
            textBox.BackColor = ThemeColors.Surface;
            textBox.ForeColor = ThemeColors.TextPrimary;
            textBox.BorderStyle = BorderStyle.FixedSingle;
        }

        private static void ApplyComboBoxTheme(ComboBox comboBox)
        {
            comboBox.Font = ThemeFonts.BodyMedium;
            comboBox.BackColor = ThemeColors.Surface;
            comboBox.ForeColor = ThemeColors.TextPrimary;
            comboBox.FlatStyle = FlatStyle.Flat;
        }

        private static void ApplyDataGridViewTheme(DataGridView dataGridView)
        {
            // Background
            dataGridView.BackgroundColor = ThemeColors.Surface;
            dataGridView.GridColor = ThemeColors.GridLine;
            dataGridView.BorderStyle = BorderStyle.None;

            // Default cell style
            dataGridView.DefaultCellStyle.BackColor = ThemeColors.Surface;
            dataGridView.DefaultCellStyle.ForeColor = ThemeColors.TextPrimary;
            dataGridView.DefaultCellStyle.Font = ThemeFonts.BodyMedium;
            dataGridView.DefaultCellStyle.SelectionBackColor = ThemeColors.GridSelection;
            dataGridView.DefaultCellStyle.SelectionForeColor = ThemeColors.TextPrimary;
            dataGridView.DefaultCellStyle.Padding = new Padding(8, 4, 8, 4);

            // Row style
            dataGridView.RowHeadersVisible = false;
            dataGridView.RowTemplate.Height = ThemeMetrics.GridRowHeight;
            dataGridView.AlternatingRowsDefaultCellStyle.BackColor = ThemeColors.GridRowAlternate;
            dataGridView.EnableHeadersVisualStyles = false;

            // Column header style
            var headerStyle = new DataGridViewCellStyle
            {
                BackColor = ThemeColors.GridHeaderBg,
                ForeColor = ThemeColors.GridHeaderText,
                Font = ThemeFonts.LabelBold,
                Alignment = DataGridViewContentAlignment.MiddleLeft,
                Padding = new Padding(8, 8, 8, 8),
                SelectionBackColor = ThemeColors.GridHeaderBg,
                SelectionForeColor = ThemeColors.GridHeaderText
            };
            dataGridView.ColumnHeadersDefaultCellStyle = headerStyle;
            dataGridView.ColumnHeadersHeight = ThemeMetrics.GridHeaderHeight;
            dataGridView.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;

            // Selection
            dataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView.MultiSelect = false;
        }

        private static void ApplyGroupBoxTheme(GroupBox groupBox)
        {
            groupBox.Font = ThemeFonts.LabelBold;
            groupBox.ForeColor = ThemeColors.TextPrimary;
            groupBox.BackColor = Color.Transparent;
        }

        private static void ApplyTabControlTheme(TabControl tabControl)
        {
            tabControl.Font = ThemeFonts.BodyMedium;
            tabControl.BackColor = ThemeColors.Surface;
            tabControl.ForeColor = ThemeColors.TextPrimary;
        }

        private static void ApplyMenuStripTheme(MenuStrip menuStrip)
        {
            menuStrip.BackColor = ThemeColors.Primary;
            menuStrip.ForeColor = ThemeColors.TextOnPrimary;
            menuStrip.Font = ThemeFonts.MenuItem;
            menuStrip.Renderer = new BlueMenuStripRenderer();
        }

        private static void ApplyToolStripTheme(ToolStrip toolStrip)
        {
            toolStrip.BackColor = ThemeColors.Surface;
            toolStrip.ForeColor = ThemeColors.TextPrimary;
            toolStrip.Font = ThemeFonts.BodyMedium;
        }

        /// <summary>
        /// Custom renderer for MenuStrip with blue theme
        /// </summary>
        private class BlueMenuStripRenderer : ToolStripProfessionalRenderer
        {
            public BlueMenuStripRenderer() : base(new BlueColorTable()) { }

            protected override void OnRenderMenuItemBackground(ToolStripItemRenderEventArgs e)
            {
                if (!e.Item.Selected)
                {
                    base.OnRenderMenuItemBackground(e);
                    return;
                }

                // Selected menu item (top menubar vs dropdown items)
                var rect = new Rectangle(Point.Empty, e.Item.Size);
                var selectedBg = e.ToolStrip is ToolStripDropDownMenu
                    ? ThemeColors.GridSelection
                    : ThemeColors.PrimaryHover;
                e.Graphics.FillRectangle(new SolidBrush(selectedBg), rect);
            }

            protected override void OnRenderItemText(ToolStripItemTextRenderEventArgs e)
            {
                // Top menubar: white text on blue. Dropdown: dark text on light background.
                e.TextColor = e.ToolStrip is MenuStrip
                    ? ThemeColors.TextOnPrimary
                    : ThemeColors.TextPrimary;
                base.OnRenderItemText(e);
            }
        }

        private class BlueColorTable : ProfessionalColorTable
        {
            public override Color ToolStripDropDownBackground => ThemeColors.Surface;
            public override Color ImageMarginGradientBegin => ThemeColors.Surface;
            public override Color ImageMarginGradientMiddle => ThemeColors.Surface;
            public override Color ImageMarginGradientEnd => ThemeColors.Surface;

            public override Color MenuBorder => ThemeColors.Border;
            public override Color MenuItemBorder => ThemeColors.Border;

            public override Color MenuItemSelected => ThemeColors.GridSelection;
            public override Color MenuItemSelectedGradientBegin => ThemeColors.GridSelection;
            public override Color MenuItemSelectedGradientEnd => ThemeColors.GridSelection;
            public override Color MenuItemPressedGradientBegin => ThemeColors.GridSelection;
            public override Color MenuItemPressedGradientEnd => ThemeColors.GridSelection;
            public override Color MenuStripGradientBegin => ThemeColors.Primary;
            public override Color MenuStripGradientEnd => ThemeColors.Primary;
        }
    }
}

