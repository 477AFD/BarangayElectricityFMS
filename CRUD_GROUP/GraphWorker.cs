using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Text;
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
        List<List<decimal>> listSets;
        List<int> indices;

        public Bitmap getGraph(PictureBox canvas)
        {
            return (Bitmap)canvas.Image;
        }

        public static GraphWorker MakeGraphWorker(DataTable dt) // For use in the project
        {
            List<List<decimal>> f = new List<List<decimal>>();
            List<decimal> rate = new List<decimal>();
            List<decimal> price = new List<decimal>();
            foreach (DataRow r in dt.Rows)
            {
                rate.Add(decimal.Parse(r["RatePerKWH"].ToString()));
                price.Add(decimal.Parse(r["Price"].ToString()));
            }
            f.Add(rate);
            f.Add(price);
            return new GraphWorker(f);
        }
        /// <summary>
        /// Creates a graph worker for drawing 2D graphs.
        /// </summary>
        /// <param name="lst">This is a list containing lists of doubles. It has to be of the same length, otherwise it will use the last digit as the filler.</param>
        public GraphWorker(List<List<decimal>> lst)
        {
            listSets = new List<List<decimal>>();
            indices = new List<int>();
            int lastCount = 0;
            int ii = 0;
            Debug.WriteLine("Expanding lists to highest length...");
            foreach (List<decimal> l in lst)
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
            foreach (List<decimal> d in listSets)
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
        public void DrawLine(PictureBox canvas, int index, Color color, Point? res = null, int GraphIntervalX = 10, int GraphIntervalY = 100)
        {
            Point r;
            if (res == null) r = new Point(canvas.Width, canvas.Height);
            else r = (Point)res;
            canvas.Image = new Bitmap(r.X, r.Y);
            using (Graphics g = Graphics.FromImage(canvas.Image))
            {
                List<decimal> run = listSets[index];
                g.Clear(Color.White);
                int lat = canvas.Image.Width / run.Count; // For width
                int lgt = (int)Math.Round(run.Max(), 0);
                int lgtmin = (int)Math.Round(run.Min(), 0);
                decimal min = run.Min() - 2;
                decimal max = run.Max() + 2;
                decimal cmax = canvas.Image.Height - 2;
                const decimal cmin = -2;
                //int x = 0;
                Pen t = new Pen(color, 2f);
                int ii = 0;
                int s = 0, p = 0;
                decimal lastDouble = 0.0M;
                Debug.WriteLine($"{canvas.Image.Width} {canvas.Image.Height} :: {lat} {lgt}");
                Pen gray = new Pen(Color.FromArgb(200, 200, 200));
                for (decimal ip = min; ip < max; ip += GraphIntervalY)
                {
                    int ptr = ((int)((ip - min) * (cmax - cmin) / (max - min))) + (int)cmin;
                    g.DrawLine(gray, 2, ptr, canvas.Image.Width - 2, ptr);
                }
                for (int ip = 0; ip < canvas.Image.Width; ip += GraphIntervalX)
                {
                    int ptr = lat * ip;
                    g.DrawLine(gray, ptr, 2, ptr, canvas.Image.Height - 2);
                }
                foreach (decimal value in run)
                {
                    if (ii++ != 0 && indices[index] > ii)
                    {

                        Point prev = new Point(p, canvas.Image.Height - ((int)((lastDouble - min) * (cmax - cmin) / (max - min))) + (int)cmin);
                        Point next = new Point(s, canvas.Image.Height - ((int)((value - min) * (cmax - cmin) / (max - min))) + (int)cmin);
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
        // NOTE: This method is DrawGraph() corrected by AI using the code above (without the EXT) (Google Gemini) but contains modifications.
        public void DrawLineExt(PictureBox canvas, int index, Color color, Point? res = null, int GraphIntervalX = 10, int GraphIntervalY = 100)
        {
            Point r = res ?? new Point(canvas.Width, canvas.Height);
            canvas.Image = new Bitmap(r.X, r.Y);

            using (Graphics g = Graphics.FromImage(canvas.Image))
            {
                List<decimal> run = listSets[index];
                if (run.Count < 2) return;
                g.Clear(Color.White);
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                g.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;
                const int marginLeft = 50;
                const int marginOther = 10;
                int drawWidth = canvas.Image.Width - marginLeft - marginOther;
                int drawHeight = canvas.Image.Height - (marginOther * 2);
                decimal min = run.Min();
                decimal max = run.Max();
                decimal range = (max - min == 0) ? 1 : (max - min);
                Pen gray = new Pen(Color.FromArgb(220, 220, 220), 1f);
                Pen linePen = new Pen(color, 2f);
                Font labelFont = new Font("Bahnschrift", 7f);
                Brush labelBrush = Brushes.Gray;
                decimal startVal = Math.Floor(min / GraphIntervalY) * GraphIntervalY;

                for (decimal val = startVal; val <= max + GraphIntervalY; val += GraphIntervalY)
                {
                    float yPos = (float)(marginOther + drawHeight - (((val - min) / range) * drawHeight));
                    if (yPos >= marginOther && yPos <= canvas.Image.Height - marginOther)
                    {
                        g.DrawLine(gray, marginLeft, yPos, canvas.Image.Width - marginOther, yPos);
                        int ee = (int)val;
                        string label = $"{ee:N0}";
                        SizeF textSize = g.MeasureString(label, labelFont);
                        g.DrawString(label, labelFont, labelBrush, marginLeft - textSize.Width - 5, yPos - (textSize.Height / 2));
                    }
                }
                float xStep = (float)drawWidth / (run.Count - 1);
                for (int i = 0; i < run.Count; i += GraphIntervalX)
                {
                    float xPos = marginLeft + (i * xStep);
                    g.DrawLine(gray, xPos, marginOther, xPos, canvas.Image.Height - marginOther);
                }
                for (int i = 0; i < run.Count - 1; ++i)
                {
                    float x1 = marginLeft + (i * xStep);
                    float y1 = (float)(marginOther + drawHeight - (((run[i] - min) / range) * drawHeight));
                    float x2 = marginLeft + ((i + 1) * xStep);
                    float y2 = (float)(marginOther + drawHeight - (((run[i + 1] - min) / range) * drawHeight));

                    g.DrawLine(linePen, x1, y1, x2, y2);
                }
                g.Flush();
            }
        }
        /// <summary>
        /// Creates a line graph of all data in this class.
        /// </summary>
        /// <param name="canvas">The form canvas to use.</param>
        public void DrawLine(PictureBox canvas, Point? res = null, int GraphIntervalX = 1, int GraphIntervalY = 100)
        {
            Point r;
            if (res == null) r = new Point(canvas.Width, canvas.Height);
            else r = (Point)res;
            canvas.Image = new Bitmap(r.X, r.Y);
            List<Color> colors = new List<Color>() { Color.Red, Color.Green, Color.Blue, Color.Magenta, Color.Cyan, Color.Yellow, Color.DarkGray, Color.Black, Color.Gray, };
            using (Graphics g = Graphics.FromImage(canvas.Image))
            {
                g.Clear(Color.White);
                List<decimal> allValues = new List<decimal>();
                foreach (List<decimal> d in listSets)
                    allValues.AddRange(d);
                Debug.WriteLine($"List size: {listSets.Count}");
                for (int i = 0, p = 0; i < listSets.Count; ++i, ++p)
                {
                    Debug.WriteLine($"List index: {i}");
                    if (p == colors.Count) p = 0;
                    List<decimal> run = listSets[i];
                    int lat = (canvas.Image.Width) / (run.Count); // For width
                    decimal min = allValues.Min() - 2;
                    decimal max = allValues.Max() + 2;
                    decimal cmax = canvas.Image.Height -2;
                    const decimal cmin = -2;
                    //int x = 0;
                    Pen t = new Pen(colors[p], 2f);
                    Debug.WriteLine($"{canvas.Image.Width} {canvas.Image.Height} :: {lat}");
                    int ii = 0;
                    int s = 0, w = 0;
                    decimal lastDouble = 0.0M;

                    Pen gray = new Pen(Color.FromArgb(200,200,200));
                    for (decimal ip = min; ip < max; ip += GraphIntervalY)
                    {
                        int ptr = ((int)((ip - min) * ((cmax) - (cmin)) / ((max) - (min)))) + (int)cmin;
                        Debug.WriteLine(ptr);
                        g.DrawLine(gray, 2, ptr, canvas.Image.Width - 2, ptr);
                    }
                    for (int ip = 0; ip < canvas.Image.Width; ip += GraphIntervalX)
                    {
                        int ptr = lat * ip;
                        g.DrawLine(gray, ptr, 2, ptr, canvas.Image.Height - 2);
                    }
                    int ptre = canvas.Image.Height;
                    foreach (decimal value in run)
                    {
                        Debug.WriteLine($"Value: {value}");
                        if (ii++ != 0 && indices[i] >= ii)
                        {
                            Point prev = new Point(w, ptre - ((int)((lastDouble - min) * (cmax - cmin) / (max - min))) + (int)cmin);
                            Point next = new Point(s, ptre - ((int)((value - min) * (cmax - cmin) / (max - min))) + (int)cmin);
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
        public void DrawLineExt(PictureBox canvas, Point? res = null, int GraphIntervalX = 1, int GraphIntervalY = 100)
        {
            Point r = res ?? new Point(canvas.Width, canvas.Height);
            canvas.Image = new Bitmap(r.X, r.Y);
            List<Color> colors = new List<Color>() { Color.Red, Color.Green, Color.Blue, Color.Magenta, Color.Cyan, Color.Yellow, Color.DarkGray, Color.Black, Color.Gray };
            using (Graphics g = Graphics.FromImage(canvas.Image))
            {
                g.Clear(Color.White);
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                const int margin = 6;
                int drawWidth = canvas.Image.Width - (margin * 2);
                int drawHeight = canvas.Image.Height - (margin * 2);
                var allValues = listSets.SelectMany(x => x).ToList();
                if (allValues.Count == 0) return;
                decimal globalMin = allValues.Min();
                decimal globalMax = allValues.Max();
                decimal range = (globalMax - globalMin == 0) ? 1 : (globalMax - globalMin);
                Pen gray = new Pen(Color.FromArgb(220, 220, 220), 1f);
                for (decimal val = globalMin; val <= globalMax; val += GraphIntervalY)
                {
                    float yPos = (float)(margin + drawHeight - (((val - globalMin) / range) * drawHeight));
                    g.DrawLine(gray, margin, yPos, canvas.Image.Width - margin, yPos);
                }
                for (int i = 0; i < listSets.Count; ++i)
                {
                    List<decimal> run = listSets[i];
                    if (run.Count < 2) continue;
                    Color lineColor = colors[i % colors.Count];
                    Pen t = new Pen(lineColor, 2f);
                    float xStep = (float)drawWidth / (run.Count - 1);

                    for (int j = 0; j < run.Count - 1; j++)
                    {
                        float x1 = margin + (j * xStep);
                        float y1 = (float)(margin + drawHeight - ((run[j] - globalMin) / range * drawHeight));
                        float x2 = margin + ((j + 1) * xStep);
                        float y2 = (float)(margin + drawHeight - ((run[j + 1] - globalMin) / range * drawHeight));
                        g.DrawLine(t, x1, y1, x2, y2);
                    }
                }
                g.Flush();
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
