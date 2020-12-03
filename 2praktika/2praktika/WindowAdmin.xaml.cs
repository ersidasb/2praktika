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
    /// Interaction logic for WindowAdmin.xaml
    /// </summary>
    public partial class WindowAdmin : Window
    {
        delegate void updateDelegate();
        updateDelegate updateStudents;
        updateDelegate updateTeachers;
        updateDelegate updateGroups;
        updateDelegate updateSubjects;

        delegate void teacherUpdateDelegate(string username);
        teacherUpdateDelegate updateTeacherSubjects;
        delegate void showTeacherWindowDelegate(User teacher);
        showTeacherWindowDelegate windowViewTeacher;

        delegate void subjectUpdateDelegate(string subjectName);
        subjectUpdateDelegate updateSubjectGroups;
        delegate void showSubjectWindowDelegate(string subjectName);
        showSubjectWindowDelegate windowViewSubject;

        Repository repository = new Repository();
        private User loggedInUser;
        public WindowAdmin()
        {
            InitializeComponent();

            updateStudents = UpdateStudentsList;
            updateTeachers = UpdateTeachersList;
            updateGroups = UpdateGroupsList;
            updateTeacherSubjects = UpdateTeacherSubjectsList;
            updateSubjectGroups = UpdateSubjectGroupsList;
            updateSubjects = UpdateSubjectsList;
            ControlAdminViewStudents.update = updateStudents;
            ControlAdminViewTeachers.update = updateTeachers;
            ControlAdminViewGroups.update = updateGroups;
            ControlAdminViewTeacherSubjects.update = updateTeacherSubjects;
            ControlAdminViewSubjectGroups.update = updateSubjectGroups;
            ControlAdminViewSubjects.update = updateSubjects;

            windowViewTeacher = ShowViewTeacherWindow;
            ControlAdminViewTeachers.window = windowViewTeacher;

            windowViewSubject = ShowViewSubjectWindow;
            ControlAdminViewSubjects.window = windowViewSubject;
        }
        public void SetLoggedInUser(User loggedInUser)
        {
            this.loggedInUser = loggedInUser;
            labelName.Content = loggedInUser.GetName();
            labelSurname.Content = loggedInUser.GetSurname();
        }

        //------------------------------LIST UPDATES-------------------------------------------------
        private void UpdateStudentsList()
        {
            try
            {
                stackPanelStudents.Children.Clear();
                List<User> usersList = repository.GetUserList(repository.GetGroupId(comboBoxGroup.SelectedItem.ToString()));
                foreach (User u in usersList)
                {
                    ControlAdminViewStudents control = new ControlAdminViewStudents();
                    control.labelStudentName.Content = u.GetName();
                    control.labelStudentSurname.Content = u.GetSurname();
                    control.labelStudentGroup.Content = repository.GetGroupName(u.GetGroupId());
                    control.SetStudent(u);
                    stackPanelStudents.Children.Add(control);
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        private void UpdateTeachersList()
        {
            try
            {
                stackPanelTeachers.Children.Clear();
                List<User> usersList = repository.GetUserList(1);
                foreach (User u in usersList)
                {
                    ControlAdminViewTeachers control = new ControlAdminViewTeachers();
                    control.labelTeacherName.Content = u.GetName();
                    control.labelTeacherSurname.Content = u.GetSurname();
                    control.SetTeacher(u);
                    stackPanelTeachers.Children.Add(control);
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        private void UpdateTeacherSubjectsList(string username)
        {
            try
            {
                stackPanelTeacherSubjects.Children.Clear();
                List<string> subjectsList = repository.GetSubjectsList(repository.GetUserId(username));
                foreach (string s in subjectsList)
                {
                    ControlAdminViewTeacherSubjects control = new ControlAdminViewTeacherSubjects();
                    control.labelTeacherSubjectName.Content = s;
                    control.SetUsername(username);
                    stackPanelTeacherSubjects.Children.Add(control);
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        private void UpdateGroupsList()
        {
            stackPanelGroups.Children.Clear();
            List<string> groupsList = repository.GetGroupsList();
            foreach (string group in groupsList)
            {
                ControlAdminViewGroups control = new ControlAdminViewGroups();
                control.labelGroupName.Content = group;
                stackPanelGroups.Children.Add(control);
            }
        }

        private void UpdateSubjectsList()
        {
            stackPanelSubjects.Children.Clear();
            List<string> subjectsList = repository.GetSubjectsList(-1);
            foreach (string subject in subjectsList)
            {
                ControlAdminViewSubjects control = new ControlAdminViewSubjects();
                control.labelSubjectName.Content = subject;
                stackPanelSubjects.Children.Add(control);
            }
        }

        private void UpdateSubjectGroupsList(string subjectName)
        {
            try
            {
                stackPanelSubjectGroups.Children.Clear();
                List<string> subjectsList = repository.GetSubjectGroupsList(repository.GetSubjectId(subjectName));
                foreach (string groupName in subjectsList)
                {
                    ControlAdminViewSubjectGroups control = new ControlAdminViewSubjectGroups();
                    control.labelSubjectGroupName.Content = groupName;
                    control.setSubjectName(labelSubjectName.Content.ToString());
                    stackPanelSubjectGroups.Children.Add(control);
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }
        //show subject panel delegate
        //------------------------------WINDOW MANAGEMENT---------------------------------------------

        private void ShowViewTeacherWindow(User teacher)
        {
            try
            {
                HidePanels();
                mainPanelViewTeacher.Visibility = Visibility.Visible;
                labelTeacherName.Content = teacher.GetName();
                labelTeacherSurname.Content = teacher.GetSurname();
                labelTeacherUsername.Content = teacher.GetUsername();
                UpdateTeacherSubjectsList(teacher.GetUsername());
            }
            catch(Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        private void ShowViewSubjectWindow(string subjectName)
        {
            try
            {
                HidePanels();
                mainPanelViewSubject.Visibility = Visibility.Visible;
                labelSubjectName.Content = subjectName;
                UpdateSubjectGroupsList(subjectName);
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }
        private void HidePanels()
        {
            mainPanelStudents.Visibility = Visibility.Collapsed;
            mainPanelAddStudent.Visibility = Visibility.Collapsed;

            mainPanelTeachers.Visibility = Visibility.Collapsed;
            mainPanelAddTeacher.Visibility = Visibility.Collapsed;
            mainPanelViewTeacher.Visibility = Visibility.Collapsed;
            mainPanelAddTeacherSubject.Visibility = Visibility.Collapsed;

            mainPanelGroups.Visibility = Visibility.Collapsed;
            mainPanelAddGroup.Visibility = Visibility.Collapsed;

            mainPanelSubjects.Visibility = Visibility.Collapsed;
            mainPanelAddSubject.Visibility = Visibility.Collapsed;
            mainPanelViewSubject.Visibility = Visibility.Collapsed;
            mainPanelAddSubjectGroup.Visibility = Visibility.Collapsed;
        }

        //--------------------------------BUTTONS-----------------------------------------------------

        private void buttonLogout_Click(object sender, RoutedEventArgs e)
        {
            MainWindow window = new MainWindow();
            Close();
            window.Show();
        }
        //--------------------------------STUDENTS-----------------------------------------------------

        private void buttonStudents_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                HidePanels();
                mainPanelStudents.Visibility = Visibility.Visible;
                stackPanelStudents.Children.Clear();
                List<string> groupsList = new List<string>();
                groupsList = repository.GetGroupsList();
                comboBoxGroup.Items.Clear();
                comboBoxGroup.Items.Add("Pasirinkti");
                comboBoxGroup.SelectedIndex = 0;
                foreach (string group in groupsList)
                {
                    comboBoxGroup.Items.Add(group);
                }
            }
            catch(Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        private void buttonSeachStudents_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (comboBoxGroup.SelectedItem.ToString().Equals("Pasirinkti"))
                    throw new Exception("Pirmiausia pasirinkite studentų grupę");
                UpdateStudentsList();
            }
            catch(Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        private void buttonAddStudent_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                HidePanels();
                textBoxStudentName.Text = "";
                textBoxStudentSurname.Text = "";
                mainPanelAddStudent.Visibility = Visibility.Visible;
                List<string> groupsList = new List<string>();
                groupsList = repository.GetGroupsList();
                comboBoxStudentGroup.Items.Clear();
                comboBoxStudentGroup.Items.Add("Pasirinkti");
                comboBoxStudentGroup.SelectedIndex = 0;
                foreach (string group in groupsList)
                {
                    comboBoxStudentGroup.Items.Add(group);
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        private void buttonCancelStudent_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                HidePanels();
                stackPanelStudents.Children.Clear();
                mainPanelStudents.Visibility = Visibility.Visible;
            }
            catch(Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        private void buttonConfirmStudent_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (comboBoxStudentGroup.SelectedItem.ToString().Equals("Pasirinkti"))
                    throw new Exception("Pasirinkite grupę");
                if (textBoxStudentName.Text.Trim(' ').Equals("") || textBoxStudentSurname.Text.Trim(' ').Equals(""))
                    throw new Exception("Užpildykite laukus");
                if (repository.GetUserId(textBoxStudentName.Text) != 0)
                    throw new Exception("Toks vardas jau egsiztuoja");
                User student = new User(textBoxStudentName.Text, textBoxStudentSurname.Text, textBoxStudentName.Text, textBoxStudentSurname.Text, repository.GetGroupId(comboBoxStudentGroup.SelectedItem.ToString()));
                repository.AddUser(student);
                MessageBox.Show($" Studentas {student.GetName()} {student.GetSurname()} sėkmingai sukurtas \n Studento prisijungimo vardas: {student.GetUsername()}\n Studento slaptažodis: {student.GetPassword()} \n Studento grupė: {repository.GetGroupName(student.GetGroupId())}");
                HidePanels();
                stackPanelStudents.Children.Clear();
                mainPanelStudents.Visibility = Visibility.Visible;
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }
        //--------------------------------TEACHERS-----------------------------------------------------

        private void buttonTeachers_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                HidePanels();
                mainPanelTeachers.Visibility = Visibility.Visible;
                UpdateTeachersList();
            }
            catch(Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        private void buttonAddTeacher_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                textBoxTeacherName.Text = "";
                textBoxTeacherSurname.Text = "";
                HidePanels();
                mainPanelAddTeacher.Visibility = Visibility.Visible;
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        private void buttonCancelTeacher_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                HidePanels();
                mainPanelTeachers.Visibility = Visibility.Visible;
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        private void buttonConfirmTeacher_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (textBoxTeacherName.Text.Trim(' ').Equals("") || textBoxTeacherSurname.Text.Trim(' ').Equals(""))
                    throw new Exception("Užpildykite laukus");
                if (repository.GetUserId(textBoxTeacherName.Text.ToString()) != 0)
                    throw new Exception("Toks vardas jau egsiztuoja");
                User teacher = new User(textBoxTeacherName.Text, textBoxTeacherSurname.Text, textBoxTeacherName.Text, textBoxTeacherSurname.Text, 1);
                repository.AddUser(teacher);
                MessageBox.Show($" Dėstytojas {teacher.GetName()} {teacher.GetSurname()} sėkmingai sukurtas \n Dėstytojo prisijungimo vardas: {teacher.GetUsername()}\n Dėstytojo slaptažodis: {teacher.GetPassword()}");
                HidePanels();
                updateTeachers();
                mainPanelTeachers.Visibility = Visibility.Visible;
                textBoxStudentName.Text = "";
                textBoxStudentSurname.Text = "";
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        private void buttonAddTeacherSubject_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                HidePanels();
                mainPanelAddTeacherSubject.Visibility = Visibility.Visible; 
                List<string> subjectsList = new List<string>();
                subjectsList = repository.GetSubjectsList(0);
                comboBoxTeacherSubjects.Items.Clear();
                comboBoxTeacherSubjects.Items.Add("Pasirinkti");
                comboBoxTeacherSubjects.SelectedIndex = 0;
                foreach (string subject in subjectsList)
                {
                    comboBoxTeacherSubjects.Items.Add(subject);
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        private void buttonCancelTeacherSubject_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                HidePanels();
                mainPanelViewTeacher.Visibility = Visibility.Visible;
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        private void buttonConfirmTeacherSubject_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (comboBoxTeacherSubjects.SelectedItem.ToString().Equals("Pasirinkti"))
                    throw new Exception("Pirmiausia pasirinkite dalyką");
                User teacher = repository.GetUser(repository.GetUserId(labelTeacherUsername.Content.ToString()));
                repository.UpdateSubjectTeacher(repository.GetUserId(teacher.GetUsername()),comboBoxTeacherSubjects.SelectedItem.ToString());
                MessageBox.Show($"Dalykas {comboBoxTeacherSubjects.SelectedItem}\nsėkmingai priskirtas dėstytojui {teacher.GetName()} {teacher.GetSurname()}");
                HidePanels();
                ShowViewTeacherWindow(teacher);
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }
        //---------------------------------GROUPS----------------------------------------------------

        private void buttonGroups_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                HidePanels();
                mainPanelGroups.Visibility = Visibility.Visible;
                UpdateGroupsList();
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        private void buttonAddGroup_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                HidePanels();
                textBoxGroupName.Text = "";
                mainPanelAddGroup.Visibility = Visibility.Visible;
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        private void buttonCancelGroup_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                HidePanels();
                mainPanelGroups.Visibility = Visibility.Visible;
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        private void buttonConfirmGroup_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (textBoxGroupName.Text.Trim(' ').Equals(""))
                    throw new Exception("Užpildykite laukus");
                if (repository.GetGroupId(textBoxGroupName.Text.ToString()) != 0)
                    throw new Exception("Tokia grupė jau egzistuoja");
                repository.AddGroup(textBoxGroupName.Text.ToString());
                MessageBox.Show($"Grupė {textBoxGroupName.Text} sėkmingai sukurta");
                HidePanels();
                UpdateGroupsList();
                mainPanelGroups.Visibility = Visibility.Visible;
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }
        //---------------------------------SUBJECTS----------------------------------------------------

        private void buttonSubjects_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                HidePanels();
                mainPanelSubjects.Visibility = Visibility.Visible;
                UpdateSubjectsList();
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        private void buttonAddSubject_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                HidePanels();
                textBoxSubjectName.Text = "";
                mainPanelAddSubject.Visibility = Visibility.Visible;
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        private void buttonCancelSubject_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                HidePanels();
                mainPanelSubjects.Visibility = Visibility.Visible;
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        private void buttonConfirmSubject_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (textBoxSubjectName.Text.Trim(' ').Equals(""))
                    throw new Exception("Užpildykite laukus");
                if (repository.GetSubjectId(textBoxSubjectName.Text.ToString()) != 0)
                    throw new Exception("Toks dalykas jau egzistuoja");
                repository.AddSubject(textBoxSubjectName.Text.ToString());
                MessageBox.Show($"Dalykas {textBoxSubjectName.Text} sėkmingai sukurta");
                HidePanels();
                UpdateSubjectsList();
                mainPanelSubjects.Visibility = Visibility.Visible;
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        private void buttonAddSubjectGroup_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                HidePanels();
                mainPanelAddSubjectGroup.Visibility = Visibility.Visible;
                List<string> groupsList = new List<string>();
                groupsList = repository.GetGroupsList();
                comboBoxSubjectGroup.Items.Clear();
                comboBoxSubjectGroup.Items.Add("Pasirinkti");
                comboBoxSubjectGroup.SelectedIndex = 0;
                foreach (string group in groupsList)
                {
                    comboBoxSubjectGroup.Items.Add(group);
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        private void buttonCancelSubjectGroup_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                HidePanels();
                mainPanelViewSubject.Visibility = Visibility.Visible;
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
            //subject window delegate
        }

        private void buttonConfirmSubjectGroup_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (comboBoxSubjectGroup.SelectedItem.ToString().Equals("Pasirinkti"))
                    throw new Exception("Pirmiausia pasirinkite grupę");
                if (repository.GetSubjectGroupId(repository.GetSubjectId(labelSubjectName.Content.ToString()),repository.GetGroupId(comboBoxSubjectGroup.SelectedItem.ToString())) != 0)
                    throw new Exception("Ši grupė jau yra priskirta šiam dalykui");
                repository.AddSubjectGroup(repository.GetSubjectId(labelSubjectName.Content.ToString()),repository.GetGroupId(comboBoxSubjectGroup.SelectedItem.ToString()));
                MessageBox.Show($"Grupė {comboBoxSubjectGroup.SelectedItem}\nsėkmingai priskirta dalykui {labelSubjectName.Content}");
                HidePanels();
                ShowViewSubjectWindow(labelSubjectName.Content.ToString());
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }
    }
}
