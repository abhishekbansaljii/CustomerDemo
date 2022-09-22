using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class Customer
    {
        public int CID
        {
            get;
            set;
        }
        public string CNAME
        {
            get;
            set;
        }
        public string CGENDER
        {
            get;
            set;
        }
        public string ADDRESS
        {
            get;
            set;
        }
        public string MOBILE
        {
            get;
            set;
        }
        public override string ToString()
        {
            string s = "";
            s += "\nCustomer ID\t:\t" + CID;
            s+= "\nCustomer Name\t:\t" + CNAME;
            s += "\nGender\t\t:\t" + CGENDER;
            s+= "\nAddress\t\t:\t" + ADDRESS;
            s += "\nMobile Number\t:\t" +MOBILE;
            return s;
        }
    }
}
