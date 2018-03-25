using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;


namespace Mobile_operator
{
    [DataContract]
    public class MobileOperator
    {
        [DataMember]
        public Dictionary<int, MobileAccount> _dictAccount;

        [DataMember]
        private Dictionary<int, DataCallEndMessage> _magazine;

        public MobileOperator()
        {
            _dictAccount = new Dictionary<int, MobileAccount>();
            _magazine = new Dictionary<int, DataCallEndMessage>();
        }

        public bool AddAAccount(MobileAccount account)
        {
            var result = default(bool);
            var results = new List<ValidationResult>();
            var context = new ValidationContext(account);

            if (!Validator.TryValidateObject(account, context, results, true))
            {
                foreach (var error in results)
                {
                    Console.WriteLine(error.ErrorMessage);
                }
            }
            else
            {
                var count = _dictAccount.Count;
                _dictAccount.Add(account.Number, account);

                if (count != _dictAccount.Count)
                {
                    _magazine.Add(account.Number, new DataCallEndMessage());
                    _dictAccount[account.Number].MessageEvent += AcceptAndSend;
                    _dictAccount[account.Number].CallEvent += AcceptAndSend;
                    result = true;
                }
            }
            return result;
        }

        public void DeleteAccount(int number)
        {
            if (_dictAccount.ContainsKey(number))
            {
                _dictAccount.Remove(number);
                _magazine.Remove(number);
                _dictAccount[number].MessageEvent -= AcceptAndSend;
                _dictAccount[number].CallEvent -= AcceptAndSend;
            }
        }

        public MobileAccount TakeAccount(int number) => _dictAccount[number];

        public void AcceptAndSend(object accountOut, CallAndMessageEventArgs eventArg)
        {
            var account = accountOut as MobileAccount;
            if (account != null)
            {
                if (_dictAccount.ContainsKey(eventArg.Number))
                {
                    if (eventArg.Message == null)
                    {
                        _magazine[eventArg.Number].InCall += 2;
                        _magazine[account.Number].OutCall += 2;
                        _dictAccount[eventArg.Number].Show(account, eventArg);
                    }
                    else
                    {
                        _magazine[eventArg.Number].InCall++;
                        _magazine[account.Number].OutCall++;
                        _dictAccount[eventArg.Number].Show(account, eventArg);
                    }
                }
            }
 
        }

        public void Top_5_Outgoing()
        {
            var result = _magazine.OrderByDescending(x => x.Value.OutCall + x.Value.OutMessage).Take(5);
            foreach(var item in result)
            {
                Console.WriteLine(item.Key);
            }
        }

        public void Top_5_Ingoing()
        {
            var result = _magazine.OrderByDescending(x => x.Value.InCall + x.Value.InMessage).Take(5);
            foreach (var item in result)
            {
                Console.WriteLine(item.Key);
            }
        }
    }
}
