using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Data.Common;
using System.Text.Json;
using System.Runtime.Serialization.Json;
using Microsoft.AspNetCore.Mvc;
using System.Web;

using WebTest.Models;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore.Query;

class Reports
{
    DBSets DBSets;

    public Reports(DBSets sets)
    {
        DBSets = sets;
    }

    public IEnumerable<ApplicationModel> GetReport(ReportConfiguration report)
    {
        switch (report.Category.ToLower())
        {
            case "компании":
                return GetAppByCompanies(report);
            case "адреса":
                return GetAppByAddresses(report);
            case "классификаторы":
                return GetAppByClassifiers(report);
            default:
                return new List<ApplicationModel>();
        }
    }
    public IEnumerable<ApplicationModel> GetAppsByFilter(FilterModel filter)
    {
        IEnumerable<ulong?> addressesList = GetAddressesID(filter.Addresses);
        IEnumerable<ulong> classifiersList = GetClassifiersID(filter.Classifiers);

        var seq =
            from app in DBSets.Applications

            join appStatus in DBSets.ApplicationStatuses on app.ApplicationStatusID equals appStatus.ApplicationStatusID
            join classifiers in DBSets.Classifiers on app.ClassifierID equals classifiers.ClassifierID
            join companies in DBSets.Companies on app.CompanyID equals companies.CompanyID
            join users in DBSets.Users on app.UserID equals users.UserID

            join addresses in DBSets.Addresses on app.AddressID equals addresses.AddressID
            join regions in DBSets.Regions on addresses.RegionID equals regions.RegionID
            join districts in DBSets.Districts on addresses.DistrictID equals districts.DistrictID
            join cities in DBSets.Cities on addresses.CityID equals cities.CityID
            join streets in DBSets.Streets on addresses.StreetID equals streets.StreetID
            join buildings in DBSets.Buildings on addresses.BuildingID equals buildings.BuildingID

            
            where classifiersList.Contains((ulong)app.ClassifierID) || classifiersList.Count() == 0
            where filter.Companies.Contains(companies.Name.ToLower()) || filter.Companies.Count() == 0
            where addressesList.Contains(app.AddressID) || addressesList.Count() == 0

            select new ApplicationModel()
            {
                ApplicationID = app.ApplicationID.ToString(),
                Address = string.Join(" ", regions.Name, districts.Name, districts.Name, cities.Name, streets.Name, buildings.Name),
                Classifier = classifiers.Name,
                Company = companies.Name,
                Status = appStatus.Name,
                UserFullName = string.Join(" ", users.LastName, users.FirstName, users.MiddleName),
                UserPhone = users.PhoneNumber,
                CreatedAt = app.CreatedAt.ToString("yyyy'-'MM'-'dd' 'HH':'mm':'ss"),
                Description = app.Description
            };
        var list = new List<ApplicationModel>();
        foreach (var period in filter.Periods)
        {
            foreach (var item in seq)
            {
                if(period.CheckDate(DateTime.Parse(item.CreatedAt))) list.Add(item);
            }
        }

        return list;
    }

    public IEnumerable<ApplicationModel> GetAppByCompanies(ReportConfiguration report)
    {
        return
             from application in DBSets.Applications
             join ApplicationStatus in DBSets.ApplicationStatuses on application.ApplicationStatusID equals ApplicationStatus.ApplicationStatusID
             join Address in DBSets.Addresses on application.AddressID equals Address.AddressID
             join Region in DBSets.Regions on Address.RegionID equals Region.RegionID
             join District in DBSets.Districts on Address.DistrictID equals District.DistrictID
             join City in DBSets.Cities on Address.CityID equals City.CityID
             join Street in DBSets.Streets on Address.StreetID equals Street.StreetID
             join Building in DBSets.Buildings on Address.BuildingID equals Building.BuildingID
             join Classifier in DBSets.Classifiers on application.ClassifierID equals Classifier.ClassifierID
             join User in DBSets.Users on application.UserID equals User.UserID
             join Company in DBSets.Companies on application.CompanyID equals Company.CompanyID

             where application.CreatedAt >= report.From && application.CreatedAt <= report.To
             where report.Specifications.Contains(Company.Name.ToLower()) || report.Specifications.Count() == 0

             select new ApplicationModel()
             {
                 ApplicationID = application.ApplicationID.ToString(),
                 CreatedAt = application.CreatedAt.ToString("yyyy'-'MM'-'dd' 'HH':'mm':'ss"),
                 Address = Region.Name + ", " + District.Name + ", " + City.Name + ", " + Street.Name + ", " + Building.Name,
                 Classifier = Classifier.Name,
                 UserFullName = User.LastName + " " + User.FirstName + " " + User.MiddleName,
                 UserPhone = User.PhoneNumber,
                 Company = Company.Name,
                 Description = application.Description.Replace("<p>", "").Replace("</p>", ""),
                 Status = application.ApplicationStatus.Name
             };
    }

