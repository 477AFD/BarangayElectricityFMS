using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing.Printing;

namespace CRUD_GROUP
{
    internal class PrintWorker
    {
        Bitmap g;
        DataTable rec;
        string n;
        public PrintWorker(Bitmap graph, DataTable record, string name = null)
        {
            g = graph;
            rec = record;
            n = name ?? "Unknown";
        }

        public bool Print()
        {
            PrintDocument f = new PrintDocument();
            f.PrintPage += new PrintPageEventHandler(PrintPageHandler);
            try {
                f.Print();
                return true;
            } catch
            {
                return false;
            }
        }

        public void PrintPageHandler(object sender, PrintPageEventArgs e)
        {
            RichString header = new RichString("Electricity Record", "Arial", 16, FontStyle.Bold);
            RichString line1 = new RichString($"Name of customer: {n}", "Arial");
            e.Graphics.DrawString(header.Text, header.Font, Brushes.Black, 100, 100);

        }
    }

    public class RichString
    {
        public Font Font { get; }
        public string Text { get; }
        public RichString(object content, string fontName = "Times New Roman", float size = 12.0f, FontStyle style = FontStyle.Regular)
        {
            Font = new Font(fontName, size, style);
            Text = $"{content}";
        }
    }
}
