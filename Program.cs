using System;
using System.Collections.Generic;
using System.Linq;

namespace FinalCode2
{

    class Category
    {
        // create category class 
        private string _categoryName;

        //create constructor
        public Category(string categoryName)
        {
            _categoryName = categoryName;
        }

        public string CategoryName
        {
            get { return _categoryName; }
        }
    }


    class Task : Category
    {
        // create task class that inherits from category
        // set up instance variables
        private string _categoryName;
        private string _taskName;
        private DateTime _dueDate;
        private string _highlight;

        //create constructor
        public Task(string taskName, string categoryName, string highlight = "blue") : base(categoryName)
        {
            _categoryName = categoryName;
            _taskName = taskName;
            _highlight = highlight;
        }

        // return methods
        public void SetCategoryName(string categoryName)
        {
            _categoryName = categoryName;
        }

        public string TaskName
        {
            get { return _taskName; }
        }

        public DateTime DueDate
        {
            get { return _dueDate; }
            set { _dueDate = value; }
        }

        public string HighLight
        {
            get { return _highlight; }
            set { _highlight = value; }
        }

        // create method to set a due date for a task
        public void SetDueDate()
        {
            Console.WriteLine();
            Console.WriteLine("You have selected to add a due date to this task.");
            Console.WriteLine();
            Console.WriteLine("Enter the new due date:");
            Console.Write("Due Day [DD] >> ");
            try
            {
                int day = Convert.ToInt32(Console.ReadLine());
                Console.Write("Due Month [MM] >> ");
                try
                {
                    int month = Convert.ToInt32(Console.ReadLine());
                    Console.Write("Due Year [YYYY] >> ");
                    try
                    {
                        int year = Convert.ToInt32(Console.ReadLine());
                        // create new datetime object
                        DateTime newDueDate = new DateTime(year, month, day);
                        //set _dueDate variable to new datetime object
                        _dueDate = newDueDate;
                        Console.WriteLine("Due Date Added.");
                    }
                    catch (InvalidOperationException)
                    {
                        throw new InvalidOperationException("Year is invalid.");
                    }
                }
                catch (InvalidOperationException)
                {
                    throw new InvalidOperationException("Month is invalid.");
                }
            }
            catch (InvalidOperationException)
            {
                throw new InvalidOperationException("Day is invalid.");
            }
        }


        // create method to set highlight on task
        public void SetHighlight()
        {
            Console.WriteLine();
            Console.WriteLine("You have selected to highlight this task.");
            Console.WriteLine();

            // check if task has already been highlighted
            if (_highlight == "red")
            {
                Console.WriteLine("The task has already been highlighted.");
                Console.WriteLine();
                Console.Write("Would you like to remove the highlight [Y/N] >> ");
                try
                {
                    char removeHighlight = Convert.ToChar(Console.ReadLine());
                    removeHighlight = char.ToUpper(removeHighlight);
                    
                    // remove highlighting
                    if (removeHighlight.Equals('Y'))
                    {
                        // remove the highlighting
                        _highlight = "blue";
                    }
                    else if (!removeHighlight.Equals('N'))
                    {
                        throw new InvalidOperationException("Invalid input.");
                    }
                }
                catch 
                {
                    throw new InvalidOperationException("Invalid input");
                }      
            }
            else
            {
                // set highlight to red
            _highlight = "red";
            Console.WriteLine("Task Highlighted.");
            }
        }
    }


    // create TasksIndividual class to stored two lists. One for categories and another with lists of tasks
    class TasksIndividual
    {
        // create two lists, one containing lists of tasks, the other a list of categories
        private List<List<Task>> _tasksIndividual;
        private List<Category> _categories;

        public TasksIndividual()
        {
            _tasksIndividual = new List<List<Task>> { };
            _categories = new List<Category> { };
        }

        // return methods
        public List<List<Task>> TaskList { get { return _tasksIndividual; } }
        public List<Category> CategoryList { get { return _categories; } }



        // create method to search for task in list by name and return the task object
        public Task ReturnTask(string taskName)
        {
            foreach (List<Task> list in _tasksIndividual)
            {
                foreach (Task task in list)
                {
                    if (taskName == task.TaskName)
                    {
                        return task;
                    }
                }
            }
            throw new InvalidOperationException("Task does not exist");
        }


