using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using DesktopRepository;
using WebRepository;
using System.Data.SqlClient;
using System.Data.Entity;
using System.IO;
using Microsoft.Win32;
using System.Reflection;
using System.ComponentModel;
using BankSystemModel;

namespace BankSystemClient
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        IRepository Repository;
        Type TypeOfRepository = null;
        int ammountForAllPeriod = 0;
        int ammountForLastMounth = 0;
        DateTime? startDiapason, endDiapason;


        public MainWindow()
        {
            InitializeComponent();
            LoadRepository();          
        }

        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            switch (TypeOfRepository.FullName)
            {
                case "WebRepository.RecordCRUD":
                    {
                        if (recordsGrid.SelectedItems.Count > 0 && typeof(Record) == recordsGrid.SelectedItem.GetType())
                        {
                            Record record = (Record)recordsGrid.SelectedItem;
                            WebRepository.RecordCRUD repository = (WebRepository.RecordCRUD)Repository;
                            //WebRepository.RecordCRUD.DeleteRecord(record.Id);
                            repository.DeleteRecord(record.Id);
                            recordsGrid.ItemsSource = null;
                            recordsGrid.ItemsSource = repository.GetRecordsForBinding();

                        }
                        break;
                    }
                case "DesktopRepository.RecordCRUD":
                    {
                        if (recordsGrid.SelectedItems.Count > 0)
                        {
                            DesktopRepository.RecordCRUD repository = (DesktopRepository.RecordCRUD)Repository;                            
                            for (int i = 0; i < recordsGrid.SelectedItems.Count; i++)
                            {
                                Record record = (Record)recordsGrid.SelectedItem;
                                if (record != null)
                                {
                                    //context.RecordCRUD.DeleteRecord(record.Id);
                                    repository.DeleteRecord(record.Id);
                                }
                            }
                            recordsGrid.ItemsSource = null;
                            recordsGrid.ItemsSource = repository.GetAllRecords();
                        }
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
            
        }

        private void refreshButton_Click(object sender, RoutedEventArgs e)
        {
            switch (TypeOfRepository.FullName)
            {
                case "WebRepository.RecordCRUD":
                    {
                        if (recordsGrid.SelectedItems.Count > 0)
                        {
                            Record record = (Record)recordsGrid.SelectedItem;
                            WebRepository.RecordCRUD repository = (WebRepository.RecordCRUD)Repository;
                            repository.UpdateRecord(record);
                        }
                        break;
                    }
                case "DesktopRepository.RecordCRUD":
                    {
                        if (recordsGrid.SelectedItems.Count > 0)
                        {
                            Record record = (Record)recordsGrid.SelectedItem;
                            DesktopRepository.RecordCRUD repository = (DesktopRepository.RecordCRUD)Repository;
                            repository.UpdateRecord(record);
                            //context.RecordCRUD.UpdateRecord(record);
                        }
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
            
        }

        private void createButton_Click(object sender, RoutedEventArgs e)
        {
            switch (TypeOfRepository.FullName)
            {
                case "WebRepository.RecordCRUD":
                    {
                        if (recordsGrid.SelectedItems.Count > 0)
                        {
                            Record record = (Record)recordsGrid.SelectedItem;
                            WebRepository.RecordCRUD repository = (WebRepository.RecordCRUD)Repository;
                            repository.CreateRecord(record);
                            recordsGrid.ItemsSource = null;
                            recordsGrid.ItemsSource = repository.GetRecordsForBinding();
                        }
                        break;
                    }
                case "DesktopRepository.RecordCRUD":
                    {
                        if (recordsGrid.SelectedItems.Count > 0)
                        {
                            Record record = (Record)recordsGrid.SelectedItem;
                            DesktopRepository.RecordCRUD repository = (DesktopRepository.RecordCRUD)Repository;
                            repository.CreateRecord(record);
                            recordsGrid.ItemsSource = null;
                            recordsGrid.ItemsSource = repository.GetAllRecords();
                        }
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
            
        }

        private void dateButton_Click(object sender, RoutedEventArgs e)
        {
            startDiapason = dateStart.SelectedDate;
            endDiapason = dateEnd.SelectedDate;            

            recordsGrid.ItemsSource = null;
            switch (TypeOfRepository.FullName)
            {
                case "WebRepository.RecordCRUD":
                    {
                        WebRepository.RecordCRUD repository = (WebRepository.RecordCRUD)Repository;
                        var listOfRecords = repository.GetPeriod((DateTime)startDiapason, (DateTime)endDiapason);
                        var listBinding = new BindingList<Record>(listOfRecords);
                        recordsGrid.ItemsSource = listBinding;

                        ammountForAllPeriod = repository.GetBalanceForPeriod(listOfRecords);
                        ammountForLastMounth = repository.GetBalanceForLastMonth();
                        break;
                    }
                case "DesktopRepository.RecordCRUD":
                    {
                        DesktopRepository.RecordCRUD repository = (DesktopRepository.RecordCRUD)Repository;
                        var listOfRecords = repository.GetPeriod((DateTime)startDiapason, (DateTime)endDiapason);
                        var listBinding = new BindingList<Record>(listOfRecords);
                        recordsGrid.ItemsSource = listBinding;

                        ammountForAllPeriod = repository.GetBalanceForPeriod(listOfRecords);
                        ammountForLastMounth = repository.GetBalanceForLastMonth();
                        break;
                    }
                default:
                    {
                        break;
                    }
            }

            resultForPeriodTextBlock.Visibility = Visibility.Visible;
            returnToStartListButton.Visibility = Visibility.Visible;
            printButton.Visibility = Visibility.Visible;
            resultForPeriodTextBlock.Text = String.Format("Сумма за весь указанный период: {0} . Сумма за последний месяц: {1} .", ammountForAllPeriod, ammountForLastMounth);

        }

        private void returnToStartListButton_Click(object sender, RoutedEventArgs e)
        {
            resultForPeriodTextBlock.Visibility = Visibility.Hidden;
            returnToStartListButton.Visibility = Visibility.Hidden;
            printButton.Visibility = Visibility.Hidden;
            switch (TypeOfRepository.FullName)
            {
                case "WebRepository.RecordCRUD":
                    {
                        WebRepository.RecordCRUD repository = (WebRepository.RecordCRUD)Repository;
                        recordsGrid.ItemsSource = repository.GetRecordsForBinding();
                        break;
                    }
                case "DesktopRepository.RecordCRUD":
                    {
                        //context = new MysqlDbContext();
                        DesktopRepository.RecordCRUD repository = (DesktopRepository.RecordCRUD)Repository;
                        recordsGrid.ItemsSource = repository.GetAllRecords();
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
        }

        private void printButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            string filename = "";
            Nullable<bool> result = openFileDialog.ShowDialog();
            if (result == true)
            {
                filename = openFileDialog.FileName;
            }
            List<Record> listOfRecords = null;
            switch (TypeOfRepository.FullName)
            {
                case "WebRepository.RecordCRUD":
                    {
                        WebRepository.RecordCRUD repository = (WebRepository.RecordCRUD)Repository;
                        listOfRecords = repository.GetPeriod((DateTime)startDiapason, (DateTime)endDiapason);                        
                        break;
                    }
                case "DesktopRepository.RecordCRUD":
                    {
                        DesktopRepository.RecordCRUD repository = (DesktopRepository.RecordCRUD)Repository;
                        listOfRecords = repository.GetPeriod((DateTime)startDiapason, (DateTime)endDiapason);
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
            using (StreamWriter sw = new StreamWriter(filename, false, System.Text.Encoding.Default))
            {
                sw.WriteLine("     {0,25}{1,15}{2,20}{3,25}", "Операция", "Сумма", "Дата", "Операция поступления");
                int i = 1;
                foreach (Record rec in listOfRecords)
                {
                    sw.WriteLine("{0, 5}{1,25},{2,15},{3,20}{4,25}", i, rec.NameOfObject, rec.Ammount, rec.Date.ToLongDateString(), rec.IsIncome);
                    i++;
                }
                sw.WriteLine();
                sw.WriteLine("Сумма за весь указанный период: {0} . Сумма за последний месяц: {1} .", ammountForAllPeriod, ammountForLastMounth);
            }
        }

        private void changeRepositoryButton_Click(object sender, RoutedEventArgs e)
        {
            LoadRepository();
        }

        private void LoadRepository()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Библиотека dll (*.dll)|*.dll";
            Nullable<bool> result = openFileDialog.ShowDialog();
            if (result == true)
            {
                string filename = openFileDialog.FileName;
                Assembly assembly = Assembly.LoadFrom(@filename);
                Type[] types = assembly.GetTypes();
                foreach (Type t in types)
                {
                    if (t.Name == "RecordCRUD")
                    {
                        TypeOfRepository = assembly.GetType(t.FullName, false, true);
                    }
                }
            }
            recordsGrid.ItemsSource = null;
            switch (TypeOfRepository.FullName)
            {
                case "WebRepository.RecordCRUD":
                    {
                        //WebRepository.RecordCRUD.Initialize();
                        Repository = new WebRepository.RecordCRUD();
                        WebRepository.RecordCRUD repository = (WebRepository.RecordCRUD)Repository;
                        recordsGrid.ItemsSource = repository.GetRecordsForBinding();
                        break;
                    }
                case "DesktopRepository.RecordCRUD":
                    {
                        //context = new MysqlDbContext();
                        Repository = new DesktopRepository.RecordCRUD();
                        DesktopRepository.RecordCRUD repository = (DesktopRepository.RecordCRUD)Repository;
                        recordsGrid.ItemsSource = repository.GetAllRecords();
                        //recordsGrid.ItemsSource = context.RecordCRUD.GetRecords();
                        break;
                    }
                default:
                    {
                        break;
                    }
            }

        }

    }
}
