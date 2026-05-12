using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows;
using Microsoft.Win32;

namespace asd
{
    public partial class MainWindow : Window
    {
        private ObservableCollection<BloodSugarEntry> Entries { get; } = new();

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
            listBox.ItemsSource = Entries;

           
            for (int i = 1; i <= 30; i++) 
            napsorszam.Items.Add(i);
            napszak.Items.Add("reggel");
            napszak.Items.Add("délben");
            napszak.Items.Add("este");
            etkezesiIdo.Items.Add("Éhgyomorra (>8 óra koplalás)");
            etkezesiIdo.Items.Add("Étkezés után 2 órával");

            napsorszam.SelectedIndex = 0;
            napszak.SelectedIndex = 0;
            etkezesiIdo.SelectedIndex = 0;
        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            if (napsorszam.SelectedItem == null)
            {
                MessageBox.Show("Válassza ki a nap sorszámát!", "Hiba");
                return;
            }
            int day = (int)napsorszam.SelectedItem;
            if (day < 1 || day > 30)
            {
                MessageBox.Show("A nap sorszáma csak 1 és 30 között lehet.", "Hiba");
                return;
            }

            
            if (napszak.SelectedItem == null)
            {
                MessageBox.Show("Válassza ki a napszakot (reggel, délben, este).", "Hiba");
                return;
            }
            string timeOfDay = napszak.SelectedItem.ToString();

        
            if (etkezesiIdo.SelectedItem == null)
            {
                MessageBox.Show("Válassza ki, hogy éhgyomorra vagy étkezés után 2 órával történt a mérés.", "Hiba");
                return;
            }
            string timing = etkezesiIdo.SelectedItem.ToString();

        
            string raw = ertekTextBox.Text?.Trim() ?? "";
            if (string.IsNullOrEmpty(raw))
            {
                MessageBox.Show("Adjon meg egy értéket 0,0 - 40,0 mmol/L között.", "Hiba");
                return;
            }
            
            raw = raw.Replace(',', '.');
            if (!double.TryParse(raw, NumberStyles.Float, CultureInfo.InvariantCulture, out double value))
            {
                MessageBox.Show("Az érték nem érvényes szám.", "Hiba");
                return;
            }
            if (value < 0.0 || value > 40.0)
            {
                MessageBox.Show("Az értéknek 0,0 és 40,0 között kell lennie.", "Hiba");
                return;
            }

            bool exists = Entries.Any(ei => ei.Day == day && ei.TimeOfDay == timeOfDay && ei.Timing == timing);
            if (exists)
            {
                MessageBox.Show("Erre az időpontra már van felvett adat!", "Duplikátum");
                return;
            }

            var entry = new BloodSugarEntry(day, timeOfDay, timing, value);

           
            int insertIndex = 0;
            while (insertIndex < Entries.Count && CompareEntries(Entries[insertIndex], entry) <= 0) insertIndex++;
            Entries.Insert(insertIndex, entry);

            
            ertekTextBox.Clear();
        }

        private int CompareEntries(BloodSugarEntry a, BloodSugarEntry b)
        {
            int c = a.Day.CompareTo(b.Day);
            if (c != 0) return c;
            int orderA = TimeOrder(a.TimeOfDay);
            int orderB = TimeOrder(b.TimeOfDay);
            if (orderA != orderB) return orderA.CompareTo(orderB);
            int timingA = TimingOrder(a.Timing);
            int timingB = TimingOrder(b.Timing);
            return timingA.CompareTo(timingB);
        }

        private int TimeOrder(string t)
        {
            return t switch
            {
                "reggel" => 0,
                "délben" => 1,
                "este" => 2,
                _ => 3
            };
        }

        private int TimingOrder(string s)
        {
            return s.StartsWith("Éhgyomorra") ? 0 : 1;
        }

        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            if (Entries.Count == 0)
            {
                MessageBox.Show("Nincs menteni való adat.", "Információ", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            var dlg = new SaveFileDialog
            {
                Filter = "Szövegfájl (*.txt)|*.txt|CSV fájl (*.csv)|*.csv|Minden fájl (*.*)|*.*",
                FileName = "vercukor_naplo.txt",
                DefaultExt = ".txt"
            };
            if (dlg.ShowDialog() == true)
            {
                try
                {
                    using var sw = new StreamWriter(dlg.FileName, false, System.Text.Encoding.UTF8);
                    sw.WriteLine("Nap;Napszak;Mérés ideje;Érték (mmol/L);Megjegyzés");
                    foreach (var eItem in Entries)
                    {
                        string comment = string.IsNullOrEmpty(eItem.Status) ? "" : eItem.Status;
                        sw.WriteLine($"{eItem.Day};{eItem.TimeOfDay};{eItem.Timing};{eItem.Value.ToString(CultureInfo.InvariantCulture)};{comment}");
                    }
                    MessageBox.Show("Mentés sikeres.", "Kész", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Hiba a mentés során: {ex.Message}", "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }

    public class BloodSugarEntry
    {
        public int Day { get; }
        public string TimeOfDay { get; }
        public string Timing { get; }
        public double Value { get; }
        public string Status { get; }

        public BloodSugarEntry(int day, string timeOfDay, string timing, double value)
        {
            Day = day;
            TimeOfDay = timeOfDay;
            Timing = timing;
            Value = Math.Round(value, 1);

           
            if (timing.StartsWith("Éhgyomorra"))
            {
                if (Value < 3.9) Status = "Alacsony";
                else if (Value > 5.6) Status = "Magas";
                else Status = "";
            }
            else 
            {
                if (Value < 3.9) Status = "Alacsony";
                else if (Value >= 7.8) Status = "Magas";
                else Status = "";
            }
        }

        public override string ToString()
        {
            var statusPart = string.IsNullOrEmpty(Status) ? "" : $" - {Status}";
            return $"Nap {Day} - {TimeOfDay} - {Timing}: {Value:0.0} mmol/L{statusPart}";
        }
    }
}