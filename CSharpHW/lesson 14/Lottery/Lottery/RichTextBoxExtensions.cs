using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Documents;


namespace Lottery
{
    public static class RichTextBoxExtensions
    {
        //public static void AppendText(this RichTextBox box, string text, Color color)
        //{
        //    box.SelectionStart = box.TextLength;
        //    box.SelectionLength = 0;
        //    box.SelectionColor = color;
        //    box.AppendText(text);
        //    box.Col = box.ForeColor;
        //}

        //public static void AppendText(this RichTextBox box, string text, string color)
        //{
        //    BrushConverter bc = new BrushConverter();
        //    TextRange tr = new TextRange(box.Document.ContentEnd, box.Document.ContentEnd);
        //    tr.Text = text;
        //    try
        //    {
        //        tr.ApplyPropertyValue(TextElement.ForegroundProperty,
        //            bc.ConvertFromString(color));
        //    }
        //    catch (FormatException) { }
        //}
    }
}
