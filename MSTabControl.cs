using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace milano88.UI.Controls
{
    public class MSTabControl : TabControl
    {
        public MSTabControl()
        {
            base.SetStyle(ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.OptimizedDoubleBuffer, true);
            this.DoubleBuffered = true;
            this.SizeMode = TabSizeMode.Fixed;
            this.Font = new Font("Segoe UI", 9F);
            this.ItemSize = new Size(80, 25);
        }

        private Color _headerColor = Color.Transparent;
        [Category("Custom Properties")]
        [DefaultValue(typeof(Color), "Transparent")]
        public Color HeaderColor
        {
            get => _headerColor;
            set
            {
                _headerColor = value;
                this.Invalidate();
            }
        }

        private Color _activeTextColor = Color.White;
        [Category("Custom Properties")]
        [DefaultValue(typeof(Color), "White")]
        public Color ActiveTextColor
        {
            get => _activeTextColor;
            set
            {
                _activeTextColor = value;
                this.Invalidate();
            }
        }

        private Color _activeTabColor = Color.DodgerBlue;
        [Category("Custom Properties")]
        [DefaultValue(typeof(Color), "DodgerBlue")]
        public Color ActiveTabColor
        {
            get => _activeTabColor;
            set
            {
                _activeTabColor = value;
                this.Invalidate();
            }
        }

        private Color _inActiveTextColor = Color.White;
        [Category("Custom Properties")]
        [DefaultValue(typeof(Color), "White")]
        public Color InActiveTextColor
        {
            get => _inActiveTextColor;
            set
            {
                _inActiveTextColor = value;
                this.Invalidate();
            }
        }

        private Color _inActiveTabColor = Color.DeepSkyBlue;
        [Category("Custom Properties")]
        [DefaultValue(typeof(Color), "DeepSkyBlue")]
        public Color InActiveTabColor
        {
            get => _inActiveTabColor;
            set
            {
                _inActiveTabColor = value;
                this.Invalidate();
            }
        }

        private Color _pageColor = Color.White;
        [Category("Custom Properties")]
        [DefaultValue(typeof(Color), "White")]
        public Color PageColor
        {
            get => _pageColor;
            set
            {
                _pageColor = value;
                this.Invalidate();
            }
        }

        private Color _borderColor = Color.DodgerBlue;
        [Category("Custom Properties")]
        [DefaultValue(typeof(Color), "DodgerBlue")]
        public Color BorderColor
        {
            get => _borderColor;
            set
            {
                _borderColor = value;
                this.Invalidate();
            }
        }

        private Color _tabUnderLineColor = Color.White;
        [Category("Custom Properties")]
        [DefaultValue(typeof(Color), "White")]
        public Color TabUnderLineColor
        {
            get => _tabUnderLineColor;
            set
            {
                _tabUnderLineColor = value;
                this.Invalidate();
            }
        }

        [Category("Custom Properties")]
        [DefaultValue(typeof(Size), "80, 25")]
        public new Size ItemSize { get => base.ItemSize; set => base.ItemSize = value; }

        [Category("Custom Properties")]
        [DefaultValue(typeof(Font), "Segoe UI, 9pt")]
        public override Font Font { get => base.Font; set => base.Font = value; }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics graphics = e.Graphics;
            graphics.FillRectangle(new SolidBrush(_headerColor), new Rectangle(0, 0, base.Width, base.Height));
            try { SelectedTab.BackColor = _pageColor; }
            catch { }
            try { SelectedTab.BorderStyle = BorderStyle.None; }
            catch { }
            for (int i = 0; i <= TabCount - 1; i++)
            {
                Rectangle rectangle = new Rectangle(new Point(GetTabRect(i).Location.X, GetTabRect(i).Location.Y), new Size(GetTabRect(i).Width, GetTabRect(i).Height));
                Rectangle rect = new Rectangle(rectangle.Location, new Size(ItemSize.Width, ItemSize.Height));
                int centerTextX = rect.X + (rect.Width / 2) - TextRenderer.MeasureText(TabPages[i].Text, Font).Width / 2;
                int centerTextY = rect.Y + (rect.Height / 2) - TextRenderer.MeasureText(TabPages[i].Text, Font).Height / 2 - 2;
                if (i == SelectedIndex)
                {
                    graphics.FillRectangle(new SolidBrush(_headerColor), rect);
                    graphics.FillRectangle(new SolidBrush(_activeTabColor), rect.X - 2, rect.Y - 2, rect.Width, rect.Height);
                    graphics.FillRectangle(new SolidBrush(_tabUnderLineColor), rect.X - 2, rect.Height - 2, rect.Width, 2);
                    TextRenderer.DrawText(graphics, TabPages[i].Text, Font, new Point(centerTextX, centerTextY), _activeTextColor);
                }
                else
                {
                    graphics.FillRectangle(new SolidBrush(_inActiveTabColor), rect.X - 2, rect.Y - 2, rect.Width, rect.Height);
                    TextRenderer.DrawText(graphics, TabPages[i].Text, Font, new Point(centerTextX, centerTextY), _inActiveTextColor);
                }
            }
            graphics.FillRectangle(new SolidBrush(_pageColor), new Rectangle(0, ItemSize.Height, Width, Height - ItemSize.Height));
            graphics.DrawRectangle(new Pen(_borderColor, 1f), new Rectangle(0, ItemSize.Height, Width - 1, Height - ItemSize.Height - 1));
        }

        struct RECT{ public int Left, Top, Right, Bottom; }
        protected override void WndProc(ref Message m)
        {
            if (m.Msg == 0x1300 + 40)
            {
                RECT rc = (RECT)m.GetLParam(typeof(RECT));
                rc.Left -= 3;
                rc.Right += 3;
                rc.Top -= 3;
                rc.Bottom += 3;
                System.Runtime.InteropServices.Marshal.StructureToPtr(rc, m.LParam, true);
            }
            base.WndProc(ref m);
        }
    }
}
