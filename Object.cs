using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace Lab6_OOP
{
    public class Object
    {
        public virtual void draw(PaintEventArgs e) { }
        public virtual void highlight() { }
        public virtual bool mouseClick_on_Object(int x_, int y_) { return false; }
        public virtual void change_highlight() { }
        public virtual bool get_highlighted() { return false; }
    }
    public class CCircle : Object
    {
        private int x, y, r;
        private bool highlighted = false;
        public CCircle()
        {
            x = 0;
            y = 0;
            r = 30;
        }
        public CCircle(int x, int y)
        {
            this.x = x;
            this.y = y;
            r = 30;
        }
        public override void draw(PaintEventArgs e)
        {
            //Graphics g = Graphics.FromImage(Form1.bmp);
            if (highlighted == false)
                e.Graphics.FillEllipse(Brush.violBrush, x - r, y - r, r * 2, r * 2);
            else e.Graphics.FillEllipse(Brush.pinkBrush, x - r, y - r, r * 2, r * 2);
        }
        public override bool mouseClick_on_Object(int x_, int y_)
        {
            if ((x_ - x)* (x_ - x)+ (y_ - y)* (y_ - y) <= r * r)
                return true;
            else return false;
        }
        public override void change_highlight()
        {
            if (highlighted)
                highlighted = false;
            else highlighted = true;
        }
        public override bool get_highlighted()
        {
            return highlighted;
        }
    };
}
