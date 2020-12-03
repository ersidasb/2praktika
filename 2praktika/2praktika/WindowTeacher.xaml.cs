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
using System.Windows.Shapes;

namespace _2praktika
{
    /// <summary>
    /// Interaction logic for WindowTeacher.xaml
    /// </summary>
    public partial class WindowTeacher : Window
    {
        delegate void updateDelegate();
        updateDelegate updateStudents;

        private User loggedInUser;
        Repository repository = new Repository();
        public WindowTeacher(User user)
        {
            try
            {
                InitializeComponent();
                loggedInUser = user;
                labelName.Content = user.GetName();
                labelSurname.Content = user.GetSurname();

                List<string> groupsList = new List<string>();
                groupsList = repository.GetGroupsList();
                comboBoxGroup.Items.Clear();
                comboBoxGroup.Items.Add("Pasirinkti");
                comboBoxGroup.SelectedIndex = 0;
                foreach (string group in groupsList)
                {
                    comboBoxGroup.Items.Add(group);
                }

                List<string> subjectsList = new List<string>();
                subjectsList = repository.GetSubjectsList(repository.GetUserId(user.GetUsername()));
                comboBoxSubject.Items.Clear();
                comboBoxSubject.Items.Add("Pasirinkti");
                comboBoxSubject.SelectedIndex = 0;
                foreach (string subject in subjectsList)
                {
                    comboBoxSubject.Items.Add(subject);
                }
                updateStudents = UpdateStudentsList;
                ControlTeacherViewStudents.update = updateStudents;
            }
            catch(Exception)
            {
                Close();
                throw new Exception("error logging in");
            }
        }

        private void UpdateStudentsList()
        {
            try
            {
                stackPanelStudents.Children.Clear();
                List<User> usersList = new List<User>();
                usersList = repository.GetUserList(repository.GetGroupId(comboBoxGroup.SelectedItem.ToString()));
                if (!usersList.Any())
                    throw new Exception("Šioje grupėje studentų nėra");
                foreach (User u in usersList)
                {
                    ControlTeacherViewStudents control = new ControlTeacherViewStudents();
                    control.labelStudentName.Content = u.GetName();
                    control.labelStudentSurname.Content = u.GetSurname();
                    int grade = repository.GetGrade(repository.GetUserId(u.GetUsername()), repository.GetSubjectId(comboBoxSubject.SelectedItem.ToString()));
                    if (grade == -1)
                        control.labelGrade.Content = "Neįvesta";
                    else
                        control.labelGrade.Content = grade.ToString();
                    control.SetStudent(u);
                    control.SetSubject(comboBoxSubject.SelectedItem.ToString());
                    stackPanelStudents.Children.Add(control);
                }
            }
            catch (Exception exc)
            {
                throw new Exception(exc.Message);
            }
        }

        private void buttonLogout_Click(object sender, RoutedEventArgs e)
        {
            MainWindow window = new MainWindow();
            Close();
            window.Show();
        }

        private void buttonSearch_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                stackPanelStudents.Children.Clear();
                if (comboBoxGroup.SelectedItem.Equals("Pasirinkti"))
                    throw new Exception("Pasirinkite grupę");
                if (comboBoxSubject.SelectedItem.Equals("Pasirinkti"))
                    throw new Exception("Pasirinkite dalyką");
                if (repository.GetSubjectGroupId(repository.GetSubjectId(comboBoxSubject.SelectedItem.ToString()), repository.GetGroupId(comboBoxGroup.SelectedItem.ToString())) == 0)
                    throw new Exception("Šis dalykas nepriklauso šiai grupei");
                UpdateStudentsList();
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }
    }
}
