

namespace SimpleMandlebrot;

internal class Mandelbrot : Form
{
    Label canvas;
    Bitmap image;
    (double x, double y) center = new();
    double scale = 1;
    
    public Mandelbrot()
    {
        this.Size = new(800, 600);
        canvas = new Label();
        canvas.Size = new(600, 400);
        canvas.Location = new(50,50);
        image = new(canvas.Size.Width, canvas.Size.Height);

        canvas.Paint += Draw;
        canvas.MouseDown += Button;
        Controls.Add(canvas);

    }

    void Draw(object o, PaintEventArgs pea)
    {
        for (int x = 0; x < image.Width; x++)
        {
            for (int y = 0; y < image.Height; y++)
            {
                image.SetPixel(x, y, Mand(x, y, 200));
            }
        }
        pea.Graphics.DrawImageUnscaled(image, new Point(0, 0));
    }

    void Button(object o, MouseEventArgs mea)
    {
        
        center = MandCoördinate(mea.X, mea.Y);
        if (mea.Button == MouseButtons.Left)
        {
            scale /= 2;
        }
        if (mea.Button == MouseButtons.Right)
        {
            scale *= 2; 
        }

        canvas.Invalidate();
        
    }

    (double, double) MandCoördinate (int x, int y)
    {
        double sx = scale * (4 * x - 2 * image.Width) / image.Width + center.x;
        double sy = scale * (4 * y - 2 * image.Height) / image.Width + center.y;


        return (sx, sy);
    }

    Color Mand(int x, int y, int max)
    {

        
        int mandnum = MandNum(MandCoördinate(x,y), max);


        if (mandnum % 2 == 0) 
        {
            return Color.Black;
        }
        else
        {
            return Color.White;
        }
    }



    int MandNum((double x, double y) n, int max)
    {
        double real = 0;
        double imaginary = 0;
        for (int i = 0; i < max; i++)
        {
            (real, imaginary) = (real * real - imaginary * imaginary + n.x, 2 * real * imaginary + n.y);

            if (real*real + imaginary*imaginary > 4)
            {
                return i;
            }
        }
        return max;
    }






}
