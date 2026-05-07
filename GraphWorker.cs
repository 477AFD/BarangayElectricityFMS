using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ProgressBar;

namespace GraphingTests
{
    internal class GraphWorker
    {
        List<List<double>> listSets;
        List<int> indices;
        /// <summary>
        /// Creates a graph worker for drawing 2D graphs.
        /// </summary>
        /// <param name="lst">This is a list containing lists of doubles. It has to be of the same length, otherwise it will use the last digit as the filler.</param>
        public GraphWorker(List<List<double>> lst)
        {
            listSets = new List<List<double>>();
            indices = new List<int>();
            int lastCount = 0;
            int ii = 0;
            Debug.WriteLine("Expanding lists to highest length...");
            foreach (List<double> l in lst)
            {
                indices.Add(l.Count);
                if (ii++ != 0)
                {
                    if (l.Count < lastCount)
                    {
                        Debug.WriteLine($"Expanding last list {ii + 1}");
                        for (int i = l.Count; i < lastCount; ++i) l.Add(l[l.Count - 1]);
                    }
                    else if (l.Count > lastCount) 
                    {
                        Debug.WriteLine("List is larger than previous lists. Expanding previous lists...");
                        int t = l.Count;
                        for (int i = 0; i < listSets.Count; ++i)
                        {
                            Debug.WriteLine($"Expanding list {i + 1}");
                            for (int j = listSets[i].Count; j < t; ++j) listSets[i].Add(listSets[i][listSets[i].Count - 1]);
                        }
                    }
                }
                lastCount = l.Count;
                Debug.WriteLine($"Count {ii}: {l.Count}");
                listSets.Add(l);
            }
            lastCount = 0; ii = 0;
            Debug.WriteLine("Checking length...");
            foreach (List<double> d in listSets)
            {
                if (ii++ != 0)
                {
                    if (d.Count != lastCount) 
                    { 
                        Debug.WriteLine($"MISMATCH: List has {d.Count}, last one is {lastCount}"); 
                        throw new DataMisalignedException($"MISMATCH: List has {d.Count}, last one is {lastCount}"); 
                    }
                } 
                lastCount = d.Count;
                string r = "";
                foreach (double s in d) r += $"{s} ";
                Debug.WriteLine($"Count {ii}: {d.Count} :> {r}");
            }
        }

