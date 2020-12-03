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

namespace _2praktika
{
    /// <summary>
    /// Interaction logic for ControlTeacherViewStudents.xaml
    /// </summary>
    public partial class ControlTeacherViewStudents : UserControl
    {
        public static Delegate update;
        Repository repository = new Repository();
        private User student;
        private string subject;
        public ControlTeacherViewStudents()
        {
            InitializeComponent();
        }

        public void SetStudent(User student)
        {
            this.student = student;
        }

        public void SetSubject(string subject)
        {
            this.subject = subject;
        }

        private void buttonChangeGrade_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                panelViewGrade.Visibility = Visibility.Collapsed;
                panelEditGrade.Visibility = Visibility.Visible;
                comboBoxSelectGrade.Items.Clear();
                comboBoxSelectGrade.Items.Add("Pasirinkti");
                comboBoxSelectGrade.SelectedIndex = 0;
                for(int i=0;i<=10;i++)
                    comboBoxSelectGrade.Items.Add(i.ToString());
            }
            catch (Exception)
            {
                MessageBox.Show("change grade error");
            }
        }

        private void buttonConfirmGrade_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (comboBoxSelectGrade.SelectedItem.Equals("Pasirinkti"))
                    throw new Exception("Pirmiausia pasirinkite pažymį");
                repository.AddGrade(repository.GetUserId(student.GetUsername()),repository.GetSubjectId(subject),Convert.ToInt32(comboBoxSelectGrade.SelectedItem));
                update.DynamicInvoke();
                MessageBox.Show("Pažymys sėkmingai įvestas");
            }
            catch(Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        private void buttonCancelGrade_Click(object sender, RoutedEventArgs e)
        {
            panelEditGrade.Visibility = Visibility.Collapsed;
            panelViewGrade.Visibility = Visibility.Visible;
        }
    }
}
