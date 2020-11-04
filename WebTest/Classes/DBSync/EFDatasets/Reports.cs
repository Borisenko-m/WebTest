using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Excel_Interop.Datasets
{
    class Reports
    {
        DBSets DBSets;
       
        public Reports(DBSets sets)
        {
            DBSets = sets;
        }

        public IEnumerable<IEnumerable<string>> GetReport(DateTime from, DateTime to)
        {//                                                               от                     до
            var applications = DBSets.Applications.Where(a => a.CreatedAt >= from && a.CreatedAt <= to);
            var list = new List<List<string>>();
            var tempList = new List<string>();

            foreach (var a in applications)
            {
                tempList.Add(a.ApplicationID.ToString());
                tempList.Add(a.CreatedAt.ToString());
                tempList.Add(a.Address.Region.Name + ", " + a.Address.District.Name + ", " + a.Address.City.Name + ", " + a.Address.Street.Name + ", " + a.Address.Building.Name);
                tempList.Add(a.Classifier.Name);
                tempList.Add(a.User.LastName + " " + a.User.FirstName + " " + a.User.MiddleName);
                tempList.Add(a.User.PhoneNumber);
                tempList.Add(a.Company.Name);
                tempList.Add(a.Description);
                list.Add(tempList);

                tempList.Clear();
            }

            return list;
        }
    }
}
