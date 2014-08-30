using System.Collections.Generic;
using Core.Model;
using System.Linq;
using System;

namespace Core.Service
{
    

    public class AppBase
    {

        private static List<IUserProfile> _onlineUsers;

        
        private static List<IViewElement> _allViewElements;

        
        private static int _organazationId;

       
        private static int _userId;

      
        private static string _organizationName;

        private static string _organizationChartName;

      
        private static string _legalId { get; set; }

        
        private static string _nationalId { get; set; }

        
        private static bool _isLegal { get; set; }

       
        public static Dictionary<string, IList<string>> _viewElementGrantedToUser;

        private static string _organizationDomainName { get; set; }

        private string _userPortName { get; set; }
        private bool _showAllData { get; set; } 

       
        private int? _organizationChartId { get; set; }

     
        public Dictionary<string, IList<string>> ViewElementGrantedToUser
        {
            get { return _viewElementGrantedToUser ?? (_viewElementGrantedToUser = new Dictionary<string, IList<string>>()); }
            set { _viewElementGrantedToUser = value; }
        }


        public string OrganizationDomainName
        {
            get { return _organizationDomainName; }
            set { _organizationDomainName = value; }
        }



       
        public int OrganazationId
        {
            get { return _organazationId; }
            set { _organazationId = value; }
        }

       
        public int UserId
        {
            get { return _userId; }
            set { _userId = value; }
        }

        
        public bool IsLegal
        {
            get { return _isLegal; }
            set { _isLegal = value; }
        }

       
        public string LegalId
        {
            get { return _legalId; }
            set { _legalId = value; }
        }

     
      
        public string NationalId
        {
            get { return _nationalId; }
            set { _nationalId = value; }
        }

       
        public string OrganizationName
        {
            get { return _organizationName; }
            set { _organizationName = value; }
        }

       
        public string OrganizationChartName
        {
            get { return _organizationChartName; }
            set { _organizationChartName = value; }
        }


        public int? OrganizationChartId
        {
            get { return _organizationChartId; }
            set { _organizationChartId = value; }
        }

       
        public List<IUserProfile> OnlineUsers
        {
            get { return _onlineUsers ?? (_onlineUsers = new List<IUserProfile>()); }
        }

      
        public List<IViewElement> AllViewElements
        {
            get { return _allViewElements ?? (_allViewElements = new List<IViewElement>()); }
            set { _allViewElements = value; }
        }

        public string UserPortName
        {
            get { return _userPortName; }
            set { _userPortName = value; }
        }


        public bool ShowAllData
        {
            get { return _showAllData; }
            set { _showAllData = value; }
        }

       

        public static bool HasCurrentUserAccess(string userName, string url = null, string uniqueName = null)
        {
            bool hasAcces = string.IsNullOrEmpty(userName);

            IList<string> viewElementForUserName = new List<string>();


            if (url == null && uniqueName == null)
            {
                throw new Exception("url &  also uniqueName can't be null.");
            }


            else if (AppBase._viewElementGrantedToUser.TryGetValue(userName.ToLower(), out viewElementForUserName))
            {
                if (uniqueName == null)
                {
                    var viewElementGrantedToUser = viewElementForUserName;
                    hasAcces = viewElementGrantedToUser.Any(v => v.Split('#')[1].ToLower() == url.ToLower());
                }
                else
                {
                    var arr = uniqueName.Split('#');
                    if (arr.Count() == 2)
                    {
                        uniqueName = arr[0];
                    }

                    var viewElementGrantedToUser = viewElementForUserName;
                    hasAcces = viewElementGrantedToUser.Any(v => v.Split('#')[0].ToLower() == uniqueName.ToLower());
                }
            }
            //when TryGetValue for _viewElementGrantedToUser doesn't exist
            else
            {
                hasAcces = false;

            }
            return hasAcces;
        }
    }
}