        // create method to search for category in list by name and return the task object
        public Category ReturnCategory(string categoryName)
        {
            foreach (Category item in _categories)
            {
                if (categoryName == item.CategoryName)
                {
                    return item;
                }
            }
            throw new InvalidOperationException("Category does not exist");
        }


        // create method that takes a task name as a string and an existing category name
        //as a string. Create new task object and add to the list in tasksIndividual that corresponds
        // to the category
        public void AddTask(string taskName, string categoryName)
        {
            // create new task and category objects and add each to its respective list

            Category category = ReturnCategory(categoryName);

            int categoryIndex = _categories.IndexOf(category);

            Task task = new Task(taskName, category.CategoryName);

            _tasksIndividual[categoryIndex].Add(task);

        }


        // create a method to remove a task from the list
        public void DeleteTask(string taskName)
        {

            Task task = ReturnTask(taskName);

            foreach (List<Task> item in _tasksIndividual)
            {
                if (item.Contains(task))
                {
                    item.Remove(task);
                }
            }       
        }


        // create a method that will move a task from one category to another
        public void MoveTask(string taskName, string categoryName)
        {
            Task task = ReturnTask(taskName);
            Category category = ReturnCategory(categoryName);

            // change the category name
            task.SetCategoryName(categoryName);

            int categoryIndex = _categories.IndexOf(category);

            foreach (List<Task> item in _tasksIndividual)
            {
                if (item.Contains(task))
                {
                    // remove task from current list
                    item.Remove(task);                    
                }
            }

            // add task to new list
            _tasksIndividual[categoryIndex].Add(task);

        }
           

        public void ReorderTask(string taskName, int newIndex)
        {

            Task task = ReturnTask(taskName);

            foreach (List<Task> item in _tasksIndividual)
            {
                if (item.Contains(task))
                {
                    if (newIndex < item.Count)
                    {
                        // remove task at previous index and add back in at user specified index
                        int oldIndex = item.IndexOf(task);
                        item.RemoveAt(oldIndex);
                        item.Insert(newIndex, task);
                    }
                    else
                    {
                        throw new InvalidOperationException("Index out of range");
                    }                     
                }
            }
        }

        public void HighlightTask(string taskName)
        {

            Task task = ReturnTask(taskName);

            // call SetHighlight() method
            try
            {
                task.SetHighlight();
            }
            catch (InvalidOperationException e)
            {
                Console.WriteLine(e.Message);
            }

        }

        public void SetDueDate(string taskName)
        {

            Task task = ReturnTask(taskName);

            task.SetDueDate();

        }

        public void AddCategory(string category)
        { 
            try
            {
                // check if category already already exists
                Category categoryName = ReturnCategory(category);
                Console.WriteLine("Category already exists.");
            }
            catch (InvalidOperationException)
            {
                // if not, add it and create new list empty task list to correspond
                Category newCategory = new Category(category);
                _categories.Add(newCategory);
                List<Task> newTaskList = new List<Task> { };
                _tasksIndividual.Add(newTaskList);
            }
        }


        public void DeleteCategory(string categoryName)
        {
            // delete the cateogry from the category list and the task lists from the same index in tasksIndividual
            Category categoryToDelete = ReturnCategory(categoryName);
            int categoryIndex = _categories.IndexOf(categoryToDelete);
            _categories.Remove(categoryToDelete);
            _tasksIndividual.RemoveAt(categoryIndex);

        }
    }

    class Program
    {
        enum MenuOptions
        {
            AddTask = 1,
            AddCategory,
            DeleteCategory,
            ModifyTask,
            Quit
        }

