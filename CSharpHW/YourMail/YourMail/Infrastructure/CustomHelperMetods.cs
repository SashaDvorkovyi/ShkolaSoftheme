using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YourMail.Interfaces;
using YourMail.Models;

namespace YourMail.Infrastructure
{
    public static class CustomHelperMetods
    {
        public static MvcHtmlString MyGrid(this HtmlHelper helper, IEnumerable<ITypesOfLetter> tLetters, string namberOfPeage)
        {
            return new MvcHtmlString(MyGrid(tLetters, namberOfPeage));
        }
        public static string MyGrid( IEnumerable<ITypesOfLetter> tLetters, string namberOfPeage)
        {
            var arrayHeders = new string[] { "Chek for delete", "Date", "To/From Whom", "Subject", "", "" };

            var tagDiv = new TagBuilder("div");
            tagDiv.GenerateId("Conteiner");

            var tagTable = new TagBuilder("table");
            var tagTr = new TagBuilder("tr");
            tagTable.InnerHtml += tagTr.ToString();

            for (var i = 0; i < arrayHeders.Length; i++)
            {
                var tagTh = new TagBuilder("th");
                tagTh.SetInnerText(arrayHeders[i]);
                tagTable.InnerHtml += tagTh.ToString();
            }

            var countLetters = tLetters.Count();

            var integerDivisors = countLetters / 5;

           var remainder = countLetters % 5;

            var caunter = default(int);

            if (int.TryParse(namberOfPeage, out int intNamberPage))
            {
                if (intNamberPage < integerDivisors)
                {
                    intNamberPage = integerDivisors;
                }
            }
            else
            {
                intNamberPage = 1;
            }

            foreach (var letter in tLetters)
            {
                if (caunter >= ((intNamberPage - 1) * 5) && caunter < ((intNamberPage - 1) * 5 + 5))
                {
                    tagTr = new TagBuilder("tr");

                    var tagTd = new TagBuilder("td");
                    var tagInput = new TagBuilder("input");
                    tagInput.Attributes.Add("type", "checkbox");
                    tagInput.Attributes.Add("name", letter.Id.ToString());
                    tagInput.AddCssClass("CheckForDelete");
                    tagTd.InnerHtml += tagInput.ToString();
                    tagTr.InnerHtml += tagTd.ToString();

                    tagTd = new TagBuilder("td");
                    tagTd.SetInnerText(letter.Data.ToString());
                    tagTr.InnerHtml += tagTd.ToString();

                    tagTd = new TagBuilder("td");
                    if (letter is SendLetter)
                    {
                        tagTd.SetInnerText(letter.ToWhoms.ToString());
                    }
                    else
                    {
                        tagTd.SetInnerText(letter.FromWhom.ToString());
                    }
                    tagTr.InnerHtml += tagTd.ToString();

                    tagTd = new TagBuilder("td");
                    tagTd.SetInnerText(letter.Subject.ToString());
                    tagTr.InnerHtml += tagTd.ToString();

                    tagTd = new TagBuilder("td");
                    tagInput = new TagBuilder("input");
                    tagInput.Attributes.Add("type", "button");
                    tagInput.Attributes.Add("name", letter.Id.ToString());
                    tagInput.Attributes.Add("value", "Open");
                    tagInput.AddCssClass("Open");
                    tagTd.InnerHtml += tagInput.ToString();
                    tagTr.InnerHtml += tagTd.ToString();

                    tagTd = new TagBuilder("td");
                    tagInput = new TagBuilder("input");
                    tagInput.Attributes.Add("type", "button");
                    tagInput.Attributes.Add("name", letter.Id.ToString());
                    tagInput.Attributes.Add("value", "Delete");
                    tagInput.AddCssClass("Delete");
                    tagTd.InnerHtml += tagInput.ToString();
                    tagTr.InnerHtml += tagTd.ToString();

                    tagTable.InnerHtml += tagTr.ToString();
                }
                caunter++;
            }

            tagDiv.InnerHtml += tagTable.ToString();

            if (remainder != 0)
            {
                for (var i = 1; i <= ((countLetters - remainder) / 5 + 1); i++)
                {
                    var tegA = new TagBuilder("a");
                    tegA.AddCssClass("NubmerOfPage");
                    tegA.SetInnerText(i.ToString());

                    tagDiv.InnerHtml += tegA.ToString();
                }
            }
            else
            {
                for (var i = 1; i <= ((countLetters - remainder) / 5); i++)
                {
                    var tegA = new TagBuilder("a");
                    tegA.AddCssClass("NubmerOfPage");
                    tegA.SetInnerText(i.ToString());

                    tagDiv.InnerHtml += tegA.ToString();
                }
            }

            return tagDiv.ToString();
        }
    }
}