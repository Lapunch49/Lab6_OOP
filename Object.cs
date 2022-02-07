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
        protected int x, y;
        public СObject() { }
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
        public void set_color(Color new_color)
        {
            color = new_color;
        }
        public virtual string classname() { return "Cbject"; }
        public virtual СObject new_obj(int x, int y, Color c)
        {
            return new СObject();
        }
        public virtual void resize(bool inc, int pbW, int pbH) { }
        protected virtual bool check_move(int move, int pbW, int pbH) { return true; }
        public void move(int move, int pbW, int pbH)
        {
            if (check_move(move, pbW, pbH) == true && move != 0)
                switch (move)
                {
                    case 1: { x += 10; break; }
                    case -1: { x -= 10; break; }
                    case 2: { y -= 10; break; }
                    case -2: { y += 10; break; }
                    default: break;
                }
        }
        protected virtual int check_resize(bool inc, int pbW, int pbH) // возвращает значение, на которое увеличится объект
        {
            return 10;
        }
    }

    public class CRectangle : СObject
    {
        protected int h = 50, w = 70;
        public CRectangle(int x, int y, Color col)
        {
            this.x = x;
            this.y = y;
            color = col;
        }
        public override void draw(PaintEventArgs e)
        {
            Brush.normBrush.Color = color;
            if (highlighted == false)
                e.Graphics.FillRectangle(Brush.normBrush, x - w / 2, y - h / 2, w, h);
            else e.Graphics.FillRectangle(Brush.highlightBrush, x - w / 2, y - h / 2, w, h);
        }
        public override bool mouseClick_on_Object(int x_, int y_)
        {
            if ((x + w / 2) >= x_ && (x - w / 2) <= x_ && (y + h / 2) >= y_ && (y - h / 2) <= y_)
                return true;
            else return false;
        }
        public override string classname() { return "CRectangle"; }
        public override СObject new_obj(int x, int y, Color color)
        {
            return new CRectangle(x, y, color);
        }
        protected override bool check_move(int move, int pbW, int pbH)
        {
            switch (move)
            {
                case 1:
                    {
                        if (x + w / 2 + 10 > pbW) x = pbW - w / 2;
                        return (x + w / 2 + 10 <= pbW);
                    }
                case -1:
                    {
                        if (x - w / 2 - 10 < 0) x = w / 2;
                        return (x - w / 2 - 10 >= 0);
                    }
                case 2:
                    {
                        if (y - h / 2 - 10 < 0) y = h / 2;
                        return (y - h / 2 - 10 >= 0);
                    }
                case -2:
                    {
                        if (y + h / 2 + 10 > pbH) y = pbH - h / 2;
                        return (y + h / 2 + 10 <= pbH);
                    }
                default: return base.check_move(move, pbW, pbH);
            }
        }
        protected override int check_resize(bool inc, int pbW, int pbH)
        {
            int i = 10;
            if (inc == true)
            {
                if (x + w / 2 + 10 > pbW) i = pbW - x - w / 2;
                if (x - w / 2 - 10 < 0 && x - w / 2 < i) i = x - w / 2;
                if (y + h / 2 + 10 > pbH && pbH - y - h / 2 < i) i = pbH - y - h / 2;
                if (y - h / 2 - 10 < 0 && y - h / 2 < i) i = y - h / 2;
            }
            return i;
        }        
        public override void resize(bool inc, int pbW, int pbH)
        {
            if (inc == true)
            {
                int delt_size = check_resize(inc, pbW, pbH);
                w += delt_size; h += delt_size;
            }
            else if (h > 10)
            {
                w -= 10; h -= 10;
            }
        }
    };
    public class CSquare : CRectangle
    {
        public CSquare(int x, int y, Color col) : base(x, y, col)
        {
            w = 50;
        }
        public override string classname() { return "CSquare"; }
        public override СObject new_obj(int x, int y, Color color)
        {
            return new CSquare(x, y, color);
        }
    }
    public class CEllipse : CRectangle
    {
        public CEllipse(int x, int y, Color col) : base(x, y, col) { }
        public override void draw(PaintEventArgs e)
        {
            Brush.normBrush.Color = color;
            if (highlighted == false)
                e.Graphics.FillEllipse(Brush.normBrush, x - w / 2, y - h / 2, w, h);
            else e.Graphics.FillEllipse(Brush.highlightBrush, x - w / 2, y - h / 2, w, h);
        }
        public override bool mouseClick_on_Object(int x_, int y_)
        {
            //if ((x + w / 2) >= x_ && (x - w / 2) <= x_ && (y + h / 2) >= y_ && (y - h / 2) <= y_)
            if ((x_ - x)*(x_ - x)* (h * h)+ (y_ - y) * (y_ - y) *(w*w)<=(w*w*h*h)/4)
                return true;
            else return false;
        }
        public override string classname() { return "CEllipse"; }
        public override СObject new_obj(int x, int y, Color color)
        {
            return new CEllipse(x, y, color);
        }
    }
    public class CCircle : CEllipse
    {
        private int r;
        public CCircle(int x, int y, Color color) : base(x, y, color)
        {
            w = 50;
            h = 50;
            //r = h / 2;
        }
        //public override void draw(PaintEventArgs e)
        //{
        //    Brush.normBrush.Color = color;
        //    if (highlighted == false)
        //        e.Graphics.FillEllipse(Brush.normBrush, x - r, y - r, r * 2, r * 2);
        //    else e.Graphics.FillEllipse(Brush.highlightBrush, x - r, y - r, r * 2, r * 2);
        //}
        public override bool mouseClick_on_Object(int x_, int y_)
        {
            if ((x_ - x) * (x_ - x) + (y_ - y) * (y_ - y) <= w * w/4)
                return true;
            else return false;
        }
        public override string classname() { return "CCircle"; }
        public override СObject new_obj(int x, int y, Color color)
        {
            return new CCircle(x, y, color);
        }
        //public override void resize(bool inc, int pbW, int pbH)
        //{
        //    if (inc == true)
        //        r += 10;
        //    else if (r > 0) r -= 10;
        //}
        //protected override bool check_move(int move, int pbW, int pbH)
        //{
        //    switch (move)
        //    {
        //        case 1:
        //            {
        //                if (x + r + 10 > pbW) x = pbW - r;
        //                return (x + r + 10 <= pbW);
        //            }
        //        case -1:
        //            {
        //                if (x - r - 10 < 0) x = r;
        //                return (x - r - 10 >= 0);
        //            }
        //        case 2:
        //            {
        //                if (y - r - 10 < 0) y = r;
        //                return (y - r - 10 >= 0);
        //            }
        //        case -2:
        //            {
        //                if (y + r + 10 > pbH) y = pbH - r;
        //                return (y + r + 10 <= pbH);
        //            }
        //        default: return base.check_move(move, pbW, pbH);
        //    }
        //}
        //protected override int check_resize(bool inc, int pbW, int pbH)
        //{
        //    return 0;
        //}
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
            if (x >= (x_ - l) && x <= (x_ + l) && y >= (y_ - l) && y <= (y_ + l))
                return true;
            else return false;
        }
        public override string classname() { return "CTriangle"; }
        public override СObject new_obj(int x, int y, Color color)
        {
            return new CTriangle(x, y, color);
        }
        public override void resize(bool inc, int pbW, int pbH)
        {
            if (inc == true)
                l += 10;
            else if (l > 5) l -= 10;
        }
        protected override bool check_move(int move, int pbW, int pbH)
        {
            switch (move)
            {
                case 1:
                    {
                        if (x + l + 10 > pbW) x = pbW - l;
                        return (x + l + 10 <= pbW);
                    }
                case -1:
                    {
                        if (x - l - 10 < 0) x = l;
                        return (x - l - 10 >= 0);
                    }
                case 2:
                    {
                        if (y - l - 10 < 0) y = l;
                        return (y - l - 10 >= 0);
                    }
                case -2:
                    {
                        if (y + l + 10 > pbH) y = pbH - l;
                        return (y + l + 10 <= pbH);
                    }
                default: return base.check_move(move, pbW, pbH);
            }
        }
    };
}