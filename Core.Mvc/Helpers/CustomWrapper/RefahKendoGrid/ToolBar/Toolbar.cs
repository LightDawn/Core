using Core.Mvc.Helpers.CustomWrapper.DataSource;
using Core.Mvc.Helpers.CustomWrapper.Infrastructure;
using Core.Mvc.Helpers.RefahKendoGrid.Infrastructure;
using Core.Mvc.Helpers.RefahKendoGrid.Settings.ColumnConfig;
using Kendo.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Mvc.Helpers.RefahKendoGrid
{
    [Serializable()]
    public class Toolbar :JsonObjectBase
    {
        public Toolbar()
        {
            CRUDOperation = new AccessOperation(); 
        }

        internal Toolbar(bool makeDefaultCommands = false)
        {
            CRUDOperation = new AccessOperation();
            if (makeDefaultCommands)
            {
                Commands = GetDefaultCommandList();
            }
        }

        internal List<ColumnCommand> GetDefaultCommandList(List<ColumnCommand> commandColumns = null)
        {
            var commands = commandColumns ?? new List<ColumnCommand>();

            if (CRUDOperation.ReadOnly)
            {
                return commands.Count > 0 ? new List<ColumnCommand>() : commands;
            }
            else
            {
                if (commands.Count == 0)
                {
                    var commandDic = new Dictionary<GCommandRP, string>();
                    if (CRUDOperation.Insertable && CRUDOperation.Updatable && CRUDOperation.Removable)
                    {
                        commands = new List<ColumnCommand> { new ColumnCommand { Name = GCommandRP.Create , Text = "جدید" },
                                                             new ColumnCommand { Name = GCommandRP.Edit , Text = "ویرایش" },
                                                             new ColumnCommand { Name = GCommandRP.Delete , Text = "حذف"  } ,
                                                             new ColumnCommand { Name = GCommandRP.Refresh , Text = ""  } ,
                        };
                    }
                    else if (CRUDOperation.Insertable && !CRUDOperation.Updatable && CRUDOperation.Removable )
                    {
                        commands = new List<ColumnCommand> { new ColumnCommand { Name = GCommandRP.Create , Text = "جدید" },
                                                         new ColumnCommand { Name = GCommandRP.Delete , Text = "حذف"  }};
                    }
                    else if (CRUDOperation.Insertable && CRUDOperation.Updatable && !CRUDOperation.Removable)
                    {
                        commands = new List<ColumnCommand> { new ColumnCommand { Name = GCommandRP.Create , Text = "جدید" },
                                                         new ColumnCommand { Name = GCommandRP.Edit , Text = "ویرایش" }};
                    }
                    else if (!CRUDOperation.Insertable && CRUDOperation.Updatable && CRUDOperation.Removable)
                    {
                        commands = new List<ColumnCommand> { new ColumnCommand { Name = GCommandRP.Edit , Text = "ویرایش" },
                                                         new ColumnCommand { Name = GCommandRP.Delete , Text = "حذف"  }};
                    }
                    else if (!CRUDOperation.Insertable && !CRUDOperation.Updatable && CRUDOperation.Removable)
                    {
                        commands = new List<ColumnCommand> { new ColumnCommand { Name = GCommandRP.Delete, Text = "حذف" } };
                    }
                    else if (!CRUDOperation.Insertable && CRUDOperation.Updatable && !CRUDOperation.Removable)
                    {
                        commands = new List<ColumnCommand> { new ColumnCommand { Name = GCommandRP.Edit, Text = "ویرایش" } };
                    }
                    else if (CRUDOperation.Insertable && !CRUDOperation.Updatable && !CRUDOperation.Removable)
                    {
                        commands = new List<ColumnCommand> { new ColumnCommand { Name = GCommandRP.Create, Text = "جدید" } };
                    }
                    if (!CRUDOperation.Refreshable)
                    {
                        commands = commands.Where(com => com.Name != GCommandRP.Refresh).ToList();
                    }
                    commands.Add(new ColumnCommand { Name = GCommandRP.Custom, Text = "" });
                    commands.Add(new ColumnCommand { Name = GCommandRP.Search, Text = "جستجو" });
                    commands.Add(new ColumnCommand { Name = GCommandRP.UserGuide, Text = "راهنما" });
                    return commands;
                }
                else
                {
                    FilterCommandColumnsBasedOnCRUDOperation(commands);
                }
                return commands;
            }
        }

        private void FilterCommandColumnsBasedOnCRUDOperation(List<ColumnCommand> commands)
        {
            if (CRUDOperation.Insertable && !CRUDOperation.Updatable && CRUDOperation.Removable)
            {
                commands = commands.Where(com => com.Name == GCommandRP.Create || com.Name == GCommandRP.Delete).ToList();//&& com.Name != GCommandRP.Edit 
            }
            else if (CRUDOperation.Insertable && CRUDOperation.Updatable && !CRUDOperation.Removable)
            {
                commands = commands.Where(com => com.Name == GCommandRP.Create || com.Name == GCommandRP.Edit).ToList();
            }
            else if (!CRUDOperation.Insertable && CRUDOperation.Updatable && CRUDOperation.Removable)
            {
                commands = commands.Where(com => com.Name == GCommandRP.Edit || com.Name == GCommandRP.Delete).ToList();
            }
            else if (!CRUDOperation.Insertable && !CRUDOperation.Updatable && CRUDOperation.Removable)
            {
                commands = commands.Where(com => com.Name == GCommandRP.Delete).ToList();
            }
            else if (!CRUDOperation.Insertable && CRUDOperation.Updatable && !CRUDOperation.Removable)
            {
                commands = commands.Where(com => com.Name == GCommandRP.Edit).ToList();
            }
            else if (CRUDOperation.Insertable && !CRUDOperation.Updatable && !CRUDOperation.Removable)
            {
                commands = commands.Where(com => com.Name == GCommandRP.Create).ToList();
            }
            if (!CRUDOperation.Refreshable)
            {
                commands = commands.Where(com => com.Name != GCommandRP.Refresh).ToList();
            }
        }

        internal AccessOperation CRUDOperation { get; set; }

        public Toolbar(Dictionary<GCommandRP,string> commandItems)
        {
            Commands = new List<ColumnCommand>();
            if (commandItems.Any())
            {
                foreach (var item in commandItems)
                {
                    switch (item.Key)
                    {
                        case GCommandRP.Create:
                            Commands.Add(MakeToolbarDefaultCommands(item.Key , item.Value , "جدید"));
                            break;
                        case GCommandRP.Edit:
                            Commands.Add(MakeToolbarDefaultCommands(item.Key, item.Value, "ویرایش"));
                            break;
                        case GCommandRP.Delete:
                            Commands.Add(MakeToolbarDefaultCommands(item.Key, item.Value, "حذف"));
                            break;
                        case GCommandRP.Refresh:
                            Commands.Add(MakeToolbarDefaultCommands(item.Key, "" , ""));
                            break;
                        case GCommandRP.Custom:
                            Commands.Add(MakeToolbarDefaultCommands(item.Key, "", ""));
                            break;
                        case GCommandRP.Search:
                            Commands.Add(MakeToolbarDefaultCommands(item.Key, item.Value , "جستجو"));
                            break;
                        case GCommandRP.UserGuide:
                            Commands.Add(MakeToolbarDefaultCommands(item.Key, item.Value, "راهنما"));
                            break;
                        default:
                            break;
                    }
                }
            }
            
        }

        private ColumnCommand MakeToolbarDefaultCommands(GCommandRP commandType, string commandText , string defaultString)
        {
            return new ColumnCommand { Name = commandType , Text = string.IsNullOrEmpty(commandText) ? defaultString : commandText };
        }
        
        public List<ColumnCommand> Commands { get; set; }



        protected override void Serialize(IDictionary<string, object> json)
        {
            
        }
    }
}
