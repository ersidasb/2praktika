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
    /// Interaction logic for ControlAdminViewTeachers.xaml
    /// </summary>
    public partial class ControlAdminViewTeachers : UserControl
    {
        public static Delegate update;
        public static Delegate window;
        Repository repository = new Repository();
        private User teacher = null;
        public ControlAdminViewTeachers()
        {
            InitializeComponent();
        }

        public void SetTeacher(User user)
        {
            teacher = user;
        }

        private void buttonViewTeacher_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                window.DynamicInvoke(teacher);
            }
            catch
            {
                MessageBox.Show("error");
            }
        }

        private void buttonRemoveTeacher_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Ar tikrai norite pašalinti šį dėstytoją?", "", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                try
                {
                    List<string> subjectList = repository.GetSubjectsList(repository.GetUserId(teacher.GetUsername()));
                    foreach (string subject in subjectList)
                    {
                        repository.UpdateSubjectTeacher(0, subject);
                    }
                    repository.RemoveUser(repository.GetUserId(teacher.GetUsername()));
                    update.DynamicInvoke();
                }
                catch
                {
                    MessageBox.Show("error");
                }
            }
        }
    }
}
