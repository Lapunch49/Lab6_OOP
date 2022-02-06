using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace Lab6_OOP
{
    public class СObject
    {
        protected bool highlighted = false;
        protected Color color;
        public virtual void draw(PaintEventArgs e) { }
        public virtual bool mouseClick_on_Object(int x_, int y_) { return false; }
        public void change_highlight()
        {
            if (highlighted)
                highlighted = false;
            else highlighted = true;
        }
        public bool get_highlighted()
        {
            return highlighted;
        }
    }
    public class CCircle : СObject
    {
        private int r;
        private Point p;
        public CCircle(int x, int y, int r, Color color)
        {
            this.p.X = x;
            this.p.Y = y;
            this.r = r;
            this.color = color;
        }
        public override void draw(PaintEventArgs e)
        {
            Brush.normBrush.Color = color;
            if (highlighted == false)
                e.Graphics.FillEllipse(Brush.normBrush, p.X - r, p.Y - r, r * 2, r * 2);
            else e.Graphics.FillEllipse(Brush.highlightBrush, p.X - r, p.Y - r, r * 2, r * 2);
        }
        public override bool mouseClick_on_Object(int x_, int y_)
        {
            if ((x_ - p.X)* (x_ - p.X) + (y_ - p.Y) * (y_ - p.Y) <= r * r)
                return true;
            else return false;
        }
    };

    public class CTriangle : СObject
    {
        private Point x, y, z;
        public CTriangle(Point x, Point y, Point z, Color col)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            color = col;
        }
        public override void draw(PaintEventArgs e)
        {
            //Graphics g = Graphics.FromImage(Form1.bmp);
            if (highlighted == false)
                e.Graphics.FillPolygon(Brush.normBrush, new Point[] { x, y, z });
            else e.Graphics.FillPolygon(Brush.highlightBrush, new Point[] { x, y, z });
        }
        public override bool mouseClick_on_Object(int x_, int y_)
        {
            //if ((x_ - x) * (x_ - x) + (y_ - y) * (y_ - y) <= r * r)
                return true;
            //else return false;
        }
    };
}
