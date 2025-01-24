using System;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Threading;

namespace SqlAmoz.DB
{
    public static class Database
    {
        static string[] PersonNames = new string[] { "Ali", "Mohammad", "Hassan", "Mahdi", "Sajjad", "Sadegh" };
        static string[] CourseNames = new string[] { "Programming", "Math", "Physic", "Chemistry", "Sport" };
        static string[] ProgrammingLanguage = new string[] { "Python", "Javascript", "C#", "Java", "C" };
        static string[] GradeStrings = new string[] { "High", "Normal", "Low" };
        static float[] Scores = new float[] { 10, 10.5F, 11, 11.5F, 12, 12.5F, 13, 13.5F, 14, 14.5F, 15, 15.5F, 16, 16.5F, 17, 17.5F, 18, 18.5F, 19, 19.5F, 20 };
        const string Filename = "mydb.sqlite";

        public static SQLiteConnection Connection { get => new SQLiteConnection("Data Source=" + Filename + ";Version=3;"); }

        public static void InitDatabase()
        {
            if (!File.Exists(Filename))
            {
                var frm = new WaitForm();

                frm.Show();

                var th = new Thread(InitDatabaseAsync);
                th.Start();
                th.Join();

                frm.Close();
            }
        }

        private static void InitDatabaseAsync()
        {
            SQLiteConnection conn = Connection;
            conn.Open();

            using (SQLiteCommand cmd = new SQLiteCommand("CREATE TABLE Persons(Id int, Name varchar(255), Age int, Grade int, [Score Quran] REAL)", conn))
                cmd.ExecuteNonQuery();

            using (SQLiteCommand cmd = new SQLiteCommand("CREATE TABLE Courses(Id int, PersonId int, Name varchar(255), Score REAL)", conn))
                cmd.ExecuteNonQuery();

            using (SQLiteCommand cmd = new SQLiteCommand("CREATE TABLE Programmings(Id int, PersonId int, Language varchar(255), Grade varchar(255))", conn))
                cmd.ExecuteNonQuery();

            var persons = InitPersons();
            using (SQLiteDataAdapter dataAdapter = new SQLiteDataAdapter("SELECT * FROM Persons", conn))
            {
                SQLiteCommandBuilder commandBuilder = new SQLiteCommandBuilder(dataAdapter);
                dataAdapter.InsertCommand = commandBuilder.GetInsertCommand();

                dataAdapter.Update(persons);
            }

            var courses = InitCourses();
            using (SQLiteDataAdapter dataAdapter = new SQLiteDataAdapter("SELECT * FROM Courses", conn))
            {
                SQLiteCommandBuilder commandBuilder = new SQLiteCommandBuilder(dataAdapter);
                dataAdapter.InsertCommand = commandBuilder.GetInsertCommand();

                dataAdapter.Update(courses);
            }

            var programmings = InitProgrammings();
            using (SQLiteDataAdapter dataAdapter = new SQLiteDataAdapter("SELECT * FROM Programmings", conn))
            {
                SQLiteCommandBuilder commandBuilder = new SQLiteCommandBuilder(dataAdapter);
                dataAdapter.InsertCommand = commandBuilder.GetInsertCommand();

                dataAdapter.Update(programmings);
            }

            conn.Close();
        }

        private static DataTable InitPersons()
        {
            Random rnd = new Random(100);
            DataTable dt;

            dt = new DataTable("Persons");
            dt.Columns.Add("ID", typeof(int));
            dt.Columns.Add("Name", typeof(string));
            dt.Columns.Add("Age", typeof(int));
            dt.Columns.Add("Grade", typeof(int));
            dt.Columns.Add("Score Quran", typeof(float));

            for (int i = 0; i < 100; i++)
            {
                var dr = dt.NewRow();
                dr[0] = i + 1;
                dr[1] = PersonNames[rnd.Next() % PersonNames.Length];
                dr[2] = rnd.Next(10, 20);
                dr[3] = rnd.Next(4, 7);
                dr[4] = Scores[rnd.Next() % Scores.Length];
                dt.Rows.Add(dr);
            }

            return dt;
        }

        private static DataTable InitCourses()
        {
            Random rnd = new Random(200);
            DataTable dt;

            dt = new DataTable("Courses");
            dt.Columns.Add("ID", typeof(int));
            dt.Columns.Add("PersonId", typeof(int));
            dt.Columns.Add("Name", typeof(string));
            dt.Columns.Add("Score", typeof(float));

            var lst = Enumerable.Range(0, 100 * CourseNames.Length)
                .Select(i => new { rnd = rnd.Next(), person = i / CourseNames.Length, course = i % CourseNames.Length })
                .Where(x => x.person != 5 && x.person != 10)
                .OrderBy(x => x.rnd)
                .Select(x => new { person = x.person, course = x.course })
                .Take(300)
                .ToList();
            ;

            for (int i = 0; i < lst.Count; i++)
            {
                var data = lst[i];

                var dr = dt.NewRow();
                dr[0] = i + 1;
                dr[1] = data.person;
                dr[2] = CourseNames[data.course];
                dr[3] = Scores[rnd.Next() % Scores.Length];
                dt.Rows.Add(dr);
            }

            return dt;
        }

        private static DataTable InitProgrammings()
        {
            Random rnd = new Random(300);
            DataTable dt;

            dt = new DataTable("Programmings");
            dt.Columns.Add("ID", typeof(int));
            dt.Columns.Add("PersonId", typeof(int));
            dt.Columns.Add("Language", typeof(string));
            dt.Columns.Add("Grade", typeof(string));

            var lst = Enumerable.Range(0, 100 * ProgrammingLanguage.Length)
                .Select(i => new { rnd = rnd.Next(), person = i / ProgrammingLanguage.Length, prog = i % ProgrammingLanguage.Length })
                .Where(x => x.person != 6 && x.person != 21)
                .OrderBy(x => x.rnd)
                .Select(x => new { person = x.person, prog = x.prog })
                .Take(200)
                .ToList();
            ;

            for (int i = 0; i < lst.Count; i++)
            {
                var data = lst[i];

                var dr = dt.NewRow();
                dr[0] = i + 1;
                dr[1] = data.person;
                dr[2] = ProgrammingLanguage[data.prog];
                dr[3] = GradeStrings[rnd.Next() % GradeStrings.Length];
                dt.Rows.Add(dr);
            }

            return dt;
        }
    }
}
