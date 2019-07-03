using System;
using System.Collections;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using ProductAPI.Attributes;

namespace ProductAPI.Dto
{
    [MongoCollection("companies")]
    public class CompanyDto : EntityDto
    {

        public CompanyDto()
        {

        }
       

        [BsonElement("name")]
        public string Name
        {
            get;
            set;
        }

        [BsonElement("industry")]
        public string Industry
        {
            get;
            set;
        }

        [BsonElement("size")]
        public string Size
        {
            get;
            set;
        }
        [BsonElement("headquarter")]
        public string Headquarter
        {
            get;
            set;
        }
        [BsonElement("revenue")]
        public string Revenue
        {
            get;
            set;
        }
        [BsonElement("new_industry")]
        public string NewIndustry
        {
            get;
            set;
        }
        [BsonElement("ratings")]
        public ICollection<Rating> Ratings{
            get;set;
        }

        [BsonElement("jobs")]
        public ICollection<Job> Jobs
        {
            get; set;
        }
   
    }

    public class Rating
    {
        [BsonElement("hasRating")]
        public bool HasRating
        {
            get;
            set;
        }
        [BsonElement("type")]
        public string Type
        {
            get;
            set;
        }
        [BsonElement("value")]
        public double Value
        {
            get;
            set;
        }
    }

    public class Job
    {
        [BsonElement("title")]
        public string Title
        {
            get;
            set;
        }
        [BsonElement("average_salary")]
        public double AverageSalary
        {
            get;
            set;
        }
        [BsonElement("level")]
        public string Level
        {
            get;
            set;
        }
        [BsonElement("categories")]
        public List<string> Categories
        {
            get;
            set;
        }

        [BsonElement("skill_count")]
        public int SkillCount
        {
            get;
            set;
        }
    }
}