        public int MarginX { get; set; } = 10;
        public int MarginY { get; set; } = 10;
        /// <summary>
        /// Creates a single line graph.
        /// </summary>
        /// <param name="canvas">The form canvas to use.</param>
        /// <param name="index"></param>
        public void DrawLine(PictureBox canvas, int index, Color color, Point? res = null, int GraphIntervalX = 10, int GraphIntervalY = 10)
        {
            Point r;
            
            
                if (res == null) r = new Point(canvas.Width, canvas.Height);
                else r = (Point)res;
                canvas.Image = new Bitmap(r.X, r.Y);
            
            using (Graphics g = Graphics.FromImage(canvas.Image))
            {
                List<double> run = listSets[index];
                g.Clear(Color.White);
                int lat = canvas.Image.Width / (run.Count-2); // For width
                int lgt = (int)Math.Round(run.Max(), 0);
                int lgtmin = (int)Math.Round(run.Min(), 0);
                double min = run.Min() - 2;
                double max = run.Max() + 2;
                double cmax = canvas.Image.Height - 2;
                const double cmin = 2;
                //int x = 0;
                Pen t = new Pen(color, 2f);
                int ii = 0;
                int s = 0, p = 0;
                double lastDouble = 0.0;
                Debug.WriteLine($"{canvas.Image.Width} {canvas.Image.Height} :: {lat} {lgt}");
                Pen gray = new Pen(Color.FromArgb(200, 200, 200));
                for (int i = 2; i < canvas.Image.Height; i += GraphIntervalY)
                {
                    int ptr = ((int)((i - min) * (cmax - cmin) / (max - min))) + (int)min;
                    g.DrawLine(gray, 2, ptr, canvas.Image.Width - 2, ptr);
                }
                for (int ip = 0; ip < canvas.Image.Width; ip += GraphIntervalX)
                {
                    int ptr = lat * ip;
                    g.DrawLine(gray, ptr, 2, ptr, canvas.Image.Height - 2);
                }
                foreach (double value in run)
                {
                    if (ii++ != 0 && indices[index] > ii)
                    {

                        Point prev = new Point(p, canvas.Image.Height - ((int)((lastDouble - min) * (cmax - cmin) / (max - min))) + (int)min);
                        Point next = new Point(s, canvas.Image.Height - ((int)((value - min) * (cmax - cmin) / (max - min))) + (int)min);
                        p += lat;
                        Debug.WriteLine($"Point {ii - 1}: [{prev.X} {prev.Y}] .. [{next.X} {next.Y}]");
                        g.DrawLine(t, prev, next);
                        
                    }
                    s += lat;
                    lastDouble = value;
                }
                g.Flush();
            }
        }
        /// <summary>
        /// Creates a line graph of all data in this class.
        /// </summary>
        /// <param name="canvas">The form canvas to use.</param>
        public void DrawLine(PictureBox canvas, Point? res = null, int GraphIntervalX = 1, int GraphIntervalY = 10)
        {
            Point r;
            if (res == null) r = new Point(canvas.Width, canvas.Height);
            else r = (Point)res;
            canvas.Image = new Bitmap(r.X, r.Y);
            List<Color> colors = new List<Color>() { Color.Red, Color.Green, Color.Blue, Color.Magenta, Color.Cyan, Color.Yellow, Color.DarkGray, Color.Black, Color.Gray, };
            using (Graphics g = Graphics.FromImage(canvas.Image))
            {
                g.Clear(Color.White);
                List<double> allValues = new List<double>();
                foreach (List<double> d in listSets)
                    allValues.AddRange(d);
                for (int i = 0, p = 0; i < listSets.Count; ++i, ++p)
                {
                    if (p == colors.Count) p = 0;
                    List<double> run = listSets[i];
                    int lat = (canvas.Image.Width) / (run.Count - 2); // For width
                    double min = allValues.Min() - 2;
                    double max = allValues.Max() + 2;
                    double cmax = canvas.Image.Height - 2;
                    const double cmin = 2;
                    //int x = 0;
                    Pen t = new Pen(colors[p], 2f);
                    Debug.WriteLine($"{canvas.Image.Width} {canvas.Image.Height} :: {lat}");
                    int ii = 0;
                    int s = 0, w = 0;
                    double lastDouble = 0.0;

                    Pen gray = new Pen(Color.FromArgb(200,200,200));
                    for (int ip = 2; ip < canvas.Image.Height; ip += GraphIntervalY)
                    {
                        int ptr = ((int)((ip - min) * ((cmax) - (cmin)) / ((max) - (min)))) + (int)min;
                        g.DrawLine(gray, 2, ptr, canvas.Image.Width - 2, ptr);
                    }
                    for (int ip = 0; ip < canvas.Image.Width; ip += GraphIntervalX)
                    {
                        int ptr = lat * ip;
                        g.DrawLine(gray, ptr, 2, ptr, canvas.Image.Height - 2);
                    }
                    int ptre = canvas.Image.Height;
                    foreach (double value in run)
                    {
                        if (ii++ != 0 && indices[i] > ii)
                        {
                            Point prev = new Point(w, ptre - ((int)((lastDouble - min) * (cmax - cmin) / (max - min))) + (int)min);
                            Point next = new Point(s, ptre - ((int)((value - min) * (cmax - cmin) / (max - min))) + (int)min);
                            w += lat;
                            Debug.WriteLine($"Point {ii - 1}: [{prev.X} {prev.Y}] .. [{next.X} {next.Y}]");
                            g.DrawLine(t, prev, next);
                        }
                        s += lat;
                        lastDouble = value;
                    }
                }
            }
        }

        static double Map(double value, double fromSource, double toSource, double fromTarget, double toTarget)
        {
            return (value - fromSource) / (toSource - fromSource) * (toTarget - fromTarget) + fromTarget;
        }

        //public void DrawBar(PictureBox canvas, Point? res = null, int width = 10, int spacing = 5)
        //{
        //    Point r;
        //    if (res == null) r = new Point(canvas.Width, canvas.Height);
        //    else r = (Point)res;
        //    canvas.Image = new Bitmap(r.X, r.Y);
        //    List<Color> colors = new List<Color>() { Color.Red, Color.Green, Color.Blue, Color.Magenta, Color.Cyan, Color.Yellow, Color.DarkGray, Color.Black, Color.Gray, };
        //    using (Graphics g = Graphics.FromImage(canvas.Image))
        //    {
        //        g.Clear(Color.White);
        //        List<double> allValues = new List<double>();
        //        foreach (List<double> d in listSets) allValues.AddRange(d);
        //        // Define actual width per box, starting from 0
        //        int w = listSets.Count;
        //        int h = width / w;
        //        int e = 0;
        //        int q = (width * w - w) + spacing;
        //        for (int i = 0, p = 0; i < listSets.Count; i++, p++)
        //        {
        //            if (p == colors.Count) p = 0;
        //            List<double> run = listSets[i];
        //            int lat = canvas.Image.Width / run.Count; // For width
        //            double min = allValues.Min() - 2;
        //            double max = allValues.Max() + 2;
        //            double cmax = canvas.Image.Height - 2;
        //            const double cmin = 2;
        //            //int x = 0;
        //            Pen t = new Pen(colors[p]);
        //            for (int s = 0; s < run.Count; s++)
        //            {
        //                g.DrawRectangle(t, new Rectangle(new Point(cmax, )))
        //            }
        //        }
        //    }
        //}
    }
}
