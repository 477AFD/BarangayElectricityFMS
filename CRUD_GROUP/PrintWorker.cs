using System;
using System.Data;
using System.Drawing;
using System.Threading.Tasks;
using System.Drawing.Printing;
using System.Windows.Forms;

namespace CRUD_GROUP
{
    /// <summary>
    /// This is the class for a Print Worker. Its job is to make and print records.
    /// </summary>
    internal class PrintWorker
    {
        #region Variables
        Bitmap g;
        Bitmap p;
        DataTable rec;
        string n;
        #endregion

        #region Constructor
        /// <summary>
        /// Creates a document for printing.
        /// </summary>
        /// <param name="graph">Specifies a graph describing the trend of price</param>
        /// <param name="price">Specifies a graph describing the trend of price rate per kWh</param>
        /// <param name="record">Specifies the record stored for the user</param>
        /// <param name="name">The full name of the customer (not username)</param>
        public PrintWorker(Bitmap graph, Bitmap price, DataTable record, string name = null)
        {
            g = graph;
            p = price;
            rec = record;
            n = name ?? "Unknown";
        }
        #endregion

        #region Print command
        public bool Print()
        {
            PrintDocument f = new PrintDocument();
            bool res = false;
            f.PrintPage += new PrintPageEventHandler(PrintPageHandler);
            try
            {
                PrintDialog r = new PrintDialog
                {
                    Document = f
                };
                if (r.ShowDialog() == DialogResult.OK)
                {
                    PrintPreviewDialog w = new PrintPreviewDialog
                    {
                        Document = f
                    };
                    DialogResult re = w.ShowDialog(); // This command prints the document upon clicking OK (or Print) in the dialog
                    res = re == DialogResult.OK || re == DialogResult.Yes;
                }
                else res = false;
            }
            catch (InvalidOperationException ex) 
            {
                MessageBox.Show($"{ex.Message}\nCheck your printer settings!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                res = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"SEEKER DETECTED:\n{ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                res = false;
            }
            finally
            {
                f.Dispose();
            }
            return res;
        }
        #endregion

        #region Document constructor
        /// <summary>
        /// This is where we code in printer commands using the e.Graphics property
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void PrintPageHandler(object sender, PrintPageEventArgs e)
        {
            RichString header = new RichString($"Electricity Consumption Report - {DateTime.Now}", "Arial", 16, FontStyle.Bold);
            RichString line1 = new RichString($"Name of customer: {n}", "Arial");
            RichString figure1 = new RichString($"Rate trend in {rec.Rows.Count} months", "Arial", 9);
            RichString figure2 = new RichString($"Price trend in {rec.Rows.Count} months", "Arial", 9);
            using (Graphics cmd = e.Graphics) // Creates a Graphics object from printer to draw commands.
            {
                cmd.DrawString(header.Text.ToString(), header.Font, Brushes.Black, 32, 32); // Tells the printer to place "Electricity Consumption Report - date" in position (32, 32)
                cmd.DrawString(line1.Text.ToString(), line1.Font, Brushes.Black, 32, 64);
                cmd.DrawImage(p, 32, 100, 384, 216); // Tells the printer to draw a graph containing rate per kWh in position (32, 100) with size 384x216 units
                Pen t = new Pen(Color.Black, 2f);
                cmd.DrawRectangle(t, 32, 100, 384, 216);
                cmd.DrawImage(g, 420, 100, 384, 216); // Tells the printer to draw a graph containing price in position (420, 100) with size 384x216 units
                cmd.DrawRectangle(t, 420, 100, 384, 216);
                // Render figures
                cmd.DrawString(figure1.Text.ToString(), figure1.Font, Brushes.Gray, 144, 321);
                cmd.DrawString(figure2.Text.ToString(), figure2.Font, Brushes.Gray, 540, 321);
                RichString line2 = new RichString("Electricity Record:", "Arial", 12);
                cmd.DrawString(line2.Text.ToString(), line2.Font, Brushes.Black, 32, 345);
                // Render table
                int Yaxis = 377; // Initial value, row is 18 units per rectangle
                int Xaxis = 32;
                Size cellSize = new Size(130, 30);
                Pen tl = new Pen(Color.Black, 1f);
                Font ft = new Font("Arial Narrow", 11f);
                Brush b = new SolidBrush(Color.Black);
                // Header
                Point[] ptw = new Point[6] {
                        new Point(Xaxis + 3, Yaxis + 3),
                        new Point(Xaxis + 3 + cellSize.Width, 3 + Yaxis),
                        new Point(Xaxis + 3 + cellSize.Width * 2, 3 + Yaxis),
                        new Point(Xaxis + 3 + cellSize.Width * 3, 3 + Yaxis),
                        new Point(Xaxis + 3 + cellSize.Width * 4, 3 + Yaxis),
                        new Point(Xaxis + 3 + cellSize.Width * 5, 3 + Yaxis)
                    };
                cmd.DrawString("Date", ft, b, ptw[0]);
                cmd.DrawString("Prev. kWh", ft, b, ptw[1]);
                cmd.DrawString("Curr. kWh", ft, b, ptw[2]);
                cmd.DrawString("Rate", ft, b, ptw[3]);
                cmd.DrawString("Price", ft, b, ptw[4]);
                cmd.DrawString("Receipt #", ft, b, ptw[5]);
                cmd.DrawRectangle(tl, Xaxis, Yaxis, cellSize.Width, cellSize.Height);
                cmd.DrawRectangle(tl, Xaxis + cellSize.Width, Yaxis, cellSize.Width, cellSize.Height);
                cmd.DrawRectangle(tl, Xaxis + cellSize.Width * 2, Yaxis, cellSize.Width, cellSize.Height);
                cmd.DrawRectangle(tl, Xaxis + cellSize.Width * 3, Yaxis, cellSize.Width, cellSize.Height);
                cmd.DrawRectangle(tl, Xaxis + cellSize.Width * 4, Yaxis, cellSize.Width, cellSize.Height);
                cmd.DrawRectangle(tl, Xaxis + cellSize.Width * 5, Yaxis, cellSize.Width, cellSize.Height);
                // Body
                for (int d = Yaxis + cellSize.Height, i = 0; i < rec.Rows.Count; i++, d += cellSize.Height)
                {
                    // Get the points to draw rectangles. The Point class is like a cardinal system: it has X and Y axis.
                    Point p1 = new Point(Xaxis, d);
                    Point p2 = new Point(Xaxis + cellSize.Width, d);
                    Point p3 = new Point(Xaxis + cellSize.Width * 2, d);
                    Point p4 = new Point(Xaxis + cellSize.Width * 3, d);
                    Point p5 = new Point(Xaxis + cellSize.Width * 4, d);
                    Point p6 = new Point(Xaxis + cellSize.Width * 5, d);
                    // Get the points to render text
                    Point pt1 = new Point(Xaxis + 3, d + 3);
                    Point pt2 = new Point(Xaxis + 3 + cellSize.Width, 3 + d);
                    Point pt3 = new Point(Xaxis + 3 + cellSize.Width * 2, 3 + d);
                    Point pt4 = new Point(Xaxis + 3 + cellSize.Width * 3, 3 + d);
                    Point pt5 = new Point(Xaxis + 3 + cellSize.Width * 4, 3 + d);
                    Point pt6 = new Point(Xaxis + 3 + cellSize.Width * 5, 3 + d);
                    // Create rectangles with dimensions
                    Rectangle r1 = new Rectangle(p1, cellSize);
                    Rectangle r2 = new Rectangle(p2, cellSize);
                    Rectangle r3 = new Rectangle(p3, cellSize);
                    Rectangle r4 = new Rectangle(p4, cellSize);
                    Rectangle r5 = new Rectangle(p5, cellSize);
                    Rectangle r6 = new Rectangle(p6, cellSize);
                    // Draw rectangles
                    Task.Delay(100);
                    cmd.DrawRectangle(tl, r1);
                    cmd.DrawRectangle(tl, r2);
                    cmd.DrawRectangle(tl, r3);
                    cmd.DrawRectangle(tl, r4);
                    cmd.DrawRectangle(tl, r5);
                    cmd.DrawRectangle(tl, r6);
                    // Render text (rec)
                    DataRow x = rec.Rows[i];
                    cmd.DrawString($"{x["DateCreated"]}", ft, b, pt1);
                    cmd.DrawString($"{x["PreviousKWH"]}", ft, b, pt2);
                    cmd.DrawString($"{x["CurrentKWH"]}", ft, b, pt3);
                    cmd.DrawString($"{x["RatePerKWH"]:C}", ft, b, pt4);
                    cmd.DrawString($"{x["Price"]:C}", ft, b, pt5);
                    cmd.DrawString($"{x["ReceiptNo"]}", ft, b, pt6);
                }
            }
        }
        #endregion
    }

    /// <summary>
    /// This is a string, but it is more of a rich text - a string with font family, font style, and font size.<br />
    /// This is only used in this class file, so it was placed here.
    /// </summary>
    public class RichString
    {
        #region Properties
        public Font Font { get; }
        public object Text { get; }
        #endregion

        #region Constructor
        public RichString(object content, string fontName = "Times New Roman", float size = 12.0f, FontStyle style = FontStyle.Regular)
        {
            Font = new Font(fontName, size, style);
            Text = content;
        }
        #endregion
    }
}