    public IEnumerable<ApplicationModel> GetAppByClassifiers(ReportConfiguration report)
    {
        IEnumerable<ulong> classifiersList = GetClassifiersID(report.Specifications);
        return
            from application in DBSets.Applications
            join ApplicationStatus in DBSets.ApplicationStatuses on application.ApplicationStatusID equals ApplicationStatus.ApplicationStatusID
            join Address in DBSets.Addresses on application.AddressID equals Address.AddressID
            join Region in DBSets.Regions on Address.RegionID equals Region.RegionID
            join District in DBSets.Districts on Address.DistrictID equals District.DistrictID
            join City in DBSets.Cities on Address.CityID equals City.CityID
            join Street in DBSets.Streets on Address.StreetID equals Street.StreetID
            join Building in DBSets.Buildings on Address.BuildingID equals Building.BuildingID
            join Classifier in DBSets.Classifiers on application.ClassifierID equals Classifier.ClassifierID
            join User in DBSets.Users on application.UserID equals User.UserID
            join Company in DBSets.Companies on application.CompanyID equals Company.CompanyID

            where application.CreatedAt >= report.From && application.CreatedAt <= report.To
            where classifiersList.Contains(Classifier.ClassifierID) || classifiersList.Contains((ulong)Classifier.ParentID) || report.Specifications.Count() == 0

            select new ApplicationModel()
            {
                ApplicationID = application.ApplicationID.ToString(),
                CreatedAt = application.CreatedAt.ToString("yyyy'-'MM'-'dd' 'HH':'mm':'ss"),
                Address = Region.Name + ", " + District.Name + ", " + City.Name + ", " + Street.Name + ", " + Building.Name,
                Classifier = Classifier.Name,
                UserFullName = User.LastName + " " + User.FirstName + " " + User.MiddleName,
                UserPhone = User.PhoneNumber,
                Company = Company.Name,
                Description = application.Description.Replace("<p>", "").Replace("</p>", "")
            };


    }

    public IEnumerable<ApplicationModel> GetAppByAddresses(ReportConfiguration report)
    {

        IEnumerable<ulong?> addressesList = GetAddressesID(report.Specifications);
        return
            from application in DBSets.Applications
            join ApplicationStatus in DBSets.ApplicationStatuses on application.ApplicationStatusID equals ApplicationStatus.ApplicationStatusID
            join Address in DBSets.Addresses on application.AddressID equals Address.AddressID
            join Region in DBSets.Regions on Address.RegionID equals Region.RegionID
            join District in DBSets.Districts on Address.DistrictID equals District.DistrictID
            join City in DBSets.Cities on Address.CityID equals City.CityID
            join Street in DBSets.Streets on Address.StreetID equals Street.StreetID
            join Building in DBSets.Buildings on Address.BuildingID equals Building.BuildingID
            join Classifier in DBSets.Classifiers on application.ClassifierID equals Classifier.ClassifierID
            join User in DBSets.Users on application.UserID equals User.UserID
            join Company in DBSets.Companies on application.CompanyID equals Company.CompanyID

            where application.CreatedAt >= report.From && application.CreatedAt <= report.To
            where addressesList.Contains(application.AddressID) || report.Specifications.Count() == 0

            select new ApplicationModel()
            {
                ApplicationID = application.ApplicationID.ToString(),
                CreatedAt = application.CreatedAt.ToString("yyyy'-'MM'-'dd' 'HH':'mm':'ss"),
                Address = Region.Name + ", " + District.Name + ", " + City.Name + ", " + Street.Name + ", " + Building.Name,
                Classifier = Classifier.Name,
                UserFullName = User.LastName + " " + User.FirstName + " " + User.MiddleName,
                UserPhone = User.PhoneNumber,
                Company = Company.Name,
                Description = application.Description.Replace("<p>", "").Replace("</p>", "")
            };
    }