        enum TaskOptions
        {
            ChangeDueDate = 1,
            MoveTask,
            HighlightTask,
            DeleteTask,
            ReOrderTask,
            BackToMain
        }
        static void Main(string[] args)
        {
            TasksIndividual tasksIndividual = new TasksIndividual();

            // add first three categories
            tasksIndividual.AddCategory("Family");
            tasksIndividual.AddCategory("Work");
            tasksIndividual.AddCategory("Personal");

            DisplayConsole(tasksIndividual);

            int option = ReadMenuOption(0, tasksIndividual);

            do
            {
                

                switch (option)
                {
                    case (int)MenuOptions.AddTask:

                        Console.WriteLine();
                        Console.WriteLine("You Have Selected to Add a Task.");


                        Console.WriteLine();
                        Console.Write("Enter the Name of the Task >> ");

                        string taskName;
                        string categoryName;

                        try
                        {
                            taskName = FormatName(Console.ReadLine());
                            Console.WriteLine();
                            Console.Write("Enter the Name of Category to Add the Task to (");
                            DisplayCategories(tasksIndividual);
                            try
                            {
                                categoryName = FormatName(Console.ReadLine());
                                try
                                {
                                    tasksIndividual.AddTask(taskName, categoryName);
                                    Console.WriteLine("Task added.");
                                    Console.ReadLine();
                                }
                                catch (InvalidOperationException e)
                                {
                                    Console.WriteLine(e.Message);
                                    Console.ReadLine();
                                }

                            }
                            catch (InvalidOperationException e)
                            {
                                Console.WriteLine(e.Message);
                            }
                        }
                        catch (InvalidOperationException e)
                        {
                            Console.WriteLine(e.Message);
                            Console.ReadLine();
                        }

                        DisplayConsole(tasksIndividual);
                        option = ReadMenuOption(0, tasksIndividual);

                        break;

                    case (int)MenuOptions.AddCategory:

                        Console.WriteLine();
                        Console.WriteLine("You Have Selected to Add a Category.");

                        Console.WriteLine();
                        Console.Write("Enter the Name of the New Category >> ");

                        try
                        {
                            categoryName = FormatName(Console.ReadLine());
                            tasksIndividual.AddCategory(categoryName);
                            Console.WriteLine("Category Added");
                            Console.ReadLine();
                        }
                        catch (InvalidOperationException e)
                        {
                            Console.WriteLine(e.Message);
                            Console.ReadLine();
                        }

                        DisplayConsole(tasksIndividual);
                        option = ReadMenuOption(0, tasksIndividual);

                        break;

                    case (int)MenuOptions.DeleteCategory:

                        Console.WriteLine();
                        Console.WriteLine("You Have Selected to Delete a Category.");

                        Console.WriteLine();
                        Console.Write("Enter the Name of Category to Delete (");
                        DisplayCategories(tasksIndividual);

                        try
                        {
                            categoryName = FormatName(Console.ReadLine());
                            tasksIndividual.DeleteCategory(categoryName);
                            Console.WriteLine("Category Deleted.");
                            Console.ReadLine();
                        }
                        catch (InvalidOperationException e)
                        {
                            Console.WriteLine(e.Message);
                            Console.ReadLine();
                        }

                        DisplayConsole(tasksIndividual);
                        option = ReadMenuOption(0, tasksIndividual);

                        break;

                    case (int)MenuOptions.ModifyTask:

                        // ask for task name and bring up separate menu to make modifications to it 

                        Console.WriteLine();
                        Console.WriteLine("You Have Selected to Modify a Task");

                        Console.WriteLine();
                        Console.Write("Enter the name of the task you wish to modify >> ");

                        int taskOption;

                        try
                        {
                            taskName = FormatName(Console.ReadLine());

                            try
                            {
                                Task taskToModify = tasksIndividual.ReturnTask(taskName);
                                Console.WriteLine("Task Found.");
                                Console.ReadLine();

                                DisplayConsole(tasksIndividual);

                                taskOption = ReadMenuOption(1, tasksIndividual);

                                do
                                {

                                    switch (taskOption)
                                    {
                                        case (int)TaskOptions.ChangeDueDate:

                                            try
                                            {
                                                tasksIndividual.SetDueDate(taskName);
                                                Console.ReadLine();
                                            }
                                            catch (InvalidOperationException e)
                                            {
                                                Console.WriteLine(e.Message);
                                                Console.ReadLine();
                                            }
                                            
                                            DisplayConsole(tasksIndividual);
                                            taskOption = ReadMenuOption(1, tasksIndividual);

                                            break;

                                        case (int)TaskOptions.MoveTask:


                                            Console.WriteLine();
                                            Console.WriteLine("You have selected to Move the Task.");

                                            Console.WriteLine();
                                            Console.Write("Enter the Name of the New Category >> ");

                                            try
                                            {
                                                categoryName = FormatName(Console.ReadLine());
                                                tasksIndividual.MoveTask(taskName, categoryName);
                                                Console.WriteLine("Task Moved.");
                                                Console.ReadLine();
                                            }
                                            catch (InvalidOperationException e)
                                            {
                                                Console.WriteLine(e.Message);
                                                Console.ReadLine();
                                            }


                                            DisplayConsole(tasksIndividual);
                                            taskOption = ReadMenuOption(1, tasksIndividual);

                                            break;

                                        case (int)TaskOptions.HighlightTask:

                                            try
                                            {
                                                tasksIndividual.HighlightTask(taskName);
                                                Console.ReadLine();
                                            }
                                            catch (InvalidOperationException e)
                                            {
                                                Console.WriteLine(e.Message);
                                                Console.ReadLine();
                                            }


                                            DisplayConsole(tasksIndividual);
                                            taskOption = ReadMenuOption(1, tasksIndividual);

                                            break;

                                        case (int)TaskOptions.DeleteTask:

                                            try
                                            {
                                                tasksIndividual.DeleteTask(taskName);
                                                Console.ReadLine();
                                            }
                                            catch (InvalidOperationException e)
                                            {
                                                Console.WriteLine(e.Message);
                                                Console.ReadLine();
                                            }



                                            DisplayConsole(tasksIndividual);
                                            taskOption = ReadMenuOption(1, tasksIndividual);

                                            break;

                                        case (int)TaskOptions.ReOrderTask:

                                            Console.WriteLine();
                                            Console.WriteLine("You Have Selected to Re-Order the Task");
                                            Console.WriteLine();
                                            Console.Write("Enter the Item # That You Would Like the Task to Appear Next to >> ");

                                            if (int.TryParse(Console.ReadLine(), out int newIndex))
                                            {
                                                try
                                                {
                                                    tasksIndividual.ReorderTask(taskName, newIndex);
                                                    Console.WriteLine("Task Re-Ordered.");
                                                }
                                                catch (InvalidOperationException e)
                                                {
                                                    Console.WriteLine(e.Message);
                                                    Console.ReadLine();
                                                }
                                                
                                            }
                                            else
                                            {
                                                Console.WriteLine("Invalid input.");
                                                Console.ReadLine();
                                            }

                                            DisplayConsole(tasksIndividual);
                                            taskOption = ReadMenuOption(1, tasksIndividual);

                                            break;

                                        case (int)TaskOptions.BackToMain:
                                            break;

                                    }
                                } while (taskOption != (int)TaskOptions.BackToMain);
                            }
                            catch (InvalidOperationException e)
                            {
                                Console.WriteLine(e.Message);
                            }
                        }
                        catch (InvalidOperationException e)
                        {
                            Console.WriteLine(e.Message);
                        }


                        Console.WriteLine("Returning to Main Menu...");

                        Console.ReadLine();
                        DisplayConsole(tasksIndividual);
                        option = ReadMenuOption(0, tasksIndividual);
                        break;

                    case (int)MenuOptions.Quit:
                        break;
                }
            } while (option != (int)MenuOptions.Quit);






            // create a loop to display the names of the current categories in the categories list

            static void DisplayCategories(TasksIndividual tasksIndividual)
            {
                List<Category> categories = tasksIndividual.CategoryList;

                for (int i = 0; i < categories.Count; i++)
                {
                    Console.Write("{0}", i == categories.Count - 1 ? categories[i].CategoryName + ")" : categories[i].CategoryName + ", ");
                }
                Console.Write(" >> ");
            }




            // format the user input

            static string FormatName(string name)
            {
                try
                {
                    return (char.ToUpper(name[0]) + name.Substring(1));
                }
                catch
                {
                    throw new InvalidOperationException("Invalid input");
                }
                
            }










            static void DisplayConsole(TasksIndividual tasksIndividual)
            {
                Console.Clear();
                Console.WriteLine();

                string dashedLine = new string(' ', 10) + new string('-', tasksIndividual.CategoryList.Count * 40);

                Console.ForegroundColor = ConsoleColor.Blue;

                Console.WriteLine(new string(' ', 60) + "CATEGORIES");
                Console.WriteLine(dashedLine);
                Console.Write("{0,10}", "item #");

                // display the category names
                foreach (Category categoryName in tasksIndividual.CategoryList)
                {
                    Console.Write("\t\t{0,-25}|", categoryName.CategoryName.ToString());
                }
                Console.WriteLine();
                Console.WriteLine(dashedLine);

                int[] numberofTasks = new int[tasksIndividual.TaskList.Count];

                for (int i = 0; i < tasksIndividual.TaskList.Count; i++)
                {
                    numberofTasks[i] = tasksIndividual.TaskList[i].Count;
                }

                int maxTasksInACategory = numberofTasks.Max();

                // loop through each list in tasks individual and print out to the console
                // if value empty, print 'N/A'
                for (int i = 0; i < maxTasksInACategory; i++)
                {
                    Console.Write("{0,10}|", i);

                    foreach (List<Task> item in tasksIndividual.TaskList)
                    {
                        if (item.Count > i)
                        {
                            string taskName = item[i].TaskName;

                            if (item[i].TaskName.Length > 23)
                            {
                                taskName = item[i].TaskName.Substring(0,23);
                            }
                            
                            if (item[i].HighLight == "red")
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                if (item[i].DueDate != default(DateTime))
                                {
                                    Console.Write("\t{0,-23} {1, 9}", taskName, item[i].DueDate.ToShortDateString());
                                }
                                else
                                {                                                        
                                    Console.Write("\t\t{0,-25}", taskName);
                                }
                                
                                Console.ForegroundColor = ConsoleColor.Blue;
                                Console.Write("|");
                            }
                            else
                            {
                                if (item[i].DueDate != default(DateTime))
                                {
                                    Console.Write("\t{0,-23} {1, -9}|", taskName, item[i].DueDate.ToShortDateString());
                                }
                                else
                                {
                                    Console.Write("\t\t{0,-25}|", taskName);
                                } 
                            }
                        }
                        else
                        {
                            Console.Write("\t\t{0,-25}|", "N/A");
                        }
                    }
                    Console.WriteLine();
                }

                Console.ResetColor();
            }






