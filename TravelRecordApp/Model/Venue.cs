﻿using System;
using System.Collections.Generic;
using System.Text;
using TravelRecordApp.Helpers;

namespace TravelRecordApp.Model
{
    //public class Meta
    //{
    //    public int code { get; set; }
    //    public string requestId { get; set; }
    //}

    //public class LabeledLatLng
    //{
    //    public string label { get; set; }
    //    public double lat { get; set; }
    //    public double lng { get; set; }
    //}

    public class Location
    {
        public string address { get; set; }
        public double lat { get; set; }
        public double lng { get; set; }
        //public IList<LabeledLatLng> labeledLatLngs { get; set; }
        public int distance { get; set; }
        public string postalCode { get; set; }
        public string cc { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string country { get; set; }
        public IList<string> formattedAddress { get; set; }
        public string crossStreet { get; set; }
    }

    //public class Icon
    //{
    //    public string prefix { get; set; }
    //    public string suffix { get; set; }
    //}

    public class Category
    {
        public string id { get; set; }
        public string name { get; set; }
        public string pluralName { get; set; }
        public string shortName { get; set; }
        //public Icon icon { get; set; }
        public bool primary { get; set; }
    }

    public class VenuePage
    {
        public string id { get; set; }
    }

    public class Venue
    {
        public string id { get; set; }
        public string name { get; set; }
        public Location location { get; set; }
        public IList<Category> categories { get; set; }
        //public string referralId { get; set; }
        //public bool hasPerk { get; set; }
        public VenuePage venuePage { get; set; }
    }

    public class Response
    {
        public IList<Venue> venues { get; set; }
        //public bool confident { get; set; }
    }


    public class VenueInfos
    {
        public Response response { get; set; }

        public static string GenerateURL(double latitude, double longitude)
        {
            string url = string.Format(Constants.VenueSearch, latitude, longitude, Constants.ClientId, Constants.ClientSecret, DateTime.Now.ToString("yyyyMMdd"));
            return url;
        }
    }
}
