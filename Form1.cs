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
        public bool ctrlPress = false;
        Storage stCircles = new Storage(10);
        public Form1()
            {
                InitializeComponent();
            }
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            ctrlPress = e.Control;
            if (e.KeyCode == Keys.Delete) // выделенные объекты удалятся из хранилища, и произойдет перерисовка
            {
                for (int i=0; i<stCircles.get_count(); ++i)
                {
                    if (stCircles.get_el(i).get_highlighted() == true)
                    {
                        stCircles.del(i);
                        i--;
                    }
                }
                pictureBox1.Invalidate();
            }

        }
        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            ctrlPress = e.Control;
        }
        private void setAllHighlightFalse() 
        {
            // в хранилище меняем у выделенных кругов св. выделенности
            for (int i = 0; i < stCircles.get_count(); ++i)
                if (stCircles.get_el(i).get_highlighted() == true)
                    stCircles.get_el(i).change_highlight();
        }
        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                int ind = -1; // попадание по кругу с индексом ind

                // определяем попадание по существующему кругу 
                for (int i = 0; i < stCircles.get_count(); ++i)
                    if (stCircles.get_el(i).mouseClick_on_Object(e.X, e.Y))
                        ind = i;

                // не попали по кругу - убираем все выделения, создаем новый круг и считаем, что мы попали по нему
                if (ind == -1)
                {
                    setAllHighlightFalse();
                    stCircles.add(new CCircle(e.X, e.Y));
                    ind = stCircles.get_count()-1;
                }
                else
                {
                    // попали по кругу - проверяем ctrl
                    // если ctrl не зажат - убираем остальные выделения
                    if (ctrlPress != true)
                    {
                        setAllHighlightFalse();
                    }
                }
                // выделяем круг, по которому попали
                stCircles.get_el(ind).change_highlight();
                pictureBox1.Invalidate();
            }
        }
        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            for (int i = 0; i < stCircles.get_count(); ++i)
                if (stCircles.get_el(i) != null)
                    stCircles.get_el(i).draw(e);
        }
    }
    public static class Brush
    {
        public static SolidBrush violBrush = new SolidBrush(Color.FromArgb(141, 143, 240));
        public static SolidBrush pinkBrush = new SolidBrush(Color.FromArgb(219, 125, 171));
    }
}





