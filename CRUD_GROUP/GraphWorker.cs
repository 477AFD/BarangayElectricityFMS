using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Text;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;

namespace GraphingTests
{
    internal class GraphWorker
    {
        #region Variables
        List<List<decimal>> listSets;
        List<int> indices;
        #endregion

        #region Graph Worker maker
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
        #endregion

        #region Constructor
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
        #endregion

        #region Properties
        /// <summary>
        /// This read-only property represents the index in which a bar in a bar graph or point in a line graph is clicked.
        /// </summary>
        public int ClickedIndex { get; private set; } = -1;
        /// <summary>
        /// Sets the type of graph.<br />
        /// Less than or equal to 1 = bar graph<br />
        /// 2 or higher = line graph
        /// </summary>
        public int GraphType { get; set; } = 1;
        #endregion

        #region Graph compositor
        /// <summary>
        /// Draws a line graph or a bar graph.
        /// </summary>
        /// <param name="canvas">The canvas target to use.</param>
        /// <param name="index">The graph index to use.</param>
        /// <param name="color">The color to use as a foreground color for the rectangle or line.</param>
        /// <param name="res">The screen resolution to use. Currently uses a Point class.</param>
        /// <param name="GraphIntervalX">The graph interval width to use. Default is 10. Changing it is not recommended.</param>
        /// <param name="GraphIntervalY">The graph interval per height to use. Currently deprecated, and it was replaced by a built-in automated interval selector.</param>
        /// <param name="isSnapshot">This indicates if it is a snapshot. If true, it renders at 8K (7680x4320) resolution. Enabling print mode overrides this setting.</param>
        /// <param name="click">This indicates which point on the graph is clicked.</param>
        /// <param name="doubleClick">This indicates if it was double clicked. Currently provides no functionality.</param>
        /// <param name="isPrint">This indicates if it is used in printing. If true, it renders in paper mode at 1080p.</param>
        public void DrawLineExt(PictureBox canvas, int index, Color color, Point? res = null, int GraphIntervalX = 10, int GraphIntervalY = 100, bool isSnapshot = false, Point? click = null, bool doubleClick = false, bool isPrint = false)
        {
            Point r = res ?? new Point(canvas.Width, canvas.Height);
            canvas.Image = (isPrint) ? new Bitmap(1920, 1080) : (!isSnapshot) ? new Bitmap(r.X, r.Y) : new Bitmap(7680, 4320);
             
            using (Graphics g = Graphics.FromImage(canvas.Image))
            {
                List<decimal> run = listSets[index];
                if (run.Count < 2) return;
                if (!isPrint) g.Clear(Color.FromArgb(32,50,32));
                else g.Clear(Color.White);
                g.SmoothingMode = SmoothingMode.AntiAlias;
                g.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;
                int marginLeft = (isPrint) ? 120 : (isSnapshot) ? 233 : 50;
                int marginOther = (isPrint) ? 20 : (isSnapshot) ? 15 : 10;
                int drawWidth = canvas.Image.Width - marginLeft - marginOther;
                int drawHeight = canvas.Image.Height - (marginOther * 2);
                decimal x = run.Max();
                // Get suitable interval depending on the largest item in a list
                if (x < 100) GraphIntervalY = 1;
                else if (x >= 100 && x < 250) GraphIntervalY = 5;
                else if (x >= 250 && x < 500) GraphIntervalY = 10;
                else if (x >= 500 && x < 1000) GraphIntervalY = 50;
                else if (x >= 1000 && x < 5000) GraphIntervalY = 250;
                else if (x >= 5000 && x < 10000) GraphIntervalY = 500;
                else if (x >= 10000 && x < 15000) GraphIntervalY = 1000;
                else if (x >= 15000 && x < 30000) GraphIntervalY = 2500;
                else if (x >= 30000 && x < 50000) GraphIntervalY = 5000;
                else if (x >= 50000 && x < 100000) GraphIntervalY = 10000;
                else GraphIntervalY = 25000;
                decimal min = (GraphType >= 2) ? run.Min() : run.Min() - GraphIntervalY;
                decimal max = run.Max();
                decimal range = (max - min == 0) ? 1 : (max - min);
                Color dfcolor = (isPrint) ? Color.FromArgb(220, 220, 220) : Color.FromArgb(0, 0, 0);
                Pen gray = (!isSnapshot) ? new Pen(dfcolor, 1f) : new Pen(dfcolor, 4f);
                Pen linePen = (!isSnapshot) ? new Pen(color, 2f) : new Pen(color, 10f);
                Font labelFont = (isPrint) ? new Font("Arial", 20f, FontStyle.Bold) : (!isSnapshot) ? new Font("Bahnschrift", 7f) :  new Font("Arial", 35f);
                Brush labelBrush = (isPrint) ? Brushes.DarkGray : Brushes.White;
                Pen lightPen = new Pen(Color.FromArgb(192, 255, 255, 255));
                Pen darkPen = new Pen(Color.FromArgb(192, 0, 0, 0));
                decimal startVal = Math.Floor(min / GraphIntervalY) * GraphIntervalY;
                SolidBrush barPen = new SolidBrush(color);
                // Draw horizontal gridlines and units per line
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
                int count = (GraphType < 2) ? run.Count : run.Count - 1;
                int pcount = (GraphType < 2) ? run.Count + 1 : run.Count;
                float xStep = (float)drawWidth / (count);
                // Draw vertical lines in intervals of list count (run.Count)
                for (int i = 0; i < pcount; i += GraphIntervalX)
                {
                    float xPos = marginLeft + (i * xStep);
                    g.DrawLine(gray, xPos, marginOther, xPos, canvas.Image.Height - marginOther);
                }
                List<KeyValuePair<Point, int>> ptrs = new List<KeyValuePair<Point, int>>(); // Stores the point and its index
                // Draw graph
                for (int i = 0; i < count; ++i)
                {
                    float x1 = marginLeft + (i * xStep);
                    float y1 = (float)(marginOther + drawHeight - (((run[i] - min) / range) * drawHeight));
                    float x2 = marginLeft + ((i + 1) * xStep);
                    float y2 = 0;
                    if (GraphType >= 2) y2 = (float)(marginOther + drawHeight - (((run[i + 1] - min) / range) * drawHeight));
                    if (i == 0) ptrs.Add(new KeyValuePair<Point, int>(new Point((int)x1, (int)y1), i));
                    ptrs.Add(new KeyValuePair<Point, int>(new Point((int)x2, (int)y2), i + 1));
                    if (GraphType >= 2)
                    {
                        // x1, y1 = previous point
                        // x2, y2 = next point
                        g.DrawLine(linePen, x1, y1, x2, y2);
                        if (i == 0) ptrs.Add(new KeyValuePair<Point, int>(new Point((int)x1, (int)y1), i));
                        ptrs.Add(new KeyValuePair<Point, int>(new Point((int)x2, (int)y2), i + 1));
                    } else
                    {
                        Rectangle rectangle = new Rectangle((int)x1 + 1, (int)y1 + 1, (int)(x2-x1) - 1, (int)(drawHeight - y1 + marginOther) - 1);
                        Point shine = new Point((int)x1 + 1, (int)y1 + 1);
                        Point shineX = new Point((int)x2 - 1, (int)y1 + 1);
                        Point shineY = new Point((int)x1 + 1, canvas.Image.Height - marginOther);
                        Point shineNeg = new Point((int)x2 - 1, canvas.Image.Height - marginOther);
                        g.FillRectangle(barPen, rectangle);
                        g.DrawLine(lightPen, shine, shineX);
                        g.DrawLine(lightPen, shine, shineY);
                        g.DrawLine(darkPen, shineNeg, shineX);
                        g.DrawLine(darkPen, shineNeg, shineY);
                        ptrs.Add(new KeyValuePair<Point, int>(new Point((int)((x2 + x1) / 2), (int)y1), i));
                    }
                }
                Pen crosshair = new Pen(Color.FromArgb(77,0,0,0), 1.666667f);
                Pen target = new Pen(Color.FromArgb(255, 255, 255, 255), 3f);
                // After this, render the lines in click (circle) and double-click (crosshair) when the image is clicked (like a touchscreen)
                if (click != null)
                {
                    Point c = (Point)click;
                    if (doubleClick && c.X > marginLeft && c.X < canvas.Image.Width - marginOther && c.Y > marginOther && c.Y < canvas.Image.Height - marginOther)
                    {
                        bool t = false;
                        foreach (KeyValuePair<Point, int> nptr in ptrs)
                        {
                            if (c.X <= nptr.Key.X + 25 && c.X >= nptr.Key.X - 25 && c.Y <= nptr.Key.Y + 25 && c.Y >= nptr.Key.Y - 25)
                            {
                                // Horizontal
                                g.DrawLine(target, marginLeft, nptr.Key.Y, canvas.Image.Width - marginOther, nptr.Key.Y);
                                // Vertical
                                g.DrawLine(target, nptr.Key.X, marginOther, nptr.Key.X, canvas.Image.Height - marginOther);
                                ClickedIndex = nptr.Value;
                                t = true;
                                break;
                            }
                        }
                        if (!t)
                        {
                            ClickedIndex = -1;
                            // Horizontal
                            g.DrawLine(crosshair, marginLeft, c.Y, canvas.Image.Width - marginOther, c.Y);
                            // Vertical
                            g.DrawLine(crosshair, c.X, marginOther, c.X, canvas.Image.Height - marginOther);
                        }

                    }
                    else
                    {
                        g.DrawEllipse(crosshair, new Rectangle(new Point(c.X - 4, c.Y - 4), new Size(9, 9)));
                    }
                }
                else ClickedIndex = -1;
                
                g.Flush();
            }
        }
        #endregion
    }
}
