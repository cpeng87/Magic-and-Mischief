// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class Date
// {
//     public int season;
//     public int day;
//     public int year;

//     public Date(int season, int day, int year)
//     {
//         this.season = season;
//         this.day = day;
//         this.year = year;
//     }

//     public Date()
//     {
//         this.season = 0;
//         this.day = 0;
//         this.year = 1;
//     }

//     public override bool Equals(object obj)
//     {
//         if (obj == null || GetType() != obj.GetType())
//             return false;

//         Date other = (Date)obj;
//         return season == other.season && day == other.day && year == other.year;
//     }

//     public override int GetHashCode()
//     {
//         unchecked
//         {
//             int hash = 17;
//             hash = hash * 23 + season.GetHashCode();
//             hash = hash * 23 + day.GetHashCode();
//             hash = hash * 23 + year.GetHashCode();
//             return hash;
//         }
//     }

//     public override string ToString()
//     {
//         string rtn = season + "/" + day + "/" + year;
//         return rtn;
//     }
// }