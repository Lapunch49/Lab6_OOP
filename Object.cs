using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace Lab6_OOP
{
    public class СObject
    {
        public СObject() { }
        protected bool highlighted = false;
        protected Color color;
        protected int x, y;
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
        public virtual string classname() { return "Cbject"; }
        public virtual СObject new_obj(int x, int y, Color c)
        {
            return new СObject();
        }
    }
    public class CCircle : СObject
    {
        private int r = 25;
        public CCircle(int x, int y, Color color)
        {
            this.x = x;
            this.y = y;
            this.color = color;
        }
        public override void draw(PaintEventArgs e)
        {
            Brush.normBrush.Color = color;
            if (highlighted == false)
                e.Graphics.FillEllipse(Brush.normBrush, x- r, y- r, r * 2, r * 2);
            else e.Graphics.FillEllipse(Brush.highlightBrush, x - r, y - r, r * 2, r * 2);
        }
        public override bool mouseClick_on_Object(int x_, int y_)
        {
            if ((x_ - x)* (x_ - x) + (y_ - y) * (y_ - y) <= r * r)
                return true;
            else return false;
        }
        public override string classname() { return "CCircle"; }
        public override СObject new_obj(int x, int y, Color color)
        {
            return new CCircle(x, y, color);
        }
    };

    public class CTriangle : СObject
    {
        private int l = 25;
        public CTriangle(int x, int y, Color col)
        {
            this.x = x;
            this.y = y;
            color = col;
        }
        public override void draw(PaintEventArgs e)
        {
            Brush.normBrush.Color = color;
            Point[] arrPoints = { new Point(x, y - l), new Point(x + l, y + l), new Point(x - l, y + l) };
            if (highlighted == false)
                e.Graphics.FillPolygon(Brush.normBrush, arrPoints);
            else e.Graphics.FillPolygon(Brush.highlightBrush, arrPoints);
        }
        public override bool mouseClick_on_Object(int x_, int y_)
        {
            if (x>=(x_-l) && x <= (x_ + l) && y >= (y_ - l) && y <= (y_ + l))
                return true;
            else return false;
        }
        public override string classname() { return "CTriangle"; }
        public override СObject new_obj(int x, int y, Color color)
        {
            return new CTriangle(x, y, color);
        }
    };
}
