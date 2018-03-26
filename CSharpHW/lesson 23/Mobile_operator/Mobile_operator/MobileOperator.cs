using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Xml.XPath;
using System.Xml;



namespace Mobile_operator
{
    [DataContract]
    public class MobileOperator
    {
        //[DataMember]
        //public Dictionary<int, MobileAccount> _dictAccount;

        //[DataMember]
        //private Dictionary<int, DataCallEndMessage> _magazine;

        public MobileOperator()
        {
            //_dictAccount = new Dictionary<int, MobileAccount>();
            //_magazine = new Dictionary<int, DataCallEndMessage>();
        }


        public bool AddAAccount(MobileAccount account)
        {

            //XPathDocument xPathDoc = new XPathDocument("mobileOperator.xml");
            //XPathNavigator xPathNavigator= xPathDoc.CreateNavigator();
            //string str = account.Number.ToString();
            //XPathExpression expression = xPathNavigator.Compile("/MobileOperator/_dictAccount/a:KeyValueOfintMobileAccountYbXkmbSP");
            //XPathNodeIterator iterator = xPathNavigator.Select(expression);
            //if (iterator.MoveNext())
            //{
            //    xPathNavigator.AppendChildElement("a:Key", "a:KeyValueOfintMobileAccountYbXkmbSP", "mobileOperator.xml", account.Number.ToString());
            //}

            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load("mobileOperator.xml");
            XmlNodeList xmlNodes = xmlDocument.GetElementsByTagName("a:Key");
            foreach (XmlNode node in xmlNodes)
            {
                if (int.Parse(node.InnerText) == account.Number)
                {
                    return false;
                }
            }
            XmlNodeList xmlNod = xmlDocument.GetElementsByTagName("_dictAccount");
            foreach (XmlNode node in xmlNod)
            {
                XmlText numberKey = xmlDocument.CreateTextNode(account.Number.ToString());
                XmlText email = xmlDocument.CreateTextNode(account.Email.ToString());
                XmlText firstName = xmlDocument.CreateTextNode(account.FirstNume.ToString());
                XmlText number = xmlDocument.CreateTextNode(account.Number.ToString());
                XmlText secondName = xmlDocument.CreateTextNode(account.SecondNume.ToString());

                XmlElement userElem1 = xmlDocument.CreateElement("a:KeyValueOfintMobileAccountYbXkmbSP");
                XmlElement userElem11 = xmlDocument.CreateElement("a:Key");
                XmlElement userElem12 = xmlDocument.CreateElement("a:Value");
                XmlElement userElem121 = xmlDocument.CreateElement("Email");
                XmlElement userElem122 = xmlDocument.CreateElement("FirstNume");
                XmlElement userElem123 = xmlDocument.CreateElement("Number");
                XmlElement userElem124 = xmlDocument.CreateElement("SecondNume");
                XmlElement userElem125 = xmlDocument.CreateElement("addressBook");

                userElem11.AppendChild(numberKey);
                userElem121.AppendChild(email);
                userElem122.AppendChild(firstName);
                userElem123.AppendChild(number);
                userElem124.AppendChild(secondName);

                userElem12.AppendChild(userElem121);
                userElem12.AppendChild(userElem122);
                userElem12.AppendChild(userElem123);
                userElem12.AppendChild(userElem124);
                userElem12.AppendChild(userElem125);

                userElem1.AppendChild(userElem11);
                userElem1.AppendChild(userElem12);
                node.AppendChild(userElem1);
                xmlDocument.Save("mobileOperator.xml");
            }


                //var result = default(bool)
                //var results = new List<ValidationResult>();
                //var context = new ValidationContext(account);

                //if (!Validator.TryValidateObject(account, context, results, true))
                //{
                //    foreach (var error in results)
                //    {
                //        Console.WriteLine(error.ErrorMessage);
                //    }
                //}
                //else
                //{
                //    var count = _dictAccount.Count;
                //    _dictAccount.Add(account.Number, account);

                //    if (count != _dictAccount.Count)
                //    {
                //        _magazine.Add(account.Number, new DataCallEndMessage());
                //        _dictAccount[account.Number].MessageEvent += AcceptAndSend;
                //        _dictAccount[account.Number].CallEvent += AcceptAndSend;
                //        result = true;
                //    }
                //}
                return true;
        }

        //public void DeleteAccount(int number)
        //{
        //    if (_dictAccount.ContainsKey(number))
        //    {
        //        _dictAccount.Remove(number);
        //        _magazine.Remove(number);
        //        _dictAccount[number].MessageEvent -= AcceptAndSend;
        //        _dictAccount[number].CallEvent -= AcceptAndSend;
        //    }
        //}

        //public MobileAccount TakeAccount(int number) => _dictAccount[number];

        //public void AcceptAndSend(object accountOut, CallAndMessageEventArgs eventArg)
        //{
        //    var account = accountOut as MobileAccount;
        //    if (account != null)
        //    {
        //        if (_dictAccount.ContainsKey(eventArg.Number))
        //        {
        //            if (eventArg.Message == null)
        //            {
        //                _magazine[eventArg.Number].InCall += 2;
        //                _magazine[account.Number].OutCall += 2;
        //                _dictAccount[eventArg.Number].Show(account, eventArg);
        //            }
        //            else
        //            {
        //                _magazine[eventArg.Number].InCall++;
        //                _magazine[account.Number].OutCall++;
        //                _dictAccount[eventArg.Number].Show(account, eventArg);
        //            }
        //        }
        //    }
 
        //}

        //public void Top_5_Outgoing()
        //{
        //    var result = _magazine.OrderByDescending(x => x.Value.OutCall + x.Value.OutMessage).Take(5);
        //    foreach(var item in result)
        //    {
        //        Console.WriteLine(item.Key);
        //    }
        //}

        //public void Top_5_Ingoing()
        //{
        //    var result = _magazine.OrderByDescending(x => x.Value.InCall + x.Value.InMessage).Take(5);
        //    foreach (var item in result)
        //    {
        //        Console.WriteLine(item.Key);
        //    }
        //}
    }
}
