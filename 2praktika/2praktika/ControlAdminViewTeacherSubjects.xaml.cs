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
    /// Interaction logic for ControlAdminViewTeacherSubjects.xaml
    /// </summary>
    public partial class ControlAdminViewTeacherSubjects : UserControl
    {
        public static Delegate update;
        Repository repository = new Repository();
        private string username;
        public ControlAdminViewTeacherSubjects()
        {
            InitializeComponent();
        }
        public void SetUsername(string username)
        {
            this.username = username;
        }

        private void buttonRemoveTeacherSubject_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Ar tikrai norite atskirti šį dalyką nuo dėstytojo?", "", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                try
                {
                    repository.UpdateSubjectTeacher(0,labelTeacherSubjectName.Content.ToString());
                    update.DynamicInvoke(username);
                }
                catch
                {
                    MessageBox.Show("remove teacher subject error");
                }
            }
        }
    }
}
