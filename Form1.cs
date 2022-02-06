using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;

namespace Lab6_OOP
{
    public partial class Form1 : Form
    {
        public bool ctrlPress = false; // для выделения нескольких объектов
        public bool shiftPress = false; // для увеличения размера (shift +)
        static Color c = Color.White;
        Storage storObj = new Storage(10);
        СObject[] ObjList = 
            {new CCircle(0,0,c),
            new CTriangle(0,0,c),
            new CRectangle(0,0, c),
            new CSquare(0,0, c),
            new CEllipse(0,0,c)
        };
        string cur_select = "CCircle"; // текущий выбор фигуры, которая будет создаваться при нажатии на пустое место 
        public Form1()
            {
                InitializeComponent();
                this.KeyPreview = true;
        }
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            ctrlPress = e.Control;
            shiftPress = e.Shift;
            if (e.KeyCode == Keys.Delete) // выделенные объекты удалятся из хранилища, и произойдет перерисовка
            {
                for (int i=0; i<storObj.get_count(); ++i)
                {
                    if (storObj.get_el(i).get_highlighted() == true)
                    {
                        storObj.del(i);
                        i--;
                    }
                }
                pictureBox1.Invalidate();
            }
            // увеличение размера объектов 
            if (e.KeyCode == Keys.Oemplus && shiftPress == true)
            {
                for (int i = 0; i < storObj.get_count(); ++i)
                    if (storObj.get_el(i).get_highlighted() == true)
                        storObj.get_el(i).resize(true);
                pictureBox1.Invalidate();
            }
            // уменьшение размера объектов
            if (e.KeyCode == Keys.OemMinus)
            {
                for (int i = 0; i < storObj.get_count(); ++i)
                    if (storObj.get_el(i).get_highlighted() == true)
                        storObj.get_el(i).resize(false);
                pictureBox1.Invalidate();
            }
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            ctrlPress = e.Control;
            shiftPress = e.Shift;
        }
        private void setAllHighlightFalse() 
        {
            // в хранилище меняем у выделенных объектов св. выделенности
            for (int i = 0; i < storObj.get_count(); ++i)
                if (storObj.get_el(i).get_highlighted() == true)
                    storObj.get_el(i).change_highlight();
        }
        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                int ind = -1; // попадание по кругу с индексом ind

                // определяем попадание по существующему объекту 
                for (int i = 0; i < storObj.get_count(); ++i)
                    if (storObj.get_el(i).mouseClick_on_Object(e.X, e.Y))
                        ind = i;

                // не попали по объекту 
                if (ind == -1)
                {
                    // убираем все выделения
                    setAllHighlightFalse();

                    // создаем новый объект
                    СObject newObj = createObj();
                    newObj = newObj.new_obj(e.X, e.Y,Brush.normBrush.Color);
                    storObj.add(newObj);

                    //считаем, что мы попали по нему
                    ind = storObj.get_count()-1;
                }
                else
                {
                    // попали по сущ-му объекту - проверяем ctrl
                    // если ctrl не зажат - убираем остальные выделения
                    if (ctrlPress != true)
                    {
                        setAllHighlightFalse();
                    }
                }
                // выделяем круг, по которому попали
                storObj.get_el(ind).change_highlight();
                pictureBox1.Invalidate();
            }
        }
        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            for (int i = 0; i < storObj.get_count(); ++i)
                if (storObj.get_el(i) != null)
                    storObj.get_el(i).draw(e);
        }
        private СObject createObj()
        {
            for (int i=0; i < ObjList.Length; ++i)
            {
                if (ObjList[i].classname() == cur_select)
                    return ObjList[i];
            }
            return new CCircle(0,0,c);
        }

        private void btn_color_Click(object sender, EventArgs e)
        {
            Color new_color = ((Button)sender).BackColor;
            // у выделенных объектов меняем цвет
            for (int i = 0; i < storObj.get_count(); ++i)
                if (storObj.get_el(i).get_highlighted() == true)
                    storObj.get_el(i).set_color(new_color);
            // меняем текущий цвет, используемый при рисовании новых фигур
            Brush.normBrush.Color = new_color;
        }

        private void btn_other_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                Brush.normBrush.Color = colorDialog1.Color;
                //((Button)sender).BackColor = Brush.normBrush.Color;
            }
        }
        private void btn_shape_Click(object sender, EventArgs e)
        {
            cur_select = ((Button)sender).Name.ToString();
        }
    }
    public static class Brush
    {
        public static SolidBrush normBrush = new SolidBrush(Color.LightPink);
        public static SolidBrush highlightBrush = new SolidBrush(Color.Red);
    }
}