    private IEnumerable<ulong> GetClassifiersID(IEnumerable<string> report)
    {
        List<ulong> list = new List<ulong>();
        var result =
        from Classifier in DBSets.Classifiers

        where report.Contains(Classifier.Name)

        select new
        {
            Classifier = Classifier.ClassifierID,
        };

        list.AddRange(result.Select(l => l.Classifier));
        return list;
    }

    private IEnumerable<ulong?> GetAddressesID(IEnumerable<string> adresses)
    {
        List<string> spec = adresses.ToList();
        List<ulong?> list = new List<ulong?>();
        for (int i = 0; i < spec.Count && i + 3 < spec.Count; i += 4)
        {
            var result =
            from Address in DBSets.Addresses
            join Region in DBSets.Regions on Address.RegionID equals Region.RegionID
            join District in DBSets.Districts on Address.DistrictID equals District.DistrictID
            join City in DBSets.Cities on Address.CityID equals City.CityID
            join Street in DBSets.Streets on Address.StreetID equals Street.StreetID

            where Region.Name == spec[i] || spec[i] == ""
            where District.Name == spec[i + 1] || spec[i + 1] == ""
            where City.Name == spec[i + 2] || spec[i + 2] == ""
            where Street.Name == spec[i + 3] || spec[i + 3] == ""

            select new
            {
                Address = Address.AddressID,
            };

            list.AddRange(result.Select(l => l.Address));
        }
        return list;
    }

    public IEnumerable<string?> GetCompanies(string company) =>
        DBSets.Companies.Where(c => c.Name.ToLower().Contains(company.ToLower())).Select(c => c.Name).ToList();

    public IEnumerable<string?> GetClassifiers(string classifier) =>
        DBSets.Classifiers.Where(c => c.Name.ToLower().Contains(classifier.ToLower())).Select(c => c.Name).ToList();

    public IEnumerable<string?> GetRegions(string region) =>
       DBSets.Regions.Where(r => r.Name.ToLower().Contains(region.ToLower())).Select(c => c.Name).ToList();

    public IEnumerable<string?> GetDistricts(string region, string district)
    {
        var result =
            from District in DBSets.Districts
            join Region in DBSets.Regions on District.RegionID equals Region.RegionID
            where Region.Name.ToLower().Contains(region)
            where District.Name.ToLower().Contains(district)

            select new
            {
                District = District.Name
            };

        return result.Select(l => l.District);
    }

    public IEnumerable<string?> GetCities(string region, string district, string city)
    {
        var result =
            from City in DBSets.Cities
            join District in DBSets.Districts on City.DistrictID equals District.DistrictID
            join Region in DBSets.Regions on District.RegionID equals Region.RegionID
            where Region.Name.ToLower().Contains(region)
            where District.Name.ToLower().Contains(district)
            where City.Name.ToLower().Contains(city)

            select new
            {
                City = City.Name
            };

        return result.Select(l => l.City);
    }

    public IEnumerable<string?> GetStreets(string region, string district, string city, string street)
    {
        var result =
            from Street in DBSets.Streets
            join City in DBSets.Cities on Street.CityID equals City.CityID
            join District in DBSets.Districts on City.DistrictID equals District.DistrictID
            join Region in DBSets.Regions on District.RegionID equals Region.RegionID
            where Region.Name.ToLower().Contains(region)
            where District.Name.ToLower().Contains(district)
            where City.Name.ToLower().Contains(city)
            where City.Name.ToLower().Contains(street)

            select new
            {
                Street = Street.Name
            };

        return result.Select(l => l.Street);
    }


}
