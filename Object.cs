using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace Lab6_OOP
{
    public class CObject
    {
        protected bool highlighted = false;
        protected Color color;
        protected int x, y;
        public CObject() { }
        public CObject(int x, int y, Color color)
        {
            this.x = x;
            this.y = y;
            this.color = color;
        }
        public virtual void draw(PaintEventArgs e)
        {
            e.Graphics.DrawLine(Brush.highlightPen, x, y, x + 1, y + 1);
        }
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
        public virtual CObject new_obj(int x, int y, Color c)
        {
            return new CObject();
        }
        public virtual void resize(bool inc, int pbW, int pbH) { }
        public virtual bool check_move(int move, int pbW, int pbH)
        {
            switch (move)
            {
                case 1:
                    {
                        if (x + 10 > pbW) x = pbW;
                        return (x + 10 <= pbW);
                    }
                case -1:
                    {
                        if (x - 10 < 0) x = 0;
                        return (x - 10 >= 0);
                    }
                case 2:
                    {
                        if (y - 10 < 0) y = 0;
                        return (y - 10 >= 0);
                    }
                case -2:
                    {
                        if (y + 10 > pbH) y = pbH;
                        return (y + 10 <= pbH);
                    }
                default: return false;
            }
        }
        public virtual int check_move2(int move, int pbW, int pbH, int d)
        {
            switch (move)
            {
                case 1: return Math.Min(pbW - x, d);
                case -1: return Math.Min(x, d);
                case 2: return Math.Min(y, d);
                case -2: return Math.Min(pbH - y, d);
                default: return 0;
            }
        }
        public virtual void move(int move, int pbW, int pbH)
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
        public int get_x()
        {
            return x;
        }
        public int get_y()
        {
            return y;
        }
        public void set_x(int new_x) // переделать 
        {
            x = new_x;
        }
        public void set_y(int new_y)
        {
            y = new_y;
        }
        public void move_x(int dx) // переделать 
        {
            x += dx;
        }
        public void move_y(int dy)
        {
            y += dy;
        }
    }

    public class CRectangle : CObject
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
        public override CObject new_obj(int x, int y, Color color)
        {
            return new CRectangle(x, y, color);
        }
        public override bool check_move(int move, int pbW, int pbH)
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
                if (w != h)
                    //h = h + delt_size *h/w;
                    //float delt_siz2 = (float)delt_size * h / ((float)w);
                    h += (int)((float)delt_size * h / ((float)w));
                else h += delt_size;
                w += delt_size;
            }
            else if (h > 20 && w > 20)
            {
                if (w != h)
                {
                    w -= 10; h = h - (int)((float)10 * h / ((float)w));
                }
                else { w -= 10; h -= 10; }
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
        public override CObject new_obj(int x, int y, Color color)
        {
            return new CSquare(x, y, color);
        }
    }
    public class CEllipse : CRectangle
    {
        public CEllipse(int x, int y, Color color) : base(x, y, color) { }
        public override void draw(PaintEventArgs e)
        {
            Brush.normBrush.Color = color;
            if (highlighted == false)
                e.Graphics.FillEllipse(Brush.normBrush, x - w / 2, y - h / 2, w, h);
            else e.Graphics.FillEllipse(Brush.highlightBrush, x - w / 2, y - h / 2, w, h);
        }
        public override bool mouseClick_on_Object(int x_, int y_)
        {
            if (((x_ - x) * (x_ - x) * (h * h) + (y_ - y) * (y_ - y) * (w * w)) * 4 <= w * w * h * h && base.mouseClick_on_Object(x_, y_))
                return true;
            else return false;
        }
        public override string classname() { return "CEllipse"; }
        public override CObject new_obj(int x, int y, Color color)
        {
            return new CEllipse(x, y, color);
        }
    }
    public class CCircle : CEllipse
    {
        public CCircle(int x, int y, Color color) : base(x, y, color)
        {
            w = 50;
        }
        public override string classname() { return "CCircle"; }
        public override CObject new_obj(int x, int y, Color color)
        {
            return new CCircle(x, y, color);
        }
    };
    public class CTriangle : CSquare
    {
        public CTriangle(int x, int y, Color color) : base(x, y, color) { }

        public override void draw(PaintEventArgs e)
        {
            Brush.normBrush.Color = color;
            Point[] arrPoints = { new Point(x, y - w / 2), new Point(x + w / 2, y + w / 2), new Point(x - w / 2, y + w / 2) };
            if (highlighted == false)
                e.Graphics.FillPolygon(Brush.normBrush, arrPoints);
            else e.Graphics.FillPolygon(Brush.highlightBrush, arrPoints);
        }
        public override bool mouseClick_on_Object(int x_, int y_)
        {
            int dy = y_ - y;
            int dx = x_ - x;
            if (dy <= w / 2 && dy >= 2 * dx - w / 2 && dy >= -2 * dx - w / 2)
                return true;
            else return false;
        }
        public override string classname() { return "CTriangle"; }
        public override CObject new_obj(int x, int y, Color color)
        {
            return new CTriangle(x, y, color);
        }
    };
    public class CRhomb : CRectangle
    {
        public CRhomb(int x, int y, Color color) : base(x, y, color)
        {
            h = 70; w = 35;
        }
        public override void draw(PaintEventArgs e)
        {
            Brush.normBrush.Color = color;
            Point[] arrPoints = { new Point(x - w / 2, y), new Point(x, y - h / 2), new Point(x + w / 2, y), new Point(x, y + h / 2) };
            if (highlighted == false)
                e.Graphics.FillPolygon(Brush.normBrush, arrPoints);
            else e.Graphics.FillPolygon(Brush.highlightBrush, arrPoints);
        }
        public override bool mouseClick_on_Object(int x_, int y_)
        {
            int dy = y_ - y;
            int dx = x_ - x;
            if (dy >= 2 * dx - h / 2 && dy >= -2 * dx - h / 2 && dy <= 2 * dx + h / 2 && dy <= -2 * dx + h / 2)
                return true;
            else return false;
        }
        public override void resize(bool inc, int pbW, int pbH)
        {
            if (inc == true)
            {
                int delt_size = check_resize(inc, pbW, pbH);
                if (w != h)
                    w += delt_size / 2;
                else w += delt_size;
                h += delt_size;
            }
            else if (h > 10)
            {
                w -= 5; h -= 10;
            }
        }
        public override string classname() { return "CRhomb"; }
        public override CObject new_obj(int x, int y, Color color)
        {
            return new CRhomb(x, y, color);
        }
    }

    public class CTrapeze : CRectangle
    {
        public CTrapeze(int x, int y, Color color) : base(x, y, color) { }
        Point[] get_arrPoints()
        {
            return new Point[] {
                new Point(x - w / 2, y + h / 2), new Point(x - w / 4, y - h / 2),
                new Point(x + w / 4, y - h / 2), new Point(x + w / 2, y + h / 2) };
        }
        public override void draw(PaintEventArgs e)
        {
            Brush.normBrush.Color = color;
            if (highlighted == false)
            {
                e.Graphics.FillPolygon(Brush.normBrush, get_arrPoints());
            }
            else
            {
                e.Graphics.FillPolygon(Brush.highlightBrush, get_arrPoints());
            }
        }
        public override bool mouseClick_on_Object(int x_, int y_)
        {
            System.Drawing.Drawing2D.GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath();
            path.AddPolygon(get_arrPoints());
            Region rgn = new Region(path);
            return (rgn.IsVisible(x_, y_) == true);
        }
        public override string classname() { return "CTrapeze"; }
        public override CObject new_obj(int x, int y, Color color)
        {
            return new CTrapeze(x, y, color);
        }
    }
    public class CLine : CObject
    {
        private CObject Point1;
        public CLine(CObject point_st, int x, int y, Color color) : base(x, y, color)
        {
            Point1 = point_st;
            Point1.set_color(color);
        }
        public override string classname() { return "CLine"; }
        public override void draw(PaintEventArgs e)
        {
            Brush.normPen.Color = color;
            if (highlighted == false)
                e.Graphics.DrawLine(Brush.normPen, Point1.get_x(), Point1.get_y(), x, y);
            else e.Graphics.DrawLine(Brush.highlightPen, Point1.get_x(), Point1.get_y(), x, y);
        }
        public override bool mouseClick_on_Object(int x_, int y_)
        {
            int x1 = Point1.get_x();
            int y1 = Point1.get_y();
            System.Drawing.Drawing2D.GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath();
            path.AddPolygon(new Point[] { new Point(x1 + 2, y1 - 2), new Point(x1 - 2, y1 + 2), new Point(x - 2, y + 2), new Point(x + 2, y - 2) });
            Region rgn = new Region(path);
            return (rgn.IsVisible(x_, y_) == true);
        }
        public override void move(int move, int pbW, int pbH)
        {
            int d = check_move2(move, pbW, pbH, 10);
            int d1 = Point1.check_move2(move, pbW, pbH, 10);
            if (d >= 0 && d1 >= 0)
            {
                int d_ = Math.Min(d, d1);
                switch (move)
                {
                    case 1: { x += d_; Point1.move_x(d_); break; }
                    case -1: { x -= d_; Point1.move_x(-d_); break; }
                    case 2: { y -= d_; Point1.move_y(-d_); break; }
                    case -2: { y += d_; Point1.move_y(d_); break; }
                    default: break;
                }
            }
        }
        protected override int check_resize(bool inc, int pbW, int pbH)
        {
            if (x == 0 || Point1.get_x() == 0 || x == pbW || Point1.get_x() == pbW
                || y == 0 || Point1.get_y() == 0 || y == pbH || Point1.get_y() == pbH)
                return 0;
            else return 1;
        }
        public override void resize(bool inc, int pbW, int pbH)
        {
            //if (x >= Point1.get_x()) {
            //    int d = check_move2(1, pbW, pbH, 5);
            //    int d1 = Point1.check_move2(-1, pbW, pbH, 5);
            //}
            //else
            //{
            //    int d = check_move2(-1, pbW, pbH, 5);
            //    int d1 = Point1.check_move2(1, pbW, pbH, 5);
            //}
            ////int d1 = Point1.check_move2(move, pbW, pbH, 5);
            //if (d >= 0 && d1 >= 0)
            //{
            //    int d_ = Math.Min(d, d1);
            //    switch (move)
            //    {
            //        case 1: { x += d_; Point1.set_x(d_); break; }
            //        case -1: { x -= d_; Point1.set_x(-d_); break; }
            //        case 2: { y -= d_; Point1.set_y(-d_); break; }
            //        case -2: { y += d_; Point1.set_y(d_); break; }
            //        default: break;
            //    }
            //}
            if (inc == true && check_resize(inc, pbW, pbH) == 1)
            {
                // для простоты представляем отрезок диагональю прямоугольника, который хотим увеличить 
                //int w = Math.Abs(x - Point1.get_x());
                //int h = Math.Abs(y - Point1.get_y());
                //int x0 = Math.Min(x, Point1.get_x());
                //int y0 = Math.Min(y, Point1.get_y());
                //// находим возможное на поле смещение точек dh и dw
                //int dw, dh;
                //if (x0 + w + 10 <= pbW) dw = 10;
                //else dw = pbW - w;
                //float d10 = (float)10 * h / ((float)w); // для пропорционального увел-ия прям-ка
                //if (y0 - d10 >= 0) dh = (int)d10;
                //else dh = (y0);
                //if (x0 - dw < 0) dw = x0;
                //if (y0 + h + dh >= pbH) dh = pbH - y0 - h;
                //// присваиваем новые координаты точкам
                //if (x == x0) { x -= dw; Point1.set_x(dw); }
                //else { x += dw; Point1.set_x(-dw); }
                //if (y == y0) { y -= dh; Point1.set_y(dh); }
                //else { y += dh; Point1.set_y(-dh); }

                int w = Math.Abs(x - Point1.get_x());
                int h = Math.Abs(y - Point1.get_y());
                int x0 = Math.Min(x, Point1.get_x());
                int y0 = Math.Min(y, Point1.get_y());
                Rectangle rect = new Rectangle(x0, y0, w, h);
                //rect.Inflate(10, (int)(10 * h / (float)w));
                Size inflateSize = new Size(50, 50);
                rect.Inflate(inflateSize);
                if (x == x0) { x = rect.X; Point1.set_x(rect.Right); }
                else { x = rect.Right; Point1.set_x(rect.X); }
                if (y == y0) { y = rect.Y; Point1.set_y(rect.Bottom); }
                else { y = rect.Bottom; Point1.set_y(rect.Y); }
            }
        }
    }
}