            static int ReadMenuOption(int menuNumber, TasksIndividual tasksIndividual)
            {

                bool isValidInput = false;
                do
                {
                    if (menuNumber == 0)
                    {
                        DisplayMenuZero();
                    }
                    else
                    {
                        DisplayMenuOne();
                    }

                    Console.WriteLine();
                    Console.Write(">> ");

                    if (int.TryParse(Console.ReadLine(), out int selection))
                    {
                        if (menuNumber == 0)
                        {
                            if (selection > 0 & selection < 6)
                            {
                                return selection;
                            }
                            else
                            {
                                Console.WriteLine("Invalid Input. Please Select Another Number.");
                                Console.ReadLine();
                            }
                        }
                        else
                        {
                            if (selection > 0 & selection < 7)
                            {
                                return selection;
                            }
                            else
                            {
                                Console.WriteLine("Invalid Input. Please Select Another Number.");
                                Console.ReadLine();
                            }
                        }
                    }
                    
                    DisplayConsole(tasksIndividual);

                } while (!isValidInput);
                return 0;
            }




            static void DisplayMenuZero()
            {
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine("Please Select an Option (1 - 5) from the following: ");
                Console.WriteLine();
                Console.WriteLine("1. Add a Task");
                Console.WriteLine("2. Add a Category");
                Console.WriteLine("3. Delete a Category");
                Console.WriteLine("4. Modify an Existing Task");
                Console.WriteLine("5. Quit");
            }

            static void DisplayMenuOne()
            {
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine("Please Select an Option (1 - 6) from the following: ");
                Console.WriteLine();
                Console.WriteLine("1. Add Task Due Date");
                Console.WriteLine("2. Move Task to Another Category");
                Console.WriteLine("3. Highlight Task");
                Console.WriteLine("4. Delete Task");
                Console.WriteLine("5. Re-Order Task");
                Console.WriteLine("6. Exit to Main Menu");
            }
        }
    }
}
