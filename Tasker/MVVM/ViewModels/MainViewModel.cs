using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tasker.MVVM.Models;

namespace Tasker.MVVM.ViewModels
{
    [AddINotifyPropertyChangedInterface]
    public class MainViewModel
    {
        public ObservableCollection<Category> Categories { get; set; }

        public ObservableCollection<MyTask> Tasks { get; set; }

        public MainViewModel()
        {
            FillData();
        }

        private void FillData()
        {
            Categories = new ObservableCollection<Category>
            {
                new Category
                {
                    Id = 1,
                    CategoryName = ".NET MAUI Course",
                    Color = "#CF14DF"
                },
                new Category
                {
                    Id = 2,
                    CategoryName = "Tutorials",
                    Color = "#df6f14"
                },
                new Category
                {
                    Id = 3,
                    CategoryName = "Shopping",
                    Color = "#14df80"
                }
            };

            Tasks = new ObservableCollection<MyTask>
            {
                new MyTask
                {
                    TaskName = "Upload exercise files",
                    Completed = false,
                    CatogoryId = 1
                },
                new MyTask
                {
                    TaskName = "Plan next course",
                    Completed = false,
                    CatogoryId = 1
                },
                new MyTask
                {
                    TaskName = "Upload new ASP.NET video on YouTube",
                    Completed = false,
                    CatogoryId = 2
                },
                new MyTask
                {
                    TaskName = "Fix Setting.cs class of the Project",
                    Completed = false,
                    CatogoryId = 2
                },
                new MyTask
                {
                    TaskName = "Update github repository",
                    Completed = true,
                    CatogoryId = 2
                },
                new MyTask
                {
                    TaskName = "Buy Product",
                    Completed = false,
                    CatogoryId = 3
                },
                new MyTask
                {
                    TaskName = "Go for the pepperoni pizza",
                    Completed = false,
                    CatogoryId = 3
                }
            };

            UpdateData();   //  Get Pending Tasks / Percentage and TaskColor
        }

        public void UpdateData()
        {
            foreach (var c in Categories)
            {
                var tasks = from t in Tasks
                            where t.CatogoryId == c.Id
                            select t;

                var completed = from t in tasks
                                where t.Completed == true
                                select t;

                var notCompleted = from t in tasks
                                   where t.Completed == false
                                   select t;

                c.PendingTasks = notCompleted.Count();
                c.Percentage = (float)completed.Count() / (float)tasks.Count();
            }
            foreach (var t in Tasks)
            {
                var catColor =
                    (from c in Categories
                     where c.Id == t.CatogoryId
                     select c.Color).FirstOrDefault();
                t.TaskColor = catColor;
            }
        }
    }
}
