using System.Collections.Generic;
using Core.Model;
using System.Linq;
using System;

namespace Core.Service
{
    /// <summary> 						            
    ///Created by :Kiaee
    ///Created on Date :1392/5/26
    /// This class Contains Members(Properties , Methods and Fields) that use in application level.like online users , ... . 
    ///Edited by :Alavi , Tabandeh , Pouyan 
    ///Edited on Date :1392/7/21
    /// </summary>

    public class AppBase
    {

        /// <summary> 						            
        ///Created by :Tabandeh
        ///Created on Date :1392/6/25
        /// <remarks>Define field for list of online users.</remarks>
        /// </summary>
        private static List<IUserProfile> _onlineUsers;

        /// <summary> 						            
        ///Created by :Pouyan
        ///Created on Date :1392/7/21
        /// <remarks>Define field for list of view elements.</remarks>
        /// </summary>
        private static List<IViewElement> _allViewElements;

        /// <summary> 						            
        ///Created by :Tabandeh
        ///Created on Date :1392/6/25
        /// </summary>
        private static int _organazationId;

        /// <summary> 						            
        ///Created by :Azizi
        ///Created on Date :1392/9/13
        /// </summary>
        private static int _userId;

        /// <summary> 						            
        ///Created by :Tabandeh
        ///Created on Date :1392/6/25
        /// </summary>
        private static string _organizationName;

        /// <summary> 						            
        ///Created by :Pouyan
        ///Created on Date :1392/8/13
        /// </summary>
        private static string _organizationChartName;

        /// <summary> 						            
        ///Created by :Pouyan
        ///Created on Date :1392/6/25
        /// <remarks>شناسه حقوقی شرکت</remarks>
        /// </summary>
        private static string _legalId { get; set; }

        /// <summary> 						            
        ///Created by :Tabandeh
        ///Created on Date :1392/6/25
        /// <remarks>کد ملی</remarks>
        /// </summary>
        private static string _nationalId { get; set; }

        /// <summary> 						            
        ///Created by :Tabandeh
        ///Created on Date :1392/6/25
        /// </summary>
        private static bool _isLegal { get; set; }

        ///  <summary>
        ///Created by :Pouyan 
        ///Created on Date :1392/7/14
        ///<remarks>declare a dictianary for view elements that granted(assigned) to user.</remarks>
        /// </summary>
        public static Dictionary<string, IList<string>> _viewElementGrantedToUser;

        private static string _organizationDomainName { get; set; }

        private string _userPortName { get; set; }
        private bool _showAllData { get; set; } //اطلاعات همه شناسه ها یا فقط خودش و زیر مجموعه خودش

        ///  <summary>
        ///Created by :Pouyan 
        ///Created on Date :1392/7/14
        ///<remarks>get and set value from and to the _viewElementGrantedToUser's dictioanary</remarks>
        /// </summary>
        private int? _organizationChartId { get; set; }

        ////باید null پذیر باشد چون ادمین جایگاه سازمانی ندارد
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



        ///  <summary>
        ///Created by :Tanabandeh 
        ///Created on Date :1392/6/25
        ///<remarks>property that get and set value from and to the OrganazationId. </remarks>
        /// </summary>
        /// <summary>
        ///Edited by :Pouyan 
        ///Edited on Date :1392/5/28 , 1392/6/9
        /// </summary>
        public int OrganazationId
        {
            get { return _organazationId; }
            set { _organazationId = value; }
        }

        ///  <summary>
        ///Created by :Azizi 
        ///Created on Date :1392/9/13
        ///<remarks>property that get and set value from and to the UserId. </remarks>
        /// </summary>
        public int UserId
        {
            get { return _userId; }
            set { _userId = value; }
        }

        ///  <summary>
        ///Created by :Pouyan 
        ///Created on Date :1392/6/19
        ///<remarks>property that get and set value from and to the IsLegal</remarks>
        /// </summary>
        public bool IsLegal
        {
            get { return _isLegal; }
            set { _isLegal = value; }
        }

        ///  <summary>
        ///Created by :Pouyan 
        ///Created on Date :1392/6/19
        ///<remarks>property that get and set value from and to the LegalId</remarks>
        /// </summary>
        public string LegalId
        {
            get { return _legalId; }
            set { _legalId = value; }
        }

        ///  <summary>
        ///Created by :Pouyan 
        ///Created on Date :1392/6/19
        ///<remarks>property that get and set value from and to the LegalId</remarks>
        /// </summary>
        public string NationalId
        {
            get { return _nationalId; }
            set { _nationalId = value; }
        }

        ///  <summary>
        ///Created by :Pouyan 
        ///Created on Date :1392/6/9
        ///<remarks>property that get and set value from and to the OrganizationName</remarks>
        /// </summary>
        public string OrganizationName
        {
            get { return _organizationName; }
            set { _organizationName = value; }
        }

        ///  <summary>
        ///Created by :Pouyan 
        ///Created on Date :1392/8/13
        ///<remarks>property that get and set value from and to the OrganizationChartName</remarks> 
        /// </summary>
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

        ///  <summary>
        ///Created by :Tabandeh 
        ///Created on Date :1392/6/25
        ///<remarks>property that get and set value from and to the OnlineUsers</remarks> 
        ///Edited by :Kiaee 
        ///Edited on Date :1392/5/26
        ///Description : 
        /// </summary>
        public List<IUserProfile> OnlineUsers
        {
            get { return _onlineUsers ?? (_onlineUsers = new List<IUserProfile>()); }
        }

        ///<summary>
        ///Created by :Pouyan 
        ///Created on Date :1392/7/21
        ///<remarks>property that get and set value from and to the AllViewElements</remarks>  
        /// </summary>
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

        ///  <summary>
        ///Created by :Tabandeh 
        ///Created on Date :1392/7/21
        ///<remarks>this method returns viewElements that granted to user. </remarks>
        ///Edited by :Alavi 
        ///Edited on Date :1392/7/21
        ///Edited by :Azizi 
        ///Edited on Date :1392/9/13
        ///</summary>

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
