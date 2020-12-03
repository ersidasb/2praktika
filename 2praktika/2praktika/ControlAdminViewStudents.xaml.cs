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
    /// Interaction logic for ControlAdminViewStudents.xaml
    /// </summary>
    public partial class ControlAdminViewStudents : UserControl
    {
        public static Delegate update;
        Repository repository = new Repository();
        private User student = null;
        public ControlAdminViewStudents()
        {
            InitializeComponent();
        }

        public void SetStudent(User user)
        {
            student = user;
        }
        private void buttonRemoveStudent_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Ar tikrai norite pašalinti šį studentą?", "", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                try
                {
                    repository.RemoveUser(repository.GetUserId(student.GetUsername()));
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
