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

class Reports
{
    DBSets DBSets;
    public Reports()
    {
        DBSets = new DBSets();
    }

    public Reports(DBSets sets)
    {
        DBSets = sets;
    }

    public string? GetReport(DateTime fromDate, DateTime toDate)
    {//                                                               от                     до
        //var applications = DBSets.Applications;
        var list = new List<List<string?>>();
        var tempList = new List<string?>();
        //var apps = DBSets.Applications.Where(l => l.CreatedAt >= from && l.CreatedAt <= to).ToList(); ;
        var result =
            from application in DBSets.Applications
            where application.CreatedAt >= fromDate && application.CreatedAt <= toDate
            join Address in DBSets.Addresses on application.AddressID equals Address.AddressID
            join Region in DBSets.Regions on Address.RegionID equals Region.RegionID
            join District in DBSets.Districts on Address.DistrictID equals District.DistrictID
            join City in DBSets.Cities on Address.CityID equals City.CityID
            join Street in DBSets.Streets on Address.StreetID equals Street.StreetID
            join Building in DBSets.Buildings on Address.BuildingID equals Building.BuildingID
            join Classifier in DBSets.Classifiers on application.ClassifierID equals Classifier.ClassifierID
            join User in DBSets.Users on application.UserID equals User.UserID
            join Company in DBSets.Companies on application.CompanyID equals Company.CompanyID

            select new ReportModel()
            {
                ApplicationID = application.ApplicationID.ToString(),
                CreatedAt = application.CreatedAt.ToString(),
                Address = Region.Name + ", " + District.Name + ", " + City.Name + ", " + Street.Name + ", " + Building.Name,
                Classifier = Classifier.Name,
                UserFullName = User.LastName + User.FirstName + User.MiddleName,
                UserPhone = User.PhoneNumber,
                Company = Company.Name,
                Description = application.Description.Replace("<p>", "").Replace("</p>", "")
            };

        return new ModelToJson<ReportModel>() { Models = result.Select(l => l) }.JsonToString();

    }

    public string? GetReport(ReportConfiguration report)
    {
        switch (report.Type.ToLower())
        {
            case "заявки":
                if (report.Category == "Все") report.Category = "";
                var list = new List<List<string?>>();
                var tempList = new List<string?>();
                //var apps = DBSets.Applications.Where(l => l.CreatedAt >= from && l.CreatedAt <= to).ToList(); ;
                var result =
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
                    where ApplicationStatus.Name.ToLower().Contains(report.Category.ToLower())

                    select new ReportModel()
                    {
                        ApplicationID = application.ApplicationID.ToString(),
                        CreatedAt = application.CreatedAt.ToString(),
                        Address = Region.Name + ", " + District.Name + ", " + City.Name + ", " + Street.Name + ", " + Building.Name,
                        Classifier = Classifier.Name,
                        UserFullName = User.LastName + " " + User.FirstName + " " + User.MiddleName,
                        UserPhone = User.PhoneNumber,
                        Company = Company.Name,
                        Description = application.Description.Replace("<p>", "").Replace("</p>", "")
                    };

                return new ModelToJson<ReportModel>() { Models = result.Select(l => l) }.JsonToString();
        }

        return "";
    }


}
