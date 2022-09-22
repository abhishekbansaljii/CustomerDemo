using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using View;

namespace CustomerDemo
{
    public class Program
    {
        static void Main(string[] args)
        {
            
            CustomerView cvw = new CustomerView();
            bool sts = true;
            while (sts) {
                int ch = cvw.MenuChoice();
                switch (ch) { 
                    case 1:
                        cvw.AddCustomerView();
                        break;
                    case 2:
                        cvw.UpdateCustomerView();
                        break;
                    case 3:
                        cvw.DeleteCustomerView();
                        break;
                    case 4:
                        cvw.FindCustomerView();
                        break;
                    case 5:
                        cvw.CustomerSummaryView();
                        break;
                    case 6:
                        sts = false;
                        break;
                    default: Console.WriteLine("\n\n******Wrong Choice*******\n\n");
                        break;
                }
            }
        }
    }
}
